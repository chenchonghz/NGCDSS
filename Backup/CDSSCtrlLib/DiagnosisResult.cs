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

            //Add By ZX 2010-3-25 ����Ͻ����ϼӸ����Ա༭�ĸ����ؼ�
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

        //revised by lch 090406 �޸�BugDB00005764�����ؼ���չ���߶ȼӴ�
        private const int MAX_HEIGHT = 187; //�ؼ�չ��ʱ�Ĵ���߶�
        private const int MIN_HEIGHT = 32;  //�ؼ�����ʱ�Ĵ���߶�

        public event ResultChangeHandler ResultChange;

        public string DiseaseName;
        public string Result;
        public string TreatmentTarget;
        public string TreatmentSuggestion;
        public string SelfCheck;
        public string DataNeeded;
        public string DiagnosisSteps;

        //Add by ZX 2010-3-25 �����������б���������������ĵó�����

        /// <summary>
        /// ����� ���Ҽ�����
        /// </summary>
        public string ReasonSelfCheck;
        /// <summary>
        /// ����� ����Ŀ��
        /// </summary>
        public string ReasonTreatmentTarget;

        /// <summary>
        /// ����� ���ƽ���
        /// </summary>
        public string ReasonTreatmentSuggestion;

        #region ϵͳ�¼�

        private void DiagnosisResult_Load(object sender, EventArgs e)
        {
            this.AutoScroll = false;    //��Դ�༭�������е�Ĭ��ֵ��Ч��������MS��Bug����ֻ�������������ʾ���á�
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
            {//�����ǰ������״̬,�����л���չ��״̬
                this.Height = MAX_HEIGHT;
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Expand_normal;
            }
            else
            {//�����ǰ��չ��״̬,�����л�������״̬
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

        #region �û��¼�
        /// <summary>
        /// �¼�����,����֪ͨ�ϲ���ʾ��Ϲ��̴���
        /// �������Ϲ��̵���ʾ���ܿ��ܻ���Ӹ��ӣ�������������ͼ��·����ʾ��
        /// ���Խ��ù��ܶ�������������ר�ŵ�ģ��������
        /// </summary>
        public event ShowDiagnosisStepsEventHandler ShowDiagnosisSteps;
        private void RaiseShowDiagnosisStepsEvent(string DiagnosisSteps)
        {
            ShowDiagnosisStepsEventHandler temp = ShowDiagnosisSteps;
            if (temp != null)
                temp(DiagnosisSteps);
        }

        #endregion

        #region ���ܺ���
        /// <summary>
        /// ������ݣ��ָ���ʼ״̬
        /// </summary>
        public void ClearData()
        {
            //����ֶ�
            this.DiseaseName = string.Empty;
            this.Result = string.Empty;
            this.TreatmentTarget = string.Empty;
            this.TreatmentSuggestion = string.Empty;
            this.SelfCheck = string.Empty;
            this.DataNeeded = string.Empty;
            this.DiagnosisSteps = string.Empty;

            //Add by ZX 2010-3-25 ��������ó��Ľ������
            this.ReasonSelfCheck = string.Empty;
            this.ReasonTreatmentSuggestion = string.Empty;
            this.ReasonTreatmentTarget = string.Empty;

            //�ָ�����������״̬
            this.Height = MIN_HEIGHT;
            //�ָ���ť״̬
            this.lblShowDetail.Enabled = false;
            this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Shrink_disable;
            //��տؼ��ı�
            this.lblName.Text = string.Empty;
            this.lblResult.Text = string.Empty;
            this.rtxTreatmentTarget.Text = string.Empty;
            this.rtxTreatmentSuggestion.Text = string.Empty;
            this.rtxRemark.Text = string.Empty;
        }

        /// <summary>
        /// ���½�����ʾ
        /// </summary>
        public void ShowDiagnosisResult()
        {
            //�����ؼ���ֵ
            this.lblName.Text = this.DiseaseName;
            this.lblShowDetail.Enabled = true;
            if (this.Result.Contains("����"))
            {
                this.lblShowDetail.Image = CDSSCtrlLib.Properties.Resources.Shrink_normal;
                this.Height = MIN_HEIGHT;
                if (this.DataNeeded == "")
                {//����
                    this.lblName.ForeColor = Color.Green;
                    this.lblResult.ForeColor = Color.Green;
                    this.lblResult.Text = "����";
                }
                else
                {//��������Ҫ��������
                    this.lblName.ForeColor = Color.Black;
                    this.lblResult.ForeColor = Color.Black;
                    this.lblResult.Text = "����  ����Ҫ�������ݣ�" + this.DataNeeded + ")";
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
                    this.lblResult.Text = this.Result + "  ����Ҫ�������ݣ�" + this.DataNeeded + ")";
            }

            //BugDB00005699 revised by wbf 2009-03-25
            this.rtxTreatmentTarget.Text = this.TreatmentTarget;
            this.rtxTreatmentSuggestion.Text = this.TreatmentSuggestion;
            this.rtxRemark.Text = this.SelfCheck;
        }
        #endregion
    }
}
