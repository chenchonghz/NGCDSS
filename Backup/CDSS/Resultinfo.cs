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

        private bool bLoaded = false;    //��������Ƿ��Ѿ����ع�
        private bool bReasoned = false; //����Ƿ��Ѿ������
        private List<CDSSCtrlLib.DiagnosisResult> DiagnosisResultCtrlList = new List<CDSSCtrlLib.DiagnosisResult>();//��Ͻ�����ʾ�ؼ��б�

        #region ϵͳ�¼�
        public Resultinfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �������ʱ������Ͻ��ۿؼ��б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resultinfo_Load(object sender, EventArgs e)
        {
            CreateDiagnosisResultCtrlList();
        }

        /// <summary>
        /// ��������ʾ״̬�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resultinfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (PatInfo.bNewPatient)
                {
                    if (!this.bReasoned)    //�������벡��,���δ�������ִ������
                        DoReasoning();
                }
                else
                {
                    if (!bLoaded)   //������ʷ����,�����ʷ����δ���ع���ִ�м���
                        LoadDataFromVarToUI();
                }
            }
        }

        /// <summary>
        /// �����������ť
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
        /// �����¼�֪ͨ��ҳ�������Ѹ���
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

        #region �û��¼�
        /// <summary>
        /// ����DiagnosisResult��ShowDiagnosisStepsEvent�¼���������ʾ��Ϲ���
        /// </summary>
        /// <param name="DiagnosisSteps"></param>
        private void DiagnosisResult_ShowDiagnosisSteps(string DiagnosisSteps)
        {
            DiagnosisStepsForm frmDiagnosisSteps = new DiagnosisStepsForm();
            frmDiagnosisSteps.ShowDiagnosisSteps(DiagnosisSteps);
        }
        #endregion

        #region ���ܺ���
        /// <summary>
        /// ������Ͻ��ۿؼ��б�
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
        /// �����Ͻ��ۿؼ��е�����
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
        /// �����������
        /// </summary>
        public void ClearData()
        {
            this.lblResult.Text = String.Empty;
            this.lblRiskDegree.Text = String.Empty;
            this.lblRiskDegreeCode.Text = string.Empty;

            ResetDiagnosisResultCtrl();

            //��������δ���ر�־
            this.bLoaded = false;
            //����δ�����־
            this.bReasoned = false;
        }

        /// <summary>
        /// ������Ͻ���
        /// </summary>
        /// <returns></returns>
        public void LoadDataFromUIToVar()
        {

        }

        /// <summary>
        /// ��������
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

                //Add by ZX 2010-3-25 ��������ó��Ľ��۴���ɱ༭txtbox���䵱����
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
            //���������Ѽ��ر�־
            this.bLoaded = true;
        }

        /// <summary>
        /// ������ʾ���
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
             * ��������ó�����һͬ����ReasoningDiseaseDiagnosedResultList��DiseaseDiagnosedResultList     
             *****************************************************************/
            foreach (CDSSOneDiseaseDiagnosedResult result in GlobalData.DiagnosedResult.DiseaseDiagnosedResultList)
            {
                GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList.Add(result.Clone());
            }            

            LoadDataFromVarToUI();

            /*****************************************************************
             * add by lch 090609
             * �����ϴ�������־����
             *****************************************************************/
            UpLoadIELog();


            //����Ϊ������
            this.bReasoned = true;

            //2009-9-4:ccj
            //��ʾ����
            RaiseDataChangedEvent(this, null);
            //�޸Ľ���
        }
        /// <summary>
        /// add by lch 090609
        /// ��һ��������XML�ļ��������û�ID������ID����������������ʱ�䣬�������кţ�
        /// �ڶ��������������־�ļ�
        ///     ȡ��==�����������ϴ�
        /// ������������rar�ļ���CDSS_OperationLog����
        /// ���Ĳ���ɾ���������������IELog�ļ��к�IELog.rar
        /// </summary>
        public void UpLoadIELog()
        {
            /************************************************
             * ����XML�ļ�
             *************************************************/
            string strIEProcessXMLName =
               System.DateTime.Now.ToString("yyyyMMddHHmmss") + "_"
               + GlobalData.UserInfo.UserID + "_" + GlobalData.PatBasicInfo.PatID + "_" + /*GlobalData.PatBasicInfo.PatName + "_" +*/ GlobalData.RecordInfo.RecordSeq;
            string strLogPath = ClipsConfig.ReadConfig("LogPath");
            //string strLogFlag = ClipsConfig.ReadConfig("LogFlag");
            string strIELogDirectory = strLogPath + "IELog";

            //�����־�ļ�Ŀ¼�����ڣ��򴴽���Ŀ¼
            if (Directory.Exists(strIELogDirectory) == false)
            {
                Directory.CreateDirectory(strIELogDirectory);
            }
            string strIELogXML = strIELogDirectory + "\\" + strIEProcessXMLName + ".xml";

            if (!File.Exists(strIELogXML))
            {
                FileStream fs1 = new FileStream(strIELogXML, FileMode.Create, FileAccess.Write);//�����ļ�   
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r");//д��ֵ 
                sw.WriteLine("<IELogInfo>\r");
                sw.WriteLine("<!--IELog�������Ϣ���û�ID������ID������ʱ��,�������к�-->");
                //�û�ID
                sw.WriteLine("<UserID>");
                sw.WriteLine(GlobalData.UserInfo.UserID.ToString());
                sw.WriteLine("</UserID>\r");
                //����ID
                sw.WriteLine("<PatID>");
                sw.WriteLine(GlobalData.PatBasicInfo.PatID.ToString());
                sw.WriteLine("</PatID>\r");
                //��������
                //sw.WriteLine("<PatName>");
                //sw.WriteLine(GlobalData.PatBasicInfo.PatName.ToString());
                //sw.WriteLine("</PatName>\r");
                //����ʱ��
                sw.WriteLine("<DoReasoningTime>");
                sw.WriteLine(DBAccess.GetServerCurrentTime());
                sw.WriteLine("</DoReasoningTime>\r");
                //�������к�
                sw.WriteLine("<RecordSEQ>");
                sw.WriteLine(GlobalData.RecordInfo.RecordSeq.ToString());
                sw.WriteLine("</RecordSEQ>\r");

                sw.WriteLine("</IELogInfo>");
                sw.Close();
                fs1.Close();
            }

            /************************************************
            * ѹ��IELog�ļ�
            *************************************************/
            //string strRARPath = strLogPath + "\\IELog" + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_"
            //    + GlobalData.UserInfo.UserID + "_"
            //    + GlobalData.PatBasicInfo.PatID + "_"
            //    //+ GlobalData.PatBasicInfo.PatName + "_"
            //    + GlobalData.RecordInfo.RecordSeq
            //    + ".rar";

            //����ICSharpCode.SharpZipLib.dll���򼯽���ѹ��ʱ�����������Զ����ɵ�ѹ���ļ���Ϊ.zip��׺��ɾ��.rar��׺������Ӧ���޸� CDSSOperationLog.OperationDescription ֵ��
            string strRARPath = strLogPath + "\\IELog" + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_"
                + GlobalData.UserInfo.UserID + "_"
                + GlobalData.PatBasicInfo.PatID + "_"
                //+ GlobalData.PatBasicInfo.PatName + "_"
                + GlobalData.RecordInfo.RecordSeq;

            #region ԭ�Ȳ���WinRAR����ѹ���Ĵ���
            //string strArguments = "a -s -m5 -ep1 \"" + strRARPath + "\"    \"" + strIELogDirectory + "\"";
            ////ָ�������ó���ʱʹ�õ�Ӧ�ó����ļ�����Ϊѹ������WinRAR.exe�����ݸ�Ӧ�ó���������в���Ϊ��a -ep  ѹ������·���������ļ����� ��Ҫѹ�����ļ��е����ơ�
            //System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(@"WinRAR.exe", strArguments);

            //info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;//����ѹ������
            //System.Diagnostics.Process Proc = System.Diagnostics.Process.Start(info);//����ѹ���ļ�
            //Proc.WaitForExit(5000);
            //if (Proc.HasExited == false)
            //{
            //    Proc.Kill();
            //}
            #endregion

            //����ICSharpCode.SharpZipLib.dll���򼯽���ѹ��
            ZipClass zipClass = new ZipClass();
            zipClass.ZipFileOrDir(strIELogDirectory, strRARPath, 9);

          
            #region �ϴ��ļ���FTP
            //************************************************
            // * �ϴ��ļ���FTP
            // *************************************************/

            //FileInfo fileInf = new FileInfo(strRARPath);
            //string uri = "ftp://" + "192.168.3.31/lch" + "/" + fileInf.Name;
            //FtpWebRequest reqFTP;

            //// ����uri����FtpWebRequest���� 
            //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            //// ftp�û���������
            //reqFTP.Credentials = new NetworkCredential("", "");

            //// Ĭ��Ϊtrue�����Ӳ��ᱻ�ر�
            //// ��һ������֮��ִ��
            //reqFTP.KeepAlive = false;

            //// ָ��ִ��ʲô����
            //reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            //// ָ�����ݴ�������
            //reqFTP.UseBinary = true;

            //// �ϴ��ļ�ʱ֪ͨ�������ļ��Ĵ�С
            //reqFTP.ContentLength = fileInf.Length;

            //// �����С����Ϊ2kb
            //int buffLength = 2048;

            //byte[] buff = new byte[buffLength];
            //int contentLen;

            //// ��һ���ļ��� (System.IO.FileStream) ȥ���ϴ����ļ�
            //FileStream fs = fileInf.OpenRead();
            //try
            //{
            //    // ���ϴ����ļ�д����
            //    Stream strm = reqFTP.GetRequestStream();

            //    // ÿ�ζ��ļ�����2kb
            //    contentLen = fs.Read(buff, 0, buffLength);

            //    // ������û�н���
            //    while (contentLen != 0)
            //    {
            //        // �����ݴ�file stream д�� upload stream
            //        strm.Write(buff, 0, contentLen);

            //        contentLen = fs.Read(buff, 0, buffLength);
            //    }

            //    // �ر�������
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
            *���ã��û����������ˢ�¡���ť����ʾ���û��Ĳ����ǡ�������־������¼�ò�����־
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "������־";
            //CDSSOperationLog.OperationDescription = strRARPath;

            //����ICSharpCode.SharpZipLib.dll���򼯽���ѹ��ʱ������Ӧ���޸�
            CDSSOperationLog.OperationDescription = strRARPath+".zip";
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);



            /************************************************
             * ɾ�������ļ��к�ѹ���ļ�
             * 
             * �޸� by lch 090615 ѹ���ļ����ﲻ����ɾ����
             * �ڳ����˳�ʱ����Ҫ��������ռ��Լ�������־��
             * �ڱ�����������־����ɾ����
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