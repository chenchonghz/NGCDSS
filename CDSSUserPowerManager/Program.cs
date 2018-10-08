using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CDSSUserPowerManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login frmLogin = new Login();
            DialogResult drLogin = frmLogin.ShowDialog();
            if (drLogin == DialogResult.OK)
            {
                Application.Run(new UserManage());
            }             
        }
    }
}