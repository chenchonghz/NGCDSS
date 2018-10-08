using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSDBAccess;
using CDSSSystemData;

namespace CDSS
{
    public partial class ConditionGrouping : InfoFormBaseClass
    {
        int QueryCount = 1;

        int SpeType = 0;

        DataTable resultTable = new DataTable();

        DataSet conditionSet = new DataSet();

        List<List<string>> conditionGroup = new List<List<string>>();

        string preSelectedIndex = "-1";

        public ConditionGrouping()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 根据QueryCount的值创建相应个数控件
        /// </summary>
        private void CreateQueryBuilder()
        {
            //QueryBuilder qbCtl = new QueryBuilder();
            //qbCtl.Name = "queryBuilder" + QueryCount;
            //qbCtl.Delete += new QueryBuilder.DeleteHandler(qbCtl_Delete);
            //qbCtl.AutoValidate = AutoValidate.EnableAllowFocusChange;
            //qbCtl.Validating += new CancelEventHandler(qbCtl_Validating);
            //flowPanel.Controls.Add(qbCtl);
            //flowPanel.ScrollControlIntoView(qbCtl);
            //LogicControl();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            QueryCount++;
            CreateQueryBuilder();
        }

        private void qbCtl_Delete(object sender, EventArgs e)
        {
            //QueryBuilder qb = (QueryBuilder)sender;
            //flowPanel.Controls.Remove(qb);
            //QueryCount--;
            //for (int i = 1; i <= flowPanel.Controls.Count; i++)
            //{
            //    QueryBuilder ctl = (QueryBuilder)flowPanel.Controls[i - 1];
            //    ctl.Name = "queryBuilder" + i;
            //}
            //LogicControl();
        }

        private void qbCtl_Validating(object sender, CancelEventArgs e)
        {
            //CheckQuery(sender as QueryBuilder);
        }

        /// <summary>
        /// 控制最后一个控件逻辑关系只有“无”，其他的只有“与或”；
        /// </summary>
        private void LogicControl()
        {
            //foreach (QueryBuilder var in flowPanel.Controls)
            //{
            //    if (var.Name == "queryBuilder" + flowPanel.Controls.Count)
            //    {
            //        string old = var.cmbLgc.Text;
            //        var.cmbLgc.Items.Clear();
            //        var.cmbLgc.Items.AddRange(new object[] { "无" });
            //        var.cmbLgc.SelectedIndex = 0;
            //        var.cmbLgc.Text = old;
            //    }
            //    else
            //    {
            //        string old = var.cmbLgc.Text;
            //        var.cmbLgc.Items.Clear();
            //        var.cmbLgc.Items.AddRange(new object[] { "且", "或", "无" });
            //        var.cmbLgc.Text = old;
            //    }
            //}
        }

        private void CalcStatisicResult()
        {
            this.resultTable.Clear();
            this.resultTable.Columns.Clear();

            List<string> countNum = new List<string>();

            string selectStr = this.GetWhereClause();

            List<string> totalWhereClause = this.conditionGroup[0];

            for (int groupIndex = 1; groupIndex < this.conditionGroup.Count; groupIndex++ )
            {
                List<string> currentCondition = this.conditionGroup[groupIndex];
                List<string> whereGroup = new List<string>();

                foreach (string baseWhere in totalWhereClause)
                {
                    foreach (string currentWhere in currentCondition)
                    {
                        string where = "(" + baseWhere + " and " + currentWhere + ")";
                        whereGroup.Add(where);
                    }
                }

                totalWhereClause = whereGroup;
            }

            foreach (string where in totalWhereClause)
            {
                string sqlStr = selectStr + where;
                DataTable table = SQLServerDBInterface.GetDataSet(sqlStr);
                countNum.Add(table.Rows[0].ItemArray[0].ToString());
            }

            for (int colNum = 0; colNum < totalWhereClause.Count; colNum++)
            {
                this.resultTable.Columns.Add(colNum.ToString());
            }

            this.resultTable.Rows.Add(countNum.ToArray());
        }

