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
        public bool bLoaded = false;    //��������Ƿ��Ѿ����ع�
        
        #region ϵͳ�¼�
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

            #region ��ѹ��ҩ
            oMedicineDictionary = new MedicineDictionary();

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "�������׿���";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "Ѫ�ܽ�����������ϼ�";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "Ѫ�ܽ�����ת��ø���Ƽ�";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "�����������ͼ�";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "����������";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);
            
            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "�������������ͼ�";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "�����Ƽ�";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "���ఢ�������弤����";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "����";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);


            #region ��λ��Ƶ�Ρ�;��

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "mg/��";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            //Ƶ��
            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "һ��1��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "һ��2��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "һ��3��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "һ��4��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "8Сʱһ��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);


            //;��
            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "�ڷ�";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "���";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "ͿĨ";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            #endregion

            this.medicineControl_jyxy.SetMedicineDictionary(oMedicineDictionary);
            #endregion

            #region �г�ҩ
            oMedicineDictionary = new MedicineDictionary();

            #region ��λ��Ƶ�Ρ�;��

            DrugUnits = new DrugUnits();
            DrugUnits.drugUnit = "��/��";
            oMedicineDictionary.DrugUnitsList.Add(DrugUnits);

            //Ƶ��
            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "һ��1��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "һ��2��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "һ��3��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "һ��4��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);

            DrugFrequency = new DrugFrequency();
            DrugFrequency.drugFrequency = "8Сʱһ��";
            oMedicineDictionary.DrugFrequencyList.Add(DrugFrequency);


            //;��
            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "�ڷ�";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "���";
            oMedicineDictionary.DrugByRouteList.Add(DrugByWays);

            DrugByWays = new DrugByRoute();
            DrugByWays.drugByRoute = "ͿĨ";
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
                    LoadDataFromVarToUI(); //�ڴ�����ʾ������ʱ���������
            }
        }
        #endregion

        #region �û��¼�
        /// <summary>
        /// ��ҳ�����ݸı�ʱ�������¼�
        /// </summary>
        /// <param name="sender">�¼�������</param>
        /// <param name="e"></param>
        protected void DataModified(object sender, EventArgs e)
        {
            //�������֮ǰ����δ�����,��ֱ�ӷ���
            if (IsModified || (!PatInfo.bNewPatient && !bLoaded))
                return;

            //�����¼�,֪ͨ��������±��水ť״̬
            RaiseDataChangedEvent(this, e);
            IsModified = true;
        }




        #endregion

        #region ���ܺ���
        public override void LoadDataFromVarToUI()
        {
            //��ҩ�ؼ��õ��ı���
            List<MedicineInfo> MedicineInfoList;

            if (GlobalData.HypertensionInfo.HasHypertension)    //true ��ʾ�и�Ѫѹ��false��ʾû��
            {
                this.rbt_HTY.Checked = true;

                //��Ѫѹ����
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


                //��ѹ��ҩ
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
                //�г�ҩ����
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

            

            //���������Ѽ��ر�־
            this.bLoaded = true;
        }
        
        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return;

            //��ҩ�ؼ��õ��ı���
            List<MedicineInfo> MedicineInfoList;

            //�ָ�Ĭ��ֵ
            GlobalData.HypertensionInfo.Clear();

            if (this.rbt_HTY.Checked)  //��ʾ�и�Ѫѹ
            {
                GlobalData.HypertensionInfo.HasHypertension = true;

                //��Ѫѹ����
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

                //��ѹ��ҩ
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
                //�г�ҩ����
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
        /// ���ҳ������
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

            //add by lch 090318 �޸�BugDB00005653 ������ո�����ҩ��ϢΪ��
            //��ҩ�ؼ��õ��ı���
            List<MedicineInfo> MedicineInfoList;
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_jyxy.SetMedicineInfo(MedicineInfoList);
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_zcy.SetMedicineInfo(MedicineInfoList);


            //��������δ���ر�־
            this.bLoaded = false;
            this.IsModified = false;
        }

        /// <summary>
        /// ��պ���
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
