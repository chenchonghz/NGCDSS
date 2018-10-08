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
using System.Runtime.InteropServices;//API �����ռ�
using CDSSDBAccess;

namespace CDSS
{
    public partial class Login : Form
    {
        //�Զ�������س���
        public const int NGCDSS_CONST_INT_DETECT_INTERVAL_MAX = 360;  //�������ʱ��
        public const int NGCDSS_CONST_INT_DETECT_INTERVAL_MIN = 5;   //��С�����ʱ��
        public const int NGCDSS_CONST_INT_DETECT_INTERVAL_DEF = 30; //Ĭ�ϼ����ʱ��

        public const int NGCDSS_CONST_INT_REMIND_INTERVAL_MAX = 30;  //������Ѽ��ʱ��
        public const int NGCDSS_CONST_INT_REMIND_INTERVAL_MIN = 2;   //��С���Ѽ��ʱ��
        public const int NGCDSS_CONST_INT_REMIND_INTERVAL_DEF = 5;   //Ĭ�����Ѽ��ʱ��


        //�Զ��������õ��ı���
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
        /// ����MD5����
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string Md5Security(string pwd)
        { 
            string pwd_MD5;  //���ܺ�����
            pwd_MD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
            return pwd_MD5;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
             if (this.txb_UserName.Text == "")
            {
                MessageBox.Show("�������û�����", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txb_UserName.Focus();
                return;
            }
            if (this.txb_UserPwd.Text == "")
            {
                MessageBox.Show("���������룡", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txb_UserPwd.Focus();
                return;
            }
            //PWD���ܺ������
            string pwd_MD5=Md5Security(this.txb_UserPwd.Text.ToString().Trim());

            try
            {
                DataTable table = DBAccess.GetUsersExsit(this.txb_UserName.Text.ToString().Trim());

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("���û������ڣ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txb_UserName.Text = "";
                    txb_UserPwd.Text = "";
                    txb_UserName.Focus();
                }
                else
                {
                    foreach (DataRow Row in table.Rows)
                    {
                        //�жϼ��ܺ�����������ݿ��е�PWD�Ƿ�һ��
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
                             *���ã���¼�ɹ��󣬻�ȡ�������û������һ�ε�¼ʱ�䣬��¼�������˴ε�¼����DB��ʱ���
                             *add by lch 090605
                            *********************************************************************************/
                            GlobalData.UserInfo.LastLoginTime = GlobalData.UserInfo.LoginConnDBTime;//�ϴε�¼��ʱ��������һ�ε�¼��ʱ��
                            GlobalData.UserInfo.LoginFrequency = GlobalData.UserInfo.LoginFrequency + 1;//�ϴε�¼��Ĵ���+1�������ڵ�¼�Ĵ���
                            GlobalData.UserInfo.LoginConnDBTime = DateTime.Parse(DBAccess.GetServerCurrentTime());//���ڵ�ʱ����Ǵ˴ε�¼��ʱ��
                            //GlobalData.UserInfo.SaveCaseTime = (DateTime)Row["SaveCaseTime"].ToString();
                            //TODO:���永����ʱ�������ڵ�����永����ʱ��

                            //���ڲ������ݿ⣬update��Щ���ݡ�
                            bool result = DBAccess.UpdateUsers();
                            if (!result)
                            {
                                MessageBox.Show("��¼�����쳣��������ϵcdssinfo@vico-lab.com��", "��¼ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            /************************************************
                            *Add by lch 090610 ������������             
                            ************************************************/

                            if (int.Parse(System.Configuration.ConfigurationManager.AppSettings["AutoUpdate"]) == 1)
                            {
                                Timer m_timerNewVerMonitor = new Timer();
                                m_timerNewVerMonitor.Enabled = true;

                                if (GetUpdateInfo())
                                {
                                    //�ڵ���ShellExecute����ʱ������nShowCmd�������ⲿ����ִ��ʱ�����Ƿ���ʾ����ʾģʽ��n_ShowWinMode����ָ����
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
                                    //������������
                                    try
                                    {
                                        ShellExecute(IntPtr.Zero, new StringBuilder(""), new StringBuilder(m_strMeDSysUpdatePath), new StringBuilder(m_strCurrentAppName), new StringBuilder(""), SW_SHOW);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("�Զ�����ʧ�ܣ������Ի���ϵcdssinfo@vico-lab.com��\n\n��ϸ��Ϣ����ȡ�Զ����³���������Ϣʧ�ܡ�", "�Զ�����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    //�����°汾���
                                    m_timerNewVerMonitor.Interval = m_nDetectInterval;
                                    m_timerNewVerMonitor.Start();
                                }
                                else
                                {
                                    MessageBox.Show("�Զ����³���û�а�װ�����ò���ȷ���޷�����������", "�Զ�����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            MessageBox.Show("����������������룡", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txb_UserPwd.Text = "";
                            txb_UserPwd.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("��¼�����쳣��������ϵcdssinfo@vico-lab.com��", "��¼ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txb_UserName.Text = "";
                txb_UserPwd.Text = "";
                txb_UserName.Focus();
            }
        }
        
        //�޸�Jyl��081223��BugA3��
        private void btn_End_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK && MessageBox.Show("�Ƿ��˳�����ҽ���ٴ�����֧�������", "��ʾ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
        }


        /**********************************************************
         * add by lch 090610 �����Զ����������API������GetUpdateInfo()
         **********************************************************/

        //����˵����section��INI�ļ��еĶ������ƣ�key��INI�ļ��еĹؼ��֣�
        //def���޷���ȡʱ��ʱ���ȱʡ��ֵ��retVal����ȡ��ֵ��size����ֵ�Ĵ�С��
        //filePath��INI�ļ�������·�������ơ�
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileInt(string section, string key, int def, string filePath);


        [DllImport("shell32.dll")]
        public static extern int ShellExecute(IntPtr hwnd, StringBuilder lpOperation, StringBuilder lpFile, StringBuilder lpParameters, StringBuilder lpDirectory, int nShowCmd);

        /// <summary>
        /// ��ȡ�Զ������������Ϣ
        /// </summary>
        /// <returns>bool</returns>
        private bool GetUpdateInfo()
        {
            // ��ȡӦ�ó�������Ŀ¼�ĸ�Ŀ¼
            string strParentFolderPath = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
            if (strParentFolderPath.LastIndexOf("\\") == -1)
                return false;

            //Marked by ZQY@20090618
            //else
            //    strParentFolderPath = strParentFolderPath.Substring(0, strParentFolderPath.LastIndexOf("\\"));

            //��ȡ�Զ���������·��
            m_strMeDSysUpdatePath = strParentFolderPath + "\\MeDSysUpdate\\MeDSysUpdate.exe";
            if (!System.IO.File.Exists(m_strMeDSysUpdatePath))
                return false;

            //��ȡ�Զ��������������ļ�·��
            m_strIniFilePath = strParentFolderPath + "\\MeDSysUpdate\\MeDSysUpdate.ini";
            if (!System.IO.File.Exists(m_strIniFilePath))
                return false;

            //��ȡ��ǰӦ�ó�����
            StringBuilder strTempAPPName = new StringBuilder();
            GetPrivateProfileString("CurrentAppInfo", "AppName", String.Empty, strTempAPPName, 256, m_strIniFilePath);
            m_strCurrentAppName = strTempAPPName.ToString();

            //��ȡ��ǰ�汾��()
            StringBuilder strTempVision = new StringBuilder();
            GetPrivateProfileString("CurrentAppInfo", "AppVer", String.Empty, strTempVision, 256, m_strIniFilePath);
            m_strCurrentAppVer = strTempVision.ToString();
            GlobalData.UserInfo.CurrentAppVer = strTempVision.ToString();

            //��ȡ�����ʱ��
            m_nDetectInterval = GetPrivateProfileInt("AppSetting", "DetectInterval", 30, m_strIniFilePath);

            //��ȡ���Ѽ��ʱ��
            m_nRemindInterval = GetPrivateProfileInt("AppSetting", "RemindInterval", 5, m_strIniFilePath);

            //ȷ�����ʱ��ֵ����Ҫ��
            if (m_nDetectInterval > NGCDSS_CONST_INT_DETECT_INTERVAL_MAX || m_nDetectInterval < NGCDSS_CONST_INT_DETECT_INTERVAL_MIN)
                m_nDetectInterval = NGCDSS_CONST_INT_DETECT_INTERVAL_DEF;
            if (m_nRemindInterval > NGCDSS_CONST_INT_REMIND_INTERVAL_MAX || m_nRemindInterval < NGCDSS_CONST_INT_REMIND_INTERVAL_MIN)
                m_nRemindInterval = NGCDSS_CONST_INT_REMIND_INTERVAL_DEF;
            if (m_nRemindInterval > m_nDetectInterval)
                m_nRemindInterval = m_nDetectInterval;

            //ת��Ϊ����
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