        private void ConditionGrouping_Load(object sender, EventArgs e)
        {
            CreateQueryBuilder();
        }

        private string GetWhereClause()
        {
            SpeType = 0;
            string SelectSQLStr = "select COUNT(CDSS_RecordHistory.RecordSEQ) from CDSS_RecordHistory ";
            string JoinSQLStr = "";
            string WhereSQLStr = string.Empty;
            List<string> conditions = new List<string>();

            foreach (DataTable table in this.conditionSet.Tables)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow dr = table.Rows[i];
                    QueryBuilder.SQLStr msg = new QueryBuilder.SQLStr();
                    msg.ConditionName = dr["colCnName"].ToString();
                    msg.Condition = dr["colName"].ToString();
                    msg.Sign = dr["sign"].ToString();
                    msg.TableName = dr["tableName"].ToString();
                    msg.LogicSign = dr["logicSign"].ToString();
                    msg.CheckValue = dr["vaule"].ToString();
                    msg.ConditionType = dr["colType"].ToString();
                    msg.SqlDemo = dr["sqlDemo"].ToString();

                    if (msg.TableName != "CDSS_RecordHistory")
                    {
                        if (JoinSQLStr.IndexOf(msg.TableName) < 0)
                        {
                            if (!JoinSQLStr.Contains(msg.SqlDemo))
                            {
                                JoinSQLStr += msg.SqlDemo;
                            }
                        }
                    }

                    switch (msg.ConditionType)
                    {
                        case "文本":
                            WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
                            break;
                        case "数字":
                            try
                            {
                                Convert.ToDouble(msg.CheckValue);
                            }
                            catch
                            {
                                throw new Exception("条件错误：“" + msg.ConditionName + "”值为数字，请重新输入");
                            }
                            WhereSQLStr += "convert(float," + msg.TableName + "." + msg.Condition + ")" + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
                            break;
                        case "日期":
                            try
                            {
                                Convert.ToDateTime(msg.CheckValue.Replace("'", ""));
                            }
                            catch
                            {
                                throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
                            }
                            WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
                            break;
                        case "布尔":
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
                        case "其他":
                            try
                            {
                                Convert.ToDateTime(msg.CheckValue.Replace("'", ""));
                            }
                            catch
                            {
                                throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
                            }

                            WhereSQLStr += " (SELECT CDSS_PatBasicInfo_1.PatSEQ FROM dbo.CDSS_PatBasicInfo AS CDSS_PatBasicInfo_1 INNER JOIN "
                            + "dbo.CDSS_RecordHistory ON CDSS_PatBasicInfo_1.PatSEQ = dbo.CDSS_RecordHistory.PatSEQ"
                            + " WHERE (dbo.CDSS_RecordHistory.PatVisitDateTime = CONVERT(DATETIME, " + msg.CheckValue + ", 102))))" + (msg.LogicSign == " " ? "" : "INTERSECT");
                            SpeType++;
                            break;
                        default:
                            break;
                    }

                    WhereSQLStr = "(" + WhereSQLStr;

                    if (string.IsNullOrEmpty(msg.LogicSign.Trim()))
                    {
                        conditions.Add(WhereSQLStr);
                        WhereSQLStr = string.Empty;
                    }
                }

                this.conditionGroup.Add(new List<string> (conditions));
                conditions.Clear();
            }

