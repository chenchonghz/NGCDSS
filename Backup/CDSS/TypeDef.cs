using System;
using System.Collections.Generic;
using System.Text;

namespace CDSS
{
    public delegate void CustomEventHandle();   //用于无参的事件处理函数

    /// <summary>
    /// 查询条件
    /// </summary>
    public class QueryCondition
    {
        public string strName;
        public string strSex;
        public DateTime? dtBirthDayFrom;
        public DateTime? dtBirthDayTo;
        public DateTime? dtVisitFrom;
        public DateTime? dtVisitTo;
        public string strResult;
        public string strRiskScoreFrom;
        public string strRiskScoreTo;
        public bool bHaveDM;
        public bool bHaveLipidsDisorder;
        public bool bHaveHypertension;
        public bool bHaveHyperuricuria;
    }


    /// <summary>
    /// 打印数据 
    /// </summary>
    public class PrintDataSource
    {
        #region 一般信息
        private string m_PatName;
        public string PatName
        {
            get { return m_PatName; }
            set { m_PatName = value; }
        }

        private string m_PatSex;
        public string PatSex
        {
            get { return m_PatSex; }
            set { m_PatSex = value; }
        }

        private string m_PatAge;
        public string PatAge
        {
            get { return m_PatAge; }
            set { m_PatAge = value; }
        }

        private string m_PatFrom;
        public string PatFrom
        {
            get { return m_PatFrom; }
            set { m_PatFrom = value; }
        }

        private string m_Doctor;
        public string Doctor
        {
            get { return m_Doctor; }
            set { m_Doctor = value; }
        }

        private string m_OpDate;
        public string OpDate
        {
            get { return m_OpDate; }
            set { m_OpDate = value; }
        }

#endregion


        #region 诊断结论
        //糖代谢
        private string m_TdxSummary;
        public string TdxSummary
        {
            get { return m_TdxSummary; }
            set { m_TdxSummary = value; }
        }

        private string m_TdxResult;
        public string TdxResult
        {
            get { return m_TdxResult; }
            set { m_TdxResult = value; }
        }

        private string m_TdxTreatmentTarget;
        public string TdxTreatmentTarget
        {
            get { return m_TdxTreatmentTarget; }
            set { m_TdxTreatmentTarget = value; }
        }

        private string m_TdxTreatmentSuggestion;
        public string TdxTreatmentSuggestion
        {
            get { return m_TdxTreatmentSuggestion; }
            set { m_TdxTreatmentSuggestion = value; }
        }

        private string m_TdxSelfCheck;
        public string TdxSelfCheck
        {
            get { return m_TdxSelfCheck; }
            set { m_TdxSelfCheck = value; }
        }

        private string m_TdxLife;
        public string TdxLife
        {
            get { return m_TdxLife; }
            set { m_TdxLife = value; }
        }

        private string m_TdxHistory;
        public string TdxHistory
        {
            get { return m_TdxHistory; }
            set { m_TdxHistory = value; }
        }

        private string m_TdxLabTest;
        public string TdxLabTest
        {
            get { return m_TdxLabTest; }
            set { m_TdxLabTest = value; }
        }

        //脂代谢
        private string m_ZdxSummary;
        public string ZdxSummary
        {
            get { return m_ZdxSummary; }
            set { m_ZdxSummary = value; }
        }

        private string m_ZdxResult;
        public string ZdxResult
        {
            get { return m_ZdxResult; }
            set { m_ZdxResult = value; }
        }

        private string m_ZdxTreatmentTarget;
        public string ZdxTreatmentTarget
        {
            get { return m_ZdxTreatmentTarget; }
            set { m_ZdxTreatmentTarget = value; }
        }

        private string m_ZdxTreatmentSuggestion;
        public string ZdxTreatmentSuggestion
        {
            get { return m_ZdxTreatmentSuggestion; }
            set { m_ZdxTreatmentSuggestion = value; }
        }

        private string m_ZdxSelfCheck;
        public string ZdxSelfCheck
        {
            get { return m_ZdxSelfCheck; }
            set { m_ZdxSelfCheck = value; }
        }

