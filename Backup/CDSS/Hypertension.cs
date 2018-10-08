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
    public partial class Hypertension : InfoFormBaseClass
    {
        public bool bLoaded = false;    //标记数据是否已经加载过
        
        #region 系统事件
        public Hypertension()
        {
            InitializeComponent();
            panel_cDrugType.Visible = false;
            panel_jyWdrugType.Visible = false;
            panel_MaxMinPressure.Visible = false;
            panel_cDrug.Enabled = false;
            panel_jyWdrug.Enabled = false;
            panel_usual.Enabled = false;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_HTDate.Value = dt;

        }
        private void Hypertension_Load(object sender, EventArgs e)
        {
            MedicineDictionary oMedicineDictionary;
            MedicineDict oMedicineDict;
            DrugUnits DrugUnits;
            DrugFrequency DrugFrequency;
            DrugByRoute DrugByWays;

            #region 降压西药
            oMedicineDictionary = new MedicineDictionary();

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "钙离子拮抗剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "血管紧张素受体阻断剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "血管紧张素转换酶抑制剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "贝特受体阻滞剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "噻嗪类利尿剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);
            
            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "阿尔法受体阻滞剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "复方制剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "中枢阿尔法受体激动剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "其他";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);


            #region 单位、频次、途径

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "mg/日";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            //频次
            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "一日1次";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "一日2次";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

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

            this.medicineControl_jyxy.SetMedicineDictionary(oMedicineDictionary);
            #endregion

            #region 中成药
            oMedicineDictionary = new MedicineDictionary();

            #region 单位、频次、途径

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "粒/日";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            //频次
            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "一日1次";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "一日2次";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

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

            this.medicineControl_zcy.SetMedicineDictionary(oMedicineDictionary);
            #endregion
        }

        private void rbt_HTN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_HTY.Checked)
            {
                panel_MaxMinPressure.Visible = true;

                panel_cDrug.Enabled = true;
                panel_jyWdrug.Enabled = true;
                panel_usual.Enabled = true;
            }
            else
            {
                panel_MaxMinPressure.Visible = false;
                panel_cDrug.Enabled = false;
                panel_jyWdrug.Enabled = false;
                panel_cDrugType.Visible = false;
                panel_jyWdrugType.Visible = false;
                panel_MaxMinPressure.Visible = false;
                panel_usual.Enabled = false;
                rbt_ChinesDrugN.Checked = true;
                rbt_WestDrugN.Checked = true;
            }

        }

        private void rbt_WestDrugN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_WestDrugY.Checked)
                panel_jyWdrugType.Visible = true;
            else
                panel_jyWdrugType.Visible = false;
        }

        private void rbt_ChinesDrugN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_ChinesDrugY.Checked)
                panel_cDrugType.Visible = true;
            else
                panel_cDrugType.Visible = false;
        }

        private void Hypertension_VisibleChanged(object sender, EventArgs e)
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

            if (GlobalData.HypertensionInfo.HasHypertension)    //true 表示有高血压，false表示没有
            {
                this.rbt_HTY.Checked = true;

                //高血压类型
                if (GlobalData.HypertensionInfo.listHypertensionSymptoms.Count > 0)
                {
                    this.cmb_HypertensionType.Text = GlobalData.HypertensionInfo.listHypertensionSymptoms[0].SymptomsName;
                    this.dateTimePicker_HTDate.Text = GlobalData.HypertensionInfo.listHypertensionSymptoms[0].SymptomsDetectedDateTime.ToString();
                }
                else
                {
                    this.cmb_HypertensionType.Text = "";
                }

                this.txb_MaxSBP.Text = GlobalData.HypertensionInfo.MaxSBP;
                this.txb_MaxDBP.Text = GlobalData.HypertensionInfo.MaxDBP;
                this.txb_MinSBP.Text = GlobalData.HypertensionInfo.MinSBP;
                this.txb_MinDBP.Text = GlobalData.HypertensionInfo.MinDBP;


                //降压西药
                if (GlobalData.HypertensionInfo.listStepDownWestMedicine.Count > 0)
                {
                    rbt_WestDrugY.Checked = true;
                    MedicineInfoList = new List<MedicineInfo>();

                    foreach (CDSSMedicineInfo CDSSMedicineInfo in GlobalData.HypertensionInfo.listStepDownWestMedicine)
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
                    this.medicineControl_jyxy.SetMedicineInfo(MedicineInfoList);

                }
                //中成药治疗
                if (GlobalData.HypertensionInfo.listStepDownChineseMedication.Count > 0)
                {
                    rbt_ChinesDrugY.Checked = true;
                    MedicineInfoList = new List<MedicineInfo>();

                    foreach (CDSSMedicineInfo CDSSMedicineInfo in GlobalData.HypertensionInfo.listStepDownChineseMedication)
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
                    this.medicineControl_zcy.SetMedicineInfo(MedicineInfoList);

                }
                this.txb_TCYearL.Text = GlobalData.HypertensionInfo.BPControlFromYear;
                this.txb_TCYearR.Text = GlobalData.HypertensionInfo.BPControlToYear;
                this.txb_LeftSBP.Text = GlobalData.HypertensionInfo.PeacetimeMinSBP;
                this.txb_RightSBP.Text = GlobalData.HypertensionInfo.PeacetimeMaxSBP;
                this.txb_LeftDBP.Text = GlobalData.HypertensionInfo.PeacetimeMinDBP;
                this.txb_RightDBP.Text = GlobalData.HypertensionInfo.PeacetimeMaxDBP;
            }
            else
            {
                this.rbt_HTN.Checked = true ;
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
            GlobalData.HypertensionInfo.Clear();

            if (this.rbt_HTY.Checked)  //表示有高血压
            {
                GlobalData.HypertensionInfo.HasHypertension = true;

                //高血压类型
                if (this.cmb_HypertensionType.Text != "")
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.cmb_HypertensionType.Text;
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_HTDate.Text);
                    GlobalData.HypertensionInfo.listHypertensionSymptoms.Add(Types);
                }

                GlobalData.HypertensionInfo.MaxSBP = this.txb_MaxSBP.Text;
                GlobalData.HypertensionInfo.MaxDBP = this.txb_MaxDBP.Text;
                GlobalData.HypertensionInfo.MinSBP = this.txb_MinSBP.Text;
                GlobalData.HypertensionInfo.MinDBP = this.txb_MinDBP.Text;

                //降压西药
                MedicineInfoList = new List<MedicineInfo>();
                MedicineInfoList = this.medicineControl_jyxy.GetMedicineInfo();

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

                            GlobalData.HypertensionInfo.listStepDownWestMedicine.Add(CDSSMedicineInfo);
                        }
                    }
                }
                //中成药治疗
                MedicineInfoList = new List<MedicineInfo>();
                MedicineInfoList = this.medicineControl_zcy.GetMedicineInfo();

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

                            GlobalData.HypertensionInfo.listStepDownChineseMedication.Add(CDSSMedicineInfo);
                        }
                    }
                }
                GlobalData.HypertensionInfo.BPControlFromYear = this.txb_TCYearL.Text;
                GlobalData.HypertensionInfo.BPControlToYear = this.txb_TCYearR.Text;
                GlobalData.HypertensionInfo.PeacetimeMinSBP = this.txb_LeftSBP.Text;
                GlobalData.HypertensionInfo.PeacetimeMaxSBP = this.txb_RightSBP.Text;
                GlobalData.HypertensionInfo.PeacetimeMinDBP = this.txb_LeftDBP.Text;
                GlobalData.HypertensionInfo.PeacetimeMaxDBP = this.txb_RightDBP.Text;

            }
            else if (this.rbt_HTN.Checked)
            {
                GlobalData.HypertensionInfo.HasHypertension = false;
            }
            IsModified = false;
        }
        
        /// <summary>
        /// 清空页面内容
        /// </summary>
        public void ClearData()
        {
            ResetControl(this.Controls);

            panel_cDrugType.Visible = false;
            panel_jyWdrugType.Visible = false;
            panel_MaxMinPressure.Visible = false;
            panel_cDrug.Enabled = false;
            panel_jyWdrug.Enabled = false;
            panel_usual.Enabled = false;

            rbt_HTN.Checked = true;
            rbt_WestDrugN.Checked = true;
            rbt_ChinesDrugN.Checked = true;

            //add by lch 090318 修复BugDB00005653 设置清空各项用药信息为空
            //用药控件用到的变量
            List<MedicineInfo> MedicineInfoList;
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_jyxy.SetMedicineInfo(MedicineInfoList);
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_zcy.SetMedicineInfo(MedicineInfoList);


            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
        }

        /// <summary>
        /// 清空函数
        /// </summary>
        /// <param name="Controls"></param>
        private void ResetControl(Control.ControlCollection Controls)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    control.Text = String.Empty;
                }
                if (control is ComboBox)
                {
                    control.Text = String.Empty;
                }
                if (control is CDSSCtrlLib.TextBoxNumControl)
                {
                    ((CDSSCtrlLib.TextBoxNumControl)control).Text = String.Empty;
                }
                if (control is CDSSCtrlLib.DateTimeControl)
                {
                    ((CDSSCtrlLib.DateTimeControl)control).Value = new DateTime(DateTime.Now.Year, 1, 1);
                }
                else if (control is Panel)
                {
                    ResetControl(control.Controls);
                }
                else if (control is TableLayoutPanel)
                {
                    ResetControl(control.Controls);
                }
                else if (control is GroupBox)
                {
                    ResetControl(control.Controls);
                }
            }
        }

        #endregion  
    }
}