            return SelectSQLStr + JoinSQLStr + "where ";
        }

        private void buttonGrouping_Click(object sender, EventArgs e)
        {
            //if (this.dataGridViewSetting.SelectedRows.Count > 0)
            //{
            //    string name = this.dataGridViewSetting.SelectedRows[0].Index.ToString();
            //    this.GetTemplateDataSet(name);

            //    this.preSelectedIndex = name;
            //}

            //this.CalcStatisicResult();
            //this.dataGridViewGroup.DataSource = this.resultTable;
        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            //int rowIndex = this.dataGridViewSetting.Rows.Add(new string[] { "新增组" });
            //this.dataGridViewSetting.Rows[rowIndex].Resizable = DataGridViewTriState.False;
        }

        private void dataGridViewSetting_SelectionChanged(object sender, EventArgs e)
        {
            //if (this.preSelectedIndex != "-1")
            //{
            //    this.GetTemplateDataSet(this.preSelectedIndex);
            //}

            //if (this.dataGridViewSetting.SelectedRows.Count > 0)
            //{
            //    string name = this.dataGridViewSetting.SelectedRows[0].Index.ToString();
            //    if (this.conditionSet.Tables.Contains(name))
            //    {
            //        this.SetTemplate(name);
            //    }
            //    else
            //    {
            //        flowPanel.Controls.Clear();
            //        QueryCount = 0;
            //    }

            //    this.preSelectedIndex = name;
            //}
        }

        private void GetTemplateDataSet(string name)
        {
            //if (this.conditionSet.Tables.Contains(name))
            //{
            //    this.conditionSet.Tables.Remove(name);
            //}

            //DataTable dt = new DataTable();

            //dt.TableName = name;
            //DataColumn[] columns = new DataColumn[] { 
            //            new DataColumn("colCnName"),
            //            new DataColumn("colName"), 
            //            new DataColumn("tableName"), 
            //            new DataColumn("colType"), 
            //            new DataColumn("sign"), 
            //            new DataColumn("vaule"), 
            //            new DataColumn("logicSign"),
            //            new DataColumn("sqlDemo")};

            //dt.Columns.AddRange(columns);
            //for (int i = 0; i < flowPanel.Controls.Count; i++)
            //{
            //    QueryBuilder.SQLStr msg = ((QueryBuilder)flowPanel.Controls[i]).ShowSQLStr();
            //    dt.Rows.Add(new object[] { msg.ConditionName, msg.Condition, msg.TableName, msg.ConditionType, msg.Sign, msg.CheckValue, msg.LogicSign, msg.SqlDemo });
            //}
            //this.conditionSet.Tables.Add(dt);
        }

        private void SetTemplate(string name)
        {
            //加载查询条件控件
            //flowPanel.Controls.Clear();
            //QueryCount = 0;

            //if (this.conditionSet.Tables.Count > 1)
            //{
            //    for (int i = 0; i < this.conditionSet.Tables[name].Rows.Count; i++)
            //    {
            //        DataRow dr = this.conditionSet.Tables[name].Rows[i];
            //        QueryBuilder.SQLStr msg = new QueryBuilder.SQLStr();
            //        msg.ConditionName = dr["colCnName"].ToString();
            //        msg.Condition = dr["colName"].ToString();
            //        msg.Sign = dr["sign"].ToString();
            //        msg.TableName = dr["tableName"].ToString();
            //        msg.LogicSign = dr["logicSign"].ToString();
            //        msg.CheckValue = dr["vaule"].ToString();
            //        msg.ConditionType = dr["colType"].ToString();
            //        msg.SqlDemo = dr["sqlDemo"].ToString();

            //        QueryBuilder qbCtl = new QueryBuilder();
            //        qbCtl.Delete += new QueryBuilder.DeleteHandler(qbCtl_Delete);
            //        qbCtl.Name = "queryBuilder" + (i + 1);
            //        qbCtl.FillQueryBuilder(msg);
            //        QueryCount = i + 1;
            //        flowPanel.Controls.Add(qbCtl);
            //    }
            //    LogicControl();
            //}
        }

        private void buttonDelCondition_Click(object sender, EventArgs e)
        {
            //this.dataGridViewSetting.Rows.Remove(this.dataGridViewSetting.SelectedRows[0]);
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            //this.dataGridViewSetting.Rows.Clear();
            //flowPanel.Controls.Clear();
            //this.resultTable.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}