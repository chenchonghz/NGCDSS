using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using CDSSSystemData;
using System.IO;
using CDSSCtrlLib;

namespace CDSSDBAccess
{
    public struct Period
    {
        public DateTime StartTime;
        public DateTime EndTime;
    }
    public static class DBAccess
    {
        public static classSQLServerDBInterface SQLServerDBInterface=new classSQLServerDBInterface();
        /// <summary>
        /// 最后错误信息
        /// </summary>
        public static string LastErrorInfo;

        /// <summary>
        /// 进度通知事件
        /// </summary>
        public static event ReportCurrentProgressEventHandler ReportCurrentProgress;
        private static void RaiseReportCurrentProgressEvent(int Value, string DetailInfo)
        {
            ReportCurrentProgressEventHandler temp = ReportCurrentProgress;
            if (temp != null)
                temp(Value, DetailInfo);
        }
        /// <summary>
        /// 根据登录名查询该用户的信息
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public static DataTable GetUsersExsit(string strUserID)
        {
            string strSql = "SELECT * FROM CDSS_UserInfo WHERE UserID='" + strUserID + "'";

            try
            {
                DataTable Table = SQLServerDBInterface.GetDataSet(strSql);
                return Table;
            }
            catch (SQLiteException e)
            {
                throw e;
            }
        }

