    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;


namespace CDSS
{
    public partial class InfoEnter : Form
    {
        //�¼�����
        public event CustomEventHandle ShowResultPageEvent; 

        //2009-8-28:ccj
        //�¼�1����ʾ�����ѱ���
        public event CustomEventHandle ShowSaveEvent;
        //�¼�2����ʾ�����Ѹ���
        public event CustomEventHandle ShowChangeEvent;
        //�޸Ľ���

        private bool save = false;
        public static Form pendingForm;
        Resultinfo result;

        //�����ж�QuickInfoEnterForm���������Ƿ����仯
        public static bool judgeTextChanged=false;

        private Basicinfo frmBasicInfo = new Basicinfo();
        private PerHistory frmPerHistory = new PerHistory();
        private FamilyDH frmFamilyDH = new FamilyDH();
        private AGMinfo frmAGMinfo = new AGMinfo();
        private Hypertension frmHypertension = new Hypertension();
        private LipidsDisorder frmLipidsDisorder = new LipidsDisorder();
        private Hyperuricuria frmHyperuricuria = new Hyperuricuria();
        private Nephropathy frmNephropathy = new Nephropathy();
        private OtherDiseaseHistoryinfo frmOtherDHinfo = new OtherDiseaseHistoryinfo();
        private OtherExaminfo frmOtherExaminfo = new OtherExaminfo();
        private Labinfo frmLabinfo = new Labinfo();
        private Phyinfo frmPhyinfo = new Phyinfo();
        private QuickInfoEnterForm frmQuickInfoEnter=new QuickInfoEnterForm ();

        private InfoFormBaseClass frmCurrent;    //ҳ���Ұ벿�ֵ�ǰ��ʾ�Ĵ���

        public InfoEnter()
        {
            InitializeComponent();
            //���Ӷ��Ӵ���DataChanged�¼��Ĵ���
        }
        

        /// <summary>
        /// ��load�¼��м��ظ����Ӵ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoEnter_Load(object sender, EventArgs e)
        {
            frmBasicInfo.TopLevel = false;
            frmBasicInfo.FormBorderStyle = FormBorderStyle.None;
            frmBasicInfo.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmBasicInfo);
            this.frmBasicInfo.UseNextButton = true;
            this.frmBasicInfo.DataChanged += this.ChildWinDataChanged;
            this.frmBasicInfo.PatBasicInfoChanged += this.frmBasicInfo_OnPatBasicInfoChanged;
            this.frmBasicInfo.ShowNextPage += this.ShowNextPage;
            

            frmPerHistory.TopLevel = false;
            frmPerHistory.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmPerHistory);
            this.frmPerHistory.DataChanged += this.ChildWinDataChanged;
            this.frmPerHistory.ShowNextPage += this.ShowNextPage;

