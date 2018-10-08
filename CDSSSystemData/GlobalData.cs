using System;
using System.Collections.Generic;
using System.Text;

namespace CDSSSystemData
{
    public static class GlobalData
    {
        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public static CDSSUserInfo UserInfo = new CDSSUserInfo();
        /// <summary>
        /// 病人基本信息
        /// </summary>
        public static CDSSPatBasicInfo PatBasicInfo = new CDSSPatBasicInfo();
        /// <summary>
        /// 糖代谢异常信息
        /// </summary>
        public static CDSSAGMInfo AGMInfo = new CDSSAGMInfo();
        /// <summary>
        /// 高血压信息
        /// </summary>
        public static CDSSHypertensionInfo HypertensionInfo = new CDSSHypertensionInfo();
        /// <summary>
        /// 血脂紊乱信息
        /// </summary>
        public static CDSSDyslipidemiaInfo DyslipidemiaInfo = new CDSSDyslipidemiaInfo();
        /// <summary>
        /// 高尿酸血症信息
        /// </summary>
        public static CDSSHyperuricemiaInfo HyperuricemiaInfo = new CDSSHyperuricemiaInfo();
        /// <summary>
        /// 非糖尿病肾脏疾病
        /// </summary>
        public static CDSSNephropathyInfo NephropathyInfo = new CDSSNephropathyInfo();
        /// <summary>
        /// 其他疾病史
        /// </summary>
        public static CDSSOtherDiseaseHistoryInfo OtherDiseaseHistoryInfo = new CDSSOtherDiseaseHistoryInfo();
        /// <summary>
        /// 个人史信息
        /// </summary>
        public static CDSSPersonalHistoryInfo PersonalHistoryInfo = new CDSSPersonalHistoryInfo();
        /// <summary>
        /// 家族疾病史信息
        /// </summary>
        public static CDSSFamilyDiseaseHistoryInfo FamilyDiseaseHistoryInfo = new CDSSFamilyDiseaseHistoryInfo();
        /// <summary>
        /// 体格检查信息
        /// </summary>
        public static CDSSPhysicalInfo PhysicalInfo = new CDSSPhysicalInfo();
        /// <summary>
        /// 实验室检查信息
        /// </summary>
        public static CDSSLabExamInfo LabExamInfo = new CDSSLabExamInfo();
        /// <summary>
        /// 其他检查信息
        /// </summary>
        public static CDSSOtherExamInfo OtherExamInfo = new CDSSOtherExamInfo();
        /// <summary>
        /// 诊断结论
        /// </summary>
        public static CDSSDiagnosedResult DiagnosedResult = new CDSSDiagnosedResult();        
        /// <summary>
        /// 膳食处方
        /// </summary>
        public static CDSSDietSuggestion DietSuggestion = new CDSSDietSuggestion();
        /// <summary>
        /// 运动建议
        /// </summary>
        public static CDSSExerciseSuggestion ExerciseSuggestion = new CDSSExerciseSuggestion();
        /// <summary>
        /// 记录索引（CDSSDBAccess要用到）
        /// </summary>
        public static CDSSRecordInfo RecordInfo = new CDSSRecordInfo();
        /// <summary>
        /// 用户操作日志
        /// </summary>
        public static List<CDSSOperationLog> OperationLog = new List<CDSSOperationLog>();
        /// <summary>
        /// 清空当前加载的病人的所有数据
        /// </summary>
        public static void Clear()
        {
            PatBasicInfo.Clear();
            AGMInfo.Clear();
            HypertensionInfo.Clear();
            DyslipidemiaInfo.Clear();
            HyperuricemiaInfo.Clear();
            NephropathyInfo.Clear();
            OtherDiseaseHistoryInfo.Clear();
            PersonalHistoryInfo.Clear();
            FamilyDiseaseHistoryInfo.Clear();
            PhysicalInfo.Clear();
            LabExamInfo.Clear();
            OtherExamInfo.Clear();
            DiagnosedResult.Clear();
            DietSuggestion.Clear();
            ExerciseSuggestion.Clear();
            RecordInfo.Clear();
        }
    }
}
