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
    public partial class Nephropathy : InfoFormBaseClass
    {
        public bool bLoaded = false;    //标记数据是否已经加载过

        #region 系统事件
        public Nephropathy()
        {
            InitializeComponent();
            panel_NephType.Enabled = false;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_SBDate.Value = dt;
            dateTimePicker_SGNAbnormalDate.Value = dt;
        }

        private void rbt_NephropathyN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_NephropathyY.Checked)
            {
               dateTimePicker_SBDate.Enabled = true;
               panel_NephType.Enabled = true;
            }
            else
            {
                dateTimePicker_SBDate.Enabled = false;
                panel_NephType.Enabled = false;
            }
        }

        private void rbt_sgnAbnormalN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_sgnAbnormalY.Checked)
            {
                dateTimePicker_SGNAbnormalDate.Enabled = true;

            }
            else
            {
                dateTimePicker_SGNAbnormalDate.Enabled = false;       
                
                //*需要填写代码，清空panel组控件内所有控件的内容*
            }
        }

        private void rbt_ywxsss_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            
            if (rbt_ywxsss.Checked)
                txb_ywxsss.Enabled = true;
            else
                txb_ywxsss.Enabled = false;
            txb_ywxsss.Text = String.Empty;         
        }
        
        private void checkBox_Crunrevealed_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox_Crunrevealed.Checked)
            {
                txb_maxCr.Text = "";
                txb_maxCr.Enabled = false;
            }
            else
            {
                txb_maxCr.Enabled = true;
            }
        }

        private void checkBox_BUNunrevealed_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox_BUNunrevealed.Checked)
            {
                txb_maxBUN.Text = "";
                txb_maxBUN.Enabled = false;
            }
            else
            {
                txb_maxBUN.Enabled = true;
            }
        }

        private void Nephropathy_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!PatInfo.bNewPatient && !bLoaded)
                    LoadDataFromVarToUI(); //在窗体显示出来的时候加载数据
            }
        }

        private void rbt_otherSgnyc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_otherSgnyc.Checked)
                txb_otherSgnyc.Enabled = true;
            else
                txb_otherSgnyc.Enabled = false;
            txb_otherSgnyc.Text = "";
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
            if (GlobalData.NephropathyInfo .HasNephropathy)  //有糖尿病肾脏疾病
            {
                this.rbt_NephropathyY.Checked = true;

                //非糖尿病肾脏疾病类型
                if (GlobalData.NephropathyInfo.listNephropathySymptoms.Count > 0)
                {
                    this.dateTimePicker_SBDate.Text = GlobalData.NephropathyInfo.listNephropathySymptoms[0].SymptomsDetectedDateTime.ToString();

                    string tmp = GlobalData.NephropathyInfo.listNephropathySymptoms[0].SymptomsName.ToString();
                    if (tmp == "肾小球肾炎")
                        this.rbt_sxqsy.Checked = true;
                    else if (tmp == "间质性肾炎")
                        this.rbt_jzxsy.Checked = true;
                    else if (tmp == "肾盂肾炎")
                        this.rbt_sysy.Checked = true;
                    else if (tmp == "自家免疫性肾病")
                        this.rbt_zjmyxsy.Checked = true;
                    else if (tmp == "不明原因")
                        this.rbt_NoReasonSgnyc.Checked = true;
                    else if (tmp.Contains("药物性肾损伤"))
                    {
                        this.rbt_ywxsss.Checked = true;
                        if (tmp.IndexOf(':') != -1)
                        {
                            this.txb_ywxsss.Text = tmp.Substring(tmp.IndexOf(':') + 1);
                        }
                        else
                        {
                            this.txb_ywxsss.Text = "";
                        }
                        
                    }
                    else if (tmp.Contains("其它"))
                    {
                        this.rbt_otherSgnyc.Checked = true;
                        if (tmp.IndexOf(':') != -1)
                        {
                            this.txb_otherSgnyc.Text = tmp.Substring(tmp.IndexOf(':') + 1);
                        }
                        else
                        {
                            this.txb_otherSgnyc.Text = "";
                        }
                    }
                }

                //最高血肌酐值:具体值表示或者“不详”表示
                if (GlobalData.NephropathyInfo.MAXCreatinine != "不详")
                {
                    this.txb_maxCr.Text = GlobalData.NephropathyInfo.MAXCreatinine;
                    this.checkBox_Crunrevealed.Checked = false;
                }
                else
                {
                    this.txb_maxCr.Text = "";
                    this.checkBox_Crunrevealed.Checked = true;
                }

                //最高血尿素值
                if (GlobalData.NephropathyInfo.MAXBloodUrea != "不详")
                {
                    this.txb_maxBUN.Text = GlobalData.NephropathyInfo.MAXBloodUrea;
                    this.checkBox_BUNunrevealed.Checked = false;
                }
                else
                {
                    this.txb_maxBUN.Text = "";
                    this.checkBox_BUNunrevealed.Checked = true;
                }
            }
            else
                this.rbt_NephropathyY.Checked = false;

            

            //肾功能异常
            if (GlobalData.NephropathyInfo.HasRenalAbnormal)  //表示肾功能异常
            {
                this.rbt_sgnAbnormalY.Checked = true;
                this.dateTimePicker_SGNAbnormalDate.Text = GlobalData.NephropathyInfo.RenalAbnormalDetectedDateTime.ToString();

            }
            else
            {
                this.rbt_sgnAbnormalY.Checked = false;
            }

            
            //设置数据已加载标志
            this.bLoaded = true;
        }

        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return ;

            GlobalData.NephropathyInfo.Clear();

            if (this.rbt_NephropathyY.Checked)   //表示有非糖尿病肾脏疾病
            {
                GlobalData.NephropathyInfo.HasNephropathy = true;

                #region 非糖尿病肾脏疾病类型
                if (this.rbt_sxqsy.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = this.rbt_sxqsy.Text.ToString();
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_SBDate.Text);
                    GlobalData.NephropathyInfo.listNephropathySymptoms.Add(Symptoms);
                }
                else if (this.rbt_jzxsy.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = this.rbt_jzxsy.Text.ToString();
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_SBDate.Text);
                    GlobalData.NephropathyInfo.listNephropathySymptoms.Add(Symptoms);
                }
                else if (this.rbt_sysy.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = this.rbt_sysy.Text.ToString();
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_SBDate.Text);
                    GlobalData.NephropathyInfo.listNephropathySymptoms.Add(Symptoms);
                }
                else if (this.rbt_zjmyxsy.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = this.rbt_zjmyxsy.Text.ToString();
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_SBDate.Text);
                    GlobalData.NephropathyInfo.listNephropathySymptoms.Add(Symptoms);
                }
                else if (this.rbt_NoReasonSgnyc.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = this.rbt_NoReasonSgnyc.Text.ToString();
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_SBDate.Text);
                    GlobalData.NephropathyInfo.listNephropathySymptoms.Add(Symptoms);
                }
                else if (this.rbt_ywxsss.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = this.rbt_ywxsss.Text.ToString() + ":" + this.txb_ywxsss.Text.ToString();
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_SBDate.Text);
                    GlobalData.NephropathyInfo.listNephropathySymptoms.Add(Symptoms);
                }
                else if (this.rbt_otherSgnyc.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    //revised by lch 090402 修改非糖尿病肾脏疾病类型中的其它类型的赋值情况
                    Symptoms.SymptomsName = this.rbt_otherSgnyc.Text.ToString() + ":" + this.txb_otherSgnyc.Text.ToString();
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_SBDate.Text);
                    GlobalData.NephropathyInfo.listNephropathySymptoms.Add(Symptoms);
                }
                #endregion

                #region 最高血肌酐值and最高血尿素值
                string MaxCr = string.Empty;
                string MaxBU = string.Empty;

                if (this.txb_maxBUN.Text.ToString() == "")
                {
                    GlobalData.NephropathyInfo.MAXBloodUrea = "不详";
                }
                else
                    GlobalData.NephropathyInfo.MAXBloodUrea = this.txb_maxBUN.Text;

                if (this.txb_maxCr.Text.ToString() == "")
                {
                    GlobalData.NephropathyInfo.MAXCreatinine = "不详";
                }
                else
                    GlobalData.NephropathyInfo.MAXCreatinine = this.txb_maxCr.Text;
                #endregion
            }
            else
            {
                GlobalData.NephropathyInfo.HasNephropathy = false;
            }

            
            //肾功能异常
            if (this.rbt_sgnAbnormalY.Checked)  //表示肾功能异常
            {
                GlobalData.NephropathyInfo.HasRenalAbnormal = true;
                GlobalData.NephropathyInfo.RenalAbnormalDetectedDateTime = DateTime.Parse(this.dateTimePicker_SGNAbnormalDate.Text );
            }
            else
            {
                GlobalData.NephropathyInfo.HasRenalAbnormal = false ;
            }
            IsModified = false;
        }

        /// <summary>
        /// 清空页面内容
        /// </summary>
        public void ClearData()
        {
            foreach (System.Windows.Forms.Control count in this.panel_NephType.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
            }
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_SBDate.Value = dt;
            rbt_NephropathyN.Checked = true;
            rbt_sgnAbnormalN.Checked = true;
            rbt_sxqsy.Checked = false;
            rbt_jzxsy.Checked = false;
            rbt_sysy.Checked = false;
            rbt_zjmyxsy.Checked = false;
            rbt_NoReasonSgnyc.Checked = false;
            rbt_ywxsss.Checked = false;
            rbt_otherSgnyc.Checked = false;
            checkBox_Crunrevealed.Checked = false;
            checkBox_BUNunrevealed.Checked = false;
            panel_NephType.Enabled = false;        
            dateTimePicker_SGNAbnormalDate.Value = dt;
            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
        }

        #endregion
    }
}