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
                    MessageBox.Show("�����ע��ɹ�����������ʹ�ã�", "��ʾ");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("ע���������ȷ��ע�����Ƿ���д��ȷ��", "��ʾ");
                }
            }
            else
            {
                MessageBox.Show("������32λע���룡", "��ʾ");
            }
        }

        private void btn_End_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbName.Text.Trim())
                || string.IsNullOrEmpty(txbUnitName.Text.Trim())
                || string.IsNullOrEmpty(txbEmail.Text.Trim()))
            {
                MessageBox.Show("�뽫�û���Ϣ��д������", "��ʾ");
                return;
            }

            if (DialogResult.OK == MessageBox.Show("��ȷ��������д��Ϣ����ȷ�ԣ��Ա����յ�������.\r\n�����ȷ������д��������ã�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                string strMessageBody = "name:{" + txbName.Text.Trim() + "}\n";
                strMessageBody += "unit:{" + txbUnitName.Text.Trim() + "}\n";
                strMessageBody += "email:{" + txbEmail.Text.Trim() + "}\n";
                strMessageBody += "ver:{" + GlobalData.UserInfo.CurrentAppVer + "}\n";
                strMessageBody += "mNum:{" + txb_Machine.Text.Trim() + "}";
                //�����ʼ�
                try
                {
                    SmtpClient mailClient = new SmtpClient("60.191.25.26");

                    //��½SMTP�������������֤.
                    mailClient.Credentials = new System.Net.NetworkCredential("cdssinfo", "vicocdss");

                    //cdssinfo@vico-lab.com�����˵�ַ���ռ��˵�ַ
                    MailMessage message = new MailMessage(new MailAddress("cdssinfo@vico-lab.com"), new MailAddress("cdssinfo@vico-lab.com"));

                    message.Body = strMessageBody;//�ʼ�����
                    message.Subject = "ע��������_" + txbUnitName.Text.Trim();//�ʼ�����

                    //����....
                    mailClient.Send(message);

                    MessageBox.Show("���ͳɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("����ʧ��,�����ԣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}