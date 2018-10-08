using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CDSSCtrlLib
{
    public delegate void ShowDiagnosisStepsEventHandler(string DiagnosisSteps);
    public delegate void ResultChangeHandler(object sender, DiagnosisResult.ResultChangeArgs e);

    public partial class DiagnosisResult : UserControl
    {
        public class ResultChangeArgs : EventArgs
        {
            private string[] _var;
            public ResultChangeArgs(string[] m)
            {
                _var = m;
            }
            public string[] Content
            {
                get
                {
                    return _var;
                }
            }
        }

        public DiagnosisResult()
        {
            InitializeComponent();

            //Add By ZX 2010-3-25 再诊断界面上加个可以编辑的浮动控件
            rtxTreatmentTarget.Click += delegate(object sender, EventArgs e)
           {
               EditRichTextBox editTxt_pop = new EditRichTextBox(rtxTreatmentTarget);
               editTxt_pop.ReasonResult = ReasonTreatmentTarget;
               editTxt_pop.Closing += delegate(object sender1, CDSSCtrlLib.EditRichTextBox.ContentArgs e1)
               {
                   rtxTreatmentTarget.Text = e1.Content;
                   OnResultChange();
               };
               editTxt_pop.Show(label5);
           };
            rtxTreatmentSuggestion.Click += delegate(object sender, EventArgs e)
            {
                EditRichTextBox editTxt_pop = new EditRichTextBox(rtxTreatmentSuggestion);
                editTxt_pop.ReasonResult = ReasonTreatmentSuggestion;
                editTxt_pop.Closing += delegate(object sender1, EditRichTextBox.ContentArgs e1)
                {
                    rtxTreatmentSuggestion.Text = e1.Content;
                    OnResultChange();
                };
                editTxt_pop.Show(label7);
            };
            rtxRemark.Click += delegate(object sender, EventArgs e)
            {
                EditRichTextBox editTxt_pop = new EditRichTextBox(rtxRemark);
                editTxt_pop.ReasonResult = ReasonSelfCheck;
                editTxt_pop.Closing += delegate(object sender1, EditRichTextBox.ContentArgs e1)
                {
                    rtxRemark.Text = e1.Content;
                    OnResultChange();
                };
                editTxt_pop.Show(label9);
            };
        }

        protected virtual void OnResultChange()
        {
            string[] val = new string[3] { rtxTreatmentTarget.Text, rtxTreatmentSuggestion.Text, rtxRemark.Text };
            ResultChangeArgs e = new ResultChangeArgs(val);
            if (ResultChange != null)
                ResultChange(this, e);
        }

        //revised by lch 090406 修复BugDB00005764，将控件的展开高度加大
        private const int MAX_HEIGHT = 187; //控件展开时的窗体高度
        private const int MIN_HEIGHT = 32;  //控件收缩时的窗体高度

        public event ResultChangeHandler ResultChange;

        public string DiseaseName;
        public string Result;
        public string TreatmentTarget;
        public string TreatmentSuggestion;
        public string SelfCheck;
        public string DataNeeded;
        public string DiagnosisSteps;

        //Add by ZX 2010-3-25 增加三个公有变量，接受推理机的得出结论

        /// <summary>
        /// 推理机 自我监测结论
        /// </summary>
        public string ReasonSelfCheck;
        /// <summary>
        /// 推理机 治疗目标
        /// </summary>
        public string ReasonTreatmentTarget;

        /// <summary>
        /// 推理机 治疗建议
        /// </summary>
        public string ReasonTreatmentSuggestion;

        #region 系统事件

        private void DiagnosisResult_Load(object sender, EventArgs e)
        {
            this.AutoScroll = false;    //资源编辑器属性中的默认值无效（可能是MS的Bug），只能在这里进行显示设置。
            ClearData();
        }


        private void lblShowDetail_MouseEnter(object sender, EventArgs e)
        {
            if (this.Height == MAX_HEIGHT)
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Expand_over;
            else
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Shrink_over;
        }

        private void lblShowDetail_MouseLeave(object sender, EventArgs e)
        {
            if (this.Height == MAX_HEIGHT)
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Expand_normal;
            else
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Shrink_normal;
        }


        private void lblShowDiagnosisSteps_MouseEnter(object sender, EventArgs e)
        {
            this.lblShowDiagnosisSteps.Image = CDSSCtrlLib.Properties.Resources.DiagnosisSteps_over;
        }

        private void lblShowDiagnosisSteps_MouseLeave(object sender, EventArgs e)
        {
            this.lblShowDiagnosisSteps.Image = CDSSCtrlLib.Properties.Resources.DiagnosisSteps_normal;
        }

        private void lblShowDetail_Click(object sender, EventArgs e)
        {
            if (this.Height == MIN_HEIGHT)
            {//如果当前是收缩状态,将其切换到展开状态
                this.Height = MAX_HEIGHT;
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Expand_normal;
            }
            else
            {//如果当前是展开状态,将其切换到收缩状态
                this.Height = MIN_HEIGHT;
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Shrink_normal;
            }

        }

        private void lblShowDiagnosisSteps_Click(object sender, EventArgs e)
        {
            this.lblShowDiagnosisSteps.Image = CDSSCtrlLib.Properties.Resources.DiagnosisSteps_press;
            RaiseShowDiagnosisStepsEvent(this.DiagnosisSteps);
            this.lblShowDiagnosisSteps.Image = CDSSCtrlLib.Properties.Resources.DiagnosisSteps_normal;
        }

        #endregion

        #region 用户事件
        /// <summary>
        /// 事件定义,用于通知上层显示诊断过程窗体
        /// 因这后诊断过程的显示功能可能会更加复杂，例如增加流程图中路径显示，
        /// 所以将该功能独立出来，交给专门的模块来处理
        /// </summary>
        public event ShowDiagnosisStepsEventHandler ShowDiagnosisSteps;
        private void RaiseShowDiagnosisStepsEvent(string DiagnosisSteps)
        {
            ShowDiagnosisStepsEventHandler temp = ShowDiagnosisSteps;
            if (temp != null)
                temp(DiagnosisSteps);
        }

        #endregion

        #region 功能函数
        /// <summary>
        /// 清空数据，恢复初始状态
        /// </summary>
        public void ClearData()
        {
            //清空字段
            this.DiseaseName = string.Empty;
            this.Result = string.Empty;
            this.TreatmentTarget = string.Empty;
            this.TreatmentSuggestion = string.Empty;
            this.SelfCheck = string.Empty;
            this.DataNeeded = string.Empty;
            this.DiagnosisSteps = string.Empty;

            //Add by ZX 2010-3-25 将推理机得出的结论清空
            this.ReasonSelfCheck = string.Empty;
            this.ReasonTreatmentSuggestion = string.Empty;
            this.ReasonTreatmentTarget = string.Empty;

            //恢复界面至收缩状态
            this.Height = MIN_HEIGHT;
            //恢复按钮状态
            this.lblShowDetail.Enabled = false;
            this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Shrink_disable;
            //清空控件文本
            this.lblName.Text = string.Empty;
            this.lblResult.Text = string.Empty;
            this.rtxTreatmentTarget.Text = string.Empty;
            this.rtxTreatmentSuggestion.Text = string.Empty;
            this.rtxRemark.Text = string.Empty;
        }

        /// <summary>
        /// 更新界面显示
        /// </summary>
        public void ShowDiagnosisResult()
        {
            //给各控件赋值
            this.lblName.Text = this.DiseaseName;
            this.lblShowDetail.Enabled = true;
            if (this.Result.Contains("正常"))
            {
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Shrink_normal;
                this.Height = MIN_HEIGHT;
                if (this.DataNeeded == "")
                {//正常
                    this.lblName.ForeColor = Color.Green;
                    this.lblResult.ForeColor = Color.Green;
                    this.lblResult.Text = "正常";
                }
                else
                {//正常但需要补充数据
                    this.lblName.ForeColor = Color.Black;
                    this.lblResult.ForeColor = Color.Black;
                    this.lblResult.Text = "正常  （需要补充数据：" + this.DataNeeded + ")";
                }
            }
            else
            {
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Expand_normal;
                this.Height = MAX_HEIGHT;
                this.lblName.ForeColor = Color.Red;
                this.lblResult.ForeColor = Color.Red;
                if (this.DataNeeded == "")
                    this.lblResult.Text = this.Result;
                else
                    this.lblResult.Text = this.Result + "  （需要补充数据：" + this.DataNeeded + ")";
            }

            //BugDB00005699 revised by wbf 2009-03-25
            this.rtxTreatmentTarget.Text = this.TreatmentTarget;
            this.rtxTreatmentSuggestion.Text = this.TreatmentSuggestion;
            this.rtxRemark.Text = this.SelfCheck;
        }
        #endregion
    }
}
