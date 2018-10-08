using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;

namespace CDSSUserRegist
{
    public partial class PassGenerator : Form
    {
        public PassGenerator()
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
        private void btn_Regist_Click(object sender, EventArgs e)
        {
            //PWD加密后的数据
            this.txbMd5Pwd.Text= Md5Security(this.txb_UserPwd.Text.ToString().Trim());
        }
    }
}