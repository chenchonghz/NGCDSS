using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSSystemData;
using CDSSDBAccess;
using System.IO;
using Utilities.RecordTreeNodeStateFuction;
using System.Data.SQLite;

namespace CDSS
{
    public partial class HighLevelQueryForm : InfoFormBaseClass
    {
        #region --Fields/Attributes/Propertys--

        int SpeType = 0;
        int QueryCount = 1;//条件控件个数（查询条件个数）
        string TempFilePath = Application.StartupPath + "\\UserInfo\\" + GlobalData.UserInfo.UserID;
        ToolTip tips = new ToolTip();
        public event CustomEventHandle ShowInfoEnterPageEvent;

        //存放保存TreeNode状态的XML文件路径
        string filePath = "RecordHighLeverQueryFormTreeNodeState.xml";
        RecordTreeNodeState recordTreeNodeState = new RecordTreeNodeState();

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public HighLevelQueryForm()
        {
            InitializeComponent();
            CreateNodes();

            //判断保存记录CheckTreeNode状态的文件是否存在若存在加载相应的状态
            if (File.Exists(filePath))
            {
                recordTreeNodeState.LoadState(this.treeDisplayCloumns, filePath);
            }
        }

        #region --Functions--
        
        /// <summary>
        /// 根据配置生成树
        /// </summary>
        private void CreateNodes()
        {
            CheckTreeNode root, node;
            treeDisplayCloumns.Nodes.Clear();
            DataSet ds = new DataSet();
            ds.ReadXml(Application.StartupPath + "\\Resource\\TreeConfig.xml");
            foreach (DataTable dt in ds.Tables)
            {
                string[] tmp = dt.TableName.Split('!');
                root = new CheckTreeNode(tmp[1]);
                root.Name = tmp[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["canSelect"].ToString() == "0")
                    {
                        node = new CheckTreeNode(dr["cnName"].ToString());
                        node.Name = dr["enName"].ToString();
                        node.Tag = new string[] { tmp[0], dr["tag"].ToString(), dr["JoinSqlStr"].ToString(), dr["standard"].ToString() };
                        if (dr["Visible"].ToString() == "true")
                            root.Nodes.Add(node);
                    }
                }
                if (root.Nodes.Count > 0)
                    treeDisplayCloumns.Nodes.Add(root);
            }
        }        

        #region 模板存储

        /// <summary>
        /// 控制最后一个控件逻辑关系只有“无”，其他的只有“与或”；
        /// </summary>
        private void LogicControl()
        {
            foreach (QueryBuilder var in flowPanel.Controls)
            {
                if (var.Name == "queryBuilder" + flowPanel.Controls.Count)
                {
                    string old = var.cmbLgc.Text;
                    var.cmbLgc.Items.Clear();
                    var.cmbLgc.Items.AddRange(new object[] { "无" });
                    var.cmbLgc.SelectedIndex = 0;
                    var.cmbLgc.Text = old;
                }
                else
                {
                    string old = var.cmbLgc.Text;
                    var.cmbLgc.Items.Clear();
                    var.cmbLgc.Items.AddRange(new object[] { "且", "或" });
                    var.cmbLgc.Text = old;
                }
            }
        }

