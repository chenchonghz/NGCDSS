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
    public partial class AGMinfo : InfoFormBaseClass
    {
        public bool bLoaded = false;    //标记数据是否已经加载过

        #region 系统事件 
        public AGMinfo()
        {
            InitializeComponent();
            this.panel_DType.Visible = false;
            this.panel_HTType.Visible = false;
            this.panel_IType.Visible = false;
            this.panel_TType.Visible = false;
            this.panel_AcuteType.Visible = false;
            this.panel_ChronicType.Visible = false;
            dateTimePicker_AGMDate.Enabled = false;
            panel_Acute.Enabled = false;
            panel_Chronic.Enabled = false;
            panel_Diagnosed.Enabled = false;
            panel_HT.Enabled = false;
            panel_TT.Enabled = false;
            panel_IT.Enabled = false;
            //发现时间 初始化
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_AGMDate.Value = dt;
            dateTimePicker_DTime1.Value = dt;
            dateTimePicker_DTime2.Value = dt;
            dateTimePicker_jxbfTime1.Value = dt;
            dateTimePicker_jxbfTime2.Value = dt;
            dateTimePicker_mxbfTime1.Value = dt;
            dateTimePicker_mxbfTime2.Value = dt;
            dateTimePicker_mxbfTime3.Value = dt;
            dateTimePicker_mxbfTime4.Value = dt;
        }

        /// <summary>
        /// 窗体登录，设置药品字典信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AGMinfo_Load(object sender, EventArgs e)
        {
            MedicineDictionary oMedicineDictionary;
            MedicineDict oMedicineDict;
            DrugUnits DrugUnits;
            DrugFrequency DrugFrequency;
            DrugByRoute DrugByWays;

            #region 降糖药
            oMedicineDictionary = new MedicineDictionary();

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "磺酰脲类";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "格列奈类";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "双胍类";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "糖苷酶抑制剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "格列酮类";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "复合剂";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);


            #region 单位、频次、途径

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "mg/每次";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "粒/每次";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "ML/每勺";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "包/每次";
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

            this.medicineControl_jty.SetMedicineDictionary(oMedicineDictionary);
            #endregion

            #region  中成药
            oMedicineDictionary = new MedicineDictionary();

            #region 单位、频次、途径

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "mg/每次";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "粒/每次";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "ML/每勺";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "包/每次";
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

            #region  胰岛素
            oMedicineDictionary = new MedicineDictionary();

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "普通胰岛素";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "精蛋白锌胰岛素";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "精蛋白锌重组人胰岛素N";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "重组人胰岛素R";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "甘精胰岛素";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "地特胰岛素";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "门冬胰岛素";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "赖脯胰岛素";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "预混人胰岛素30R（70/30）";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "预混人胰岛素50R";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "预混门冬胰岛素30";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "预混赖脯胰岛素25";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            #region 单位、频次、途径

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "U/日";
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
            DrugByWays.drugByRoute = "注射器";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "注射笔";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "胰岛素泵";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            #endregion
            this.medicineControl_yds.SetMedicineDictionary(oMedicineDictionary);
            #endregion

        }

        private void rbt_DN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (this.rbt_DY.Checked)
            {
                this.panel_DType.Visible = true;
            }
            else
                this.panel_DType.Visible = false;
        }

        private void rbt_jxbfN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (this.rbt_jxbfY.Checked)
                this.panel_AcuteType.Visible = true;
            else
                this.panel_AcuteType.Visible = false;
        }

        private void rbt_mxbfN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (this.rbt_mxbfY.Checked)
                this.panel_ChronicType.Visible = true;
            else
                this.panel_ChronicType.Visible = false;
        }

        private void rbt_jtyN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (this.rbt_jtyY.Checked)
                this.panel_HTType.Visible = true;
            else
                this.panel_HTType.Visible = false;
        }

        private void rbt_zyN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_zyY.Checked)
                this.panel_TType.Visible = true;
            else
                this.panel_TType.Visible = false;
        }

        private void rbt_ydsN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_ydsY.Checked)
                this.panel_IType.Visible = true;
            else
                this.panel_IType.Visible = false;
        }

        private void rbt_Normal_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_Abnormal.Checked)
            {
                dateTimePicker_AGMDate.Enabled = true;
                panel_Acute.Enabled = true;
                panel_Chronic.Enabled = true;
                panel_Diagnosed.Enabled = true;
                panel_HT.Enabled = true;
                panel_TT.Enabled = true;
                panel_IT.Enabled = true;
            }
            else
            {
                dateTimePicker_AGMDate.Enabled = false;
                panel_Acute.Enabled = false;
                panel_Chronic.Enabled = false;
                panel_Diagnosed.Enabled = false;
                panel_HT.Enabled = false;
                panel_TT.Enabled = false;
                panel_IT.Enabled = false;
                this.panel_DType.Visible = false;
                this.panel_HTType.Visible = false;
                this.panel_IType.Visible = false;
                this.panel_TType.Visible = false;
                this.panel_AcuteType.Visible = false;
                this.panel_ChronicType.Visible = false;
                rbt_DN.Checked = true;
                rbt_jtyN.Checked = true;
                rbt_jxbfN.Checked = true;
                rbt_mxbfN.Checked = true;
                rbt_ydsN.Checked = true;
                rbt_zyN.Checked = true;
            }

        }
       
        private void AGMinfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!PatInfo.bNewPatient && !bLoaded)
                    LoadDataFromVarToUI (); //在窗体显示出来的时候加载数据
            }            
        }

        private void cmb_DType1_TextChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            dateTimePicker_DTime1.Value = dateTimePicker_AGMDate.Value;
        }

        private void cmb_DType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            dateTimePicker_DTime2.Value = dateTimePicker_AGMDate.Value;
        }
        #endregion

        #region 用户事件
        /// <summary>
        /// 当页面内容改变时引发该事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e"></param>
        protected  void DataModified(object sender, EventArgs e)
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
        /// <summary>
        /// 从全局变量加载数据到界面
        /// </summary>
        public override void LoadDataFromVarToUI()
        {
            //用药控件用到的变量
            List<MedicineInfo> MedicineInfoList;

            if (GlobalData.AGMInfo.HasAGMAbnormal)   //true表示异常，false表示正常
            {
                this.rbt_Abnormal.Checked = true;        //rbt_Abnormal表示异常

                this.dateTimePicker_AGMDate.Text = GlobalData.AGMInfo.AbnormalDetectedDateTime.ToString();

                #region 糖尿病确诊类型
                if (GlobalData.AGMInfo.listConfirmedSymptoms.Count > 0)
                {
                    this.rbt_DY.Checked = true;            //rbt_DY 表示确诊
                    for (int i = 0; i < GlobalData.AGMInfo.listConfirmedSymptoms.Count; i++)
                    {
                        if (i == 0)
                        {
                            this.cmb_DType1.Text = GlobalData.AGMInfo.listConfirmedSymptoms[0].SymptomsName.ToString();
                            this.dateTimePicker_DTime1.Text = GlobalData.AGMInfo.listConfirmedSymptoms[0].SymptomsDetectedDateTime.ToString();
                        }
                        if (i == 1)
                        {
                            this.cmb_DType2.Text = GlobalData.AGMInfo.listConfirmedSymptoms[1].SymptomsName.ToString();
                            this.dateTimePicker_DTime2.Text = GlobalData.AGMInfo.listConfirmedSymptoms[1].SymptomsDetectedDateTime.ToString();
                        }
                    }
                }
                else
                {
                    this.rbt_DN.Checked = true;
                    this.cmb_DType1.Text = "";
                    this.cmb_DType2.Text = "";
                }
                #endregion

                #region 急性并发症
                if (GlobalData.AGMInfo.listAcuteSymptoms.Count > 0)
                {
                    this.rbt_jxbfY.Checked = true;         //rbt_jxbfY表示有急性并发症
                    for (int i = 0; i < GlobalData.AGMInfo.listAcuteSymptoms.Count; i++)
                    {
                        if (i == 0)
                        {
                            this.cmb_jxbfType1.Text = GlobalData.AGMInfo.listAcuteSymptoms[i].SymptomsName.ToString();
                            this.dateTimePicker_jxbfTime1.Text = GlobalData.AGMInfo.listAcuteSymptoms[i].SymptomsDetectedDateTime.ToString();
                        }
                        if (i == 1)
                        {
                            this.cmb_jxbfType2.Text = GlobalData.AGMInfo.listAcuteSymptoms[i].SymptomsName.ToString();
                            this.dateTimePicker_jxbfTime2.Text = GlobalData.AGMInfo.listAcuteSymptoms[i].SymptomsDetectedDateTime.ToString();
                        }
                    }
                }
                else
                {
                    this.rbt_jxbfN.Checked = true;        //rbt_jxbfN表示没有急性并发症
                    this.cmb_jxbfType1.Text = "";
                    this.cmb_jxbfType2.Text = "";
                }
                #endregion

                #region 慢性并发症
                if (GlobalData.AGMInfo.listChronicSymptoms.Count > 0)
                {
                    this.rbt_mxbfY.Checked = true;          //rbt_mxbfY表示有慢性并发症
                    /*revised by lch 090319 修复 BugDB00005652  
                    遍历的list写错了，写成了急性并发症的list，导致数据没有加载上来。*/
                    for (int i = 0; i < GlobalData.AGMInfo.listChronicSymptoms.Count; i++)
                    {
                        if (i == 0)
                        {
                            this.cmb_mxbfType1.Text = GlobalData.AGMInfo.listChronicSymptoms[0].SymptomsName.ToString();
                            this.dateTimePicker_mxbfTime1.Text = GlobalData.AGMInfo.listChronicSymptoms[0].SymptomsDetectedDateTime.ToString();
                        }
                        if (i == 1)
                        {
                            this.cmb_mxbfType2.Text = GlobalData.AGMInfo.listChronicSymptoms[1].SymptomsName.ToString();
                            this.dateTimePicker_mxbfTime2.Text = GlobalData.AGMInfo.listChronicSymptoms[1].SymptomsDetectedDateTime.ToString();
                        }
                        if (i == 2)
                        {
                            this.cmb_mxbfType3.Text = GlobalData.AGMInfo.listChronicSymptoms[2].SymptomsName.ToString();
                            this.dateTimePicker_mxbfTime3.Text = GlobalData.AGMInfo.listChronicSymptoms[2].SymptomsDetectedDateTime.ToString();
                        }
                        if (i == 3)
                        {
                            this.cmb_mxbfType4.Text = GlobalData.AGMInfo.listChronicSymptoms[3].SymptomsName.ToString();
                            this.dateTimePicker_mxbfTime4.Text = GlobalData.AGMInfo.listChronicSymptoms[3].SymptomsDetectedDateTime.ToString();
                        }
                    }
                }
                else
                {
                    this.rbt_mxbfN.Checked = true;        //rbt_mxbfN表示没有慢性并发症
                    this.cmb_mxbfType1.Text = "";
                    this.cmb_mxbfType2.Text = "";
                    this.cmb_mxbfType3.Text = "";
                    this.cmb_mxbfType4.Text = "";
                }
                #endregion

                #region 降糖药治疗
                if (GlobalData.AGMInfo.listHypogMedicineInfo.Count > 0)
                {
                    rbt_jtyY.Checked = true;
                    MedicineInfoList = new List<MedicineInfo>();

                    foreach (CDSSMedicineInfo CDSSMedicineInfo in GlobalData.AGMInfo.listHypogMedicineInfo)
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
                    this.medicineControl_jty.SetMedicineInfo(MedicineInfoList);

                }
                #endregion

                #region 中成药治疗
                if (GlobalData.AGMInfo.listChineseMedicineInfo.Count > 0)
                {
                    rbt_zyY.Checked = true;
                    MedicineInfoList = new List<MedicineInfo>();

                    foreach (CDSSMedicineInfo CDSSMedicineInfo in GlobalData.AGMInfo.listChineseMedicineInfo)
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
                #endregion

                #region 胰岛素治疗
                if (GlobalData.AGMInfo.listInsulinMedicineInfo.Count > 0)
                {
                    rbt_ydsY.Checked = true;
                    MedicineInfoList = new List<MedicineInfo>();

                    foreach (CDSSMedicineInfo CDSSMedicineInfo in GlobalData.AGMInfo.listInsulinMedicineInfo)
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
                    this.medicineControl_yds.SetMedicineInfo(MedicineInfoList);
                }
                #endregion
            }
            else
            {
                this.rbt_Normal.Checked = true;           //rbt_Normal表示正常
            }

            

            //设置数据已加载标志
            this.bLoaded = true;
        }

        /// <summary>
        /// 将界面数据保存至变量
        /// </summary>
        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return;

            //用药控件用到的变量
            List<MedicineInfo> MedicineInfoList;

            //恢复默认值
            GlobalData.AGMInfo.Clear();

            if (this.rbt_Abnormal.Checked)     //表示异常
            {
                GlobalData.AGMInfo.HasAGMAbnormal = true;
                //add by lch 090408 for BugDB00005799,保存糖尿病发现时间
                GlobalData.AGMInfo.AbnormalDetectedDateTime =DateTime.Parse(this.dateTimePicker_AGMDate.Text);
                #region 确诊类型
                if (this.cmb_DType1.Text.Trim() != "")
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.cmb_DType1.Text.Trim();
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_DTime1.Text);
                    GlobalData.AGMInfo.listConfirmedSymptoms.Add(Types);
                }
                if (this.cmb_DType2.Text.Trim() != "")
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.cmb_DType2.Text.Trim();
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_DTime2.Text);
                    GlobalData.AGMInfo.listConfirmedSymptoms.Add(Types);
                }
                #endregion

                #region 急性并发症
                if (this.cmb_jxbfType1.Text != "")
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.cmb_jxbfType1.Text.Trim();
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_jxbfTime1.Text);
                    GlobalData.AGMInfo.listAcuteSymptoms.Add(Types);
                }
                if (this.cmb_jxbfType2.Text != "")
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.cmb_jxbfType2.Text.Trim();
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_jxbfTime2.Text);
                    GlobalData.AGMInfo.listAcuteSymptoms.Add(Types);
                }
                #endregion

                #region 慢性并发症
                if (this.cmb_mxbfType1.Text != "")
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.cmb_mxbfType1.Text.Trim();
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_mxbfTime1.Text);
                    GlobalData.AGMInfo.listChronicSymptoms.Add(Types);
                }
                if (this.cmb_mxbfType2.Text != "")
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.cmb_mxbfType2.Text.Trim();
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_mxbfTime2.Text);
                    GlobalData.AGMInfo.listChronicSymptoms.Add(Types);
                }
                if (this.cmb_mxbfType3.Text != "")
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.cmb_mxbfType3.Text.Trim();
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_mxbfTime3.Text);
                    GlobalData.AGMInfo.listChronicSymptoms.Add(Types);
                }
                if (this.cmb_mxbfType4.Text != "")
                {
                    CDSSSymptomsInfo Types = new CDSSSymptomsInfo();
                    Types.SymptomsName = this.cmb_mxbfType4.Text.Trim();
                    Types.SymptomsDetectedDateTime = DateTime.Parse(this.dateTimePicker_mxbfTime4.Text);
                    GlobalData.AGMInfo.listChronicSymptoms.Add(Types);
                }
                #endregion

                #region 降糖药治疗
                MedicineInfoList = new List<MedicineInfo>();
                MedicineInfoList = this.medicineControl_jty.GetMedicineInfo();

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

                            GlobalData.AGMInfo.listHypogMedicineInfo.Add(CDSSMedicineInfo);
                        }
                    }
                }
                #endregion

                #region 中成药治疗
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

                            GlobalData.AGMInfo.listChineseMedicineInfo.Add(CDSSMedicineInfo);
                        }
                    }
                }
                #endregion

                #region 胰岛素治疗
                MedicineInfoList = new List<MedicineInfo>();
                MedicineInfoList = this.medicineControl_yds.GetMedicineInfo();

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

                            GlobalData.AGMInfo.listInsulinMedicineInfo.Add(CDSSMedicineInfo);
                        }
                    }
                }
                #endregion

            }
            else if (this.rbt_Normal.Checked)
            {
                GlobalData.AGMInfo.HasAGMAbnormal = false;          //表示糖代谢正常，则其他所有信息均为空。
            }

            IsModified = false;
        }

        /// <summary>
        /// 清空页面内容
        /// </summary>
        public void ClearData()
        {
            rbt_Normal.Checked = true;
            rbt_DN.Checked = true;
            rbt_jxbfN.Checked = true;
            rbt_mxbfN.Checked = true;
            rbt_jtyN.Checked = true;
            rbt_zyN.Checked = true;
            rbt_ydsN.Checked = true;
            panel_DType.Visible = false;
            panel_HTType.Visible = false;
            panel_IType.Visible = false;
            panel_TType.Visible = false;
            panel_AcuteType.Visible = false;
            panel_ChronicType.Visible = false;
            panel_Acute.Enabled = false;
            panel_Chronic.Enabled = false;
            panel_Diagnosed.Enabled = false;
            panel_HT.Enabled = false;
            panel_TT.Enabled = false;
            panel_IT.Enabled = false;
            dateTimePicker_AGMDate.Enabled = false;
            
            ResetControl(this.Controls);

            //add by lch 090318 修复BugDB00005653 设置清空各项用药信息为空
            //用药控件用到的变量
            List<MedicineInfo> MedicineInfoList;

            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_jty.SetMedicineInfo(MedicineInfoList);
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_zcy.SetMedicineInfo(MedicineInfoList);
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_yds.SetMedicineInfo(MedicineInfoList);
  
            
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
            }
        }

        #endregion

    }
}
