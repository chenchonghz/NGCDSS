using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CDSSUserPowerManager
{
    public static class DBManager
    {
        public static string LoginedUserID = "";

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
                DataTable Table = DBAccess.GetDataSet(strSql);
                return Table;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="value">长度为8的字符串数组</param>
        /// <returns>布尔型</returns>
        public static bool AddUser(string[] vaule)
        {
            string strSql = "INSERT INTO [dbo].[CDSS_UserInfo]([UserID],"
                + "[UserPwd],[UserName],[Department],[Title],[Phone],[Company],"
                + "[UserPower],[MailAddress])VALUES('" + vaule[0] + "','" + vaule[1] + "','"
                + vaule[2] + "','" + vaule[3] + "','" + vaule[4] + "','"
                + vaule[5] + "','" + vaule[6] + "'," + vaule[7] + ",'" + vaule[8] + "')";

            try
            {
                int val = DBAccess.ExecuteCommand(strSql);
                if (val > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 查询所有用户的信息
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public static DataTable GetAllUsers()
        {
            string strSql = "SELECT * FROM CDSS_UserInfo";

            try
            {
                DataTable Table = DBAccess.GetDataSet(strSql);
                return Table;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 删除指定用户
        /// </summary>
        /// <param name="UserID">用户帐号</param>
        /// <returns>bool</returns>
        public static bool DeleteUser(string UserID)
        {
            string sql = "delete from CDSS_UserInfo where UserID='" + UserID + "'";
            try
            {
                int val = DBAccess.ExecuteCommand(sql);
                if (val > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool UpdateUser(string[] vaule)
        {
            string sql = "update CDSS_UserInfo set UserName=@UserName,Department=@Department,"
                + "Title=@Title,Phone=@Phone,Company=@Company,UserPower=@UserPower,"
                + "MailAddress=@MailAddress,SyncFlag=@SyncFlag where UserID=@UserID";
            SqlParameter[] parater = new SqlParameter[]
			    {
                    new SqlParameter("@UserID",vaule[0]),
                    new SqlParameter("@UserName",vaule[1]),
                    new SqlParameter("@Department",vaule[2]),
                    new SqlParameter("@Title",vaule[3]),
                    new SqlParameter("@Phone",vaule[4]),
                    new SqlParameter("@Company",vaule[5]),
                    new SqlParameter("@UserPower",vaule[6]),
                    new SqlParameter("@MailAddress",vaule[7]),
                     new SqlParameter("@SyncFlag",vaule[8])
                };

            try
            {
                int result = DBAccess.ExecuteCommand(sql, parater);
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
