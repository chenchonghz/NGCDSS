using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

using System.Data;
using CDSSSystemData;
using CDSSDBAccess;
using WebSerivceModel;
using System.Data.SQLite;
using Utils;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using CDSSCLIPSEngine;
using CDSSFunction;
using CDSSEngine;
using Newtonsoft.Json.Linq;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class Service : System.Web.Services.WebService
{
    public Service() {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    /// <summary>
    /// 登录
    /// </summary>
    //[WebMethod]
    //[XmlInclude(typeof(CDSSUserInfo))]
    //public RestResult getUserByName(string userName, string password)
    //{
    //    RestResult restResult = new RestResult();
    //    if (userName == "")
    //    {
    //        restResult.status = "ERROR";
    //        restResult.message = "用户名为空！";
    //        return restResult;
    //    }
    //    if (password == "")
    //    {
    //        restResult.status = "ERROR";
    //        restResult.message = "密码为空！";
    //        return restResult;
    //    }
    //    //PWD加密后的数据
    //    string pwd_MD5 = Security.Md5Security(password);

    //    try
    //    {

    //        DataTable table = DBAccess.GetUsersExsit(userName);

    //        if (table.Rows.Count != 1)
    //        {
    //            restResult.status = "ERROR";
    //            restResult.message = "该用户不存在！";
    //            return restResult;
    //        }
    //        else
    //        {
    //            DataRow Row = table.Rows[0];
    //            //判断加密后的数据与数据库中的PWD是否一致
    //            if (Row["UserPwd"].Equals(pwd_MD5))
    //            {
    //                GlobalData.UserInfo.Clear();
    //                GlobalData.UserInfo.UserName = Row["UserName"].ToString();
    //                GlobalData.UserInfo.UserID = Row["UserID"].ToString();
    //                GlobalData.UserInfo.Department = Row["Department"].ToString();
    //                GlobalData.UserInfo.Title = Row["Title"].ToString();
    //                GlobalData.UserInfo.Phone = Row["Phone"].ToString();
    //                GlobalData.UserInfo.Company = Row["Company"].ToString();
    //                GlobalData.UserInfo.MailAddress = Row["MailAddress"].ToString();
    //                if (Row["LastLoginTime"] != Convert.DBNull)
    //                {
    //                    GlobalData.UserInfo.LastLoginTime = (DateTime)Row["LastLoginTime"];
    //                }
    //                if (Row["LoginFrequency"] != Convert.DBNull)
    //                {
    //                    GlobalData.UserInfo.LoginFrequency = int.Parse(Row["LoginFrequency"].ToString());
    //                }
    //                if (Row["LoginConnDBTime"] != Convert.DBNull)
    //                {
    //                    GlobalData.UserInfo.LoginConnDBTime = (DateTime)Row["LoginConnDBTime"];
    //                }
    //                if (Row["SaveCaseTime"] != Convert.DBNull)
    //                {
    //                    GlobalData.UserInfo.SaveCaseTime = (DateTime)Row["SaveCaseTime"];
    //                }

    //                restResult.message = "成功";
    //                CDSSUserInfo userInfo = GlobalData.UserInfo;
    //                restResult.data = userInfo;
    //                return restResult;
    //            }
    //            else
    //            {
    //                restResult.status = "ERROR";
    //                restResult.message = "密码错误";
    //                return restResult;
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        restResult.message = "登录出现异常错误，请联系cdssinfo@vico-lab.com！";
    //        restResult.status = "ERROR";
    //        return restResult;
    //    }

    //}

    [WebMethod]
    [XmlInclude(typeof(CDSSUserInfo))]
    public RestResult getUserByName(string value)
    {
        RestResult restResult = new RestResult();
        LoginReceive loginReceive = new LoginReceive(value);
        if (loginReceive.userName == "")
        {
            restResult.status = "ERROR";
            restResult.message = "用户名为空！";
            return restResult;
        }
        if (loginReceive.password == "")
        {
            restResult.status = "ERROR";
            restResult.message = "密码为空！";
            return restResult;
        }
        //PWD加密后的数据
        string pwd_MD5 = Security.Md5Security(loginReceive.password);

        try
        {

            DataTable table = DBAccess.GetUsersExsit(loginReceive.userName);

            if (table.Rows.Count != 1)
            {
                restResult.status = "ERROR";
                restResult.message = "该用户不存在！";
                return restResult;
            }
            else
            {
                DataRow Row = table.Rows[0];
                //判断加密后的数据与数据库中的PWD是否一致
                if (Row["UserPwd"].Equals(pwd_MD5))
                {
                    GlobalData.UserInfo.Clear();
                    GlobalData.UserInfo.UserName = Row["UserName"].ToString();
                    GlobalData.UserInfo.UserID = Row["UserID"].ToString();
                    GlobalData.UserInfo.Department = Row["Department"].ToString();
                    GlobalData.UserInfo.Title = Row["Title"].ToString();
                    GlobalData.UserInfo.Phone = Row["Phone"].ToString();
                    GlobalData.UserInfo.Company = Row["Company"].ToString();
                    GlobalData.UserInfo.MailAddress = Row["MailAddress"].ToString();
                    if (Row["LastLoginTime"] != Convert.DBNull)
                    {
                        GlobalData.UserInfo.LastLoginTime = (DateTime)Row["LastLoginTime"];
                    }
                    if (Row["LoginFrequency"] != Convert.DBNull)
                    {
                        GlobalData.UserInfo.LoginFrequency = int.Parse(Row["LoginFrequency"].ToString());
                    }
                    if (Row["LoginConnDBTime"] != Convert.DBNull)
                    {
                        GlobalData.UserInfo.LoginConnDBTime = (DateTime)Row["LoginConnDBTime"];
                    }
                    if (Row["SaveCaseTime"] != Convert.DBNull)
                    {
                        GlobalData.UserInfo.SaveCaseTime = (DateTime)Row["SaveCaseTime"];
                    }
                    restResult.status = "SUCCESS";
                    restResult.message = "成功";
                    CDSSUserInfo userInfo = GlobalData.UserInfo;
                    restResult.data = userInfo;
                    return restResult;
                }
                else
                {
                    restResult.status = "ERROR";
                    restResult.message = "密码错误";
                    return restResult;
                }

            }
        }
        catch (Exception ex)
        {
            restResult.message = "登录出现异常错误，请联系cdssinfo@vico-lab.com！";
            restResult.status = "ERROR";
            return restResult;
        }

    }

    /// <summary>
    /// 保存数据
    /// </summary>
    [WebMethod]
    public RestResult SavePatData(GlobalDataInfo globalData)
    {
        RestResult restResult = new RestResult();
        GlobalData.PatBasicInfo = globalData.PatBasicInfo;
        GlobalData.AGMInfo = globalData.AGMInfo;
        GlobalData.HypertensionInfo = globalData.HypertensionInfo;
        GlobalData.DyslipidemiaInfo = globalData.DyslipidemiaInfo;
        GlobalData.HyperuricemiaInfo = globalData.HyperuricemiaInfo;
        GlobalData.NephropathyInfo = globalData.NephropathyInfo;
        GlobalData.OtherDiseaseHistoryInfo = globalData.OtherDiseaseHistoryInfo;
        GlobalData.PersonalHistoryInfo = globalData.PersonalHistoryInfo;
        GlobalData.FamilyDiseaseHistoryInfo = globalData.FamilyDiseaseHistoryInfo;
        GlobalData.PhysicalInfo = globalData.PhysicalInfo;
        GlobalData.LabExamInfo = globalData.LabExamInfo;
        GlobalData.OtherExamInfo = globalData.OtherExamInfo;
        GlobalData.DiagnosedResult = globalData.DiagnosedResult;
        GlobalData.DietSuggestion = globalData.DietSuggestion;
        GlobalData.ExerciseSuggestion = globalData.ExerciseSuggestion;
        GlobalData.RecordInfo = globalData.RecordInfo;
        if (DBAccess.SaveDataToDB())
        {
            restResult.message = "保存成功！";
        }
        else
        {
            string ErrorInfo = "保存病案记录时出现错误。\n详细信息：";
            ErrorInfo += DBAccess.LastErrorInfo;
            restResult.message = ErrorInfo;
            restResult.status = "ERROR";
        }
        return restResult;
    }
    /// <summary>
    /// 获取病人各项信息
    /// </summary>
    [WebMethod]
    [XmlInclude(typeof(GlobalDataInfo))]
    public RestResult GetGlobalDataByRecordSEQ(int recordSeq)
    {
        RestResult restResult = new RestResult();
        GlobalData.RecordInfo.RecordSeq = recordSeq;
        if (DBAccess.GetDataFromDB())
        {
            GlobalDataInfo globalData = new GlobalDataInfo();
            globalData.PatBasicInfo = GlobalData.PatBasicInfo;
            globalData.AGMInfo = GlobalData.AGMInfo;
            globalData.HypertensionInfo = GlobalData.HypertensionInfo;
            globalData.DiagnosedResult = GlobalData.DiagnosedResult;
            globalData.HyperuricemiaInfo = GlobalData.HyperuricemiaInfo;
            globalData.NephropathyInfo = GlobalData.NephropathyInfo;
            globalData.OtherDiseaseHistoryInfo = GlobalData.OtherDiseaseHistoryInfo;
            globalData.PersonalHistoryInfo = GlobalData.PersonalHistoryInfo;
            globalData.FamilyDiseaseHistoryInfo = GlobalData.FamilyDiseaseHistoryInfo;
            globalData.PhysicalInfo = GlobalData.PhysicalInfo;
            globalData.LabExamInfo = GlobalData.LabExamInfo;
            globalData.OtherExamInfo = GlobalData.OtherExamInfo;
            globalData.DiagnosedResult = GlobalData.DiagnosedResult;
            globalData.DietSuggestion = GlobalData.DietSuggestion;
            globalData.ExerciseSuggestion = GlobalData.ExerciseSuggestion;
            globalData.RecordInfo = GlobalData.RecordInfo;
            restResult.data = globalData;
        }
        else
        {
            string ErrorInfo = "读取病案记录时出现错误。\n详细信息：";
            ErrorInfo += DBAccess.LastErrorInfo;
            restResult.message = ErrorInfo;
            restResult.status = "ERROR";
        }
        return restResult;
    }
    /// <summary>
    /// 查询
    /// </summary>
    [WebMethod]
    [XmlInclude(typeof(QueryResultList))]
    public RestResult QueryPatDataByCondition()
    {
        RestResult restResult = new RestResult();
        try
        {
            QueryCondition qc = new QueryCondition();
            qc.UserID = "admin";
            qc.PatID = "";
            qc.strName = "";
            qc.strResult = "";
            qc.strSex = "";
            DataTable tableResult = DBAccess.Query(qc);
            // restResult.data = tableResult;
            if (tableResult.Rows.Count < 1)
            {
                restResult.status = "ERROR";
                restResult.message = "没有查询到患者信息！";
                return restResult;
            }
            else
            {
                QueryResultList patientList = new QueryResultList();
                Dictionary<string, DataRow> patbasic = new Dictionary<string, DataRow>();
                DESClass DESClass = new DESClass();
                for (int i = 0; i < tableResult.Rows.Count; i++)
                {
                    if (!patbasic.ContainsKey(tableResult.Rows[i]["PatSEQ"].ToString()))
                        patbasic.Add(tableResult.Rows[i]["PatSEQ"].ToString(), tableResult.Rows[i]);

                }
                foreach (KeyValuePair<string, DataRow> dr in patbasic)
                {
                    QueryPatient patient = new QueryPatient();
                    patient.RecordSEQ = dr.Value["RecordSEQ"].ToString();
                    patient.PatVisitDateTime = ((DateTime)dr.Value["PatVisitDateTime"]).ToString("yyyy-MM-dd");
                    patient.PatID = dr.Value["PatID"].ToString();
                    patient.PatName = DESClass.DESDecrypt(dr.Value["PatName"].ToString().Trim()).TrimEnd('\0');
                    patient.PatSex = dr.Value["PatSex"].ToString().Trim();
                    patient.PatSEQ = dr.Value["PatSEQ"].ToString();
                    patientList.queryResultList.Add(patient);
                }
                restResult.data = patientList;
                //restResult.data = patientBasicInfo;
            }
        } catch (Exception e)
        {
            restResult.status = "ERROR";
            restResult.message = e.ToString();
        }
        return restResult;
    }

    /// <summary>
    /// 获取病史记录
    /// </summary>
    [WebMethod]
    [XmlInclude(typeof(QueryResultList))]
    public RestResult QueryDiseaseHistoryByRecordSeq(string recordSeq)
    {
        RestResult restResult = new RestResult();
        QueryResultList historyList = new QueryResultList();
        // foreach(string recordSeq in recordSeqs) {
            QueryDisHisRecord histroy = new QueryDisHisRecord();
            histroy.metabolicsSyndrome = DBAccess.GetNeededResult(Convert.ToInt32(recordSeq), "代谢综合征");
            histroy.dangerDegree = DBAccess.GetNeededResult(Convert.ToInt32(recordSeq), "危险度");
            histroy.dangerScore = DBAccess.GetNeededResult(Convert.ToInt32(recordSeq), "危险度积分");
            histroy.lostInfo = DBAccess.GetNeededData(Convert.ToInt32(recordSeq));
            int recordStatus = DBAccess.GetRecordStatus(Convert.ToInt32(recordSeq));
            if (!string.IsNullOrEmpty(histroy.lostInfo) && recordStatus == 0)
            {
                histroy.color = "red";
            }
            historyList.queryResultList.Add(histroy);
        //}
        restResult.data = historyList;
        return restResult;
    }

    /// <summary>
    /// 获取诊断结论
    /// </summary>
    [WebMethod]
    [XmlInclude(typeof(CDSSDiagnosedResult))]
    public RestResult GetDiagnosedResultByReasoning()
    {
        RestResult restResult = new RestResult();
        //GlobalData.PatBasicInfo = globalData.PatBasicInfo;
        //GlobalData.AGMInfo = globalData.AGMInfo;
        //GlobalData.HypertensionInfo = globalData.HypertensionInfo;
        //GlobalData.DyslipidemiaInfo = globalData.DyslipidemiaInfo;
        //GlobalData.HyperuricemiaInfo = globalData.HyperuricemiaInfo;
        //GlobalData.NephropathyInfo = globalData.NephropathyInfo;
        //GlobalData.OtherDiseaseHistoryInfo = globalData.OtherDiseaseHistoryInfo;
        //GlobalData.PersonalHistoryInfo = globalData.PersonalHistoryInfo;
        //GlobalData.FamilyDiseaseHistoryInfo = globalData.FamilyDiseaseHistoryInfo;
        //GlobalData.PhysicalInfo = globalData.PhysicalInfo;
        //GlobalData.LabExamInfo = globalData.LabExamInfo;
        //GlobalData.DiagnosedResult.Clear();
        List<FunctionTypeDef.EventModels> lstEventModels = new List<FunctionTypeDef.EventModels>();
        CDSSFunction.Interface.ObtainInfernceEvents(ref lstEventModels);
        CDSSFunction.Interface.SetInferenceNeededEvents(lstEventModels);
        CDSSFunction.Interface.InfernceExplanationExecute();
        foreach (CDSSOneDiseaseDiagnosedResult result in GlobalData.DiagnosedResult.DiseaseDiagnosedResultList)
        {
            GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList.Add(result.Clone());
        }
        restResult.data = GlobalData.DiagnosedResult;
        return restResult;
    }

    [WebMethod]
    public String testAPIFunction (String value)
    {
        TestModel model = new TestModel(value);
        return "success";
    }

    /// <summary>
    /// 保存病人基本信息
    /// </summary>
    [WebMethod]
    public RestResult SavePatBasicDataToDB(string value)
    {
        RestResult restResult = new RestResult();
        ReceivePatData receivePatData = new ReceivePatData(value);
        if (DBAccess.SaveDataToDB())
        {
            restResult.message = "保存成功！";
        }
        else
        {
            string ErrorInfo = "保存病案记录时出现错误。\n详细信息：";
            ErrorInfo += DBAccess.LastErrorInfo;
            restResult.message = ErrorInfo;
            restResult.status = "ERROR";
        }
        return restResult;
    }

    /// <summary>
    /// 保存快速录入信息
    /// </summary>
    [WebMethod]
    public RestResult SaveQuickInfoToDB(string value)
    {
        RestResult restResult = new RestResult();
        ReceiveQuickInfo receiveQuickInfo = new ReceiveQuickInfo(value);
        if (DBAccess.SaveDataToDB())
        {
            restResult.status = "SUCCESS";
            restResult.message = "保存成功！";
        }
        else
        {
            string ErrorInfo = "保存病案记录时出现错误。\n详细信息：";
            ErrorInfo += DBAccess.LastErrorInfo;
            restResult.message = ErrorInfo;
            restResult.status = "ERROR";
        }
        return restResult;
    }
}





