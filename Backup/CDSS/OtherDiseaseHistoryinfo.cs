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
    public partial class OtherDiseaseHistoryinfo : InfoFormBaseClass
    {
        public bool bLoaded = false;    //标记数据是否已经加载过

        #region 系统事件
        public OtherDiseaseHistoryinfo()
        {
            InitializeComponent();
            panel_CHDType.Visible = false;
            panel_CVDType.Visible = false;
            panel_Operation.Enabled = false;          
            panel_PancreatitisType.Visible = false;
            panel_TumorDetail.Visible = false;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_YXYDate.Value = dt;
            dateTimePicker_TumourDate.Value = dt;
            dateTimePicker_OtherDate.Value = dt;
            dateTimePicker_OperDate.Value = dt;
            dateTimePicker_DNYDate.Value = dt;
            dateTimePicker_CVDDate.Value = dt;
            dateTimePicker_CHDDate.Value = dt;       
        }

        private void rbt_CHDN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_CHDY.Checked)
            {
                dateTimePicker_CHDDate.Enabled = true;
                panel_CHDType.Visible = true;
            }
            else
            {
                dateTimePicker_CHDDate.Enabled = false;
                panel_CHDType.Visible = false;
            }
        }

        private void rbt_CVDN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_CVDY.Checked)
            {
                dateTimePicker_CVDDate.Enabled = true;
                panel_CVDType.Visible = true;
            }
            else
            {
                dateTimePicker_CVDDate.Enabled = false;
                panel_CVDType.Visible = false;
            }
        }

        private void rbt_CholecystitisN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_CholecystitisY.Checked)
            {
                dateTimePicker_DNYDate.Enabled = true;
                panel_Operation.Enabled = true;
            }
            else
            {
                dateTimePicker_DNYDate.Enabled = false;
                panel_Operation.Enabled = false;

            }
        }

        private void rbt_OperationN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_OperationY.Checked)
                dateTimePicker_OperDate.Enabled = true;
            else
                dateTimePicker_OperDate.Enabled = false;
        }

        private void rbt_PancreatitisN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_PancreatitisY.Checked)
            {
                dateTimePicker_YXYDate.Enabled = true;
                panel_PancreatitisType.Visible = true;
            }
            else
            {
                dateTimePicker_YXYDate.Enabled = false;
                panel_PancreatitisType.Visible = false;
            }
        }

        private void rbt_TumorN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_TumorY.Checked)
            {
                panel_TumorDetail.Visible = true;
                dateTimePicker_TumourDate.Enabled = true;
            }
            else
            {
                panel_TumorDetail.Visible = false;
                dateTimePicker_TumourDate.Enabled = false;
            }
        }      

        private void rbt_OtherDN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_OtherDY.Checked)
            {
                txb_OtherDName.Enabled = true;
                dateTimePicker_OtherDate.Enabled = true;
                
            }
            else
            {
                txb_OtherDName.Enabled = false;
                dateTimePicker_OtherDate.Enabled = false;
                txb_OtherDName.Text = String.Empty;  
            }
        }

        private void OtherDiseaseHistoryinfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!PatInfo.bNewPatient && !bLoaded)
                    LoadDataFromVarToUI(); //在窗体显示出来的时候加载数据
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
            //冠心病
            if (GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Count > 0)
            {
                this.rbt_CHDY.Checked = true;

                this.dateTimePicker_CHDDate.Text = GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease[0].SymptomsDetectedDateTime.ToString();

                string tmp = GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease[0].SymptomsName;
                switch (tmp)
                {
                    case "心绞痛":
                        this.rbt_xjt.Checked = true;
                        break;
                    case "无症状心肌缺血":
                        this.rbt_wzzxjqx.Checked = true;
                        break;
                    case "心肌梗死":
                        this.rbt_xjgs.Checked = true;
                        break;
                    case "缺血性心肌病":
                        this.rbt_qxxxjb.Checked = true;
                        break;
                }
            }
            else
            {
                this.rbt_CHDY.Checked = false;
            }

            //脑血管疾病
            if (GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease.Count > 0)
            {
                this.rbt_CVDY.Checked = true;

                this.dateTimePicker_CVDDate.Text = GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease[0].SymptomsDetectedDateTime.ToString();

                string tmp = GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease[0].SymptomsName;
                switch (tmp)
                {
                    case "脑出血":
                        this.rbt_ncx.Checked = true;
                        break;
                    case "脑血栓":
                        this.rbt_nxs.Checked = true;
                        break;
                    case "TIA":
                        this.rbt_TIA.Checked = true;
                        break;
                    case "脑血管畸形":
                        this.rbt_nxgjx.Checked = true;
                        break;
                    case "缺血性脑血管病":
                        this.rbt_qxxnxgb.Checked = true;
                        break;
                }
            }
            else
            {
                this.rbt_CVDY.Checked = false;
            }
            
            //胆囊炎、胆石症
            if (GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis)
            {
                this.rbt_CholecystitisY.Checked = true;
                this.dateTimePicker_DNYDate.Text = GlobalData.OtherDiseaseHistoryInfo.CholecystitisDetectedDateTime.ToString();
            }
            else
            {
                this.rbt_CholecystitisY.Checked = false;
            }

            //胆囊摘除术
            if (GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery)
            {
                this.rbt_OperationY.Checked = true;
                this.dateTimePicker_OperDate.Text = GlobalData.OtherDiseaseHistoryInfo.GallbladderSurgeryDateTime.ToString();
            }
            else
            {
                this.rbt_OperationY.Checked = false;
            }

            //胰腺炎
            if (GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Count > 0)
            {
                this.rbt_PancreatitisY.Checked =true ;
                this.dateTimePicker_YXYDate.Text = GlobalData.OtherDiseaseHistoryInfo.listPancreatitis[0].SymptomsDetectedDateTime.ToString();

                for (int i = 0; i < GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Count; i++)
                {
                    if (GlobalData.OtherDiseaseHistoryInfo.listPancreatitis[i].SymptomsName.Equals("急性胰腺炎"))
                    {
                        this.cbx_jxyxy.Checked = true;
                    }
                    else if (GlobalData.OtherDiseaseHistoryInfo.listPancreatitis[i].SymptomsName.Equals("慢性胰腺炎"))
                    {
                        this.cbx_mxyxy.Checked = true;
                    }
                    else if (GlobalData.OtherDiseaseHistoryInfo.listPancreatitis[i].SymptomsName.Equals("胰腺（假性）囊肿"))
                    {
                        this.cbx_yxnz.Checked = true;
                    }
                }
            }
            else
            {
                this.rbt_PancreatitisY.Checked =false ;
            }

            //恶性肿瘤
            if (GlobalData.OtherDiseaseHistoryInfo.HasCancer)
            {
                this.rbt_TumorY.Checked = true;
                this.dateTimePicker_TumourDate.Text = GlobalData.OtherDiseaseHistoryInfo.CancerDetectedDateTime.ToString();
                this.txb_TumorSite.Text = GlobalData.OtherDiseaseHistoryInfo.CancerPart;

                string tmp = GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis;
                switch (tmp)
                {
                    case "良好":
                        this.rbt_TumorResult1.Checked = true;
                        break;
                    case "治疗中":
                        this.rbt_TumorResult2.Checked = true;
                        break;
                    case "恶化":
                        this.rbt_TumorResult3.Checked = true;
                        break;
                    case "未治":
                        this.rbt_TumorResult4.Checked = true;
                        break;
                }
            }
            else
            {
                this.rbt_TumorY.Checked =false ;
            }

            //其他疾病
            if (GlobalData.OtherDiseaseHistoryInfo.OtherDisease != "")
            {
                this.rbt_OtherDY.Checked = true;
                this.txb_OtherDName.Text = GlobalData.OtherDiseaseHistoryInfo.OtherDisease;
                this.dateTimePicker_OtherDate.Text = GlobalData.OtherDiseaseHistoryInfo.OtherDiseaseDetectedDateTime.ToString();
            }
            else
            {
                this.rbt_OtherDY.Checked =false ;
                this.txb_OtherDName.Text = "";
            }
            //设置数据已加载标志
            this.bLoaded = true;
        }

        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return;

            //恢复默认值
            GlobalData.OtherDiseaseHistoryInfo.Clear();

            //冠心病
            if (this.rbt_CHDY.Checked)
            {
                /* revised by lch 090319 修复BugDB00005658,
                 * 设置为：如果“有”选中，但没有选中任何类型，最后还是认为没有这种病*/
                CDSSSymptomsInfo Symptoms;

                if (rbt_xjt.Checked)
                {
                    Symptoms=new CDSSSymptomsInfo ();
                    Symptoms.SymptomsName = rbt_xjt.Text;
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(dateTimePicker_CHDDate.Text);
                    GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Add(Symptoms);
                }
                else if (rbt_wzzxjqx.Checked)
                {
                    Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = rbt_wzzxjqx.Text;
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(dateTimePicker_CHDDate.Text);
                    GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Add(Symptoms);
                }
                else if (rbt_xjgs.Checked)
                {
                    Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = rbt_xjgs.Text;
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(dateTimePicker_CHDDate.Text);
                    GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Add(Symptoms);
                }
                else if (rbt_qxxxjb.Checked)
                {
                    Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = rbt_qxxxjb.Text;
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(dateTimePicker_CHDDate.Text);
                    GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Add(Symptoms);
                }
                
            }
            //脑血管疾病
            if (this.rbt_CVDY.Checked)
            {
                CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();

                if (rbt_ncx.Checked)
                    Symptoms.SymptomsName = rbt_ncx.Text;
                else if (rbt_nxs.Checked)
                    Symptoms.SymptomsName = rbt_nxs.Text;
                else if (rbt_TIA.Checked)
                    Symptoms.SymptomsName = rbt_TIA.Text;
                else if (rbt_nxgjx.Checked)
                    Symptoms.SymptomsName = rbt_nxgjx.Text;
                else if (rbt_qxxnxgb.Checked)
                    Symptoms.SymptomsName = rbt_qxxnxgb.Text;
                Symptoms.SymptomsDetectedDateTime = DateTime.Parse(dateTimePicker_CVDDate.Text);

                GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease .Add(Symptoms);
            }
            //胆囊炎、胆石症
            if (this.rbt_CholecystitisY.Checked)
            {
                GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis = true;
                GlobalData.OtherDiseaseHistoryInfo.CholecystitisDetectedDateTime = DateTime.Parse(dateTimePicker_DNYDate.Text);
            }
            else
            {
                GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis = false ;
            }

            //胆囊摘除术
            if (this.rbt_OperationY.Checked)
            {
                GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery = true;
                GlobalData.OtherDiseaseHistoryInfo.GallbladderSurgeryDateTime = DateTime.Parse(dateTimePicker_OperDate.Text);
            }
            else
            {
                GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery = false ; 
            }
            //胰腺炎
            if (rbt_PancreatitisY.Checked)
            {
                if (cbx_jxyxy.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = cbx_jxyxy.Text;
                    Symptoms.SymptomsDetectedDateTime =  DateTime.Parse(dateTimePicker_YXYDate.Text);
                    GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Add(Symptoms);
                }
                if (cbx_mxyxy.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = cbx_mxyxy.Text;
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(dateTimePicker_YXYDate.Text);
                    GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Add(Symptoms);
                }
                if (cbx_yxnz.Checked)
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsName = cbx_yxnz.Text;
                    Symptoms.SymptomsDetectedDateTime = DateTime.Parse(dateTimePicker_YXYDate.Text);
                    GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Add(Symptoms);
                }
            }
            //恶性肿瘤
            if (rbt_TumorY.Checked)
            {
                GlobalData.OtherDiseaseHistoryInfo.HasCancer = true;
                GlobalData.OtherDiseaseHistoryInfo.CancerDetectedDateTime = DateTime.Parse(dateTimePicker_TumourDate.Text);
                GlobalData.OtherDiseaseHistoryInfo.CancerPart = txb_TumorSite.Text;
                if (rbt_TumorResult1.Checked)
                    GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis = rbt_TumorResult1.Text;
                else if (rbt_TumorResult2.Checked)
                    GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis = rbt_TumorResult2.Text;
                else if (rbt_TumorResult3.Checked)
                    GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis = rbt_TumorResult3.Text;
                else if (rbt_TumorResult4.Checked)
                    GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis = rbt_TumorResult4.Text;
            }
            else
            {
                GlobalData.OtherDiseaseHistoryInfo.HasCancer = false;
            }
            //其他疾病
            if (rbt_OtherDY.Checked)
            {
                GlobalData.OtherDiseaseHistoryInfo.OtherDisease = txb_OtherDName.Text;
                GlobalData.OtherDiseaseHistoryInfo.OtherDiseaseDetectedDateTime = DateTime.Parse(dateTimePicker_OtherDate.Text);
            }
            else
            {
                GlobalData.OtherDiseaseHistoryInfo.OtherDisease = "";
            }
            IsModified = false;
        }

        /// <summary>
        /// 清空页面内容
        /// </summary>
        public void ClearData()
        {
            dateTimePicker_YXYDate.Enabled = false;
            dateTimePicker_TumourDate.Enabled = false;
            dateTimePicker_OtherDate.Enabled = false;
            dateTimePicker_OperDate.Enabled = false;
            dateTimePicker_DNYDate.Enabled = false;
            dateTimePicker_CVDDate.Enabled = false;
            dateTimePicker_CHDDate.Enabled = false;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_YXYDate.Value = dt;
            dateTimePicker_TumourDate.Value = dt;
            dateTimePicker_OtherDate.Value = dt;
            dateTimePicker_OperDate.Value = dt;
            dateTimePicker_DNYDate.Value = dt;
            dateTimePicker_CVDDate.Value = dt;
            dateTimePicker_CHDDate.Value = dt;
            txb_TumorSite.Text = String.Empty;
            txb_OtherDName.Text = String.Empty;
            
            rbt_CHDN.Checked = true;
            rbt_xjt.Checked = false;
            rbt_wzzxjqx.Checked = false;
            rbt_xjgs.Checked = false;
            rbt_qxxxjb.Checked = false;

            rbt_CVDN.Checked = true;
            rbt_ncx.Checked = false;
            rbt_nxs.Checked = false;
            rbt_TIA.Checked = false;
            rbt_nxgjx.Checked = false;
            rbt_qxxnxgb.Checked = false;

            rbt_CholecystitisN.Checked = true;
            rbt_OperationN.Checked = true;
            rbt_PancreatitisN.Checked = true;           
            rbt_TumorN.Checked = true;
            rbt_TumorResult1.Checked = true;
            rbt_OtherDN.Checked = true;
            panel_CHDType.Visible = false;
            panel_CVDType.Visible = false;
            panel_Operation.Enabled = false;
            cbx_jxyxy.Checked = false;
            cbx_mxyxy.Checked = false;
            cbx_yxnz.Checked = false;         
            
            panel_PancreatitisType.Visible = false;
            panel_TumorDetail.Visible = false;
           
            txb_OtherDName.Enabled = false;
            
            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
        }

        #endregion   
    }
}