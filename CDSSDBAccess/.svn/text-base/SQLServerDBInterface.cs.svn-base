using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SQLite;

namespace CDSSDBAccess
{

    public class classSQLServerDBInterface
    {
        //added by yanhui 20120321
        private string m_directoryOfDB = "";
        public classSQLServerDBInterface(params string[] DBDirectory)
        {
            if (DBDirectory.Length > 0)
            {
                m_directoryOfDB = DBDirectory[0];
                string strNewConnectionString = "Data Source=" + m_directoryOfDB;
                
                connection = new SQLiteConnection(strNewConnectionString);
                connection.Open();
            }
            else
            {
                m_directoryOfDB = System.AppDomain.CurrentDomain.BaseDirectory + "NGCDSS.db";
                string strNewConnectionString = "Data Source=" + m_directoryOfDB;
                connection = new SQLiteConnection(strNewConnectionString);
                connection.Open();
            }
        }
        //数据库连接属性 
        
        public  string Directory
        {
            get 
            {
                return m_directoryOfDB;
            }
            set
            {
                m_directoryOfDB = value;
            }
        }
        private  SQLiteConnection connection;
        public  SQLiteConnection Connection
        {
            get
            {
                if (Directory == "")
                {
                    OpenDB();
                }
                else if (Directory != "")
                {
                    OpenDB(Directory);
                }
                return connection;

            }
        }
        public  bool OpenDB(params string[] DBDirectory)
        {
            if (DBDirectory.Length > 0)
            {
                string strNewConnectionString = "Data Source=" + DBDirectory[0];
                if (connection == null || connection.ConnectionString != strNewConnectionString)
                {                    
                    connection = new SQLiteConnection(strNewConnectionString);
                    connection.Open();                    
                }                
                else if (connection.State.ToString() == "Closed")
                {
                    connection.Open();                    
                }
            }
            else
            {
                string directory = System.AppDomain.CurrentDomain.BaseDirectory;
                string strNewConnectionString = "Data Source=" + directory + "NGCDSS.db";
                if (connection == null||connection.ConnectionString != strNewConnectionString)
                {
                    connection = new SQLiteConnection(strNewConnectionString);
                    connection.Open();
                }                
                else if (connection.State.ToString() == "Closed") 
                {                    
                    connection.Open();                    
                }
            }
            return true;

        }
        public  void CloseDB()
        {
            connection.Close();
        }
        /// <summary>
        /// 执行无参SQL语句
        /// </summary>
        public  int ExecuteCommand(string safeSql)
        {
            SQLiteCommand cmd = new SQLiteCommand(safeSql, Connection);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// 执行带参SQL语句
        /// </summary>
        public  int ExecuteCommand(string sql, params SQLiteParameter[] values)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            // cmd.Parameters.AddRange(values);
            if (values.Length > 0)
            {
                foreach (SQLiteParameter parm in values)
                    cmd.Parameters.Add(parm);
            }

            return cmd.ExecuteNonQuery();

        }
        /// <summary>
        /// 执行无参SQL语句，并返回执行记录数
        /// </summary>
        public  int GetScalar(string safeSql)
        {
            SQLiteCommand cmd = new SQLiteCommand(safeSql, Connection);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }
        /// <summary>
        /// 执行有参SQL语句，并返回执行记录数
        /// </summary>
        public  int GetScalar(string sql, params SQLiteParameter[] values)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            //cmd.Parameters.AddRange(values);
            if (values.Length > 0)
            {
                foreach (SQLiteParameter parm in values)
                    cmd.Parameters.Add(parm);
            }

            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }
        /// <summary>
        /// 执行无参SQL语句，并返回执行结果
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public  string GetStringScalar(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            try
            {
                string result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch (SQLiteException e)
            {
                string newsql;

                

                    newsql = sql.Replace("TOP 1", "");
                    string a = " LIMIT 1";
                    newsql = newsql + a;


                    SQLiteCommand newcmd = new SQLiteCommand(newsql, Connection);
                    string result = newcmd.ExecuteScalar().ToString();
                    return result;
                
               
            }
        }
        /// <summary>
        /// 执行无参SQL语句，并返回SQLiteDataReader
        /// </summary>
        public  SQLiteDataReader GetReader(string safeSql)
        {
            SQLiteCommand cmd = new SQLiteCommand(safeSql, Connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        /// <summary>
        /// 执行有参SQL语句，并返回SQLiteDataReader
        /// </summary>
        public  SQLiteDataReader GetReader(string sql, params SQLiteParameter[] values)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            //cmd.Parameters.AddRange(values);
            if (values.Length > 0)
            {
                foreach (SQLiteParameter parm in values)
                    cmd.Parameters.Add(parm);
            }
            SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public  int ExecuteCommand(params SQLiteParameter[] values)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "Pro_UpdateBooksCatagory";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddRange(values);
            if (values.Length > 0)
            {
                foreach (SQLiteParameter parm in values)
                    cmd.Parameters.Add(parm);
            }
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public  int GetScalar(params SQLiteParameter[] values)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "Pro_InsertOrder";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddRange(values);
            if (values.Length > 0)
            {
                foreach (SQLiteParameter parm in values)
                    cmd.Parameters.Add(parm);
            }
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }


        public  DataTable GetDataSet(string safeSql)
        {
            DataSet ds = new DataSet();
            SQLiteCommand cmd = new SQLiteCommand(safeSql, Connection);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }

        public  DataTable GetDataSet(string sql, params SQLiteParameter[] values)
        {
            DataSet ds = new DataSet();
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            // cmd.Parameters.AddRange(values);
            if (values.Length > 0)
            {
                foreach (SQLiteParameter parm in values)
                    cmd.Parameters.Add(parm);
            }
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }



    }

}