        /// <summary>
        /// 根据QueryCount的值创建相应个数控件
        /// </summary>
        private void CreateQueryBuilder()
        {
            QueryBuilder qbCtl = new QueryBuilder();
            qbCtl.Name = "queryBuilder" + QueryCount;
            qbCtl.Delete += new QueryBuilder.DeleteHandler(qbCtl_Delete);
            qbCtl.AutoValidate = AutoValidate.EnableAllowFocusChange;
            qbCtl.Validating += new CancelEventHandler(qbCtl_Validating);
            flowPanel.Controls.Add(qbCtl);
            flowPanel.ScrollControlIntoView(qbCtl);
            LogicControl();
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="filePath">模板信息文件存放路径</param>
        private void SaveTemplate(string filePath)
        {
            AddSelectTemplate getTemplateNameFrm = new AddSelectTemplate();
            getTemplateNameFrm.txtTemplateName.Text = lblTemplateName.Text;
            getTemplateNameFrm.txtTemplateName.SelectAll();
            getTemplateNameFrm.IsNew = true;
            getTemplateNameFrm.FilePath = filePath;
            getTemplateNameFrm.ShowDialog();
            if (getTemplateNameFrm.DialogResult == DialogResult.OK)
            {
                DataSet ds = GetTemplateDataSet(getTemplateNameFrm.txtTemplateName.Text.Trim(), getTemplateNameFrm.txtRemark.Text.Trim());
                ds.WriteXml(getTemplateNameFrm.FileName);
            }
            lblTemplateName.Text = getTemplateNameFrm.txtTemplateName.Text.Trim();
        }

        /// <summary>
        /// 返回模板的数据
        /// 表格1表示查询条件
        /// 表格2表示需要显示行、模板注释                
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="remark">模板注释</param>
        /// <returns>DataSet</returns>
        private DataSet GetTemplateDataSet(string name, string remark)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.TableName = "query";
            DataColumn[] columns = new DataColumn[] { 
                        new DataColumn("colCnName"),
                        new DataColumn("colName"), 
                        new DataColumn("tableName"), 
                        new DataColumn("colType"), 
                        new DataColumn("sign"), 
                        new DataColumn("vaule"), 
                        new DataColumn("logicSign")};
            dt.Columns.AddRange(columns);
            for (int i = 0; i < flowPanel.Controls.Count; i++)
            {
                QueryBuilder.SQLStr msg = ((QueryBuilder)flowPanel.Controls[i]).ShowSQLStr();
                dt.Rows.Add(new object[] { msg.ConditionName, msg.Condition, msg.TableName, msg.ConditionType, msg.Sign, msg.CheckValue, msg.LogicSign });
            }
            ds.Tables.Add(dt);

            dt = new DataTable();

            dt.TableName = "tempInfo";
            columns = new DataColumn[] { 
                        new DataColumn("tempName"), 
                        new DataColumn("tempRemark"), 
                        new DataColumn("displayColumn")};
            dt.Columns.AddRange(columns);
            dt.Rows.Add(new object[] { name, remark, GetTreeSelect() });
            ds.Tables.Add(dt);

            return ds;
        }

        /// <summary>
        /// 获取可显示项TreeView的选项，转化成一定格式的字符串
        /// </summary>
        /// <returns>转化后的字符串</returns>
        private string GetTreeSelect()
        {
            string selectVal = "", tmp = "";
            foreach (TreeNode node in treeDisplayCloumns.Nodes)
            {
                bool hasChecked = false;
                foreach (TreeNode child in node.Nodes)
                {
                    if (child.Checked)
                    {
                        tmp += "," + child.Name;
                        hasChecked = true;
                    }
                }
                if (hasChecked)
                    tmp = node.Name + tmp + "|";
                else
                    tmp = "";

                selectVal += tmp;
                tmp = "";
            }
            return selectVal;
        }

        /// <summary>
        /// 加载模板数据
        /// </summary>
        /// <param name="DataSource">加载的数据源</param>
        /// <returns>是否成功</returns>
        private bool LoadTemplate(DataSet DataSource)
        {
            //加载查询条件控件
            flowPanel.Controls.Clear();
            QueryCount = 0;

            if (DataSource.Tables.Count > 1)
            {
                for (int i = 0; i < DataSource.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = DataSource.Tables[0].Rows[i];
                    QueryBuilder.SQLStr msg = new QueryBuilder.SQLStr();
                    msg.ConditionName = dr["colCnName"].ToString();
                    msg.Condition = dr["colName"].ToString();
                    msg.Sign = dr["sign"].ToString();
                    msg.TableName = dr["tableName"].ToString();
                    msg.LogicSign = dr["logicSign"].ToString();
                    msg.CheckValue = dr["vaule"].ToString();
                    msg.ConditionType = dr["colType"].ToString();

                    QueryBuilder qbCtl = new QueryBuilder();
                    qbCtl.Delete += new QueryBuilder.DeleteHandler(qbCtl_Delete);
                    qbCtl.Name = "queryBuilder" + (i + 1);
                    qbCtl.FillQueryBuilder(msg);
                    QueryCount = i + 1;
                    flowPanel.Controls.Add(qbCtl);
                }
                LogicControl();

                //加载显示定制数据、模板名称等
                lblTemplateName.Text = DataSource.Tables[1].Rows[0]["tempName"].ToString();

                string dispplayColumn = DataSource.Tables[1].Rows[0]["displayColumn"].ToString();

                LoadDisplayTree(dispplayColumn);
            }
            else
            {
                lblTemplateName.Text = DataSource.Tables[0].Rows[0]["tempName"].ToString();

                string dispplayColumn = DataSource.Tables[0].Rows[0]["displayColumn"].ToString();

                LoadDisplayTree(dispplayColumn);
            }
            return false;
        }

