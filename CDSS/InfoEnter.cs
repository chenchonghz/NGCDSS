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
        //事件定义
        public event CustomEventHandle ShowResultPageEvent; 

        //2009-8-28:ccj
        //事件1：显示数据已保存
        public event CustomEventHandle ShowSaveEvent;
        //事件2：显示数据已更改
        public event CustomEventHandle ShowChangeEvent;
        //修改结束

        private bool save = false;
        public static Form pendingForm;
        Resultinfo result;

        //用来判断QuickInfoEnterForm界面内容是否发生变化
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

        private InfoFormBaseClass frmCurrent;    //页面右半部分当前显示的窗体

        public InfoEnter()
        {
            InitializeComponent();
            //增加对子窗体DataChanged事件的处理
        }
        

        /// <summary>
        /// 在load事件中加载各个子窗体
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
        /// 显示指定窗体
        /// </summary>
        /// <param name="frm">指定要显示的窗体</param>
        private void ShowPage(InfoFormBaseClass frm, bool bLoadDataFromUIToVar)
        {
            if (frmCurrent != null)
            {
                if (bLoadDataFromUIToVar)
                {
                  /*2008-8-27：ccj*/
                  //修改描述：页面转换自动保存数据
                  frmCurrent.LoadDataFromUIToVar();
                  CDSSDBAccess.DBAccess.SaveDataToDB();
                  ShowSave();
                  //修改结束
                }
                frmCurrent.Hide();
            }
            frmCurrent = frm;
            frmCurrent.Show();
            frmCurrent.Focus();
            this.btnSave.Enabled = frmCurrent.IsModified;
        }
        

        /// <summary>
        /// 根据treeView中的选择切换页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //仅当鼠标或键盘操作该控件时才有效
            if (e.Action != TreeViewAction.ByMouse && e.Action != TreeViewAction.ByKeyboard)
                return;

            //当修改QuickInfoEnterForm界面内容是改变各相关输入界面的bLoaded状态
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
                case "病人基本信息":
                    ShowPage(frmBasicInfo,true);
                    break;

                case "疾病史":
                case "糖代谢异常":
                    ShowPage(frmAGMinfo, true);
                    break;

                case "高血压":
                    ShowPage(frmHypertension, true);
                    break;

                case "血脂紊乱":
                    ShowPage(frmLipidsDisorder, true);
                    break;

                case "高尿酸血症":
                    ShowPage(frmHyperuricuria, true);
                    break;

                case "非糖尿病肾脏疾病":
                    ShowPage(frmNephropathy, true);
                    break;

                case "其它疾病史":
                    ShowPage(frmOtherDHinfo, true);
                    break;

                case "个人史":
                    ShowPage(frmPerHistory, true);
                    break;

                case "家族疾病史":
                    ShowPage(frmFamilyDH, true);
                    break;

                case "体格检查":
                    ShowPage(frmPhyinfo, true);
                    break;

                case "实验室检查":
                    ShowPage(frmLabinfo, true);
                    break;

                case "其它检查":
                    ShowPage(frmOtherExaminfo, true);
                    break;
                case "快速信息输入":
                    ShowPage(frmQuickInfoEnter, true);
                    break;
            }
            this.Cursor = Cursors.Default;
         }
        
        /// <summary>
        /// 保存全部
        /// </summary>
        public void LoadDataFromUIToVar()
        {
            //写入db所有的info，并判断是否有哪些信息是必须项，但是还没有输入，提示所有必须填写的还没有填写的项目
            //如果编号为空怎么办？如何控制？
            //病人基本信息
            if (frmBasicInfo.IsModified)
                frmBasicInfo.LoadDataFromUIToVar();
            //疾病史
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
            //实验室信息
            if (frmLabinfo.IsModified)
                frmLabinfo.LoadDataFromUIToVar();
            //体格检查信息
            if (frmPhyinfo.IsModified)
                frmPhyinfo.LoadDataFromUIToVar();
            //其它检查信息
            if (frmOtherExaminfo.IsModified)
                frmOtherExaminfo.LoadDataFromUIToVar();
            //个人史信息
            if (frmPerHistory.IsModified)
                frmPerHistory.LoadDataFromUIToVar();
            //家族史信息
            if (frmFamilyDH.IsModified)
                frmFamilyDH.LoadDataFromUIToVar();
            //快速信息输入
            if (frmQuickInfoEnter.IsModified)
                frmQuickInfoEnter.LoadDataFromUIToVar();
        }
        

        /// <summary>
        /// add by wbf 081226
        /// 判断是否有页面信息未保存
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
        /// 初始化信息录入页面
        /// </summary>
        public void InitInfoEnterPage()
        {
            if (PatInfo.bNewPatient)
            {//新入病人情况下,使快速录入节点默认选中
                this.TreeView.SelectedNode = this.TreeView.Nodes[7];
                ShowPage(frmQuickInfoEnter);
                frmQuickInfoEnter.Focus();
            }
            else
            {//历史病人情况下,使病人基本信息节点默认选中
                this.TreeView.SelectedNode = this.TreeView.Nodes[0];
                ShowPage(frmBasicInfo);
                frmBasicInfo.Focus();
            }
        }


        /// <summary>
        /// 该函数用于引发事件
        /// </summary>
        private void ShowResultPage()
        {
            CustomEventHandle temp = ShowResultPageEvent;
            if (temp != null)
                temp();
        }