        private string m_ZdxLife;
        public string ZdxLife
        {
            get { return m_ZdxLife; }
            set { m_ZdxLife = value; }
        }

        private string m_ZdxHistory;
        public string ZdxHistory
        {
            get { return m_ZdxHistory; }
            set { m_ZdxHistory = value; }
        }

        private string m_ZdxLabTest;
        public string ZdxLabTest
        {
            get { return m_ZdxLabTest; }
            set { m_ZdxLabTest = value; }
        }

        //血压
        private string m_XySummary;
        public string XySummary
        {
            get { return m_XySummary; }
            set { m_XySummary = value; }
        }

        private string m_XyResult;
        public string XyResult
        {
            get { return m_XyResult; }
            set { m_XyResult = value; }
        }

        private string m_XyTreatmentTarget;
        public string XyTreatmentTarget
        {
            get { return m_XyTreatmentTarget; }
            set { m_XyTreatmentTarget = value; }
        }

        private string m_XyTreatmentSuggestion;
        public string XyTreatmentSuggestion
        {
            get { return m_XyTreatmentSuggestion; }
            set { m_XyTreatmentSuggestion = value; }
        }

        private string m_XySelfCheck;
        public string XySelfCheck
        {
            get { return m_XySelfCheck; }
            set { m_XySelfCheck = value; }
        }

        private string m_XyLife;
        public string XyLife
        {
            get { return m_XyLife; }
            set { m_XyLife = value; }
        }

        private string m_XyHistory;
        public string XyHistory
        {
            get { return m_XyHistory; }
            set { m_XyHistory = value; }
        }

        private string m_XyLabTest;
        public string XyLabTest
        {
            get { return m_XyLabTest; }
            set { m_XyLabTest = value; }
        }

        //血尿酸
        private string m_XnsSummary;
        public string XnsSummary
        {
            get { return m_XnsSummary; }
            set { m_XnsSummary = value; }
        }

        private string m_XnsResult;
        public string XnsResult
        {
            get { return m_XnsResult; }
            set { m_XnsResult = value; }
        }

        private string m_XnsTreatmentTarget;
        public string XnsTreatmentTarget
        {
            get { return m_XnsTreatmentTarget; }
            set { m_XnsTreatmentTarget = value; }
        }

        private string m_XnsTreatmentSuggestion;
        public string XnsTreatmentSuggestion
        {
            get { return m_XnsTreatmentSuggestion; }
            set { m_XnsTreatmentSuggestion = value; }
        }

        private string m_XnsSelfCheck;
        public string XnsSelfCheck
        {
            get { return m_XnsSelfCheck; }
            set { m_XnsSelfCheck = value; }
        }

        private string m_XnsLife;
        public string XnsLife
        {
            get { return m_XnsLife; }
            set { m_XnsLife = value; }
        }

        private string m_XnsHistory;
        public string XnsHistory
        {
            get { return m_XnsHistory; }
            set { m_XnsHistory = value; }
        }

        private string m_XnsLabTest;
        public string XnsLabTest
        {
            get { return m_XnsLabTest; }
            set { m_XnsLabTest = value; }
        }

        //肥胖度
        private string m_FpdSummary;
        public string FpdSummary
        {
            get { return m_FpdSummary; }
            set { m_FpdSummary = value; }
        }

        private string m_FpdResult;
        public string FpdResult
        {
            get { return m_FpdResult; }
            set { m_FpdResult = value; }
        }

        private string m_FpdTreatmentTarget;
        public string FpdTreatmentTarget
        {
            get { return m_FpdTreatmentTarget; }
            set { m_FpdTreatmentTarget = value; }
        }

        private string m_FpdTreatmentSuggestion;
        public string FpdTreatmentSuggestion
        {
            get { return m_FpdTreatmentSuggestion; }
            set { m_FpdTreatmentSuggestion = value; }
        }

        private string m_FpdSelfCheck;
        public string FpdSelfCheck
        {
            get { return m_FpdSelfCheck; }
            set { m_FpdSelfCheck = value; }
        }

        private string m_FpdLife;
        public string FpdLife
        {
            get { return m_FpdLife; }
            set { m_FpdLife = value; }
        }

