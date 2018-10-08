using System;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net.Mail;
using System.Windows.Forms;
using System.Configuration;
using CDSSCtrlLib;
using CDSSSystemData;
using System.Runtime.InteropServices;//API �����ռ�
using CDSSDBAccess;
using Utilities;
using Utilities.RecordTreeNodeStateFuction;

namespace CDSS
{
    public partial class MainForm : Form
    {
        private WelcomeForm frmWelcom = new WelcomeForm();
        private QueryForm frmQuery = new QueryForm();
        private Resultinfo frmResult = new Resultinfo();
        private Suggestioninfo frmSuggest = new Suggestioninfo();
        private InfoEnter frmInfo = new InfoEnter();
        private UserFeedback frmFeedback = new UserFeedback();//add by lch 090605 ʵ�����û����봰��
        private PrintForm frmPrint = new PrintForm();
        private CheckBox btnCurrent;
        private Form frmCurrent;
        private ProgressForm frmProgress = new ProgressForm();
        private ImportForm frmImport = new ImportForm();
        private delegate DialogResult NewTaskDelegate();
        private NewTaskDelegate task;
        private RegFrom frmReg = new RegFrom();
        private Statistic frmStatistic = new Statistic();
        private ConfigFileEditorForm frmConfigFileEditor = new ConfigFileEditorForm();
        private DataVerification frmDataVer = new DataVerification();//����У�����
        private delegate void FlushClient(string RNum);//����        
        //added by yanhui 20120319
        private ImportDBMerge frmImportDBMerge = new ImportDBMerge(); 
        /******************************************************        
        * Revise History��
        * 2008-12-22 ���� ���FormClosing�¼������ڹر�ǰ����Ӧ
        ******************************************************/
        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            //add by lch  090617 ������������ʾ�汾��
            this.Text = "��л�ۺ������ƾ���֧�����   �汾��" + GlobalData.UserInfo.CurrentAppVer;
           
            /********************************************************************************
             *���ã��û��������¼����ť����������أ���ʾ�ɹ���¼����¼�ò�����־
             *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = GlobalData.UserInfo.LoginConnDBTime;//�޸�by lch 090616 �����������ݿ�ʱ�����¼ʱ��һ��
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "��¼";
            CDSSOperationLog.OperationDescription = "��ǰ����İ汾��Ϊ:" + GlobalData.UserInfo.CurrentAppVer;//����汾��Ϣ
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            //task = CheckIsRegOrNot;
            //AsyncCallback callBack = new AsyncCallback(AsyncCallbackImpl);
            //IAsyncResult asyncResult = task.BeginInvoke(callBack, null);
        }

        public void AsyncCallbackImpl(IAsyncResult ar)
        {
            if (DialogResult.OK != task.EndInvoke(ar))
            {
                this.FormClosing -= new FormClosingEventHandler(MainForm_FormClosing);
                Application.Exit();
            }
        }

        /// <summary>
        /// //add by zx 100608 �������Ƿ��Ѿ�ע��
        /// </summary>
        public DialogResult CheckIsRegOrNot()
        {
            try
            {
                if (!RegMachineClass.CheckRegist())
                {
                    ShowRegFrm(RegMachineClass.Reg);
                    return frmReg.DialogResult;
                }
                return DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                return DialogResult.OK;
            }
           
        }

        private void ShowRegFrm(string RNum)
        {
            if (this.InvokeRequired || this.InvokeRequired)
            {
                try
                {
                    FlushClient fc = new FlushClient(ShowRegFrm);
                    this.Invoke(fc, RNum);
                }
                catch { ;}
            }
            else
            {
                frmReg.RNum = RNum;
                frmReg.ShowDialog(this);
            }
        }

        #region ��Ӧϵͳ�¼�
        private void MainForm_Load(object sender, EventArgs e)
        {
            UserLogRecycle.UserLogRecycleControl();
            //�޸�by Jyl��081223��bugA3ɾ����¼������ʾ�������Ƶ�Program.cs�ڣ�����������ʼִ��
            ////��¼
            //Login frmLogin = new Login();
            //if (frmLogin.ShowDialog() != DialogResult.OK)
            //{
            //    this.Close();              
            //}
            //��ȡ��������ƿ���״̬
            if (ConfigurationManager.AppSettings["MustFill"] == "1")
                PatInfo.bMustFill = true;
            else
                PatInfo.bMustFill = false;
            //ҳ����ʾ��ʼ��
            frmWelcom.TopLevel = false;
            frmWelcom.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmWelcom);

            //��������ʼ��
            frmImport.TopLevel = false;
            frmImport.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmImport);

            //added by yanhui 20120319
            frmImportDBMerge.TopLevel = false;
            frmImportDBMerge.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmImportDBMerge);

            //����У������ʼ��
            frmDataVer.TopLevel = false;
            frmDataVer.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmDataVer);

            frmQuery.TopLevel = false;
            frmQuery.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmQuery);
            this.frmQuery.ShowInfoEnterPageEvent += frmQuery_OnShowInfoEnterPage;//�����ѯҳ��������ת����Ϣ¼��ҳ���¼�

            frmResult.TopLevel = false;
            frmResult.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmResult);

            frmSuggest.TopLevel = false;
            frmSuggest.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmSuggest);

            //��Ӵ�ӡ���壬Revised by XY��20081221
            frmPrint.TopLevel = false;
            frmPrint.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmPrint);

            //add by lch 090605 �����û����봰��
            frmFeedback.TopLevel = false;
            frmFeedback.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmFeedback);

            frmStatistic.TopLevel = false;
            frmStatistic.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmStatistic);

            frmConfigFileEditor.TopLevel = false;
            frmConfigFileEditor.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmConfigFileEditor);

            frmInfo.TopLevel = false;
            frmInfo.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmInfo);
            this.frmInfo.ShowResultPageEvent += frmInfo_OnShowResultPage; //������Ϣ¼��ҳ��������ת����Ͻ���ҳ���¼�
            this.frmInfo.PatBasicInfoChanged += frmInfo_OnPatBasicInfoChanged;//������Ϣ¼��ҳ��ת����BasicInfoҳ���˻�����Ϣ�Ѹ����¼�
            //2009-8-28��ccj
            //������Ϣ¼��ҳ�������ı�����ʾ�仯�¼�
            this.frmInfo.ShowSaveEvent += frmInfo_OnShowSave;
            this.frmInfo.ShowChangeEvent += OnShowChange;

            //2009-9-3:ccj
            //������ҳ�������ı�����ʾ�仯�¼�
            this.frmSuggest.DataChangedEvent += OnShowChange;
            //2009-9-4:ccj
            //�������ҳ�������ı�����ʾ�仯�¼�
            this.frmResult.DataChangedEvent += OnShowChange;
            //�޸Ľ���
            ShowWelcomePage();
        }

        /******************************************************         
                *Author������
                *Create Date��2008-12-22 
                *Function���رմ���ǰ�������ѣ����û�ȷ����ִ�С�
                * 
                *Revise History��
                ******************************************************/

