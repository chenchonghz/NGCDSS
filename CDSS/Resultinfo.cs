using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSSystemData;
using CDSSFunction;
using CDSSDBAccess;
using CDSSCLIPSEngine;
using System.IO;
using System.Xml;
using System.Net;
using Utilities;


namespace CDSS
{
    public partial class Resultinfo : Form
    {

        private bool bLoaded = false;    //标记数据是否已经加载过
        private bool bReasoned = false; //标记是否已经推理过
        private List<CDSSCtrlLib.DiagnosisResult> DiagnosisResultCtrlList = new List<CDSSCtrlLib.DiagnosisResult>();//诊断结论显示控件列表

        #region 系统事件
        public Resultinfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载时创建诊断结论控件列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resultinfo_Load(object sender, EventArgs e)
        {
            CreateDiagnosisResultCtrlList();
        }

        /// <summary>
        /// 处理窗体显示状态更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resultinfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (PatInfo.bNewPatient)
                {
                    if (!this.bReasoned)    //对于新入病人,如果未推理过就执行推理
                        DoReasoning();
                }
                else
                {
                    if (!bLoaded)   //对于历史病人,如果历史数据未加载过就执行加载
                        LoadDataFromVarToUI();
                }
            }
        }

        /// <summary>
        /// 点击重新推理按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblReReason_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.bReasoned = false;
            DoReasoning();

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 发起事件通知主页面数据已更改
        /// </summary>
        public event CustomEventHandle DataChangedEvent;
        protected void RaiseDataChangedEvent(object sender, EventArgs e)
        {
            CustomEventHandle temp = DataChangedEvent;
            if (temp != null)
                temp();
        }


        private void lblReReason_MouseDown(object sender, MouseEventArgs e)
        {
            this.lblReReason.Image = CDSS.Properties.Resources.Button_press;
        }

        private void lblReReason_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.ClientRectangle.Contains(e.Location))
                this.lblReReason.Image = CDSS.Properties.Resources.Button_over;
            else
                this.lblReReason.Image = CDSS.Properties.Resources.Button_normal;
        }

        private void lblReReason_MouseEnter(object sender, EventArgs e)
        {
            this.lblReReason.Image = CDSS.Properties.Resources.Button_over;
        }

        private void lblReReason_MouseLeave(object sender, EventArgs e)
        {
            this.lblReReason.Image = CDSS.Properties.Resources.Button_normal;
        }

        #endregion

        #region 用户事件
        /// <summary>
        /// 处理DiagnosisResult的ShowDiagnosisStepsEvent事件，用于显示诊断过程
        /// </summary>
        /// <param name="DiagnosisSteps"></param>
        private void DiagnosisResult_ShowDiagnosisSteps(string DiagnosisSteps)
        {
            DiagnosisStepsForm frmDiagnosisSteps = new DiagnosisStepsForm();
            frmDiagnosisSteps.ShowDiagnosisSteps(DiagnosisSteps);
        }
        #endregion

        #region 功能函数
        /// <summary>
        /// 创建诊断结论控件列表
        /// </summary>
        private void CreateDiagnosisResultCtrlList()
        {
            CDSSCtrlLib.DiagnosisResult DiagnosisResultCtrl;
            foreach (Control control in this.tableLayoutPanel1.Controls)
            {
                DiagnosisResultCtrl = control as CDSSCtrlLib.DiagnosisResult;
                if (DiagnosisResultCtrl != null)
                {
                    DiagnosisResultCtrl.Visible = false;
                    this.DiagnosisResultCtrlList.Add(DiagnosisResultCtrl);
                }
            }
        }

        /// <summary>
        /// 清空诊断结论控件中的数据
        /// </summary>
        private void ResetDiagnosisResultCtrl()
        {
            foreach (CDSSCtrlLib.DiagnosisResult DiagnosisResultCtrl in this.DiagnosisResultCtrlList)
            {
                DiagnosisResultCtrl.Visible = false;
                DiagnosisResultCtrl.ClearData();
            }
        }

        /// <summary>
        /// 清除界面数据
        /// </summary>
        public void ClearData()
        {
            this.lblResult.Text = String.Empty;
            this.lblRiskDegree.Text = String.Empty;
            this.lblRiskDegreeCode.Text = string.Empty;

            ResetDiagnosisResultCtrl();

            //设置数据未加载标志
            this.bLoaded = false;
            //设置未推理标志
            this.bReasoned = false;
        }

        /// <summary>
        /// 保存诊断结论
        /// </summary>
        /// <returns></returns>
        public void LoadDataFromUIToVar()
        {

        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadDataFromVarToUI()
        {
            this.lblResult.Text = GlobalData.DiagnosedResult.HasMS;
            this.lblRiskDegreeCode.Text = GlobalData.DiagnosedResult.RiskDegreeCode;
            this.lblRiskDegree.Text = GlobalData.DiagnosedResult.RiskDegree;

            int i = 0;
            CDSSCtrlLib.DiagnosisResult DiagnosisResultCtrl;
            foreach (CDSSOneDiseaseDiagnosedResult ODDR in GlobalData.DiagnosedResult.DiseaseDiagnosedResultList)
            {
                if (i >= this.DiagnosisResultCtrlList.Count)
                    break;
                DiagnosisResultCtrl = this.DiagnosisResultCtrlList[i];
                DiagnosisResultCtrl.DiseaseName = ODDR.Name;
                DiagnosisResultCtrl.Result = ODDR.Result;
                DiagnosisResultCtrl.TreatmentTarget = ODDR.TreatmentTarget;
                DiagnosisResultCtrl.TreatmentSuggestion = ODDR.TreatmentSuggestion;
                DiagnosisResultCtrl.SelfCheck = ODDR.SelfCheck;
                DiagnosisResultCtrl.DataNeeded = ODDR.DataNeeded;
                DiagnosisResultCtrl.DiagnosisSteps = ODDR.DiagnosisSteps;

                //Add by ZX 2010-3-25 将推理机得出的结论窗体可编辑txtbox，充当建议
                try
                {
                    DiagnosisResultCtrl.ReasonSelfCheck = GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].SelfCheck;
                    DiagnosisResultCtrl.ReasonTreatmentTarget = GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].TreatmentTarget;
                    DiagnosisResultCtrl.ReasonTreatmentSuggestion = GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].TreatmentSuggestion;
                }
                catch
                {
                    DiagnosisResultCtrl.ReasonSelfCheck = "";
                    DiagnosisResultCtrl.ReasonTreatmentTarget = "";
                    DiagnosisResultCtrl.ReasonTreatmentSuggestion ="";
                }
                DiagnosisResultCtrl.Tag = ODDR;
                DiagnosisResultCtrl.ResultChange += delegate(object sender, DiagnosisResult.ResultChangeArgs e)
                {
                    DiagnosisResult tmp = (DiagnosisResult)sender;
                    tmp.TreatmentTarget = e.Content[0];
                    tmp.TreatmentSuggestion = e.Content[1];
                    tmp.SelfCheck = e.Content[2];
                    tmp.ShowDiagnosisResult();
                    CDSSOneDiseaseDiagnosedResult global = (CDSSOneDiseaseDiagnosedResult)tmp.Tag;
                    global.TreatmentTarget = e.Content[0];
                    global.TreatmentSuggestion = e.Content[1];
                    global.SelfCheck = e.Content[2];
                    tmp.Visible = true;
                    RaiseDataChangedEvent(this, null);
                };
                DiagnosisResultCtrl.ShowDiagnosisResult();
                DiagnosisResultCtrl.Visible = true;
                i++;
            }
            //设置数据已加载标志
            this.bLoaded = true;
        }

        /// <summary>
        /// 推理并显示结果
        /// </summary>
        public void DoReasoning()
        {
            this.lblResult.Text = String.Empty;
            this.lblRiskDegree.Text = String.Empty;
            this.lblRiskDegreeCode.Text = string.Empty;
            ResetDiagnosisResultCtrl();
            GlobalData.DiagnosedResult.Clear();

            List<FunctionTypeDef.EventModels> lstEventModels = new List<FunctionTypeDef.EventModels>();
            CDSSFunction.Interface.ObtainInfernceEvents(ref lstEventModels);
            CDSSFunction.Interface.SetInferenceNeededEvents(lstEventModels);
            CDSSFunction.Interface.InfernceExplanationExecute();

            /*****************************************************************
             * add by zx 20100330
             * 将推理机得出结论一同付给ReasoningDiseaseDiagnosedResultList与DiseaseDiagnosedResultList     
             *****************************************************************/
            foreach (CDSSOneDiseaseDiagnosedResult result in GlobalData.DiagnosedResult.DiseaseDiagnosedResultList)
            {
                GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList.Add(result.Clone());
            }            

            LoadDataFromVarToUI();

            /*****************************************************************
             * add by lch 090609
             * 调用上传推理日志方法
             *****************************************************************/
            UpLoadIELog();


            //设置为已推理
            this.bReasoned = true;

            //2009-9-4:ccj
            //提示保存
            RaiseDataChangedEvent(this, null);
            //修改结束
        }
        /// <summary>
        /// add by lch 090609
        /// 第一步：创建XML文件，保存用户ID，病人ID，病人姓名，推理时间，索引序列号；
        /// 第二步：打包推理日志文件
        ///     取消==》第三步：上传
        /// 第三步：保存rar文件至CDSS_OperationLog表中
        /// 第四步：删除本次推理产生的IELog文件夹和IELog.rar
        /// </summary>
        public void UpLoadIELog()
        {
            /************************************************
             * 创建XML文件
             *************************************************/
            string strIEProcessXMLName =
               System.DateTime.Now.ToString("yyyyMMddHHmmss") + "_"
               + GlobalData.UserInfo.UserID + "_" + GlobalData.PatBasicInfo.PatID + "_" + /*GlobalData.PatBasicInfo.PatName + "_" +*/ GlobalData.RecordInfo.RecordSeq;
            string strLogPath = ClipsConfig.ReadConfig("LogPath");
            //string strLogFlag = ClipsConfig.ReadConfig("LogFlag");
            string strIELogDirectory = strLogPath + "IELog";

            //如果日志文件目录不存在，则创建该目录
            if (Directory.Exists(strIELogDirectory) == false)
            {
                Directory.CreateDirectory(strIELogDirectory);
            }
            string strIELogXML = strIELogDirectory + "\\" + strIEProcessXMLName + ".xml";

            if (!File.Exists(strIELogXML))
            {
                FileStream fs1 = new FileStream(strIELogXML, FileMode.Create, FileAccess.Write);//创建文件   
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r");//写入值 
                sw.WriteLine("<IELogInfo>\r");
                sw.WriteLine("<!--IELog的相关信息，用户ID，病人ID，推理时间,索引序列号-->");
                //用户ID
                sw.WriteLine("<UserID>");
                sw.WriteLine(GlobalData.UserInfo.UserID.ToString());
                sw.WriteLine("</UserID>\r");
                //病人ID
                sw.WriteLine("<PatID>");
                sw.WriteLine(GlobalData.PatBasicInfo.PatID.ToString());
                sw.WriteLine("</PatID>\r");
                //病人姓名
                //sw.WriteLine("<PatName>");
                //sw.WriteLine(GlobalData.PatBasicInfo.PatName.ToString());
                //sw.WriteLine("</PatName>\r");
                //推理时间
                sw.WriteLine("<DoReasoningTime>");
                sw.WriteLine(DBAccess.GetServerCurrentTime());
                sw.WriteLine("</DoReasoningTime>\r");
                //索引序列号
                sw.WriteLine("<RecordSEQ>");
                sw.WriteLine(GlobalData.RecordInfo.RecordSeq.ToString());
                sw.WriteLine("</RecordSEQ>\r");

                sw.WriteLine("</IELogInfo>");
                sw.Close();
                fs1.Close();
            }

            /************************************************
            * 压缩IELog文件
            *************************************************/
            //string strRARPath = strLogPath + "\\IELog" + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_"
            //    + GlobalData.UserInfo.UserID + "_"
            //    + GlobalData.PatBasicInfo.PatID + "_"
            //    //+ GlobalData.PatBasicInfo.PatName + "_"
            //    + GlobalData.RecordInfo.RecordSeq
            //    + ".rar";

            //采用ICSharpCode.SharpZipLib.dll程序集进行压缩时方法中让其自动生成的压缩文件名为.zip后缀故删除.rar后缀，并相应的修改 CDSSOperationLog.OperationDescription 值。
            string strRARPath = strLogPath + "\\IELog" + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_"
                + GlobalData.UserInfo.UserID + "_"
                + GlobalData.PatBasicInfo.PatID + "_"
                //+ GlobalData.PatBasicInfo.PatName + "_"
                + GlobalData.RecordInfo.RecordSeq;

            #region 原先采用WinRAR进行压缩的代码
            //string strArguments = "a -s -m5 -ep1 \"" + strRARPath + "\"    \"" + strIELogDirectory + "\"";
            ////指定启动该程序时使用的应用程序文件名称为压缩程序WinRAR.exe，传递给应用程序的命令行参数为“a -ep  压缩包的路径（包含文件名） 需要压缩的文件夹的名称”
            //System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(@"WinRAR.exe", strArguments);

            //info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;//隐藏压缩窗口
            //System.Diagnostics.Process Proc = System.Diagnostics.Process.Start(info);//生成压缩文件
            //Proc.WaitForExit(5000);
            //if (Proc.HasExited == false)
            //{
            //    Proc.Kill();
            //}
            #endregion

            //采用ICSharpCode.SharpZipLib.dll程序集进行压缩
            ZipClass zipClass = new ZipClass();
            zipClass.ZipFileOrDir(strIELogDirectory, strRARPath, 9);

          
            #region 上传文件至FTP
            //************************************************
            // * 上传文件至FTP
            // *************************************************/

            //FileInfo fileInf = new FileInfo(strRARPath);
            //string uri = "ftp://" + "192.168.3.31/lch" + "/" + fileInf.Name;
            //FtpWebRequest reqFTP;

            //// 根据uri创建FtpWebRequest对象 
            //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            //// ftp用户名和密码
            //reqFTP.Credentials = new NetworkCredential("", "");

            //// 默认为true，连接不会被关闭
            //// 在一个命令之后被执行
            //reqFTP.KeepAlive = false;

            //// 指定执行什么命令
            //reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            //// 指定数据传输类型
            //reqFTP.UseBinary = true;

            //// 上传文件时通知服务器文件的大小
            //reqFTP.ContentLength = fileInf.Length;

            //// 缓冲大小设置为2kb
            //int buffLength = 2048;

            //byte[] buff = new byte[buffLength];
            //int contentLen;

            //// 打开一个文件流 (System.IO.FileStream) 去读上传的文件
            //FileStream fs = fileInf.OpenRead();
            //try
            //{
            //    // 把上传的文件写入流
            //    Stream strm = reqFTP.GetRequestStream();

            //    // 每次读文件流的2kb
            //    contentLen = fs.Read(buff, 0, buffLength);

            //    // 流内容没有结束
            //    while (contentLen != 0)
            //    {
            //        // 把内容从file stream 写入 upload stream
            //        strm.Write(buff, 0, contentLen);

            //        contentLen = fs.Read(buff, 0, buffLength);
            //    }

            //    // 关闭两个流
            //    strm.Close();
            //    fs.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    //MessageBox.Show(ex.Message, "Upload Error");
            //}
            #endregion


            /********************************************************************************
            *作用：用户点击【结论刷新】按钮，表示该用户的操作是“推理日志”，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "推理日志";
            //CDSSOperationLog.OperationDescription = strRARPath;

            //采用ICSharpCode.SharpZipLib.dll程序集进行压缩时进行相应的修改
            CDSSOperationLog.OperationDescription = strRARPath+".zip";
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);



            /************************************************
             * 删除本地文件夹和压缩文件
             * 
             * 修改 by lch 090615 压缩文件这里不进行删除，
             * 在程序退出时还需要保存操作日记以及推理日志，
             * 在保存完推理日志后再删除。
             *************************************************/
            if (Directory.Exists(strIELogDirectory) == true)
            {
                Directory.Delete(strIELogDirectory, true);
            }
            //if (File.Exists(strRARPath))
            //{
            //    File.Delete(strRARPath);
            //}
        }

        #endregion

    }

}