        private string m_FpdHistory;
        public string FpdHistory
        {
            get { return m_FpdHistory; }
            set { m_FpdHistory = value; }
        }

        private string m_FpdLabTest;
        public string FpdLabTest
        {
            get { return m_FpdLabTest; }
            set { m_FpdLabTest = value; }
        }

        //蛋白尿
        private string m_DbnSummary;
        public string DbnSummary
        {
            get { return m_DbnSummary; }
            set { m_DbnSummary = value; }
        }

        private string m_DbnResult;
        public string DbnResult
        {
            get { return m_DbnResult; }
            set { m_DbnResult = value; }
        }

        private string m_DbnTreatmentTarget;
        public string DbnTreatmentTarget
        {
            get { return m_DbnTreatmentTarget; }
            set { m_DbnTreatmentTarget = value; }
        }

        private string m_DbnTreatmentSuggestion;
        public string DbnTreatmentSuggestion
        {
            get { return m_DbnTreatmentSuggestion; }
            set { m_DbnTreatmentSuggestion = value; }
        }

        private string m_DbnSelfCheck;
        public string DbnSelfCheck
        {
            get { return m_DbnSelfCheck; }
            set { m_DbnSelfCheck = value; }
        }

        private string m_DbnLife;
        public string DbnLife
        {
            get { return m_TdxLife; }
            set { m_TdxLife = value; }
        }

        private string m_DbnHistory;
        public string DbnHistory
        {
            get { return m_DbnHistory; }
            set { m_DbnHistory = value; }
        }

        private string m_DbnLabTest;
        public string DbnLabTest
        {
            get { return m_DbnLabTest; }
            set { m_DbnLabTest = value; }
        }

        //其他
        private string m_OtherSummary;
        public string OtherSummary
        {
            get { return m_OtherSummary; }
            set { m_OtherSummary = value; }
        }

        private string m_OtherResult;
        public string OtherResult
        {
            get { return m_OtherResult; }
            set { m_OtherResult = value; }
        }

        private string m_OtherTreatmentTarget;
        public string OtherTreatmentTarget
        {
            get { return m_OtherTreatmentTarget; }
            set { m_OtherTreatmentTarget = value; }
        }

        private string m_OtherTreatmentSuggestion;
        public string OtherTreatmentSuggestion
        {
            get { return m_OtherTreatmentSuggestion; }
            set { m_OtherTreatmentSuggestion = value; }
        }

        private string m_OtherSelfCheck;
        public string OtherSelfCheck
        {
            get { return m_OtherSelfCheck; }
            set { m_OtherSelfCheck = value; }
        }

        private string m_OtherLife;
        public string OtherLife
        {
            get { return m_OtherLife; }
            set { m_OtherLife = value; }
        }

        private string m_OtherHistory;
        public string OhterHistory
        {
            get { return m_OtherHistory; }
            set { m_OtherHistory = value; }
        }

        private string m_OtherLabTest;
        public string OtherLabTest
        {
            get { return m_OtherLabTest; }
            set { m_OtherLabTest = value; }
        }

#endregion

        #region  膳食处方
        // 膳食类别
        private string m_DietType;
        public string DietType
        {
            get { return m_DietType; }
            set { m_DietType = value; }
        }

        //总热量
        private string m_TotalEnergy;
        public string TotalEnergy
        {
            get { return m_TotalEnergy; }
            set { m_TotalEnergy = value; }
        }

        //饮水量
        private string m_TotalWater;
        public string TotalWater
        {
            get { return m_TotalWater; }
            set { m_TotalWater = value; }
        }

        //糖类所占百分比
        private string m_CarboPercent;
        public string CarboPercent
        {
            get { return m_CarboPercent; }
            set { m_CarboPercent = value; }
        }

        //糖类食物份数
        private string m_CarboCount;
        public string CarboCount
        {
            get { return m_CarboCount; }
            set { m_CarboCount = value; }
        }

        //谷薯份数
        private string m_CerealCount;
        public string CerealCount
        {
            get { return m_CerealCount; }
            set { m_CerealCount = value; }
        }