            frmFamilyDH.TopLevel = false;
            frmFamilyDH.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmFamilyDH);
            this.frmFamilyDH.DataChanged += this.ChildWinDataChanged;
            this.frmFamilyDH.ShowNextPage += this.ShowNextPage;

            frmAGMinfo.TopLevel = false;
            frmAGMinfo.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmAGMinfo);
            this.frmAGMinfo.DataChanged += this.ChildWinDataChanged;
            this.frmAGMinfo.ShowNextPage += this.ShowNextPage;

            frmHypertension.TopLevel = false;
            frmHypertension.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmHypertension);
            this.frmHypertension.DataChanged += this.ChildWinDataChanged;
            this.frmHypertension.ShowNextPage += this.ShowNextPage;

            frmLipidsDisorder.TopLevel = false;
            frmLipidsDisorder.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmLipidsDisorder);
            this.frmLipidsDisorder.DataChanged += this.ChildWinDataChanged;
            this.frmLipidsDisorder.ShowNextPage += this.ShowNextPage;

            frmHyperuricuria.TopLevel = false;
            frmHyperuricuria.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmHyperuricuria);
            this.frmHyperuricuria.DataChanged += this.ChildWinDataChanged;
            this.frmHyperuricuria.ShowNextPage += this.ShowNextPage;

            frmNephropathy.TopLevel = false;
            frmNephropathy.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmNephropathy);
            this.frmNephropathy.DataChanged += this.ChildWinDataChanged;
            this.frmNephropathy.ShowNextPage += this.ShowNextPage;

            frmOtherDHinfo.TopLevel = false;
            frmOtherDHinfo.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmOtherDHinfo);
            this.frmOtherDHinfo.DataChanged += this.ChildWinDataChanged;
            this.frmOtherDHinfo.ShowNextPage += this.ShowNextPage;

            frmOtherExaminfo.TopLevel = false;
            frmOtherExaminfo.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmOtherExaminfo);
            this.frmOtherExaminfo.DataChanged += this.ChildWinDataChanged;

            frmLabinfo.TopLevel = false;
            frmLabinfo.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmLabinfo);
            this.frmLabinfo.DataChanged += this.ChildWinDataChanged;
            this.frmLabinfo.ShowNextPage += this.ShowNextPage;

            frmPhyinfo.TopLevel = false;
            frmPhyinfo.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(frmPhyinfo);
            this.frmPhyinfo.DataChanged += this.ChildWinDataChanged;
            this.frmPhyinfo.ShowNextPage += this.ShowNextPage;

            frmQuickInfoEnter .TopLevel =false;
            frmQuickInfoEnter.Dock=DockStyle.Fill;
            pnlInfo.Controls.Add (frmQuickInfoEnter );
            this.frmQuickInfoEnter.DataChanged += this.ChildWinDataChanged;
        }

        private void ShowPage(InfoFormBaseClass frm)
        {
            ShowPage(frm, false);
        }

        /// <summary>
        /// ��ʾָ������
        /// </summary>
        /// <param name="frm">ָ��Ҫ��ʾ�Ĵ���</param>
        private void ShowPage(InfoFormBaseClass frm, bool bLoadDataFromUIToVar)
        {
            if (frmCurrent != null)
            {
                if (bLoadDataFromUIToVar)
                {
                  /*2008-8-27��ccj*/
                  //�޸�������ҳ��ת���Զ���������
                  frmCurrent.LoadDataFromUIToVar();
                  CDSSDBAccess.DBAccess.SaveDataToDB();
                  ShowSave();
                  //�޸Ľ���
                }
                frmCurrent.Hide();
            }
            frmCurrent = frm;
            frmCurrent.Show();
            frmCurrent.Focus();
            this.btnSave.Enabled = frmCurrent.IsModified;
        }
        

        /// <summary>
        /// ����treeView�е�ѡ���л�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //����������̲����ÿؼ�ʱ����Ч
            if (e.Action != TreeViewAction.ByMouse && e.Action != TreeViewAction.ByKeyboard)
                return;

            //���޸�QuickInfoEnterForm���������Ǹı�������������bLoaded״̬
            if (judgeTextChanged == true)
            {
                this.frmAGMinfo.bLoaded = false;
                this.frmBasicInfo.bLoaded = false;
                this.frmFamilyDH.bLoaded = false;
                this.frmHypertension.bLoaded = false;
                this.frmHyperuricuria.bLoaded = false;
                this.frmLabinfo.bLoaded = false;
                this.frmLipidsDisorder.bLoaded = false;
                this.frmNephropathy.bLoaded = false;
                this.frmOtherDHinfo.bLoaded = false;
                this.frmOtherExaminfo.bLoaded = false;
                this.frmPerHistory.bLoaded = false;
                this.frmPhyinfo.bLoaded = false;               
            }

            this.Cursor = Cursors.WaitCursor;
            switch (e.Node.Text)
            {
                case "���˻�����Ϣ":
                    ShowPage(frmBasicInfo,true);
                    break;

                case "����ʷ":
                case "�Ǵ�л�쳣":
                    ShowPage(frmAGMinfo, true);
                    break;

                case "��Ѫѹ":
                    ShowPage(frmHypertension, true);
                    break;

                case "Ѫ֬����":
                    ShowPage(frmLipidsDisorder, true);
                    break;

                case "������Ѫ֢":
                    ShowPage(frmHyperuricuria, true);
                    break;

                case "���������༲��":
                    ShowPage(frmNephropathy, true);
                    break;

                case "��������ʷ":
                    ShowPage(frmOtherDHinfo, true);
                    break;

                case "����ʷ":
                    ShowPage(frmPerHistory, true);
                    break;

                case "���弲��ʷ":
                    ShowPage(frmFamilyDH, true);
                    break;

                case "�����":
                    ShowPage(frmPhyinfo, true);
                    break;

                case "ʵ���Ҽ��":
                    ShowPage(frmLabinfo, true);
                    break;

                case "�������":
                    ShowPage(frmOtherExaminfo, true);
                    break;
                case "������Ϣ����":
                    ShowPage(frmQuickInfoEnter, true);
                    break;
            }
            this.Cursor = Cursors.Default;
         }
        
        /// <summary>
        /// ����ȫ��
        /// </summary>
        public void LoadDataFromUIToVar()
        {
            //д��db���е�info�����ж��Ƿ�����Щ��Ϣ�Ǳ�������ǻ�û�����룬��ʾ���б�����д�Ļ�û����д����Ŀ
            //������Ϊ����ô�죿��ο��ƣ�
            //���˻�����Ϣ
            if (frmBasicInfo.IsModified)
                frmBasicInfo.LoadDataFromUIToVar();
            //����ʷ
            if (frmAGMinfo.IsModified)
                frmAGMinfo.LoadDataFromUIToVar();
            if (frmHypertension.IsModified)
                frmHypertension.LoadDataFromUIToVar();
            if (frmLipidsDisorder.IsModified)
                frmLipidsDisorder.LoadDataFromUIToVar();
            if (frmHyperuricuria.IsModified)
                frmHyperuricuria.LoadDataFromUIToVar();
            if (frmNephropathy.IsModified)
                frmNephropathy.LoadDataFromUIToVar();
            if (frmOtherDHinfo.IsModified)
                frmOtherDHinfo.LoadDataFromUIToVar();
            //ʵ������Ϣ
            if (frmLabinfo.IsModified)
                frmLabinfo.LoadDataFromUIToVar();
            //�������Ϣ
            if (frmPhyinfo.IsModified)
                frmPhyinfo.LoadDataFromUIToVar();
            //���������Ϣ
            if (frmOtherExaminfo.IsModified)
                frmOtherExaminfo.LoadDataFromUIToVar();
            //����ʷ��Ϣ
            if (frmPerHistory.IsModified)
                frmPerHistory.LoadDataFromUIToVar();
            //����ʷ��Ϣ
            if (frmFamilyDH.IsModified)
                frmFamilyDH.LoadDataFromUIToVar();
            //������Ϣ����
            if (frmQuickInfoEnter.IsModified)
                frmQuickInfoEnter.LoadDataFromUIToVar();
        }
        

        /// <summary>
        /// add by wbf 081226
        /// �ж��Ƿ���ҳ����Ϣδ����
        /// </summary>
        /// <returns></returns>
        public bool DataModifiedCheck()
        {           
            if (frmBasicInfo.IsModified || frmAGMinfo.IsModified ||
                frmHypertension.IsModified || frmLipidsDisorder.IsModified ||
                frmHyperuricuria.IsModified || frmNephropathy.IsModified ||
                frmOtherDHinfo.IsModified || frmLabinfo.IsModified ||
                frmPhyinfo.IsModified || frmOtherExaminfo.IsModified ||
                frmPerHistory.IsModified || frmFamilyDH.IsModified || frmQuickInfoEnter.IsModified)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��ʼ����Ϣ¼��ҳ��
        /// </summary>
        public void InitInfoEnterPage()
        {
            if (PatInfo.bNewPatient)
            {//���벡�������,ʹ����¼��ڵ�Ĭ��ѡ��
                this.TreeView.SelectedNode = this.TreeView.Nodes[7];
                ShowPage(frmQuickInfoEnter);
                frmQuickInfoEnter.Focus();
            }
            else
            {//��ʷ���������,ʹ���˻�����Ϣ�ڵ�Ĭ��ѡ��
                this.TreeView.SelectedNode = this.TreeView.Nodes[0];
                ShowPage(frmBasicInfo);
                frmBasicInfo.Focus();
            }
        }


        /// <summary>
        /// �ú������������¼�
        /// </summary>
        private void ShowResultPage()
        {
            CustomEventHandle temp = ShowResultPageEvent;
            if (temp != null)
                temp();
        }

/// <summary>
/// �����¼���֪ͨҳ�������ѱ���
/// </summary>
        private void ShowSave()
        {
            CustomEventHandle temp = ShowSaveEvent;
            if (temp != null)
             {
                temp();
             }
        }

/// <summary>
/// �����¼���֪ͨҳ�������и���
/// </summary>
        private void ShowChange()
        {
            CustomEventHandle temp = ShowChangeEvent;
            if (temp != null)
             {
                temp();
              }
        }

        /**************************************************************************
        * ����ˣ�XY��
        * ���ʱ�䣺20081221��
        * ���˵�������ݱ������Ƿ����������жϼ���ʾ���ܣ�
        * ��Ӳ��֣���ӡ��жϱ�ѡ��Ϊ��ʱ����������
                    �޸ġ�����ȫ����ť�����¼����͡���Ͻ��۰�ť�����¼�����
        **************************************************************************/
        /// <summary>
        /// �жϱ�ѡ��Ϊ��ʱ������
        /// </summary>
        /// <returns></returns>
        public string ErrorMsg = "";
        public bool ShowNonErrMsg()
        {
            bool ErrJudge = false;

            List<string> lstErrorMsg = new List<string>();
            if (frmBasicInfo.ForbidNon())
            {
                int nCurIndex = lstErrorMsg.Count + 1;
                lstErrorMsg.Add(nCurIndex.ToString() + "�����˻�����Ϣҳ��\r\n");
                ErrJudge = true;
            }
            if (frmPerHistory.ForbidNon())
            {
                int nCurIndex = lstErrorMsg.Count + 1;
                lstErrorMsg.Add(nCurIndex.ToString() + "������ʷҳ��\r\n");
                ErrJudge = true;
            }
            if (frmPhyinfo.ForbidNon())
            {
                int nCurIndex = lstErrorMsg.Count + 1;
                lstErrorMsg.Add(nCurIndex.ToString() + "�������ҳ��\r\n");
                ErrJudge = true;
            }
            if (frmLabinfo.ForbidNon())
            {
                int nCurIndex = lstErrorMsg.Count + 1;
                lstErrorMsg.Add(nCurIndex.ToString() + "��ʵ���Ҽ��ҳ��\r\n");
                ErrJudge = true;
            }
            if (ErrJudge == true)
            {
                ErrorMsg = "*Ϊ�����������Ϣ��δ��д������\r\n";
                for (int i = 0; i < lstErrorMsg.Count; i++)
                {
                    ErrorMsg += lstErrorMsg[i];
                }
                //ErrorMsg += "�벹�䣡";
                //MessageBox.Show(ErrorMsg ,"δ������ʾ");
                return true;
            }
            return false;
        }
        

        /// <summary>
        /// ���水ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;            
            btnSave.Enabled = false;
            frmCurrent.LoadDataFromUIToVar();

            /*2008-8-27��ccj*/
            //�޸�������ҳ��ת���Զ���������

            this.LoadDataFromUIToVar();
            CDSSDBAccess.DBAccess.SaveDataToDB();

            //�޸Ľ���
            this.Cursor = Cursors.Default;
        }


        /// <summary>
        /// ����ȫ����ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;            
            btnSaveAll.Enabled = false;
            LoadDataFromUIToVar();
            btnSaveAll.Enabled = true;
            btnSave.Enabled = false;    //ʹ���水ť����
            this.Cursor = Cursors.Default;
            if (ShowNonErrMsg())
            {
                MessageBox.Show(ErrorMsg + "�벹�䣡", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(this,"��Ϣ�Ѿ�ȫ�����棡�Ƿ�鿴��Ͻ��ۣ�", "��ʾ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                ShowResultPage();
        }


        /// <summary>
        /// ��Ͻ��۰�ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGotoResultPage_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnGotoResultPage.Enabled = false;
            //������Ϣ¼��ҳ������
            LoadDataFromUIToVar();
            if (ShowNonErrMsg())
            {
                MessageBox.Show(ErrorMsg + "�벹�䣡", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGotoResultPage.Enabled = true;
                this.Cursor = Cursors.Default;
                return;
            }
            //�����¼���֪ͨ����ܽ�ҳ���л�����Ͻ���ҳ��
            ShowResultPage();
            btnGotoResultPage.Enabled = true;
            this.Cursor = Cursors.Default;
        }


        /// <summary>
        /// �����Ӵ������ݸı���¼�,���Ӵ������ݸı��,��Ҫ�����水ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildWinDataChanged(object sender, EventArgs e)
        {
            ShowChange(); 
            if ((sender == frmCurrent) && (!this.btnSave.Enabled))
                this.btnSave.Enabled = true;
        }


        /// <summary>
        /// ������Ӵ��������
        /// </summary>
        public void ClearData()
        {
            frmAGMinfo.ClearData();
            frmBasicInfo.ClearData();
            frmHypertension.ClearData();
            frmHyperuricuria.ClearData();
            frmLabinfo.ClearData();
            frmLipidsDisorder.ClearData();
            frmNephropathy.ClearData();
            frmOtherDHinfo.ClearData();
            frmOtherExaminfo.ClearData();
            frmPerHistory.ClearData();
            frmPhyinfo.ClearData();
            frmFamilyDH.ClearData();
            frmQuickInfoEnter.ClearData();
        }


        /// <summary>
        /// ��mainform����frmBasicInfoҳ��PatBasicInfoChanged�¼�������֪ͨmainform���²�����Ϣ��
        /// </summary>
        public event CustomEventHandle PatBasicInfoChanged;
        protected void RaisePatBasicInfoChangedEvent()
        {
            CustomEventHandle temp = PatBasicInfoChanged;
            if (temp != null)
                temp();
        }

        /// <summary>
        /// ��ӦfrmBasicInfoҳ��PatBasicInfoChanged�¼�
        /// </summary>
        private void frmBasicInfo_OnPatBasicInfoChanged()
        {
            RaisePatBasicInfoChangedEvent();
        }

        private void ShowNextPage(object sender, string formName)
        {
            switch (formName)
            {
                case "Basicinfo":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[1].Nodes[0];
                    this.ShowPage(this.frmAGMinfo);
                    break;
                case "AGMinfo":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[1].Nodes[1];
                    this.ShowPage(this.frmHypertension);
                    break;
                case "Hypertension":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[1].Nodes[2];
                    this.ShowPage(this.frmLipidsDisorder);
                    break;
                case "LipidsDisorder":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[1].Nodes[3];
                    this.ShowPage(this.frmHyperuricuria);
                    break;
                case "Hyperuricuria":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[1].Nodes[4];
                    this.ShowPage(this.frmNephropathy);
                    break;
                case "Nephropathy":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[1].Nodes[5];
                    this.ShowPage(this.frmOtherDHinfo);
                    break;
                case "OtherDiseaseHistoryinfo":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[2];
                    this.ShowPage(this.frmPerHistory);
                    break;
                case "PerHistory":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[3];
                    this.ShowPage(this.frmFamilyDH);
                    break;
                case "FamilyDH":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[4];
                    this.ShowPage(this.frmPhyinfo);
                    break;
                case "Phyinfo":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[5];
                    this.ShowPage(this.frmLabinfo);
                    break;
                case "Labinfo":
                    this.TreeView.SelectedNode = this.TreeView.Nodes[6];
                    this.ShowPage(this.frmOtherExaminfo);
                    break;   
                default:
                    break;
            }
        }        
    }
}