        /// <summary>
        /// 加载显示定制树
        /// </summary>
        /// <param name="data"></param>
        private void LoadDisplayTree(string data)
        {
            treeDisplayCloumns.UncheckAll();
            for (int i = 0; i < data.Split('|').Length - 1; i++)
            {
                string[] ColumnMsg = data.Split('|')[i].Split(',');
                if (ColumnMsg.Length == 0) return;
                TreeNode[] root = treeDisplayCloumns.Nodes.Find(ColumnMsg[0], false);
                int checkCount = 0;
                for (int j = 1; j < ColumnMsg.Length; j++)
                {
                    CheckTreeNode node = root[0].Nodes.Find(ColumnMsg[j], false)[0] as CheckTreeNode;
                    node.Toggle(CheckTreeNode.CheckBoxState.Unchecked);
                    checkCount++;
                }
            }
        }

        #endregion

        #region 查询语句生成

        /// <summary>
        /// 通过树的选项构造SQL查询语句的显示字段
        /// </summary>
        /// <returns></returns>
        private string InitDisplayFieldsByTree()
        {
            string selectVal = "", tmp = "";
            for (int i = 0; i < treeDisplayCloumns.Nodes.Count; i++)
            {
                TreeNode node = treeDisplayCloumns.Nodes[i];
                bool hasChecked = false;
                for (int j = 0; j < node.Nodes.Count; j++)
                {
                    TreeNode child = node.Nodes[j];
                    if (child.Checked)
                    {
                        tmp += "," + child.Name + " [" + child.Text + "]";
                        hasChecked = true;
                    }
                }
                if (hasChecked)
                    tmp = node.Name + tmp + "|";
                else
                    tmp = "";

                selectVal += tmp;
                tmp = "";
            }
            return selectVal;
        }

        /// <summary>
        /// 按照条件生成查询SQL语句
        /// </summary>
        //private string InitSQLStr()
        //{
        //    SpeType = 0;
        //    string SelectSQLStr = "select CDSS_RecordHistory.RecordSEQ from CDSS_RecordHistory ";
        //    string JoinSQLStr = "";
        //    string WhereSQLStr = " ";
        //    DESClass DESClass = new DESClass();
        //    for (int i = 0; i < flowPanel.Controls.Count; i++)
        //    {
        //        QueryBuilder qb = (QueryBuilder)flowPanel.Controls[i];
        //        QueryBuilder.SQLStr msg = qb.ShowSQLStr();
        //        if (msg.TableName != "CDSS_RecordHistory" && qb.ValueType != QueryBuilder.ValueTypes.Other)
        //        {
        //            if (JoinSQLStr.IndexOf(msg.TableName) < 0)
        //                JoinSQLStr += msg.SqlDemo;
        //        }
        //        if (msg.Condition == "PatName")
        //            msg.CheckValue = "'" + DESClass.DESEncrypt(msg.CheckValue.Replace("'", "")) + "'";
        //        if (msg.Condition == "PatBirthday")
        //            msg.CheckValue = "'" + DESClass.DESEncrypt(msg.CheckValue.Replace("'", "") + " 0:00:00") + "'";
        //        switch (qb.ValueType)
        //        {
        //            case QueryBuilder.ValueTypes.Text:
        //                WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
        //                break;
        //            case QueryBuilder.ValueTypes.Num:
        //                try
        //                {
        //                    Convert.ToDouble(msg.CheckValue);
        //                }
        //                catch
        //                {
        //                    throw new Exception("条件错误：“" + msg.ConditionName + "”值为数字，请重新输入");
        //                }
        //                WhereSQLStr += "convert(float," + msg.TableName + "." + msg.Condition + ")" + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
        //                break;
        //            case QueryBuilder.ValueTypes.Date:
        //                try
        //                {
        //                    Convert.ToDateTime(msg.CheckValue.Replace("'", ""));
        //                }
        //                catch
        //                {
        //                    throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
        //                }
        //                WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
        //                break;
        //            case QueryBuilder.ValueTypes.Bool:
        //                if (msg.CheckValue.Replace("'", "") == "是")
        //                {
        //                    msg.CheckValue = "'true'";
        //                }
        //                else if (msg.CheckValue.Replace("'", "") == "否")
        //                {
        //                    msg.CheckValue = "'false'";
        //                }
        //                else if (msg.CheckValue.Replace("'", "") == "1")
        //                {
        //                    msg.CheckValue = "'true'";
        //                }
        //                else if (msg.CheckValue.Replace("'", "") == "0")
        //                {
        //                    msg.CheckValue = "'false'";
        //                }

