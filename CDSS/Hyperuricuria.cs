using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSSystemData;
using CDSSCtrlLib.MedicineControlLib;

namespace CDSS
{
    public partial class Hyperuricuria : InfoFormBaseClass
    {
        public bool bLoaded = false;    //标记数据是否已经加载过
        private bool bGJHZ = false;  //关节红肿是否有效 

        #region 系统事件
        public Hyperuricuria()
        {
            InitializeComponent();
            panel_HUTypeReason.Enabled = false;
            panel_jnsDrugType.Visible = false;
            panel_JNSY.Enabled = false;
            panel_TFGJY.Enabled = false;
            panel_TFS.Enabled = false;
            //revise 1119
            panel_arthritis_flare.Enabled = false;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_HUADate.Value = dt;
            dateTimePicker_TFDate.Value = dt;
        }
        private void Hyperuricuria_Load(object sender, EventArgs e)
        {
            MedicineDictionary oMedicineDictionary;
            MedicineDict oMedicineDict;
            DrugUnits DrugUnits;
            DrugFrequency DrugFrequency;
            DrugByRoute DrugByWays;

            #region 降尿酸药
            oMedicineDictionary = new MedicineDictionary();

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "别嘌呤醇";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "苯溴马隆";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "丙磺舒";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "其它";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            #region 单位、频次、途径

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "mg/日";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            //频次
            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "一日3次";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "一日4次";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "8小时一次";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);


            //途径
            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "口服";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "外敷";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "涂抹";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            #endregion

            this.medicineControl_jnsy.SetMedicineDictionary(oMedicineDictionary);
            #endregion
        }

