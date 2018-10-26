using System;
using System.Collections.Generic;
using System.Web;
using CDSSSystemData;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using CDSSDBAccess;

/// <summary>
/// Summary description for RestResult
/// </summary>
namespace WebSerivceModel
{
    public class RestResult
    {
        // 请求状态：SUCCESS,ERROR
        //
        public string status = "SUCCESS";

        // 请求返回的数据 
        public Object data;

        // 请求返回的message
        public string message;
    }

    public abstract class QueryResult
    {

    }

    public class QueryPatient:QueryResult
    {
        public string RecordSEQ;
        public string PatVisitDateTime;
        public string PatSEQ;
        public string PatID;
        public string PatName;
        public string PatSex;
    }

    public class QueryDisHisRecord:QueryResult
    {
        public string recordSEQ;
        public string color;
        public string metabolicsSyndrome;
        public string dangerDegree;
        public string dangerScore;
        public string lostInfo;
    }
    public class QueryResultList
    {
        [XmlElement(typeof(QueryPatient))]
        [XmlElement(typeof(QueryDisHisRecord))]
        public List<QueryResult> queryResultList;

        public QueryResultList()
        {
            queryResultList = new List<QueryResult>();
        }
    }

    public class GlobalDataInfo
    {
       
        /// <summary>
        /// 病人基本信息
        /// </summary>
        public CDSSPatBasicInfo PatBasicInfo = new CDSSPatBasicInfo();
        /// <summary>
        /// 糖代谢异常信息
        /// </summary>
        public CDSSAGMInfo AGMInfo = new CDSSAGMInfo();
        /// <summary>
        /// 高血压信息
        /// </summary>
        public CDSSHypertensionInfo HypertensionInfo = new CDSSHypertensionInfo();
        /// <summary>
        /// 血脂紊乱信息
        /// </summary>
        public CDSSDyslipidemiaInfo DyslipidemiaInfo = new CDSSDyslipidemiaInfo();
        /// <summary>
        /// 高尿酸血症信息
        /// </summary>
        public CDSSHyperuricemiaInfo HyperuricemiaInfo = new CDSSHyperuricemiaInfo();
        /// <summary>
        /// 非糖尿病肾脏疾病
        /// </summary>
        public CDSSNephropathyInfo NephropathyInfo = new CDSSNephropathyInfo();
        /// <summary>
        /// 其他疾病史
        /// </summary>
        public CDSSOtherDiseaseHistoryInfo OtherDiseaseHistoryInfo = new CDSSOtherDiseaseHistoryInfo();
        /// <summary>
        /// 个人史信息
        /// </summary>
        public CDSSPersonalHistoryInfo PersonalHistoryInfo = new CDSSPersonalHistoryInfo();
        /// <summary>
        /// 家族疾病史信息
        /// </summary>
        public CDSSFamilyDiseaseHistoryInfo FamilyDiseaseHistoryInfo = new CDSSFamilyDiseaseHistoryInfo();
        /// <summary>
        /// 体格检查信息
        /// </summary>
        public CDSSPhysicalInfo PhysicalInfo = new CDSSPhysicalInfo();
        /// <summary>
        /// 实验室检查信息
        /// </summary>
        public CDSSLabExamInfo LabExamInfo = new CDSSLabExamInfo();
        /// <summary>
        /// 其他检查信息
        /// </summary>
        public CDSSOtherExamInfo OtherExamInfo = new CDSSOtherExamInfo();
        /// <summary>
        /// 诊断结论
        /// </summary>
        public CDSSDiagnosedResult DiagnosedResult = new CDSSDiagnosedResult();
        /// <summary>
        /// 膳食处方
        /// </summary>
        public CDSSDietSuggestion DietSuggestion = new CDSSDietSuggestion();
        /// <summary>
        /// 运动建议
        /// </summary>
        public CDSSExerciseSuggestion ExerciseSuggestion = new CDSSExerciseSuggestion();
        /// <summary>
        /// 记录索引（CDSSDBAccess要用到）
        /// </summary>
        public CDSSRecordInfo RecordInfo = new CDSSRecordInfo();
        /// <summary>
        /// 用户操作日志
        /// </summary>
        public List<CDSSOperationLog> OperationLog = new List<CDSSOperationLog>();
        /// <summary>
        /// 清空当前加载的病人的所有数据
        /// </summary>
        public void Clear()
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