        //水果份数
        private string m_Fruitcount;
        public string Fruitcount
        {
            get { return m_Fruitcount; }
            set { m_Fruitcount = value; }
        }

        //蔬菜份数
        private string m_GreenstuffCount;
        public string GreenstuffCount
        {
            get { return m_GreenstuffCount; }
            set { m_GreenstuffCount = value; }
        }

        //蛋白质类所占百分比
        private string m_ProteinPercent;
        public string ProteinPercent
        {
            get { return m_ProteinPercent; }
            set { m_ProteinPercent = value; }
        }

        //蛋白质类食物份数
        private string m_ProteinCount;
        public string ProteinCount
        {
            get { return m_ProteinCount; }
            set { m_ProteinCount = value; }
        }

        //奶制品份数
        private string m_DairyCount;
        public string DairyCount
        {
            get { return m_DairyCount; }
            set { m_DairyCount = value; }
        }

        //蛋类份数
        private string m_EggCount;
        public string EggCount
        {
            get { return m_EggCount; }
            set { m_EggCount = value; }
        }

        //畜禽鱼虾份数
        private string m_MeatCount;
        public string MeatCount
        {
            get { return m_MeatCount; }
            set { m_MeatCount = value; }
        }

        //豆制品份数
        private string m_BeanProductCount;
        public string BeanProductCount
        {
            get { return m_BeanProductCount; }
            set { m_BeanProductCount = value; }
        }

        //脂肪类所占百分比
        private string m_FatPercent;
        public string FatPercent
        {
            get { return m_FatPercent; }
            set { m_FatPercent = value; }
        }

        //脂肪类食物份数
        private string m_FatCount;
        public string FatCount
        {
            get { return m_FatCount; }
            set { m_FatCount = value; }
        }

        //植物油份数
        private string m_VegetableOilCount;
        public string VegetableOilCount
        {
            get { return m_VegetableOilCount; }
            set { m_VegetableOilCount = value; }
        }

        //其他脂肪类食物份数
        private string m_OtherFatFoodCount;
        public string OtherFatFoodCount
        {
            get { return m_OtherFatFoodCount; }
            set { m_OtherFatFoodCount = value; }
        }

        //限量食品
        private string m_LimitedFood;
        public string LimitedFood
        {
            get { return m_LimitedFood; }
            set { m_LimitedFood = value; }
        }

        //忌食食品
        private string m_AvoidFood;
        public string AvoidFood
        {
            get { return m_AvoidFood; }
            set { m_AvoidFood = value; }
        }

#endregion

        #region  运动建议
        private string m_ExAim;
        public string ExAim
        {
            get { return m_ExAim; }
            set { m_ExAim = value; }
        }

        private string m_ExEnergyConsum;
        public string ExEnergyConsum
        {
            get { return m_ExEnergyConsum; }
            set { m_ExEnergyConsum = value; }
        }

        private string m_ExPlan1;
        public string ExPlan1
        {
            get { return m_ExPlan1; }
            set { m_ExPlan1 = value; }
        }

        private string m_ExChoice1;
        public string ExChoice1
        {
            get { return m_ExChoice1; }
            set { m_ExChoice1 = value; }
        }

        private string m_ExPlan2;
        public string ExPlan2
        {
            get { return m_ExPlan2; }
            set { m_ExPlan2 = value; }
        }

        private string m_ExChoice2;
        public string ExChoice2
        {
            get { return m_ExChoice2; }
            set { m_ExChoice2 = value; }
        }

        private string m_ExPlan3;
        public string ExPlan3
        {
            get { return m_ExPlan3; }
            set { m_ExPlan3 = value; }
        }

        private string m_ExChoice3;
        public string ExChoice3
        {
            get { return m_ExChoice3; }
            set { m_ExChoice3 = value; }
        }

        private string m_ExPlan4;
        public string ExPlan4
        {
            get { return m_ExPlan4; }
            set { m_ExPlan4 = value; }
        }

        private string m_ExChoice4;
        public string ExChoice4
        {
            get { return m_ExChoice4; }
            set { m_ExChoice4 = value; }
        }

#endregion

    }
}
