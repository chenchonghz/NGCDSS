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
    public partial class LipidsDisorder : InfoFormBaseClass
    {
        public bool bLoaded = false;    //标记数据是否已经加载过

        #region 系统事件
        public LipidsDisorder()
        {
            InitializeComponent();
            panel_DrugType.Visible = false;
            panel_DrugYN.Enabled = false;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_LDDate.Value = dt;
            dateTimePicker_LDDate.Enabled = false;
            panel_LDType.Enabled = false;
        }

        private void LipidsDisorder_Load(object sender, EventArgs e)
        {
            MedicineDictionary oMedicineDictionary;
            MedicineDict oMedicineDict;
            DrugUnits DrugUnits;
            DrugFrequency DrugFrequency;
            DrugByRoute DrugByWays;

            #region 调脂药
            oMedicineDictionary = new MedicineDictionary();

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "他汀类";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "贝特类";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "胆固醇吸收抑制剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "胆酸鳌合剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "烟酸制剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "脂肪酶抑制剂";
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

            this.medicineControl_tzy.SetMedicineDictionary(oMedicineDictionary);
            #endregion
        }

        private void rbt_LDN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_LDY.Checked)
            {
                panel_DrugYN.Enabled = true;
                dateTimePicker_LDDate.Enabled = true;
                panel_LDType.Enabled = true;
                dateTimePicker_LDDate.Enabled = true;              
            }
            else
            {
                panel_DrugType.Visible = false;
                panel_DrugYN.Enabled = false;
                dateTimePicker_LDDate.Enabled = false;
                panel_LDType.Enabled =false;
                rbt_DrugN.Checked = true;
                dateTimePicker_LDDate.Enabled = false;
            }
        }

        private void rbt_DrugN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_DrugY.Checked)
            {
                //revised by lch 090319 修复BugDB00005661，设置控件可见性
                panel_DrugType.Visible = true;
            }
            else
            {
                panel_DrugType.Visible = false;
            }
        }

        private void LipidsDisorder_VisibleChanged(object sender, EventArgs e)
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

            if (GlobalData.DyslipidemiaInfo.HasDyslipidemia)   //表示有血脂紊乱
            {
                this.rbt_LDY.Checked = true;

                #region 血脂紊乱类型
                if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Count > 0)
                {
                    this.dateTimePicker_LDDate.Text = GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[0].SymptomsDetectedDateTime.ToString();

                    for (int i = 0; i < GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Count; i++)
                    {
                        if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsName.Equals("高胆固醇"))
                        {
                            this.checkBox_TC.Checked = true;
                        }
                        else if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsName.Equals("低高密度脂蛋白胆固醇血症"))
                        {
                            this.checkBox_HDLC.Checked = true;
                        }
                        else if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsName.Equals("高甘油三酯血症"))
                        {
                            this.checkBox_TG.Checked = true;
                        }
                        else if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsName.Equals("高低密度脂蛋白胆固醇血症"))
                        {
                            this.checkBox_LDLC.Checked = true;
                        }
                    }
                }
                else
                {
                    this.checkBox_TC.Checked = false;
                    this.checkBox_HDLC.Checked = false;
                    this.checkBox_TG.Checked = false;
                    this.checkBox_LDLC.Checked = false;
                }
                #endregion

                //调脂药
                if (GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Count > 0)
                {
                    rbt_DrugY.Checked = true;
                    MedicineInfoList = new List<MedicineInfo>();

                    foreach (CDSSMedicineInfo CDSSMedicineInfo in GlobalData.DyslipidemiaInfo.listLipidlowerDrugs)
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
                    this.medicineControl_tzy.SetMedicineInfo(MedicineInfoList);
                }

            }
            else
            {
                this.rbt_LDY.Checked = false ;
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

            GlobalData.DyslipidemiaInfo.Clear();

            if (this.rbt_LDY.Checked)  // 表示有血脂紊乱
            {
                GlobalData.DyslipidemiaInfo.HasDyslipidemia = true;

                #region 血脂紊乱类型
                if (this.checkBox_TC.Checked)
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName  = this.checkBox_TC.Text ;
                    Types.SymptomsDetectedDateTime  = DateTime .Parse (this.dateTimePicker_LDDate.Text);
                    GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Add(Types); 
                }
                if (this.checkBox_HDLC.Checked)
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.checkBox_HDLC.Text;
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_LDDate.Text);
                    GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Add(Types);
                }
                if (this.checkBox_TG.Checked)
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.checkBox_TG.Text;
                    Types.SymptomsDetectedDateTime = DateTime .Parse (this.dateTimePicker_LDDate.Text);
                    GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Add(Types);
                }
                if (this.checkBox_LDLC.Checked)
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.checkBox_LDLC.Text;
                    Types.SymptomsDetectedDateTime = DateTime .Parse (this.dateTimePicker_LDDate.Text);
                    GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Add(Types);
                }
                #endregion

                //调脂药
                MedicineInfoList = new List<MedicineInfo>();
                MedicineInfoList = this.medicineControl_tzy.GetMedicineInfo();
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

                            GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Add(CDSSMedicineInfo);
                        }
                    }
                }
            }
            else
            {
                GlobalData.DyslipidemiaInfo.HasDyslipidemia = false ;
            }
            IsModified = false;
        }

        /// <summary>
        /// 清空页面内容
        /// </summary>
        public void ClearData()
        {
            foreach (System.Windows.Forms.Control count in this.panel_LDYN.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
                if (count is ComboBox)
                    count.Text = String.Empty;
            }
            rbt_LDN.Checked = true;
            rbt_DrugN.Checked = true;
            panel_DrugType.Visible = false;
            panel_DrugYN.Enabled = false;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_LDDate.Value = dt;
            dateTimePicker_LDDate.Enabled = false;
            checkBox_TC.Checked = false;
            checkBox_HDLC.Checked = false;
            checkBox_TG.Checked = false;
            checkBox_LDLC.Checked = false;

            //add by lch 090318 修复BugDB00005653 设置清空各项用药信息为空
            //用药控件用到的变量
            List<MedicineInfo> MedicineInfoList;
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_tzy.SetMedicineInfo(MedicineInfoList);

            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
        }

        #endregion
    }
}