using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSSystemData;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;//API 命名空间
using CDSSDBAccess;

namespace CDSS
{
    public partial class Login : Form
    {
        //自动升级相关常量
        public const int NGCDSS_CONST_INT_DETECT_INTERVAL_MAX = 360;  //最大检测间隔时间
        public const int NGCDSS_CONST_INT_DETECT_INTERVAL_MIN = 5;   //最小检测间隔时间
        public const int NGCDSS_CONST_INT_DETECT_INTERVAL_DEF = 30; //默认检测间隔时间

        public const int NGCDSS_CONST_INT_REMIND_INTERVAL_MAX = 30;  //最大提醒间隔时间
        public const int NGCDSS_CONST_INT_REMIND_INTERVAL_MIN = 2;   //最小提醒间隔时间
        public const int NGCDSS_CONST_INT_REMIND_INTERVAL_DEF = 5;   //默认提醒间隔时间


        //自动升级需用到的变量
        private string m_strMeDSysUpdatePath = String.Empty;
        private string m_strIniFilePath = String.Empty;
        private string m_strCurrentAppName = String.Empty;
        private string m_strCurrentAppVer = String.Empty;
        private int m_nDetectInterval = NGCDSS_CONST_INT_DETECT_INTERVAL_DEF * 60 * 1000;
        private int m_nRemindInterval = NGCDSS_CONST_INT_REMIND_INTERVAL_DEF * 60 * 1000;

        public Login()
        {
            InitializeComponent(); 
        }