        private void MainForm_FormClosing(Object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("�Ƿ��˳�����ҽ���ٴ�����֧�������", "��ʾ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //������رգ��ͷ�Excelռ�õ���Դ
            frmImport.ExcelControl.CloseExcelApplication();
            frmStatistic.RecordTreeNodeState();
            frmDataVer.RecordTreeNodeState();
            frmImport.RecordTreeNodeState();
            if (dr != DialogResult.Yes)
            {
                e.Cancel = true;                
            }

            /********************************************************************************
            *���ã��û�ȷ���˳����򣬼�¼�ò�����־
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "�˳�";
            // CDSSOperationLog.OperationDescription = GlobalData.UserInfo.CurrentAppVer;//����汾��Ϣ
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            //��������־���������ݿ�
            //DBAccess.SaveAllOperationLog();
            UserLogOperate.SaveLogData();
        }

        /// <summary>
        /// ��ѯ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {

            if (btnQuery.Checked)
                return;
            /********************************************************************************
            *���ã��û���������桾��ѯ����ť����¼�ò�����־
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "��ѯ";
            CDSSOperationLog.OperationDescription = "RecordSEQΪ:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            SwitchButton(btnQuery);
            //add by lch 090617 �����ѯ����Ĳ�ѯ�����
            //�޸�Bug���û���������֮�󣬻ص���ѯ���棬��������²�ѯ��ֱ�ӵ���������ݣ������
            frmQuery.lvPatHistory.Items.Clear();
            frmQuery.PatBasicClear();
            ShowPage(frmQuery); 
            this.Cursor = Cursors.Default;
        }
        /**********************************************************************************************************
        *  ����ˣ�XY��
        *  ���ʱ�䣺20090326��
        *  ���˵�������������������Ƿ�ѡ�С��Ƿ���õĲ�ͬ״̬���벻ͬͼƬ��
        *  ��Ӳ��֣���ѯ�����롢��Ϣ¼�롢��Ͻ��ۡ���ʳ�˶����顢��ӡ�����永������գ���С�����˳���ť��
        **********************************************************************************************************/
        private void btnQuery_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnQuery.Image = CDSS.Properties.Resources.��ѯ_press;
        }

        private void btnQuery_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.ClientRectangle.Contains(e.Location))
                this.btnQuery.Image = CDSS.Properties.Resources.��ѯ_over;
            else
                this.btnQuery.Image = CDSS.Properties.Resources.��ѯ_normal;
        }

