using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSSystemData;

namespace CDSS
{
    public partial class OtherExaminfo : InfoFormBaseClass
    {
        public bool bLoaded = false;    //标记数据是否已经加载过

        #region 系统事件
        public OtherExaminfo()
        {
            InitializeComponent();
        }

        private void rbt_EcgAbnormal_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_EcgAbnormal.Checked)
            {
                //panel_ECGAbnormal.Enabled = true;
                cmb_EcgResult.Text = "";
                cmb_EcgResult.Items.Clear();
                cmb_EcgResult.Items.Add("心律失常-頻发室上性早搏");
                cmb_EcgResult.Items.Add("心律失常-頻发室性早搏");
                cmb_EcgResult.Items.Add("心律失常-阵发性房颤");
                cmb_EcgResult.Items.Add("心律失常--慢性房颤");
                cmb_EcgResult.Items.Add("预激综合征");
                cmb_EcgResult.Items.Add("异常S-ST");
                cmb_EcgResult.Items.Add("急性前壁心肌梗死");
                cmb_EcgResult.Items.Add("急性侧壁心肌梗死");
                cmb_EcgResult.Items.Add("急性下壁心肌梗死");
                cmb_EcgResult.Items.Add("急性前间壁心肌梗死");
                cmb_EcgResult.Items.Add("急性高侧壁心肌梗死");
                cmb_EcgResult.Items.Add("陈旧性前壁心肌梗死");
                cmb_EcgResult.Items.Add("陈旧性侧壁心肌梗死");
                cmb_EcgResult.Items.Add("陈旧性下壁心肌梗死");
                cmb_EcgResult.Items.Add("陈旧性前间壁心肌梗死");
                cmb_EcgResult.Items.Add("陈旧性高侧壁心肌梗死");
                cmb_EcgResult.Items.Add("其它");
            }
            else
            {
                //panel_ECGAbnormal.Enabled = false;
                cmb_EcgResult.Text = "";
                cmb_EcgResult.Items.Clear();
                cmb_EcgResult.Items.Add("窦性心律");
                cmb_EcgResult.Items.Add("窦性心律不齐");
                cmb_EcgResult.Items.Add("窦性心动过缓");
            }
        }

        private void rbt_VuAbnormal_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_VuAbnormal.Checked)
                panel_VUAbnormal.Enabled = true;
            else
                panel_VUAbnormal.Enabled = false;
        }

        private void rbt_OtherAbnormal_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_OtherAbnormal.Checked)
                panel_OtherAbnormal.Enabled = true;
            else
                panel_OtherAbnormal.Enabled = false;
        }

        private void cmb_EcgResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_EcgResult.Text == "其它")
                txb_EcgResult.Enabled = true;
            else
                txb_EcgResult.Enabled = false;
                txb_EcgResult.Text = String.Empty;
        }

        private void OtherExaminfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!PatInfo.bNewPatient && !bLoaded)
                    LoadDataFromVarToUI (); //在窗体显示出来的时候加载数据
            }
        }    
        #endregion

        #region 用户事件
        /// <summary>
        /// 当页面内容改变时引发该事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e"></param>
        protected void DataModified(object sender, EventArgs e)
        {
            //如果在这之前数据未保存过,则直接返回
            if (IsModified || (!PatInfo.bNewPatient && !bLoaded))
                return;

            //引发事件,通知父窗体更新保存按钮状态
            RaiseDataChangedEvent(this, e);
            IsModified = true;
        }

        #endregion

        #region 功能函数
        public override void LoadDataFromVarToUI()
        {
            //心电图
            if (GlobalData.OtherExamInfo.HasECGAbnormal)   //心电图正常,true
            {
                this.rbt_EcgNormal.Checked = true;
                this.cmb_EcgResult.Text = GlobalData.OtherExamInfo.ECGAbnormalType;
            }
            else                                          //心电图异常,false
            {
                this.rbt_EcgAbnormal.Checked = true;
                if (GlobalData.OtherExamInfo.ECGAbnormalType != "")
                {
                    string Type = GlobalData.OtherExamInfo.ECGAbnormalType;
                    if (Type.Contains("其它"))
                    {
                        if (Type.IndexOf(':') != -1)
                        {
                            if (Type.Substring(0, Type.IndexOf(':')).Equals("其它"))
                            {
                                this.cmb_EcgResult.Text = "其它";
                                this.txb_EcgResult.Text = Type.Substring(Type.IndexOf(':') + 1);
                            }
                        }
                        else
                        {
                            this.cmb_EcgResult.Text = "其它";
                            this.txb_EcgResult.Text = "";
                        }                  
                    }
                    else
                    {
                        this.cmb_EcgResult.Text = Type;
                    }
                }
                else
                {
                    this.cmb_EcgResult.Text = "";
                }
            }

            //血管超声
            if (GlobalData.OtherExamInfo.listVascularUltrasound.Count > 0)
            {
                this.rbt_VuAbnormal.Checked = true;

                for (int i = 0; i < GlobalData.OtherExamInfo.listVascularUltrasound.Count; i++)
                {
                    if (i == 0)
                    {
                        this.cmb_VuType1.Text = GlobalData.OtherExamInfo.listVascularUltrasound[0].VascularAbnormalType;
                        this.cmb_VuType1Site.Text = GlobalData.OtherExamInfo.listVascularUltrasound[0].VascularAbnormalPart;
                    }
                    else if (i == 1)
                    {
                        this.cmb_VuType2.Text = GlobalData.OtherExamInfo.listVascularUltrasound[1].VascularAbnormalType;
                        this.cmb_VuType2Site.Text = GlobalData.OtherExamInfo.listVascularUltrasound[1].VascularAbnormalPart;
                    }
                    else if (i == 2)
                    {
                        this.cmb_VuType3.Text = GlobalData.OtherExamInfo.listVascularUltrasound[2].VascularAbnormalType;
                        this.cmb_VuType3Site.Text = GlobalData.OtherExamInfo.listVascularUltrasound[2].VascularAbnormalPart;
                    }
                }
            }
            else
                rbt_VuNormal.Checked = true;

            //其它检查
            if (GlobalData.OtherExamInfo.listOtherExamAbnormal.Count > 0)
            {
                rbt_OtherAbnormal.Checked = true;

                for (int i = 0; i < GlobalData.OtherExamInfo.listOtherExamAbnormal.Count; i++)
                {
                    if (i == 0)
                    {
                        txb_Exam1.Text = GlobalData.OtherExamInfo.listOtherExamAbnormal[0].ExamItemName;
                        richtxb_Exam1Result.Text = GlobalData.OtherExamInfo.listOtherExamAbnormal[0].ExamResult;
                    }
                    else if (i == 1)
                    {
                        txb_Exam2.Text = GlobalData.OtherExamInfo.listOtherExamAbnormal[1].ExamItemName;
                        richtxb_Exam2Result.Text = GlobalData.OtherExamInfo.listOtherExamAbnormal[1].ExamResult;
                    }
                    else if (i == 2)
                    {
                        txb_Exam3.Text = GlobalData.OtherExamInfo.listOtherExamAbnormal[2].ExamItemName;
                        richtxb_Exam3Result.Text = GlobalData.OtherExamInfo.listOtherExamAbnormal[2].ExamResult;
                    }
                    else if (i == 3)
                    {
                        txb_Exam4.Text = GlobalData.OtherExamInfo.listOtherExamAbnormal[3].ExamItemName;
                        richtxb_Exam4Result.Text = GlobalData.OtherExamInfo.listOtherExamAbnormal[3].ExamResult;
                    }
                }
            }
            else
            {
                rbt_OtherNormal.Checked = true;
            }
            //设置数据已加载标志
            this.bLoaded = true;
        }

        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return;

            GlobalData.OtherExamInfo.Clear();

            //心电图
            if (this.rbt_EcgNormal.Checked)   //心电图正常
            {
                GlobalData.OtherExamInfo.HasECGAbnormal=true;
                GlobalData.OtherExamInfo.ECGAbnormalType =this.cmb_EcgResult.Text ;
            }
            else                                          //心电图异常
            {
                GlobalData.OtherExamInfo.HasECGAbnormal = false ;
                if (this.cmb_EcgResult.Text.Trim().Equals("其它"))
                {
                    GlobalData.OtherExamInfo.ECGAbnormalType = this.cmb_EcgResult.Text.Trim() + ':' + this.txb_EcgResult.Text;
                }
                else
                    GlobalData.OtherExamInfo.ECGAbnormalType = this.cmb_EcgResult.Text;
            }

            //血管超声
            if (this.rbt_VuAbnormal.Checked)
            {
                if (this.cmb_VuType1.Text.Trim() != "")
                {
                    CDSSVascularUltrasound VascularUltrasound1 = new CDSSVascularUltrasound();
                    VascularUltrasound1.VascularAbnormalType = this.cmb_VuType1.Text;
                    VascularUltrasound1.VascularAbnormalPart = this.cmb_VuType1Site.Text;
                    GlobalData.OtherExamInfo.listVascularUltrasound.Add(VascularUltrasound1);
                }
                if (this.cmb_VuType2.Text.Trim() != "")
                {
                    CDSSVascularUltrasound VascularUltrasound2 = new CDSSVascularUltrasound();
                    VascularUltrasound2.VascularAbnormalType = this.cmb_VuType2.Text;
                    VascularUltrasound2.VascularAbnormalPart = this.cmb_VuType2Site.Text;
                    GlobalData.OtherExamInfo.listVascularUltrasound.Add(VascularUltrasound2);
                }
                if (this.cmb_VuType3.Text.Trim()!="")
                {
                    CDSSVascularUltrasound VascularUltrasound3 = new CDSSVascularUltrasound();
                    VascularUltrasound3.VascularAbnormalType = this.cmb_VuType3.Text;
                    VascularUltrasound3.VascularAbnormalPart = this.cmb_VuType3Site.Text;
                    GlobalData.OtherExamInfo.listVascularUltrasound.Add(VascularUltrasound3);
                }
            }
            //其它检查
            if (rbt_OtherAbnormal.Checked)
            {
                if (txb_Exam1.Text.Trim() != "")
                {
                    CDSSOtherExamAbnormal OtherExam1 = new CDSSOtherExamAbnormal();
                    OtherExam1.ExamItemName = txb_Exam1.Text;
                    OtherExam1.ExamResult = richtxb_Exam1Result.Text;
                    GlobalData.OtherExamInfo.listOtherExamAbnormal.Add(OtherExam1);
                }
                if (txb_Exam2.Text.Trim() != "")
                {
                    CDSSOtherExamAbnormal OtherExam2 = new CDSSOtherExamAbnormal();
                    OtherExam2.ExamItemName = txb_Exam2.Text;
                    OtherExam2.ExamResult = richtxb_Exam2Result.Text;
                    GlobalData.OtherExamInfo.listOtherExamAbnormal.Add(OtherExam2);
                }
                if (txb_Exam3.Text.Trim() != "")
                {
                    CDSSOtherExamAbnormal OtherExam3 = new CDSSOtherExamAbnormal();
                    OtherExam3.ExamItemName = txb_Exam3.Text;
                    OtherExam3.ExamResult = richtxb_Exam3Result.Text;
                    GlobalData.OtherExamInfo.listOtherExamAbnormal.Add(OtherExam3);
                }
                if (txb_Exam4.Text.Trim() != "")
                {
                    CDSSOtherExamAbnormal OtherExam4 = new CDSSOtherExamAbnormal();
                    OtherExam4.ExamItemName = txb_Exam4.Text;
                    OtherExam4.ExamResult = richtxb_Exam4Result.Text;
                    GlobalData.OtherExamInfo.listOtherExamAbnormal.Add(OtherExam4);
                }
            }
            IsModified = false;
        }

        /// <summary>
        /// 清空页面内容,//add by lch 090318 修复BugDB00005653 设置清空界面清空
        /// </summary>
        public void ClearData()
        {
            foreach (System.Windows.Forms.Control count in this.panel_OtherAbnormal.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
                if (count is RichTextBox)
                    count.Text = String.Empty;
            }
            foreach (System.Windows.Forms.Control count in this.panel_ECGAbnormal.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
                if (count is ComboBox)
                    count.Text = String.Empty;
            }
            foreach (System.Windows.Forms.Control count in this.tableLayoutPanel1.Controls)
            {
                if (count is ComboBox)
                    count.Text = String.Empty;
            }
            rbt_EcgNormal.Checked = true;
            rbt_VuNormal.Checked = true;
            rbt_OtherNormal.Checked = true;
            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
        }
        #endregion   
    }
}