        public static bool UpdateUsers()
        {
            string sql = "Update CDSS_UserInfo set LastLoginTime=@LastLoginTime,LoginFrequency=@LoginFrequency,LoginConnDBTime=@LoginConnDBTime "
                        + " where UserID=@UserID";
            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@LastLoginTime",GlobalData.UserInfo.LastLoginTime),
                new SQLiteParameter("@LoginFrequency",GlobalData.UserInfo.LoginFrequency),
                new SQLiteParameter("@LoginConnDBTime",GlobalData.UserInfo.LoginConnDBTime.ToString("s")),
                new SQLiteParameter("@UserID",GlobalData.UserInfo.UserID)       
			};
            try
            {
                int result = SQLServerDBInterface.ExecuteCommand(sql, para);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                return false;
            }
        }
        public static bool UpdateUsers(DateTime SaveCaseTime)
        {
            string sql = "Update CDSS_UserInfo set SaveCaseTime=@SaveCaseTime where UserID=@UserID";
            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@SaveCaseTime",SaveCaseTime.ToString("s")),
                new SQLiteParameter("@UserID",GlobalData.UserInfo.UserID)       
			};
            try
            {
                int result = SQLServerDBInterface.ExecuteCommand(sql, para);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                return false;
            }
        }
        /// <summary>
        /// 查询符合条件的病案记录
        /// </summary>
        /// <param name="oQueryCondition">查询条件</param>
        /// <returns>符合条件的病案记录</returns>
        public static DataTable Query(QueryCondition oQueryCondition)
        {
            //SQL Change By ZX，100414 查询同一病人多次就诊信息
            string sql = "SELECT CDSS_RecordHistory.RecordSEQ,CDSS_RecordHistory.PatSEQ,PatID,PatName,PatSex,PatBirthday,PatVisitDateTime,PatBirthProvince,PatBirthCity,"
                        + "PatEducationLevel,PatPhone FROM CDSS_PatBasicInfo,CDSS_RecordInfo,CDSS_RecordHistory "
                        + " where CDSS_RecordHistory.RecordSEQ=CDSS_RecordInfo.RecordSEQ and CDSS_PatBasicInfo.PatSEQ=CDSS_RecordHistory.PatSEQ "
                        + "and HistoryRecordStatus<>2";// and UserID='" + oQueryCondition.UserID + "'"; 不同用户之间也可相互查询 ns 20100715
            DESClass des = new DESClass();

            //添加查询条件
            //姓名
            if (oQueryCondition.strName != "")
            {
                // TO DO:
                // 没有实现模糊查询
                sql += " and PatName like '" + des.DESEncrypt(oQueryCondition.strName) + "'"; // 姓名加密后再比较
            }

            //性别
            if (oQueryCondition.strSex != "")
                sql += " and PatSex='" + oQueryCondition.strSex + "'";

            //生日
            if (oQueryCondition.dtBirthDayFrom.HasValue && oQueryCondition.dtBirthDayTo.HasValue)
            {
                sql += " and PatBirthday between '"
                    + oQueryCondition.dtBirthDayFrom.Value.Date.ToString() + "' and '"
                    + oQueryCondition.dtBirthDayTo.Value.Date.ToString() + "'";
            }
            else if (oQueryCondition.dtBirthDayFrom.HasValue)
            {
                sql += " and PatBirthday = '"
                    + des.DESEncrypt(oQueryCondition.dtBirthDayFrom.Value.Date.ToString()) + "'"; // 出生日期加密后再比较
            }
            //就诊日期
            if (oQueryCondition.dtVisitFrom.HasValue && oQueryCondition.dtVisitTo.HasValue)
            {
                sql += " and PatVisitDateTime between '"
                    + oQueryCondition.dtVisitFrom.Value.Date.ToString("s") + "' and '"
                    + oQueryCondition.dtVisitTo.Value.Date.ToString("s") + "'";
            }
            else if (oQueryCondition.dtVisitFrom.HasValue)
            {
               // sql += " and PatVisitDateTime = '"
                 //   + oQueryCondition.dtVisitFrom.Value.Date.ToString("s") + "'";
                sql += " and PatVisitDateTime between '"
                    + oQueryCondition.dtVisitFrom.Value.Date.ToString("s") + "' and '"
                    + oQueryCondition.dtVisitFrom.Value.Date.AddHours(23).ToString("s") + "'";

            }

            //2009-8-27:ccj
            //添加ID查询
            if (!string.IsNullOrEmpty(oQueryCondition.PatID))
            {
                sql += "  and PatID LIKE '%" + oQueryCondition.PatID.ToString() + "%'";
            }
            //修改结束

            try
            {
               // sql += " ORDER BY PatVisitDateTime DESC, PatSEQ ASC";
                DataTable Table = SQLServerDBInterface.GetDataSet(sql);
                return Table;
            }
            catch (SQLiteException e)
            {
                throw e;
            }

        }

        public static DataTable GetPatientData(string patSEQ)
        {
            string sql = "SELECT CDSS_RecordHistory.PatVisitDateTime, CDSS_RecordInfo.RecordSEQ, CDSS_PhysicalInfo.Height, CDSS_PhysicalInfo.WC, "
                + "CDSS_PhysicalInfo.HC, CDSS_PhysicalInfo.HR, CDSS_PhysicalInfo.Weigh, CDSS_PhysicalInfo.SBP1, CDSS_PhysicalInfo.DBP1, "
                + "CDSS_LabExamInfo.BG, CDSS_LabExamInfo.FBG, CDSS_LabExamInfo.TwoHPBG, CDSS_LabExamInfo.TC, CDSS_LabExamInfo.HDLC, "
                + "CDSS_LabExamInfo.TG, CDSS_LabExamInfo.LDLC, CDSS_LabExamInfo.HBA1C "
                + "FROM CDSS_RecordInfo INNER JOIN "
                + "CDSS_RecordHistory ON CDSS_RecordInfo.RecordSEQ = CDSS_RecordHistory.RecordSEQ INNER JOIN "
                + "CDSS_PhysicalInfo ON CDSS_RecordInfo.RecordSEQ = CDSS_PhysicalInfo.RecordSEQ INNER JOIN "
                + "CDSS_LabExamInfo ON CDSS_RecordInfo.RecordSEQ = CDSS_LabExamInfo.RecordSEQ "
                + "WHERE (CDSS_RecordHistory.PatSEQ = " + patSEQ + ")  "
                + "ORDER BY CDSS_RecordHistory.PatVisitDateTime";

            DataTable Table = SQLServerDBInterface.GetDataSet(sql);
            Table.Columns.Add("BMI");
            foreach (DataRow row in Table.Rows)
            {
                double height, weight;
                if (!string.IsNullOrEmpty(row["Height"].ToString()) && !string.IsNullOrEmpty(row["Weigh"].ToString()))
                {
                    height = double.Parse(row["Height"].ToString());
                    weight = double.Parse(row["Weigh"].ToString());
                    double bmi = weight * 10000 / height / height;
                    row["BMI"] = bmi.ToString("00.0");
                }
                else
                {
                    row["BMI"] = DBNull.Value;
                }
            }
            return Table;
        }

        public static string GetNeededResult(int recordSEQ, string NeededResult)
        {
            string sql = "select Result from CDSS_DiagnosedResult where RecordSEQ=" + recordSEQ + " and Name= '" + NeededResult + "'";
            DataTable NeededResultTable = SQLServerDBInterface.GetDataSet(sql);
            if (NeededResultTable.Rows.Count <= 0)
            {
                return null;
            }
            else
                return NeededResultTable.Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取缺失信息
        /// </summary>
        /// <param name="recordSEQ">RecordSEQ</param>
        /// <returns>缺失信息（未缺失信息则返回NULL）</returns>
        public static string GetNeededData(int recordSEQ)
        {
            string sql = "SELECT DISTINCT DataNeeded FROM CDSS_DiagnosedResult WHERE (RecordSEQ = " + recordSEQ.ToString() + ") AND (DataNeeded <> '') AND (DataNeeded IS NOT NULL)";
            DataTable dataNeededTable = SQLServerDBInterface.GetDataSet(sql);

            if (dataNeededTable.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                string data = string.Empty;

                foreach (DataRow dataNeededItem in dataNeededTable.Rows)
                {
                    data += dataNeededItem.ItemArray[0].ToString() + " ";
                }

                return data.Trim();
            }
        }

        /// <summary>
        /// 返回状态记录
        /// </summary>
        /// <param name="recordSEQ">The record SEQ.</param>
        /// <returns>状态记录</returns>
        public static int GetRecordStatus(int recordSEQ)
        {
            //SQL Change By ZX，100414 查询同一病人多次就诊信息
            string sql = "SELECT HistoryRecordStatus FROM CDSS_RecordHistory WHERE RecordSEQ = " + recordSEQ.ToString();
            DataTable dataNeededTable = SQLServerDBInterface.GetDataSet(sql);

            if (dataNeededTable.Rows.Count == 1)
            {
                return (int)dataNeededTable.Rows[0].ItemArray[0];
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 设置状态记录
        /// </summary>
        /// <param name="recordSEQ">The record SEQ.</param>
        /// <param name="recordStatus">The record status.</param>
        /// <returns>设置状态</returns>
        public static int SetRecordStatus(int recordSEQ, int recordStatus)
        {
            string sql = "UPDATE [CDSS_RecordHistory] SET [HistoryRecordStatus] = " + recordStatus.ToString() + " WHERE RecordSEQ = " + recordSEQ.ToString();
            return SQLServerDBInterface.ExecuteCommand(sql);
        }

        /// <summary>
        /// 将DB中一份病案中基本无大改动的信息读取到全局变量
        /// 例如病人姓名、疾病史等数据不会发生过多变化
        /// </summary>
        /// <returns></returns>
        public static bool GetOnlyDataFromDB(int RecordSeq)
        {
            if (RecordSeq < 1)
            {
                LastErrorInfo = "病案索引值有误。";
                return false;
            }

            string RecordSEQ = RecordSeq.ToString();

            RaiseReportCurrentProgressEvent(10, "正在查询病人基本信息");
            if (!GetBasicinfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(20, "正在查询糖代谢异常信息");
            if (!GetAGMInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(30, "正在查询高血压信息");
            if (!GetHypertensionInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(40, "正在查询血脂紊乱信息");
            if (!GetDyslipidemiaInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(50, "正在查询高尿酸血症信息");
            if (!GetHyperuricemiaInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(60, "正在查询非糖尿病肾脏疾病信息");
            if (!GetNephropathyInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(70, "正在查询其它疾病史");
            if (!GetOtherDiseaseHistoryInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(80, "正在查询个人史");
            if (!GetPersonalHistoryInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(90, "正在查询家庭疾病史");
            if (!GetFamilyDiseaseHistoryInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(100, "查询完毕，准备显示结果");
            return true;
        }

        /// <summary>
        /// 将DB中的一份病案数据读取到全局变量中
        /// </summary>
        /// <returns></returns>
        public static bool GetDataFromDB()
        {
            if (GlobalData.RecordInfo.RecordSeq < 1)
            {
                LastErrorInfo = "病案索引值有误。";
                return false;
            }

            string RecordSEQ = GlobalData.RecordInfo.RecordSeq.ToString();

            RaiseReportCurrentProgressEvent(5, "请稍候");
            if (!GetRecordInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(10, "正在查询病人基本信息");
            if (!GetBasicinfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(15, "正在查询糖代谢异常信息");
            if (!GetAGMInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(25, "正在查询高血压信息");
            if (!GetHypertensionInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(35, "正在查询血脂紊乱信息");
            if (!GetDyslipidemiaInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(40, "正在查询高尿酸血症信息");
            if (!GetHyperuricemiaInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(45, "正在查询非糖尿病肾脏疾病信息");
            if (!GetNephropathyInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(50, "正在查询其它疾病史");
            if (!GetOtherDiseaseHistoryInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(55, "正在查询个人史");
            if (!GetPersonalHistoryInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(60, "正在查询家庭疾病史");
            if (!GetFamilyDiseaseHistoryInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(65, "正在查询体格检查结果");
            if (!GetPhysicalInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(70, "正在查询实验室检查结果");
            if (!GetLabExamInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(80, "正在查询其它检查结果");
            if (!GetOtherExamInfo(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(85, "正在查询以往诊断结论");
            if (!GetDiagnosedResultList(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(88, "正在查询以往诊断结论");
            if (!GetReasoningDiagnoseResultList(RecordSEQ))
                return false;


            RaiseReportCurrentProgressEvent(90, "正在查询以往膳食处方");
            if (!GetDietSuggestion(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(95, "正在查询以往运动建议");
            if (!GetExerciseSuggestion(RecordSEQ))
                return false;

            RaiseReportCurrentProgressEvent(100, "查询完毕，准备显示结果");
            return true;
        }
        /// <summary>
        /// added by yanhui for accelerating the speed of merging,which need another connection to DATABASE 20120331
        /// </summary>
        /// <returns></returns>
        public static bool GetDataFromDB(classSQLServerDBInterface anotherSQLServerInterface)
        {
            if (GlobalData.RecordInfo.RecordSeq < 1)
            {
                LastErrorInfo = "病案索引值有误。";
                return false;
            }

            string RecordSEQ = GlobalData.RecordInfo.RecordSeq.ToString();

            RaiseReportCurrentProgressEvent(5, "请稍候");
            if (!GetRecordInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(10, "正在查询病人基本信息");
            if (!GetBasicinfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(15, "正在查询糖代谢异常信息");
            if (!GetAGMInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(25, "正在查询高血压信息");
            if (!GetHypertensionInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(35, "正在查询血脂紊乱信息");
            if (!GetDyslipidemiaInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(40, "正在查询高尿酸血症信息");
            if (!GetHyperuricemiaInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(45, "正在查询非糖尿病肾脏疾病信息");
            if (!GetNephropathyInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(50, "正在查询其它疾病史");
            if (!GetOtherDiseaseHistoryInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(55, "正在查询个人史");
            if (!GetPersonalHistoryInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(60, "正在查询家庭疾病史");
            if (!GetFamilyDiseaseHistoryInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(65, "正在查询体格检查结果");
            if (!GetPhysicalInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(70, "正在查询实验室检查结果");
            if (!GetLabExamInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(80, "正在查询其它检查结果");
            if (!GetOtherExamInfo(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(85, "正在查询以往诊断结论");
            if (!GetDiagnosedResultList(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(88, "正在查询以往诊断结论");
            if (!GetReasoningDiagnoseResultList(RecordSEQ, anotherSQLServerInterface))
                return false;


            RaiseReportCurrentProgressEvent(90, "正在查询以往膳食处方");
            if (!GetDietSuggestion(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(95, "正在查询以往运动建议");
            if (!GetExerciseSuggestion(RecordSEQ, anotherSQLServerInterface))
                return false;

            RaiseReportCurrentProgressEvent(100, "查询完毕，准备显示结果");
            return true;
        }

        /// <summary>
        /// 数据保存异常
        /// </summary>
        private class DataSaveFailureException : ApplicationException { }
        /// <summary>
        /// 将全局变量中的病案数据保存到DB中
        /// </summary>
        /// <returns></returns>
        public static bool SaveDataToDB()
        {
            /****************************************************************
             * add by lch 090605 向CDSS_UserInfo表更新字段【保存案例的时间】的值
             ****************************************************************/
           // if (!UpdateUsers(DateTime.Parse(DBAccess.GetServerCurrentTime())))
               // throw new DataSaveFailureException();

            RaiseReportCurrentProgressEvent(5, "请稍候");
            int OldRecordSEQ = GlobalData.RecordInfo.RecordSeq;
            try
            {
                if (!SaveRecordInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(10, "正在保存病人基本信息");
                if (!SaveBasicinfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(15, "正在保存糖代谢异常信息");
                if (!SaveAGMInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(25, "正在保存高血压信息");
                if (!SaveHypertensionInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(35, "正在保存血脂紊乱信息");
                if (!SaveDyslipidemiaInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(40, "正在保存高尿酸血症信息");
                if (!SaveHyperuricemiaInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(45, "正在保存非糖尿病肾脏疾病信息");
                if (!SaveNephropathyInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(50, "正在保存其它疾病史");
                if (!SaveOtherDiseaseHistoryInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(55, "正在保存个人史");
                if (!SavePersonalHistoryInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(60, "正在保存家庭疾病史");
                if (!SaveFamilyDiseaseHistoryInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(65, "正在保存体格检查结果");
                if (!SavePhysicalInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(70, "正在保存实验室检查结果");
                if (!SaveLabExamInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(80, "正在保存其它检查结果");
                if (!SaveOtherExamInfo())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(85, "正在保存诊断结论");
                if (!SaveDiagnosedResultList())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(88, "正在保存推理机诊断结论");
                if (!SaveReasoningDiagnosedResultList())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(90, "正在保存膳食处方");
                if (!SaveDietSuggestion())
                    throw new DataSaveFailureException();

                RaiseReportCurrentProgressEvent(95, "正在保存运动建议");
                if (!SaveExerciseSuggestion())
                    throw new DataSaveFailureException();
            }
            catch (DataSaveFailureException e)
            {
                if (GlobalData.RecordInfo.RecordSeq != OldRecordSEQ)
                {//保存RecordInfo成功,但保存其他信息失败的情况
                    if (GlobalData.RecordInfo.RecordSeq != 0)
                        DeleteInfoByRecordSEQ(GlobalData.RecordInfo.RecordSeq.ToString());  //因为仅保存了部分,产生了垃圾数据,需要删除
                    GlobalData.RecordInfo.RecordSeq = OldRecordSEQ; //还原RecordSeq
                }
                //保存RecordInfo失败的情况下不会产生垃圾数据,直接返回false即可
                return false;
            }
            if (OldRecordSEQ != 0)
                DeleteInfoByRecordSEQ(OldRecordSEQ.ToString()); //保存成功后,删除原有数据

            RaiseReportCurrentProgressEvent(100, "保存成功");
            return true;
        }


        /// <summary>
        /// 根据索引信息删除DB信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool DeleteInfoByRecordSEQ(string RecordSEQ)
        {
            string sql = "";

            sql = "delete from CDSS_RecordHistory where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_AGMInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_DiagnosedResult where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_DietSuggestion where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_DyslipidemiaInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_ExerciseSuggestion where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_FamilyDiseaseHistoryInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_HypertensionInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_HyperuricemiaInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_LabExamInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_NephropathyInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_OtherDiseaseHistoryInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_OtherExamInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_PersonalHistoryInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_PhysicalInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_SymptomsInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_VascularUltrasound where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_DrinkingInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_ExerciseInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_MedicineInfo where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_OtherExamAbnormal where RecordSEQ=" + RecordSEQ
                 + "; delete from CDSS_RecordInfo where RecordSEQ=" + RecordSEQ
                 + ";delete from CDSS_ReasonDiagnosedResult where RecordSEQ=" + RecordSEQ;

            int result = SQLServerDBInterface.ExecuteCommand(sql);
            if (result > 0)
                return true;
            else
                return false;
        }


        #region 病案记录索引信息
        /// <summary>
        /// 根据RecordSEQ查询索引信息
        /// </summary>
        /// <returns></returns>
        private static bool GetRecordInfo(string RecordSEQ)
        {
            GlobalData.RecordInfo.Clear();

            string sql = "select * from CDSS_RecordInfo where RecordSEQ=" + RecordSEQ;

            SQLiteDataReader reader = SQLServerDBInterface.GetReader(sql);
            if (reader.Read())
            {
                GlobalData.RecordInfo.RecordSeq = Convert .ToInt32(reader["RecordSEQ"]);
                GlobalData.RecordInfo.RecordDateTime = (DateTime)reader["RecordDateTime"];
                GlobalData.RecordInfo.UserID = (string)reader["UserID"];
                GlobalData.RecordInfo.InferenceState = (string)reader["InferenceState"];
                GlobalData.RecordInfo.KnowledgeVersion = (string)reader["KnowledgeVersion"];
                GlobalData.RecordInfo.IEVersion = (string)reader["IEVersion"];

                reader.Close();
            }
            else
            {
                reader.Close();
                return false;
            }
            return true;
        }
        /// <summary>
        /// added by yanhui,override for another connection to database 
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetRecordInfo(string RecordSEQ, classSQLServerDBInterface anotherSQLServerInterface)
        {
            GlobalData.RecordInfo.Clear();

            string sql = "select * from CDSS_RecordInfo where RecordSEQ=" + RecordSEQ;

            SQLiteDataReader reader = anotherSQLServerInterface.GetReader(sql);
            if (reader.Read())
            {
                GlobalData.RecordInfo.RecordSeq = Convert.ToInt32(reader["RecordSEQ"]);
                GlobalData.RecordInfo.RecordDateTime = (DateTime)reader["RecordDateTime"];
                GlobalData.RecordInfo.UserID = (string)reader["UserID"];
                GlobalData.RecordInfo.InferenceState = (string)reader["InferenceState"];
                GlobalData.RecordInfo.KnowledgeVersion = (string)reader["KnowledgeVersion"];
                GlobalData.RecordInfo.IEVersion = (string)reader["IEVersion"];

                reader.Close();
            }
            else
            {
                reader.Close();
                return false;
            }
            return true;
        }


        /// <summary>
        /// 保存索引表信息，返回新增的RecordSeq
        /// </summary>
        /// <returns></returns>
        private static bool SaveRecordInfo()
        {
            string sql = "insert into CDSS_RecordInfo(UserID,InferenceState,KnowledgeVersion,IEVersion)"
                       + " values(@UserID,@InferenceState,@KnowledgeVersion,@IEVersion) ;"
                       + " SELECT LAST_INSERT_ROWID()";

           // string sql = "insert into CDSS_RecordInfo(RecordSEQ,UserID,InferenceState,KnowledgeVersion,IEVersion)"
                        //           + " values(NULL,@UserID,@InferenceState,@KnowledgeVersion,@IEVersion)"; 
                        // 

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                // revised by lch 090605 修改CDSS_RecordInfo的RecordDateTime值为服务器时间，在数据库中使用getDate（）取值。
                //new SqlParameter("@RecordDateTime",GlobalData.RecordInfo .RecordDateTime),
                //new SQLiteParameter("@RecordDateTime",),
                new SQLiteParameter("@UserID",GlobalData.RecordInfo.UserID),
                new SQLiteParameter("@InferenceState",GlobalData.RecordInfo.InferenceState),
                new SQLiteParameter("@KnowledgeVersion",GlobalData.RecordInfo.KnowledgeVersion),
                new SQLiteParameter("@IEVersion",GlobalData.RecordInfo.IEVersion)       
			};
            try
            {
                int newId = SQLServerDBInterface.GetScalar(sql, para);
                if (newId > 0)
                {
                    GlobalData.RecordInfo.RecordSeq = newId;
                    return true;
                }
               else
                {
                 LastErrorInfo = "保存索引表信息失败！";
                   return false;
                }
            }
            catch (SQLiteException e)
            {
                LastErrorInfo = e.Message;
                return false;
            }

        }
        #endregion

        #region 病人基本信息
        /// <summary>
        /// 查询病人基本信息
        /// </summary>
        /// <param name="PatSEQ"></param>
        /// <returns></returns>
        private static bool GetBasicinfo(string RecordSEQ)
        {
            DESClass DESClass = new DESClass();
            //恢复默认值
            GlobalData.PatBasicInfo.Clear();

            string sql = "select * from CDSS_PatBasicInfo,CDSS_RecordHistory where CDSS_PatBasicInfo.PatSEQ=CDSS_RecordHistory.PatSEQ and RecordSEQ=" + RecordSEQ;

            SQLiteDataReader reader = SQLServerDBInterface.GetReader(sql);
            if (reader.Read())
            {
                GlobalData.PatBasicInfo.PatSEQ = reader["PatSEQ"].ToString();
                GlobalData.PatBasicInfo.PatAddress = (string)reader["PatAddress"];
                GlobalData.PatBasicInfo.PatBirthCity = (string)reader["PatBirthCity"];
                //GlobalData.PatBasicInfo.PatBirthday = (DateTime)reader["PatBirthday"];
                string PatBirthday = DESClass.DESDecrypt(reader["PatBirthday"].ToString());
                if (PatBirthday != "")
                {
                    GlobalData.PatBasicInfo.PatBirthday = DateTime.Parse(PatBirthday);
                }
                else
                {
                    GlobalData.PatBasicInfo.PatBirthday = DateTime.Parse(DateTime.Now.ToShortDateString());
                }
                GlobalData.PatBasicInfo.PatBirthProvince = (string)reader["PatBirthProvince"];
                GlobalData.PatBasicInfo.PatChildCount = (int)reader["PatChildCount"];
                GlobalData.PatBasicInfo.PatEducationLevel = (string)reader["PatEducationLevel"];
                GlobalData.PatBasicInfo.PatID = (string)reader["PatID"];
                GlobalData.PatBasicInfo.PatIncome = (string)reader["PatIncome"];
                GlobalData.PatBasicInfo.PatIncomeSource = (string)reader["PatIncomeSource"];
                string PatName = DESClass.DESDecrypt((string)reader["PatName"]);
                if (PatName != "")
                {
                    if (PatName.Trim().ToString().IndexOf('\0') > 0)
                    {
                        GlobalData.PatBasicInfo.PatName = PatName.Substring(0, PatName.Trim().ToString().IndexOf('\0'));
                    }
                    else
                    {
                        GlobalData.PatBasicInfo.PatName = PatName;
                    }
                }
                else
                {
                    GlobalData.PatBasicInfo.PatName = PatName;
                }
                //GlobalData.PatBasicInfo.PatName = (string)reader["PatName"];
                GlobalData.PatBasicInfo.PatNational = (string)reader["PatNational"];
                GlobalData.PatBasicInfo.PatPhone = (string)reader["PatPhone"];
                GlobalData.PatBasicInfo.PatProfessional = (string)reader["PatProfessional"];
                GlobalData.PatBasicInfo.PatSex = (string)reader["PatSex"];
                GlobalData.PatBasicInfo.PatSiblingsCount = (int)reader["PatSiblingsCount"];
                GlobalData.PatBasicInfo.PatTreatmentCost = (string)reader["PatTreatmentCost"];
                if (reader["PatVisitDateTime"] != Convert.DBNull)
                {
                    GlobalData.PatBasicInfo.PatVisitDateTime = (DateTime)reader["PatVisitDateTime"];
                }
                GlobalData.PatBasicInfo.PatZipcode = (string)reader["PatZipcode"];

                reader.Close();
            }
            else
            {
                reader.Close();
                LastErrorInfo = "查询病人信息失败！";
                return false;
            }
            return true;
        }
        private static bool GetBasicinfo(string RecordSEQ, classSQLServerDBInterface anotherSQLServerInterface)
        {
            DESClass DESClass = new DESClass();
            //恢复默认值
            GlobalData.PatBasicInfo.Clear();

            string sql = "select * from CDSS_PatBasicInfo,CDSS_RecordHistory where CDSS_PatBasicInfo.PatSEQ=CDSS_RecordHistory.PatSEQ and RecordSEQ=" + RecordSEQ;

            SQLiteDataReader reader = anotherSQLServerInterface.GetReader(sql);
            if (reader.Read())
            {
                GlobalData.PatBasicInfo.PatSEQ = reader["PatSEQ"].ToString();
                GlobalData.PatBasicInfo.PatAddress = (string)reader["PatAddress"];
                GlobalData.PatBasicInfo.PatBirthCity = (string)reader["PatBirthCity"];
                //GlobalData.PatBasicInfo.PatBirthday = (DateTime)reader["PatBirthday"];
                string PatBirthday = DESClass.DESDecrypt(reader["PatBirthday"].ToString());
                if (PatBirthday != "")
                {
                    GlobalData.PatBasicInfo.PatBirthday = DateTime.Parse(PatBirthday);
                }
                else
                {
                    GlobalData.PatBasicInfo.PatBirthday = DateTime.Parse(DateTime.Now.ToShortDateString());
                }
                GlobalData.PatBasicInfo.PatBirthProvince = (string)reader["PatBirthProvince"];
                GlobalData.PatBasicInfo.PatChildCount = (int)reader["PatChildCount"];
                GlobalData.PatBasicInfo.PatEducationLevel = (string)reader["PatEducationLevel"];
                GlobalData.PatBasicInfo.PatID = (string)reader["PatID"];
                GlobalData.PatBasicInfo.PatIncome = (string)reader["PatIncome"];
                GlobalData.PatBasicInfo.PatIncomeSource = (string)reader["PatIncomeSource"];
                string PatName = DESClass.DESDecrypt((string)reader["PatName"]);
                if (PatName != "")
                {
                    if (PatName.Trim().ToString().IndexOf('\0') > 0)
                    {
                        GlobalData.PatBasicInfo.PatName = PatName.Substring(0, PatName.Trim().ToString().IndexOf('\0'));
                    }
                    else
                    {
                        GlobalData.PatBasicInfo.PatName = PatName;
                    }
                }
                else
                {
                    GlobalData.PatBasicInfo.PatName = PatName;
                }
                //GlobalData.PatBasicInfo.PatName = (string)reader["PatName"];
                GlobalData.PatBasicInfo.PatNational = (string)reader["PatNational"];
                GlobalData.PatBasicInfo.PatPhone = (string)reader["PatPhone"];
                GlobalData.PatBasicInfo.PatProfessional = (string)reader["PatProfessional"];
                GlobalData.PatBasicInfo.PatSex = (string)reader["PatSex"];
                GlobalData.PatBasicInfo.PatSiblingsCount = (int)reader["PatSiblingsCount"];
                GlobalData.PatBasicInfo.PatTreatmentCost = (string)reader["PatTreatmentCost"];
                if (reader["PatVisitDateTime"] != Convert.DBNull)
                {
                    GlobalData.PatBasicInfo.PatVisitDateTime = (DateTime)reader["PatVisitDateTime"];
                }
                GlobalData.PatBasicInfo.PatZipcode = (string)reader["PatZipcode"];

                reader.Close();
            }
            else
            {
                reader.Close();
                LastErrorInfo = "查询病人信息失败！";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存病人基本信息
        /// </summary>
        /// <param name="PatSEQ"></param>
        /// <returns></returns>
        private static bool SaveBasicinfo()
        {
            DESClass DESClass = new DESClass();
            //新入病人在数据库中无PatSEQ，需要先插入CDSS_PatBasicInfo，获得
            if (GlobalData.PatBasicInfo.PatSEQ.Equals("*"))
            {
                string sqlstr = "insert into CDSS_PatBasicInfo(PatName,PatSex,PatNational,PatBirthday) values(@PatName,@PatSex,@PatNational,@PatBirthday);select LAST_INSERT_ROWID();";
                SQLiteParameter[] parater = new SQLiteParameter[]
			    {
                    new SQLiteParameter("@PatName",DESClass.DESEncrypt(GlobalData.PatBasicInfo.PatName)),
                    new SQLiteParameter("@PatSex",GlobalData.PatBasicInfo.PatSex),
                    new SQLiteParameter("@PatNational",GlobalData.PatBasicInfo.PatNational),
                    new SQLiteParameter("@PatBirthday",DESClass.DESEncrypt(GlobalData.PatBasicInfo.PatBirthday.ToString())),
                };
                GlobalData.PatBasicInfo.PatSEQ = SQLServerDBInterface.GetDataSet(sqlstr, parater).Rows[0][0].ToString();
            }
            //string sql = "insert into CDSS_PatBasicInfo(RecordSEQ,PatVisitDateTime,PatID,PatName,PatSex,PatEducationLevel,PatNational,"
            //                 + "PatIncomeSource,PatProfessional,PatTreatmentCost,PatIncome,PatZipcode,PatBirthday,"
            //                 + "PatPhone,PatBirthProvince,PatBirthCity,PatAddress,PatChildCount,PatSiblingsCount) "
            //                 + " values(@RecordSEQ,@PatVisitDateTime,@PatID,@PatName,@PatSex,@PatEducationLevel,@PatNational,"
            //                 + "@PatIncomeSource,@PatProfessional,@PatTreatmentCost,@PatIncome,@PatZipcode,@PatBirthday,"
            //                 + "@PatPhone,@PatBirthProvince,@PatBirthCity,@PatAddress,@PatChildCount,@PatSiblingsCount)";            
            string sql = " update CDSS_PatBasicInfo set PatID=@PatID,PatName=@PatName,"
                            + "PatSex=@PatSex,PatEducationLevel=@PatEducationLevel,PatNational=@PatNational,"
                            + "PatIncomeSource=@PatIncomeSource,PatProfessional=@PatProfessional,"
                            + "PatTreatmentCost=@PatTreatmentCost,PatIncome=@PatIncome,PatZipcode=@PatZipcode,"
                            + "PatBirthday=@PatBirthday,PatPhone=@PatPhone,PatBirthProvince=@PatBirthProvince,"
                            + "PatBirthCity=@PatBirthCity,PatAddress=@PatAddress,PatChildCount=@PatChildCount,"
                            + "PatSiblingsCount=@PatSiblingsCount where PatSEQ=@PatSEQ;"
                            + "insert into CDSS_RecordHistory(RecordSEQ,PatSEQ,PatVisitDateTime) "
                            + "values(@RecordSEQ,@PatSEQ,@PatVisitDateTime)";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@PatSEQ",GlobalData.PatBasicInfo.PatSEQ),
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@PatVisitDateTime",GlobalData.PatBasicInfo.PatVisitDateTime.ToString("s")),
                new SQLiteParameter("@PatID",GlobalData.PatBasicInfo.PatID),
                new SQLiteParameter("@PatName",DESClass.DESEncrypt(GlobalData.PatBasicInfo.PatName)),
               // new SqlParameter("@PatName",GlobalData.PatBasicInfo.PatName),
                new SQLiteParameter("@PatSex",GlobalData.PatBasicInfo.PatSex),
                new SQLiteParameter("@PatEducationLevel",GlobalData.PatBasicInfo.PatEducationLevel),
                new SQLiteParameter("@PatNational",GlobalData.PatBasicInfo.PatNational),
                new SQLiteParameter("@PatIncomeSource",GlobalData.PatBasicInfo.PatIncomeSource),
                new SQLiteParameter("@PatProfessional",GlobalData.PatBasicInfo.PatProfessional),
                new SQLiteParameter("@PatTreatmentCost",GlobalData.PatBasicInfo.PatTreatmentCost),
                new SQLiteParameter("@PatIncome",GlobalData.PatBasicInfo.PatIncome),
                new SQLiteParameter("@PatZipcode",GlobalData.PatBasicInfo.PatZipcode),
                //new SqlParameter("@PatBirthday",GlobalData.PatBasicInfo.PatBirthday.ToString()),
                new SQLiteParameter("@PatBirthday",DESClass.DESEncrypt(GlobalData.PatBasicInfo.PatBirthday.ToString())),
                new SQLiteParameter("@PatPhone",GlobalData.PatBasicInfo.PatPhone),
                new SQLiteParameter("@PatBirthProvince",GlobalData.PatBasicInfo.PatBirthProvince),
                new SQLiteParameter("@PatBirthCity",GlobalData.PatBasicInfo.PatBirthCity),
                new SQLiteParameter("@PatAddress",GlobalData.PatBasicInfo.PatAddress),
                new SQLiteParameter("@PatChildCount",GlobalData.PatBasicInfo.PatChildCount.ToString ()),
                new SQLiteParameter("@PatSiblingsCount",GlobalData.PatBasicInfo.PatSiblingsCount.ToString())
                
			};
            try
            {
                //string n;
              //  n = GlobalData.PatBasicInfo.PatVisitDateTime.AddHours(1).ToString("s");
                int result = SQLServerDBInterface.ExecuteCommand(sql, para);
                if (result > 0)
                    return true;
                else
                {
                    LastErrorInfo = "保存病人信息失败！";
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                LastErrorInfo = e.Message;
                return false;
            }
        }

        #endregion

        #region 糖代谢异常信息
        /// <summary>
        /// 查询糖代谢异常信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetAGMInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.AGMInfo.Clear();

            string sqlAGM = "select * from CDSS_AGMInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='AGMInfo' and RecordSEQ=" + RecordSEQ;
            string sqlMedicineInfo = "select * from CDSS_MedicineInfo where TableName='AGMInfo' and RecordSEQ=" + RecordSEQ;
            DataTable TableAGM = SQLServerDBInterface.GetDataSet(sqlAGM);
            if (TableAGM.Rows.Count != 1)    // 判读AGM信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询糖代谢异常信息失败！";
                return false;
            }

            foreach (DataRow Row in TableAGM.Rows)
            {
                GlobalData.AGMInfo.AbnormalDetectedDateTime = (DateTime)Row["AbnormalDetectedDateTime"];

                if (Row["HasAGMAbnormal"].ToString().Trim().Equals("true"))
                    GlobalData.AGMInfo.HasAGMAbnormal = true;
                else
                    GlobalData.AGMInfo.HasAGMAbnormal = false;
            }

            DataTable TableSymptomsInfo = SQLServerDBInterface.GetDataSet(sqlSymptomsInfo);
            DataTable TableMedicineInfo = SQLServerDBInterface.GetDataSet(sqlMedicineInfo);

            foreach (DataRow Row in TableSymptomsInfo.Rows)
            {
                if (Row["Types"].ToString().Trim().Equals("ConfirmedSymptoms"))  //糖代谢确诊类型
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                    Symptoms.SymptomsName = (string)Row["SymptomsName"];

                    GlobalData.AGMInfo.listConfirmedSymptoms.Add(Symptoms);
                }
                else if (Row["Types"].ToString().Trim().Equals("AcuteSymptoms"))          //急性并发症类型
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                    Symptoms.SymptomsName = (string)Row["SymptomsName"];

                    GlobalData.AGMInfo.listAcuteSymptoms.Add(Symptoms);
                }
                else if (Row["Types"].ToString().Trim().Equals("ChronicSymptoms"))          //慢性并发症类型
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                    Symptoms.SymptomsName = (string)Row["SymptomsName"];

                    GlobalData.AGMInfo.listChronicSymptoms.Add(Symptoms);
                }
            }

            foreach (DataRow Row in TableMedicineInfo.Rows)
            {
                if (Row["Types"].ToString().Trim().Equals("HypogMedicineInfo"))  //降糖药
                {
                    CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                    Medicine.Drugtype = (string)Row["Drugtype"];
                    Medicine.DrugNames = (string)Row["DrugNames"];
                    Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                    Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                    Medicine.DrugAmount = (string)Row["DrugAmount"];
                    Medicine.DrugUnits = (string)Row["DrugUnits"];
                    Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                    Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                    GlobalData.AGMInfo.listHypogMedicineInfo.Add(Medicine);
                }
                else if (Row["Types"].ToString().Trim().Equals("ChineseMedicineInfo"))          //中成药
                {
                    CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                    Medicine.Drugtype = (string)Row["Drugtype"];
                    Medicine.DrugNames = (string)Row["DrugNames"];
                    Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                    Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                    Medicine.DrugAmount = (string)Row["DrugAmount"];
                    Medicine.DrugUnits = (string)Row["DrugUnits"];
                    Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                    Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                    GlobalData.AGMInfo.listChineseMedicineInfo.Add(Medicine);

                }
                else if (Row["Types"].ToString().Trim().Equals("InsulinMedicineInfo"))          //胰岛素
                {
                    CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                    Medicine.Drugtype = (string)Row["Drugtype"];
                    Medicine.DrugNames = (string)Row["DrugNames"];
                    Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                    Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                    Medicine.DrugAmount = (string)Row["DrugAmount"];
                    Medicine.DrugUnits = (string)Row["DrugUnits"];
                    Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                    Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                    GlobalData.AGMInfo.listInsulinMedicineInfo.Add(Medicine);
                }
            }
            return true;

        }
        private static bool GetAGMInfo(string RecordSEQ, classSQLServerDBInterface anotherSQLServerInterface)
        {
            //恢复默认值
            GlobalData.AGMInfo.Clear();

            string sqlAGM = "select * from CDSS_AGMInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='AGMInfo' and RecordSEQ=" + RecordSEQ;
            string sqlMedicineInfo = "select * from CDSS_MedicineInfo where TableName='AGMInfo' and RecordSEQ=" + RecordSEQ;
            DataTable TableAGM = anotherSQLServerInterface.GetDataSet(sqlAGM);
            if (TableAGM.Rows.Count != 1)    // 判读AGM信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询糖代谢异常信息失败！";
                return false;
            }

            foreach (DataRow Row in TableAGM.Rows)
            {
                GlobalData.AGMInfo.AbnormalDetectedDateTime = (DateTime)Row["AbnormalDetectedDateTime"];

                if (Row["HasAGMAbnormal"].ToString().Trim().Equals("true"))
                    GlobalData.AGMInfo.HasAGMAbnormal = true;
                else
                    GlobalData.AGMInfo.HasAGMAbnormal = false;
            }

            DataTable TableSymptomsInfo = anotherSQLServerInterface.GetDataSet(sqlSymptomsInfo);
            DataTable TableMedicineInfo = anotherSQLServerInterface.GetDataSet(sqlMedicineInfo);

            foreach (DataRow Row in TableSymptomsInfo.Rows)
            {
                if (Row["Types"].ToString().Trim().Equals("ConfirmedSymptoms"))  //糖代谢确诊类型
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                    Symptoms.SymptomsName = (string)Row["SymptomsName"];

                    GlobalData.AGMInfo.listConfirmedSymptoms.Add(Symptoms);
                }
                else if (Row["Types"].ToString().Trim().Equals("AcuteSymptoms"))          //急性并发症类型
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                    Symptoms.SymptomsName = (string)Row["SymptomsName"];

                    GlobalData.AGMInfo.listAcuteSymptoms.Add(Symptoms);
                }
                else if (Row["Types"].ToString().Trim().Equals("ChronicSymptoms"))          //慢性并发症类型
                {
                    CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                    Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                    Symptoms.SymptomsName = (string)Row["SymptomsName"];

                    GlobalData.AGMInfo.listChronicSymptoms.Add(Symptoms);
                }
            }

            foreach (DataRow Row in TableMedicineInfo.Rows)
            {
                if (Row["Types"].ToString().Trim().Equals("HypogMedicineInfo"))  //降糖药
                {
                    CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                    Medicine.Drugtype = (string)Row["Drugtype"];
                    Medicine.DrugNames = (string)Row["DrugNames"];
                    Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                    Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                    Medicine.DrugAmount = (string)Row["DrugAmount"];
                    Medicine.DrugUnits = (string)Row["DrugUnits"];
                    Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                    Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                    GlobalData.AGMInfo.listHypogMedicineInfo.Add(Medicine);
                }
                else if (Row["Types"].ToString().Trim().Equals("ChineseMedicineInfo"))          //中成药
                {
                    CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                    Medicine.Drugtype = (string)Row["Drugtype"];
                    Medicine.DrugNames = (string)Row["DrugNames"];
                    Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                    Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                    Medicine.DrugAmount = (string)Row["DrugAmount"];
                    Medicine.DrugUnits = (string)Row["DrugUnits"];
                    Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                    Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                    GlobalData.AGMInfo.listChineseMedicineInfo.Add(Medicine);

                }
                else if (Row["Types"].ToString().Trim().Equals("InsulinMedicineInfo"))          //胰岛素
                {
                    CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                    Medicine.Drugtype = (string)Row["Drugtype"];
                    Medicine.DrugNames = (string)Row["DrugNames"];
                    Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                    Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                    Medicine.DrugAmount = (string)Row["DrugAmount"];
                    Medicine.DrugUnits = (string)Row["DrugUnits"];
                    Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                    Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                    GlobalData.AGMInfo.listInsulinMedicineInfo.Add(Medicine);
                }
            }
            return true;

        }

        /// <summary>
        /// 保存糖代谢异常信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveAGMInfo()
        {
            string sqlAGM = "insert into CDSS_AGMInfo(RecordSEQ,HasAGMAbnormal,AbnormalDetectedDateTime) "
                             + " values(@RecordSEQ,@HasAGMAbnormal,@AbnormalDetectedDateTime) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@HasAGMAbnormal",GlobalData.AGMInfo.HasAGMAbnormal.ToString().ToLower()),
                new SQLiteParameter("@AbnormalDetectedDateTime",GlobalData.AGMInfo.AbnormalDetectedDateTime)    
			};

            string sqlConfirmedSymptoms = "";
            string sqlAcuteSymptoms = "";
            string sqlChronicSymptoms = "";
            string sqlHypogMedicineInfo = "";
            string sqlChineseMedicineInfo = "";
            string sqlInsulinMedicineInfo = "";

            //糖尿病确诊类型
            if (GlobalData.AGMInfo.listConfirmedSymptoms.Count > 0)
            {
                for (int i = 0; i < GlobalData.AGMInfo.listConfirmedSymptoms.Count; i++)
                {
                  
                    sqlConfirmedSymptoms += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'AGMInfo','ConfirmedSymptoms','"
                    + GlobalData.AGMInfo.listConfirmedSymptoms[i].SymptomsName + "','"
                    + GlobalData.AGMInfo.listConfirmedSymptoms[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }
            //急性并发症类型
            if (GlobalData.AGMInfo.listAcuteSymptoms.Count > 0)
            {
                for (int i = 0; i < GlobalData.AGMInfo.listAcuteSymptoms.Count; i++)
                {
                    sqlAcuteSymptoms += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'AGMInfo','AcuteSymptoms','"
                    + GlobalData.AGMInfo.listAcuteSymptoms[i].SymptomsName + "','"
                    + GlobalData.AGMInfo.listAcuteSymptoms[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }
            //慢性并发症类型
            if (GlobalData.AGMInfo.listChronicSymptoms.Count > 0)
            {
                for (int i = 0; i < GlobalData.AGMInfo.listChronicSymptoms.Count; i++)
                {
                    sqlChronicSymptoms += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'AGMInfo','ChronicSymptoms','"
                    + GlobalData.AGMInfo.listChronicSymptoms[i].SymptomsName + "','"
                    + GlobalData.AGMInfo.listChronicSymptoms[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }
            //降糖药
            if (GlobalData.AGMInfo.listHypogMedicineInfo.Count > 0)
            {
                for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                {
                    sqlHypogMedicineInfo += "insert into CDSS_MedicineInfo(RecordSEQ,TableName,Types,Drugtype,DrugNames,DrugBeginTime,"
                       + "DrugEndTime,DrugAmount,DrugUnits,DrugByRoute,DrugFrequency)"
                       + "values(" + GlobalData.RecordInfo.RecordSeq
                       + " ,'AGMInfo','HypogMedicineInfo','"
                       + GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype + "','"
                       + GlobalData.AGMInfo.listHypogMedicineInfo[i].DrugNames + "','"
                       + GlobalData.AGMInfo.listHypogMedicineInfo[i].DrugBeginTime.ToString("s") + "','"
                       + GlobalData.AGMInfo.listHypogMedicineInfo[i].DrugEndTime.ToString("s")+ "','"
                       + GlobalData.AGMInfo.listHypogMedicineInfo[i].DrugAmount + "','"
                       + GlobalData.AGMInfo.listHypogMedicineInfo[i].DrugUnits + "','"
                       + GlobalData.AGMInfo.listHypogMedicineInfo[i].DrugByRoute + "','"
                       + GlobalData.AGMInfo.listHypogMedicineInfo[i].DrugFrequency + "');";
                }
            }
            //中成药
            if (GlobalData.AGMInfo.listChineseMedicineInfo.Count > 0)
            {
                for (int i = 0; i < GlobalData.AGMInfo.listChineseMedicineInfo.Count; i++)
                {
                    sqlChineseMedicineInfo += "insert into CDSS_MedicineInfo(RecordSEQ,TableName,Types,Drugtype,DrugNames,DrugBeginTime,"
                       + "DrugEndTime,DrugAmount,DrugUnits,DrugByRoute,DrugFrequency)"
                       + "values(" + GlobalData.RecordInfo.RecordSeq
                       + " ,'AGMInfo','ChineseMedicineInfo','"
                       + GlobalData.AGMInfo.listChineseMedicineInfo[i].Drugtype + "','"
                       + GlobalData.AGMInfo.listChineseMedicineInfo[i].DrugNames + "','"
                       + GlobalData.AGMInfo.listChineseMedicineInfo[i].DrugBeginTime.ToString("s") + "','"
                       + GlobalData.AGMInfo.listChineseMedicineInfo[i].DrugEndTime.ToString("s") + "','"
                       + GlobalData.AGMInfo.listChineseMedicineInfo[i].DrugAmount + "','"
                       + GlobalData.AGMInfo.listChineseMedicineInfo[i].DrugUnits + "','"
                       + GlobalData.AGMInfo.listChineseMedicineInfo[i].DrugByRoute + "','"
                       + GlobalData.AGMInfo.listChineseMedicineInfo[i].DrugFrequency + "');";
                }
            }
            //胰岛素
            if (GlobalData.AGMInfo.listInsulinMedicineInfo.Count > 0)
            {
                for (int i = 0; i < GlobalData.AGMInfo.listInsulinMedicineInfo.Count; i++)
                {
                    sqlInsulinMedicineInfo += "insert into CDSS_MedicineInfo(RecordSEQ,TableName,Types,Drugtype,DrugNames,DrugBeginTime,"
                       + "DrugEndTime,DrugAmount,DrugUnits,DrugByRoute,DrugFrequency)"
                       + "values(" + GlobalData.RecordInfo.RecordSeq
                       + " ,'AGMInfo','InsulinMedicineInfo','"
                       + GlobalData.AGMInfo.listInsulinMedicineInfo[i].Drugtype + "','"
                       + GlobalData.AGMInfo.listInsulinMedicineInfo[i].DrugNames + "','"
                       + GlobalData.AGMInfo.listInsulinMedicineInfo[i].DrugBeginTime.ToString("s") + "','"
                       + GlobalData.AGMInfo.listInsulinMedicineInfo[i].DrugEndTime.ToString("s")+ "','"
                       + GlobalData.AGMInfo.listInsulinMedicineInfo[i].DrugAmount + "','"
                       + GlobalData.AGMInfo.listInsulinMedicineInfo[i].DrugUnits + "','"
                       + GlobalData.AGMInfo.listInsulinMedicineInfo[i].DrugByRoute + "','"
                       + GlobalData.AGMInfo.listInsulinMedicineInfo[i].DrugFrequency + "');";
                }
            }

            SQLiteCommand sqlCmd = SQLServerDBInterface.Connection.CreateCommand();
            sqlCmd.Connection = SQLServerDBInterface.Connection;
            sqlCmd.Transaction = SQLServerDBInterface.Connection.BeginTransaction();
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                sqlCmd.CommandText = sqlAGM;
                sqlCmd.Parameters.AddRange(para);
                sqlCmd.ExecuteNonQuery();

                if (sqlConfirmedSymptoms != "")
                {
                    sqlCmd.CommandText = sqlConfirmedSymptoms;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlAcuteSymptoms != "")
                {
                    sqlCmd.CommandText = sqlAcuteSymptoms;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlChronicSymptoms != "")
                {
                    sqlCmd.CommandText = sqlChronicSymptoms;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlHypogMedicineInfo != "")
                {
                    sqlCmd.CommandText = sqlHypogMedicineInfo;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlChineseMedicineInfo != "")
                {
                    sqlCmd.CommandText = sqlChineseMedicineInfo;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlInsulinMedicineInfo != "")
                {
                    sqlCmd.CommandText = sqlInsulinMedicineInfo;
                    sqlCmd.ExecuteNonQuery();
                }
                sqlCmd.Transaction.Commit();       //接受交易，完成操作
                return true;

            }
            catch (SQLiteException e)
            {
                sqlCmd.Transaction.Rollback();
                LastErrorInfo = e.Message;
                return false;
            }


        }

        #endregion

        #region 高血压信息
        /// <summary>
        /// 查询高血压信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetHypertensionInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.HypertensionInfo.Clear();

            string sqlHypertension = "select * from CDSS_HypertensionInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='HypertensionInfo' and RecordSEQ=" + RecordSEQ;
            string sqlMedicineInfo = "select * from CDSS_MedicineInfo where TableName='HypertensionInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableHypertension = SQLServerDBInterface.GetDataSet(sqlHypertension);
            if (TableHypertension.Rows.Count != 1)    // 判读Hypertension信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询高血压信息失败！";
                return false;
            }

            foreach (DataRow Row in TableHypertension.Rows)
            {
                if (Row["HasHypertension"].ToString().Trim().Equals("true"))
                    GlobalData.HypertensionInfo.HasHypertension = true;
                else
                    GlobalData.HypertensionInfo.HasHypertension = false;

                GlobalData.HypertensionInfo.MaxDBP = (string)Row["MaxDBP"];
                GlobalData.HypertensionInfo.MaxSBP = (string)Row["MaxSBP"];
                GlobalData.HypertensionInfo.MinDBP = (string)Row["MinDBP"];
                GlobalData.HypertensionInfo.MinSBP = (string)Row["MinSBP"];
                GlobalData.HypertensionInfo.BPControlFromYear = (string)Row["BPControlFromYear"];
                GlobalData.HypertensionInfo.BPControlToYear = (string)Row["BPControlToYear"];
                GlobalData.HypertensionInfo.PeacetimeMaxDBP = (string)Row["PeacetimeMaxDBP"];
                GlobalData.HypertensionInfo.PeacetimeMaxSBP = (string)Row["PeacetimeMaxSBP"];
                GlobalData.HypertensionInfo.PeacetimeMinDBP = (string)Row["PeacetimeMinDBP"];
                GlobalData.HypertensionInfo.PeacetimeMinSBP = (string)Row["PeacetimeMinSBP"];
            }

            DataTable TableSymptomsInfo = SQLServerDBInterface.GetDataSet(sqlSymptomsInfo);
            DataTable TableMedicineInfo = SQLServerDBInterface.GetDataSet(sqlMedicineInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("HypertensionSymptoms"))  //高血压类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.HypertensionInfo.listHypertensionSymptoms.Add(Symptoms);
                    }
                }
            }
            if (TableMedicineInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableMedicineInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("StepDownWestMedicine"))  //降压西药
                    {
                        CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                        Medicine.Drugtype = (string)Row["Drugtype"];
                        Medicine.DrugNames = (string)Row["DrugNames"];
                        Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                        Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                        Medicine.DrugAmount = (string)Row["DrugAmount"];
                        Medicine.DrugUnits = (string)Row["DrugUnits"];
                        Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                        Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                        GlobalData.HypertensionInfo.listStepDownWestMedicine.Add(Medicine);
                    }
                    else if (Row["Types"].ToString().Trim().Equals("StepDownChineseMedication"))          //中成药
                    {
                        CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                        Medicine.Drugtype = (string)Row["Drugtype"];
                        Medicine.DrugNames = (string)Row["DrugNames"];
                        Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                        Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                        Medicine.DrugAmount = (string)Row["DrugAmount"];
                        Medicine.DrugUnits = (string)Row["DrugUnits"];
                        Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                        Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                        GlobalData.HypertensionInfo.listStepDownChineseMedication.Add(Medicine);

                    }
                }
            }
            return true;
        }
        private static bool GetHypertensionInfo(string RecordSEQ, classSQLServerDBInterface anotherSQLServerInterface)
        {
            //恢复默认值
            GlobalData.HypertensionInfo.Clear();

            string sqlHypertension = "select * from CDSS_HypertensionInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='HypertensionInfo' and RecordSEQ=" + RecordSEQ;
            string sqlMedicineInfo = "select * from CDSS_MedicineInfo where TableName='HypertensionInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableHypertension = anotherSQLServerInterface.GetDataSet(sqlHypertension);
            if (TableHypertension.Rows.Count != 1)    // 判读Hypertension信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询高血压信息失败！";
                return false;
            }

            foreach (DataRow Row in TableHypertension.Rows)
            {
                if (Row["HasHypertension"].ToString().Trim().Equals("true"))
                    GlobalData.HypertensionInfo.HasHypertension = true;
                else
                    GlobalData.HypertensionInfo.HasHypertension = false;

                GlobalData.HypertensionInfo.MaxDBP = (string)Row["MaxDBP"];
                GlobalData.HypertensionInfo.MaxSBP = (string)Row["MaxSBP"];
                GlobalData.HypertensionInfo.MinDBP = (string)Row["MinDBP"];
                GlobalData.HypertensionInfo.MinSBP = (string)Row["MinSBP"];
                GlobalData.HypertensionInfo.BPControlFromYear = (string)Row["BPControlFromYear"];
                GlobalData.HypertensionInfo.BPControlToYear = (string)Row["BPControlToYear"];
                GlobalData.HypertensionInfo.PeacetimeMaxDBP = (string)Row["PeacetimeMaxDBP"];
                GlobalData.HypertensionInfo.PeacetimeMaxSBP = (string)Row["PeacetimeMaxSBP"];
                GlobalData.HypertensionInfo.PeacetimeMinDBP = (string)Row["PeacetimeMinDBP"];
                GlobalData.HypertensionInfo.PeacetimeMinSBP = (string)Row["PeacetimeMinSBP"];
            }

            DataTable TableSymptomsInfo = anotherSQLServerInterface.GetDataSet(sqlSymptomsInfo);
            DataTable TableMedicineInfo = anotherSQLServerInterface.GetDataSet(sqlMedicineInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("HypertensionSymptoms"))  //高血压类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.HypertensionInfo.listHypertensionSymptoms.Add(Symptoms);
                    }
                }
            }
            if (TableMedicineInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableMedicineInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("StepDownWestMedicine"))  //降压西药
                    {
                        CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                        Medicine.Drugtype = (string)Row["Drugtype"];
                        Medicine.DrugNames = (string)Row["DrugNames"];
                        Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                        Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                        Medicine.DrugAmount = (string)Row["DrugAmount"];
                        Medicine.DrugUnits = (string)Row["DrugUnits"];
                        Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                        Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                        GlobalData.HypertensionInfo.listStepDownWestMedicine.Add(Medicine);
                    }
                    else if (Row["Types"].ToString().Trim().Equals("StepDownChineseMedication"))          //中成药
                    {
                        CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                        Medicine.Drugtype = (string)Row["Drugtype"];
                        Medicine.DrugNames = (string)Row["DrugNames"];
                        Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                        Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                        Medicine.DrugAmount = (string)Row["DrugAmount"];
                        Medicine.DrugUnits = (string)Row["DrugUnits"];
                        Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                        Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                        GlobalData.HypertensionInfo.listStepDownChineseMedication.Add(Medicine);

                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 保存高血压信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveHypertensionInfo()
        {
            string sqlHypertension = "insert into CDSS_HypertensionInfo(RecordSEQ,HasHypertension,MaxSBP,MaxDBP,MinSBP,MinDBP,"
                                   + " BPControlFromYear,BPControlToYear,PeacetimeMinSBP,PeacetimeMaxSBP,PeacetimeMinDBP, "
                                   + " PeacetimeMaxDBP) "
                                   + " values(@RecordSEQ,@HasHypertension,@MaxSBP,@MaxDBP,@MinSBP,@MinDBP,"
                                   + " @BPControlFromYear,@BPControlToYear,@PeacetimeMinSBP,@PeacetimeMaxSBP,@PeacetimeMinDBP, "
                                   + " @PeacetimeMaxDBP) ;";
            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@HasHypertension",GlobalData.HypertensionInfo.HasHypertension.ToString().ToLower()),
                new SQLiteParameter("@MaxSBP",GlobalData.HypertensionInfo.MaxSBP),
                new SQLiteParameter("@MaxDBP",GlobalData.HypertensionInfo.MaxDBP),
                new SQLiteParameter("@MinSBP",GlobalData.HypertensionInfo.MinSBP),
                new SQLiteParameter("@MinDBP",GlobalData.HypertensionInfo.MinDBP),
                new SQLiteParameter("@BPControlFromYear",GlobalData.HypertensionInfo.BPControlFromYear),
                new SQLiteParameter("@BPControlToYear",GlobalData.HypertensionInfo.BPControlToYear),
                new SQLiteParameter("@PeacetimeMinSBP",GlobalData.HypertensionInfo.PeacetimeMinSBP),
                new SQLiteParameter("@PeacetimeMaxSBP",GlobalData.HypertensionInfo.PeacetimeMaxSBP),
                new SQLiteParameter("@PeacetimeMinDBP",GlobalData.HypertensionInfo.PeacetimeMinDBP),
                new SQLiteParameter("@PeacetimeMaxDBP",GlobalData.HypertensionInfo.PeacetimeMaxDBP)
			};

            string sqlHypertensionSymptoms = "";
            string sqlStepDownChineseMedication = "";
            string sqlStepDownWestMedicine = "";

            //高血压类型
            if (GlobalData.HypertensionInfo.listHypertensionSymptoms.Count > 0)
            {
                for (int i = 0; i < GlobalData.HypertensionInfo.listHypertensionSymptoms.Count; i++)
                {
                    sqlHypertensionSymptoms += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'HypertensionInfo','HypertensionSymptoms','"
                    + GlobalData.HypertensionInfo.listHypertensionSymptoms[i].SymptomsName + "','"
                    + GlobalData.HypertensionInfo.listHypertensionSymptoms[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }
            //中成药
            if (GlobalData.HypertensionInfo.listStepDownChineseMedication.Count > 0)
            {
                for (int i = 0; i < GlobalData.HypertensionInfo.listStepDownChineseMedication.Count; i++)
                {
                    sqlStepDownChineseMedication += "insert into CDSS_MedicineInfo(RecordSEQ,TableName,Types,Drugtype,DrugNames,DrugBeginTime,"
                       + "DrugEndTime,DrugAmount,DrugUnits,DrugByRoute,DrugFrequency)"
                       + "values(" + GlobalData.RecordInfo.RecordSeq
                       + " ,'HypertensionInfo','StepDownChineseMedication','"
                       + GlobalData.HypertensionInfo.listStepDownChineseMedication[i].Drugtype + "','"
                       + GlobalData.HypertensionInfo.listStepDownChineseMedication[i].DrugNames + "','"
                       + GlobalData.HypertensionInfo.listStepDownChineseMedication[i].DrugBeginTime.ToString("s") + "','"
                       + GlobalData.HypertensionInfo.listStepDownChineseMedication[i].DrugEndTime.ToString("s")+ "','"
                       + GlobalData.HypertensionInfo.listStepDownChineseMedication[i].DrugAmount + "','"
                       + GlobalData.HypertensionInfo.listStepDownChineseMedication[i].DrugUnits + "','"
                       + GlobalData.HypertensionInfo.listStepDownChineseMedication[i].DrugByRoute + "','"
                       + GlobalData.HypertensionInfo.listStepDownChineseMedication[i].DrugFrequency + "');";
                }
            }
            //降压西药
            if (GlobalData.HypertensionInfo.listStepDownWestMedicine.Count > 0)
            {
                for (int i = 0; i < GlobalData.HypertensionInfo.listStepDownWestMedicine.Count; i++)
                {
                    sqlStepDownWestMedicine += "insert into CDSS_MedicineInfo(RecordSEQ,TableName,Types,Drugtype,DrugNames,DrugBeginTime,"
                       + "DrugEndTime,DrugAmount,DrugUnits,DrugByRoute,DrugFrequency)"
                       + "values(" + GlobalData.RecordInfo.RecordSeq
                       + " ,'HypertensionInfo','StepDownWestMedicine','"
                       + GlobalData.HypertensionInfo.listStepDownWestMedicine[i].Drugtype + "','"
                       + GlobalData.HypertensionInfo.listStepDownWestMedicine[i].DrugNames + "','"
                       + GlobalData.HypertensionInfo.listStepDownWestMedicine[i].DrugBeginTime.ToString("s") + "','"
                       + GlobalData.HypertensionInfo.listStepDownWestMedicine[i].DrugEndTime.ToString("s") + "','"
                       + GlobalData.HypertensionInfo.listStepDownWestMedicine[i].DrugAmount + "','"
                       + GlobalData.HypertensionInfo.listStepDownWestMedicine[i].DrugUnits + "','"
                       + GlobalData.HypertensionInfo.listStepDownWestMedicine[i].DrugByRoute + "','"
                       + GlobalData.HypertensionInfo.listStepDownWestMedicine[i].DrugFrequency + "');";
                }
            }

            SQLiteCommand sqlCmd = SQLServerDBInterface.Connection.CreateCommand();
            sqlCmd.Connection = SQLServerDBInterface.Connection;
            sqlCmd.Transaction = SQLServerDBInterface.Connection.BeginTransaction();
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                sqlCmd.CommandText = sqlHypertension;
                sqlCmd.Parameters.AddRange(para);
                sqlCmd.ExecuteNonQuery();

                if (sqlHypertensionSymptoms != "")
                {
                    sqlCmd.CommandText = sqlHypertensionSymptoms;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlStepDownChineseMedication != "")
                {
                    sqlCmd.CommandText = sqlStepDownChineseMedication;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlStepDownWestMedicine != "")
                {
                    sqlCmd.CommandText = sqlStepDownWestMedicine;
                    sqlCmd.ExecuteNonQuery();
                }
                sqlCmd.Transaction.Commit();       //接受交易，完成操作
                return true;

            }
            catch (SQLiteException e)
            {
                sqlCmd.Transaction.Rollback();
                LastErrorInfo = e.Message;
                return false;
            }
        }

        #endregion

        #region 血脂紊乱信息
        /// <summary>
        /// 查询血脂紊乱信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetDyslipidemiaInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.DyslipidemiaInfo.Clear();

            string sqlDyslipidemiaInfo = "select * from CDSS_DyslipidemiaInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='DyslipidemiaInfo' and RecordSEQ=" + RecordSEQ;
            string sqlMedicineInfo = "select * from CDSS_MedicineInfo where TableName='DyslipidemiaInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableDyslipidemiaInfo = SQLServerDBInterface.GetDataSet(sqlDyslipidemiaInfo);
            if (TableDyslipidemiaInfo.Rows.Count != 1)    // 判读DyslipidemiaInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询血脂紊乱信息失败！";
                return false;
            }

            foreach (DataRow Row in TableDyslipidemiaInfo.Rows)
            {
                if (Row["HasDyslipidemia"].ToString().Trim().Equals("true"))
                    GlobalData.DyslipidemiaInfo.HasDyslipidemia = true;
                else
                    GlobalData.DyslipidemiaInfo.HasDyslipidemia = false;
            }

            DataTable TableSymptomsInfo = SQLServerDBInterface.GetDataSet(sqlSymptomsInfo);
            DataTable TableMedicineInfo = SQLServerDBInterface.GetDataSet(sqlMedicineInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].Equals("DyslipidemiaSymptoms"))  //血脂紊乱类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Add(Symptoms);
                    }
                }
            }
            if (TableMedicineInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableMedicineInfo.Rows)
                {
                    if (Row["Types"].Equals("LipidlowerDrugs"))  //调脂药
                    {
                        CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                        Medicine.Drugtype = (string)Row["Drugtype"];
                        Medicine.DrugNames = (string)Row["DrugNames"];
                        Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                        Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                        Medicine.DrugAmount = (string)Row["DrugAmount"];
                        Medicine.DrugUnits = (string)Row["DrugUnits"];
                        Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                        Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                        GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Add(Medicine);
                    }
                }
            }
            return true;
        }
        private static bool GetDyslipidemiaInfo(string RecordSEQ,classSQLServerDBInterface anotherSQLServerInterface)
        {
            //恢复默认值
            GlobalData.DyslipidemiaInfo.Clear();

            string sqlDyslipidemiaInfo = "select * from CDSS_DyslipidemiaInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='DyslipidemiaInfo' and RecordSEQ=" + RecordSEQ;
            string sqlMedicineInfo = "select * from CDSS_MedicineInfo where TableName='DyslipidemiaInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableDyslipidemiaInfo = anotherSQLServerInterface.GetDataSet(sqlDyslipidemiaInfo);
            if (TableDyslipidemiaInfo.Rows.Count != 1)    // 判读DyslipidemiaInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询血脂紊乱信息失败！";
                return false;
            }

            foreach (DataRow Row in TableDyslipidemiaInfo.Rows)
            {
                if (Row["HasDyslipidemia"].ToString().Trim().Equals("true"))
                    GlobalData.DyslipidemiaInfo.HasDyslipidemia = true;
                else
                    GlobalData.DyslipidemiaInfo.HasDyslipidemia = false;
            }

            DataTable TableSymptomsInfo = anotherSQLServerInterface.GetDataSet(sqlSymptomsInfo);
            DataTable TableMedicineInfo = anotherSQLServerInterface.GetDataSet(sqlMedicineInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].Equals("DyslipidemiaSymptoms"))  //血脂紊乱类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Add(Symptoms);
                    }
                }
            }
            if (TableMedicineInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableMedicineInfo.Rows)
                {
                    if (Row["Types"].Equals("LipidlowerDrugs"))  //调脂药
                    {
                        CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                        Medicine.Drugtype = (string)Row["Drugtype"];
                        Medicine.DrugNames = (string)Row["DrugNames"];
                        Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                        Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                        Medicine.DrugAmount = (string)Row["DrugAmount"];
                        Medicine.DrugUnits = (string)Row["DrugUnits"];
                        Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                        Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                        GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Add(Medicine);
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// 保存血脂紊乱信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveDyslipidemiaInfo()
        {
            string sqlDyslipidemia = "insert into CDSS_DyslipidemiaInfo(RecordSEQ,HasDyslipidemia) "
                                   + " values(@RecordSEQ,@HasDyslipidemia) ;";
            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@HasDyslipidemia",GlobalData.DyslipidemiaInfo.HasDyslipidemia.ToString().ToLower())
			};

            string sqlDyslipidemiaSymptoms = "";
            string sqlLipidlowerDrugs = "";

            //血脂紊乱类型
            if (GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Count > 0)
            {
                for (int i = 0; i < GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms.Count; i++)
                {
                    sqlDyslipidemiaSymptoms += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'DyslipidemiaInfo','DyslipidemiaSymptoms','"
                    + GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsName + "','"
                    + GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }
            //调脂药
            if (GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Count > 0)
            {
                for (int i = 0; i < GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Count; i++)
                {
                    sqlLipidlowerDrugs += "insert into CDSS_MedicineInfo(RecordSEQ,TableName,Types,Drugtype,DrugNames,DrugBeginTime,"
                       + "DrugEndTime,DrugAmount,DrugUnits,DrugByRoute,DrugFrequency)"
                       + "values(" + GlobalData.RecordInfo.RecordSeq
                       + " ,'DyslipidemiaInfo','LipidlowerDrugs','"
                       + GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].Drugtype + "','"
                       + GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].DrugNames + "','"
                       + GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].DrugBeginTime.ToString("s") + "','"
                       + GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].DrugEndTime.ToString("s")+ "','"
                       + GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].DrugAmount + "','"
                       + GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].DrugUnits + "','"
                       + GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].DrugByRoute + "','"
                       + GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].DrugFrequency + "');";
                }
            }

            SQLiteCommand sqlCmd = SQLServerDBInterface.Connection.CreateCommand();
            sqlCmd.Connection = SQLServerDBInterface.Connection;
            sqlCmd.Transaction = SQLServerDBInterface.Connection.BeginTransaction();
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                sqlCmd.CommandText = sqlDyslipidemia;
                sqlCmd.Parameters.AddRange(para);
                sqlCmd.ExecuteNonQuery();

                if (sqlDyslipidemiaSymptoms != "")
                {
                    sqlCmd.CommandText = sqlDyslipidemiaSymptoms;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlLipidlowerDrugs != "")
                {
                    sqlCmd.CommandText = sqlLipidlowerDrugs;
                    sqlCmd.ExecuteNonQuery();
                }

                sqlCmd.Transaction.Commit();       //接受交易，完成操作
                return true;

            }
            catch (SQLiteException e)
            {
                sqlCmd.Transaction.Rollback();
                LastErrorInfo = e.Message;
                return false;
            }
        }

        #endregion

        #region 高尿酸血症信息
        /// <summary>
        /// 查询高尿酸血症信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetHyperuricemiaInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.HyperuricemiaInfo.Clear();

            string sqlHyperuricemiaInfo = "select * from CDSS_HyperuricemiaInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='HyperuricemiaInfo' and RecordSEQ=" + RecordSEQ;
            string sqlMedicineInfo = "select * from CDSS_MedicineInfo where TableName='HyperuricemiaInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableHyperuricemiaInfo = SQLServerDBInterface.GetDataSet(sqlHyperuricemiaInfo);
            if (TableHyperuricemiaInfo.Rows.Count != 1)    // 判读HyperuricemiaInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询高尿酸血症信息失败！";
                return false;
            }

            foreach (DataRow Row in TableHyperuricemiaInfo.Rows)
            {
                if (Row["HasHyperuricemia"].ToString().Trim().Equals("true"))
                    GlobalData.HyperuricemiaInfo.HasHyperuricemia = true;
                else
                    GlobalData.HyperuricemiaInfo.HasHyperuricemia = false;

                GlobalData.HyperuricemiaInfo.HyperuricemiaType = (string)Row["HyperuricemiaType"];

                if (Row["HasGoutyArthritis"].ToString().Trim().Equals("true"))
                    GlobalData.HyperuricemiaInfo.HasGoutyArthritis = true;
                else
                    GlobalData.HyperuricemiaInfo.HasGoutyArthritis = false;

                GlobalData.HyperuricemiaInfo.GoutyArthritisDetectedDateTime = (DateTime)Row["GoutyArthritisDetectedDateTime"];

                if (Row["HasTophus"].ToString().Trim().Equals("true"))
                    GlobalData.HyperuricemiaInfo.HasTophus = true;
                else
                    GlobalData.HyperuricemiaInfo.HasTophus = false;

                GlobalData.HyperuricemiaInfo.TophusPart = (string)Row["TophusPart"];

                if (Row["HasJointSwelling"].ToString().Trim().Equals("true"))
                    GlobalData.HyperuricemiaInfo.HasJointSwelling = true;
                else
                    GlobalData.HyperuricemiaInfo.HasJointSwelling = false;
            }

            DataTable TableSymptomsInfo = SQLServerDBInterface.GetDataSet(sqlSymptomsInfo);
            DataTable TableMedicineInfo = SQLServerDBInterface.GetDataSet(sqlMedicineInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("HyperuricemiaSymptoms"))  //高尿酸血症类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms.Add(Symptoms);
                    }
                }
            }
            if (TableMedicineInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableMedicineInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("UricAcidlowerDrugs"))  //降尿酸药
                    {
                        CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                        Medicine.Drugtype = (string)Row["Drugtype"];
                        Medicine.DrugNames = (string)Row["DrugNames"];
                        Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                        Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                        Medicine.DrugAmount = (string)Row["DrugAmount"];
                        Medicine.DrugUnits = (string)Row["DrugUnits"];
                        Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                        Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                        GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs.Add(Medicine);
                    }
                }
            }
            return true;
        }
        private static bool GetHyperuricemiaInfo(string RecordSEQ,classSQLServerDBInterface anotherSQLServerInterface)
        {
            //恢复默认值
            GlobalData.HyperuricemiaInfo.Clear();

            string sqlHyperuricemiaInfo = "select * from CDSS_HyperuricemiaInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='HyperuricemiaInfo' and RecordSEQ=" + RecordSEQ;
            string sqlMedicineInfo = "select * from CDSS_MedicineInfo where TableName='HyperuricemiaInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableHyperuricemiaInfo = anotherSQLServerInterface.GetDataSet(sqlHyperuricemiaInfo);
            if (TableHyperuricemiaInfo.Rows.Count != 1)    // 判读HyperuricemiaInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询高尿酸血症信息失败！";
                return false;
            }

            foreach (DataRow Row in TableHyperuricemiaInfo.Rows)
            {
                if (Row["HasHyperuricemia"].ToString().Trim().Equals("true"))
                    GlobalData.HyperuricemiaInfo.HasHyperuricemia = true;
                else
                    GlobalData.HyperuricemiaInfo.HasHyperuricemia = false;

                GlobalData.HyperuricemiaInfo.HyperuricemiaType = (string)Row["HyperuricemiaType"];

                if (Row["HasGoutyArthritis"].ToString().Trim().Equals("true"))
                    GlobalData.HyperuricemiaInfo.HasGoutyArthritis = true;
                else
                    GlobalData.HyperuricemiaInfo.HasGoutyArthritis = false;

                GlobalData.HyperuricemiaInfo.GoutyArthritisDetectedDateTime = (DateTime)Row["GoutyArthritisDetectedDateTime"];

                if (Row["HasTophus"].ToString().Trim().Equals("true"))
                    GlobalData.HyperuricemiaInfo.HasTophus = true;
                else
                    GlobalData.HyperuricemiaInfo.HasTophus = false;

                GlobalData.HyperuricemiaInfo.TophusPart = (string)Row["TophusPart"];

                if (Row["HasJointSwelling"].ToString().Trim().Equals("true"))
                    GlobalData.HyperuricemiaInfo.HasJointSwelling = true;
                else
                    GlobalData.HyperuricemiaInfo.HasJointSwelling = false;
            }

            DataTable TableSymptomsInfo = anotherSQLServerInterface.GetDataSet(sqlSymptomsInfo);
            DataTable TableMedicineInfo = anotherSQLServerInterface.GetDataSet(sqlMedicineInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("HyperuricemiaSymptoms"))  //高尿酸血症类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms.Add(Symptoms);
                    }
                }
            }
            if (TableMedicineInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableMedicineInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("UricAcidlowerDrugs"))  //降尿酸药
                    {
                        CDSSMedicineInfo Medicine = new CDSSMedicineInfo();
                        Medicine.Drugtype = (string)Row["Drugtype"];
                        Medicine.DrugNames = (string)Row["DrugNames"];
                        Medicine.DrugBeginTime = (DateTime)Row["DrugBeginTime"];
                        Medicine.DrugEndTime = (DateTime)Row["DrugEndTime"];
                        Medicine.DrugAmount = (string)Row["DrugAmount"];
                        Medicine.DrugUnits = (string)Row["DrugUnits"];
                        Medicine.DrugByRoute = (string)Row["DrugByRoute"];
                        Medicine.DrugFrequency = (string)Row["DrugFrequency"];
                        GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs.Add(Medicine);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 保存高尿酸血症信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveHyperuricemiaInfo()
        {
            string sqlHyperuricemiaInfo = "insert into CDSS_HyperuricemiaInfo(RecordSEQ,HasHyperuricemia,HyperuricemiaType,"
                                   + "HasGoutyArthritis,GoutyArthritisDetectedDateTime,HasTophus,TophusPart,HasJointSwelling) "
                                    + " values(@RecordSEQ,@HasHyperuricemia,@HyperuricemiaType,@HasGoutyArthritis,"
                                   + "@GoutyArthritisDetectedDateTime,@HasTophus,@TophusPart,@HasJointSwelling) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@HasHyperuricemia",GlobalData.HyperuricemiaInfo.HasHyperuricemia.ToString().ToLower()),
                new SQLiteParameter("@HyperuricemiaType",GlobalData.HyperuricemiaInfo.HyperuricemiaType),
                new SQLiteParameter("@HasGoutyArthritis",GlobalData.HyperuricemiaInfo.HasGoutyArthritis.ToString().ToLower()),
                new SQLiteParameter("@GoutyArthritisDetectedDateTime",GlobalData.HyperuricemiaInfo.GoutyArthritisDetectedDateTime.ToString("s")),
                new SQLiteParameter("@HasTophus",GlobalData.HyperuricemiaInfo.HasTophus.ToString().ToLower()),
                new SQLiteParameter("@TophusPart",GlobalData.HyperuricemiaInfo.TophusPart),
                new SQLiteParameter("@HasJointSwelling",GlobalData.HyperuricemiaInfo.HasJointSwelling.ToString().ToLower())
			};

            string sqlHyperuricemiaSymptoms = "";
            string sqlUricAcidlowerDrugs = "";

            //高尿酸血症类型
            if (GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms.Count > 0)
            {
                for (int i = 0; i < GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms.Count; i++)
                {
                    sqlHyperuricemiaSymptoms += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'HyperuricemiaInfo','HyperuricemiaSymptoms','"
                    + GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms[i].SymptomsName + "','"
                    + GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }
            //降尿酸药
            if (GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs.Count > 0)
            {
                for (int i = 0; i < GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs.Count; i++)
                {
                    sqlUricAcidlowerDrugs += "insert into CDSS_MedicineInfo(RecordSEQ,TableName,Types,Drugtype,DrugNames,DrugBeginTime,"
                       + "DrugEndTime,DrugAmount,DrugUnits,DrugByRoute,DrugFrequency)"
                       + "values(" + GlobalData.RecordInfo.RecordSeq
                       + " ,'HyperuricemiaInfo','UricAcidlowerDrugs','"
                       + GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs[i].Drugtype + "','"
                       + GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs[i].DrugNames + "','"
                       + GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs[i].DrugBeginTime.ToString("s") + "','"
                       + GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs[i].DrugEndTime.ToString("s") + "','"
                       + GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs[i].DrugAmount + "','"
                       + GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs[i].DrugUnits + "','"
                       + GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs[i].DrugByRoute + "','"
                       + GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs[i].DrugFrequency + "');";
                }
            }

            SQLiteCommand sqlCmd = SQLServerDBInterface.Connection.CreateCommand();
            sqlCmd.Connection = SQLServerDBInterface.Connection;
            sqlCmd.Transaction = SQLServerDBInterface.Connection.BeginTransaction();
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                sqlCmd.CommandText = sqlHyperuricemiaInfo;
                sqlCmd.Parameters.AddRange(para);
                sqlCmd.ExecuteNonQuery();

                if (sqlHyperuricemiaSymptoms != "")
                {
                    sqlCmd.CommandText = sqlHyperuricemiaSymptoms;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlUricAcidlowerDrugs != "")
                {
                    sqlCmd.CommandText = sqlUricAcidlowerDrugs;
                    sqlCmd.ExecuteNonQuery();
                }

                sqlCmd.Transaction.Commit();       //接受交易，完成操作
                return true;

            }
            catch (SQLiteException e)
            {
                sqlCmd.Transaction.Rollback();
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        #region 非糖尿病肾脏疾病
        /// <summary>
        /// 查询非糖尿病肾脏疾病
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetNephropathyInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.NephropathyInfo.Clear();

            string sqlNephropathyInfo = "select * from CDSS_NephropathyInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='NephropathyInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableNephropathyInfo = SQLServerDBInterface.GetDataSet(sqlNephropathyInfo);
            if (TableNephropathyInfo.Rows.Count != 1)    // 判读NephropathyInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询非糖尿病肾脏疾病信息失败！";
                return false;
            }

            foreach (DataRow Row in TableNephropathyInfo.Rows)
            {
                if (Row["HasNephropathy"].ToString().Trim().Equals("true"))
                    GlobalData.NephropathyInfo.HasNephropathy = true;
                else
                    GlobalData.NephropathyInfo.HasNephropathy = false;

                if (Row["HasRenalAbnormal"].ToString().Trim().Equals("true"))
                    GlobalData.NephropathyInfo.HasRenalAbnormal = true;
                else
                    GlobalData.NephropathyInfo.HasRenalAbnormal = false;

                GlobalData.NephropathyInfo.RenalAbnormalDetectedDateTime = (DateTime)Row["RenalAbnormalDetectedDateTime"];
                GlobalData.NephropathyInfo.MAXBloodUrea = (string)Row["MAXBloodUrea"];
                GlobalData.NephropathyInfo.MAXCreatinine = (string)Row["MAXCreatinine"];
            }

            DataTable TableSymptomsInfo = SQLServerDBInterface.GetDataSet(sqlSymptomsInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("NephropathySymptoms"))  //非糖尿病肾脏疾病类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.NephropathyInfo.listNephropathySymptoms.Add(Symptoms);
                    }
                }
            }
            return true;
        }
        private static bool GetNephropathyInfo(string RecordSEQ,classSQLServerDBInterface anotherSQLServerInterface)
        {
            //恢复默认值
            GlobalData.NephropathyInfo.Clear();

            string sqlNephropathyInfo = "select * from CDSS_NephropathyInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='NephropathyInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableNephropathyInfo = anotherSQLServerInterface.GetDataSet(sqlNephropathyInfo);
            if (TableNephropathyInfo.Rows.Count != 1)    // 判读NephropathyInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询非糖尿病肾脏疾病信息失败！";
                return false;
            }

            foreach (DataRow Row in TableNephropathyInfo.Rows)
            {
                if (Row["HasNephropathy"].ToString().Trim().Equals("true"))
                    GlobalData.NephropathyInfo.HasNephropathy = true;
                else
                    GlobalData.NephropathyInfo.HasNephropathy = false;

                if (Row["HasRenalAbnormal"].ToString().Trim().Equals("true"))
                    GlobalData.NephropathyInfo.HasRenalAbnormal = true;
                else
                    GlobalData.NephropathyInfo.HasRenalAbnormal = false;

                GlobalData.NephropathyInfo.RenalAbnormalDetectedDateTime = (DateTime)Row["RenalAbnormalDetectedDateTime"];
                GlobalData.NephropathyInfo.MAXBloodUrea = (string)Row["MAXBloodUrea"];
                GlobalData.NephropathyInfo.MAXCreatinine = (string)Row["MAXCreatinine"];
            }

            DataTable TableSymptomsInfo = anotherSQLServerInterface.GetDataSet(sqlSymptomsInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("NephropathySymptoms"))  //非糖尿病肾脏疾病类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.NephropathyInfo.listNephropathySymptoms.Add(Symptoms);
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 保存非糖尿病肾脏疾病
        /// </summary>
        /// <returns></returns>
        private static bool SaveNephropathyInfo()
        {
            string sqlNephropathyInfo = "insert into CDSS_NephropathyInfo(RecordSEQ,HasNephropathy,HasRenalAbnormal,"
                                     + "RenalAbnormalDetectedDateTime,MAXCreatinine,MAXBloodUrea) "
                                      + " values(@RecordSEQ,@HasNephropathy,@HasRenalAbnormal,"
                                     + "@RenalAbnormalDetectedDateTime,@MAXCreatinine,@MAXBloodUrea) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@HasNephropathy",GlobalData.NephropathyInfo.HasNephropathy.ToString().ToLower()),
                new SQLiteParameter("@HasRenalAbnormal",GlobalData.NephropathyInfo.HasRenalAbnormal.ToString().ToLower()),
                new SQLiteParameter("@RenalAbnormalDetectedDateTime",GlobalData.NephropathyInfo.RenalAbnormalDetectedDateTime),
                new SQLiteParameter("@MAXCreatinine",GlobalData.NephropathyInfo.MAXCreatinine),
                new SQLiteParameter("@MAXBloodUrea",GlobalData.NephropathyInfo.MAXBloodUrea)
			};

            string sqlNephropathySymptoms = "";

            //非糖尿病肾脏疾病类型
            if (GlobalData.NephropathyInfo.listNephropathySymptoms.Count > 0)
            {
                /*revised by lch 090402 修复 BugDB00005652  
                遍历的list写错了，写成了高血压类型的list，导致数据没有加载上来。*/
                for (int i = 0; i < GlobalData.NephropathyInfo.listNephropathySymptoms.Count; i++)
                {
                    sqlNephropathySymptoms += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'NephropathyInfo','NephropathySymptoms','"
                    + GlobalData.NephropathyInfo.listNephropathySymptoms[i].SymptomsName + "','"
                    + GlobalData.NephropathyInfo.listNephropathySymptoms[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }

            SQLiteCommand sqlCmd = SQLServerDBInterface.Connection.CreateCommand();
            sqlCmd.Connection = SQLServerDBInterface.Connection;
            sqlCmd.Transaction = SQLServerDBInterface.Connection.BeginTransaction();
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                sqlCmd.CommandText = sqlNephropathyInfo;
                sqlCmd.Parameters.AddRange(para);
                sqlCmd.ExecuteNonQuery();

                if (sqlNephropathySymptoms != "")
                {
                    sqlCmd.CommandText = sqlNephropathySymptoms;
                    sqlCmd.ExecuteNonQuery();
                }

                sqlCmd.Transaction.Commit();       //接受交易，完成操作
                return true;

            }
            catch (SQLiteException e)
            {
                sqlCmd.Transaction.Rollback();
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        #region 其他疾病史
        /// <summary>
        /// 查询其他疾病史
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetOtherDiseaseHistoryInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.OtherDiseaseHistoryInfo.Clear();

            string sqlOtherDiseaseHistoryInfo = "select * from CDSS_OtherDiseaseHistoryInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='OtherDiseaseHistoryInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableOtherDiseaseHistoryInfo = SQLServerDBInterface.GetDataSet(sqlOtherDiseaseHistoryInfo);
            if (TableOtherDiseaseHistoryInfo.Rows.Count != 1)    // 判读NephropathyInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询其他疾病史信息失败！";
                return false;
            }

            foreach (DataRow Row in TableOtherDiseaseHistoryInfo.Rows)
            {
                if (Row["HasCholecystitis"].ToString().Trim().Equals("true"))
                    GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis = true;
                else
                    GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis = false;

                GlobalData.OtherDiseaseHistoryInfo.CholecystitisDetectedDateTime = (DateTime)Row["CholecystitisDetectedDateTime"];


                if (Row["HasGallbladderSurgery"].ToString().Trim().Equals("true"))
                    GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery = true;
                else
                    GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery = false;

                GlobalData.OtherDiseaseHistoryInfo.GallbladderSurgeryDateTime = (DateTime)Row["GallbladderSurgeryDateTime"];

                if (Row["HasCancer"].ToString().Trim().Equals("true"))
                    GlobalData.OtherDiseaseHistoryInfo.HasCancer = true;
                else
                    GlobalData.OtherDiseaseHistoryInfo.HasCancer = false;

                GlobalData.OtherDiseaseHistoryInfo.CancerPart = (string)Row["CancerPart"];
                GlobalData.OtherDiseaseHistoryInfo.CancerDetectedDateTime = (DateTime)Row["CancerDetectedDateTime"];
                GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis = (string)Row["CancerPrognosis"];
                GlobalData.OtherDiseaseHistoryInfo.OtherDisease = (string)Row["OtherDisease"];
                GlobalData.OtherDiseaseHistoryInfo.OtherDiseaseDetectedDateTime = (DateTime)Row["OtherDiseaseDetectedDateTime"];
            }

            DataTable TableSymptomsInfo = SQLServerDBInterface.GetDataSet(sqlSymptomsInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("CoronaryHeartDisease"))  //冠心病类型类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Add(Symptoms);
                    }
                    else if (Row["Types"].ToString().Trim().Equals("CerebrovascularDisease"))  //脑血管疾病类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease.Add(Symptoms);
                    }
                    else if (Row["Types"].ToString().Trim().Equals("Pancreatitis"))  //胰腺炎类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Add(Symptoms);
                    }
                }
            }
            return true;
        }
        private static bool GetOtherDiseaseHistoryInfo(string RecordSEQ, classSQLServerDBInterface anotherSQLServerInterface)
        {
            //恢复默认值
            GlobalData.OtherDiseaseHistoryInfo.Clear();

            string sqlOtherDiseaseHistoryInfo = "select * from CDSS_OtherDiseaseHistoryInfo where RecordSEQ=" + RecordSEQ;
            string sqlSymptomsInfo = "select * from CDSS_SymptomsInfo where TableName='OtherDiseaseHistoryInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableOtherDiseaseHistoryInfo = anotherSQLServerInterface.GetDataSet(sqlOtherDiseaseHistoryInfo);
            if (TableOtherDiseaseHistoryInfo.Rows.Count != 1)    // 判读NephropathyInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询其他疾病史信息失败！";
                return false;
            }

            foreach (DataRow Row in TableOtherDiseaseHistoryInfo.Rows)
            {
                if (Row["HasCholecystitis"].ToString().Trim().Equals("true"))
                    GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis = true;
                else
                    GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis = false;

                GlobalData.OtherDiseaseHistoryInfo.CholecystitisDetectedDateTime = (DateTime)Row["CholecystitisDetectedDateTime"];


                if (Row["HasGallbladderSurgery"].ToString().Trim().Equals("true"))
                    GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery = true;
                else
                    GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery = false;

                GlobalData.OtherDiseaseHistoryInfo.GallbladderSurgeryDateTime = (DateTime)Row["GallbladderSurgeryDateTime"];

                if (Row["HasCancer"].ToString().Trim().Equals("true"))
                    GlobalData.OtherDiseaseHistoryInfo.HasCancer = true;
                else
                    GlobalData.OtherDiseaseHistoryInfo.HasCancer = false;

                GlobalData.OtherDiseaseHistoryInfo.CancerPart = (string)Row["CancerPart"];
                GlobalData.OtherDiseaseHistoryInfo.CancerDetectedDateTime = (DateTime)Row["CancerDetectedDateTime"];
                GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis = (string)Row["CancerPrognosis"];
                GlobalData.OtherDiseaseHistoryInfo.OtherDisease = (string)Row["OtherDisease"];
                GlobalData.OtherDiseaseHistoryInfo.OtherDiseaseDetectedDateTime = (DateTime)Row["OtherDiseaseDetectedDateTime"];
            }

            DataTable TableSymptomsInfo = anotherSQLServerInterface.GetDataSet(sqlSymptomsInfo);
            if (TableSymptomsInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableSymptomsInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("CoronaryHeartDisease"))  //冠心病类型类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Add(Symptoms);
                    }
                    else if (Row["Types"].ToString().Trim().Equals("CerebrovascularDisease"))  //脑血管疾病类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease.Add(Symptoms);
                    }
                    else if (Row["Types"].ToString().Trim().Equals("Pancreatitis"))  //胰腺炎类型
                    {
                        CDSSSymptomsInfo Symptoms = new CDSSSymptomsInfo();
                        Symptoms.SymptomsDetectedDateTime = (DateTime)Row["SymptomsDetectedDateTime"];
                        Symptoms.SymptomsName = (string)Row["SymptomsName"];

                        GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Add(Symptoms);
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 保存其他疾病史
        /// </summary>
        /// <returns></returns>
        private static bool SaveOtherDiseaseHistoryInfo()
        {
            string sqlOtherDiseaseHistoryInfo = "insert into CDSS_OtherDiseaseHistoryInfo(RecordSEQ,HasCholecystitis,"
                                        + "CholecystitisDetectedDateTime,HasGallbladderSurgery,GallbladderSurgeryDateTime,"
                                        + " HasCancer,CancerPart,CancerDetectedDateTime,CancerPrognosis,OtherDisease,"
                                        + " OtherDiseaseDetectedDateTime) "
                                         + " values(@RecordSEQ,@HasCholecystitis,"
                                        + " @CholecystitisDetectedDateTime,@HasGallbladderSurgery,@GallbladderSurgeryDateTime,"
                                        + " @HasCancer,@CancerPart,@CancerDetectedDateTime,@CancerPrognosis,@OtherDisease,"
                                        + " @OtherDiseaseDetectedDateTime) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@HasCholecystitis",GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis.ToString().ToLower()),
                new SQLiteParameter("@CholecystitisDetectedDateTime",GlobalData.OtherDiseaseHistoryInfo.CholecystitisDetectedDateTime),
                new SQLiteParameter("@HasGallbladderSurgery",GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery.ToString().ToLower()),
                new SQLiteParameter("@GallbladderSurgeryDateTime",GlobalData.OtherDiseaseHistoryInfo.GallbladderSurgeryDateTime),
                new SQLiteParameter("@HasCancer",GlobalData.OtherDiseaseHistoryInfo.HasCancer.ToString().ToLower()),
                new SQLiteParameter("@CancerPart",GlobalData.OtherDiseaseHistoryInfo.CancerPart),
                new SQLiteParameter("@CancerDetectedDateTime",GlobalData.OtherDiseaseHistoryInfo.CancerDetectedDateTime),
                new SQLiteParameter("@CancerPrognosis",GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis),
                new SQLiteParameter("@OtherDisease",GlobalData.OtherDiseaseHistoryInfo.OtherDisease),
                new SQLiteParameter("@OtherDiseaseDetectedDateTime",GlobalData.OtherDiseaseHistoryInfo.OtherDiseaseDetectedDateTime)           
			};

            string sqlCoronaryHeartDisease = "";
            string sqlCerebrovascularDisease = "";
            string sqlPancreatitis = "";

            //冠心病类型类型
            if (GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Count > 0)
            {
                for (int i = 0; i < GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Count; i++)
                {
                    sqlCoronaryHeartDisease += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'OtherDiseaseHistoryInfo','CoronaryHeartDisease','"
                    + GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease[i].SymptomsName + "','"
                    + GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }
            //脑血管疾病类型
            if (GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease.Count > 0)
            {
                for (int i = 0; i < GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease.Count; i++)
                {
                    sqlCerebrovascularDisease += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'OtherDiseaseHistoryInfo','CerebrovascularDisease','"
                    + GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease[i].SymptomsName + "','"
                    + GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }

            //胰腺炎类型
            if (GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Count > 0)
            {
                for (int i = 0; i < GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Count; i++)
                {
                    sqlPancreatitis += "insert into CDSS_SymptomsInfo(RecordSEQ,TableName,Types,SymptomsName,SymptomsDetectedDateTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'OtherDiseaseHistoryInfo','Pancreatitis','"
                    + GlobalData.OtherDiseaseHistoryInfo.listPancreatitis[i].SymptomsName + "','"
                    + GlobalData.OtherDiseaseHistoryInfo.listPancreatitis[i].SymptomsDetectedDateTime.ToString("s") + "') ;";
                }
            }

            SQLiteCommand sqlCmd = SQLServerDBInterface.Connection.CreateCommand();
            sqlCmd.Connection = SQLServerDBInterface.Connection;
            sqlCmd.Transaction = SQLServerDBInterface.Connection.BeginTransaction();
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                sqlCmd.CommandText = sqlOtherDiseaseHistoryInfo;
                sqlCmd.Parameters.AddRange(para);
                sqlCmd.ExecuteNonQuery();

                if (sqlCoronaryHeartDisease != "")
                {
                    sqlCmd.CommandText = sqlCoronaryHeartDisease;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlCerebrovascularDisease != "")
                {
                    sqlCmd.CommandText = sqlCerebrovascularDisease;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlPancreatitis != "")
                {
                    sqlCmd.CommandText = sqlPancreatitis;
                    sqlCmd.ExecuteNonQuery();
                }

                sqlCmd.Transaction.Commit();       //接受交易，完成操作
                return true;

            }
            catch (SQLiteException e)
            {
                sqlCmd.Transaction.Rollback();
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        #region 个人史
        /// <summary>
        /// 查询个人史信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetPersonalHistoryInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.PersonalHistoryInfo.Clear();

            string sqlPersonalHistoryInfo = "select * from CDSS_PersonalHistoryInfo where RecordSEQ=" + RecordSEQ;
            string sqlExerciseInfo = "select * from CDSS_ExerciseInfo where TableName='PersonalHistoryInfo' and RecordSEQ=" + RecordSEQ;
            string sqlDrinkingInfo = "select * from CDSS_DrinkingInfo where TableName='PersonalHistoryInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TablePersonalHistoryInfo = SQLServerDBInterface.GetDataSet(sqlPersonalHistoryInfo);
            if (TablePersonalHistoryInfo.Rows.Count != 1)    // 判读PersonalHistoryInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询个人史信息失败！";
                return false;
            }

            foreach (DataRow Row in TablePersonalHistoryInfo.Rows)
            {
                GlobalData.PersonalHistoryInfo.MaxWeight = (string)Row["MaxWeight"];
                GlobalData.PersonalHistoryInfo.MinWeight = (string)Row["MinWeight"];
                GlobalData.PersonalHistoryInfo.MaxWeightAge = (string)Row["MaxWeightAge"];
                GlobalData.PersonalHistoryInfo.MaxWeightLastedYears = (string)Row["MaxWeightLastedYears"];

                if (Row["IsSmokeing"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.IsSmokeing = true;
                else
                    GlobalData.PersonalHistoryInfo.IsSmokeing = false;

                GlobalData.PersonalHistoryInfo.SmokingAgeBegin = (string)Row["SmokingAgeBegin"];
                GlobalData.PersonalHistoryInfo.SmokingFrequency = (string)Row["SmokingFrequency"];
                GlobalData.PersonalHistoryInfo.RecentSmokingFrequency = (string)Row["RecentSmokingFrequency"];
                GlobalData.PersonalHistoryInfo.SmokingAgeEnd = (string)Row["SmokingAgeEnd"];

                if (Row["IsDrinking"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.IsDrinking = true;
                else
                    GlobalData.PersonalHistoryInfo.IsDrinking = false;

                GlobalData.PersonalHistoryInfo.DrinkingAgeBegin = (string)Row["DrinkingAgeBegin"];
                GlobalData.PersonalHistoryInfo.DrinkingAgeEnd = (string)Row["DrinkingAgeEnd"];

                if (Row["HasControlDiet"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.HasControlDiet = true;
                else
                    GlobalData.PersonalHistoryInfo.HasControlDiet = false;

                GlobalData.PersonalHistoryInfo.MainFoodAmount = (string)Row["MainFoodAmount"];
                GlobalData.PersonalHistoryInfo.OilAmount = (string)Row["OilAmount"];
                GlobalData.PersonalHistoryInfo.ProteinAmount = (string)Row["ProteinAmount"];

                if (Row["HasBearing"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.HasBearing = true;
                else
                    GlobalData.PersonalHistoryInfo.HasBearing = false;

                if (Row["HasGDM"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.HasGDM = true;
                else
                    GlobalData.PersonalHistoryInfo.HasGDM = false;

                GlobalData.PersonalHistoryInfo.GDMAgeBegin = (string)Row["GDMAgeBegin"];

                if (Row["IsNeonateHeavierThan4Kg"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg = true;
                else
                    GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg = false;


                GlobalData.PersonalHistoryInfo.NeonateCount = (string)Row["NeonateCount"];
                GlobalData.PersonalHistoryInfo.BearingAge1 = (string)Row["BearingAge1"];
                GlobalData.PersonalHistoryInfo.NeonateWeight1 = (string)Row["NeonateWeight1"];
                GlobalData.PersonalHistoryInfo.BearingAge2 = (string)Row["BearingAge2"];
                GlobalData.PersonalHistoryInfo.NeonateWeight2 = (string)Row["NeonateWeight2"];


                if (Row["HasExerciseRecent"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.HasExerciseRecent = true;
                else
                    GlobalData.PersonalHistoryInfo.HasExerciseRecent = false;
            }

            DataTable TableExerciseInfo = SQLServerDBInterface.GetDataSet(sqlExerciseInfo);
            DataTable TableDrinkingInfo = SQLServerDBInterface.GetDataSet(sqlDrinkingInfo);
            if (TableExerciseInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableExerciseInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("ExerciseInfo"))
                    {
                        CDSSExerciseInfo ExerciseInfo = new CDSSExerciseInfo();
                        ExerciseInfo.ExerciseType = (string)Row["ExerciseType"];
                        ExerciseInfo.DaysOneWeek = (string)Row["DaysOneWeek"];
                        ExerciseInfo.LastedHourOneDay = (string)Row["LastedHourOneDay"];

                        GlobalData.PersonalHistoryInfo.listExerciseInfo.Add(ExerciseInfo);
                    }
                }
            }
            if (TableDrinkingInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableDrinkingInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("DrinkingInfo"))
                    {
                        CDSSDrinkingInfo DrinkingInfo = new CDSSDrinkingInfo();
                        DrinkingInfo.DrinkingType = (string)Row["DrinkingType"];
                        DrinkingInfo.TimesOneWeek = (string)Row["TimesOneWeek"];
                        DrinkingInfo.AmountOneTime = (string)Row["AmountOneTime"];

                        GlobalData.PersonalHistoryInfo.listDrinkingInfo.Add(DrinkingInfo);
                    }
                }
            }
            return true;
        }
        private static bool GetPersonalHistoryInfo(string RecordSEQ,classSQLServerDBInterface anotherSQLServerInterface)
        {
            //恢复默认值
            GlobalData.PersonalHistoryInfo.Clear();

            string sqlPersonalHistoryInfo = "select * from CDSS_PersonalHistoryInfo where RecordSEQ=" + RecordSEQ;
            string sqlExerciseInfo = "select * from CDSS_ExerciseInfo where TableName='PersonalHistoryInfo' and RecordSEQ=" + RecordSEQ;
            string sqlDrinkingInfo = "select * from CDSS_DrinkingInfo where TableName='PersonalHistoryInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TablePersonalHistoryInfo = anotherSQLServerInterface.GetDataSet(sqlPersonalHistoryInfo);
            if (TablePersonalHistoryInfo.Rows.Count != 1)    // 判读PersonalHistoryInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询个人史信息失败！";
                return false;
            }

            foreach (DataRow Row in TablePersonalHistoryInfo.Rows)
            {
                GlobalData.PersonalHistoryInfo.MaxWeight = (string)Row["MaxWeight"];
                GlobalData.PersonalHistoryInfo.MinWeight = (string)Row["MinWeight"];
                GlobalData.PersonalHistoryInfo.MaxWeightAge = (string)Row["MaxWeightAge"];
                GlobalData.PersonalHistoryInfo.MaxWeightLastedYears = (string)Row["MaxWeightLastedYears"];

                if (Row["IsSmokeing"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.IsSmokeing = true;
                else
                    GlobalData.PersonalHistoryInfo.IsSmokeing = false;

                GlobalData.PersonalHistoryInfo.SmokingAgeBegin = (string)Row["SmokingAgeBegin"];
                GlobalData.PersonalHistoryInfo.SmokingFrequency = (string)Row["SmokingFrequency"];
                GlobalData.PersonalHistoryInfo.RecentSmokingFrequency = (string)Row["RecentSmokingFrequency"];
                GlobalData.PersonalHistoryInfo.SmokingAgeEnd = (string)Row["SmokingAgeEnd"];

                if (Row["IsDrinking"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.IsDrinking = true;
                else
                    GlobalData.PersonalHistoryInfo.IsDrinking = false;

                GlobalData.PersonalHistoryInfo.DrinkingAgeBegin = (string)Row["DrinkingAgeBegin"];
                GlobalData.PersonalHistoryInfo.DrinkingAgeEnd = (string)Row["DrinkingAgeEnd"];

                if (Row["HasControlDiet"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.HasControlDiet = true;
                else
                    GlobalData.PersonalHistoryInfo.HasControlDiet = false;

                GlobalData.PersonalHistoryInfo.MainFoodAmount = (string)Row["MainFoodAmount"];
                GlobalData.PersonalHistoryInfo.OilAmount = (string)Row["OilAmount"];
                GlobalData.PersonalHistoryInfo.ProteinAmount = (string)Row["ProteinAmount"];

                if (Row["HasBearing"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.HasBearing = true;
                else
                    GlobalData.PersonalHistoryInfo.HasBearing = false;

                if (Row["HasGDM"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.HasGDM = true;
                else
                    GlobalData.PersonalHistoryInfo.HasGDM = false;

                GlobalData.PersonalHistoryInfo.GDMAgeBegin = (string)Row["GDMAgeBegin"];

                if (Row["IsNeonateHeavierThan4Kg"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg = true;
                else
                    GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg = false;


                GlobalData.PersonalHistoryInfo.NeonateCount = (string)Row["NeonateCount"];
                GlobalData.PersonalHistoryInfo.BearingAge1 = (string)Row["BearingAge1"];
                GlobalData.PersonalHistoryInfo.NeonateWeight1 = (string)Row["NeonateWeight1"];
                GlobalData.PersonalHistoryInfo.BearingAge2 = (string)Row["BearingAge2"];
                GlobalData.PersonalHistoryInfo.NeonateWeight2 = (string)Row["NeonateWeight2"];


                if (Row["HasExerciseRecent"].ToString().Trim().Equals("true"))
                    GlobalData.PersonalHistoryInfo.HasExerciseRecent = true;
                else
                    GlobalData.PersonalHistoryInfo.HasExerciseRecent = false;
            }

            DataTable TableExerciseInfo = anotherSQLServerInterface.GetDataSet(sqlExerciseInfo);
            DataTable TableDrinkingInfo = anotherSQLServerInterface.GetDataSet(sqlDrinkingInfo);
            if (TableExerciseInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableExerciseInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("ExerciseInfo"))
                    {
                        CDSSExerciseInfo ExerciseInfo = new CDSSExerciseInfo();
                        ExerciseInfo.ExerciseType = (string)Row["ExerciseType"];
                        ExerciseInfo.DaysOneWeek = (string)Row["DaysOneWeek"];
                        ExerciseInfo.LastedHourOneDay = (string)Row["LastedHourOneDay"];

                        GlobalData.PersonalHistoryInfo.listExerciseInfo.Add(ExerciseInfo);
                    }
                }
            }
            if (TableDrinkingInfo.Rows.Count > 0)
            {
                foreach (DataRow Row in TableDrinkingInfo.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("DrinkingInfo"))
                    {
                        CDSSDrinkingInfo DrinkingInfo = new CDSSDrinkingInfo();
                        DrinkingInfo.DrinkingType = (string)Row["DrinkingType"];
                        DrinkingInfo.TimesOneWeek = (string)Row["TimesOneWeek"];
                        DrinkingInfo.AmountOneTime = (string)Row["AmountOneTime"];

                        GlobalData.PersonalHistoryInfo.listDrinkingInfo.Add(DrinkingInfo);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 保存个人史信息
        /// </summary>
        /// <returns></returns>
        private static bool SavePersonalHistoryInfo()
        {
            string sqlPersonalHistoryInfo = "insert into CDSS_PersonalHistoryInfo(RecordSEQ,MaxWeight,MinWeight,MaxWeightAge, "
                               + "MaxWeightLastedYears,IsSmokeing,SmokingAgeBegin,SmokingFrequency,RecentSmokingFrequency,"
                               + "SmokingAgeEnd,IsDrinking,DrinkingAgeBegin,DrinkingAgeEnd,HasControlDiet,MainFoodAmount,OilAmount,ProteinAmount,HasBearing,HasGDM,GDMAgeBegin,"
                               + "IsNeonateHeavierThan4Kg,NeonateCount,BearingAge1,NeonateWeight1,BearingAge2,NeonateWeight2,"
                               + " HasExerciseRecent) "
                               + " values(@RecordSEQ,@MaxWeight,@MinWeight,@MaxWeightAge, "
                               + "@MaxWeightLastedYears,@IsSmokeing,@SmokingAgeBegin,@SmokingFrequency,@RecentSmokingFrequency,"
                               + "@SmokingAgeEnd,@IsDrinking,@DrinkingAgeBegin,@DrinkingAgeEnd,@HasControlDiet,@MainFoodAmount,@OilAmount,@ProteinAmount,@HasBearing,@HasGDM,@GDMAgeBegin,"
                               + "@IsNeonateHeavierThan4Kg,@NeonateCount,@BearingAge1,@NeonateWeight1,@BearingAge2,@NeonateWeight2,"
                               + " @HasExerciseRecent) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@MaxWeight",GlobalData.PersonalHistoryInfo.MaxWeight),
                new SQLiteParameter("@MinWeight",GlobalData.PersonalHistoryInfo.MinWeight),
                new SQLiteParameter("@MaxWeightAge",GlobalData.PersonalHistoryInfo.MaxWeightAge),
                new SQLiteParameter("@MaxWeightLastedYears",GlobalData.PersonalHistoryInfo.MaxWeightLastedYears),
                new SQLiteParameter("@IsSmokeing",GlobalData.PersonalHistoryInfo.IsDrinking.ToString().ToLower()),
                new SQLiteParameter("@SmokingAgeBegin",GlobalData.PersonalHistoryInfo.SmokingAgeBegin),
                new SQLiteParameter("@SmokingFrequency",GlobalData.PersonalHistoryInfo.SmokingFrequency),
                new SQLiteParameter("@RecentSmokingFrequency",GlobalData.PersonalHistoryInfo.RecentSmokingFrequency),
                new SQLiteParameter("@SmokingAgeEnd",GlobalData.PersonalHistoryInfo.SmokingAgeEnd),
                new SQLiteParameter("@IsDrinking",GlobalData.PersonalHistoryInfo.IsDrinking.ToString().ToLower()),
                new SQLiteParameter("@DrinkingAgeBegin",GlobalData.PersonalHistoryInfo.DrinkingAgeBegin),
                new SQLiteParameter("@DrinkingAgeEnd",GlobalData.PersonalHistoryInfo.DrinkingAgeEnd),
                new SQLiteParameter("@HasControlDiet",GlobalData.PersonalHistoryInfo.HasControlDiet.ToString().ToLower()),
                new SQLiteParameter("@MainFoodAmount",GlobalData.PersonalHistoryInfo.MainFoodAmount),
                new SQLiteParameter("@OilAmount",GlobalData.PersonalHistoryInfo.OilAmount),
                new SQLiteParameter("@ProteinAmount",GlobalData.PersonalHistoryInfo.ProteinAmount),
                new SQLiteParameter("@HasBearing",GlobalData.PersonalHistoryInfo.HasBearing.ToString().ToLower()),
                new SQLiteParameter("@HasGDM",GlobalData.PersonalHistoryInfo.HasGDM.ToString().ToLower()),
                new SQLiteParameter("@GDMAgeBegin",GlobalData.PersonalHistoryInfo.GDMAgeBegin),
                new SQLiteParameter("@IsNeonateHeavierThan4Kg",GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg.ToString().ToLower()),
                new SQLiteParameter("@NeonateCount",GlobalData.PersonalHistoryInfo.NeonateCount),
                new SQLiteParameter("@BearingAge1",GlobalData.PersonalHistoryInfo.BearingAge1),
                new SQLiteParameter("@NeonateWeight1",GlobalData.PersonalHistoryInfo.NeonateWeight1),
                new SQLiteParameter("@BearingAge2",GlobalData.PersonalHistoryInfo.BearingAge2),
                new SQLiteParameter("@NeonateWeight2",GlobalData.PersonalHistoryInfo.NeonateWeight2),
                new SQLiteParameter("@HasExerciseRecent",GlobalData.PersonalHistoryInfo.HasExerciseRecent.ToString().ToLower())      
			};

            string sqlExerciseInfo = "";
            string sqlDrinkingInfo = "";


            //运动信息
            if (GlobalData.PersonalHistoryInfo.listExerciseInfo.Count > 0)
            {
                for (int i = 0; i < GlobalData.PersonalHistoryInfo.listExerciseInfo.Count; i++)
                {
                    sqlExerciseInfo += "insert into CDSS_ExerciseInfo(RecordSEQ,TableName,Types,ExerciseType,DaysOneWeek,LastedHourOneDay) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'PersonalHistoryInfo','ExerciseInfo','"
                    + GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType + "','"
                    + GlobalData.PersonalHistoryInfo.listExerciseInfo[i].DaysOneWeek + "','"
                    + GlobalData.PersonalHistoryInfo.listExerciseInfo[i].LastedHourOneDay + "') ;";
                }
            }
            //饮酒信息
            if (GlobalData.PersonalHistoryInfo.listDrinkingInfo.Count > 0)
            {
                for (int i = 0; i < GlobalData.PersonalHistoryInfo.listDrinkingInfo.Count; i++)
                {
                    sqlDrinkingInfo += "insert into CDSS_DrinkingInfo(RecordSEQ,TableName,Types,DrinkingType,TimesOneWeek,AmountOneTime) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'PersonalHistoryInfo','DrinkingInfo','"
                    + GlobalData.PersonalHistoryInfo.listDrinkingInfo[i].DrinkingType + "','"
                    + GlobalData.PersonalHistoryInfo.listDrinkingInfo[i].TimesOneWeek + "','"
                    + GlobalData.PersonalHistoryInfo.listDrinkingInfo[i].AmountOneTime + "') ;";
                }
            }

            SQLiteCommand sqlCmd = SQLServerDBInterface.Connection.CreateCommand();
            sqlCmd.Connection = SQLServerDBInterface.Connection;
            sqlCmd.Transaction = SQLServerDBInterface.Connection.BeginTransaction();
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                sqlCmd.CommandText = sqlPersonalHistoryInfo;
                sqlCmd.Parameters.AddRange(para);
                sqlCmd.ExecuteNonQuery();

                if (sqlExerciseInfo != "")
                {
                    sqlCmd.CommandText = sqlExerciseInfo;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlDrinkingInfo != "")
                {
                    sqlCmd.CommandText = sqlDrinkingInfo;
                    sqlCmd.ExecuteNonQuery();
                }


                sqlCmd.Transaction.Commit();       //接受交易，完成操作
                return true;

            }
            catch (SQLiteException e)
            {
                sqlCmd.Transaction.Rollback();
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        #region 家族疾病史
        /// <summary>
        /// 查询家族疾病史
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetFamilyDiseaseHistoryInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.FamilyDiseaseHistoryInfo.Clear();

            string sqlFamilyDiseaseHistoryInfo = "select * from CDSS_FamilyDiseaseHistoryInfo where RecordSEQ=" + RecordSEQ;

            DataTable TableFamilyDiseaseHistoryInfo = SQLServerDBInterface.GetDataSet(sqlFamilyDiseaseHistoryInfo);
            if (TableFamilyDiseaseHistoryInfo.Rows.Count != 1)    // 判读FamilyDiseaseHistoryInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询家族疾病史信息失败！";
                return false;
            }

            foreach (DataRow Row in TableFamilyDiseaseHistoryInfo.Rows)
            {
                GlobalData.FamilyDiseaseHistoryInfo.FatherHistory = (string)Row["FatherHistory"];
                GlobalData.FamilyDiseaseHistoryInfo.MotherHistory = (string)Row["MotherHistory"];
                GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory = (string)Row["ChildrenHistory"];
                GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory = (string)Row["SiblingsHistory"];
                GlobalData.FamilyDiseaseHistoryInfo.OtherHistory = (string)Row["OtherHistory"];
            }
            return true;
        }
        private static bool GetFamilyDiseaseHistoryInfo(string RecordSEQ, classSQLServerDBInterface anotherSQLServerInterface)
        {
            //恢复默认值
            GlobalData.FamilyDiseaseHistoryInfo.Clear();

            string sqlFamilyDiseaseHistoryInfo = "select * from CDSS_FamilyDiseaseHistoryInfo where RecordSEQ=" + RecordSEQ;

            DataTable TableFamilyDiseaseHistoryInfo = anotherSQLServerInterface.GetDataSet(sqlFamilyDiseaseHistoryInfo);
            if (TableFamilyDiseaseHistoryInfo.Rows.Count != 1)    // 判读FamilyDiseaseHistoryInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询家族疾病史信息失败！";
                return false;
            }

            foreach (DataRow Row in TableFamilyDiseaseHistoryInfo.Rows)
            {
                GlobalData.FamilyDiseaseHistoryInfo.FatherHistory = (string)Row["FatherHistory"];
                GlobalData.FamilyDiseaseHistoryInfo.MotherHistory = (string)Row["MotherHistory"];
                GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory = (string)Row["ChildrenHistory"];
                GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory = (string)Row["SiblingsHistory"];
                GlobalData.FamilyDiseaseHistoryInfo.OtherHistory = (string)Row["OtherHistory"];
            }
            return true;
        }


        /// <summary>
        /// 保存家族疾病史
        /// </summary>
        /// <returns></returns>
        private static bool SaveFamilyDiseaseHistoryInfo()
        {
            string sqlFamilyDiseaseHistoryInfo = "insert into CDSS_FamilyDiseaseHistoryInfo(RecordSEQ,FatherHistory,MotherHistory, "
                                + "SiblingsHistory,ChildrenHistory,OtherHistory) "
                                + " values(@RecordSEQ,@FatherHistory,@MotherHistory,@SiblingsHistory, "
                                + "@ChildrenHistory,@OtherHistory) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@FatherHistory",GlobalData.FamilyDiseaseHistoryInfo.FatherHistory),
                new SQLiteParameter("@MotherHistory",GlobalData.FamilyDiseaseHistoryInfo.MotherHistory),
                new SQLiteParameter("@SiblingsHistory",GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory),
                new SQLiteParameter("@ChildrenHistory",GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory),
                new SQLiteParameter("@OtherHistory",GlobalData.FamilyDiseaseHistoryInfo.OtherHistory)   
			};

            try
            {
                int result = SQLServerDBInterface.ExecuteCommand(sqlFamilyDiseaseHistoryInfo, para);
                if (result > 0)
                    return true;
                else
                {
                    LastErrorInfo = "保存家族疾病史信息失败！";
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        #region 体格检查信息
        /// <summary>
        /// 查询体格检查信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetPhysicalInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.PhysicalInfo.Clear();

            string sqlPhysicalInfo = "select * from CDSS_PhysicalInfo where RecordSEQ=" + RecordSEQ;

            DataTable TablePhysicalInfo = SQLServerDBInterface.GetDataSet(sqlPhysicalInfo);
            if (TablePhysicalInfo.Rows.Count != 1)    // 判读FamilyDiseaseHistoryInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询体格检查信息失败！";
                return false;
            }

            foreach (DataRow Row in TablePhysicalInfo.Rows)
            {
                GlobalData.PhysicalInfo.Height = (string)Row["Height"];
                GlobalData.PhysicalInfo.Weigh = (string)Row["Weigh"];
                GlobalData.PhysicalInfo.WC = (string)Row["WC"];
                GlobalData.PhysicalInfo.HC = (string)Row["HC"];
                GlobalData.PhysicalInfo.HR = (string)Row["HR"];


                if (Row["HasDyskinesia"].ToString().Trim().Equals("true"))
                    GlobalData.PhysicalInfo.HasDyskinesia = true;
                else
                    GlobalData.PhysicalInfo.HasDyskinesia = false;

                GlobalData.PhysicalInfo.DyskinesiaPart = (string)Row["DyskinesiaPart"];
                GlobalData.PhysicalInfo.SBP1 = (string)Row["SBP1"];
                GlobalData.PhysicalInfo.DBP1 = (string)Row["DBP1"];
                GlobalData.PhysicalInfo.SBP2 = (string)Row["SBP2"];
                GlobalData.PhysicalInfo.DBP2 = (string)Row["DBP2"];
            }
            return true;
        }
        private static bool GetPhysicalInfo(string RecordSEQ, classSQLServerDBInterface anotherSQLServerInterface)
        {
            //恢复默认值
            GlobalData.PhysicalInfo.Clear();

            string sqlPhysicalInfo = "select * from CDSS_PhysicalInfo where RecordSEQ=" + RecordSEQ;

            DataTable TablePhysicalInfo = anotherSQLServerInterface.GetDataSet(sqlPhysicalInfo);
            if (TablePhysicalInfo.Rows.Count != 1)    // 判读FamilyDiseaseHistoryInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询体格检查信息失败！";
                return false;
            }

            foreach (DataRow Row in TablePhysicalInfo.Rows)
            {
                GlobalData.PhysicalInfo.Height = (string)Row["Height"];
                GlobalData.PhysicalInfo.Weigh = (string)Row["Weigh"];
                GlobalData.PhysicalInfo.WC = (string)Row["WC"];
                GlobalData.PhysicalInfo.HC = (string)Row["HC"];
                GlobalData.PhysicalInfo.HR = (string)Row["HR"];


                if (Row["HasDyskinesia"].ToString().Trim().Equals("true"))
                    GlobalData.PhysicalInfo.HasDyskinesia = true;
                else
                    GlobalData.PhysicalInfo.HasDyskinesia = false;

                GlobalData.PhysicalInfo.DyskinesiaPart = (string)Row["DyskinesiaPart"];
                GlobalData.PhysicalInfo.SBP1 = (string)Row["SBP1"];
                GlobalData.PhysicalInfo.DBP1 = (string)Row["DBP1"];
                GlobalData.PhysicalInfo.SBP2 = (string)Row["SBP2"];
                GlobalData.PhysicalInfo.DBP2 = (string)Row["DBP2"];
            }
            return true;
        }
        /// <summary>
        /// 保存体格检查信息
        /// </summary>
        /// <returns></returns>
        private static bool SavePhysicalInfo()
        {
            string sqlPhysicalInfo = "insert into CDSS_PhysicalInfo(RecordSEQ,Height,Weigh,WC,HC,HR,HasDyskinesia, "
                               + "DyskinesiaPart,SBP1,DBP1,SBP2,DBP2) "
                               + " values(@RecordSEQ,@Height,@Weigh,@WC,@HC,@HR,@HasDyskinesia, "
                               + "@DyskinesiaPart,@SBP1,@DBP1,@SBP2,@DBP2) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@Height",GlobalData.PhysicalInfo.Height),
                new SQLiteParameter("@Weigh",GlobalData.PhysicalInfo.Weigh),
                new SQLiteParameter("@WC",GlobalData.PhysicalInfo.WC),
                new SQLiteParameter("@HC",GlobalData.PhysicalInfo.HC),
                new SQLiteParameter("@HR",GlobalData.PhysicalInfo.HR),
                new SQLiteParameter("@HasDyskinesia",GlobalData.PhysicalInfo.HasDyskinesia.ToString().ToLower()),
                new SQLiteParameter("@DyskinesiaPart",GlobalData.PhysicalInfo.DyskinesiaPart),
                new SQLiteParameter("@SBP1",GlobalData.PhysicalInfo.SBP1 ),
                new SQLiteParameter("@DBP1",GlobalData.PhysicalInfo.DBP1),
                new SQLiteParameter("@SBP2",GlobalData.PhysicalInfo.SBP2),
                new SQLiteParameter("@DBP2",GlobalData.PhysicalInfo.DBP2)
			};

            try
            {
                int result = SQLServerDBInterface.ExecuteCommand(sqlPhysicalInfo, para);
                if (result > 0)
                    return true;
                else
                {
                    LastErrorInfo = "保存体格检查信息失败！";
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        #region 实验室检查
        /// <summary>
        /// 查询实验室检查信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetLabExamInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.LabExamInfo.Clear();

            string sqlLabExamInfo = "select * from CDSS_LabExamInfo where RecordSEQ=" + RecordSEQ;

            DataTable TableLabExamInfo = SQLServerDBInterface.GetDataSet(sqlLabExamInfo);
            if (TableLabExamInfo.Rows.Count != 1)    // 判读FamilyDiseaseHistoryInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询实验室检查信息失败！";
                return false;
            }

            foreach (DataRow Row in TableLabExamInfo.Rows)
            {
                GlobalData.LabExamInfo.LabExamDateTime = (DateTime)Row["LabExamDateTime"];
                GlobalData.LabExamInfo.BG = (string)Row["BG"];
                GlobalData.LabExamInfo.FBG = (string)Row["FBG"];
                GlobalData.LabExamInfo.TWOHPBG = (string)Row["TwoHPBG"];
                GlobalData.LabExamInfo.FoodCount = (string)Row["FoodCount"];
                GlobalData.LabExamInfo.OGTTFBG = (string)Row["OGTTFBG"];
                GlobalData.LabExamInfo.OGTTPBG = (string)Row["OGTTPBG"];
                GlobalData.LabExamInfo.BeforeBreakfast = (string)Row["BeforeBreakfast"];
                GlobalData.LabExamInfo.AfterBreakfast = (string)Row["AfterBreakfast"];
                GlobalData.LabExamInfo.BeforeLunch = (string)Row["BeforeLunch"];
                GlobalData.LabExamInfo.AfterLunch = (string)Row["AfterLunch"];
                GlobalData.LabExamInfo.BeforeSupper = (string)Row["BeforeSupper"];
                GlobalData.LabExamInfo.AfterSupper = (string)Row["AfterSupper"];
                GlobalData.LabExamInfo.BeforeSleep = (string)Row["BeforeSleep"];
                GlobalData.LabExamInfo.LC = (string)Row["LC"];
                GlobalData.LabExamInfo.TC = (string)Row["TC"];
                GlobalData.LabExamInfo.HDLC = (string)Row["HDLC"];
                GlobalData.LabExamInfo.TG = (string)Row["TG"];
                GlobalData.LabExamInfo.LDLC = (string)Row["LDLC"];
                GlobalData.LabExamInfo.CR = (string)Row["CR"];
                GlobalData.LabExamInfo.AlanineAminotransferase = (string)Row["AlanineAminotransferase"];
                GlobalData.LabExamInfo.UN = (string)Row["UN"];
                GlobalData.LabExamInfo.AspartateAminotransferase = (string)Row["AspartateAminotransferase"];
                GlobalData.LabExamInfo.ALBCR = (string)Row["ALBCR"];
                GlobalData.LabExamInfo.US = (string)Row["US"];
                GlobalData.LabExamInfo.UrinaryProtein = (string)Row["UrinaryProtein"];
                GlobalData.LabExamInfo.NTT = (string)Row["NTT"];
                GlobalData.LabExamInfo.UPH = (string)Row["UPH"];
                GlobalData.LabExamInfo.UUA = (string)Row["UUA"];
                GlobalData.LabExamInfo.HBA1C = (string)Row["HBA1C"];
                GlobalData.LabExamInfo.BCL = (string)Row["BCL"];
                GlobalData.LabExamInfo.BUA = (string)Row["BUA"];
                GlobalData.LabExamInfo.BKA = (string)Row["BKA"];
                GlobalData.LabExamInfo.BNA = (string)Row["BNA"];
                GlobalData.LabExamInfo.BCO2CP = (string)Row["BCO2CP"];
                GlobalData.LabExamInfo.BGA = (string)Row["BGA"];
                GlobalData.LabExamInfo.BP = (string)Row["BP"];
                GlobalData.LabExamInfo.SerumTotalProtein = (string)Row["SerumTotalProtein"];
                GlobalData.LabExamInfo.SerumAlbumin = (string)Row["SerumAlbumin"];
                GlobalData.LabExamInfo.FastingInsulin = (string)Row["FastingInsulin"];
                GlobalData.LabExamInfo.FastingCPeptide = (string)Row["FastingCPeptide"];
                GlobalData.LabExamInfo.PostprandialInsulin = (string)Row["PostprandialInsulin"];
                GlobalData.LabExamInfo.PostprandialCPeptide = (string)Row["PostprandialCPeptide"];
                GlobalData.LabExamInfo.ICA = (string)Row["ICA"];
                GlobalData.LabExamInfo.GDA65 = (string)Row["GDA65"];
                GlobalData.LabExamInfo.Tbil = (string)Row["Tbil"];
                GlobalData.LabExamInfo.AFP = (string)Row["AFP"];
                GlobalData.LabExamInfo.CEA = (string)Row["CEA"];
            }
            return true;
        }
        private static bool GetLabExamInfo(string RecordSEQ,classSQLServerDBInterface anotherSQLServerDBInterface)
        {
            //恢复默认值
            GlobalData.LabExamInfo.Clear();

            string sqlLabExamInfo = "select * from CDSS_LabExamInfo where RecordSEQ=" + RecordSEQ;

            DataTable TableLabExamInfo = anotherSQLServerDBInterface.GetDataSet(sqlLabExamInfo);
            if (TableLabExamInfo.Rows.Count != 1)    // 判读FamilyDiseaseHistoryInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询实验室检查信息失败！";
                return false;
            }

            foreach (DataRow Row in TableLabExamInfo.Rows)
            {
                GlobalData.LabExamInfo.LabExamDateTime = (DateTime)Row["LabExamDateTime"];
                GlobalData.LabExamInfo.BG = (string)Row["BG"];
                GlobalData.LabExamInfo.FBG = (string)Row["FBG"];
                GlobalData.LabExamInfo.TWOHPBG = (string)Row["TwoHPBG"];
                GlobalData.LabExamInfo.FoodCount = (string)Row["FoodCount"];
                GlobalData.LabExamInfo.OGTTFBG = (string)Row["OGTTFBG"];
                GlobalData.LabExamInfo.OGTTPBG = (string)Row["OGTTPBG"];
                GlobalData.LabExamInfo.BeforeBreakfast = (string)Row["BeforeBreakfast"];
                GlobalData.LabExamInfo.AfterBreakfast = (string)Row["AfterBreakfast"];
                GlobalData.LabExamInfo.BeforeLunch = (string)Row["BeforeLunch"];
                GlobalData.LabExamInfo.AfterLunch = (string)Row["AfterLunch"];
                GlobalData.LabExamInfo.BeforeSupper = (string)Row["BeforeSupper"];
                GlobalData.LabExamInfo.AfterSupper = (string)Row["AfterSupper"];
                GlobalData.LabExamInfo.BeforeSleep = (string)Row["BeforeSleep"];
                GlobalData.LabExamInfo.LC = (string)Row["LC"];
                GlobalData.LabExamInfo.TC = (string)Row["TC"];
                GlobalData.LabExamInfo.HDLC = (string)Row["HDLC"];
                GlobalData.LabExamInfo.TG = (string)Row["TG"];
                GlobalData.LabExamInfo.LDLC = (string)Row["LDLC"];
                GlobalData.LabExamInfo.CR = (string)Row["CR"];
                GlobalData.LabExamInfo.AlanineAminotransferase = (string)Row["AlanineAminotransferase"];
                GlobalData.LabExamInfo.UN = (string)Row["UN"];
                GlobalData.LabExamInfo.AspartateAminotransferase = (string)Row["AspartateAminotransferase"];
                GlobalData.LabExamInfo.ALBCR = (string)Row["ALBCR"];
                GlobalData.LabExamInfo.US = (string)Row["US"];
                GlobalData.LabExamInfo.UrinaryProtein = (string)Row["UrinaryProtein"];
                GlobalData.LabExamInfo.NTT = (string)Row["NTT"];
                GlobalData.LabExamInfo.UPH = (string)Row["UPH"];
                GlobalData.LabExamInfo.UUA = (string)Row["UUA"];
                GlobalData.LabExamInfo.HBA1C = (string)Row["HBA1C"];
                GlobalData.LabExamInfo.BCL = (string)Row["BCL"];
                GlobalData.LabExamInfo.BUA = (string)Row["BUA"];
                GlobalData.LabExamInfo.BKA = (string)Row["BKA"];
                GlobalData.LabExamInfo.BNA = (string)Row["BNA"];
                GlobalData.LabExamInfo.BCO2CP = (string)Row["BCO2CP"];
                GlobalData.LabExamInfo.BGA = (string)Row["BGA"];
                GlobalData.LabExamInfo.BP = (string)Row["BP"];
                GlobalData.LabExamInfo.SerumTotalProtein = (string)Row["SerumTotalProtein"];
                GlobalData.LabExamInfo.SerumAlbumin = (string)Row["SerumAlbumin"];
                GlobalData.LabExamInfo.FastingInsulin = (string)Row["FastingInsulin"];
                GlobalData.LabExamInfo.FastingCPeptide = (string)Row["FastingCPeptide"];
                GlobalData.LabExamInfo.PostprandialInsulin = (string)Row["PostprandialInsulin"];
                GlobalData.LabExamInfo.PostprandialCPeptide = (string)Row["PostprandialCPeptide"];
                GlobalData.LabExamInfo.ICA = (string)Row["ICA"];
                GlobalData.LabExamInfo.GDA65 = (string)Row["GDA65"];
                GlobalData.LabExamInfo.Tbil = (string)Row["Tbil"];
                GlobalData.LabExamInfo.AFP = (string)Row["AFP"];
                GlobalData.LabExamInfo.CEA = (string)Row["CEA"];
            }
            return true;
        }

        /// <summary>
        /// 保存实验室检查信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveLabExamInfo()
        {
            string sqlLabExamInfo = "insert into CDSS_LabExamInfo(RecordSEQ,LabExamDateTime,BG,FBG,TwoHPBG,FoodCount,"
                              + "OGTTFBG,OGTTPBG,BeforeBreakfast,AfterBreakfast,BeforeLunch,AfterLunch,BeforeSupper,AfterSupper, "
                              + "BeforeSleep,LC,TC,HDLC,TG,LDLC,CR,AlanineAminotransferase,UN,AspartateAminotransferase,"
                              + "ALBCR,US,UrinaryProtein,NTT,UPH,UUA,HBA1C,BCL,BUA,BKA,BNA,BCO2CP,BGA,BP,SerumTotalProtein,"
                              + "SerumAlbumin,FastingInsulin,FastingCPeptide,PostprandialInsulin,PostprandialCPeptide,ICA,GDA65,Tbil,AFP,CEA) "
                              + " values(@RecordSEQ,@LabExamDateTime,@BG,@FBG,@TwoHPBG,@FoodCount,"
                              + "@OGTTFBG,@OGTTPBG,@BeforeBreakfast,@AfterBreakfast,@BeforeLunch,@AfterLunch,@BeforeSupper,@AfterSupper, "
                              + "@BeforeSleep,@LC,@TC,@HDLC,@TG,@LDLC,@CR,@AlanineAminotransferase,@UN,@AspartateAminotransferase,"
                              + "@ALBCR,@US,@UrinaryProtein,@NTT,@UPH,@UUA,@HBA1C,@BCL,@BUA,@BKA,@BNA,@BCO2CP,@BGA,@BP,@SerumTotalProtein,"
                              + "@SerumAlbumin,@FastingInsulin,@FastingCPeptide,@PostprandialInsulin,@PostprandialCPeptide,@ICA,@GDA65,@Tbil,@AFP,@CEA) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@LabExamDateTime",GlobalData.LabExamInfo.LabExamDateTime),
                new SQLiteParameter("@BG",GlobalData.LabExamInfo.BG),
                new SQLiteParameter("@FBG",GlobalData.LabExamInfo.FBG),
                new SQLiteParameter("@TwoHPBG",GlobalData.LabExamInfo.TWOHPBG),
                new SQLiteParameter("@FoodCount",GlobalData.LabExamInfo.FoodCount),
                new SQLiteParameter("@OGTTFBG",GlobalData.LabExamInfo.OGTTFBG),
                new SQLiteParameter("@OGTTPBG",GlobalData.LabExamInfo.OGTTPBG),
                new SQLiteParameter("@BeforeBreakfast",GlobalData.LabExamInfo.BeforeBreakfast),
                new SQLiteParameter("@AfterBreakfast",GlobalData.LabExamInfo.AfterBreakfast),
                new SQLiteParameter("@BeforeLunch",GlobalData.LabExamInfo.BeforeLunch),
                new SQLiteParameter("@AfterLunch",GlobalData.LabExamInfo.AfterLunch ),
                new SQLiteParameter("@BeforeSupper",GlobalData.LabExamInfo.BeforeSupper),
                new SQLiteParameter("@AfterSupper",GlobalData.LabExamInfo.AfterSupper),
                new SQLiteParameter("@BeforeSleep",GlobalData.LabExamInfo.BeforeSleep),
                new SQLiteParameter("@LC",GlobalData.LabExamInfo.LC),
                new SQLiteParameter("@TC",GlobalData.LabExamInfo.TC),
                new SQLiteParameter("@HDLC",GlobalData.LabExamInfo.HDLC),
                new SQLiteParameter("@TG",GlobalData.LabExamInfo.TG),
                new SQLiteParameter("@LDLC",GlobalData.LabExamInfo.LDLC),
                new SQLiteParameter("@CR",GlobalData.LabExamInfo.CR ),
                new SQLiteParameter("@AlanineAminotransferase",GlobalData.LabExamInfo.AlanineAminotransferase),
                new SQLiteParameter("@UN",GlobalData.LabExamInfo.UN),
                new SQLiteParameter("@AspartateAminotransferase",GlobalData.LabExamInfo.AspartateAminotransferase ),
                new SQLiteParameter("@ALBCR",GlobalData.LabExamInfo.ALBCR),
                new SQLiteParameter("@US",GlobalData.LabExamInfo.US),
                new SQLiteParameter("@UrinaryProtein",GlobalData.LabExamInfo.UrinaryProtein),
                new SQLiteParameter("@NTT",GlobalData.LabExamInfo.NTT),
                new SQLiteParameter("@UPH",GlobalData.LabExamInfo.UPH),
                new SQLiteParameter("@UUA",GlobalData.LabExamInfo.UUA),
                new SQLiteParameter("@HBA1C",GlobalData.LabExamInfo.HBA1C),
                new SQLiteParameter("@BCL",GlobalData.LabExamInfo.BCL),
                new SQLiteParameter("@BUA",GlobalData.LabExamInfo.BUA),
                new SQLiteParameter("@BKA",GlobalData.LabExamInfo.BKA),
                new SQLiteParameter("@BNA",GlobalData.LabExamInfo.BNA),
                new SQLiteParameter("@BCO2CP",GlobalData.LabExamInfo.BCO2CP),
                new SQLiteParameter("@BGA",GlobalData.LabExamInfo.BGA),
                new SQLiteParameter("@BP",GlobalData.LabExamInfo.BP),
                new SQLiteParameter("@SerumTotalProtein",GlobalData.LabExamInfo.SerumTotalProtein),
                new SQLiteParameter("@SerumAlbumin",GlobalData.LabExamInfo.SerumAlbumin),
                new SQLiteParameter("@FastingInsulin",GlobalData.LabExamInfo.FastingInsulin ),
                new SQLiteParameter("@FastingCPeptide",GlobalData.LabExamInfo.FastingCPeptide ),
                new SQLiteParameter("@PostprandialInsulin",GlobalData.LabExamInfo.PostprandialInsulin),
                new SQLiteParameter("@PostprandialCPeptide",GlobalData.LabExamInfo.PostprandialCPeptide),
                new SQLiteParameter("@ICA", GlobalData.LabExamInfo.ICA ),
                new SQLiteParameter("@GDA65",GlobalData.LabExamInfo.GDA65),
                new SQLiteParameter("@Tbil",GlobalData.LabExamInfo.Tbil),
                new SQLiteParameter("@AFP",GlobalData.LabExamInfo.AFP),
                new SQLiteParameter("@CEA",GlobalData.LabExamInfo.CEA)
			};

            try
            {
                int result = SQLServerDBInterface.ExecuteCommand(sqlLabExamInfo, para);
                if (result > 0)
                    return true;
                else
                {
                    LastErrorInfo = "保存实验室检查信息失败！";
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        #region 其他检查
        /// <summary>
        /// 查询其他检查信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetOtherExamInfo(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.OtherExamInfo.Clear();

            string sqlOtherExamInfo = "select * from CDSS_OtherExamInfo where RecordSEQ=" + RecordSEQ;
            string sqlVascularUltrasound = "select * from CDSS_VascularUltrasound where TableName='OtherExamInfo' and RecordSEQ=" + RecordSEQ;
            string sqlOtherExamAbnormal = "select * from CDSS_OtherExamAbnormal where TableName='OtherExamInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableOtherExamInfo = SQLServerDBInterface.GetDataSet(sqlOtherExamInfo);
            if (TableOtherExamInfo.Rows.Count > 1)    // 判读OtherExamInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询其他检查信息出现错误！";
                return false;
            }
            else if (TableOtherExamInfo.Rows.Count == 0)   //数据库没有该条信息
            {
                return true;
            }

            foreach (DataRow Row in TableOtherExamInfo.Rows)
            {
                GlobalData.OtherExamInfo.ECGAbnormalType = (string)Row["ECGAbnormalType"];

                if (Row["HasECGAbnormal"].ToString().Trim().Equals("true"))
                    GlobalData.OtherExamInfo.HasECGAbnormal = true;
                else
                    GlobalData.OtherExamInfo.HasECGAbnormal = false;

            }

            DataTable TableVascularUltrasound = SQLServerDBInterface.GetDataSet(sqlVascularUltrasound);
            DataTable TableOtherExamAbnormal = SQLServerDBInterface.GetDataSet(sqlOtherExamAbnormal);

            if (TableVascularUltrasound.Rows.Count > 0)
            {
                foreach (DataRow Row in TableVascularUltrasound.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("VascularUltrasound"))  //血管超声类型
                    {
                        CDSSVascularUltrasound VascularUltrasound = new CDSSVascularUltrasound();
                        VascularUltrasound.VascularAbnormalType = (string)Row["VascularAbnormalType"];
                        VascularUltrasound.VascularAbnormalPart = (string)Row["VascularAbnormalPart"];

                        GlobalData.OtherExamInfo.listVascularUltrasound.Add(VascularUltrasound);
                    }
                }
            }
            if (TableOtherExamAbnormal.Rows.Count > 0)
            {
                foreach (DataRow Row in TableOtherExamAbnormal.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("OtherExamAbnormal"))  //其他检查
                    {
                        CDSSOtherExamAbnormal OtherExamAbnormal = new CDSSOtherExamAbnormal();
                        OtherExamAbnormal.ExamItemName = (string)Row["ExamItemName"];
                        OtherExamAbnormal.ExamResult = (string)Row["ExamResult"];

                        GlobalData.OtherExamInfo.listOtherExamAbnormal.Add(OtherExamAbnormal);
                    }
                }
            }
            return true;
        }
        private static bool GetOtherExamInfo(string RecordSEQ, classSQLServerDBInterface anotherSQLServerDBInterface)
        {
            //恢复默认值
            GlobalData.OtherExamInfo.Clear();

            string sqlOtherExamInfo = "select * from CDSS_OtherExamInfo where RecordSEQ=" + RecordSEQ;
            string sqlVascularUltrasound = "select * from CDSS_VascularUltrasound where TableName='OtherExamInfo' and RecordSEQ=" + RecordSEQ;
            string sqlOtherExamAbnormal = "select * from CDSS_OtherExamAbnormal where TableName='OtherExamInfo' and RecordSEQ=" + RecordSEQ;

            DataTable TableOtherExamInfo = anotherSQLServerDBInterface.GetDataSet(sqlOtherExamInfo);
            if (TableOtherExamInfo.Rows.Count > 1)    // 判读OtherExamInfo信息是否只有1条记录，不是则返回false
            {
                LastErrorInfo = "查询其他检查信息出现错误！";
                return false;
            }
            else if (TableOtherExamInfo.Rows.Count == 0)   //数据库没有该条信息
            {
                return true;
            }

            foreach (DataRow Row in TableOtherExamInfo.Rows)
            {
                GlobalData.OtherExamInfo.ECGAbnormalType = (string)Row["ECGAbnormalType"];

                if (Row["HasECGAbnormal"].ToString().Trim().Equals("true"))
                    GlobalData.OtherExamInfo.HasECGAbnormal = true;
                else
                    GlobalData.OtherExamInfo.HasECGAbnormal = false;

            }

            DataTable TableVascularUltrasound = anotherSQLServerDBInterface.GetDataSet(sqlVascularUltrasound);
            DataTable TableOtherExamAbnormal = anotherSQLServerDBInterface.GetDataSet(sqlOtherExamAbnormal);

            if (TableVascularUltrasound.Rows.Count > 0)
            {
                foreach (DataRow Row in TableVascularUltrasound.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("VascularUltrasound"))  //血管超声类型
                    {
                        CDSSVascularUltrasound VascularUltrasound = new CDSSVascularUltrasound();
                        VascularUltrasound.VascularAbnormalType = (string)Row["VascularAbnormalType"];
                        VascularUltrasound.VascularAbnormalPart = (string)Row["VascularAbnormalPart"];

                        GlobalData.OtherExamInfo.listVascularUltrasound.Add(VascularUltrasound);
                    }
                }
            }
            if (TableOtherExamAbnormal.Rows.Count > 0)
            {
                foreach (DataRow Row in TableOtherExamAbnormal.Rows)
                {
                    if (Row["Types"].ToString().Trim().Equals("OtherExamAbnormal"))  //其他检查
                    {
                        CDSSOtherExamAbnormal OtherExamAbnormal = new CDSSOtherExamAbnormal();
                        OtherExamAbnormal.ExamItemName = (string)Row["ExamItemName"];
                        OtherExamAbnormal.ExamResult = (string)Row["ExamResult"];

                        GlobalData.OtherExamInfo.listOtherExamAbnormal.Add(OtherExamAbnormal);
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 保存其他检查信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveOtherExamInfo()
        {
            string sqlOtherExamInfo = "insert into CDSS_OtherExamInfo(RecordSEQ,HasECGAbnormal,ECGAbnormalType) "
                                   + " values(@RecordSEQ,@HasECGAbnormal,@ECGAbnormalType) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@HasECGAbnormal",GlobalData.OtherExamInfo.HasECGAbnormal.ToString().ToLower()),
                new SQLiteParameter("@ECGAbnormalType",GlobalData.OtherExamInfo.ECGAbnormalType)
			};

            string sqlVascularUltrasound = "";
            string sqlOtherExamAbnormal = "";

            //血管超声类型
            if (GlobalData.OtherExamInfo.listVascularUltrasound.Count > 0)
            {
                for (int i = 0; i < GlobalData.OtherExamInfo.listVascularUltrasound.Count; i++)
                {
                    sqlVascularUltrasound += "insert into CDSS_VascularUltrasound(RecordSEQ,TableName,Types,VascularAbnormalType,VascularAbnormalPart) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq
                    + " ,'OtherExamInfo','VascularUltrasound','"
                    + GlobalData.OtherExamInfo.listVascularUltrasound[i].VascularAbnormalType + "','"
                    + GlobalData.OtherExamInfo.listVascularUltrasound[i].VascularAbnormalPart + "') ;";
                }
            }
            //其他检查
            if (GlobalData.OtherExamInfo.listOtherExamAbnormal.Count > 0)
            {
                for (int i = 0; i < GlobalData.OtherExamInfo.listOtherExamAbnormal.Count; i++)
                {
                    sqlOtherExamAbnormal += "insert into CDSS_OtherExamAbnormal(RecordSEQ,TableName,Types,ExamItemName,ExamResult)"
                       + "values(" + GlobalData.RecordInfo.RecordSeq
                       + " ,'OtherExamInfo','OtherExamAbnormal','"
                       + GlobalData.OtherExamInfo.listOtherExamAbnormal[i].ExamItemName + "','"
                       + GlobalData.OtherExamInfo.listOtherExamAbnormal[i].ExamResult + "');";
                }
            }

            SQLiteCommand sqlCmd = SQLServerDBInterface.Connection.CreateCommand();
            sqlCmd.Connection = SQLServerDBInterface.Connection;
            sqlCmd.Transaction = SQLServerDBInterface.Connection.BeginTransaction();
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                sqlCmd.CommandText = sqlOtherExamInfo;
                sqlCmd.Parameters.AddRange(para);
                sqlCmd.ExecuteNonQuery();

                if (sqlVascularUltrasound != "")
                {
                    sqlCmd.CommandText = sqlVascularUltrasound;
                    sqlCmd.ExecuteNonQuery();
                }
                if (sqlOtherExamAbnormal != "")
                {
                    sqlCmd.CommandText = sqlOtherExamAbnormal;
                    sqlCmd.ExecuteNonQuery();
                }

                sqlCmd.Transaction.Commit();       //接受交易，完成操作
                return true;

            }
            catch (SQLiteException e)
            {
                sqlCmd.Transaction.Rollback();
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        #region 诊断结论
        /// <summary>
        /// 查询诊断结论信息
        /// </summary>
        ///// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetDiagnosedResultList(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.DiagnosedResult.Clear();
            string sql = "select * from CDSS_DiagnosedResult where RecordSEQ=" + RecordSEQ;
            DataTable TableDiagnosedResult = SQLServerDBInterface.GetDataSet(sql);

            if (TableDiagnosedResult.Rows.Count == 0)   //数据库没有该条信息
            {
                return true;
            }
            else if (TableDiagnosedResult.Rows.Count > 0)
            {
                foreach (DataRow Row in TableDiagnosedResult.Rows)
                {
                    if ((string)Row["Name"] == "代谢综合征")
                    {
                        GlobalData.DiagnosedResult.HasMS = (string)Row["Result"];
                    }
                    else if ((string)Row["Name"] == "危险度积分")
                    {
                        GlobalData.DiagnosedResult.RiskDegreeCode = (string)Row["Result"];
                    }
                    else if ((string)Row["Name"] == "危险度")
                    {
                        GlobalData.DiagnosedResult.RiskDegree = (string)Row["Result"];
                    }
                    else
                    {
                        CDSSOneDiseaseDiagnosedResult OneDiseaseDiagnosedResult = new CDSSOneDiseaseDiagnosedResult();
                        OneDiseaseDiagnosedResult.Name = (string)Row["Name"];
                        OneDiseaseDiagnosedResult.Result = (string)Row["Result"];
                        OneDiseaseDiagnosedResult.TreatmentTarget = (string)Row["TreatmentTarget"];
                        OneDiseaseDiagnosedResult.TreatmentSuggestion = (string)Row["TreatmentSuggestion"];
                        OneDiseaseDiagnosedResult.SelfCheck = (string)Row["SelfCheck"];
                        OneDiseaseDiagnosedResult.DataNeeded = (string)Row["DataNeeded"];
                        OneDiseaseDiagnosedResult.DiagnosisSteps = (string)Row["DiagnosisSteps"];
                        GlobalData.DiagnosedResult.DiseaseDiagnosedResultList.Add(OneDiseaseDiagnosedResult);
                    }

                }
            }
            return true;
        }
        private static bool GetDiagnosedResultList(string RecordSEQ, classSQLServerDBInterface anotherSQLServerDBInterface)
        {
            //恢复默认值
            GlobalData.DiagnosedResult.Clear();
            string sql = "select * from CDSS_DiagnosedResult where RecordSEQ=" + RecordSEQ;
            DataTable TableDiagnosedResult = anotherSQLServerDBInterface.GetDataSet(sql);

            if (TableDiagnosedResult.Rows.Count == 0)   //数据库没有该条信息
            {
                return true;
            }
            else if (TableDiagnosedResult.Rows.Count > 0)
            {
                foreach (DataRow Row in TableDiagnosedResult.Rows)
                {
                    if ((string)Row["Name"] == "代谢综合征")
                    {
                        GlobalData.DiagnosedResult.HasMS = (string)Row["Result"];
                    }
                    else if ((string)Row["Name"] == "危险度积分")
                    {
                        GlobalData.DiagnosedResult.RiskDegreeCode = (string)Row["Result"];
                    }
                    else if ((string)Row["Name"] == "危险度")
                    {
                        GlobalData.DiagnosedResult.RiskDegree = (string)Row["Result"];
                    }
                    else
                    {
                        CDSSOneDiseaseDiagnosedResult OneDiseaseDiagnosedResult = new CDSSOneDiseaseDiagnosedResult();
                        OneDiseaseDiagnosedResult.Name = (string)Row["Name"];
                        OneDiseaseDiagnosedResult.Result = (string)Row["Result"];
                        OneDiseaseDiagnosedResult.TreatmentTarget = (string)Row["TreatmentTarget"];
                        OneDiseaseDiagnosedResult.TreatmentSuggestion = (string)Row["TreatmentSuggestion"];
                        OneDiseaseDiagnosedResult.SelfCheck = (string)Row["SelfCheck"];
                        OneDiseaseDiagnosedResult.DataNeeded = (string)Row["DataNeeded"];
                        OneDiseaseDiagnosedResult.DiagnosisSteps = (string)Row["DiagnosisSteps"];
                        GlobalData.DiagnosedResult.DiseaseDiagnosedResultList.Add(OneDiseaseDiagnosedResult);
                    }

                }
            }
            return true;
        }


        /******************       
         * 
         * Add BY ZX 2010-03-30
         *                          
         ******************/
        /// <summary>
        /// 查询推理机诊断结论信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetReasoningDiagnoseResultList(string RecordSEQ)
        {
            string sql = "select * from CDSS_ReasonDiagnosedResult where RecordSEQ=" + RecordSEQ;
            DataTable TableDiagnosedResult = SQLServerDBInterface.GetDataSet(sql);

            if (TableDiagnosedResult.Rows.Count == 0)   //数据库没有该条信息
            {
                return true;
            }
            else if (TableDiagnosedResult.Rows.Count > 0)
            {
                foreach (DataRow Row in TableDiagnosedResult.Rows)
                {
                    if ((string)Row["Name"] == "代谢综合征")
                    {
                        GlobalData.DiagnosedResult.HasMS = (string)Row["Result"];
                    }
                    else if ((string)Row["Name"] == "危险度积分")
                    {
                        GlobalData.DiagnosedResult.RiskDegreeCode = (string)Row["Result"];
                    }
                    else if ((string)Row["Name"] == "危险度")
                    {
                        GlobalData.DiagnosedResult.RiskDegree = (string)Row["Result"];
                    }
                    else
                    {
                        CDSSOneDiseaseDiagnosedResult OneDiseaseDiagnosedResult = new CDSSOneDiseaseDiagnosedResult();
                        OneDiseaseDiagnosedResult.Name = (string)Row["Name"];
                        OneDiseaseDiagnosedResult.Result = (string)Row["Result"];
                        OneDiseaseDiagnosedResult.TreatmentTarget = (string)Row["TreatmentTarget"];
                        OneDiseaseDiagnosedResult.TreatmentSuggestion = (string)Row["TreatmentSuggestion"];
                        OneDiseaseDiagnosedResult.SelfCheck = (string)Row["SelfCheck"];
                        OneDiseaseDiagnosedResult.DataNeeded = (string)Row["DataNeeded"];
                        OneDiseaseDiagnosedResult.DiagnosisSteps = (string)Row["DiagnosisSteps"];
                        GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList.Add(OneDiseaseDiagnosedResult);
                    }

                }
            }
            return true;
        }
        private static bool GetReasoningDiagnoseResultList(string RecordSEQ, classSQLServerDBInterface anotherSQLServerDBInterface)
        {
            string sql = "select * from CDSS_ReasonDiagnosedResult where RecordSEQ=" + RecordSEQ;
            DataTable TableDiagnosedResult = anotherSQLServerDBInterface.GetDataSet(sql);

            if (TableDiagnosedResult.Rows.Count == 0)   //数据库没有该条信息
            {
                return true;
            }
            else if (TableDiagnosedResult.Rows.Count > 0)
            {
                foreach (DataRow Row in TableDiagnosedResult.Rows)
                {
                    if ((string)Row["Name"] == "代谢综合征")
                    {
                        GlobalData.DiagnosedResult.HasMS = (string)Row["Result"];
                    }
                    else if ((string)Row["Name"] == "危险度积分")
                    {
                        GlobalData.DiagnosedResult.RiskDegreeCode = (string)Row["Result"];
                    }
                    else if ((string)Row["Name"] == "危险度")
                    {
                        GlobalData.DiagnosedResult.RiskDegree = (string)Row["Result"];
                    }
                    else
                    {
                        CDSSOneDiseaseDiagnosedResult OneDiseaseDiagnosedResult = new CDSSOneDiseaseDiagnosedResult();
                        OneDiseaseDiagnosedResult.Name = (string)Row["Name"];
                        OneDiseaseDiagnosedResult.Result = (string)Row["Result"];
                        OneDiseaseDiagnosedResult.TreatmentTarget = (string)Row["TreatmentTarget"];
                        OneDiseaseDiagnosedResult.TreatmentSuggestion = (string)Row["TreatmentSuggestion"];
                        OneDiseaseDiagnosedResult.SelfCheck = (string)Row["SelfCheck"];
                        OneDiseaseDiagnosedResult.DataNeeded = (string)Row["DataNeeded"];
                        OneDiseaseDiagnosedResult.DiagnosisSteps = (string)Row["DiagnosisSteps"];
                        GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList.Add(OneDiseaseDiagnosedResult);
                    }

                }
            }
            return true;
        }

        /// <summary>
        /// 保存诊断结论信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveDiagnosedResultList()
        {
            string sqlDiagnosedResultList = "";
            sqlDiagnosedResultList += "insert into CDSS_DiagnosedResult(RecordSEQ,Name,Result) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq + ",'代谢综合征','" + GlobalData.DiagnosedResult.HasMS + "');";
            sqlDiagnosedResultList += "insert into CDSS_DiagnosedResult(RecordSEQ,Name,Result) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq + ",'危险度积分','" + GlobalData.DiagnosedResult.RiskDegreeCode + "');";
            sqlDiagnosedResultList += "insert into CDSS_DiagnosedResult(RecordSEQ,Name,Result) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq + ",'危险度','" + GlobalData.DiagnosedResult.RiskDegree + "');";

            if (GlobalData.DiagnosedResult.DiseaseDiagnosedResultList.Count > 0)
            {
                for (int i = 0; i < GlobalData.DiagnosedResult.DiseaseDiagnosedResultList.Count; i++)
                {
                    sqlDiagnosedResultList += "insert into CDSS_DiagnosedResult(RecordSEQ,Name,Result,TreatmentTarget,TreatmentSuggestion,SelfCheck,DataNeeded,DiagnosisSteps) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq + ",'"
                    + GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].Name + "','"
                    + GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].Result + "','"
                    + GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].TreatmentTarget + "','"
                    + GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].TreatmentSuggestion + "','"
                    + GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].SelfCheck + "','"
                    + GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].DataNeeded + "','"
                    + GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].DiagnosisSteps + "') ;";
                }
            }
            else
            {
                return true;
            }

            try
            {
                int result = SQLServerDBInterface.ExecuteCommand(sqlDiagnosedResultList);
                if (result > 0)
                    return true;
                else
                {
                    LastErrorInfo = "保存诊断结论信息失败！";
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                LastErrorInfo = e.Message;
                return false;
            }

        }


        /******************       
         * 
         * Add BY ZX 2010-03-30
         *                          
         ******************/
        /// <summary>
        /// 保存推理机诊断结论信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveReasoningDiagnosedResultList()
        {
            string sqlDiagnosedResultList = "";
            sqlDiagnosedResultList += "insert into CDSS_ReasonDiagnosedResult(RecordSEQ,Name,Result) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq + ",'代谢综合征','" + GlobalData.DiagnosedResult.HasMS + "');";
            sqlDiagnosedResultList += "insert into CDSS_ReasonDiagnosedResult(RecordSEQ,Name,Result) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq + ",'危险度积分','" + GlobalData.DiagnosedResult.RiskDegreeCode + "');";
            sqlDiagnosedResultList += "insert into CDSS_ReasonDiagnosedResult(RecordSEQ,Name,Result) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq + ",'危险度','" + GlobalData.DiagnosedResult.RiskDegree + "');";

            if (GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList.Count > 0)
            {
                for (int i = 0; i < GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList.Count; i++)
                {
                    sqlDiagnosedResultList += "insert into CDSS_ReasonDiagnosedResult(RecordSEQ,Name,Result,TreatmentTarget,TreatmentSuggestion,SelfCheck,DataNeeded,DiagnosisSteps) "
                    + " values(" + GlobalData.RecordInfo.RecordSeq + ",'"
                    + GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].Name + "','"
                    + GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].Result + "','"
                    + GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].TreatmentTarget + "','"
                    + GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].TreatmentSuggestion + "','"
                    + GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].SelfCheck + "','"
                    + GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].DataNeeded + "','"
                    + GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList[i].DiagnosisSteps + "') ;";
                }
            }
            else
            {
                return true;
            }

            try
            {
                int result = SQLServerDBInterface.ExecuteCommand(sqlDiagnosedResultList);
                if (result > 0)
                    return true;
                else
                {
                    LastErrorInfo = "保存诊断结论信息失败！";
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                LastErrorInfo = e.Message;
                return false;
            }

        }
        #endregion

        #region 膳食处方
        /// <summary>
        /// 查询膳食处方信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetDietSuggestion(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.DietSuggestion.Clear();

            string sql = "select * from CDSS_DietSuggestion where RecordSEQ=" + RecordSEQ;
            DataTable TableDietSuggestion = SQLServerDBInterface.GetDataSet(sql);

            if (TableDietSuggestion.Rows.Count > 0)
            {
                foreach (DataRow Row in TableDietSuggestion.Rows)
                {

                    GlobalData.DietSuggestion.DietType = (string)Row["DietType"];
                    GlobalData.DietSuggestion.TotalEnergy = (string)Row["TotalEnergy"];
                    GlobalData.DietSuggestion.TotalWater = (string)Row["TotalWater"];
                    GlobalData.DietSuggestion.CarboPercent = (string)Row["CarboPercent"];
                    GlobalData.DietSuggestion.CarboCount = (string)Row["CarboCount"];
                    GlobalData.DietSuggestion.CerealCount = (string)Row["CerealCount"];
                    GlobalData.DietSuggestion.CerealDetail = (string)Row["CerealDetail"];
                    GlobalData.DietSuggestion.Fruitcount = (string)Row["Fruitcount"];
                    GlobalData.DietSuggestion.FruitDetail = (string)Row["FruitDetail"];
                    GlobalData.DietSuggestion.GreenstuffCount = (string)Row["GreenstuffCount"];
                    GlobalData.DietSuggestion.GreenstuffDetail = (string)Row["GreenstuffDetail"];
                    GlobalData.DietSuggestion.ProteinPercent = (string)Row["ProteinPercent"];
                    GlobalData.DietSuggestion.ProteinCount = (string)Row["ProteinCount"];
                    GlobalData.DietSuggestion.DairyCount = (string)Row["DairyCount"];
                    GlobalData.DietSuggestion.DairyDetail = (string)Row["DairyDetail"];
                    GlobalData.DietSuggestion.EggCount = (string)Row["EggCount"];
                    GlobalData.DietSuggestion.EggDetail = (string)Row["EggDetail"];
                    GlobalData.DietSuggestion.MeatCount = (string)Row["MeatCount"];
                    GlobalData.DietSuggestion.MeatDetail = (string)Row["MeatDetail"];
                    GlobalData.DietSuggestion.BeanProductCount = (string)Row["BeanProductCount"];
                    GlobalData.DietSuggestion.BeanProductDetail = (string)Row["BeanProductDetail"];
                    GlobalData.DietSuggestion.FatPercent = (string)Row["FatPercent"];
                    GlobalData.DietSuggestion.FatCount = (string)Row["FatCount"];
                    GlobalData.DietSuggestion.VegetableOilCount = (string)Row["VegetableOilCount"];
                    GlobalData.DietSuggestion.VegetableOilDetail = (string)Row["VegetableOilDetail"];
                    GlobalData.DietSuggestion.OtherFatFoodCount = (string)Row["OtherFatFoodCount"];
                    GlobalData.DietSuggestion.OtherFatFoodDetail = (string)Row["OtherFatFoodDetail"];
                    GlobalData.DietSuggestion.LimitedFood = (string)Row["LimitedFood"];
                    GlobalData.DietSuggestion.AvoidFood = (string)Row["AvoidFood"];
                    GlobalData.DietSuggestion.DataNeeded = (string)Row["DataNeeded"];
                }
            }
            return true;

        }
        private static bool GetDietSuggestion(string RecordSEQ,classSQLServerDBInterface anotherSQLServerDBInterface)
        {
            //恢复默认值
            GlobalData.DietSuggestion.Clear();

            string sql = "select * from CDSS_DietSuggestion where RecordSEQ=" + RecordSEQ;
            DataTable TableDietSuggestion = anotherSQLServerDBInterface.GetDataSet(sql);

            if (TableDietSuggestion.Rows.Count > 0)
            {
                foreach (DataRow Row in TableDietSuggestion.Rows)
                {

                    GlobalData.DietSuggestion.DietType = (string)Row["DietType"];
                    GlobalData.DietSuggestion.TotalEnergy = (string)Row["TotalEnergy"];
                    GlobalData.DietSuggestion.TotalWater = (string)Row["TotalWater"];
                    GlobalData.DietSuggestion.CarboPercent = (string)Row["CarboPercent"];
                    GlobalData.DietSuggestion.CarboCount = (string)Row["CarboCount"];
                    GlobalData.DietSuggestion.CerealCount = (string)Row["CerealCount"];
                    GlobalData.DietSuggestion.CerealDetail = (string)Row["CerealDetail"];
                    GlobalData.DietSuggestion.Fruitcount = (string)Row["Fruitcount"];
                    GlobalData.DietSuggestion.FruitDetail = (string)Row["FruitDetail"];
                    GlobalData.DietSuggestion.GreenstuffCount = (string)Row["GreenstuffCount"];
                    GlobalData.DietSuggestion.GreenstuffDetail = (string)Row["GreenstuffDetail"];
                    GlobalData.DietSuggestion.ProteinPercent = (string)Row["ProteinPercent"];
                    GlobalData.DietSuggestion.ProteinCount = (string)Row["ProteinCount"];
                    GlobalData.DietSuggestion.DairyCount = (string)Row["DairyCount"];
                    GlobalData.DietSuggestion.DairyDetail = (string)Row["DairyDetail"];
                    GlobalData.DietSuggestion.EggCount = (string)Row["EggCount"];
                    GlobalData.DietSuggestion.EggDetail = (string)Row["EggDetail"];
                    GlobalData.DietSuggestion.MeatCount = (string)Row["MeatCount"];
                    GlobalData.DietSuggestion.MeatDetail = (string)Row["MeatDetail"];
                    GlobalData.DietSuggestion.BeanProductCount = (string)Row["BeanProductCount"];
                    GlobalData.DietSuggestion.BeanProductDetail = (string)Row["BeanProductDetail"];
                    GlobalData.DietSuggestion.FatPercent = (string)Row["FatPercent"];
                    GlobalData.DietSuggestion.FatCount = (string)Row["FatCount"];
                    GlobalData.DietSuggestion.VegetableOilCount = (string)Row["VegetableOilCount"];
                    GlobalData.DietSuggestion.VegetableOilDetail = (string)Row["VegetableOilDetail"];
                    GlobalData.DietSuggestion.OtherFatFoodCount = (string)Row["OtherFatFoodCount"];
                    GlobalData.DietSuggestion.OtherFatFoodDetail = (string)Row["OtherFatFoodDetail"];
                    GlobalData.DietSuggestion.LimitedFood = (string)Row["LimitedFood"];
                    GlobalData.DietSuggestion.AvoidFood = (string)Row["AvoidFood"];
                    GlobalData.DietSuggestion.DataNeeded = (string)Row["DataNeeded"];
                }
            }
            return true;

        }
        /// <summary>
        /// 保存膳食处方信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveDietSuggestion()
        {
            string sqlDietSuggestion = "insert into CDSS_DietSuggestion(RecordSEQ,DietType,TotalEnergy,TotalWater,CarboPercent,CarboCount,"
                              + "CerealCount,CerealDetail,Fruitcount,FruitDetail,GreenstuffCount,GreenstuffDetail,ProteinPercent,ProteinCount,"
                              + "DairyCount,DairyDetail,EggCount,EggDetail,MeatCount,MeatDetail,BeanProductCount,BeanProductDetail,FatPercent,"
                              + "FatCount,VegetableOilCount,VegetableOilDetail,OtherFatFoodCount,OtherFatFoodDetail,"
                              + "LimitedFood,AvoidFood,DataNeeded) "
                              + " values(@RecordSEQ,@DietType,@TotalEnergy,@TotalWater,@CarboPercent,@CarboCount,"
                              + "@CerealCount,@CerealDetail,@Fruitcount,@FruitDetail,@GreenstuffCount,@GreenstuffDetail,@ProteinPercent,@ProteinCount,"
                              + "@DairyCount,@DairyDetail,@EggCount,@EggDetail,@MeatCount,@MeatDetail,@BeanProductCount,@BeanProductDetail,@FatPercent,"
                              + "@FatCount,@VegetableOilCount,@VegetableOilDetail,@OtherFatFoodCount,@OtherFatFoodDetail,"
                              + "@LimitedFood,@AvoidFood,@DataNeeded) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@DietType",GlobalData.DietSuggestion.DietType),
                new SQLiteParameter("@TotalEnergy",GlobalData.DietSuggestion.TotalEnergy),
                new SQLiteParameter("@TotalWater",GlobalData.DietSuggestion.TotalWater),
                new SQLiteParameter("@CarboPercent",GlobalData.DietSuggestion.CarboPercent),
                new SQLiteParameter("@CarboCount",GlobalData.DietSuggestion.CarboCount),
                new SQLiteParameter("@CerealCount",GlobalData.DietSuggestion.CerealCount),
                new SQLiteParameter("@CerealDetail",GlobalData.DietSuggestion.CerealDetail),
                new SQLiteParameter("@Fruitcount",GlobalData.DietSuggestion.Fruitcount),
                new SQLiteParameter("@FruitDetail",GlobalData.DietSuggestion.FruitDetail),
                new SQLiteParameter("@GreenstuffCount",GlobalData.DietSuggestion.GreenstuffCount),
                new SQLiteParameter("@GreenstuffDetail",GlobalData.DietSuggestion.GreenstuffDetail ),
                new SQLiteParameter("@ProteinPercent",GlobalData.DietSuggestion.ProteinPercent),
                new SQLiteParameter("@ProteinCount",GlobalData.DietSuggestion.ProteinCount),
                new SQLiteParameter("@DairyCount",GlobalData.DietSuggestion.DairyCount),
                new SQLiteParameter("@DairyDetail",GlobalData.DietSuggestion.DairyDetail),
                new SQLiteParameter("@EggCount",GlobalData.DietSuggestion.EggCount),
                new SQLiteParameter("@EggDetail",GlobalData.DietSuggestion.EggDetail),
                new SQLiteParameter("@MeatCount",GlobalData.DietSuggestion.MeatCount),
                new SQLiteParameter("@MeatDetail",GlobalData.DietSuggestion.MeatDetail),
                new SQLiteParameter("@BeanProductCount",GlobalData.DietSuggestion.BeanProductCount),
                new SQLiteParameter("@BeanProductDetail",GlobalData.DietSuggestion.BeanProductDetail),
                new SQLiteParameter("@FatPercent",GlobalData.DietSuggestion.FatPercent),
                new SQLiteParameter("@FatCount",GlobalData.DietSuggestion.FatCount ),
                new SQLiteParameter("@VegetableOilCount",GlobalData.DietSuggestion.VegetableOilCount),
                new SQLiteParameter("@VegetableOilDetail",GlobalData.DietSuggestion.VegetableOilDetail),
                new SQLiteParameter("@OtherFatFoodCount",GlobalData.DietSuggestion.OtherFatFoodCount),
                new SQLiteParameter("@OtherFatFoodDetail",GlobalData.DietSuggestion.OtherFatFoodDetail),
                //revise by lch 090331 修复BugDB00005739，
                //LimitedFood的值被赋值为GlobalData.DietSuggestion.OtherFatFoodDetail，
                //改正后还将数据库中LimitedFood、AvoidFood、DataNeeded、DietType的数据长度加长到Max
                //运动建议的表中ExerciseTarget、EnergyCost、DataNeeded数据长度也做了同样的修改。
                new SQLiteParameter("@LimitedFood",GlobalData.DietSuggestion.LimitedFood),
                new SQLiteParameter("@AvoidFood",GlobalData.DietSuggestion.AvoidFood),
                new SQLiteParameter("@DataNeeded",GlobalData.DietSuggestion.DataNeeded)
            
			};

            try
            {
                int result = SQLServerDBInterface.ExecuteCommand(sqlDietSuggestion, para);
                if (result > 0)
                    return true;
                else
                {
                    LastErrorInfo = "保存膳食处方信息失败！";
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        #region 运动建议
        /// <summary>
        /// 查询运动建议信息
        /// </summary>
        /// <param name="RecordSEQ"></param>
        /// <returns></returns>
        private static bool GetExerciseSuggestion(string RecordSEQ)
        {
            //恢复默认值
            GlobalData.ExerciseSuggestion.Clear();

            string sql = "select * from CDSS_ExerciseSuggestion where RecordSEQ=" + RecordSEQ;
            DataTable TableExerciseSuggestion = SQLServerDBInterface.GetDataSet(sql);

            if (TableExerciseSuggestion.Rows.Count > 0)
            {
                foreach (DataRow Row in TableExerciseSuggestion.Rows)
                {

                    GlobalData.ExerciseSuggestion.ExerciseTarget = (string)Row["ExerciseTarget"];
                    GlobalData.ExerciseSuggestion.EnergyCost = (string)Row["EnergyCost"];
                    GlobalData.ExerciseSuggestion.NoIntensityExercise = (string)Row["NoIntensityExercise"];
                    GlobalData.ExerciseSuggestion.NoIntensityExerciseItems = (string)Row["NoIntensityExerciseItems"];
                    GlobalData.ExerciseSuggestion.LowIntensityExercise = (string)Row["LowIntensityExercise"];
                    GlobalData.ExerciseSuggestion.LowIntensityExerciseItems = (string)Row["LowIntensityExerciseItems"];
                    GlobalData.ExerciseSuggestion.MiddleIntensityExercise = (string)Row["MiddleIntensityExercise"];
                    GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems = (string)Row["MiddleIntensityExerciseItems"];
                    GlobalData.ExerciseSuggestion.HighIntensityExercise = (string)Row["HighIntensityExercise"];
                    GlobalData.ExerciseSuggestion.HighIntensityExerciseItems = (string)Row["HighIntensityExerciseItems"];
                    GlobalData.ExerciseSuggestion.DataNeeded = (string)Row["DataNeeded"];
                }
            }
            return true;
        }
        private static bool GetExerciseSuggestion(string RecordSEQ, classSQLServerDBInterface anotherSQLServerDBInterface)
        {
            //恢复默认值
            GlobalData.ExerciseSuggestion.Clear();

            string sql = "select * from CDSS_ExerciseSuggestion where RecordSEQ=" + RecordSEQ;
            DataTable TableExerciseSuggestion = anotherSQLServerDBInterface.GetDataSet(sql);

            if (TableExerciseSuggestion.Rows.Count > 0)
            {
                foreach (DataRow Row in TableExerciseSuggestion.Rows)
                {

                    GlobalData.ExerciseSuggestion.ExerciseTarget = (string)Row["ExerciseTarget"];
                    GlobalData.ExerciseSuggestion.EnergyCost = (string)Row["EnergyCost"];
                    GlobalData.ExerciseSuggestion.NoIntensityExercise = (string)Row["NoIntensityExercise"];
                    GlobalData.ExerciseSuggestion.NoIntensityExerciseItems = (string)Row["NoIntensityExerciseItems"];
                    GlobalData.ExerciseSuggestion.LowIntensityExercise = (string)Row["LowIntensityExercise"];
                    GlobalData.ExerciseSuggestion.LowIntensityExerciseItems = (string)Row["LowIntensityExerciseItems"];
                    GlobalData.ExerciseSuggestion.MiddleIntensityExercise = (string)Row["MiddleIntensityExercise"];
                    GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems = (string)Row["MiddleIntensityExerciseItems"];
                    GlobalData.ExerciseSuggestion.HighIntensityExercise = (string)Row["HighIntensityExercise"];
                    GlobalData.ExerciseSuggestion.HighIntensityExerciseItems = (string)Row["HighIntensityExerciseItems"];
                    GlobalData.ExerciseSuggestion.DataNeeded = (string)Row["DataNeeded"];
                }
            }
            return true;
        }
        /// <summary>
        /// 保存运动建议信息
        /// </summary>
        /// <returns></returns>
        private static bool SaveExerciseSuggestion()
        {
            string sqlExerciseSuggestion = "insert into CDSS_ExerciseSuggestion(RecordSEQ,ExerciseTarget,EnergyCost,NoIntensityExercise,NoIntensityExerciseItems,LowIntensityExercise,"
                             + "LowIntensityExerciseItems,MiddleIntensityExercise,MiddleIntensityExerciseItems,HighIntensityExercise,HighIntensityExerciseItems,DataNeeded) "
                             + " values(@RecordSEQ,@ExerciseTarget,@EnergyCost,@NoIntensityExercise,@NoIntensityExerciseItems,@LowIntensityExercise,@LowIntensityExerciseItems,"
                             + "@MiddleIntensityExercise,@MiddleIntensityExerciseItems,@HighIntensityExercise,@HighIntensityExerciseItems,@DataNeeded) ;";

            SQLiteParameter[] para = new SQLiteParameter[]
			{
                new SQLiteParameter("@RecordSEQ",GlobalData.RecordInfo.RecordSeq),
                new SQLiteParameter("@ExerciseTarget",GlobalData.ExerciseSuggestion.ExerciseTarget),
                new SQLiteParameter("@EnergyCost",GlobalData.ExerciseSuggestion.EnergyCost),
                new SQLiteParameter("@NoIntensityExercise",GlobalData.ExerciseSuggestion.NoIntensityExercise),
                new SQLiteParameter("@NoIntensityExerciseItems",GlobalData.ExerciseSuggestion.NoIntensityExerciseItems),
                new SQLiteParameter("@LowIntensityExercise",GlobalData.ExerciseSuggestion.LowIntensityExercise),
                new SQLiteParameter("@LowIntensityExerciseItems",GlobalData.ExerciseSuggestion.LowIntensityExerciseItems),
                new SQLiteParameter("@MiddleIntensityExercise",GlobalData.ExerciseSuggestion.MiddleIntensityExercise),
                new SQLiteParameter("@MiddleIntensityExerciseItems",GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems),
                new SQLiteParameter("@HighIntensityExercise",GlobalData.ExerciseSuggestion.HighIntensityExercise),
                new SQLiteParameter("@HighIntensityExerciseItems",GlobalData.ExerciseSuggestion.HighIntensityExerciseItems),
                new SQLiteParameter("@DataNeeded",GlobalData.ExerciseSuggestion.DataNeeded),
			};

            try
            {
                int result = SQLServerDBInterface.ExecuteCommand(sqlExerciseSuggestion, para);
                if (result > 0)
                    return true;
                else
                {
                    LastErrorInfo = "保存运动建议信息失败！";
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                LastErrorInfo = e.Message;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 得到数据库服务器时间
        /// </summary>
        /// <returns></returns>
        public static string GetServerCurrentTime()
        {
            string strSql = "SELECT   datetime('now')";
            string ServerCurrentTime = string.Empty;
            try
            {
                ServerCurrentTime = SQLServerDBInterface.GetStringScalar(strSql);
                return ServerCurrentTime;
            }
            catch (SQLiteException e)
            {
                return ServerCurrentTime;
            }
        }
        /// <summary>
        /// 保存推理日志到数据库中
        /// </summary>
        /// <param name="strRARPath"></param>
        /// <returns></returns>
        public static int SaveReasonedLog(CDSSOperationLog CDSSOperationLog)
        {
            int result = 0;

            FileInfo fi = new FileInfo(CDSSOperationLog.OperationDescription);//CDSSOperationLog.OperationDescription记录着rar文件的路径
            FileStream fs = fi.OpenRead();

            try
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));

                string sql = "insert into CDSS_OperationLog(OperationUserID,OperationStep,OperationTime,"
                            + "OperationReasonedLog,OperationName) values('" + CDSSOperationLog.OperationUserID
                            + "'," + CDSSOperationLog.OperationStep + ",'" + CDSSOperationLog.OperationTime + "',"
                            + "@file,'" + CDSSOperationLog.OperationName + "')";
                //SQLiteParameter spFile = new SQLiteParameter("@file", SqlDbType.Image);
                SQLiteParameter spFile = new SQLiteParameter("@file", DbType.Binary,bytes.Length, ParameterDirection.Input,false,0,0,null,DataRowVersion.Current,bytes);
               
                spFile.Value = bytes;    

                result = SQLServerDBInterface.ExecuteCommand(sql, spFile);
            }
            finally
            {
                fs.Close();
            }

            /************************************************
             * 保存完推理日志后再删除压缩文件
             *************************************************/
            if (File.Exists(CDSSOperationLog.OperationDescription))
            {
                File.Delete(CDSSOperationLog.OperationDescription);
            }
            return result;

        }
        ///// <summary>
        ///// 获取推理日志
        ///// </summary>
        ///// <returns></returns>
        //public static bool GetReasonedLog()
        //{
        //    string sql = "SELECT OperationDescription from CDSS_OperationLog where OperationLogSEQ=1";

        //    try
        //    {
        //        SqlDataReader dr = SQLServerDBInterface.GetReader(sql);
        //        byte[] File = null;
        //        if (dr.Read())
        //        {
        //            File = (byte[])dr[0];
        //        }
        //        FileStream fs = new FileStream("C:\\IELog.rar", FileMode.CreateNew);
        //        BinaryWriter bw = new BinaryWriter(fs);
        //        bw.Write(File, 0, File.Length);
        //        bw.Close();
        //        fs.Close();

        //        return true ;
        //    }
        //    catch (SqlException e)
        //    {
        //        return false ;
        //    }
        //}

        ///// <summary>
        ///// 一次性保存所有操作日志
        ///// </summary>
        ///// <returns></returns>
        //public static bool SaveAllOperationLog()
        //{
        //    if (GlobalData.OperationLog.Count < 1)
        //        return false;
        //    foreach (CDSSOperationLog CDSSOperationLog in GlobalData.OperationLog)
        //    {
        //        int result = 0;
        //        if (CDSSOperationLog.OperationName == "推理日志")
        //        {
        //            result = DBAccess.SaveReasonedLog(CDSSOperationLog);
        //        }
        //        else
        //        {
        //            string sql = "insert into CDSS_OperationLog(OperationUserID,OperationStep,OperationTime,"
        //                + "OperationDescription,OperationName) values(@OperationUserID,@OperationStep,"
        //                +"@OperationTime,@OperationDescription,@OperationName)";

        //            SqlParameter[] para = new SqlParameter[]
        //            {
        //                 new SqlParameter("@OperationUserID",CDSSOperationLog.OperationUserID),
        //                 new SqlParameter("@OperationStep",CDSSOperationLog.OperationStep),
        //                 new SqlParameter("@OperationTime",CDSSOperationLog.OperationTime),
        //                 new SqlParameter("@OperationDescription",CDSSOperationLog.OperationDescription),
        //                 new SqlParameter("@OperationName",CDSSOperationLog.OperationName)
        //            };
        //            result = SQLServerDBInterface.ExecuteCommand(sql, para);
        //        }
        //        if (result > 0)
        //            continue;
        //        else
        //            return false;
        //    }
        //    return true;
        //}


        /// <summary>
        /// 保存单个操作步骤
        /// </summary>
        /// <param name="CDSSOperationLog"></param>
        /// <returns></returns>
        public static bool SaveAllOperationLog(CDSSOperationLog CDSSOperationLog)
        {
            int result = 0;
            if (CDSSOperationLog.OperationName == "推理日志")
            {
                result = DBAccess.SaveReasonedLog(CDSSOperationLog);
            }
            else
            {
                string sql = "insert into CDSS_OperationLog(OperationUserID,OperationStep,OperationTime,"
                    + "OperationDescription,OperationName) values(@OperationUserID,@OperationStep,"
                    + "@OperationTime,@OperationDescription,@OperationName)";

                SQLiteParameter[] para = new SQLiteParameter[]
			        {
                         new SQLiteParameter("@OperationUserID",CDSSOperationLog.OperationUserID),
                         new SQLiteParameter("@OperationStep",CDSSOperationLog.OperationStep),
                         new SQLiteParameter("@OperationTime",CDSSOperationLog.OperationTime),
                         new SQLiteParameter("@OperationDescription",CDSSOperationLog.OperationDescription),
                         new SQLiteParameter("@OperationName",CDSSOperationLog.OperationName)
 			        };
                result = SQLServerDBInterface.ExecuteCommand(sql, para);
            }
            if (result > 0)
                return true;
            else
                return false;
        }
        public static string GetValueQuerySQL(Period singlePeriod, string queryCondition, string[] valuePara)
        {
            DateTime dateStart = singlePeriod.StartTime;
            DateTime dateEnd = singlePeriod.EndTime;
            string sql = "SELECT ";
            string dateBegin = dateStart.Year + "-" + dateStart.Month + "-" + dateStart.Day;
            string dateFinish = dateEnd.Year + "-" + dateEnd.Month + "-" + dateEnd.Day;
            for (int i = 0; i < valuePara.GetLength(0);i++ )
            {
                sql += valuePara[i];
                if (i!=(valuePara.GetLength(0)-1))
                {
                    sql += ",";
                }
            }
            sql += " FROM CDSS_PhysicalInfo,CDSS_LabExamInfo WHERE CDSS_PhysicalInfo.RecordSEQ=CDSS_LabExamInfo.RecordSEQ";
            sql += " AND CDSS_LabExamInfo.RecordSEQ in ";
            sql += "(SELECT CDSS_RecordHistory.RecordSEQ FROM CDSS_RecordHistory WHERE (PatVisitDateTime>='" + dateBegin + "' AND PatVisitDateTime<='";
            sql +=  dateFinish+"'))";
            sql += " AND CDSS_PhysicalInfo.RecordSEQ IN ("+queryCondition + ")";
            
            return sql;
        }
        public static string GetDignosedQuerySQL(DateTime startTime, DateTime endTime, string queryCondition, string dignosedPara)
         {
             string dateBegin = startTime.Year + "-" + startTime.Month + "-" + startTime.Day;
             string dateFinish = endTime.Year + "-" + endTime.Month + "-" + endTime.Day;
             string sql = "";
            if (dignosedPara=="代谢综合征")
            {
                sql = "select count(*) from CDSS_DiagnosedResult where Name='代谢综合征' and Result LIKE '%有%'";
                    sql += " and CDSS_DiagnosedResult.RecordSEQ in (" + queryCondition + ")";
                    sql += " and CDSS_DiagnosedResult.RecordSEQ in ";
                    sql += "(SELECT CDSS_RecordHistory.RecordSEQ FROM CDSS_RecordHistory WHERE (PatVisitDateTime>='" + dateBegin + "' AND PatVisitDateTime<='";
                    sql += dateFinish + "'))";
                    sql += "AND CDSS_DiagnosedResult.RecordSEQ IN (" + queryCondition + ")";    
            } 
            else
            {
                sql = "select count(*) from CDSS_DiagnosedResult where Result LIKE '%" + dignosedPara;
                    sql += "%' and CDSS_DiagnosedResult.RecordSEQ in (" + queryCondition + ")";
                    sql += " and CDSS_DiagnosedResult.RecordSEQ in ";
                    sql += "(SELECT CDSS_RecordHistory.RecordSEQ FROM CDSS_RecordHistory WHERE (PatVisitDateTime>='" + dateBegin + "' AND PatVisitDateTime<='";
                    sql += dateFinish + "'))";
                    sql += "AND CDSS_DiagnosedResult.RecordSEQ IN (" + queryCondition + ")"; 
            }
            return sql;
         }
        public static List<DataTable> GetValueParaDataSet(string[] childQueryCondition, List<Period> IntervalPoints, string[] valueParas)
        {
            List<DataTable> ValueParaCollection = new List<DataTable>();
            //各时间段的循环，外循环
            for (int i = 0; i < IntervalPoints.Count;i++ )
            {
                //统计组和对照组的循环，内循环
                for (int j = 0; j < 2;j++ )
                {
                    string sql=GetValueQuerySQL(IntervalPoints[i],childQueryCondition[j],valueParas);
                    DataTable table= SQLServerDBInterface.GetDataSet(sql);
                    ValueParaCollection.Add(table);                                
                }
            }
            return ValueParaCollection;
        }
        public static int GetDignosedCount(DateTime startTime,DateTime endTime,string childQueryCondition,string para)
        {
            string sql = GetDignosedQuerySQL(startTime, endTime, childQueryCondition, para);
            int c = SQLServerDBInterface.GetScalar(sql);
            return c;
        }
        
        public static List<int> GetPeriodTotalCountDataTable(List<Period> IntervalPoints)
        {
            List<int> totalPatients = new List<int>();
            for (int i = 0; i < IntervalPoints.Count; i ++)
            {
                string dateBegin = IntervalPoints[i].StartTime.Year + "-" + IntervalPoints[i].StartTime.Month + "-" + IntervalPoints[i].StartTime.Day;
                string dateFinish = IntervalPoints[i].EndTime.Year + "-" + IntervalPoints[i].EndTime.Month + "-" + IntervalPoints[i].EndTime.Day;
                string sql = "select count(*) from CDSS_RecordHistory WHERE (PatVisitDateTime>='" + dateBegin + "' AND PatVisitDateTime<='";
                sql += dateFinish + "')";
                int c = SQLServerDBInterface.GetScalar(sql);
                totalPatients.Add(c);
            }
            return totalPatients;
        }
        public static string InitSQLStr(System.Windows.Forms.FlowLayoutPanel flowPanel, string[] displayTables)
        {
            int SpeType = 0;
            string SelectSQLStr = "select CDSS_RecordHistory.RecordSEQ from CDSS_RecordHistory ";
            string JoinSQLStr = "";
            string WhereSQLStr = " ";
            DESClass DESClass = new DESClass();
            for (int i = 0; i < flowPanel.Controls.Count; i++)
            {
                QueryBuilder qb = (QueryBuilder)flowPanel.Controls[i];
                QueryBuilder.SQLStr msg = qb.ShowSQLStr();
                if (msg.TableName != "CDSS_RecordHistory" && qb.ValueType != QueryBuilder.ValueTypes.Other)
                {
                    if (JoinSQLStr.IndexOf(msg.TableName) < 0)
                        JoinSQLStr += msg.SqlDemo;
                }
                if (msg.Condition == "PatName")
                    msg.CheckValue = "'" + DESClass.DESEncrypt(msg.CheckValue.Replace("'", "")) + "'";
                if (msg.Condition == "PatBirthday")
                    msg.CheckValue = "'" + DESClass.DESEncrypt(msg.CheckValue.Replace("'", "") + " 0:00:00") + "'";
                switch (qb.ValueType)
                {
                    case QueryBuilder.ValueTypes.Text:
                        WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
                        break;
                    case QueryBuilder.ValueTypes.Num:
                        try
                        {
                            Convert.ToDouble(msg.CheckValue);
                        }
                        catch
                        {
                            throw new Exception("条件错误：“" + msg.ConditionName + "”值为数字，请重新输入");
                        }
                        if (msg.Sign != " = ")
                        {
                            WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + "\'" + msg.CheckValue + "')" + msg.LogicSign;
                        } // string fgj;
                        else
                        {
                            WhereSQLStr += msg.TableName + "." + msg.Condition + "-'" + msg.CheckValue + "' between -0.00001 and 0.00001 )" + msg.LogicSign;
                        }
                        break;
                    case QueryBuilder.ValueTypes.Date:   //revised by fgj
                        DateTime Value;
                        string b;
                        try
                        {
                           Value= Convert.ToDateTime(msg.CheckValue.Replace("'", ""));
                        }
                        catch
                        {
                            throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
                        }
                       
                        b = Value.ToString("s");// revised by fgj 
                        WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + "'" + b + "')" + msg.LogicSign;
                        break;
                    case QueryBuilder.ValueTypes.Bool:
                        if (msg.CheckValue.Replace("'", "") == "是")
                        {
                            msg.CheckValue = "'true'";
                        }
                        else if (msg.CheckValue.Replace("'", "") == "否")
                        {
                            msg.CheckValue = "'false'";
                        }
                        else if (msg.CheckValue.Replace("'", "") == "1")
                        {
                            msg.CheckValue = "'true'";
                        }
                        else if (msg.CheckValue.Replace("'", "") == "0")
                        {
                            msg.CheckValue = "'false'";
                        }

                        try
                        {
                            Convert.ToBoolean(msg.CheckValue.Replace("'", ""));
                        }
                        catch
                        {
                            throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
                        }
                        WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
                        break;
                    case QueryBuilder.ValueTypes.Other:
                        try
                        {
                            Convert.ToDateTime(msg.CheckValue.Replace("'", ""));
                        }
                        catch
                        {
                            throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
                        }
                        SelectSQLStr = "SELECT CDSS_RecordHistory.RecordSEQ from CDSS_RecordHistory left join CDSS_PatBasicInfo on CDSS_RecordHistory.PatSEQ = CDSS_PatBasicInfo.PatSEQ ";
                        WhereSQLStr += " (SELECT CDSS_PatBasicInfo_1.PatSEQ FROM dbo.CDSS_PatBasicInfo AS CDSS_PatBasicInfo_1 INNER JOIN "
                        + "dbo.CDSS_RecordHistory ON CDSS_PatBasicInfo_1.PatSEQ = dbo.CDSS_RecordHistory.PatSEQ"
                        + " WHERE (dbo.CDSS_RecordHistory.PatVisitDateTime = CONVERT(DATETIME, " + msg.CheckValue + ", 102))))" + (msg.LogicSign == " " ? "" : "INTERSECT");
                        SpeType++;
                        break;
                    default:
                        break;
                }
                WhereSQLStr = "(CDSS_RecordHistory.HistoryRecordStatus=0 and" + WhereSQLStr; //revised by fgj
            }
            if (SpeType > 0)
            {
                if (SpeType == flowPanel.Controls.Count)
                    WhereSQLStr = " CDSS_PatBasicInfo.PatSEQ in" + WhereSQLStr;
                else
                    throw new Exception("若条件中有一项为“就诊时间筛选”，则查询条件必须全部为此类型");
            }
            //change by zx 修正能查找到其他医生的病人资料
            string sqlStr = "select RecordSEQ from CDSS_RecordInfo where RecordSEQ in(" + SelectSQLStr + JoinSQLStr + "where" + WhereSQLStr + ")"; //and UserID='" + GlobalData.UserInfo.UserID + "'";

            string showSelectSQLStr = " from CDSS_RecordHistory ";
            string showJoinSQLStr = "";
            string showWhereSQLStr = "where CDSS_RecordHistory.RecordSEQ in (" + sqlStr + ")";
            string showFileds = "";


            //string[] displayTables = InitDisplayFieldsByTree().Split('|');
            for (int i = 0; i < displayTables.Length; i++)
            {
                if (displayTables[i] != string.Empty)
                {
                    string[] fields = displayTables[i].Split(',');
                    for (int j = 1; j < fields.Length; j++)
                    {
                        showFileds += fields[0] + "." + fields[j] + ",";
                    }
                    if (showFileds.IndexOf("RecordSEQ") < 0)
                    {
                        showFileds += "CDSS_RecordHistory.RecordSEQ,CDSS_RecordHistory.HistoryRecordStatus,";
                    }
                    if (fields[0] == "CDSS_PatBasicInfo")
                    {
                        if (showJoinSQLStr.IndexOf("CDSS_PatBasicInfo") < 0)
                        {
                            showJoinSQLStr += "left join " + fields[0] + " on CDSS_RecordHistory.PatSEQ = " + fields[0] + ".PatSEQ ";
                        }
                    }
                    else if (fields[0] != "CDSS_RecordHistory")
                    {
                        if (showJoinSQLStr.IndexOf(fields[0]) < 0)
                            showJoinSQLStr += "left join " + fields[0] + " on CDSS_RecordHistory.RecordSEQ = " + fields[0] + ".RecordSEQ ";
                    }
                }
            }
            return "select distinct " + showFileds.Substring(0, showFileds.Length - 1) + showSelectSQLStr + showJoinSQLStr + showWhereSQLStr;

        }
        public static string InitSQLStr(System.Windows.Forms.FlowLayoutPanel flowLayoutPanel)
        {
            
            int SpeType = 0;
            //此方法生成的SQL语句是选取在CDSS_RecordHistory中的RecordSEQ，
            string SelectSQLStr = "select CDSS_RecordHistory.RecordSEQ from CDSS_RecordHistory";
            string JoinSQLStr = "";
            string WhereSQLStr = "";
            DESClass DESClass = new DESClass();
            if(flowLayoutPanel.Controls.Count!=0)
            {
                QueryBuilder qb = (QueryBuilder)flowLayoutPanel.Controls[0];
                QueryBuilder.SQLStr msg = qb.ShowSQLStr();
                if (msg.ConditionName!="")
                {
                    WhereSQLStr+="where ";
                    for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
                    {
                        qb = (QueryBuilder)flowLayoutPanel.Controls[i];
                        msg = qb.ShowSQLStr();

                        if (msg.TableName != "CDSS_RecordHistory" && qb.ValueType != QueryBuilder.ValueTypes.Other)
                        {
                            if (JoinSQLStr.IndexOf(msg.TableName) < 0)
                            {
                                JoinSQLStr += " ";
                                JoinSQLStr += msg.SqlDemo;
                            }
                        }
                        if (msg.Condition == "PatName")
                            msg.CheckValue = "'" + DESClass.DESEncrypt(msg.CheckValue.Replace("'", "")) + "'";
                        if (msg.Condition == "PatBirthday")
                            msg.CheckValue = "'" + DESClass.DESEncrypt(msg.CheckValue.Replace("'", "") + " 0:00:00") + "'";
                        switch (qb.ValueType)
                        {
                            case QueryBuilder.ValueTypes.Text:
                                WhereSQLStr += "("+msg.TableName + "." + msg.Condition + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
                                break;
                            case QueryBuilder.ValueTypes.Num:
                                try
                                {
                                    Convert.ToDouble(msg.CheckValue);
                                }
                                catch
                                {
                                    throw new Exception("条件错误：“" + msg.ConditionName + "”值为数字，请重新输入");
                                }
                                if (msg.Sign != " = ")
                                {
                                    WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + "\'" + msg.CheckValue + "')" + msg.LogicSign;
                                } // string fgj;
                                else
                                {
                                    WhereSQLStr += msg.TableName + "." + msg.Condition + "-'" + msg.CheckValue + "' between -0.00001 and 0.00001 )" + msg.LogicSign;
                                }
                                break;
                            case QueryBuilder.ValueTypes.Date:  // revised by fgj
                                DateTime Value;
                                string b;
                                try
                                {
                                   Value= Convert.ToDateTime(msg.CheckValue.Replace("'", ""));
                                }
                                catch
                                {
                                    throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
                                }
                               
                                b = Value.ToString("s");
                  
                                WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + "'" + b + "')" + msg.LogicSign;
                                break;
                            case QueryBuilder.ValueTypes.Bool:
                                if (msg.CheckValue.Replace("'", "") == "是")
                                {
                                    msg.CheckValue = "'true'";
                                }
                                else if (msg.CheckValue.Replace("'", "") == "否")
                                {
                                    msg.CheckValue = "'false'";
                                }
                                else if (msg.CheckValue.Replace("'", "") == "1")
                                {
                                    msg.CheckValue = "'true'";
                                }
                                else if (msg.CheckValue.Replace("'", "") == "0")
                                {
                                    msg.CheckValue = "'false'";
                                }

                                try
                                {
                                    Convert.ToBoolean(msg.CheckValue.Replace("'", ""));
                                }
                                catch
                                {
                                    throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
                                }
                                WhereSQLStr +="(" +msg.TableName + "." + msg.Condition + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
                                break;
                            case QueryBuilder.ValueTypes.Other:
                                try
                                {
                                    Convert.ToDateTime(msg.CheckValue.Replace("'", ""));
                                }
                                catch
                                {
                                    throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
                                }

                                WhereSQLStr += " (SELECT CDSS_PatBasicInfo_1.PatSEQ FROM CDSS_PatBasicInfo AS CDSS_PatBasicInfo_1 INNER JOIN "
                                + "CDSS_RecordHistory ON CDSS_PatBasicInfo_1.PatSEQ = CDSS_RecordHistory.PatSEQ"
                                + " WHERE (CDSS_RecordHistory.PatVisitDateTime =  " + msg.CheckValue + ")))" + (msg.LogicSign == " " ? "" : "INTERSECT");
                                SpeType++;
                                break;
                            default:
                                break;
                    }
                }
            }
        }
            
            return SelectSQLStr + JoinSQLStr + WhereSQLStr;
        }
        public static DataTable MatchingRecord(DataRow dr, CDSSCtrlLib.CheckTreeView treeDisplayCloumns)
        {
            DESClass DESClass = new DESClass();
            string SqlStr = "";
            //string SelectSQLStr = "select CDSS_RecordHistory.RecordSEQ,CDSS_RecordHistory.PatSEQ from CDSS_RecordHistory ";
            string SelectSQLStr = "select CDSS_RecordHistory.RecordSEQ,CDSS_PatBasicInfo.PatName,CDSS_PatBasicInfo.PatBirthday,CDSS_PatBasicInfo.PatSex from CDSS_PatBasicInfo left join CDSS_RecordHistory on CDSS_PatBasicInfo.PatSEQ=CDSS_RecordHistory.PatSEQ";
			string JoinSQLStr = "";
            string WhereSQLStr = " ";
            foreach (CheckTreeNode root in treeDisplayCloumns.Nodes)
            {
                if (root.State != CheckTreeNode.CheckBoxState.Unchecked)
                {
                    foreach (CheckTreeNode child in root.Nodes)
                    {
                        if (child.State == CheckTreeNode.CheckBoxState.Checked)
                        {
                            string value = dr[child.Name].ToString();
                            if (child.Name == "PatName" || child.Name == "PatBirthday")
                                value = DESClass.DESEncrypt(value);
                            string valueType = ((string[])child.Tag)[1];
                            string Standard = ((string[])child.Tag)[3];
                            switch (valueType)
                            {
                                case "文本":
                                    WhereSQLStr += root.Name + "." + child.Name.ToString() + " = '" + value + "' and ";
                                    break;
                                case "数字":
                                    WhereSQLStr += root.Name + "." + child.Name.ToString() + "- '" + value + "'between -0.00001 and 0.00001 and ";
                                    break;
                                case "日期":
                                    string date;
                                    date = value.Replace(" ", "T0");
                                    date = date.Replace("/", "-");
                                    //WhereSQLStr += root.Name + "." + child.Name.ToString() + " = '" + value + "' and ";

                                    WhereSQLStr += root.Name + "." + child.Name.ToString() + " = '" + date + "' and ";  //revised by fgj
                                    //  WhereSQLStr += "julianday (datetime(\'" + date + "\')）-julianday（" + root.Name + "." + child.Name.ToString() + ") > -" + Standard.Split('-')[1] + " and  Julianday (datetime(\'" + value.Remove(11) + "\')）-julianday（" + root.Name + "." + child.Name.ToString() + ")<" + Standard.Split('-')[1] + " and ";  //revised by fgj
                                    //  WhereSQLStr += "（julianday ("+ value.Remove(11) + "）-julianday（" + root.Name + "." + child.Name.ToString() + ")） between -" + Standard.Split('-')[1] + " and " + Standard.Split('-')[1] + " and ";  //revised by fgj
                                    // WhereSQLStr += "datediff (" + Standard.Split('-')[0] + ",'" + value + "'," + root.Name + "." + child.Name.ToString() + ") between -" + Standard.Split('-')[1] + " and " + Standard.Split('-')[1] + " and ";
                                    break;
                                case "布尔":
                                    WhereSQLStr += root.Name + "." + child.Name.ToString() + " = '" + value.ToLower() + "' and ";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    //if (root.Name.ToString() == "CDSS_PatBasicInfo")
                    //{
                    //    JoinSQLStr += "left join CDSS_PatBasicInfo on CDSS_RecordHistory.PatSEQ = CDSS_PatBasicInfo.PatSEQ ";
                    //}
                    //else if (root.Name.ToString() != "CDSS_RecordHistory")
                    //{
                    //    JoinSQLStr += "left join " + root.Name.ToString() + " on CDSS_RecordHistory.RecordSEQ = " + root.Name.ToString() + ".RecordSEQ ";
                    //}
					if (root.Name.ToString() != "CDSS_RecordHistory" && root.Name.ToString() != "CDSS_PatBasicInfo")
                    {
                        JoinSQLStr += " left join " + root.Name.ToString() + " on CDSS_RecordHistory.RecordSEQ = " + root.Name.ToString() + ".RecordSEQ ";
                    }
                }
            }
            if (WhereSQLStr.Length <= 5)
            {
                throw new Exception("请先设置相同记录判断依据（请选择红色字体选项）");
            }
            SqlStr = SelectSQLStr + JoinSQLStr + " where " + WhereSQLStr.Substring(0, WhereSQLStr.Length - 5);
            DataTable RecoredDT = SQLServerDBInterface.GetDataSet(SqlStr);
            
            return RecoredDT;

        }
        public static DataTable Highlevelqueryfunction(string sql)
        {          
            
             DataTable dt;
             dt = SQLServerDBInterface.GetDataSet(sql);
             return dt;
        }
        //added by yanhui 20120320 for merging two database files 
        public static DataTable GetNameAndBirthdayRecordSeqDataFromDB()
        {
            //SQLServerDBInterface.OpenDB(dbDirectory);
            string sql = "select CDSS_PatBasicInfo.PatName,CDSS_PatBasicInfo.PatBirthday,CDSS_RecordHistory.RecordSEQ from CDSS_RecordHistory left join CDSS_PatBasicInfo on CDSS_RecordHistory.PatSEQ=CDSS_PatBasicInfo.PatSEQ";
            DataTable dtNameAndBirthday = SQLServerDBInterface.GetDataSet(sql);
            return dtNameAndBirthday;
        }
        public static void SetDirectory(string DBFileDirectory)
        {
            SQLServerDBInterface.Directory=DBFileDirectory;
        }
        public static void ClearDirectory()
        {
            SQLServerDBInterface.Directory = "";
        }
        public static int GetNumOfSameData(DataRow dr)
        {
            string sql = "select count(*) from CDSS_PatBasicInfo where PatName='"+dr["PatName"]+"' and PatBirthday='"+dr["PatBirthday"]+"'";            
            int num = SQLServerDBInterface.GetScalar(sql);
            return num;
        }
        public static void SetGlobalRecordSEQ(int record)
        {
            GlobalData.RecordInfo.RecordSeq = record;
        }
        public static void CloseDB()
        {
            SQLServerDBInterface.CloseDB();
        }
        public static void SetGlobalPatSEQToNew()
        {
            GlobalData.PatBasicInfo.PatSEQ="*";
        }
        public static void SetGlobalRecordSEQToNew()
        {
            GlobalData.RecordInfo.RecordSeq = 0;
        }
        public static string GetStringScalar(string sql)
        {
            return SQLServerDBInterface.GetStringScalar(sql);
        }
        public static DataTable GetDataSet(string sql)
        {
            return SQLServerDBInterface.GetDataSet(sql);
        }
    }

}