        //                try
        //                {
        //                    Convert.ToBoolean(msg.CheckValue.Replace("'", ""));
        //                }
        //                catch
        //                {
        //                    throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
        //                }
        //                WhereSQLStr += msg.TableName + "." + msg.Condition + msg.Sign + msg.CheckValue + ")" + msg.LogicSign;
        //                break;
        //            case QueryBuilder.ValueTypes.Other:
        //                try
        //                {
        //                    Convert.ToDateTime(msg.CheckValue.Replace("'", ""));
        //                }
        //                catch
        //                {
        //                    throw new Exception("条件错误：“" + msg.ConditionName + "”值为日期，请重新输入");
        //                }
        //                SelectSQLStr = "SELECT CDSS_RecordHistory.RecordSEQ from CDSS_RecordHistory left join CDSS_PatBasicInfo on CDSS_RecordHistory.PatSEQ = CDSS_PatBasicInfo.PatSEQ ";
        //                WhereSQLStr += " (SELECT CDSS_PatBasicInfo_1.PatSEQ FROM dbo.CDSS_PatBasicInfo AS CDSS_PatBasicInfo_1 INNER JOIN "
        //                + "dbo.CDSS_RecordHistory ON CDSS_PatBasicInfo_1.PatSEQ = dbo.CDSS_RecordHistory.PatSEQ"
        //                + " WHERE (dbo.CDSS_RecordHistory.PatVisitDateTime = CONVERT(DATETIME, " + msg.CheckValue + ", 102))))" + (msg.LogicSign == " " ? "" : "INTERSECT");
        //                SpeType++;
        //                break;
        //            default:
        //                break;
        //        }
        //        WhereSQLStr = "(" + WhereSQLStr;
        //    }
        //    if (SpeType > 0)
        //    {
        //        if (SpeType == QueryCount)
        //            WhereSQLStr = " CDSS_PatBasicInfo.PatSEQ in" + WhereSQLStr;
        //        else
        //            throw new Exception("若条件中有一项为“就诊时间筛选”，则查询条件必须全部为此类型");
        //    }
        //    //change by zx 修正能查找到其他医生的病人资料
        //    string sqlStr = "select RecordSEQ from CDSS_RecordInfo where RecordSEQ in(" + SelectSQLStr + JoinSQLStr + "where" + WhereSQLStr + ")"; //and UserID='" + GlobalData.UserInfo.UserID + "'";

        //    string showSelectSQLStr = " from CDSS_RecordHistory ";
        //    string showJoinSQLStr = "";
        //    string showWhereSQLStr = "where CDSS_RecordHistory.RecordSEQ in (" + sqlStr + ")";
        //    string showFileds = "";


        //    string[] displayTables = InitDisplayFieldsByTree().Split('|');
        //    for (int i = 0; i < displayTables.Length; i++)
        //    {
        //        if (displayTables[i] != string.Empty)
        //        {
        //            string[] fields = displayTables[i].Split(',');
        //            for (int j = 1; j < fields.Length; j++)
        //            {
        //                showFileds += fields[0] + "." + fields[j] + ",";
        //            }
        //            if (showFileds.IndexOf("RecordSEQ") < 0)
        //            {
        //                showFileds += "CDSS_RecordHistory.RecordSEQ,CDSS_RecordHistory.HistoryRecordStatus,";
        //            }
        //            if (fields[0] == "CDSS_PatBasicInfo")
        //            {
        //                if (showJoinSQLStr.IndexOf("CDSS_PatBasicInfo") < 0)
        //                {
        //                    showJoinSQLStr += "left join " + fields[0] + " on CDSS_RecordHistory.PatSEQ = " + fields[0] + ".PatSEQ ";
        //                }
        //            }
        //            else if (fields[0] != "CDSS_RecordHistory")
        //            {
        //                if (showJoinSQLStr.IndexOf(fields[0]) < 0)
        //                    showJoinSQLStr += "left join " + fields[0] + " on CDSS_RecordHistory.RecordSEQ = " + fields[0] + ".RecordSEQ ";
        //            }
        //        }
        //    }
        //    return "select distinct " + showFileds.Substring(0, showFileds.Length - 1) + showSelectSQLStr + showJoinSQLStr + showWhereSQLStr;

        //}

