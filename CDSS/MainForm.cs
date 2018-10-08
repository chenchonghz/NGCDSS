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
using System.Runtime.InteropServices;//API 命名空间
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
        private UserFeedback frmFeedback = new UserFeedback();//add by lch 090605 实例化用户导入窗体
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
        private DataVerification frmDataVer = new DataVerification();//数据校验界面
        private delegate void FlushClient(string RNum);//代理        
        //added by yanhui 20120319
        private ImportDBMerge frmImportDBMerge = new ImportDBMerge(); 
        /******************************************************        
        * Revise History：
        * 2008-12-22 杨艳 添加FormClosing事件，窗口关闭前的响应
        ******************************************************/
        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            //add by lch  090617 在主界面上显示版本号
            this.Text = "代谢综合征诊疗决策支持软件   版本：" + GlobalData.UserInfo.CurrentAppVer;
           
            /********************************************************************************
             *作用：用户点击【登录】按钮，主界面加载，表示成功登录，记录该操作日志
             *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = GlobalData.UserInfo.LoginConnDBTime;//修改by lch 090616 设置连接数据库时间与登录时间一致
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "登录";
            CDSSOperationLog.OperationDescription = "当前软件的版本号为:" + GlobalData.UserInfo.CurrentAppVer;//软件版本信息
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
        /// //add by zx 100608 检测软件是否已经注册
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

        #region 响应系统事件
        private void MainForm_Load(object sender, EventArgs e)
        {
            UserLogRecycle.UserLogRecycleControl();
            //修改by Jyl，081223，bugA3删除登录窗体显示操作，移到Program.cs内，整个程序的最开始执行
            ////登录
            //Login frmLogin = new Login();
            //if (frmLogin.ShowDialog() != DialogResult.OK)
            //{
            //    this.Close();              
            //}
            //获取必填项控制开关状态
            if (ConfigurationManager.AppSettings["MustFill"] == "1")
                PatInfo.bMustFill = true;
            else
                PatInfo.bMustFill = false;
            //页面显示初始化
            frmWelcom.TopLevel = false;
            frmWelcom.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmWelcom);

            //导入界面初始化
            frmImport.TopLevel = false;
            frmImport.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmImport);

            //added by yanhui 20120319
            frmImportDBMerge.TopLevel = false;
            frmImportDBMerge.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmImportDBMerge);

            //数据校验界面初始化
            frmDataVer.TopLevel = false;
            frmDataVer.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmDataVer);

            frmQuery.TopLevel = false;
            frmQuery.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmQuery);
            this.frmQuery.ShowInfoEnterPageEvent += frmQuery_OnShowInfoEnterPage;//处理查询页面引发的转入信息录入页面事件

            frmResult.TopLevel = false;
            frmResult.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmResult);

            frmSuggest.TopLevel = false;
            frmSuggest.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmSuggest);

            //添加打印窗体，Revised by XY，20081221
            frmPrint.TopLevel = false;
            frmPrint.Dock = DockStyle.Fill;
            this.pnlMainForm.Controls.Add(frmPrint);

            //add by lch 090605 设置用户导入窗体
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
            this.frmInfo.ShowResultPageEvent += frmInfo_OnShowResultPage; //处理信息录入页面引发的转入诊断结论页面事件
            this.frmInfo.PatBasicInfoChanged += frmInfo_OnPatBasicInfoChanged;//处理信息录入页面转发的BasicInfo页病人基本信息已更改事件
            //2009-8-28：ccj
            //处理信息录入页面引发的保存显示变化事件
            this.frmInfo.ShowSaveEvent += frmInfo_OnShowSave;
            this.frmInfo.ShowChangeEvent += OnShowChange;

            //2009-9-3:ccj
            //处理建议页面引发的保存显示变化事件
            this.frmSuggest.DataChangedEvent += OnShowChange;
            //2009-9-4:ccj
            //处理诊断页面引发的保存显示变化事件
            this.frmResult.DataChangedEvent += OnShowChange;
            //修改结束
            ShowWelcomePage();
        }

        /******************************************************         
                *Author：杨艳
                *Create Date：2008-12-22 
                *Function：关闭窗体前进行提醒，经用户确定后执行。
                * 
                *Revise History：
                ******************************************************/

        private void MainForm_FormClosing(Object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否退出保健医生临床决策支持软件？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //主窗体关闭，释放Excel占用的资源
            frmImport.ExcelControl.CloseExcelApplication();
            frmStatistic.RecordTreeNodeState();
            frmDataVer.RecordTreeNodeState();
            frmImport.RecordTreeNodeState();
            if (dr != DialogResult.Yes)
            {
                e.Cancel = true;                
            }

            /********************************************************************************
            *作用：用户确定退出程序，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "退出";
            // CDSSOperationLog.OperationDescription = GlobalData.UserInfo.CurrentAppVer;//软件版本信息
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            //将所有日志保存至数据库
            //DBAccess.SaveAllOperationLog();
            UserLogOperate.SaveLogData();
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {

            if (btnQuery.Checked)
                return;
            /********************************************************************************
            *作用：用户点击主界面【查询】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "查询";
            CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            SwitchButton(btnQuery);
            //add by lch 090617 情况查询界面的查询结果，
            //修复Bug：用户保存数据之后，回到查询界面，不点击重新查询，直接点击已有数据，会出错。
            frmQuery.lvPatHistory.Items.Clear();
            frmQuery.PatBasicClear();
            ShowPage(frmQuery); 
            this.Cursor = Cursors.Default;
        }
        /**********************************************************************************************************
        *  添加人：XY；
        *  添加时间：20090326；
        *  添加说明：界面美化，根据是否选中、是否可用的不同状态导入不同图片；
        *  添加部分：查询、新入、信息录入、诊断结论、饮食运动建议、打印、保存案例、清空；最小化、退出按钮。
        **********************************************************************************************************/
        private void btnQuery_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnQuery.Image = CDSS.Properties.Resources.查询_press;
        }

        private void btnQuery_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.ClientRectangle.Contains(e.Location))
                this.btnQuery.Image = CDSS.Properties.Resources.查询_over;
            else
                this.btnQuery.Image = CDSS.Properties.Resources.查询_normal;
        }

        private void btnQuery_MouseEnter(object sender, EventArgs e)
        {
            this.btnQuery.Image = CDSS.Properties.Resources.查询_over;
        }
        private void btnQuery_MouseLeave(object sender, EventArgs e)
        {
            this.btnQuery.Image = CDSS.Properties.Resources.查询_normal;
        }

        /// <summary>
        /// 新入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            Basicinfo frmNew = new Basicinfo();
            if (frmNew.ShowDialog(this) != DialogResult.OK)
                return;

            //PatInfo.patientid = 0;

            ShowInfoPage(); //切换到信息录入页面
            SetDisablePhoto();
            //this.btnNew.Image = CDSS.Properties.Resources.新入_disable;
        }
        private void btnNew_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnNew.Image = CDSS.Properties.Resources.新入_press;
        }

        private void btnNew_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnNew.Image = CDSS.Properties.Resources.新入_over;
            //else
            this.btnNew.Image = CDSS.Properties.Resources.新入_normal;
        }

        private void btnNew_MouseEnter(object sender, EventArgs e)
        {
            this.btnNew.Image = CDSS.Properties.Resources.新入_over;
        }

        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            this.btnNew.Image = CDSS.Properties.Resources.新入_normal;
        }

        /// <summary>
        /// 信息录入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfoInput_Click(object sender, EventArgs e)
        {
            if (btnInfoEnter.Checked)
                return;

            /********************************************************************************
            *作用：用户点击主界面【信息录入】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "信息录入";
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            SwitchButton(btnInfoEnter);
            ShowPage(frmInfo);
            this.Cursor = Cursors.Default;
        }
        private void btnInfoInput_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnInfoEnter.Image = CDSS.Properties.Resources.信息录入_press;
        }

        private void btnInfoInput_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnInfoEnter.Image = CDSS.Properties.Resources.信息录入_over;
            //else
            this.btnInfoEnter.Image = CDSS.Properties.Resources.信息录入_normal;
        }

        private void btnInfoInput_MouseEnter(object sender, EventArgs e)
        {
            this.btnInfoEnter.Image = CDSS.Properties.Resources.信息录入_over;
        }

        private void btnInfoInput_MouseLeave(object sender, EventArgs e)
        {
            this.btnInfoEnter.Image = CDSS.Properties.Resources.信息录入_normal;
            SetDisablePhoto();
        }
        /**************************************************************************
         * 修改人：XY；
         * 修改时间：20081221；
         * 修改说明：数据必填项是否输入完整判断及提示功能；
         * 修改部分：在“诊断结论按钮”、“治疗建议按钮”、“保存案例按钮”和
                    “清空页面按钮”中添加NonError函数判断语句。
         * 备注：暂时不提供返回到未填项页面或该文本框。
         ***************************************************************************/
        /// <summary>
        /// 诊断结论按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResult_Click(object sender, EventArgs e)
        {
            if (btnResult.Checked)
                return;

            //add by lch 090617 在点击诊断结论按钮的时候就保存数据，以免用户不点击保存案例按钮，丢失了数据
            this.frmInfo.LoadDataFromUIToVar();    //保存信息录入页面数据
            CDSSDBAccess.DBAccess.SaveDataToDB();

            // Added temporaly
            this.btnSave.Text = string.Empty;

            /********************************************************************************
            *作用：用户点击主界面【诊断结论】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "诊断结论";
            CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            this.frmInfo.LoadDataFromUIToVar();
            //Console.WriteLine(GlobalData.PatBasicInfo.PatName);
            //Console.ReadLine();
            if (frmInfo.ShowNonErrMsg())
            {
                MessageBox.Show(frmInfo.ErrorMsg + "请补充！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }
            SwitchButton(btnResult);
            ShowPage(frmResult);
            this.btnSuggest.Enabled = true; //有诊断结论之后，使治疗建议按钮亮显
            SetDisablePhoto();
            this.Cursor = Cursors.Default;
        }

        private void btnResult_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnResult.Image = CDSS.Properties.Resources.诊断结论_press;
        }

        private void btnResult_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnResult.Image = CDSS.Properties.Resources.诊断结论_over;
            //else
            this.btnResult.Image = CDSS.Properties.Resources.诊断结论_normal;
        }

        private void btnResult_MouseEnter(object sender, EventArgs e)
        {
            this.btnResult.Image = CDSS.Properties.Resources.诊断结论_over;
        }
        private void btnResult_MouseLeave(object sender, EventArgs e)
        {
            this.btnResult.Image = CDSS.Properties.Resources.诊断结论_normal;
            SetDisablePhoto();
        }
        /// <summary>
        /// 饮食运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuggest_Click(object sender, EventArgs e)
        {
            if (btnSuggest.Checked)
                return;

            this.frmResult.LoadDataFromUIToVar();    //推理结果信息录入页面数据
            CDSSDBAccess.DBAccess.SaveDataToDB();
            // Added temporaly
            this.btnSave.Text = string.Empty;

            /********************************************************************************
            *作用：用户点击主界面【饮食运动建议】按钮，记录该操作日志
             *add by lch 090615
             *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "饮食运动建议";
            CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            if (frmInfo.ShowNonErrMsg())
            {
                MessageBox.Show(frmInfo.ErrorMsg + "请补充！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }
            this.btnPrint.Enabled = true;//限制打印按钮只有在治疗建议给出后才可用，Revised by XY，20081222
            SwitchButton(btnSuggest);
            ShowPage(frmSuggest);
            SetDisablePhoto();
            this.Cursor = Cursors.Default;
        }

        private void btnSuggest_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSuggest.Image = CDSS.Properties.Resources.饮食运动建议_press;
        }

        private void btnSuggest_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnSuggest.Image = CDSS.Properties.Resources.饮食运动建议_over;
            //else
            this.btnSuggest.Image = CDSS.Properties.Resources.饮食运动建议_normal;
        }

        private void btnSuggest_MouseEnter(object sender, EventArgs e)
        {
            this.btnSuggest.Image = CDSS.Properties.Resources.饮食运动建议_over;
        }

        private void btnSuggest_MouseLeave(object sender, EventArgs e)
        {
            this.btnSuggest.Image = CDSS.Properties.Resources.饮食运动建议_normal;
            SetDisablePhoto();
        }

        /// <summary>
        /// 保存案例按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            /********************************************************************************
            *作用：用户点击主界面【保存案例】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "保存案例";
            CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            this.frmInfo.LoadDataFromUIToVar();    //保存信息录入页面数据
            this.frmResult.LoadDataFromUIToVar();  //保存诊断结论页面数据
            this.frmSuggest.LoadDataFromUIToVar(); //保存治疗建议页面数据

            if (CDSSDBAccess.DBAccess.SaveDataToDB())
            {
                MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //2009-8-28:ccj
                //保存按键按下按键显示保存状态
                this.btnSave.Text = "";
                //修改结束

                //2009-9-4：ccj
                //保存后清除建议页面的更改标志
                this.frmSuggest.SuggestionDataModified = false;
                //修改结束
            }
            else
            {
                //this.frmProgress.HideProgressWindow();
                string ErrorInfo = "保存病案记录时出现错误。\n详细信息：";
                ErrorInfo += CDSSDBAccess.DBAccess.LastErrorInfo;
                MessageBox.Show(ErrorInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CDSSDBAccess.DBAccess.ReportCurrentProgress -= new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
            }
            //CDSSDBAccess.DBAccess.ReportCurrentProgress -= new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
            //this.frmProgress.HideProgressWindow();

            this.Cursor = Cursors.Default;
        }
        private void btnSave_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSave.Image = CDSS.Properties.Resources.保存案例_press;
        }

        private void btnSave_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnSave.Image = CDSS.Properties.Resources.保存案例_over;
            //else
            this.btnSave.Image = CDSS.Properties.Resources.保存案例_normal;
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            this.btnSave.Image = CDSS.Properties.Resources.保存案例_over;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            this.btnSave.Image = CDSS.Properties.Resources.保存案例_normal;

        }
        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.frmSuggest.LoadDataFromUIToVar();    //推理结果信息录入页面数据
            CDSSDBAccess.DBAccess.SaveDataToDB();
            // Added temporaly
            this.btnSave.Text = string.Empty;

            /********************************************************************************
            *作用：用户点击主界面【打印】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "打印";
            CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            ShowPage(frmPrint);
            frmPrint.ShowPrtReport();//调用显示打印报表函数，Revised by XY，20081222.
            //进入打印页面此按钮不可用，要再治疗建议给出后再次可用，Revised by XY，20081222.
            this.btnSuggest.Checked = false;//取消治疗建议选中按钮，Revised by XY，20081222.
            this.btnPrint.Enabled = false;
            SetDisablePhoto();
            this.Cursor = Cursors.Default;
        }
        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnPrint.Image = CDSS.Properties.Resources.打印_press;
        }

        private void btnPrint_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.ClientRectangle.Contains(e.Location))
                this.btnPrint.Image = CDSS.Properties.Resources.打印_over;
            else
                this.btnPrint.Image = CDSS.Properties.Resources.打印_normal;
        }

        private void btnPrint_MouseEnter(object sender, EventArgs e)
        {
            this.btnPrint.Image = CDSS.Properties.Resources.打印_over;
        }

        private void btnPrint_MouseLeave(object sender, EventArgs e)
        {
            this.btnPrint.Image = CDSS.Properties.Resources.打印_normal;
            SetDisablePhoto();
        }
        /// <summary>
        /// 清空页面按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResume_Click(object sender, EventArgs e)
        {
            /********************************************************************************
            *作用：用户点击主界面【清空页面】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "清空页面";
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;

            if (frmInfo.ShowNonErrMsg())
            {
                if (MessageBox.Show(frmInfo.ErrorMsg + "是否退出本次录入？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //add by wbf 081226 清空数据提示是否保存
                    if (frmInfo.DataModifiedCheck())
                    {
                        DialogResult dr = MessageBox.Show("数据未全部保存，是否保存？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            frmInfo.LoadDataFromUIToVar();
                            frmResult.LoadDataFromUIToVar();
                            frmSuggest.LoadDataFromUIToVar();

                            /*2008-9-4：ccj*/
                            //修改描述：页面转换自动保存数据

                            CDSSDBAccess.DBAccess.SaveDataToDB();

                            //修改结束
                        }
                    }

                    this.frmInfo.ClearData();   //清空信息录入页面
                    this.frmResult.ClearData(); //清空诊断结论页面
                    this.frmSuggest.ClearData();//清空治疗建议页面
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
                //add by wbf 081226 清空数据提示是否保存
                if (frmInfo.DataModifiedCheck())
                {
                    DialogResult dr = MessageBox.Show("数据未全部保存，是否保存？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        frmInfo.LoadDataFromUIToVar();
                        frmResult.LoadDataFromUIToVar();
                        frmSuggest.LoadDataFromUIToVar();

                        /*2008-8-27：ccj*/
                        //修改描述：页面转换自动保存数据

                        CDSSDBAccess.DBAccess.SaveDataToDB();

                        //修改结束
                    }
                }
            }

            this.frmInfo.ClearData();   //清空信息录入页面
            this.frmResult.ClearData(); //清空诊断结论页面
            this.frmSuggest.ClearData();//清空治疗建议页面
            ShowWelcomePage();
            this.Cursor = Cursors.Default;


            //2009-8-28:ccj
            //确定清空后，保存按键体现状态
            this.btnSave.Text = "";
            //修改结束

            //清空全局数据
            GlobalData.Clear();

        }

        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (btnSetting.Checked)
                return;

            /********************************************************************************
            *作用：用户点击主界面【设置】按钮，记录该操作日志            
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "设置";
            CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Cursor = Cursors.WaitCursor;
            SwitchButton(btnSetting);
            ShowPage(frmConfigFileEditor);
            this.Cursor = Cursors.Default;
        }    

       
        private void btnResume_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnResume.Image = CDSS.Properties.Resources.清空页面_press;
        }

        private void btnResume_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.ClientRectangle.Contains(e.Location))
                this.btnResume.Image = CDSS.Properties.Resources.清空页面_over;
            else
                this.btnResume.Image = CDSS.Properties.Resources.清空页面_normal;
        }

        private void btnResume_MouseEnter(object sender, EventArgs e)
        {
            this.btnResume.Image = CDSS.Properties.Resources.清空页面_over;
        }

        private void btnResume_MouseLeave(object sender, EventArgs e)
        {
            this.btnResume.Image = CDSS.Properties.Resources.清空页面_normal;
            SetDisablePhoto();
        }

        #endregion

        #region 响应用户事件
        /// <summary>
        /// 响应信息录入页面的GotoResultPageEvent事件,切换到诊断结论页面
        /// </summary>
        private void frmInfo_OnShowResultPage()
        {
            EventArgs e = new EventArgs();
            btnResult_Click(btnResult, e);
        }

        //2009-8-28:ccj
        //功能描述：在保存图标上显示保存状态的变化
        private void frmInfo_OnShowSave()
        {
            btnSave.Text = "";
        }



        //2009-9-4：ccj
        //新添显示建议页面内容变化
        private void OnShowChange()
        {
            this.btnSave.Text = "*";
        }

        //修改结束


        /// <summary>
        /// 响应查询页面的GotoInfoEnterPageEvent事件,切换到信息录入页面
        /// </summary>
        private void frmQuery_OnShowInfoEnterPage()
        {
            this.Cursor = Cursors.WaitCursor;
            //设置病人来源为数据库(非新入)
            PatInfo.bNewPatient = false;
            //CDSSDBAccess.DBAccess.ReportCurrentProgress += new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
            //frmProgress.ShowProgressWindow(this);
            if (!CDSSDBAccess.DBAccess.GetDataFromDB())
            {
                //frmProgress.HideProgressWindow();
                string ErrorInfo = "查询该条病案记录时出现错误。\n详细信息：";
                ErrorInfo += CDSSDBAccess.DBAccess.LastErrorInfo;
                MessageBox.Show(ErrorInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CDSSDBAccess.DBAccess.ReportCurrentProgress -= new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
                return;
            }

            //Add BY ZX 20100505 读入病人的疾病史、个人史、家族疾病史等每次基本不变的病人数据
            DateTime vistTime = GlobalData.PatBasicInfo.PatVisitDateTime;//就诊时间是每次就诊都不同的，所以用个中间变量存放好，带导入数据后再重新赋值
            string sqlstr = "select  RecordSEQ from CDSS_RecordHistory  where PatSEQ ="
                    + " (select TOP 1 PatSEQ from CDSS_RecordHistory where RecordSEQ=" + GlobalData.RecordInfo.RecordSeq
                    + ") order by RecordSEQ desc  "; ///‘’‘’‘’ FGJ修改
            string recordseq = DBAccess.GetStringScalar(sqlstr);
            if (recordseq != "")
            {
                DBAccess.GetOnlyDataFromDB(Convert.ToInt16(recordseq));
                GlobalData.PatBasicInfo.PatVisitDateTime = vistTime;
            }
            //CDSSDBAccess.DBAccess.ReportCurrentProgress -= new CDSSDBAccess.ReportCurrentProgressEventHandler(DBAccess_ReportCurrentProgress);
            //frmProgress.HideProgressWindow();
            //显示信息录入页面
            ShowInfoPage();
            this.Cursor = Cursors.Default;
        }


        /// <summary>
        ///响应信息录入页面转发的BasicInfo页的PatBasicInfoChanged事件，更新病人基本信息显示栏
        /// </summary>
        private void frmInfo_OnPatBasicInfoChanged()
        {
            SetPatInfo();
        }

        /// <summary>
        /// 响应数据库访问接口的处理进度
        /// </summary>
        /// <param name="Milestone"></param>
        /// <param name="DetailInfo"></param>
        private void DBAccess_ReportCurrentProgress(int Milestone, string DetailInfo)
        {
            this.frmProgress.SetDetailInfo(DetailInfo);
            this.frmProgress.SetMilestone(Milestone);
        }

        /**************************************************************
         * 添加人：XY；
         * 添加时间：20081223；
         * 添加说明：可以使用快捷键Ctrl+P，弹出打印对话框；
         * 添加部分：打印快捷键，调用打印对话框。
         **************************************************************/
        /// <summary>
        /// 打印快捷键，调用打印对话框
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

        #region 功能函数
        /// <summary>
        /// 显示指定页面
        /// </summary>
        /// <param name="frm">指定要显示的页面</param>
        private void ShowPage(Form frm)
        {
            if (frmCurrent != null)
                frmCurrent.Hide();
            //2009-8-28:ccj
            //确定清空后，保存按键体现状态
            //this.btnSave.Text = "";
            //修改结束
            frmCurrent = frm;
            frmCurrent.Show();
            frmCurrent.Focus();
        }



        /// <summary>
        /// 切换按钮状态
        /// </summary>
        /// <param name="btn">指定当前处于Checked状态的按钮</param>
        private void SwitchButton(CheckBox btn)
        {
            if (btnCurrent != null)
                btnCurrent.Checked = false;
            btnCurrent = btn;
            btnCurrent.Checked = true;
        }



        /// <summary>
        /// 将页面恢复到最初显示状态
        /// </summary>
        private void ShowWelcomePage()
        {
            //jyl 081211
            DataSet dsresult = new DataSet();
            int count = 0;
            //恢复按钮初始状态
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

            //隐藏病人信息栏
            this.pnlPatInfo.Hide();
            //显示欢迎页面
            ShowPage(frmWelcom);
            SetDisablePhoto();
        }


        /// <summary>
        /// 设置当前病人，更新页面及按钮状态
        /// </summary>
        private void ShowInfoPage()
        {
            //设置按钮状态
            this.btnQuery.Enabled = false;
            this.btnNew.Enabled = false;
            this.btnInfoEnter.Enabled = true;
            this.btnResult.Enabled = true;
            //if (PatInfo.bNewPatient)    //对于新入病人，该按钮需要得出诊断结论之后才能有效
            this.btnSuggest.Enabled = false;
            //else
            //    this.btnSuggest.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnPrint.Enabled = false;//限制打印按钮只有在治疗建议给出后才可用，Revised by XY，20081222
            this.btnResume.Enabled = true;
            this.btnImport.Enabled = false;//批量数据导入会覆盖全局变量，查看具体病人时限制导入功能，Revised by ZX，20100701
            //显示病人信息页面并加载病人信息
            this.pnlPatInfo.Show();
            SetPatInfo();

            //显示信息录入页面
            //add by 杨艳，2008-12-23 显示前需先加载患者数据
            ShowPage(frmInfo);
            SwitchButton(btnInfoEnter);
            this.frmInfo.InitInfoEnterPage();
            SetDisablePhoto();
        }


        /// <summary>
        /// 更新病人信息栏信息
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
        /// 导入按钮不可用状态时图标 Add by XY 20090325
        /// </summary>
        private void SetDisablePhoto()
        {
            if (this.btnQuery.Enabled == false)
            {
                this.btnQuery.Image = CDSS.Properties.Resources.查询_disable;
            }
            else
            {
                this.btnQuery.Image = CDSS.Properties.Resources.查询_normal;
            }
            if (this.btnNew.Enabled == false)
            {
                this.btnNew.Image = CDSS.Properties.Resources.新入_disable;
            }
            else
            {
                this.btnNew.Image = CDSS.Properties.Resources.新入_normal;
            }
            if (this.btnInfoEnter.Enabled == false)
            {
                this.btnInfoEnter.Image = CDSS.Properties.Resources.信息录入_disable;
            }
            else
            {
                this.btnInfoEnter.Image = CDSS.Properties.Resources.信息录入_normal;
            }
            if (this.btnResult.Enabled == false)
            {
                this.btnResult.Image = CDSS.Properties.Resources.诊断结论_disable;
            }
            else
            {
                this.btnResult.Image = CDSS.Properties.Resources.诊断结论_normal;
            }
            if (this.btnSuggest.Enabled == false)
            {
                this.btnSuggest.Image = CDSS.Properties.Resources.饮食运动建议_disable;
            }
            else
            {
                this.btnSuggest.Image = CDSS.Properties.Resources.饮食运动建议_normal;
            }
            if (this.btnPrint.Enabled == false)
            {
                this.btnPrint.Image = CDSS.Properties.Resources.打印_disable;
            }
            else
            {
                this.btnPrint.Image = CDSS.Properties.Resources.打印_normal;
            }
            if (this.btnSave.Enabled == false)
            {
                this.btnSave.Image = CDSS.Properties.Resources.保存案例_disable;
            }
            else
            {
                this.btnSave.Image = CDSS.Properties.Resources.保存案例_normal;
            }
            if (this.btnResume.Enabled == false)
            {
                this.btnResume.Image = CDSS.Properties.Resources.清空页面_disable;
            }
            else
            {
                this.btnResume.Image = CDSS.Properties.Resources.清空页面_normal;
            }
        }
        #endregion

        /// <summary>
        /// //add by lch 090605 【用户意见】按钮click事件，显示用户导入窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            //if (btnImport.Checked)
            //    return;

            /********************************************************************************
            *作用：用户点击主界面【数据导入】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "数据导入";
            CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
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

        //此处修改导入按钮图标显示情况，不让其显示相应的变化保留相应事件为以后扩展用
        private void btnImport_MouseDown(object sender, MouseEventArgs e)
        {
            //this.btnImport.Image = CDSS.Properties.Resources.导入_press;
        }

        private void btnImport_MouseEnter(object sender, EventArgs e)
        {
            //this.btnImport.Image = CDSS.Properties.Resources.导入_over;
        }

        private void btnImport_MouseLeave(object sender, EventArgs e)
        {
            //this.btnImport.Image = CDSS.Properties.Resources.导入_normal;
            //SetDisablePhoto();

        }

        private void btnImport_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.ClientRectangle.Contains(e.Location))
            //    this.btnImport.Image = CDSS.Properties.Resources.导入_over;
            //else
            //    this.btnImport.Image = CDSS.Properties.Resources.导入_normal;
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