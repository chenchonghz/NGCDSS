using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using CDSSSystemData;

namespace CDSS
{
    public partial class UserFeedback : Form
    {
        public UserFeedback()
        {
            InitializeComponent();
        }


        List<string> strFilePath = new List<string> (); //附件的路径
        string strHost = String.Empty;//发送者邮箱的SMTP主机
        string strMailAddrFromName = String.Empty;//发送者邮箱登录名称
        string strMailAddrFromPwd = String.Empty;//发送者邮箱密码
        string strMailAddrFrom = String.Empty;//发送者邮箱地址
        string strMailAddrTo = String.Empty;//收件者的邮箱地址

        private void btnSend_Click(object sender, EventArgs e)
        {
            string strMessageBody = "你好！"+GlobalData.UserInfo.UserName +"真诚的提出我的意见!\n\n";//邮件内容
            string strMessageSubject = "CDSS_用户意见反馈_" + GlobalData.UserInfo.UserName + "_" + GlobalData.UserInfo.UserID + "_" + DateTime.Today.ToShortDateString();//邮件标题
            string strEaseUse = "\n保健医生临床决策支持软件易用性评价:\n\n";
            string strUsingResult = "\n保健医生临床决策支持软件应用效果评价:\n\n";


            #region 临床决策支持系统保健医生版易用性评价
            //易操作
            if (this.radioButton_easyUsing1_0.Checked)
                strEaseUse += "1、 "+this.groupBox_easyUsing1.Text + ":  " + radioButton_easyUsing1_0.Tag + "\n";
            else if (this.radioButton_easyUsing1_1.Checked)
                strEaseUse += "1、 " + this.groupBox_easyUsing1.Text + ":  " + radioButton_easyUsing1_1.Tag + "\n";
            else if (this.radioButton_easyUsing1_2.Checked)
                strEaseUse += "1、 " + this.groupBox_easyUsing1.Text + ":  " + radioButton_easyUsing1_2.Tag + "\n";
            else if (this.radioButton_easyUsing1_3.Checked)
                strEaseUse += "1、 " + this.groupBox_easyUsing1.Text + ":  " + radioButton_easyUsing1_3.Tag + "\n";
            else if (this.radioButton_easyUsing1_4.Checked)
                strEaseUse += "1、 " + this.groupBox_easyUsing1.Text + ":  " + radioButton_easyUsing1_4.Tag + "\n";
            
            //用于代谢综合征诊疗所需数据全面
            if (this.radioButton_easyUsing2_0.Checked)
                strEaseUse += "2、 " + this.groupBox_easyUsing2.Text + ":  " + radioButton_easyUsing2_0.Tag + "\n";
            else if (this.radioButton_easyUsing2_1.Checked)
                strEaseUse += "2、 " + this.groupBox_easyUsing2.Text + ":  " + radioButton_easyUsing2_1.Tag + "\n";
            else if (this.radioButton_easyUsing2_2.Checked)
                strEaseUse += "2、 " + this.groupBox_easyUsing2.Text + ":  " + radioButton_easyUsing2_2.Tag + "\n";
            else if (this.radioButton_easyUsing2_3.Checked)
                strEaseUse += "2、 " + this.groupBox_easyUsing2.Text + ":  " + radioButton_easyUsing2_3.Tag + "\n";
            else if (this.radioButton_easyUsing2_4.Checked)
                strEaseUse += "2、 " + this.groupBox_easyUsing2.Text + ":  " + radioButton_easyUsing2_4.Tag + "\n";
            //诊断过程分析易懂，易理解
            if (this.radioButton_easyUsing3_0.Checked)
                strEaseUse += "3、 " + this.groupBox_easyUsing3.Text + ":  " + radioButton_easyUsing3_0.Tag + "\n";
            else if (this.radioButton_easyUsing3_1.Checked)
                strEaseUse += "3、 " + this.groupBox_easyUsing3.Text + ":  " + radioButton_easyUsing3_1.Tag + "\n";
            else if (this.radioButton_easyUsing3_2.Checked)
                strEaseUse += "3、 " + this.groupBox_easyUsing3.Text + ":  " + radioButton_easyUsing3_2.Tag + "\n";
            else if (this.radioButton_easyUsing3_3.Checked)
                strEaseUse += "3、 " + this.groupBox_easyUsing3.Text + ":  " + radioButton_easyUsing3_3.Tag + "\n";
            else if (this.radioButton_easyUsing3_4.Checked)
                strEaseUse += "3、 " + this.groupBox_easyUsing3.Text + ":  " + radioButton_easyUsing3_4.Tag + "\n";
            //使用中提示合理
            if (this.radioButton_easyUsing4_0.Checked)
                strEaseUse += "4、 " + this.groupBox_easyUsing4.Text + ":  " + radioButton_easyUsing4_0.Tag + "\n";
            else if (this.radioButton_easyUsing4_1.Checked)
                strEaseUse += "4、 " + this.groupBox_easyUsing4.Text + ":  " + radioButton_easyUsing4_1.Tag + "\n";
            else if (this.radioButton_easyUsing4_2.Checked)
                strEaseUse += "4、 " + this.groupBox_easyUsing4.Text + ":  " + radioButton_easyUsing4_2.Tag + "\n";
            else if (this.radioButton_easyUsing4_3.Checked)
                strEaseUse += "4、 " + this.groupBox_easyUsing4.Text + ":  " + radioButton_easyUsing4_3.Tag + "\n";
            else if (this.radioButton_easyUsing4_4.Checked)
                strEaseUse += "4、 " + this.groupBox_easyUsing4.Text + ":  " + radioButton_easyUsing4_4.Tag + "\n";
            //运行速度令人满意
            if (this.radioButton_easyUsing5_0.Checked)
                strEaseUse += "5、 " + this.groupBox_easyUsing5.Text + ":  " + radioButton_easyUsing5_0.Tag + "\n";
            else if (this.radioButton_easyUsing5_1.Checked)
                strEaseUse += "5、 " + this.groupBox_easyUsing5.Text + ":  " + radioButton_easyUsing5_1.Tag + "\n";
            else if (this.radioButton_easyUsing5_2.Checked)
                strEaseUse += "5、 " + this.groupBox_easyUsing5.Text + ":  " + radioButton_easyUsing5_2.Tag + "\n";
            else if (this.radioButton_easyUsing5_3.Checked)
                strEaseUse += "5、 " + this.groupBox_easyUsing5.Text + ":  " + radioButton_easyUsing5_3.Tag + "\n";
            else if (this.radioButton_easyUsing5_4.Checked)
                strEaseUse += "5、 " + this.groupBox_easyUsing5.Text + ":  " + radioButton_easyUsing5_4.Tag + "\n";
            //没有妨碍到医生的判断，并给医生诊疗提供了帮助
            if (this.radioButton_easyUsing6_0.Checked)
                strEaseUse += "6、 " + this.groupBox_easyUsing6.Text + ":  " + radioButton_easyUsing6_0.Tag + "\n";
            else if (this.radioButton_easyUsing6_1.Checked)
                strEaseUse += "6、 " + this.groupBox_easyUsing6.Text + ":  " + radioButton_easyUsing6_1.Tag + "\n";
            else if (this.radioButton_easyUsing6_2.Checked)
                strEaseUse += "6、 " + this.groupBox_easyUsing6.Text + ":  " + radioButton_easyUsing6_2.Tag + "\n";
            else if (this.radioButton_easyUsing6_3.Checked)
                strEaseUse += "6、 " + this.groupBox_easyUsing6.Text + ":  " + radioButton_easyUsing6_3.Tag + "\n";
            else if (this.radioButton_easyUsing6_4.Checked)
                strEaseUse += "6、 " + this.groupBox_easyUsing6.Text + ":  " + radioButton_easyUsing6_4.Tag + "\n";
            #endregion

            #region 临床决策支持系统保健医生版应用效果评价
            //将代谢综合征诊疗过程流程化，有利于指导医生学习和掌握代谢综合征诊疗相关知识
            if (this.radioButton_UsingResult1_0.Checked)
                strUsingResult += "1、 " + this.groupBox_UsingResult1.Text + ":  " + radioButton_UsingResult1_0.Tag + "\n";
            else if (this.radioButton_UsingResult1_1.Checked)
                strUsingResult += "1、 " + this.groupBox_UsingResult1.Text + ":  " + radioButton_UsingResult1_1.Tag + "\n";
            else if (this.radioButton_UsingResult1_2.Checked)
                strUsingResult += "1、 " + this.groupBox_UsingResult1.Text + ":  " + radioButton_UsingResult1_2.Tag + "\n";
            else if (this.radioButton_UsingResult1_3.Checked)
                strUsingResult += "1、 " + this.groupBox_UsingResult1.Text + ":  " + radioButton_UsingResult1_3.Tag + "\n";
            else if (this.radioButton_UsingResult1_4.Checked)
                strUsingResult += "1、 " + this.groupBox_UsingResult1.Text + ":  " + radioButton_UsingResult1_4.Tag + "\n";
            //提供的诊断结论全面，可以有效地防止漏诊的发生
            if (this.radioButton_UsingResult2_0.Checked)
                strUsingResult += "2、 " + this.groupBox_UsingResult2.Text + ":  " + radioButton_UsingResult2_0.Tag + "\n";
            else if (this.radioButton_UsingResult2_1.Checked)
                strUsingResult += "2、 " + this.groupBox_UsingResult2.Text + ":  " + radioButton_UsingResult2_1.Tag + "\n";
            else if (this.radioButton_UsingResult2_2.Checked)
                strUsingResult += "2、 " + this.groupBox_UsingResult2.Text + ":  " + radioButton_UsingResult2_2.Tag + "\n";
            else if (this.radioButton_UsingResult2_3.Checked)
                strUsingResult += "2、 " + this.groupBox_UsingResult2.Text + ":  " + radioButton_UsingResult2_3.Tag + "\n";
            else if (this.radioButton_UsingResult2_4.Checked)
                strUsingResult += "2、 " + this.groupBox_UsingResult2.Text + ":  " + radioButton_UsingResult2_4.Tag + "\n";
            //提供治疗建议对医生有参考价值
            if (this.radioButton_UsingResult3_0.Checked)
                strUsingResult += "3、 " + this.groupBox_UsingResult3.Text + ":  " + radioButton_UsingResult3_0.Tag + "\n";
            else if (this.radioButton_UsingResult3_1.Checked)
                strUsingResult += "3、 " + this.groupBox_UsingResult3.Text + ":  " + radioButton_UsingResult3_1.Tag + "\n";
            else if (this.radioButton_UsingResult3_2.Checked)
                strUsingResult += "3、 " + this.groupBox_UsingResult3.Text + ":  " + radioButton_UsingResult3_2.Tag + "\n";
            else if (this.radioButton_UsingResult3_3.Checked)
                strUsingResult += "3、 " + this.groupBox_UsingResult3.Text + ":  " + radioButton_UsingResult3_3.Tag + "\n";
            else if (this.radioButton_UsingResult3_4.Checked)
                strUsingResult += "3、 " + this.groupBox_UsingResult3.Text + ":  " + radioButton_UsingResult3_4.Tag + "\n";
            //提供自我监测建议，可以有效指导患者日常保健
            if (this.radioButton_UsingResult4_0.Checked)
                strUsingResult += "4、 " + this.groupBox_UsingResult4.Text + ":  " + radioButton_UsingResult4_0.Tag + "\n";
            else if (this.radioButton_UsingResult4_1.Checked)
                strUsingResult += "4、 " + this.groupBox_UsingResult4.Text + ":  " + radioButton_UsingResult4_1.Tag + "\n";
            else if (this.radioButton_UsingResult4_2.Checked)
                strUsingResult += "4、 " + this.groupBox_UsingResult4.Text + ":  " + radioButton_UsingResult4_2.Tag + "\n";
            else if (this.radioButton_UsingResult4_3.Checked)
                strUsingResult += "4、 " + this.groupBox_UsingResult4.Text + ":  " + radioButton_UsingResult4_3.Tag + "\n";
            else if (this.radioButton_UsingResult4_4.Checked)
                strUsingResult += "4、 " + this.groupBox_UsingResult4.Text + ":  " + radioButton_UsingResult4_4.Tag + "\n";
            //提供的饮食和运动建议科学并且详细
            if (this.radioButton_UsingResult5_0.Checked)
                strUsingResult += "5、 " + this.groupBox_UsingResult5.Text + ":  " + radioButton_UsingResult5_0.Tag + "\n";
            else if (this.radioButton_UsingResult5_1.Checked)
                strUsingResult += "5、 " + this.groupBox_UsingResult5.Text + ":  " + radioButton_UsingResult5_1.Tag + "\n";
            else if (this.radioButton_UsingResult5_2.Checked)
                strUsingResult += "5、 " + this.groupBox_UsingResult5.Text + ":  " + radioButton_UsingResult5_2.Tag + "\n";
            else if (this.radioButton_UsingResult5_3.Checked)
                strUsingResult += "5、 " + this.groupBox_UsingResult5.Text + ":  " + radioButton_UsingResult5_3.Tag + "\n";
            else if (this.radioButton_UsingResult5_4.Checked)
                strUsingResult += "5、 " + this.groupBox_UsingResult5.Text + ":  " + radioButton_UsingResult5_4.Tag + "\n";
            //基于临床指南，规范代谢综合征诊疗行为
            if (this.radioButton_UsingResult6_0.Checked)
                strUsingResult += "6、 " + this.groupBox_UsingResult6.Text + ":  " + radioButton_UsingResult6_0.Tag + "\n";
            else if (this.radioButton_UsingResult6_1.Checked)
                strUsingResult += "6、 " + this.groupBox_UsingResult6.Text + ":  " + radioButton_UsingResult6_1.Tag + "\n";
            else if (this.radioButton_UsingResult6_2.Checked)
                strUsingResult += "6、 " + this.groupBox_UsingResult6.Text + ":  " + radioButton_UsingResult6_2.Tag + "\n";
            else if (this.radioButton_UsingResult6_3.Checked)
                strUsingResult += "6、 " + this.groupBox_UsingResult6.Text + ":  " + radioButton_UsingResult6_3.Tag + "\n";
            else if (this.radioButton_UsingResult6_4.Checked)
                strUsingResult += "6、 " + this.groupBox_UsingResult6.Text + ":  " + radioButton_UsingResult6_4.Tag + "\n";
             #endregion


            strMessageBody += strEaseUse + strUsingResult + "\n基于保健医生临床决策支持软件其他建议：\n" + this.txtFeedBack.Text;

            //发送邮件
            try
            {
                SmtpClient mailClient = new SmtpClient("60.191.25.26");

                //登陆SMTP服务器的身份验证.
                mailClient.Credentials = new System.Net.NetworkCredential("cdssinfo", "vicocdss");

                //cdssinfo@vico-lab.com发件人地址、收件人地址
                MailMessage message = new MailMessage(new MailAddress("cdssinfo@vico-lab.com"), new MailAddress("cdssinfo@vico-lab.com"));

                message.Body = strMessageBody;//邮件内容
                message.Subject = strMessageSubject;//邮件主题

                if (strFilePath.Count>0)
                {
                    foreach (string FilePath in strFilePath)
                    {
                        //Attachment 附件
                        Attachment att = new Attachment(FilePath);
                        message.Attachments.Add(att);//添加附件
                    }
                }

                Console.WriteLine("Start Send Mail....");
                //发送....
                mailClient.Send(message);

                MessageBox.Show("发送成功！", "用户意见", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败,请重试！", "用户意见", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine("{0}", ex.Message);
                Console.ReadLine();
            }
            //清空界面
            if (this.lblFileName.Text != string.Empty)
            {
                this.lblFileName.Text = string.Empty;
                this.btnDelete.Visible = false;
            }
            this.radioButton_easyUsing1_2.Checked = true;
            this.radioButton_easyUsing2_2.Checked = true;
            this.radioButton_easyUsing3_2.Checked = true;
            this.radioButton_easyUsing4_2.Checked = true;
            this.radioButton_easyUsing5_2.Checked = true;
            this.radioButton_easyUsing6_2.Checked = true;
            this.radioButton_UsingResult1_2.Checked = true;
            this.radioButton_UsingResult2_2.Checked = true;
            this.radioButton_UsingResult3_2.Checked = true;
            this.radioButton_UsingResult4_2.Checked = true;
            this.radioButton_UsingResult5_2.Checked = true;
            this.radioButton_UsingResult6_2.Checked = true;
            this.radioButton_UsingResult1_2.Checked = true;
        }
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath;
            //关闭对话框前还原目录
            openFileDialog1.RestoreDirectory= true;
            openFileDialog1.Filter = "All Files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openFileDialog1.FileName.ToString();
                strFilePath.Add(FilePath);
                //openFileDialog1
                this.lblFileName.Text += Path.GetFileNameWithoutExtension(FilePath) + "   ";
                this.btnDelete.Visible = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lblFileName.Text == string.Empty)
                return;

            this.strFilePath.Clear();
            this.lblFileName.Text = "";

            this.btnDelete.Visible = false;
        }
    }
}