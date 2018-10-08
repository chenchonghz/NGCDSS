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
    public partial class PerHistory : InfoFormBaseClass
    {
        public bool bLoaded = false;    //标记数据是否已经加载过
        public bool bFetusFlag = false; //记录进入页面的巨大儿生产史初始状态， 
                                        //若初始状态为“是”，不作处理；若为“否”，进行判断 add by wbf 081229 修复bug B100
        public bool bClearData = false; ////add by wbf 081229 修复bug B101 清空数据标志，清空数据时不响应巨大儿判断 

        #region 系统事件
        public PerHistory()
        {
            InitializeComponent();
           // radioButton_SmokehisN.Checked = true;
        }

        private void radioButton_SmokehisN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (radioButton_SmokehisY.Checked)
                panel_SmokeDetail.Visible = true;
            else
                panel_SmokeDetail.Visible = false;
        }

        private void checkBox_FertilityUnrelated_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (checkBox_FertilityUnrelated.Checked)
            {
                rbt_FertilityY.Checked = false;
                panel_FertilityYN.Enabled = false;
                panel_FertilityDetail.Enabled = false;
                panel_GDM.Enabled = false;
            }
            else
            {
                panel_FertilityYN.Enabled = true;
                if (rbt_FertilityY.Checked)
                {
                    panel_GDM.Enabled = true;
                    panel_FertilityDetail.Visible = true;
                    panel_FertilityDetail.Enabled = true;

                }
                //panel_FertilityDetail.Visible = true;
              
            }
        }

        private void rbt_DrinkN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_DrinkY.Checked)
                panel_DrinkDetail.Visible = true;
            else
                panel_DrinkDetail.Visible = false;
        }

        private void rbt_FertilityN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_FertilityY.Checked)
            {
                panel_GDM.Enabled = true;
                panel_FertilityDetail.Visible = true;
                panel_FertilityDetail.Enabled = true;
            }
            else
            {
                panel_GDM.Enabled= false;
                panel_FertilityDetail.Visible = false;
            }
        }

        private void rbt_GDMN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_GDMN.Checked)
                txb_GDMAge.Enabled = false;
            else
                txb_GDMAge.Enabled = true;
        }

        private void rbt_ExerciseN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_ExerciseY.Checked)
                panel_ExerciseDetail.Visible = true;
            else
                panel_ExerciseDetail.Visible = false;
        }

      
        private void PerHistory_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {

                if (!PatInfo.bNewPatient && !bLoaded)
                {
                    LoadDataFromVarToUI(); //在窗体显示出来的时候加载数据
                }
                bFetusFlag = rbt_FetusL4Y.Checked;//add by wbf 081229 修复bug B100
            }
            else
            {
                bFetusFlag = rbt_FetusL4Y.Checked;//add by wbf 081229 修复bug B100
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

        /// <summary>
        /// add by wbf 081225
        /// 修复bug B97 巨大婴儿产生史的判断有误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb_FetusWeight1_DataChanged(object sender, EventArgs e)
        {
            if (!bClearData)//add by wbf 081229 修复bug B101
            {
                DataModified(sender, e);
                FetusWeightJudge();
            }
        }

        /// <summary>
        /// add by wbf 081225
        /// 修复bug B97 巨大婴儿产生史的判断有误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb_FetusWeight2_DataChanged(object sender, EventArgs e)
        {
            if (!bClearData)//add by wbf 081229 修复bug B101
            {
                DataModified(sender, e);
                FetusWeightJudge();
            }
        }


        /// <summary>
        /// add by wbf 081229 修复bug B101
        /// 巨大儿生产史医生手动点击rdb响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbt_FetusL4N_Click(object sender, EventArgs e)
        {
            if (!bClearData)
            {
                double dFetusWeight1 = 0;
                double dFetusWeight2 = 0;
                if (txb_FetusWeight1.Text != String.Empty)
                {
                    dFetusWeight1 = Convert.ToDouble(txb_FetusWeight1.Text.ToString());
                }

                if (txb_FetusWeight2.Text != String.Empty)
                {
                    dFetusWeight2 = Convert.ToDouble(txb_FetusWeight2.Text.ToString());
                }
                if (dFetusWeight1 >= 4 || dFetusWeight2 >= 4)
                {
                    if (rbt_FetusL4Y.Checked == false)
                    {
                        DialogResult dr = MessageBox.Show("当前录入胎儿值大于4kg,请确认是否有巨大胎儿生产史？", "提示信息",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.No)
                        {
                            rbt_FetusL4Y.Checked = false;
                            rbt_FetusL4N.Checked = true;
                        }
                        else
                        {
                            rbt_FetusL4Y.Checked = true;
                            rbt_FetusL4N.Checked = false;
                        }
                    }
                }
            }
        }

        #endregion

        #region 功能函数
        public override void LoadDataFromVarToUI()
        {
            this.txb_MaxWeight.Text = GlobalData.PersonalHistoryInfo.MaxWeight;
            this.txb_MinWeight.Text = GlobalData.PersonalHistoryInfo.MinWeight;
            this.txb_MaxWeightYears.Text = GlobalData.PersonalHistoryInfo.MaxWeightLastedYears;
            this.txb_MaxWeightAge.Text = GlobalData.PersonalHistoryInfo.MaxWeightAge;
            //吸烟史
            if (GlobalData.PersonalHistoryInfo.IsSmokeing)
            {
                this.radioButton_SmokehisY.Checked = true;
                this.txb_SmokeBeginAge.Text = GlobalData.PersonalHistoryInfo.SmokingAgeBegin;
                this.txb_SmokeEndAge.Text = GlobalData.PersonalHistoryInfo.SmokingAgeEnd;
                //吸烟量范围
                string tmp=GlobalData.PersonalHistoryInfo.SmokingFrequency;
                this.txb_SmokeAmountLeft.Text  = tmp.Substring(0, tmp.IndexOf('-'));
                this.txb_SmokeAmountRight.Text = tmp.Substring(tmp.IndexOf('-') + 1);
                this.txb_SmokeAmount.Text = GlobalData.PersonalHistoryInfo.RecentSmokingFrequency;
            }
            else
            {
                this.radioButton_SmokehisY.Checked = false;
            }
            //饮酒史
            if (GlobalData.PersonalHistoryInfo.IsDrinking)
            {
                this.rbt_DrinkY.Checked = true;
                this.txb_DrinkBeginAge.Text = GlobalData.PersonalHistoryInfo.DrinkingAgeBegin;
                this.txb_DrinkEndAge.Text = GlobalData.PersonalHistoryInfo.DrinkingAgeEnd;
            }
            else
            {
                this.rbt_DrinkY.Checked = false;
            }

            //饮酒详细情况,LoadDataFromVarToUI()

            if(GlobalData.PersonalHistoryInfo.listDrinkingInfo.Count>0)
            {
                for (int i = 0; i < GlobalData.PersonalHistoryInfo.listDrinkingInfo.Count; i++)
                {
                    if (i == 0)
                    {
                        cmb_DrinkType1.Text = GlobalData.PersonalHistoryInfo.listDrinkingInfo[0].DrinkingType.ToString().Trim();
                        txb_DrinkTimes1.Text = GlobalData.PersonalHistoryInfo.listDrinkingInfo[0].TimesOneWeek.ToString().Trim();
                        txb_DrinkAmount1.Text = GlobalData.PersonalHistoryInfo.listDrinkingInfo[0].AmountOneTime.ToString().Trim();
                    }
                    else if (i == 1)
                    {
                        cmb_DrinkType2.Text = GlobalData.PersonalHistoryInfo.listDrinkingInfo[1].DrinkingType.ToString().Trim();
                        txb_DrinkTimes2.Text = GlobalData.PersonalHistoryInfo.listDrinkingInfo[1].TimesOneWeek.ToString().Trim();
                        txb_DrinkAmount2.Text = GlobalData.PersonalHistoryInfo.listDrinkingInfo[1].AmountOneTime.ToString().Trim();
                    }
                }
            }

            //饮食控制
            if (GlobalData.PersonalHistoryInfo.HasControlDiet)
            {
                this.rbt_DietY.Checked = true;
                this.rbt_DietN.Checked = false;
            }
            else
            {
                this.rbt_DietY.Checked = false;
                this.rbt_DietN.Checked = true;
            }

            //每日主食
            this.txb_MainFood.Text = GlobalData.PersonalHistoryInfo.MainFoodAmount;
            //每日植物油数量
            this.txb_OilAmount.Text = GlobalData.PersonalHistoryInfo.OilAmount;
            //每日蛋白质量
            this.txb_ProteinAmount.Text = GlobalData.PersonalHistoryInfo.ProteinAmount;

            //生育情况
            if (GlobalData.PersonalHistoryInfo.HasBearing)
            {
                this.checkBox_FertilityUnrelated.Checked = false;
                this.rbt_FertilityY.Checked = true;
               
                //妊娠糖尿病
                if (GlobalData.PersonalHistoryInfo.HasGDM)
                {
                    rbt_GDMY.Checked = true;
                    txb_GDMAge.Text = GlobalData.PersonalHistoryInfo.GDMAgeBegin;
                }
                else
                {
                    rbt_GDMN.Checked =true ;
                }

                //胎儿>=4kg
                if (GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg)
                {
                    rbt_FetusL4Y.Checked = true;
                }
                else
                {
                    rbt_FetusL4N.Checked =true ;
                }
                this.txb_FetusNum.Text = GlobalData.PersonalHistoryInfo.NeonateCount;
                this.txb_BirthAge1.Text = GlobalData.PersonalHistoryInfo.BearingAge1;
                this.txb_BirthAge2.Text = GlobalData.PersonalHistoryInfo.BearingAge2;
                this.txb_FetusWeight1.Text = GlobalData.PersonalHistoryInfo.NeonateWeight1;
                this.txb_FetusWeight2.Text = GlobalData.PersonalHistoryInfo.NeonateWeight2;                
            }
            else
            {
                this.checkBox_FertilityUnrelated.Checked = true ;
                this.rbt_FertilityY.Checked = false;
                this.rbt_FertilityN.Checked = true;
            }

            //近期运动
            if (GlobalData.PersonalHistoryInfo.listExerciseInfo.Count > 0)
            {
                this.rbt_ExerciseY.Checked = true;

                for (int i = 0; i < GlobalData.PersonalHistoryInfo.listExerciseInfo.Count; i++)
                {
                    if (i == 0)
                    {
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[0].ExerciseType.Trim()!="")
                        {
                            this.cmb_ExerciseType1.Text = GlobalData.PersonalHistoryInfo.listExerciseInfo[0].ExerciseType.Trim();
                        }
                        else
                            this.cmb_ExerciseType1.SelectedIndex = -1;

                        this.txb_ExerciseDays1.Text = GlobalData.PersonalHistoryInfo.listExerciseInfo[0].DaysOneWeek;
                        this.txb_ExerciseHours1.Text = GlobalData.PersonalHistoryInfo.listExerciseInfo[0].LastedHourOneDay;
                    }
                    if (i == 1)
                    {
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[1].ExerciseType.Trim()!= "")
                        {
                            this.cmb_ExerciseType2.Text = GlobalData.PersonalHistoryInfo.listExerciseInfo[1].ExerciseType.Trim();
                        }
                        else
                            this.cmb_ExerciseType2.SelectedIndex = -1;

                        this.txb_ExerciseDays2.Text = GlobalData.PersonalHistoryInfo.listExerciseInfo[1].DaysOneWeek;
                        this.txb_ExerciseHours2.Text = GlobalData.PersonalHistoryInfo.listExerciseInfo[1].LastedHourOneDay;
                    }
                }
            }
            else
            {
                //没有指明具体近期运动，但选择了有近期运动，Revised By ZQY, 090327
                if (GlobalData.PersonalHistoryInfo.HasExerciseRecent)
                {
                    this.rbt_ExerciseY.Checked = true;
                    this.rbt_ExerciseN.Checked = false;
                }
                else
                {
                    this.rbt_ExerciseY.Checked = false;
                    this.rbt_ExerciseN.Checked = true;
                }
            }
            //设置数据已加载标志
            this.bLoaded = true;
        }

        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return;
   
            GlobalData.PersonalHistoryInfo.Clear();

            GlobalData.PersonalHistoryInfo.MaxWeight=this.txb_MaxWeight.Text  ;
            GlobalData.PersonalHistoryInfo.MinWeight = this.txb_MinWeight.Text;
            GlobalData.PersonalHistoryInfo.MaxWeightLastedYears=this.txb_MaxWeightYears.Text  ;
            GlobalData.PersonalHistoryInfo.MaxWeightAge =this.txb_MaxWeightAge.Text ;
            
            //吸烟史
            if (radioButton_SmokehisY.Checked)
            {
                GlobalData.PersonalHistoryInfo.IsSmokeing = true;
                GlobalData.PersonalHistoryInfo.SmokingAgeBegin =this.txb_SmokeBeginAge.Text ;
                GlobalData.PersonalHistoryInfo.SmokingAgeEnd = this.txb_SmokeEndAge.Text;
                //吸烟量范围
                string tmp = this.txb_SmokeAmountLeft .Text .Trim ()+'-'+this.txb_SmokeAmountRight.Text .Trim ();
                GlobalData.PersonalHistoryInfo.SmokingFrequency = tmp;
                GlobalData.PersonalHistoryInfo.RecentSmokingFrequency=this.txb_SmokeAmount.Text  ;
            }
            else 
            {
                GlobalData.PersonalHistoryInfo.IsSmokeing = false ;
            }
            //饮酒史
            if (rbt_DrinkY.Checked)
            {
                GlobalData.PersonalHistoryInfo.IsDrinking = true;
                GlobalData.PersonalHistoryInfo.DrinkingAgeBegin =txb_DrinkBeginAge.Text ;
                GlobalData.PersonalHistoryInfo.DrinkingAgeEnd =this.txb_DrinkEndAge.Text  ;

                //饮酒详细情况,LoadDataFromUIToVar()
                if (cmb_DrinkType1.Text != "")
                {
                    CDSSDrinkingInfo DrinkingInfo = new CDSSDrinkingInfo();
                    DrinkingInfo.DrinkingType=cmb_DrinkType1.Text;
                    DrinkingInfo.TimesOneWeek = txb_DrinkTimes1.Text;
                    DrinkingInfo.AmountOneTime = txb_DrinkAmount1.Text;

                    GlobalData.PersonalHistoryInfo.listDrinkingInfo.Add(DrinkingInfo);
                }
                if (cmb_DrinkType2.Text != "")
                {
                    CDSSDrinkingInfo DrinkingInfo = new CDSSDrinkingInfo();
                    DrinkingInfo.DrinkingType = cmb_DrinkType2.Text;
                    DrinkingInfo.TimesOneWeek = txb_DrinkTimes2.Text;
                    DrinkingInfo.AmountOneTime = txb_DrinkAmount2.Text;

                    GlobalData.PersonalHistoryInfo.listDrinkingInfo.Add(DrinkingInfo);
                }
            }
            else
            {
                GlobalData.PersonalHistoryInfo.IsDrinking = false;
            }

            //饮食控制
            if (this.rbt_DietY.Checked)
            {
                GlobalData.PersonalHistoryInfo.HasControlDiet = true;
            }
            else
            {
                GlobalData.PersonalHistoryInfo.HasControlDiet = false;
            }

            //每日主食
            GlobalData.PersonalHistoryInfo.MainFoodAmount = txb_MainFood.Text;
            //每日植物油数量
            GlobalData.PersonalHistoryInfo.OilAmount = txb_OilAmount.Text;
            //每日蛋白量
            GlobalData.PersonalHistoryInfo.ProteinAmount = txb_ProteinAmount.Text;

            //生育情况
            if (rbt_FertilityY.Checked)
            {
                GlobalData.PersonalHistoryInfo.HasBearing = true;

                //妊娠糖尿病
                if (rbt_GDMY.Checked)
                {
                    GlobalData.PersonalHistoryInfo.HasGDM = true;
                    GlobalData.PersonalHistoryInfo.GDMAgeBegin = this.txb_GDMAge.Text;
                }
                else
                {
                    GlobalData.PersonalHistoryInfo.HasGDM = false;
                }

                //胎儿>=4kg
                if (rbt_FetusL4Y.Checked)
                {
                    GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg = true;
                }
                else
                {
                    GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg = false;
                }

                GlobalData.PersonalHistoryInfo.NeonateCount=this.txb_FetusNum.Text  ;
                GlobalData.PersonalHistoryInfo.BearingAge1=this.txb_BirthAge1.Text  ;
                GlobalData.PersonalHistoryInfo.BearingAge2=this.txb_BirthAge2.Text  ;
                GlobalData.PersonalHistoryInfo.NeonateWeight1 =this.txb_FetusWeight1.Text ;
                GlobalData.PersonalHistoryInfo.NeonateWeight2 =this.txb_FetusWeight2.Text ;   
            }
            else
            {
                GlobalData.PersonalHistoryInfo.HasBearing = false;
            }

            //近期运动
            if (rbt_ExerciseY.Checked)
            {
                GlobalData.PersonalHistoryInfo.HasExerciseRecent = true;
                if(cmb_ExerciseType1.Text!="")
                {
                    CDSSExerciseInfo Exercise1 = new CDSSExerciseInfo();
                    Exercise1.ExerciseType =this.cmb_ExerciseType1.Text;
                    Exercise1.DaysOneWeek =this.txb_ExerciseDays1.Text;
                    Exercise1.LastedHourOneDay =this.txb_ExerciseHours1.Text;
                    GlobalData.PersonalHistoryInfo.listExerciseInfo .Add (Exercise1);
                }
                if(cmb_ExerciseType2.Text!="")
                {
                    CDSSExerciseInfo Exercise2 = new CDSSExerciseInfo();
                    Exercise2.ExerciseType =this.cmb_ExerciseType2.Text;
                    Exercise2.DaysOneWeek =this.txb_ExerciseDays2.Text;
                    Exercise2.LastedHourOneDay =this.txb_ExerciseHours2.Text;
                    GlobalData.PersonalHistoryInfo.listExerciseInfo .Add (Exercise2);
                }
            }
            IsModified = false;
        }

        /**************************************************************************
        * 添加人：XY；
        * 添加时间：20081221；
        * 添加说明：数据必填项是否输入完整判断及提示功能；
        * 添加部分：添加“判断必选项是否为空函数”。
        ***************************************************************************/
        /// <summary>
        /// 判断必选项是否为空函数
        /// </summary>
        /// <returns></returns>
        public bool ForbidNon()
        {
            if (!PatInfo.bMustFill)
            {
                return false;
            }
            else
            {
                LoadDataFromUIToVar();
                if (txb_MaxWeight.Text == "" || txb_MinWeight.Text == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 清空页面内容
        /// </summary>
        public void ClearData()
        {
            bFetusFlag = rbt_FetusL4Y.Checked;//add by wbf 081229 修复bug B100
            bClearData = true; //add by wbf 081229 修复bug B101 设置清空标志
            foreach (System.Windows.Forms.Control count in this.panel_info.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
                if (count is CDSSCtrlLib.TextBoxNumControl)
                    count.Text = String.Empty;
            }
            foreach (System.Windows.Forms.Control count in this.panel_SmokeDetail.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
            }
            foreach (System.Windows.Forms.Control count in this.panel_DrinkDetail.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
            }
            foreach (System.Windows.Forms.Control count in this.tableLayoutPanel1.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
                if (count is ComboBox)
                    count.Text = String.Empty;
            }
            foreach (System.Windows.Forms.Control count in this.panel_FertilityDetail.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
                if (count is CDSSCtrlLib.TextBoxNumControl)
                    count.Text = String.Empty;
            }
            foreach (System.Windows.Forms.Control count in this.tableLayoutPanel2.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
                if (count is ComboBox)                        //revise by Jyl,081225,清空tablelayoutPanel2中的Combox控件
                {
                    ComboBox cb = (ComboBox)count;
                    cb.SelectedIndex = -1;
                }
                    //count.Text = String.Empty;
            }
            txb_GDMAge.Text = String.Empty;
            radioButton_SmokehisN.Checked = true;
            rbt_DrinkN.Checked = true;
            checkBox_FertilityUnrelated.Checked = true;
            rbt_FertilityN.Checked = true;
            rbt_GDMN.Checked = true;
            rbt_FetusL4N.Checked = true;
            //revise by lch 090331 设置近期运动默认为无
            rbt_ExerciseN.Checked = true;
            panel_ExerciseDetail.Visible = false;

            this.txb_MainFood.Text = String.Empty;
            this.cmb_MainFoodType.Text = String.Empty;
            this.txb_OilAmount.Text = String.Empty;
            this.cmb_DietType.Text = String.Empty;
            this.cmb_ProteinType.Text = String.Empty;
            this.txb_ProteinAmount.Text = String.Empty;

            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
            bClearData = false; // //add by wbf 081229 修复bug B101 清空完毕，初始化清空标志
        }

        /// <summary>
        /// add by wbf 081225
        /// 修复bug B97 巨大婴儿产生史的判断有误
        /// </summary>
        private void FetusWeightJudge()
        {
            double dFetusWeight1 = 0;
            double dFetusWeight2 = 0;
            if (!bFetusFlag)//add by wbf 081229修复bug B100
            {
                if (txb_FetusWeight1.Text != String.Empty)
                {
                    //revised by wbf 081229 修复B99
                    if (txb_FetusWeight1.Text.ToString().Split(new char[] { '.' }).Length == 2)
                    {
                        string[] strText = txb_FetusWeight1.Text.ToString().Split(new char[] { '.' });
                        int lthSplText0 = strText[0].Length;
                        int lthSplText1 = strText[1].Length;
                        if (lthSplText0 >= 1 && lthSplText1 == 1)
                        {
                            dFetusWeight1 = Convert.ToDouble(txb_FetusWeight1.Text.ToString());
                        }
                        else
                        {
                            return;
                        }

                    }
                    else
                    {
                        return;
                    }
                }

                if (txb_FetusWeight2.Text != String.Empty)
                {
                    //revised by wbf 081229 修复B99
                    if (txb_FetusWeight2.Text.ToString().Split(new char[] { '.' }).Length == 2)
                    {
                        string[] strText = txb_FetusWeight2.Text.ToString().Split(new char[] { '.' });
                        int lthSplText0 = strText[0].Length;
                        int lthSplText1 = strText[1].Length;
                        if (lthSplText0 >= 1 && lthSplText1 == 1)
                        {
                            dFetusWeight2 = Convert.ToDouble(txb_FetusWeight2.Text.ToString());
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                if (dFetusWeight1 >= 4 || dFetusWeight2 >= 4)
                {
                    if (rbt_FetusL4Y.Checked == false)
                    {
                        rbt_FetusL4Y.Checked = true;
                        rbt_FetusL4N.Checked = false;
                    }
                    else
                    {

                    }

                }
                else
                {
                    if (rbt_FetusL4Y.Checked == true)
                    {
                        DialogResult dr = MessageBox.Show("当前录入胎儿值均小于4kg,请确认是否有巨大胎儿生产史？", "提示信息",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.No)
                        {
                            rbt_FetusL4Y.Checked = false;
                            rbt_FetusL4N.Checked = true;
                        }
                    }
                }
            }
        }

        #endregion        
    }
}