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
        public bool bLoaded = false;    //��������Ƿ��Ѿ����ع�

        #region ϵͳ�¼�
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

            #region ��֬ҩ
            oMedicineDictionary = new MedicineDictionary();

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "��͡��";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "������";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "���̴��������Ƽ�";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "�������ϼ�";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "�����Ƽ�";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "֬��ø���Ƽ�";
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
                //revised by lch 090319 �޸�BugDB00005661�����ÿؼ��ɼ���
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

            if (GlobalData.DyslipidemiaInfo.HasDyslipidemia)   //��ʾ��Ѫ֬����
            {
                this.rbt_LDY.Checked = true;

                #region Ѫ֬��������
                if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Count > 0)
                {
                    this.dateTimePicker_LDDate.Text = GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[0].SymptomsDetectedDateTime.ToString();

                    for (int i = 0; i < GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Count; i++)
                    {
                        if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsName.Equals("�ߵ��̴�"))
                        {
                            this.checkBox_TC.Checked = true;
                        }
                        else if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsName.Equals("�͸��ܶ�֬���׵��̴�Ѫ֢"))
                        {
                            this.checkBox_HDLC.Checked = true;
                        }
                        else if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsName.Equals("�߸�������Ѫ֢"))
                        {
                            this.checkBox_TG.Checked = true;
                        }
                        else if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsName.Equals("�ߵ��ܶ�֬���׵��̴�Ѫ֢"))
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

                //��֬ҩ
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

            
            //���������Ѽ��ر�־
            this.bLoaded = true;
        }

        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return;

            //��ҩ�ؼ��õ��ı���
            List<MedicineInfo> MedicineInfoList;

            GlobalData.DyslipidemiaInfo.Clear();

            if (this.rbt_LDY.Checked)  // ��ʾ��Ѫ֬����
            {
                GlobalData.DyslipidemiaInfo.HasDyslipidemia = true;

                #region Ѫ֬��������
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

                //��֬ҩ
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
        /// ���ҳ������
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

            //add by lch 090318 �޸�BugDB00005653 ������ո�����ҩ��ϢΪ��
            //��ҩ�ؼ��õ��ı���
            List<MedicineInfo> MedicineInfoList;
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_tzy.SetMedicineInfo(MedicineInfoList);

            //��������δ���ر�־
            this.bLoaded = false;
            this.IsModified = false;
        }

        #endregion
    }
}