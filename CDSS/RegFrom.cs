using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net.Mail;
using CDSSSystemData;

namespace CDSS
{
    public partial class RegFrom : Form
    {
        public RegFrom()
        {
            InitializeComponent();
            txb_Machine.Text = RegMachineClass.getMNum();
        }

        public string RNum = string.Empty;

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (txb_Reg.Text.Length == 32)
            {
                if (RNum.ToUpper() == txb_Reg.Text.ToUpper())
                {
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\NGCDSS", "RegNum", txb_Reg.Text.ToUpper(), RegistryValueKind.Unknown);
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("软件已注册成功，可以正常使用！", "提示");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("注册码错误，请确认注册码是否填写正确！", "提示");
                }
            }
            else
            {
                MessageBox.Show("请输入32位注册码！", "提示");
            }
        }

        private void btn_End_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbName.Text.Trim())
                || string.IsNullOrEmpty(txbUnitName.Text.Trim())
                || string.IsNullOrEmpty(txbEmail.Text.Trim()))
            {
                MessageBox.Show("请将用户信息填写完整！", "提示");
                return;
            }

            if (DialogResult.OK == MessageBox.Show("请确认您所填写信息的正确性，以便您收到激活码.\r\n请务必确保您填写的邮箱可用！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                string strMessageBody = "name:{" + txbName.Text.Trim() + "}\n";
                strMessageBody += "unit:{" + txbUnitName.Text.Trim() + "}\n";
                strMessageBody += "email:{" + txbEmail.Text.Trim() + "}\n";
                strMessageBody += "ver:{" + GlobalData.UserInfo.CurrentAppVer + "}\n";
                strMessageBody += "mNum:{" + txb_Machine.Text.Trim() + "}";
                //发送邮件
                try
                {
                    SmtpClient mailClient = new SmtpClient("60.191.25.26");

                    //登陆SMTP服务器的身份验证.
                    mailClient.Credentials = new System.Net.NetworkCredential("cdssinfo", "vicocdss");

                    //cdssinfo@vico-lab.com发件人地址、收件人地址
                    MailMessage message = new MailMessage(new MailAddress("cdssinfo@vico-lab.com"), new MailAddress("cdssinfo@vico-lab.com"));

                    message.Body = strMessageBody;//邮件内容
                    message.Subject = "注册码申请_" + txbUnitName.Text.Trim();//邮件主题

                    //发送....
                    mailClient.Send(message);

                    MessageBox.Show("发送成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("发送失败,请重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}