using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;

namespace DBSetup
{
    [RunInstaller(true)]
    public partial class DBInstaller : Installer
    {
        SqlConnection masterConn = new SqlConnection();

        public DBInstaller()
        {
            InitializeComponent();
        }

        private string GetSql(string name)
        {
            Assembly Asm = Assembly.GetExecutingAssembly();
            Stream strm = Asm.GetManifestResourceStream(Asm.GetName().Name + "." + name);
            StreamReader reader = new StreamReader(strm, System.Text.Encoding.GetEncoding("GB2312"));
            return reader.ReadToEnd();
        }

        private void ExecuteSql(string databaseName, string sql)
        {
            SqlCommand command = new SqlCommand(sql, this.masterConn);
            this.masterConn.ConnectionString = Properties.Settings.Default.masterConnectionString;
            command.Connection.Open();
            command.Connection.ChangeDatabase(databaseName);
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                command.Connection.Close();
            }     
        }

        private void ExecuteSp(string databaseName, string sql)
        {
            SqlCommand command = new SqlCommand(sql, this.masterConn);
            this.masterConn.ConnectionString = Properties.Settings.Default.masterConnectionString;
            command.Connection.Open();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = sql;
            command.Connection.ChangeDatabase(databaseName);
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                command.Connection.Close();
            }     
        }

        protected void AddDataTable(string dbName)
        {
            try
            {
                ExecuteSql("master", "CREATE DATABASE " + dbName);
                ExecuteSql(dbName, GetSql("InitDB.txt"));
                ExecuteSql(dbName, GetSql("InitView.txt"));
                ExecuteSql(dbName, GetSql("ConfigAdHoc.txt"));
                ExecuteSp(dbName, "CDSS_spChangeCfgOptions");
            }
            catch (Exception ex)
            {
                Console.Write("In exception handler:" + ex.Message);
            }     
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            AddDataTable("NGCDSS");
        }
    }
}