        /// <summary>
        /// 进行MD5加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string Md5Security(string pwd)
        { 
            string pwd_MD5;  //加密后数据
            pwd_MD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
            return pwd_MD5;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
             if (this.txb_UserName.Text == "")
            {
                MessageBox.Show("请输入用户名！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txb_UserName.Focus();
                return;
            }
            if (this.txb_UserPwd.Text == "")
            {
                MessageBox.Show("请输入密码！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txb_UserPwd.Focus();
                return;
            }
            //PWD加密后的数据
            string pwd_MD5=Md5Security(this.txb_UserPwd.Text.ToString().Trim());

            try
            {
                DataTable table = DBAccess.GetUsersExsit(this.txb_UserName.Text.ToString().Trim());

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("该用户不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txb_UserName.Text = "";
                    txb_UserPwd.Text = "";
                    txb_UserName.Focus();
                }
                else
                {
                    foreach (DataRow Row in table.Rows)
                    {
                        //判断加密后的数据与数据库中的PWD是否一致
                        if (Row["UserPwd"].Equals(pwd_MD5))
                        {
                            GlobalData.UserInfo.Clear();
                            GlobalData.UserInfo.UserName = Row["UserName"].ToString();
                            GlobalData.UserInfo.UserID = Row["UserID"].ToString();
                            GlobalData.UserInfo.Department = Row["Department"].ToString();
                            GlobalData.UserInfo.Title = Row["Title"].ToString();
                            GlobalData.UserInfo.Phone = Row["Phone"].ToString();
                            GlobalData.UserInfo.Company = Row["Company"].ToString();
                            GlobalData.UserInfo.MailAddress = Row["MailAddress"].ToString();
                            if (Row["LastLoginTime"] != Convert.DBNull)
                            {
                                GlobalData.UserInfo.LastLoginTime = (DateTime)Row["LastLoginTime"];
                            }
                            if (Row["LoginFrequency"] != Convert.DBNull)
                            {
                                GlobalData.UserInfo.LoginFrequency = int.Parse(Row["LoginFrequency"].ToString());
                            }
                            if (Row["LoginConnDBTime"] != Convert.DBNull)
                            {
                                GlobalData.UserInfo.LoginConnDBTime = (DateTime)Row["LoginConnDBTime"];
                            }
                            if (Row["SaveCaseTime"] != Convert.DBNull)
                            {
                                GlobalData.UserInfo.SaveCaseTime = (DateTime)Row["SaveCaseTime"];
                            }

                            

                            /********************************************************************************
                             *作用：登录成功后，获取和设置用户的最后一次登录时间，登录次数、此次登录连接DB的时间等
                             *add by lch 090605
                            *********************************************************************************/
                            GlobalData.UserInfo.LastLoginTime = GlobalData.UserInfo.LoginConnDBTime;//上次登录的时间就是最后一次登录的时间
                            GlobalData.UserInfo.LoginFrequency = GlobalData.UserInfo.LoginFrequency + 1;//上次登录后的次数+1就是现在登录的次数
                            GlobalData.UserInfo.LoginConnDBTime = DateTime.Parse(DBAccess.GetServerCurrentTime());//现在的时间就是此次登录的时间
                            //GlobalData.UserInfo.SaveCaseTime = (DateTime)Row["SaveCaseTime"].ToString();
                            //TODO:保存案例的时间设置在点击保存案例的时候

                            //现在操作数据库，update这些数据。
                            bool result = DBAccess.UpdateUsers();
                            if (!result)
                            {
                                MessageBox.Show("登录出现异常错误，请联系cdssinfo@vico-lab.com！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            /************************************************
                            *Add by lch 090610 调用升级程序             
                            ************************************************/

                            if (int.Parse(System.Configuration.ConfigurationManager.AppSettings["AutoUpdate"]) == 1)
                            {
                                Timer m_timerNewVerMonitor = new Timer();
                                m_timerNewVerMonitor.Enabled = true;

                                if (GetUpdateInfo())
                                {
                                    //在调用ShellExecute函数时，设置nShowCmd参数，外部程序执行时窗口是否显示和显示模式由n_ShowWinMode参数指定，
                                    //#define SW_HIDE             0
                                    //#define SW_SHOWNORMAL       1
                                    //#define SW_NORMAL           1
                                    //#define SW_SHOWMINIMIZED    2
                                    //#define SW_SHOWMAXIMIZED    3
                                    //#define SW_MAXIMIZE         3
                                    //#define SW_SHOWNOACTIVATE   4
                                    //#define SW_SHOW             5
                                    //#define SW_MINIMIZE         6
                                    //#define SW_SHOWMINNOACTIVE  7
                                    //#define SW_SHOWNA           8
                                    //#define SW_RESTORE          9
                                    //#define SW_SHOWDEFAULT      10
                                    //#define SW_FORCEMINIMIZE    11
                                    //#define SW_MAX              11

                                    int SW_SHOW = 5;
                                    //调用升级程序
                                    try
                                    {
                                        ShellExecute(IntPtr.Zero, new StringBuilder(""), new StringBuilder(m_strMeDSysUpdatePath), new StringBuilder(m_strCurrentAppName), new StringBuilder(""), SW_SHOW);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("自动更新失败，请重试或联系cdssinfo@vico-lab.com。\n\n详细信息：读取自动更新程序配置信息失败。", "自动更新", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    //启动新版本监控
                                    m_timerNewVerMonitor.Interval = m_nDetectInterval;
                                    m_timerNewVerMonitor.Start();
                                }
                                else
                                {
                                    MessageBox.Show("自动更新程序没有安装或配置不正确，无法正常启动！", "自动更新", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                string strParentFolderPath = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
                                m_strIniFilePath = strParentFolderPath + "\\MeDSysUpdate\\MeDSysUpdate.ini";
                                StringBuilder strTempVision = new StringBuilder();
                                GetPrivateProfileString("CurrentAppInfo", "AppVer", String.Empty, strTempVision, 256, m_strIniFilePath);
                                m_strCurrentAppVer = strTempVision.ToString();
                                GlobalData.UserInfo.CurrentAppVer = strTempVision.ToString();
                            }
                            
                            this.DialogResult = DialogResult.OK;
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("密码错误，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txb_UserPwd.Text = "";
                            txb_UserPwd.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录出现异常错误，请联系cdssinfo@vico-lab.com！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txb_UserName.Text = "";
                txb_UserPwd.Text = "";
                txb_UserName.Focus();
            }
        }
        
        //修改Jyl，081223，BugA3，
        private void btn_End_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK && MessageBox.Show("是否退出保健医生临床决策支持软件？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
        }


        /**********************************************************
         * add by lch 090610 声明自动升级的相关API函数和GetUpdateInfo()
         **********************************************************/

        //参数说明：section：INI文件中的段落名称；key：INI文件中的关键字；
        //def：无法读取时候时候的缺省数值；retVal：读取数值；size：数值的大小；
        //filePath：INI文件的完整路径和名称。
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileInt(string section, string key, int def, string filePath);


        [DllImport("shell32.dll")]
        public static extern int ShellExecute(IntPtr hwnd, StringBuilder lpOperation, StringBuilder lpFile, StringBuilder lpParameters, StringBuilder lpDirectory, int nShowCmd);

        /// <summary>
        /// 获取自动升级的相关信息
        /// </summary>
        /// <returns>bool</returns>
        private bool GetUpdateInfo()
        {
            // 获取应用程序所在目录的父目录
            string strParentFolderPath = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
            if (strParentFolderPath.LastIndexOf("\\") == -1)
                return false;

            //Marked by ZQY@20090618
            //else
            //    strParentFolderPath = strParentFolderPath.Substring(0, strParentFolderPath.LastIndexOf("\\"));

            //获取自动升级程序路径
            m_strMeDSysUpdatePath = strParentFolderPath + "\\MeDSysUpdate\\MeDSysUpdate.exe";
            if (!System.IO.File.Exists(m_strMeDSysUpdatePath))
                return false;

            //获取自动升级程序配置文件路径
            m_strIniFilePath = strParentFolderPath + "\\MeDSysUpdate\\MeDSysUpdate.ini";
            if (!System.IO.File.Exists(m_strIniFilePath))
                return false;

            //读取当前应用程序名
            StringBuilder strTempAPPName = new StringBuilder();
            GetPrivateProfileString("CurrentAppInfo", "AppName", String.Empty, strTempAPPName, 256, m_strIniFilePath);
            m_strCurrentAppName = strTempAPPName.ToString();

            //读取当前版本号()
            StringBuilder strTempVision = new StringBuilder();
            GetPrivateProfileString("CurrentAppInfo", "AppVer", String.Empty, strTempVision, 256, m_strIniFilePath);
            m_strCurrentAppVer = strTempVision.ToString();
            GlobalData.UserInfo.CurrentAppVer = strTempVision.ToString();

            //读取检测间隔时间
            m_nDetectInterval = GetPrivateProfileInt("AppSetting", "DetectInterval", 30, m_strIniFilePath);

            //读取提醒间隔时间
            m_nRemindInterval = GetPrivateProfileInt("AppSetting", "RemindInterval", 5, m_strIniFilePath);

            //确保间隔时间值符合要求
            if (m_nDetectInterval > NGCDSS_CONST_INT_DETECT_INTERVAL_MAX || m_nDetectInterval < NGCDSS_CONST_INT_DETECT_INTERVAL_MIN)
                m_nDetectInterval = NGCDSS_CONST_INT_DETECT_INTERVAL_DEF;
            if (m_nRemindInterval > NGCDSS_CONST_INT_REMIND_INTERVAL_MAX || m_nRemindInterval < NGCDSS_CONST_INT_REMIND_INTERVAL_MIN)
                m_nRemindInterval = NGCDSS_CONST_INT_REMIND_INTERVAL_DEF;
            if (m_nRemindInterval > m_nDetectInterval)
                m_nRemindInterval = m_nDetectInterval;

            //转化为毫秒
            m_nDetectInterval *= 60 * 1000;
            m_nRemindInterval *= 60 * 1000;
            return true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}