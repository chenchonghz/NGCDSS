using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using CDSSSystemData;

namespace CDSS
{
    public class LogDataTable : DataTable
    {
        public LogDataTable()
            : base()
        {
            DataColumn[] dcols = new DataColumn[] 
            { 
                new DataColumn("SEQ"),
                new DataColumn("FormName"),
                new DataColumn("ControlName"),
                new DataColumn("ControlType"),
                new DataColumn("EventType"),
                new DataColumn("TriggerTime"),
                new DataColumn("LastTime"),            
            };
            this.Columns.AddRange(dcols);
        }

        public void CopyData(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                this.Rows.Add(dr.ItemArray);
            }
        }
    }

    public class UserLogOperate
    {
        private static string logPath = Application.StartupPath + "\\UserLog\\" + GlobalData.UserInfo.UserID;

        public static string LogPath
        {
            get
            {
                if (!File.Exists(logPath))
                    Directory.CreateDirectory(logPath);
                return logPath;
            }
        }

        private static LogDataTable LogData = new LogDataTable();

        /// <summary>
        /// 最后保存时间
        /// </summary>
        private static DateTime lastSaveTime = DateTime.MinValue;

        /// <summary>
        /// 保存当天日志到日志全局变量
        /// 若遇到横跨2天，则存为2份，将已过日期的日志先保存
        /// </summary>
        /// <param name="args"></param>
        public static void SaveOperationLog(object[] args)
        {
            if (lastSaveTime.Equals(DateTime.MinValue))
                lastSaveTime = DateTime.Now;
            else
            {
                if (lastSaveTime.Date.CompareTo(DateTime.Now.Date) < 0)
                {
                    SaveLogData();
                    LogData.Rows.Clear();
                    lastSaveTime = DateTime.Now;
                }
            }
            args[0] = (LogData.Rows.Count + 1);
            LogData.Rows.Add(args);
        }

        /// <summary>
        /// 保存日志
        /// 在程序结束的时候保存
        /// </summary>
        public static void SaveLogData()
        {
            DataSet ds = new DataSet();
            LogData.TableName = "日志：" + lastSaveTime.ToShortDateString();
            if (LogData.DataSet != null)
            {
                LogData.DataSet.Tables.Remove(LogData);
            }
            ds.Tables.Add(LogData);
            try
            {
                ds.WriteXml(LogPath + "\\" + lastSaveTime.Date.ToString("yyyyMMdd") + ".xml");
            }
            catch
            {
                //日志出错备份
                ds.WriteXml(LogPath + "\\" + "ErrorBackUpLog_" + lastSaveTime.Date.ToString("yyyyMMdd") + ".xml");
            }
        }

        /// <summary>
        /// 日志保存以天为单位，一天的操作存为一个日志
        /// 读取当天的操作日志，追加日志
        /// </summary>
        public static void GetTodayLog()
        {
            if (File.Exists(LogPath + "\\" + DateTime.Now.ToShortDateString() + ".xml"))
            {
                DataSet ds = new DataSet();
                LogData.Rows.Clear();
                ds.ReadXml(LogPath + "\\" + DateTime.Now.ToShortDateString() + ".xml");
                //空日志处理
                if (ds.Tables.Count == 0) return;
                LogData.CopyData(ds.Tables[0]);
            }
        }
    }
}
