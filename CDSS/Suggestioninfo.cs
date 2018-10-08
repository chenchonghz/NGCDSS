using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSSystemData;

namespace CDSS
{
    public partial class Suggestioninfo :Form
    {
        private bool bLoaded = false;    //��������Ƿ��Ѿ����ع�
        private bool bReasoned = false; //����Ƿ��Ѿ������

        private bool bDataLoading = false;//add by wbf 081230 Bug G18 ��ֹloading�����д���cmb��Ӧ

        #region ϵͳ�¼�
        public Suggestioninfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ҳ�����ʱĬ����ʾ��ʳ����TABҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Suggestioninfo_Load(object sender, EventArgs e)
        {
            this.lblDietSuggestion.Image = global::CDSS.Properties.Resources.TabDiet_focus;
            this.lblExerciseSuggestion.Image = global::CDSS.Properties.Resources.TabExercise_default;
            this.pnlExerciseSuggestion.Visible = false;
            this.pnlDietSuggestion.Visible = true;
            this.lblDietSuggestion.BringToFront();
            this.lblExerciseSuggestion.SendToBack();
            this.isModified = false;
        }

        /// <summary>
        /// ��������ʾ״̬�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Suggestioninfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                //if (PatInfo.bNewPatient)
                //{
                //    if (!this.bReasoned)    //�������벡��,���δ�������ִ������
                //        DoReasoning();
                //}
                ////reivsed by wbf 081231 �ر���ʳ�˶�����load����
                //else
                //{
                //    if (!bLoaded)   //������ʷ����,�����ʷ����δ���ع���ִ�м���
                //        LoadDataFromVarToUI();
                //}