/// <summary>
/// 引发事件，通知页面数据已保存
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
/// 引发事件，通知页面数据有更改
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
        * 添加人：XY；
        * 添加时间：20081221；
        * 添加说明：数据必填项是否输入完整判断及提示功能；
        * 添加部分：添加“判断必选项为空时报错函数”；
                    修改“保存全部按钮单击事件”和“诊断结论按钮单击事件”。
        **************************************************************************/
        /// <summary>
        /// 判断必选项为空时报错函数
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
                lstErrorMsg.Add(nCurIndex.ToString() + "、病人基本信息页面\r\n");
                ErrJudge = true;
            }
            if (frmPerHistory.ForbidNon())
            {
                int nCurIndex = lstErrorMsg.Count + 1;
                lstErrorMsg.Add(nCurIndex.ToString() + "、个人史页面\r\n");
                ErrJudge = true;
            }
            if (frmPhyinfo.ForbidNon())
            {
                int nCurIndex = lstErrorMsg.Count + 1;
                lstErrorMsg.Add(nCurIndex.ToString() + "、体格检查页面\r\n");
                ErrJudge = true;
            }
            if (frmLabinfo.ForbidNon())
            {
                int nCurIndex = lstErrorMsg.Count + 1;
                lstErrorMsg.Add(nCurIndex.ToString() + "、实验室检查页面\r\n");
                ErrJudge = true;
            }
            if (ErrJudge == true)
            {
                ErrorMsg = "*为必填项，以下信息尚未填写完整：\r\n";
                for (int i = 0; i < lstErrorMsg.Count; i++)
                {
                    ErrorMsg += lstErrorMsg[i];
                }
                //ErrorMsg += "请补充！";
                //MessageBox.Show(ErrorMsg ,"未填项提示");
                return true;
            }
            return false;
        }
        

        /// <summary>
        /// 保存按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;            
            btnSave.Enabled = false;
            frmCurrent.LoadDataFromUIToVar();

            /*2008-8-27：ccj*/
            //修改描述：页面转换自动保存数据

            this.LoadDataFromUIToVar();
            CDSSDBAccess.DBAccess.SaveDataToDB();

            //修改结束
            this.Cursor = Cursors.Default;
        }


        /// <summary>
        /// 保存全部按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;            
            btnSaveAll.Enabled = false;
            LoadDataFromUIToVar();
            btnSaveAll.Enabled = true;
            btnSave.Enabled = false;    //使保存按钮灰显
            this.Cursor = Cursors.Default;
            if (ShowNonErrMsg())
            {
                MessageBox.Show(ErrorMsg + "请补充！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(this,"信息已经全部保存！是否查看诊断结论？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                ShowResultPage();
        }


        /// <summary>
        /// 诊断结论按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGotoResultPage_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnGotoResultPage.Enabled = false;
            //保存信息录入页面数据
            LoadDataFromUIToVar();
            if (ShowNonErrMsg())
            {
                MessageBox.Show(ErrorMsg + "请补充！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGotoResultPage.Enabled = true;
                this.Cursor = Cursors.Default;
                return;
            }
            //引发事件，通知主框架将页面切换至诊断结论页面
            ShowResultPage();
            btnGotoResultPage.Enabled = true;
            this.Cursor = Cursors.Default;
        }


        /// <summary>
        /// 处理子窗体数据改变的事件,当子窗体数据改变后,需要将保存按钮亮显
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
        /// 清除各子窗体的数据
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
        /// 向mainform传递frmBasicInfo页的PatBasicInfoChanged事件，用于通知mainform更新病人信息栏
        /// </summary>
        public event CustomEventHandle PatBasicInfoChanged;
        protected void RaisePatBasicInfoChangedEvent()
        {
            CustomEventHandle temp = PatBasicInfoChanged;
            if (temp != null)
                temp();
        }

        /// <summary>
        /// 响应frmBasicInfo页的PatBasicInfoChanged事件
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