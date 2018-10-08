using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDSSUserPowerManager
{
    public partial class UserManage : Form
    {
        #region --Fields/Attributes/Propertys--

        #endregion

        #region --Constructor--

        public UserManage()
        {
            InitializeComponent();
            ListDataBind();
            this.Text = string.Format("帐号管理-当前登录用户：{0}", DBManager.LoginedUserID);
        }

        #endregion

        #region --Functions--

        /// <summary>
        /// 绑定显示用户信息的ListView
        /// </summary>
        private void ListDataBind()
        {
            lvUserList.Items.Clear();
            DataTable dt = DBManager.GetAllUsers();
            foreach (DataRow dr in dt.Rows)
            {
                string[] cols = new string[9];
                cols[0] = dr["UserID"].ToString().Trim();
                cols[1] = dr["UserName"].ToString().Trim();
                cols[2] = dr["Title"].ToString().Trim();
                cols[3] = dr["Company"].ToString().Trim();
                cols[4] = dr["Department"].ToString().Trim();
                cols[5] = dr["Phone"].ToString().Trim();
                cols[6] = dr["MailAddress"].ToString().Trim();
                cols[7] = dr["UserPower"].ToString().Trim().Equals("0") ? "医生" : "管理员";
                cols[8] = dr["SyncFlag"].ToString().Trim().Equals("0") ? "使用中" : "禁用";
                ListViewItem item = new ListViewItem(cols, 0);
                if (dr["SyncFlag"].ToString().Trim().Equals("0"))
                    item.ForeColor = Color.FromName("WindowText");
                else
                    item.ForeColor = Color.Red;
                lvUserList.Items.Add(item);
            }

        }

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

        private void UserMsgClear()
        {
            foreach (Control ctl in groupBox1.Controls)
            {
                if (ctl is TextBox || ctl is ComboBox)
                {
                    ctl.Text = ""; ctl.Enabled = true;
                }
            }
            lvUserList.SelectedItems.Clear();
        }

        #endregion

        #region --Events--

        private void btnAdduser_Click(object sender, EventArgs e)
        {
            label9.Visible = txtUserID.Text.Trim().Equals("");
            label10.Visible = txtPwd.Text.Trim().Equals("");
            label11.Visible = txtName.Text.Trim().Equals("");
            if (label9.Visible || label10.Visible || label11.Visible)
                MessageBox.Show("请填写必填信息！");
            else
            {
                string[] userMsg = new string[9];
                userMsg[0] = txtUserID.Text.Trim();
                userMsg[1] = Md5Security(txtPwd.Text.Trim());
                userMsg[2] = txtName.Text.Trim();
                userMsg[3] = cmb_depart.Text.Trim();
                userMsg[4] = cmb_costs.Text.Trim();
                userMsg[5] = txtPhone.Text.Trim();
                userMsg[6] = txtCompany.Text.Trim();
                userMsg[7] = chkPower.Checked ? "1" : "0";
                userMsg[8] = txtMail.Text.Trim();
                bool tag = false;
                try
                {
                    tag = DBManager.AddUser(userMsg);
                }
                catch
                {
                    MessageBox.Show("当前输入用户名已存在！");
                    return;
                }
                if (tag)
                    ListDataBind();
                else
                    MessageBox.Show("添加用户失败!");
            }
        }

        private void UserManage_Load(object sender, EventArgs e)
        {
            this.lvUserList.Columns.Add("用户ID", 75, HorizontalAlignment.Center);
            this.lvUserList.Columns.Add("姓名", 75, HorizontalAlignment.Center);
            this.lvUserList.Columns.Add("职称", 75, HorizontalAlignment.Center);
            this.lvUserList.Columns.Add("所在单位", 90, HorizontalAlignment.Center);
            this.lvUserList.Columns.Add("所在部门", 90, HorizontalAlignment.Center);
            this.lvUserList.Columns.Add("联系电话", 90, HorizontalAlignment.Center);
            this.lvUserList.Columns.Add("电子邮件", 90, HorizontalAlignment.Center);
            this.lvUserList.Columns.Add("角色", 60, HorizontalAlignment.Center);
            this.lvUserList.Columns.Add("是否停用", 60, HorizontalAlignment.Center);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            UserMsgClear();
            btnDelete.Enabled = true;
            chb_Useing.Enabled = true;
            chb_NotUseing.Enabled = true;
            chkPower.Enabled = true;
            txtUserID.Enabled = true;
        }

        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvUserList.SelectedItems.Count == 0)
                return;
            else
            {
                if (DialogResult.OK == MessageBox.Show("确认删除该用户？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    DBManager.DeleteUser(lvUserList.SelectedItems[0].Text);
                    UserMsgClear();
                    ListDataBind();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lvUserList.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择相应的用户进行修改！", "提示");
                return;
            }

            string[] userMsg = new string[9];
            userMsg[0] = lvUserList.SelectedItems[0].Text;
            userMsg[1] = txtName.Text.Trim();
            userMsg[2] = cmb_depart.Text.Trim();
            userMsg[3] = cmb_costs.Text.Trim();
            userMsg[4] = txtPhone.Text.Trim();
            userMsg[5] = txtCompany.Text.Trim();
            userMsg[6] = chkPower.Checked ? "1" : "0";
            userMsg[7] = txtMail.Text.Trim();
            userMsg[8] = (chb_Useing.Checked ? "0" : "1");

            if (DBManager.UpdateUser(userMsg))
            {
                MessageBox.Show("修改成功！");
                ListDataBind();
            }
            else
                MessageBox.Show("修改失败！请重试");
        }

        private void lvUserList_Click(object sender, EventArgs e)
        {
            if (lvUserList.SelectedItems.Count != 0)
            {
                ListViewItem item = lvUserList.SelectedItems[0];
                txtUserID.Text = item.Text;
                txtName.Text = item.SubItems[1].Text;
                cmb_costs.Text = item.SubItems[2].Text;
                txtCompany.Text = item.SubItems[3].Text;
                cmb_depart.Text = item.SubItems[4].Text;
                txtPhone.Text = item.SubItems[5].Text;
                txtMail.Text = item.SubItems[6].Text;
                chkPower.Checked = item.SubItems[7].Text != "医生" ? true : false;
                txtPwd.Text = "";
                txtPwd.Enabled = false;
                chb_Useing.Checked = item.SubItems[8].Text == "使用中" ? true : false;
                chb_NotUseing.Checked = !chb_Useing.Checked;

                btnDelete.Enabled = (item.Text == DBManager.LoginedUserID) ? false : true;
                chb_Useing.Enabled = (item.Text == DBManager.LoginedUserID) ? false : true;
                chb_NotUseing.Enabled = (item.Text == DBManager.LoginedUserID) ? false : true;
                chkPower.Enabled = (item.Text == DBManager.LoginedUserID) ? false : true;
                txtUserID.Enabled = (item.Text == DBManager.LoginedUserID) ? false : true;
            }
        }
    }
}