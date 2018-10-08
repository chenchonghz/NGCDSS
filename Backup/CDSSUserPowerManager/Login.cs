using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;

namespace CDSSUserPowerManager
{
    public partial class Login : Form
    {
        #region --Fields/Attributes/Propertys--

        #endregion

        #region --Constructor--

        public Login()
        {
            InitializeComponent();
        }

        #endregion

        #region --Functions--

        /// <summary>
        /// 进行MD5加密
        /// </summary>
        /// <param name="pwd">需要加密字符串</param>
        /// <returns></returns>
        private string Md5Security(string pwd)
        {
            string pwd_MD5;  //加密后数据
            pwd_MD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
            return pwd_MD5;
        }

        #endregion

        #region --Events--

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
            string pwd_MD5 = Md5Security(this.txb_UserPwd.Text.ToString().Trim());

            DataTable table = DBManager.GetUsersExsit(this.txb_UserName.Text.ToString().Trim());

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("该用户不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txb_UserName.Text = "";
                txb_UserPwd.Text = "";
                txb_UserName.Focus();
            }
            else
            {
                if (table.Rows[0]["UserPower"].ToString() != "1")
                {
                    MessageBox.Show("权限不足，不能登录用户管理！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txb_UserName.Text = "";
                    txb_UserPwd.Text = "";
                    txb_UserName.Focus();
                }
                else
                {                    
                    this.DialogResult = DialogResult.OK;
                    DBManager.LoginedUserID = table.Rows[0]["UserID"].ToString();
                    this.Close();
                }
            }
        }

        private void btn_End_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}