        /// <summary>
        /// 确认所有条件填写是否完全
        /// </summary>
        /// <returns></returns>
        private bool CheckQuery()
        {
            errorProvider1.Clear();
            bool qtag = true, ttag = false;
            if (flowPanel.Controls.Count == 0)
            {
                MessageBox.Show("请添加检索条件", "提示");
                return false;
            }
            foreach (Control var in flowPanel.Controls)
            {
                QueryBuilder qb = var as QueryBuilder;

                if (qb.cmbName.SelectNode == null || qb.cmbName.Text == string.Empty)
                {
                    errorProvider1.SetError(qb.cmbName, "请选择相应的条件");
                    qtag = false;
                }
                if (qb.cmbJdge.Text == string.Empty)
                {
                    errorProvider1.SetError(qb.cmbJdge, "请选择比较符号");
                    qtag = false;
                }
                if (qb.txtValue.Text == string.Empty)
                {
                    errorProvider1.SetError(qb.txtValue, "请按照规则填写查询值");
                    qtag = false;
                }
                if (qb.cmbLgc.Text == string.Empty)
                {
                    errorProvider1.SetError(qb.cmbLgc, "请选择逻辑符号");
                    qtag = false;
                }
            }
            foreach (TreeNode root in treeDisplayCloumns.Nodes)
            {
                if (root.Checked)
                    ttag = true;
                else
                    foreach (TreeNode child in root.Nodes)
                    {
                        if (child.Checked)
                            ttag = true;
                    }
            }
            if (ttag == false)
            {
                MessageBox.Show("请勾选“选待显示项”列表", "提示");
            }
            return (qtag & ttag);
        }

        /// <summary>
        /// 确认单个查询条件是否完全
        /// </summary>
        /// <param name="qb"></param>
        /// <returns></returns>
        private bool CheckQuery(QueryBuilder qb)
        {
            errorProvider1.Clear();

            if (qb.cmbName.SelectNode == null || qb.cmbName.Text == string.Empty)
                errorProvider1.SetError(qb.cmbName, "请选择相应的条件");
            if (qb.cmbJdge.Text == string.Empty)
                errorProvider1.SetError(qb.cmbJdge, "请选择比较符号");
            if (qb.txtValue.Text == string.Empty)
                errorProvider1.SetError(qb.txtValue, "请按照规则填写查询值");
            if (qb.cmbLgc.Text == string.Empty)
                errorProvider1.SetError(qb.cmbLgc, "请选择逻辑符号");

            return false;
        }

        #endregion

        /// <summary>
        /// 该函数用于引发事件
        /// </summary>
        private void ShowInfoEnterPage()
        {
            CustomEventHandle temp = ShowInfoEnterPageEvent;
            if (temp != null)
                temp();
        }

        #endregion