        private void rbt_HUN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_HUY.Checked)
            {
                panel_HUTypeReason.Enabled = true;
                panel_JNSY.Enabled = true;
                panel_TFGJY.Enabled = true;
                panel_TFS.Enabled = true;
            }
            else
            {
                panel_HUTypeReason.Enabled = false;
                panel_JNSY.Enabled = false;
                panel_TFGJY.Enabled = false;
                panel_TFS.Enabled = false;
                panel_arthritis_flare.Enabled = false;
                rbt_HUtfgjyN.Checked = true;
                rbt_jnsDrugN.Checked = true;
                rbt_tfsN.Checked = true;
            }
        }

        private void rbt_HUtfgjyN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_HUtfgjyN.Checked)
            {
                dateTimePicker_TFDate.Enabled = false;
                if (rbt_tfsN.Checked)
                    panel_arthritis_flare.Enabled = false;
            }
            else
            {
                dateTimePicker_TFDate.Enabled = true;
                panel_arthritis_flare.Enabled = true;
            }
        }

        private void rbt_tfsN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_tfsY.Checked)
            {
                txb_tfsSite.Enabled = true;
                panel_arthritis_flare.Enabled = true;

            }
            else
            {
                txb_tfsSite.Enabled = false;
                if (rbt_HUtfgjyN.Checked)
                    panel_arthritis_flare.Enabled = false;
            }

        }

        private void rbt_jnsDrugN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_jnsDrugY.Checked)
                //revised by lch 090319 修复BugDB00005661，设置控件可见性
                panel_jnsDrugType.Visible= true;
            else
                panel_jnsDrugType.Visible= false;
        }

        private void rbt_HUReasonyf_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_HUReasonyf.Checked)
            {
                cmb_HUType.SelectedIndex = -1;
                cmb_HUType.Items.Clear();
                cmb_HUType.Items.Add("痛风性肾病");
                cmb_HUType.Items.Add("痛风性关节炎");
                cmb_HUType.Items.Add("高尿酸血症");
            }
            else
            {
                cmb_HUType.SelectedIndex = -1;
                cmb_HUType.Items.Clear();
                cmb_HUType.Items.Add("肾脏病变");
                cmb_HUType.Items.Add("恶性肿瘤");
                cmb_HUType.Items.Add("药物影响");
            }
        }

       
        private void Hyperuricuria_VisibleChanged(object sender, EventArgs e)
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
            //用药控件用到的变量
            List<MedicineInfo> MedicineInfoList;

            if (GlobalData.HyperuricemiaInfo.HasHyperuricemia)    //表示有高尿酸血症
            {
                this.rbt_HUY.Checked = true;

                //高尿酸类型
                if (GlobalData.HyperuricemiaInfo.HyperuricemiaType.Equals("继发"))
                {
                    this.rbt_HUReasonjf.Checked = true;
                }
                else if (GlobalData.HyperuricemiaInfo.HyperuricemiaType.Equals("原发"))
                {
                    this.rbt_HUReasonjf.Checked = false;
                }

                if (GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms.Count > 0)
                {
                    this.cmb_HUType.Text = GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms[0].SymptomsName;
                    this.dateTimePicker_HUADate.Text = GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms[0].SymptomsDetectedDateTime.ToString();
                }
                else
                {
                    this.cmb_HUType.Text = "";
                }

                //痛风关节炎
                if (GlobalData.HyperuricemiaInfo.HasGoutyArthritis)
                {
                    this.rbt_HUtfgjyY.Checked = true;
                    this.dateTimePicker_TFDate.Text = GlobalData.HyperuricemiaInfo.GoutyArthritisDetectedDateTime.ToString();
                }
                else
                {
                    this.rbt_HUtfgjyY.Checked = false;
                }

                //痛风石
                if (GlobalData.HyperuricemiaInfo.HasTophus)
                {
                    this.rbt_tfsY.Checked = true;
                    this.txb_tfsSite.Text = GlobalData.HyperuricemiaInfo.TophusPart;
                }
                else
                {
                    this.rbt_tfsY.Checked = false;
                }

                //关节红肿
                if (GlobalData.HyperuricemiaInfo.HasJointSwelling)
                {
                    this.rbt_arthritisflareY.Checked = true;
                }
                else
                {
                    this.rbt_arthritisflareY.Checked = false;
                }

                //降尿酸药
                if (GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs.Count > 0)
                {
                    rbt_jnsDrugY.Checked = true;
                    MedicineInfoList = new List<MedicineInfo>();

                    foreach (CDSSMedicineInfo CDSSMedicineInfo in GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs)
                    {
                        MedicineInfo MedicineInfo = new MedicineInfo();
                        MedicineInfo.DrugType = CDSSMedicineInfo.Drugtype;
                        MedicineInfo.DrugName = CDSSMedicineInfo.DrugNames;
                        MedicineInfo.DrugAmount = CDSSMedicineInfo.DrugAmount;
                        MedicineInfo.DrugByRoute = CDSSMedicineInfo.DrugByRoute;
                        MedicineInfo.DrugUnits = CDSSMedicineInfo.DrugUnits;
                        MedicineInfo.DrugFrequency = CDSSMedicineInfo.DrugFrequency;
                        MedicineInfo.DrugBeginTime = CDSSMedicineInfo.DrugBeginTime;
                        MedicineInfo.DrugEndTime = CDSSMedicineInfo.DrugEndTime;

                        MedicineInfoList.Add(MedicineInfo);
                    }
                    this.medicineControl_jnsy.SetMedicineInfo(MedicineInfoList);
                }
            }
            else
            {
                this.rbt_HUY.Checked = false ;
            }
            
            //设置数据已加载标志
            this.bLoaded = true;
        }

        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return;
            //用药控件用到的变量
            List<MedicineInfo> MedicineInfoList;

            //恢复默认值
            GlobalData .HyperuricemiaInfo.Clear ();

            if(this.rbt_HUY.Checked)
            {
                GlobalData .HyperuricemiaInfo .HasHyperuricemia =true;

                //高尿酸类型
                if (this.rbt_HUReasonyf.Checked)   //原发
                {
                    GlobalData.HyperuricemiaInfo.HyperuricemiaType = "原发";
                    
                }
                else    //继发
                {
                    GlobalData.HyperuricemiaInfo.HyperuricemiaType = "继发";

                }
                if (this.cmb_HUType.Text != "")
                {
                    CDSSSymptomsInfo types = new CDSSSymptomsInfo();
                    types.SymptomsName  = this.cmb_HUType.Text;
                    types.SymptomsDetectedDateTime  = DateTime .Parse (this.dateTimePicker_HUADate.Text);
                    GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms.Add(types);
                }

                //痛风关节炎
                if (this.rbt_HUtfgjyY.Checked)      //有痛风关节炎
                {
                    GlobalData.HyperuricemiaInfo.HasGoutyArthritis = true;
                    GlobalData.HyperuricemiaInfo.GoutyArthritisDetectedDateTime = DateTime .Parse (this.dateTimePicker_TFDate.Text);
                }
                else     //无痛风关节炎
                {
                    GlobalData.HyperuricemiaInfo.HasGoutyArthritis = false ;
                }

                //痛风石
                if (this.rbt_tfsY.Checked)  //有痛风石
                {
                    GlobalData.HyperuricemiaInfo.HasTophus = true;
                    GlobalData.HyperuricemiaInfo.TophusPart = this.txb_tfsSite.Text;
                }
                else    //无痛风石
                {
                    GlobalData.HyperuricemiaInfo.HasTophus = false ;
                }

                //关节红肿
                if (this.rbt_arthritisflareY.Enabled && this.rbt_arthritisflareY.Checked) //有关节红肿
                {
                    GlobalData.HyperuricemiaInfo.HasJointSwelling = true;
                }
                else   //无关节红肿
                {
                    GlobalData.HyperuricemiaInfo.HasJointSwelling = false;
                }

                //降尿酸药
                MedicineInfoList = new List<MedicineInfo>();
                MedicineInfoList = this.medicineControl_jnsy.GetMedicineInfo();

                if (MedicineInfoList != null)
                {
                    if (MedicineInfoList.Count > 0)
                    {
                        foreach (MedicineInfo MedicineInfo in MedicineInfoList)
                        {
                            CDSSMedicineInfo CDSSMedicineInfo = new CDSSMedicineInfo();
                            CDSSMedicineInfo.DrugAmount = MedicineInfo.DrugAmount;
                            CDSSMedicineInfo.DrugBeginTime = MedicineInfo.DrugBeginTime;
                            CDSSMedicineInfo.DrugByRoute = MedicineInfo.DrugByRoute;
                            CDSSMedicineInfo.DrugEndTime = MedicineInfo.DrugEndTime;
                            CDSSMedicineInfo.DrugFrequency = MedicineInfo.DrugFrequency;
                            CDSSMedicineInfo.DrugNames = MedicineInfo.DrugName;
                            CDSSMedicineInfo.Drugtype = MedicineInfo.DrugType;
                            CDSSMedicineInfo.DrugUnits = MedicineInfo.DrugUnits;

                            GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs.Add(CDSSMedicineInfo);
                        }
                    }
                }        
            }
            else 
            {
                GlobalData.HyperuricemiaInfo.HasHyperuricemia = false ;
            }
            IsModified = false;
        }

        /// <summary>
        /// 清空页面内容
        /// </summary>
        public void ClearData()
        {
            cmb_HUType.Text = String.Empty;
            txb_tfsSite.Text = String.Empty;
            rbt_HUN.Checked = true;
            rbt_HUReasonyf.Checked = true;
            rbt_HUtfgjyN.Checked = true;
            rbt_tfsN.Checked = true;
            rbt_jnsDrugN.Checked = true;
            rbt_arthritisflareN.Checked = true;

            panel_arthritis_flare.Enabled = false;
            panel_HUTypeReason.Enabled = false;
            panel_jnsDrugType.Visible = false;
            panel_JNSY.Enabled = false;
            panel_TFGJY.Enabled = false;
            panel_TFS.Enabled = false;

            //add by lch 090318 修复BugDB00005653 设置清空各项用药信息为空
            //用药控件用到的变量
            List<MedicineInfo> MedicineInfoList;
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_jnsy.SetMedicineInfo(MedicineInfoList);


            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_HUADate.Value = dt;
            dateTimePicker_TFDate.Value = dt;
            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
        }

        #endregion
        
    }
}