using System;
using System.Collections.Generic;
using System.Windows.Forms;

using CDSSSystemData;

namespace CDSS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool NotExist;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, "CDSS", out NotExist);
            if (!NotExist) Environment.Exit(1);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
            GlobalData.UserInfo.Clear();
            GlobalData.UserInfo.UserName = "测试用户";
            GlobalData.UserInfo.UserID = "Admin";
            GlobalData.UserInfo.Department = string.Empty;
            GlobalData.UserInfo.Title = string.Empty;
            GlobalData.UserInfo.Phone = string.Empty;
            GlobalData.UserInfo.Company = string.Empty;
            GlobalData.UserInfo.MailAddress = string.Empty;
            GlobalData.UserInfo.LastLoginTime = DateTime.Now;
            GlobalData.UserInfo.LoginFrequency = 0;
            GlobalData.UserInfo.LoginConnDBTime = DateTime.Now;
            GlobalData.UserInfo.SaveCaseTime = DateTime.Now;

            Application.AddMessageFilter(new EventHandling());
            //获取当天操作日志
            UserLogOperate.GetTodayLog();
            Application.Run(new MainForm());
#else
            //修改by Jyl，081223，程序入口改为登录窗体
            Login frmLogin = new Login();
            DialogResult drLogin = frmLogin.ShowDialog();
            if (drLogin == DialogResult.OK)
            {
                Application.AddMessageFilter(new EventHandling());
                //获取当天操作日志
                UserLogOperate.GetTodayLog();
                Application.Run(new MainForm());
            }
#endif
        }  
    }
}