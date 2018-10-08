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
        public bool bLoaded = false;    //��������Ƿ��Ѿ����ع�
        private bool bGJHZ = false;  //�ؽں����Ƿ���Ч 

        #region ϵͳ�¼�
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

            #region ������ҩ
            oMedicineDictionary = new MedicineDictionary();

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "�����ʴ�";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "������¡";
            oMedicineDictionary.MedicineDictList.Add(oMedicineDict);

            oMedicineDict = new MedicineDict();
            oMedicineDict.drugType = "������";
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
                //revised by lch 090319 �޸�BugDB00005661�����ÿؼ��ɼ���
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
                cmb_HUType.Items.Add("ʹ��������");
                cmb_HUType.Items.Add("ʹ���Թؽ���");
                cmb_HUType.Items.Add("������Ѫ֢");
            }
            else
            {
                cmb_HUType.SelectedIndex = -1;
                cmb_HUType.Items.Clear();
                cmb_HUType.Items.Add("���ಡ��");
                cmb_HUType.Items.Add("��������");
                cmb_HUType.Items.Add("ҩ��Ӱ��");
            }
        }

       
        private void Hyperuricuria_VisibleChanged(object sender, EventArgs e)
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

            if (GlobalData.HyperuricemiaInfo.HasHyperuricemia)    //��ʾ�и�����Ѫ֢
            {
                this.rbt_HUY.Checked = true;

                //����������
                if (GlobalData.HyperuricemiaInfo.HyperuricemiaType.Equals("�̷�"))
                {
                    this.rbt_HUReasonjf.Checked = true;
                }
                else if (GlobalData.HyperuricemiaInfo.HyperuricemiaType.Equals("ԭ��"))
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

                //ʹ��ؽ���
                if (GlobalData.HyperuricemiaInfo.HasGoutyArthritis)
                {
                    this.rbt_HUtfgjyY.Checked = true;
                    this.dateTimePicker_TFDate.Text = GlobalData.HyperuricemiaInfo.GoutyArthritisDetectedDateTime.ToString();
                }
                else
                {
                    this.rbt_HUtfgjyY.Checked = false;
                }

                //ʹ��ʯ
                if (GlobalData.HyperuricemiaInfo.HasTophus)
                {
                    this.rbt_tfsY.Checked = true;
                    this.txb_tfsSite.Text = GlobalData.HyperuricemiaInfo.TophusPart;
                }
                else
                {
                    this.rbt_tfsY.Checked = false;
                }

                //�ؽں���
                if (GlobalData.HyperuricemiaInfo.HasJointSwelling)
                {
                    this.rbt_arthritisflareY.Checked = true;
                }
                else
                {
                    this.rbt_arthritisflareY.Checked = false;
                }

                //������ҩ
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
            GlobalData .HyperuricemiaInfo.Clear ();

            if(this.rbt_HUY.Checked)
            {
                GlobalData .HyperuricemiaInfo .HasHyperuricemia =true;

                //����������
                if (this.rbt_HUReasonyf.Checked)   //ԭ��
                {
                    GlobalData.HyperuricemiaInfo.HyperuricemiaType = "ԭ��";
                    
                }
                else    //�̷�
                {
                    GlobalData.HyperuricemiaInfo.HyperuricemiaType = "�̷�";

                }
                if (this.cmb_HUType.Text != "")
                {
                    CDSSSymptomsInfo types = new CDSSSymptomsInfo();
                    types.SymptomsName  = this.cmb_HUType.Text;
                    types.SymptomsDetectedDateTime  = DateTime .Parse (this.dateTimePicker_HUADate.Text);
                    GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms.Add(types);
                }

                //ʹ��ؽ���
                if (this.rbt_HUtfgjyY.Checked)      //��ʹ��ؽ���
                {
                    GlobalData.HyperuricemiaInfo.HasGoutyArthritis = true;
                    GlobalData.HyperuricemiaInfo.GoutyArthritisDetectedDateTime = DateTime .Parse (this.dateTimePicker_TFDate.Text);
                }
                else     //��ʹ��ؽ���
                {
                    GlobalData.HyperuricemiaInfo.HasGoutyArthritis = false ;
                }

                //ʹ��ʯ
                if (this.rbt_tfsY.Checked)  //��ʹ��ʯ
                {
                    GlobalData.HyperuricemiaInfo.HasTophus = true;
                    GlobalData.HyperuricemiaInfo.TophusPart = this.txb_tfsSite.Text;
                }
                else    //��ʹ��ʯ
                {
                    GlobalData.HyperuricemiaInfo.HasTophus = false ;
                }

                //�ؽں���
                if (this.rbt_arthritisflareY.Enabled && this.rbt_arthritisflareY.Checked) //�йؽں���
                {
                    GlobalData.HyperuricemiaInfo.HasJointSwelling = true;
                }
                else   //�޹ؽں���
                {
                    GlobalData.HyperuricemiaInfo.HasJointSwelling = false;
                }

                //������ҩ
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
        /// ���ҳ������
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

            //add by lch 090318 �޸�BugDB00005653 ������ո�����ҩ��ϢΪ��
            //��ҩ�ؼ��õ��ı���
            List<MedicineInfo> MedicineInfoList;
            MedicineInfoList = new List<MedicineInfo>();
            this.medicineControl_jnsy.SetMedicineInfo(MedicineInfoList);


            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker_HUADate.Value = dt;
            dateTimePicker_TFDate.Value = dt;
            //��������δ���ر�־
            this.bLoaded = false;
            this.IsModified = false;
        }

        #endregion
        
    }
}