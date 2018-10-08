using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using CDSSSystemData;
using System.Configuration;
using Utilities;

namespace CDSS
{
    public class UserLogRecycle
    {

        public static void UserLogRecycleControl()
        {
            Thread thread = new Thread(UserLogRecycleControlRun);
            thread.Start();
        }

        public static void UserLogRecycleControlRun()
        {
            //转换成字节
            long LogSize = int.Parse(ConfigurationManager.AppSettings["UserLogMaxSize"]) << 20;
            if (LogSizeMonitor() >= LogSize)
            {
                Console.WriteLine("当前日志已经过大，进行压缩打包!");
                LogCompress(UserLogOperate.LogPath);
                //if (LogSizeMonitor() == 0)
                //    SendEmail(UserLogOperate.LogPath + "\\" + GlobalData.UserInfo.UserID + " " + DateTime.Now.ToShortDateString() + ".rar");
            }
        }

        /// <summary>
        /// 监测当前登录用户日志文件大小
        /// </summary>
        /// <returns></returns>
        public static long LogSizeMonitor()
        {
            DirectoryInfo dir = new DirectoryInfo(UserLogOperate.LogPath);
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            long size = 0;
            foreach (FileSystemInfo file in files)
            {
                FileInfo fileinfo = new FileInfo(file.FullName);
                if (fileinfo.Extension.ToLower() == ".xml")
                    size += fileinfo.Length;
            }
            return size;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="rarPath">压缩包存放路径</param>
        /// <param name="rarName">压缩包名称</param>
        public static void LogCompress(string zipPath)
        {
            ////原来用WinRAR进行压缩时的代码
            //string args = "m -ep " + rarPath + @"\" + rarName + " " + rarPath + @"\*.xml -r";
            //ProcessStartInfo info = new ProcessStartInfo(@"WinRAR.exe", args);

            //info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;//隐藏压缩窗口
            //Process Proc = Process.Start(info);//生成压缩文件
            //Proc.WaitForExit();
            //Proc.Close();

            //采用ICSharpCode.SharpZipLib.dll程序集进行压缩           
            ZipClass zipClass = new ZipClass();
            zipClass.zip(zipPath, GlobalData.UserInfo.UserID);     
        }

        /// <summary>
        /// 将日志压缩后发送给服务器
        /// </summary>
        /// <param name="filePath">压缩包文件路径</param>
        public static void SendEmail(string filePath)
        {
            try
            {
                SmtpClient mailClient = new SmtpClient("60.191.25.26");

                //登陆SMTP服务器的身份验证.
                mailClient.Credentials = new System.Net.NetworkCredential("zx", "123456");

                //cdssinfo@vico-lab.com发件人地址、收件人地址
                MailMessage message = new MailMessage(new MailAddress("zx@vico-lab.com"), new MailAddress("zx@vico-lab.com"));

                string title = GlobalData.UserInfo.UserID + "用户使用日志";

                message.Body = title;//邮件内容
                message.Subject = title;//邮件主题

                //Attachment 附件
                Attachment att = new Attachment(filePath.ToString());
                message.Attachments.Add(att);//添加附件

                Console.WriteLine("Start Send Mail....");
                //发送....
                mailClient.Send(message);

                Console.WriteLine("End Send Mail....");
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                Console.ReadLine();
            }
        }
    }
}