                //BugDB00005736 revised by wbf 2009-03-27
                LoadDataFromVarToUI();
            }
            else
            {
                LoadDataFromUIToVar();
            }
        }

        /// <summary>
        /// �����������ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReReason_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ClearData();
            DoReasoning();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// ��ʾ��ʳ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDietSuggestion_Click(object sender, EventArgs e)
        {
            this.lblDietSuggestion.Image = global::CDSS.Properties.Resources.TabDiet_focus;
            this.lblExerciseSuggestion.Image = global::CDSS.Properties.Resources.TabExercise_default;
            this.pnlExerciseSuggestion.Visible = false;
            this.pnlDietSuggestion.Visible = true;
            this.lblDietSuggestion.BringToFront();
            this.lblExerciseSuggestion.SendToBack();
        }

        /// <summary>
        /// ��ʾ�˶�����ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblExerciseSuggestion_Click(object sender, EventArgs e)
        {
            this.lblExerciseSuggestion.Image = global::CDSS.Properties.Resources.TabExercise_focus;
            this.lblDietSuggestion.Image = global::CDSS.Properties.Resources.TabDietdefault;
            this.pnlDietSuggestion.Visible = false;
            this.pnlExerciseSuggestion.Visible = true;
            this.lblExerciseSuggestion.BringToFront();
            this.lblDietSuggestion.SendToBack();
        }    

        #endregion

        #region �û��¼�

        #endregion

        #region ���ܺ���
        /// <summary>
        /// ������ʾ���
        /// </summary>
        private void DoReasoning()
        {

            //����Ϊ������
            this.bReasoned = true;
        }
        
        /// <summary>
        /// ��ս�������
        /// </summary>
        public void ClearData()
        {
            this.lblDietSuggestion.Image = global::CDSS.Properties.Resources.TabDiet_focus;
            this.lblExerciseSuggestion.Image = global::CDSS.Properties.Resources.TabExercise_default;
            this.pnlExerciseSuggestion.Visible = false;
            this.pnlDietSuggestion.Visible = true;
            this.lblDietSuggestion.BringToFront();
            this.lblExerciseSuggestion.SendToBack();

            this.lblDietType.Text = String.Empty;
            this.lblTotalEnergy.Text = String.Empty;
            this.lblTotalWater.Text = String.Empty;

            this.lblCarboPercent.Text = String.Empty;
            this.lblCarboCount.Text = String.Empty;
            this.lblCerealCount.Text = String.Empty;
            this.lblCerealDetail.Text = String.Empty;
            this.lblGreenstuffCount.Text = String.Empty;
            this.lblGreenstuffDetail.Text = String.Empty;
            this.lblFruitcount.Text = String.Empty;
            this.lblFruitDetail.Text = String.Empty;

            this.lblProteinPercent.Text = String.Empty;
            this.lblProteinCount.Text = String.Empty;
            this.lblDairyCount.Text = String.Empty;
            this.lblDairyDetail.Text = String.Empty;
            this.lblEggCount.Text = String.Empty;
            this.lblEggDetail.Text = String.Empty;
            this.lblMeatCount.Text = String.Empty;
            this.lblMeatDetail.Text = String.Empty;
            this.lblBeanProductCount.Text = String.Empty;
            this.lblBeanProductDetail.Text = String.Empty;

            this.lblFatPercent.Text = String.Empty;
            this.lblFatCount.Text = String.Empty;
            this.lblVegetableOilCount.Text = String.Empty;
            this.lblVegetableOilDetail.Text = String.Empty;
            this.lblOtherFatFoodCount.Text = String.Empty;
            this.lblOtherFatFoodDetail.Text = String.Empty;

            this.lblExerciseTarget.Text = String.Empty;
            this.lblEnergyCost.Text = String.Empty;
            this.lblNoIntensityExercise.Text = String.Empty;
            this.lblNoIntensityExerciseItems.Text = String.Empty;
            this.lblLowIntensityExercise.Text = String.Empty;
            this.lblLowIntensityExerciseItems.Text = String.Empty;
            this.lblMiddleIntensityExercise.Text = String.Empty;
            this.lblMiddleIntensityExerciseItems.Text = String.Empty;
            this.lblHighIntensityExercise.Text = String.Empty;
            this.lblHighIntensityExerciseItems.Text = String.Empty;
            this.lblExerciseDataNeeded.Text = String.Empty;
            this.isModified = false;
            GlobalData.DietSuggestion.Clear();
            GlobalData.ExerciseSuggestion.Clear();

            //��������δ���ر�־
            this.bLoaded = false; 
            //����δ�����־
            this.bReasoned = false;
        }

        /// <summary>
        /// �������ϵ����ݱ��浽ȫ�ֱ�����
        /// </summary>
        /// <returns></returns>
        public void LoadDataFromUIToVar()
        {
            //��ʳ����
            GlobalData.DietSuggestion.Clear();

            GlobalData.DietSuggestion.DietType =this.lblDietType.Text;
            GlobalData.DietSuggestion.TotalEnergy=this.lblTotalEnergy.Text ;
            GlobalData.DietSuggestion.TotalWater = this.lblTotalWater.Text;
            GlobalData.DietSuggestion.CarboPercent =this.lblCarboPercent.Text ;
            GlobalData.DietSuggestion.CarboCount=this.lblCarboCount.Text  ;
            GlobalData.DietSuggestion.CerealCount=this.lblCerealCount.Text  ;
            GlobalData.DietSuggestion.CerealDetail=this.lblCerealDetail.Text  ;
            GlobalData.DietSuggestion.Fruitcount=this.lblFruitcount.Text  ;
            GlobalData.DietSuggestion.FruitDetail=this.lblFruitDetail.Text  ;
            GlobalData.DietSuggestion.GreenstuffCount =this.lblGreenstuffCount.Text ;
            GlobalData.DietSuggestion.GreenstuffDetail=this.lblGreenstuffDetail.Text  ;
            GlobalData.DietSuggestion.ProteinPercent=this.lblProteinPercent.Text  ;
            GlobalData.DietSuggestion.ProteinCount=this.lblProteinCount.Text  ;
            GlobalData.DietSuggestion.DairyCount=this.lblDairyCount.Text  ;
            GlobalData.DietSuggestion.DairyDetail=this.lblDairyDetail.Text  ;
            GlobalData.DietSuggestion.EggCount=this.lblEggCount.Text  ;
            GlobalData.DietSuggestion.EggDetail=this.lblEggDetail.Text  ;
            GlobalData.DietSuggestion.MeatCount= this.lblMeatCount.Text ;
            GlobalData.DietSuggestion.MeatDetail=this.lblMeatDetail.Text  ;
            GlobalData.DietSuggestion.BeanProductCount=this.lblBeanProductCount.Text  ;
            GlobalData.DietSuggestion.BeanProductDetail=this.lblBeanProductDetail.Text  ;
            GlobalData.DietSuggestion.FatPercent=this.lblFatPercent.Text  ;
            GlobalData.DietSuggestion.FatCount=this.lblFatCount.Text ;
            GlobalData.DietSuggestion.VegetableOilCount=this.lblVegetableOilCount.Text  ;
            GlobalData.DietSuggestion.VegetableOilDetail=this.lblVegetableOilDetail.Text  ;
            GlobalData.DietSuggestion.OtherFatFoodCount=this.lblOtherFatFoodCount.Text  ;
            GlobalData.DietSuggestion.OtherFatFoodDetail=this.lblOtherFatFoodDetail.Text  ;
            GlobalData.DietSuggestion.LimitedFood=this.lblLimitedFood.Text  ;
            GlobalData.DietSuggestion.AvoidFood=this.lblAvoidFood.Text  ;
            GlobalData.DietSuggestion.DataNeeded=this.lblDietDataNeeded.Text  ;

            //�˶�����
            GlobalData.ExerciseSuggestion.Clear();

            GlobalData.ExerciseSuggestion.ExerciseTarget=this.lblExerciseTarget.Text  ;
            GlobalData.ExerciseSuggestion.EnergyCost=this.lblEnergyCost.Text  ;
            GlobalData.ExerciseSuggestion.NoIntensityExercise=this.lblNoIntensityExercise.Text  ;
            GlobalData.ExerciseSuggestion.NoIntensityExerciseItems =this.lblNoIntensityExerciseItems.Text ;
            GlobalData.ExerciseSuggestion.LowIntensityExercise= this.lblLowIntensityExercise.Text ;
            GlobalData.ExerciseSuggestion.LowIntensityExerciseItems=this.lblLowIntensityExerciseItems.Text  ;
            GlobalData.ExerciseSuggestion.MiddleIntensityExercise=this.lblMiddleIntensityExercise.Text  ;
            GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems=this.lblMiddleIntensityExerciseItems.Text  ;
            GlobalData.ExerciseSuggestion.HighIntensityExercise=this.lblHighIntensityExercise.Text  ;
            GlobalData.ExerciseSuggestion.HighIntensityExerciseItems=this.lblHighIntensityExerciseItems.Text  ;
            GlobalData.ExerciseSuggestion.DataNeeded=this.lblExerciseDataNeeded.Text  ;

            //IsModified = false;
        }       


        /// <summary>
        ///��ȫ�ֱ����е�������ʾ�������� 
        /// </summary>
        private void LoadDataFromVarToUI()
        {
            //��ʳ����
            this.lblDietType.Text=GlobalData.DietSuggestion.DietType;
            this.lblTotalEnergy.Text = GlobalData.DietSuggestion.TotalEnergy;
            this.lblTotalWater.Text = GlobalData.DietSuggestion.TotalWater;
            this.lblCarboPercent.Text = GlobalData.DietSuggestion.CarboPercent;
            this.lblCarboCount.Text = GlobalData.DietSuggestion.CarboCount;
            this.lblCerealCount.Text = GlobalData.DietSuggestion.CerealCount;
            this.lblCerealDetail.Text = GlobalData.DietSuggestion.CerealDetail;
            this.lblFruitcount.Text = GlobalData.DietSuggestion.Fruitcount;
            this.lblFruitDetail.Text = GlobalData.DietSuggestion.FruitDetail;
            this.lblGreenstuffCount.Text = GlobalData.DietSuggestion.GreenstuffCount;
            this.lblGreenstuffDetail.Text = GlobalData.DietSuggestion.GreenstuffDetail;
            this.lblProteinPercent.Text = GlobalData.DietSuggestion.ProteinPercent;
            this.lblProteinCount.Text = GlobalData.DietSuggestion.ProteinCount;
            this.lblDairyCount.Text = GlobalData.DietSuggestion.DairyCount;
            this.lblDairyDetail.Text = GlobalData.DietSuggestion.DairyDetail;
            this.lblEggCount.Text = GlobalData.DietSuggestion.EggCount;
            this.lblEggDetail.Text = GlobalData.DietSuggestion.EggDetail;
            this.lblMeatCount.Text = GlobalData.DietSuggestion.MeatCount;
            this.lblMeatDetail.Text = GlobalData.DietSuggestion.MeatDetail;
            this.lblBeanProductCount.Text = GlobalData.DietSuggestion.BeanProductCount;
            this.lblBeanProductDetail.Text = GlobalData.DietSuggestion.BeanProductDetail;
            this.lblFatPercent.Text = GlobalData.DietSuggestion.FatPercent;
            this.lblFatCount.Text = GlobalData.DietSuggestion.FatCount;
            this.lblVegetableOilCount.Text = GlobalData.DietSuggestion.VegetableOilCount;
            this.lblVegetableOilDetail.Text = GlobalData.DietSuggestion.VegetableOilDetail;
            this.lblOtherFatFoodCount.Text = GlobalData.DietSuggestion.OtherFatFoodCount;
            this.lblOtherFatFoodDetail.Text = GlobalData.DietSuggestion.OtherFatFoodDetail;
            this.lblLimitedFood.Text = GlobalData.DietSuggestion.LimitedFood;
            this.lblAvoidFood.Text = GlobalData.DietSuggestion.AvoidFood;
            this.lblDietDataNeeded.Text = GlobalData.DietSuggestion.DataNeeded;

            //�˶�����
            this.lblExerciseTarget.Text = GlobalData.ExerciseSuggestion.ExerciseTarget;
            this.lblEnergyCost.Text = GlobalData.ExerciseSuggestion.EnergyCost;
            this.lblNoIntensityExercise.Text = GlobalData.ExerciseSuggestion.NoIntensityExercise;
            this.lblNoIntensityExerciseItems.Text = GlobalData.ExerciseSuggestion.NoIntensityExerciseItems;
            this.lblLowIntensityExercise.Text = GlobalData.ExerciseSuggestion.LowIntensityExercise;
            this.lblLowIntensityExerciseItems.Text = GlobalData.ExerciseSuggestion.LowIntensityExerciseItems;
            this.lblMiddleIntensityExercise.Text = GlobalData.ExerciseSuggestion.MiddleIntensityExercise;
            this.lblMiddleIntensityExerciseItems.Text = GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems;
            this.lblHighIntensityExercise.Text = GlobalData.ExerciseSuggestion.HighIntensityExercise;
            this.lblHighIntensityExerciseItems.Text = GlobalData.ExerciseSuggestion.HighIntensityExerciseItems;
            this.lblExerciseDataNeeded.Text = GlobalData.ExerciseSuggestion.DataNeeded;

            SetPicBoxStatus();
            //���������Ѽ��ر�־
            this.bLoaded = true;
        }

        //BugDB00005715 revised by wbf 2009-03-26
        private void SetPicBoxStatus()
        {
            if (this.lblCerealCount.Text == string.Empty)
            {
                picCereal.Enabled = false;
            }
            else
            {
                picCereal.Enabled = true;
            }

            if( this.lblFruitcount.Text == string.Empty)
            {
                picFruit.Enabled = false;
            }
            else
            {
                picFruit.Enabled = true;
            }

            if(this.lblGreenstuffCount.Text == string.Empty)
            {
                picGreenstuff.Enabled = false;
            }
            else
            {
                picGreenstuff.Enabled = true;
            }

            if(this.lblDairyCount.Text== string.Empty)
            {
                picDairy.Enabled = false;
            }

            else
            {
                picDairy.Enabled = true;
            }

            if(this.lblEggCount.Text == string.Empty)
            {
                picEgg.Enabled = false;
            }
            else
            {
                picEgg.Enabled = true;
            }

            if(this.lblBeanProductCount.Text == string.Empty)
            {
                picBeanProduct.Enabled = false;
            }
            else
            {
                picBeanProduct.Enabled = true;
            }

            if(this.lblVegetableOilCount.Text == string.Empty)
            {
                picVegetableOil.Enabled = false;
            }
            else
            {
                picVegetableOil.Enabled = true;
            }

            if(this.lblOtherFatFoodCount.Text == string.Empty)
            {
                picOtherFatFood.Enabled = false;
            }
            else
            {
                picOtherFatFood.Enabled = true;
            }

            if(this.lblNoIntensityExercise.Text == string.Empty)
            {
                picFreeSport.Enabled = false;
            }
            else
            {
                picFreeSport.Enabled = true;
            }

            if (this.lblLowIntensityExercise.Text == string.Empty)
            {
                picLowSport.Enabled = false;
            }
            else
            {
                picLowSport.Enabled = true;
            }

            if(this.lblMiddleIntensityExercise.Text == string.Empty)
            {
                picMiddleSport.Enabled = false;
            }
            else
            {
                picMiddleSport.Enabled = true;
            }

            if(this.lblHighIntensityExercise.Text == string.Empty)
            {
                picHighSport.Enabled = false;
            }
            else
            {
                picHighSport.Enabled = true;
            }
        }

        #endregion

        private void picSuggestionDetails_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            
            string strNodeName = control.Name.Substring(3); 
            string DetailsFrmName = string.Empty;
            //add by lch 090406 �޸�BugDB00005762��������ʳ��ϸ��Ϣ���˶���Ϣ�Ĵ�������
            switch (strNodeName)
            {
                case "Cereal":
                    DetailsFrmName = "��������ϸ��Ϣ";
                    break;
                case "Fruit":
                    DetailsFrmName = "ˮ����ϸ��Ϣ";
                    break;
                case "Greenstuff":
                    DetailsFrmName = "�߲���ϸ��Ϣ";
                    break;
                case "Dairy":
                    DetailsFrmName = "����Ʒ��ϸ��Ϣ";
                    break;
                case "Egg":
                    DetailsFrmName = "������ϸ��Ϣ";
                    break;
                case "Meat":
                    DetailsFrmName = "������Ϻ��ϸ��Ϣ";
                    break;
                case "BeanProduct":
                    DetailsFrmName = "����Ʒ��ϸ��Ϣ";
                    break;
                case "VegetableOil":
                    DetailsFrmName = "ֲ������ϸ��Ϣ";
                    break;
                case "OtherFatFood":
                    DetailsFrmName = "����֬������ϸ��Ϣ";
                    break;
                case "FreeSport":
                    DetailsFrmName = "�����˶���ϸ��Ϣ";
                    break;
                case "LowSport":
                    DetailsFrmName = "����˶���ϸ��Ϣ";
                    break;
                case "MiddleSport":
                    DetailsFrmName = "�ж��˶���ϸ��Ϣ";
                    break;
                case "HighSport":
                    DetailsFrmName = "ǿ���˶���ϸ��Ϣ";
                    break;
            }

            if(strNodeName == "Cereal" || strNodeName == "Greenstuff")
            {
                //revised by lch  �޸�BugDB00005762
                ThreeMeals oThreeMeals = new ThreeMeals(strNodeName, DetailsFrmName);
                DialogResult drThreeMeals = oThreeMeals.ShowDialog();

                if (drThreeMeals == DialogResult.OK)
                {
                    this.lblCerealDetail.Text = GlobalData.DietSuggestion.CerealDetail;
                    this.lblFruitDetail.Text = GlobalData.DietSuggestion.FruitDetail;
                    this.lblGreenstuffDetail.Text = GlobalData.DietSuggestion.GreenstuffDetail;
                    this.lblDairyDetail.Text = GlobalData.DietSuggestion.DairyDetail;
                    this.lblEggDetail.Text = GlobalData.DietSuggestion.EggDetail;
                    this.lblMeatDetail.Text = GlobalData.DietSuggestion.MeatDetail;
                    this.lblBeanProductDetail.Text = GlobalData.DietSuggestion.BeanProductDetail;
                    this.lblVegetableOilDetail.Text = GlobalData.DietSuggestion.VegetableOilDetail;
                    this.lblOtherFatFoodDetail.Text = GlobalData.DietSuggestion.OtherFatFoodDetail;
                }
            }
            else
            {
                string strMealCount = string.Empty;

                switch(strNodeName)
                {
                    case "Fruit":
                        strMealCount = GlobalData.DietSuggestion.Fruitcount;
                        break;
                        case "Dairy":
                        strMealCount = GlobalData.DietSuggestion.DairyCount;
                        break;
                        case "Egg":
                        strMealCount = GlobalData.DietSuggestion.EggCount;
                        break;
                        case "Meat":
                        strMealCount = GlobalData.DietSuggestion.MeatCount;
                        break;
                        case "BeanProduct":
                        strMealCount = GlobalData.DietSuggestion.BeanProductCount;
                        break;
                        case "VegetableOil":
                        strMealCount = GlobalData.DietSuggestion.VegetableOilCount;
                        break;
                        case "OtherFatFood":
                        strMealCount = GlobalData.DietSuggestion.OtherFatFoodCount;
                        break;
                    default:
                        break;

                }
                //revised by lch  �޸�BugDB00005762
                SuggestionDetails oSuggestionDetails 
                    = new SuggestionDetails(control.Name.Substring(3),"����",strMealCount,DetailsFrmName);
                DialogResult drSuggestionDetails = oSuggestionDetails.ShowDialog();
                if (drSuggestionDetails == DialogResult.OK)
                {
                    this.lblCerealDetail.Text = GlobalData.DietSuggestion.CerealDetail;
                    this.lblFruitDetail.Text = GlobalData.DietSuggestion.FruitDetail;
                    this.lblGreenstuffDetail.Text = GlobalData.DietSuggestion.GreenstuffDetail;
                    this.lblDairyDetail.Text = GlobalData.DietSuggestion.DairyDetail;
                    this.lblEggDetail.Text = GlobalData.DietSuggestion.EggDetail;
                    this.lblMeatDetail.Text = GlobalData.DietSuggestion.MeatDetail;
                    this.lblBeanProductDetail.Text = GlobalData.DietSuggestion.BeanProductDetail;
                    this.lblVegetableOilDetail.Text = GlobalData.DietSuggestion.VegetableOilDetail;
                    this.lblOtherFatFoodDetail.Text = GlobalData.DietSuggestion.OtherFatFoodDetail;

                    this.lblNoIntensityExerciseItems.Text = GlobalData.ExerciseSuggestion.NoIntensityExerciseItems;
                    this.lblLowIntensityExerciseItems.Text = GlobalData.ExerciseSuggestion.LowIntensityExerciseItems;
                    this.lblMiddleIntensityExerciseItems.Text = GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems;
                    this.lblHighIntensityExerciseItems.Text = GlobalData.ExerciseSuggestion.HighIntensityExerciseItems;
                }
            }
            
        }

        /// <summary>
        /// toolTip����ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SuggestionDetail_MouseHover(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            switch (control.Name.ToString())
            {
                case "lblCerealDetail":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.DietSuggestion.CerealDetail);
                    break;
                case "lblFruitDetail":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.DietSuggestion.FruitDetail);
                    break;
                case "lblGreenstuffDetail":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.DietSuggestion.GreenstuffDetail);
                    break;
                case "lblDairyDetail":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.DietSuggestion.DairyDetail);
                    break;
                case "lblEggDetail":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.DietSuggestion.EggDetail);
                    break;
                case "lblMeatDetail":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.DietSuggestion.MeatDetail);
                    break;
                case "lblBeanProductDetail":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.DietSuggestion.BeanProductDetail);
                    break;
                case "lblVegetableOilDetail":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.DietSuggestion.VegetableOilDetail);
                    break;
                case "lblOtherFatFoodDetail":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.DietSuggestion.OtherFatFoodDetail);
                    break;
                case "lblNoIntensityExerciseItems":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.ExerciseSuggestion.NoIntensityExerciseItems);
                    break;
                case "lblLowIntensityExerciseItems":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.ExerciseSuggestion.LowIntensityExerciseItems);
                    break;
                case "lblMiddleIntensityExerciseItems":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems);
                    break;
                case "lblHighIntensityExerciseItems":
                    toolTip_SuggestionDetails.SetToolTip(control, GlobalData.ExerciseSuggestion.HighIntensityExerciseItems);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ��־ҳ�������Ƿ��Ѹ���
        /// </summary>
        private bool isModified = false;

        /// <summary>
        /// ����ҳ�洫�����ݸ��ı�־��
        /// </summary>
        public bool SuggestionDataModified
		{ 
            get
            {
                return  isModified;
            }
            set
            {
                isModified = value;
            }
        }
        private void DataModified(object sender, EventArgs e)
        {
            if (this.bLoaded)
            {
                isModified = true;
                RaiseDataChangedEvent(this, e);
            }
        }

        /// <summary>
        /// �����¼�֪ͨ��ҳ�������Ѹ���
        /// </summary>
        public event CustomEventHandle DataChangedEvent;
        protected void RaiseDataChangedEvent(object sender, EventArgs e)
        {
            CustomEventHandle temp = DataChangedEvent;
            if (temp != null)
                temp();
        }
    }
}