    [Serializable]
    public class TestModel {

        public TestModel(string json)
        {
            try
            {
                JObject jObject = JObject.Parse(@json);
                this.num = (int)jObject["num"];
                this.unit = (String)jObject["unit"];
            } catch (Exception e)
            {
                e.ToString();
            }

        }

        int num;
        String unit;
    }

    /// <summary>
    /// 接收登录用户名和密码
    /// </summary>
    [Serializable]
    public class LoginReceive
    {

        public LoginReceive(string json)
        {
            try
            {
                JObject jObject = JObject.Parse(@json);
                this.userName = (String)jObject["userName"];
                this.password = (String)jObject["password"];
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }

        public String userName;
        public String password;
    }

    /// <summary>
    /// 接收查询条件
    /// </summary>
    [Serializable]
    public class QueryConditionReceive
    {
        public QueryConditionReceive(String json)
        {
            try
            {
                JObject jObject = JObject.Parse(@json);
                this.UserID = GlobalData.UserInfo.UserID;
                this.strName = (String)jObject["strName"];
                this.PatID = (String)jObject["PatID"];
                this.strResult = "";
                this.strSex = (String)jObject["strSex"];
                if ((string)jObject["dtBirthDayFrom"] != "")
                    this.dtBirthDayFrom = (DateTime)jObject["dtBirthDayFrom"];
                if ((string)jObject["dtBirthDayTo"] != "")
                    this.dtBirthDayTo = (DateTime)jObject["dtBirthDayTo"];
                if ((string)jObject["dtVisitFrom"] != "")
                    this.dtVisitFrom = (DateTime)jObject["dtVisitFrom"];
                if ((string)jObject["dtVisitTo"] != "")
                    this.dtVisitTo = (DateTime)jObject["dtVisitTo"];
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        //CDSSDBAccess.QueryCondition qc = new CDSSDBAccess.QueryCondition();
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
        public string UserID;
        public string PatID;
    }

    /// <summary>
    /// 基本信息录入
    /// </summary>
    [Serializable]
    public class ReceivePatData
    {
        public ReceivePatData(string json)
        {
            try
            {
                JObject jObject = JObject.Parse(@json);
                GlobalData.PatBasicInfo.PatSEQ = "*";
                GlobalData.PatBasicInfo.PatName = (string)jObject["PatName"];
                GlobalData.PatBasicInfo.PatID = (string)jObject["PatID"];
                GlobalData.PatBasicInfo.PatSex = (string)jObject["PatSex"];
                if ((string)jObject["PatVisitDateTime"] != "")
                    GlobalData.PatBasicInfo.PatVisitDateTime = (DateTime)jObject["PatVisitDateTime"];
                GlobalData.PatBasicInfo.PatNational = (string)jObject["PatNational"];
                GlobalData.PatBasicInfo.PatEducationLevel = (string)jObject["PatEducationLevel"];
                if ((string)jObject["PatBirthday"] != "")
                    GlobalData.PatBasicInfo.PatBirthday = (DateTime)jObject["PatBirthday"];
                GlobalData.PatBasicInfo.PatIncomeSource = (string)jObject["PatIncomeSource"];
                GlobalData.PatBasicInfo.PatPhone = (string)jObject["PatPhone"];
                GlobalData.PatBasicInfo.PatTreatmentCost = (string)jObject["PatTreatmentCost"];
                if ((string)jObject["PatChildCount"] != "")
                    GlobalData.PatBasicInfo.PatChildCount = (int)jObject["PatChildCount"];
                GlobalData.PatBasicInfo.PatProfessional = (string)jObject["PatProfessional"];
                if ((string)jObject["PatSiblingsCount"] != "")
                    GlobalData.PatBasicInfo.PatSiblingsCount = (int)jObject["PatSiblingsCount"];
                GlobalData.PatBasicInfo.PatIncome = (string)jObject["PatIncome"];
                GlobalData.PatBasicInfo.PatBirthProvince = (string)jObject["PatBirthProvince"];
                GlobalData.PatBasicInfo.PatBirthCity = (string)jObject["PatBirthCity"];
                GlobalData.PatBasicInfo.PatAddress = (string)jObject["PatAddress"];
                GlobalData.PatBasicInfo.PatZipcode = (string)jObject["PatZipcode"];
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }

    /// <summary>
    /// 快速信息录入
    /// </summary>
    [Serializable]
    public class ReceiveQuickInfo
    {
        public ReceiveQuickInfo(string json)
        {
            try
            {
                JObject jObject = JObject.Parse(@json);
                //GlobalData.PatBasicInfo.PatSEQ = "*";
                GlobalData.PatBasicInfo.PatName = (string)jObject["PatName"];
                GlobalData.PatBasicInfo.PatID = (string)jObject["PatID"];
                GlobalData.PatBasicInfo.PatSex = (string)jObject["PatSex"];
                GlobalData.PatBasicInfo.PatNational = (string)jObject["PatNational"];
                GlobalData.PhysicalInfo.Height = (string)jObject["Height"];
                GlobalData.PhysicalInfo.Weigh = (string)jObject["Weigh"];
                GlobalData.PhysicalInfo.WC = (string)jObject["WC"];
                GlobalData.PhysicalInfo.SBP1 = (string)jObject["SBP1"];
                GlobalData.PhysicalInfo.DBP1 = (string)jObject["DBP1"];
                GlobalData.PhysicalInfo.HC = (string)jObject["HC"];
                GlobalData.LabExamInfo.FBG = (string)jObject["FBG"];
                GlobalData.LabExamInfo.TWOHPBG = (string)jObject["TWOHPBG"];
                GlobalData.LabExamInfo.HBA1C = (string)jObject["HBA1C"];
                GlobalData.LabExamInfo.FastingInsulin = (string)jObject["FastingInsulin"];
                GlobalData.LabExamInfo.PostprandialInsulin = (string)jObject["PostprandialInsulin"];
                GlobalData.LabExamInfo.CR = (string)jObject["CR"];
                GlobalData.LabExamInfo.AlanineAminotransferase = (string)jObject["AlanineAminotransferase"];
                GlobalData.LabExamInfo.AspartateAminotransferase = (string)jObject["AspartateAminotransferase"];
                GlobalData.LabExamInfo.SerumTotalProtein = (string)jObject["SerumTotalProtein"];
                GlobalData.LabExamInfo.SerumAlbumin = (string)jObject["SerumAlbumin"];
                GlobalData.LabExamInfo.BUA = (string)jObject["BUA"];
                GlobalData.LabExamInfo.TC = (string)jObject["TC"];
                GlobalData.LabExamInfo.HDLC = (string)jObject["HDLC"];
                GlobalData.LabExamInfo.TG = (string)jObject["TG"];
                GlobalData.LabExamInfo.LDLC = (string)jObject["LDLC"];
                GlobalData.LabExamInfo.Tbil = (string)jObject["Tbil"];
                GlobalData.LabExamInfo.UN = (string)jObject["UN"];
                GlobalData.LabExamInfo.AFP = (string)jObject["AFP"];
                GlobalData.LabExamInfo.CEA = (string)jObject["CEA"];
                GlobalData.PersonalHistoryInfo.IsSmokeing=(bool)jObject["IsSmokeing"];
                GlobalData.PersonalHistoryInfo.IsDrinking = (bool)jObject["IsDrinking"];
                GlobalData.PersonalHistoryInfo.HasExerciseRecent = (bool)jObject["HasExerciseRecent"];
                GlobalData.PersonalHistoryInfo.MainFoodAmount = (string)jObject["MainFoodAmount"];
                GlobalData.PersonalHistoryInfo.ProteinAmount = (string)jObject["ProteinAmount"];
                GlobalData.PersonalHistoryInfo.OilAmount = (string)jObject["OilAmount"];
                GlobalData.PersonalHistoryInfo.MaxWeight = (string)jObject["MaxWeight"];
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}