        private void btnQuery_MouseEnter(object sender, EventArgs e)
        {
            this.btnQuery.Image = CDSS.Properties.Resources.��ѯ_over;
        }
        private void btnQuery_MouseLeave(object sender, EventArgs e)
        {
            this.btnQuery.Image = CDSS.Properties.Resources.��ѯ_normal;
        }

        /// <summary>
        /// ���밴ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            Basicinfo frmNew = new Basicinfo();
            if (frmNew.ShowDialog(this) != DialogResult.OK)
                return;

            //PatInfo.patientid = 0;

            ShowInfoPage(); //�л�����Ϣ¼��ҳ��
            SetDisablePhoto();
            //this.btnNew.Image = CDSS.Properties.Resources.����_disable;
        }
        private void btnNew_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnNew.Image = CDSS.Properties.Resources.����_press;
        }

        private void btnNew_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnNew.Image = CDSS.Properties.Resources.����_over;
            //else
            this.btnNew.Image = CDSS.Properties.Resources.����_normal;
        }

        private void btnNew_MouseEnter(object sender, EventArgs e)
        {
            this.btnNew.Image = CDSS.Properties.Resources.����_over;
        }

        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            this.btnNew.Image = CDSS.Properties.Resources.����_normal;
        }

        /// <summary>
        /// ��Ϣ¼�밴ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfoInput_Click(object sender, EventArgs e)
        {
            if (btnInfoEnter.Checked)
                return;

            /********************************************************************************
            *���ã��û���������桾��Ϣ¼�롿��ť����¼�ò�����־
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "��Ϣ¼��";
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            SwitchButton(btnInfoEnter);
            ShowPage(frmInfo);
            this.Cursor = Cursors.Default;
        }
        private void btnInfoInput_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnInfoEnter.Image = CDSS.Properties.Resources.��Ϣ¼��_press;
        }

        private void btnInfoInput_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnInfoEnter.Image = CDSS.Properties.Resources.��Ϣ¼��_over;
            //else
            this.btnInfoEnter.Image = CDSS.Properties.Resources.��Ϣ¼��_normal;
        }

        private void btnInfoInput_MouseEnter(object sender, EventArgs e)
        {
            this.btnInfoEnter.Image = CDSS.Properties.Resources.��Ϣ¼��_over;
        }

        private void btnInfoInput_MouseLeave(object sender, EventArgs e)
        {
            this.btnInfoEnter.Image = CDSS.Properties.Resources.��Ϣ¼��_normal;
            SetDisablePhoto();
        }
        /**************************************************************************
         * �޸��ˣ�XY��
         * �޸�ʱ�䣺20081221��
         * �޸�˵�������ݱ������Ƿ����������жϼ���ʾ���ܣ�
         * �޸Ĳ��֣��ڡ���Ͻ��۰�ť���������ƽ��鰴ť���������永����ť����
                    �����ҳ�水ť�������NonError�����ж���䡣
         * ��ע����ʱ���ṩ���ص�δ����ҳ�����ı���
         ***************************************************************************/
        /// <summary>
        /// ��Ͻ��۰�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResult_Click(object sender, EventArgs e)
        {
            if (btnResult.Checked)
                return;

            //add by lch 090617 �ڵ����Ͻ��۰�ť��ʱ��ͱ������ݣ������û���������永����ť����ʧ������
            this.frmInfo.LoadDataFromUIToVar();    //������Ϣ¼��ҳ������
            CDSSDBAccess.DBAccess.SaveDataToDB();

            // Added temporaly
            this.btnSave.Text = string.Empty;

            /********************************************************************************
            *���ã��û���������桾��Ͻ��ۡ���ť����¼�ò�����־
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "��Ͻ���";
            CDSSOperationLog.OperationDescription = "RecordSEQΪ:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            this.frmInfo.LoadDataFromUIToVar();
            //Console.WriteLine(GlobalData.PatBasicInfo.PatName);
            //Console.ReadLine();
            if (frmInfo.ShowNonErrMsg())
            {
                MessageBox.Show(frmInfo.ErrorMsg + "�벹�䣡", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }
            SwitchButton(btnResult);
            ShowPage(frmResult);
            this.btnSuggest.Enabled = true; //����Ͻ���֮��ʹ���ƽ��鰴ť����
            SetDisablePhoto();
            this.Cursor = Cursors.Default;
        }

        private void btnResult_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnResult.Image = CDSS.Properties.Resources.��Ͻ���_press;
        }

        private void btnResult_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnResult.Image = CDSS.Properties.Resources.��Ͻ���_over;
            //else
            this.btnResult.Image = CDSS.Properties.Resources.��Ͻ���_normal;
        }

        private void btnResult_MouseEnter(object sender, EventArgs e)
        {
            this.btnResult.Image = CDSS.Properties.Resources.��Ͻ���_over;
        }
        private void btnResult_MouseLeave(object sender, EventArgs e)
        {
            this.btnResult.Image = CDSS.Properties.Resources.��Ͻ���_normal;
            SetDisablePhoto();
        }
        /// <summary>
        /// ��ʳ�˶���ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuggest_Click(object sender, EventArgs e)
        {
            if (btnSuggest.Checked)
                return;

            this.frmResult.LoadDataFromUIToVar();    //��������Ϣ¼��ҳ������
            CDSSDBAccess.DBAccess.SaveDataToDB();
            // Added temporaly
            this.btnSave.Text = string.Empty;

            /********************************************************************************
            *���ã��û���������桾��ʳ�˶����顿��ť����¼�ò�����־
             *add by lch 090615
             *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "��ʳ�˶�����";
            CDSSOperationLog.OperationDescription = "RecordSEQΪ:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            if (frmInfo.ShowNonErrMsg())
            {
                MessageBox.Show(frmInfo.ErrorMsg + "�벹�䣡", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }
            this.btnPrint.Enabled = true;//���ƴ�ӡ��ťֻ�������ƽ��������ſ��ã�Revised by XY��20081222
            SwitchButton(btnSuggest);
            ShowPage(frmSuggest);
            SetDisablePhoto();
            this.Cursor = Cursors.Default;
        }

        private void btnSuggest_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSuggest.Image = CDSS.Properties.Resources.��ʳ�˶�����_press;
        }

        private void btnSuggest_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnSuggest.Image = CDSS.Properties.Resources.��ʳ�˶�����_over;
            //else
            this.btnSuggest.Image = CDSS.Properties.Resources.��ʳ�˶�����_normal;
        }

        private void btnSuggest_MouseEnter(object sender, EventArgs e)
        {
            this.btnSuggest.Image = CDSS.Properties.Resources.��ʳ�˶�����_over;
        }

        private void btnSuggest_MouseLeave(object sender, EventArgs e)
        {
            this.btnSuggest.Image = CDSS.Properties.Resources.��ʳ�˶�����_normal;
            SetDisablePhoto();
        }

        /// <summary>
        /// ���永����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            /********************************************************************************
            *���ã��û���������桾���永������ť����¼�ò�����־
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "���永��";
            CDSSOperationLog.OperationDescription = "RecordSEQΪ:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            this.frmInfo.LoadDataFromUIToVar();    //������Ϣ¼��ҳ������
            this.frmResult.LoadDataFromUIToVar();  //������Ͻ���ҳ������
            this.frmSuggest.LoadDataFromUIToVar(); //�������ƽ���ҳ������

            if (CDSSDBAccess.DBAccess.SaveDataToDB())
            {
                MessageBox.Show("����ɹ���", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //2009-8-28:ccj
                //���水�����°�����ʾ����״̬
                this.btnSave.Text = "";
                //�޸Ľ���

                //2009-9-4��ccj
                //������������ҳ��ĸ��ı�־
                this.frmSuggest.SuggestionDataModified = false;
                //�޸Ľ���
            }
            else
            {
                //this.frmProgress.HideProgressWindow();
                string ErrorInfo = "���没����¼ʱ���ִ���\n��ϸ��Ϣ��";
                ErrorInfo += CDSSDBAccess.DBAccess.LastErrorInfo;
                MessageBox.Show(ErrorInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CDSSDBAccess.DBAccess.ReportCurrentProgress -= new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
            }
            //CDSSDBAccess.DBAccess.ReportCurrentProgress -= new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
            //this.frmProgress.HideProgressWindow();

            this.Cursor = Cursors.Default;
        }
        private void btnSave_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSave.Image = CDSS.Properties.Resources.���永��_press;
        }

        private void btnSave_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnSave.Image = CDSS.Properties.Resources.���永��_over;
            //else
            this.btnSave.Image = CDSS.Properties.Resources.���永��_normal;
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            this.btnSave.Image = CDSS.Properties.Resources.���永��_over;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            this.btnSave.Image = CDSS.Properties.Resources.���永��_normal;

        }
        /// <summary>
        /// ��ӡ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.frmSuggest.LoadDataFromUIToVar();    //��������Ϣ¼��ҳ������
            CDSSDBAccess.DBAccess.SaveDataToDB();
            // Added temporaly
            this.btnSave.Text = string.Empty;

            /********************************************************************************
            *���ã��û���������桾��ӡ����ť����¼�ò�����־
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "��ӡ";
            CDSSOperationLog.OperationDescription = "RecordSEQΪ:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            ShowPage(frmPrint);
            frmPrint.ShowPrtReport();//������ʾ��ӡ��������Revised by XY��20081222.
            //�����ӡҳ��˰�ť�����ã�Ҫ�����ƽ���������ٴο��ã�Revised by XY��20081222.
            this.btnSuggest.Checked = false;//ȡ�����ƽ���ѡ�а�ť��Revised by XY��20081222.
            this.btnPrint.Enabled = false;
            SetDisablePhoto();
            this.Cursor = Cursors.Default;
        }
        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnPrint.Image = CDSS.Properties.Resources.��ӡ_press;
        }

        private void btnPrint_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.ClientRectangle.Contains(e.Location))
                this.btnPrint.Image = CDSS.Properties.Resources.��ӡ_over;
            else
                this.btnPrint.Image = CDSS.Properties.Resources.��ӡ_normal;
        }

        private void btnPrint_MouseEnter(object sender, EventArgs e)
        {
            this.btnPrint.Image = CDSS.Properties.Resources.��ӡ_over;
        }

        private void btnPrint_MouseLeave(object sender, EventArgs e)
        {
            this.btnPrint.Image = CDSS.Properties.Resources.��ӡ_normal;
            SetDisablePhoto();
        }
        /// <summary>
        /// ���ҳ�水ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResume_Click(object sender, EventArgs e)
        {
            /********************************************************************************
            *���ã��û���������桾���ҳ�桿��ť����¼�ò�����־
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "���ҳ��";
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;

            if (frmInfo.ShowNonErrMsg())
            {
                if (MessageBox.Show(frmInfo.ErrorMsg + "�Ƿ��˳�����¼�룿", "��ʾ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //add by wbf 081226 ���������ʾ�Ƿ񱣴�
                    if (frmInfo.DataModifiedCheck())
                    {
                        DialogResult dr = MessageBox.Show("����δȫ�����棬�Ƿ񱣴棿", "��ʾ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            frmInfo.LoadDataFromUIToVar();
                            frmResult.LoadDataFromUIToVar();
                            frmSuggest.LoadDataFromUIToVar();

                            /*2008-9-4��ccj*/
                            //�޸�������ҳ��ת���Զ���������

                            CDSSDBAccess.DBAccess.SaveDataToDB();

                            //�޸Ľ���
                        }
                    }

                    this.frmInfo.ClearData();   //�����Ϣ¼��ҳ��
                    this.frmResult.ClearData(); //�����Ͻ���ҳ��
                    this.frmSuggest.ClearData();//������ƽ���ҳ��
                    ShowWelcomePage();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            else
            {
                //add by wbf 081226 ���������ʾ�Ƿ񱣴�
                if (frmInfo.DataModifiedCheck())
                {
                    DialogResult dr = MessageBox.Show("����δȫ�����棬�Ƿ񱣴棿", "��ʾ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        frmInfo.LoadDataFromUIToVar();
                        frmResult.LoadDataFromUIToVar();
                        frmSuggest.LoadDataFromUIToVar();

                        /*2008-8-27��ccj*/
                        //�޸�������ҳ��ת���Զ���������

                        CDSSDBAccess.DBAccess.SaveDataToDB();

                        //�޸Ľ���
                    }
                }
            }

            this.frmInfo.ClearData();   //�����Ϣ¼��ҳ��
            this.frmResult.ClearData(); //�����Ͻ���ҳ��
            this.frmSuggest.ClearData();//������ƽ���ҳ��
            ShowWelcomePage();
            this.Cursor = Cursors.Default;


            //2009-8-28:ccj
            //ȷ����պ󣬱��水������״̬
            this.btnSave.Text = "";
            //�޸Ľ���

            //���ȫ������
            GlobalData.Clear();

        }

        /// <summary>
        /// ���ð�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (btnSetting.Checked)
                return;

            /********************************************************************************
            *���ã��û���������桾���á���ť����¼�ò�����־            
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "����";
            CDSSOperationLog.OperationDescription = "RecordSEQΪ:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            SwitchButton(btnSetting);
            ShowPage(frmConfigFileEditor);
            this.Cursor = Cursors.Default;
        }    

       
        private void btnResume_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnResume.Image = CDSS.Properties.Resources.���ҳ��_press;
        }

        private void btnResume_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.ClientRectangle.Contains(e.Location))
                this.btnResume.Image = CDSS.Properties.Resources.���ҳ��_over;
            else
                this.btnResume.Image = CDSS.Properties.Resources.���ҳ��_normal;
        }

        private void btnResume_MouseEnter(object sender, EventArgs e)
        {
            this.btnResume.Image = CDSS.Properties.Resources.���ҳ��_over;
        }

        private void btnResume_MouseLeave(object sender, EventArgs e)
        {
            this.btnResume.Image = CDSS.Properties.Resources.���ҳ��_normal;
            SetDisablePhoto();
        }

        #endregion

        #region ��Ӧ�û��¼�
        /// <summary>
        /// ��Ӧ��Ϣ¼��ҳ���GotoResultPageEvent�¼�,�л�����Ͻ���ҳ��
        /// </summary>
        private void frmInfo_OnShowResultPage()
        {
            EventArgs e = new EventArgs();
            btnResult_Click(btnResult, e);
        }

        //2009-8-28:ccj
        //�����������ڱ���ͼ������ʾ����״̬�ı仯
        private void frmInfo_OnShowSave()
        {
            btnSave.Text = "";
        }



        //2009-9-4��ccj
        //������ʾ����ҳ�����ݱ仯
        private void OnShowChange()
        {
            this.btnSave.Text = "*";
        }

        //�޸Ľ���


        /// <summary>
        /// ��Ӧ��ѯҳ���GotoInfoEnterPageEvent�¼�,�л�����Ϣ¼��ҳ��
        /// </summary>
        private void frmQuery_OnShowInfoEnterPage()
        {
            this.Cursor = Cursors.WaitCursor;
            //���ò�����ԴΪ���ݿ�(������)
            PatInfo.bNewPatient = false;
            //CDSSDBAccess.DBAccess.ReportCurrentProgress += new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
            //frmProgress.ShowProgressWindow(this);
            if (!CDSSDBAccess.DBAccess.GetDataFromDB())
            {
                //frmProgress.HideProgressWindow();
                string ErrorInfo = "��ѯ����������¼ʱ���ִ���\n��ϸ��Ϣ��";
                ErrorInfo += CDSSDBAccess.DBAccess.LastErrorInfo;
                MessageBox.Show(ErrorInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CDSSDBAccess.DBAccess.ReportCurrentProgress -= new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
                return;
            }

            //Add BY ZX 20100505 ���벡�˵ļ���ʷ������ʷ�����弲��ʷ��ÿ�λ�������Ĳ�������
            DateTime vistTime = GlobalData.PatBasicInfo.PatVisitDateTime;//����ʱ����ÿ�ξ��ﶼ��ͬ�ģ������ø��м������źã����������ݺ������¸�ֵ
            string sqlstr = "select  RecordSEQ from CDSS_RecordHistory  where PatSEQ ="
                    + " (select TOP 1 PatSEQ from CDSS_RecordHistory where RecordSEQ=" + GlobalData.RecordInfo.RecordSeq
                    + ") order by RecordSEQ desc  "; ///������������ FGJ�޸�
            string recordseq = DBAccess.GetStringScalar(sqlstr);
            if (recordseq != "")
            {
                DBAccess.GetOnlyDataFromDB(Convert.ToInt16(recordseq));
                GlobalData.PatBasicInfo.PatVisitDateTime = vistTime;
            }
            //CDSSDBAccess.DBAccess.ReportCurrentProgress -= new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
            //frmProgress.HideProgressWindow();
            //��ʾ��Ϣ¼��ҳ��
            ShowInfoPage();
            this.Cursor = Cursors.Default;
        }


        /// <summary>
        ///��Ӧ��Ϣ¼��ҳ��ת����BasicInfoҳ��PatBasicInfoChanged�¼������²��˻�����Ϣ��ʾ��
        /// </summary>
        private void frmInfo_OnPatBasicInfoChanged()
        {
            SetPatInfo();
        }

        /// <summary>
        /// ��Ӧ���ݿ���ʽӿڵĴ������
        /// </summary>
        /// <param name="Milestone"></param>
        /// <param name="DetailInfo"></param>
        private void DBAccess_ReportCurrentProgress(int Milestone, string DetailInfo)
        {
            this.frmProgress.SetDetailInfo(DetailInfo);
            this.frmProgress.SetMilestone(Milestone);
        }

        /**************************************************************
         * ����ˣ�XY��
         * ���ʱ�䣺20081223��
         * ���˵��������ʹ�ÿ�ݼ�Ctrl+P��������ӡ�Ի���
         * ��Ӳ��֣���ӡ��ݼ������ô�ӡ�Ի���
         **************************************************************/
        /// <summary>
        /// ��ӡ��ݼ������ô�ӡ�Ի���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
            {
                frmPrint.ShortCutBtn();
            }
        }
        #endregion

        #region ���ܺ���
        /// <summary>
        /// ��ʾָ��ҳ��
        /// </summary>
        /// <param name="frm">ָ��Ҫ��ʾ��ҳ��</param>
        private void ShowPage(Form frm)
        {
            if (frmCurrent != null)
                frmCurrent.Hide();
            //2009-8-28:ccj
            //ȷ����պ󣬱��水������״̬
            //this.btnSave.Text = "";
            //�޸Ľ���
            frmCurrent = frm;
            frmCurrent.Show();
            frmCurrent.Focus();
        }



        /// <summary>
        /// �л���ť״̬
        /// </summary>
        /// <param name="btn">ָ����ǰ����Checked״̬�İ�ť</param>
        private void SwitchButton(CheckBox btn)
        {
            if (btnCurrent != null)
                btnCurrent.Checked = false;
            btnCurrent = btn;
            btnCurrent.Checked = true;
        }



        /// <summary>
        /// ��ҳ��ָ��������ʾ״̬
        /// </summary>
        private void ShowWelcomePage()
        {
            //jyl 081211
            DataSet dsresult = new DataSet();
            int count = 0;
            //�ָ���ť��ʼ״̬
            if (btnCurrent != null)
            {
                btnCurrent.Checked = false;
                btnCurrent = null;
            }
            btnQuery.Enabled = true;
            btnNew.Enabled = true;
            btnInfoEnter.Enabled = false;
            btnResult.Enabled = false;
            btnSuggest.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnResume.Enabled = false;
            btnImport.Enabled = true;

            //jyl 081211

            //���ز�����Ϣ��
            this.pnlPatInfo.Hide();
            //��ʾ��ӭҳ��
            ShowPage(frmWelcom);
            SetDisablePhoto();
        }


        /// <summary>
        /// ���õ�ǰ���ˣ�����ҳ�漰��ť״̬
        /// </summary>
        private void ShowInfoPage()
        {
            //���ð�ť״̬
            this.btnQuery.Enabled = false;
            this.btnNew.Enabled = false;
            this.btnInfoEnter.Enabled = true;
            this.btnResult.Enabled = true;
            //if (PatInfo.bNewPatient)    //�������벡�ˣ��ð�ť��Ҫ�ó���Ͻ���֮�������Ч
            this.btnSuggest.Enabled = false;
            //else
            //    this.btnSuggest.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnPrint.Enabled = false;//���ƴ�ӡ��ťֻ�������ƽ��������ſ��ã�Revised by XY��20081222
            this.btnResume.Enabled = true;
            this.btnImport.Enabled = false;//�������ݵ���Ḳ��ȫ�ֱ������鿴���岡��ʱ���Ƶ��빦�ܣ�Revised by ZX��20100701
            //��ʾ������Ϣҳ�沢���ز�����Ϣ
            this.pnlPatInfo.Show();
            SetPatInfo();

            //��ʾ��Ϣ¼��ҳ��
            //add by ���ޣ�2008-12-23 ��ʾǰ���ȼ��ػ�������
            ShowPage(frmInfo);
            SwitchButton(btnInfoEnter);
            this.frmInfo.InitInfoEnterPage();
            SetDisablePhoto();
        }


        /// <summary>
        /// ���²�����Ϣ����Ϣ
        /// </summary>
        private void SetPatInfo()
        {
            this.lblName.Text = GlobalData.PatBasicInfo.PatName;
            this.lblSex.Text = GlobalData.PatBasicInfo.PatSex;
            int Age = 0;
            try
            {
                Age = DateTime.Now.Year - Convert.ToDateTime(GlobalData.PatBasicInfo.PatBirthday).Year;
            }
            catch (System.Exception e)
            {
                Age = -1;
            }
            if (Age > 150 || Age < 0)
                this.lblAge.Text = string.Empty;
            else
                this.lblAge.Text = Age.ToString();
            string strProvince = GlobalData.PatBasicInfo.PatBirthProvince;
            string strCity = GlobalData.PatBasicInfo.PatBirthCity;
            this.lblFrom.Text = strProvince + strCity;
        }

        /// <summary>
        /// ���밴ť������״̬ʱͼ�� Add by XY 20090325
        /// </summary>
        private void SetDisablePhoto()
        {
            if (this.btnQuery.Enabled == false)
            {
                this.btnQuery.Image = CDSS.Properties.Resources.��ѯ_disable;
            }
            else
            {
                this.btnQuery.Image = CDSS.Properties.Resources.��ѯ_normal;
            }
            if (this.btnNew.Enabled == false)
            {
                this.btnNew.Image = CDSS.Properties.Resources.����_disable;
            }
            else
            {
                this.btnNew.Image = CDSS.Properties.Resources.����_normal;
            }
            if (this.btnInfoEnter.Enabled == false)
            {
                this.btnInfoEnter.Image = CDSS.Properties.Resources.��Ϣ¼��_disable;
            }
            else
            {
                this.btnInfoEnter.Image = CDSS.Properties.Resources.��Ϣ¼��_normal;
            }
            if (this.btnResult.Enabled == false)
            {
                this.btnResult.Image = CDSS.Properties.Resources.��Ͻ���_disable;
            }
            else
            {
                this.btnResult.Image = CDSS.Properties.Resources.��Ͻ���_normal;
            }
            if (this.btnSuggest.Enabled == false)
            {
                this.btnSuggest.Image = CDSS.Properties.Resources.��ʳ�˶�����_disable;
            }
            else
            {
                this.btnSuggest.Image = CDSS.Properties.Resources.��ʳ�˶�����_normal;
            }
            if (this.btnPrint.Enabled == false)
            {
                this.btnPrint.Image = CDSS.Properties.Resources.��ӡ_disable;
            }
            else
            {
                this.btnPrint.Image = CDSS.Properties.Resources.��ӡ_normal;
            }
            if (this.btnSave.Enabled == false)
            {
                this.btnSave.Image = CDSS.Properties.Resources.���永��_disable;
            }
            else
            {
                this.btnSave.Image = CDSS.Properties.Resources.���永��_normal;
            }
            if (this.btnResume.Enabled == false)
            {
                this.btnResume.Image = CDSS.Properties.Resources.���ҳ��_disable;
            }
            else
            {
                this.btnResume.Image = CDSS.Properties.Resources.���ҳ��_normal;
            }
        }
        #endregion

        /// <summary>
        /// //add by lch 090605 ���û��������ťclick�¼�����ʾ�û����봰��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            //if (btnImport.Checked)
            //    return;

            /********************************************************************************
            *���ã��û���������桾���ݵ��롿��ť����¼�ò�����־
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "���ݵ���";
            CDSSOperationLog.OperationDescription = "RecordSEQΪ:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            //this.Cursor = Cursors.WaitCursor;
            //SwitchButton(btnImport);
            //ShowPage(frmFeedback);
            //this.Cursor = Cursors.Default;

            //this.Cursor = Cursors.WaitCursor;
            //SwitchButton(btnImport);
            //this.pnlPatInfo.Hide();
            //this.frmImport.ShowDialog();
            //this.ShowPage(frmImport);
            //this.Cursor = Cursors.Default;

            this.cmsImport.Show(this.btnImport, 0, this.btnImport.Height);
        }

        //�˴��޸ĵ��밴ťͼ����ʾ�������������ʾ��Ӧ�ı仯������Ӧ�¼�Ϊ�Ժ���չ��
        private void btnImport_MouseDown(object sender, MouseEventArgs e)
        {
            //this.btnImport.Image = CDSS.Properties.Resources.����_press;
        }

        private void btnImport_MouseEnter(object sender, EventArgs e)
        {
            //this.btnImport.Image = CDSS.Properties.Resources.����_over;
        }

        private void btnImport_MouseLeave(object sender, EventArgs e)
        {
            //this.btnImport.Image = CDSS.Properties.Resources.����_normal;
            //SetDisablePhoto();

        }

        private void btnImport_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnImport.Image = CDSS.Properties.Resources.����_over;
            //else
            //    this.btnImport.Image = CDSS.Properties.Resources.����_normal;
        }

        private void tsmiOrdImport_Click(object sender, EventArgs e)
        {
            this.ShowPage(frmImport);
        }

        private void tsmiCheckImport_Click(object sender, EventArgs e)
        {
            this.ShowPage(frmDataVer);
        }

        private void tsmiDBMerge_Click(object sender, EventArgs e)
        {
            this.ShowPage(frmImportDBMerge);
        }

        private void chbStastic_Click(object sender, EventArgs e)
        {
            if (this.chbStastic.Checked)
                return;
            this.Cursor = Cursors.WaitCursor;
            SwitchButton(this.chbStastic);

            this.ShowPage(frmStatistic);
            this.Cursor = Cursors.Default;
        }

        private void btnQuery_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}