        #region --Events--

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            QueryCount++;
            CreateQueryBuilder();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (flowPanel.Controls.Count == 0)
            {
                MessageBox.Show("请添加检索条件后保存", "提示");
                return;
            }
            if (!File.Exists(TempFilePath))
            {
                Directory.CreateDirectory(TempFilePath);
            }
            SaveTemplate(TempFilePath + "\\");
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            tempList.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(TempFilePath);
            if (!File.Exists(TempFilePath))
            {
                Directory.CreateDirectory(TempFilePath);
            }
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            foreach (FileSystemInfo file in files)
            {
                if (file.Extension.ToLower() == ".xml")
                {
                    ToolStripMenuItem fileItem = new ToolStripMenuItem(file.Name.Replace(".xml", ""));
                    DataSet ds = new DataSet();

                    ds.ReadXml(file.FullName);
                    fileItem.Tag = ds;
                    fileItem.ToolTipText = ds.Tables[ds.Tables.Count - 1].Rows[0]["tempRemark"].ToString();
                    fileItem.Click += new EventHandler(fileItem_Click);
                    tempList.Items.Add(fileItem);
                }
            }
            if (tempList.Items.Count == 0)
            {
                tempList.Items.Add("无可用模板");
            }
            tempList.Show(BtnOpen, 0, BtnOpen.Height);
        }

        private void fileItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            DataSet ds = menuItem.Tag as DataSet;
            try
            {
                LoadTemplate(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void HighLevelQueryForm_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(BtnAdd, "新增查询条件");
            toolTip1.SetToolTip(BtnSave, "保存查询模板");
            toolTip1.SetToolTip(BtnOpen, "导入查询模板");
            toolTip1.SetToolTip(BtnExprt, "导出查询结果");
            //初始化一个查询条件控件 AddedBy ZX,100413
            CreateQueryBuilder();
            //CreateNodes();
        }

        private void qbCtl_Delete(object sender, EventArgs e)
        {
            QueryBuilder qb = (QueryBuilder)sender;
            flowPanel.Controls.Remove(qb);
            QueryCount--;
            for (int i = 1; i <= flowPanel.Controls.Count; i++)
            {
                QueryBuilder ctl = (QueryBuilder)flowPanel.Controls[i - 1];
                ctl.Name = "queryBuilder" + i;
            }
            LogicControl();
        }

        private void BtnMdlMng_Click(object sender, EventArgs e)
        {
            TemplateManage manageFrm = new TemplateManage();
            manageFrm.ShowDialog();
            DataSet ds = manageFrm.TemplateDataSet;
            if (ds != null)
            {
                LoadTemplate(ds);
            }
        }

        private void BtnSrch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DESClass DESClass = new DESClass();
            if (CheckQuery())
            {
                //string sql;
                //try
                //{
                //    sql = InitSQLStr();
                //}
                //catch (Exception ex)
                //{
                //    this.Cursor = Cursors.Default;
                //    MessageBox.Show(ex.Message);
                //    return;
                //}
                try
                {
                    //DataTable dt = SQLServerDBInterface.GetDataSet(InitSQLStr());
                    string[] displayTables = InitDisplayFieldsByTree().Split('|');
                    string querysql = DBAccess.InitSQLStr(flowPanel, displayTables);
                    DataTable dt =DBAccess.Highlevelqueryfunction(querysql);
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            dr["姓名"] = DESClass.DESDecrypt(dr["姓名"].ToString());
                        }
                        catch
                        {

                        }
                        try
                        {
                            dr["出生日期"] = Convert.ToDateTime(DESClass.DESDecrypt(dr["出生日期"].ToString())).ToString("yyyy-MM-dd");
                        }
                        catch
                        {

                        }

                    }
                    dgrdClear();
                    dataGridView1.DataSource = dt;
                    if (dataGridView1.Rows.Count != 0)
                    {
                        BtnExprt.Enabled = true;
                        lblCount.Text = dataGridView1.Rows.Count.ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示");
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void qbCtl_Validating(object sender, CancelEventArgs e)
        {
            CheckQuery(sender as QueryBuilder);
        }

        private void BtnClr_Click(object sender, EventArgs e)
        {
            flowPanel.Controls.Clear();
            QueryCount = 1;
            CreateQueryBuilder();
            treeDisplayCloumns.UncheckAll();
            lblTemplateName.Text = "新建 查询模板";
            dgrdClear();
            BtnExprt.Enabled = false;
            lblCount.Text = "0";
        }

        public void dgrdClear()
        {
            dataGridView1.DataSource = null;
            try
            {
                dataGridView1.Columns.Add(RecordSEQ);
                dataGridView1.Columns.Add(HistoryRecordStatus);
            }
            catch
            {

            }
            BtnExprt.Enabled = false;
        }

        private void BtnExprt_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    string result = "";
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (col.Visible)
                            result += col.Name + ",";
                    }
                    result += "\r\n";
                    foreach (DataGridViewRow dgrRow in dataGridView1.Rows)
                    {
                        string tmp = "";
                        foreach (DataGridViewCell cell in dgrRow.Cells)
                        {
                            if (cell.Visible)
                                tmp += cell.Value + ",";
                        }
                        result += tmp + "\r\n";
                    }
                    try
                    {
                        File.WriteAllText(saveFileDialog1.FileName, result, Encoding.Default);
                        MessageBox.Show("数据导出成功", "提示");
                    }
                    catch (IOException IOex)
                    {
                        MessageBox.Show(IOex.Message, "提示");
                    }

                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (int.Parse(dataGridView1.SelectedRows[0].Cells["HistoryRecordStatus"].Value.ToString()) == 2)
                {
                    MessageBox.Show("该条记录已经作废，无法提供修改", "提示");
                    this.Cursor = Cursors.Default;
                    return;
                }
                GlobalData.RecordInfo.RecordSeq = int.Parse(dataGridView1.SelectedRows[0].Cells["RecordSEQ"].Value.ToString());
                ShowInfoEnterPage();
            }
            this.Cursor = Cursors.Default;

            /********************************************************************************
            *作用：用户点击查询界面的【浏览】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "查询浏览";
            CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);
        }

        #endregion      
        
    }
}