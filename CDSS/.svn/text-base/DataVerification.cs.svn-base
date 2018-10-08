using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using System.IO;
using System.Data.OleDb;
using System.Threading;
using CDSSCtrlLib.MedicineControlLib;
using CDSSSystemData;
using Utilities.RecordTreeNodeStateFuction;

namespace CDSS
{
    public partial class DataVerification : Form
    {
        #region 定义的相应变量
        string TempFilePath = Application.StartupPath + "\\UserInfo\\" + GlobalData.UserInfo.UserID+"\\Config\\";

        //文件1的数据集
        private DataTable myDT1 = new DataTable();
        //文件2的数据集
        private DataTable myDT2 = new DataTable();
        //文件1、2数据集的交集
        private DataTable combineDT = new DataTable();
        //中转数据格式数据集
        private DataTable BackDT1, BackDT2;

        private Dictionary<DataRow, DataRow> interaction = new Dictionary<DataRow, DataRow>();

        private DataTable tmpDT = new DataTable();
        private WaitFrom frmWait;
        private List<string> colName = new List<string>();

        //存放保存TreeNode状态的XML文件路径
        string filePath = "RecordDataVerificationTreeNodeState.xml";
        RecordTreeNodeState recordTreeNodeState = new RecordTreeNodeState();

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataVerification()
        {
            InitializeComponent();
            CreateNodes();           

            //判断保存记录CheckTreeNode状态的文件是否存在若存在加载相应的状态
            if (File.Exists(filePath))
            {
                recordTreeNodeState.LoadState(this.treeDisplayCloumns, filePath);
            }

            myDT1.ReadXmlSchema(Application.StartupPath + "\\Resource\\CompareConfig.xml");
            myDT2 = myDT1.Clone();
            foreach (DataColumn col in myDT1.Columns)
            {
                if (col.ColumnName == "对比结果") continue;
                if (col.ColumnName == "tag") continue;
                colName.Add(col.ColumnName);
                combineDT.Columns.Add(col.ColumnName);
            }


            dgrdfile1.CellFormatting += new DataGridViewCellFormattingEventHandler(dgrd_CellFormatting);
            dgrdfile2.CellFormatting += new DataGridViewCellFormattingEventHandler(dgrd_CellFormatting);            
        }

       

        #region  DataGridView相关操作

        void dgrd_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgrd = sender as DataGridView;
            if (e.Value == null) return;
            if (e.Value.ToString().IndexOf("<color>") >= 0)
            {
                e.CellStyle.BackColor = Color.Red;
                e.Value = e.Value.ToString().Replace("<color>", "");
            }
        }

        void dgrd_SelectionChanged(object sender, EventArgs e)
        {
            if (sender.Equals(dgrdfile1))
            {
                foreach (DataGridViewRow selectRow in dgrdfile1.SelectedRows)
                {
                    if (selectRow.Index < dgrdfile2.Rows.Count)
                        dgrdfile2.Rows[selectRow.Index].Selected = true;
                }
            }
            else
            {
                foreach (DataGridViewRow selectRow in dgrdfile2.SelectedRows)
                {
                    if (selectRow.Index < dgrdfile1.Rows.Count)
                        dgrdfile1.Rows[selectRow.Index].Selected = true;
                }
            }
        }

        void dgrd_Scroll(object sender, ScrollEventArgs e)
        {
            if (sender.Equals(dgrdfile1))
            {
                dgrdfile2.FirstDisplayedScrollingRowIndex = dgrdfile1.FirstDisplayedScrollingRowIndex;
                dgrdfile2.HorizontalScrollingOffset = dgrdfile1.HorizontalScrollingOffset;
            }
            else
            {
                dgrdfile1.FirstDisplayedScrollingRowIndex = dgrdfile2.FirstDisplayedScrollingRowIndex;
                dgrdfile1.HorizontalScrollingOffset = dgrdfile2.HorizontalScrollingOffset;
            }
        }

        void dgrdfile1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }

        void dgrd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView drv1 = dgrdfile1.SelectedRows[0].DataBoundItem as DataRowView;
            DataRowView drv2 = dgrdfile2.SelectedRows[0].DataBoundItem as DataRowView;
            Dictionary<DataRow, DataRow> row = new Dictionary<DataRow, DataRow>();
            row.Add(drv1.Row, drv2.Row);
            CombineSameRow combi = new CombineSameRow();
            combi.DataRowCompleted += new CombineSameRow.DataRowCompletedHandler(frmCombineRow_DataRowCompleted);
            if (drv1.Row["对比结果"].ToString() == "相同" || drv1.Row["对比结果"].ToString() == "已核对")
            {
                MessageBox.Show("当前选择数据相同，不需合并！", "提示");
            }
            else
            {
                combi.SetVaules(row, colName);
                combi.ShowDialog();
            }
        }

        #endregion 

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

        /// <summary>
        /// 记录TreeNode的状态
        /// </summary>
        public void RecordTreeNodeState()
        {
            recordTreeNodeState.RecordState(this.treeDisplayCloumns, filePath);
        }      
        
        #region 设置数据文件，读入数据

        /// <summary>
        /// OLEDB方式访问Excel文件
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="myDT">数据容器</param>
        public void OleDBAccess(string filePath, ref DataTable myDT)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            OleDbConnection myConn = new OleDbConnection(strConn);
            string sqlstr = "";
            try
            {
                myConn.Open();
                DataTable colsMsg = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                if (colsMsg.Rows.Count == 0)
                {
                    MessageBox.Show("当前选择文件不符合导入模板格式，请重新选择！", "提示");
                    return;
                }
                else
                    sqlstr = "select * from [" + colsMsg.Rows[0]["TABLE_NAME"].ToString() + "]";
                OleDbDataAdapter myCommand = new OleDbDataAdapter(sqlstr, myConn);
                myDT = new DataTable();
                myCommand.Fill(myDT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConn.Close();
            }

        }

        /// <summary>
        /// 将数据填充到知道格式Datatable中
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <returns>填充完毕的数据集</returns>
        private DataTable FillDataTable(DataTable dataSource)
        {
            DataTable dt = myDT1.Clone();
            dt.Rows.Clear();
            foreach (DataRow sourcedr in dataSource.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (string col in colName)
                {
                    if (dataSource.Columns.Contains(col))
                        dr[col] = sourcedr[col];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        #region 线程执行函数

        private delegate void DataBindDelegate(DataGridView dgrg, DataTable myDT, int tag);

        private void DataBind(DataGridView dgrg, DataTable myDT, int tag)
        {
            if (dgrg.InvokeRequired)
            {
                DataBindDelegate databind = new DataBindDelegate(DataBind);

                this.BeginInvoke(databind, new object[] { dgrg, myDT, tag });
            }
            else
            {
                if (tag != 0)
                {
                    dgrg.DataSource = myDT;
                    if (!dgrg.Equals(dgrgCombine))
                        foreach (DataGridViewColumn col in dgrg.Columns)
                        {
                            col.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                    return;
                }
                if (dgrg.Equals(dgrdfile1))
                {
                    myDT1 = FillDataTable(myDT);
                    dgrg.DataSource = myDT1;
                    BackDT1 = myDT1.Copy();
                }
                else
                {
                    myDT2 = FillDataTable(myDT);
                    dgrg.DataSource = myDT2;
                    BackDT2 = myDT2.Copy();
                }
            }
        }

        #endregion


        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (btnDataImport.Enabled)
                {
                    dgrdfile1.DataSource = null;
                    dgrdfile2.DataSource = null;
                    textBox1.Text = "";
                    textBox2.Text = "";
                }

                FrmReset();
                if (File.Exists(openFileDialog1.FileName))
                {
                    string fileName = Path.GetFileName(openFileDialog1.FileName);
                    int maxFileLen = 20;
                    frmWait = new WaitFrom();
                    if (sender.Equals(btnOpen1))
                    {
                        textBox1.Text = openFileDialog1.FileName;
                        grbFile1.Text = string.Format("数据文件1 - {0}", fileName.Length > maxFileLen ? fileName.Substring(0, maxFileLen) : fileName);
                        backgroundWorker1.RunWorkerAsync(textBox1);
                    }
                    else
                    {
                        textBox2.Text = openFileDialog1.FileName;
                        grbFile2.Text = string.Format("数据文件2 - {0}", fileName.Length > maxFileLen ? fileName.Substring(0, maxFileLen) : fileName);
                        backgroundWorker1.RunWorkerAsync(textBox2);
                    }

                    dgrdfile1.SelectionChanged -= new EventHandler(dgrd_SelectionChanged);
                    dgrdfile2.SelectionChanged -= new EventHandler(dgrd_SelectionChanged);
                    dgrdfile1.Scroll -= new ScrollEventHandler(dgrd_Scroll);
                    dgrdfile2.Scroll -= new ScrollEventHandler(dgrd_Scroll);
                    dgrdfile1.CellDoubleClick -= new DataGridViewCellEventHandler(dgrd_CellDoubleClick);
                    dgrdfile2.CellDoubleClick -= new DataGridViewCellEventHandler(dgrd_CellDoubleClick);

                    frmWait.ShowDialog();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument.Equals(textBox1))
            {
                OleDBAccess(textBox1.Text, ref tmpDT);
                DataBind(dgrdfile1, tmpDT, 0);
            }
            else
            {
                OleDBAccess(textBox2.Text, ref tmpDT);
                DataBind(dgrdfile2, tmpDT, 0);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmWait.Close();
            if (e.Error != null)
                MessageBox.Show("数据加载过程中发生以下错误：\r\n" + e.Error.Message, "提示");
            if (myDT1.Rows.Count != 0)
            {
                lblRow1.Text = string.Format("行数：{0}", myDT1.Rows.Count.ToString());
                lblCols1.Text = string.Format("列数：{0}", myDT1.Columns.Count.ToString());
            }
            if (myDT2.Rows.Count != 0)
            {
                lblRow2.Text = string.Format("行数：{0}", myDT2.Rows.Count.ToString());
                lblCols2.Text = string.Format("列数：{0}", myDT2.Columns.Count.ToString());
            }
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
                btnDataCheck.Enabled = true;
        }

        #endregion

        #region 对照数据处理

        /// <summary>
        /// 初始化状态
        /// </summary>
        private void FrmReset()
        {
            dgrgCombine.DataSource = null;
            combineDT.Rows.Clear();
            btnCombine.Enabled = false;
            //colName.Clear();
            btnDataImport.Enabled = false;
        }

        /// <summary>
        /// 判断依据
        /// </summary>
        List<string> Standards = new List<string>();

        /// <summary>
        /// 设定判断依据
        /// </summary>
        /// <returns></returns>
        private string SetStandards()
        {
            Standards.Clear();

            string NoExistStandards = "";

            foreach (CheckTreeNode root in treeDisplayCloumns.Nodes)
            {
                foreach (CheckTreeNode child in root.Nodes)
                {
                    if (child.Checked)
                    {
                        if (colName.Contains(child.Text))
                        {
                            if (!Standards.Contains(child.Text))
                                Standards.Add(child.Text);
                        }
                        else
                            NoExistStandards = child.Text + ",";
                    }
                }
            }
            return NoExistStandards;
        }

        private void btnDataCheck_Click(object sender, EventArgs e)
        {
            FrmReset();
            string NoExistStandards = SetStandards();

            if (Standards.Count == 0)
            {
                MessageBox.Show("请选择两个数据文件共有列名做为参照", "提示");
                return;
            }
            if (NoExistStandards.Length != 0)
            {
                MessageBox.Show("两数据文件共有列列名不包含以下选项：" + Environment.NewLine + NoExistStandards, "提示");
            }
            backgroundWorker2.RunWorkerAsync();
            panel4.Visible = true;
        }

        /// <summary>
        /// 比较数据
        /// </summary>
        private void EqualData()
        {
            interaction.Clear();
            List<string> values = new List<string>();
            //List<int> addIndex = new List<int>();

            //确保以数据较多的数据集为基点，循环查找匹配项
            DataTable maxDT, minDT;

            if (myDT1.Rows.Count > myDT2.Rows.Count)
            {
                maxDT = BackDT1.Copy();
                minDT = BackDT2.Copy();
                maxDT.TableName = "myDT1";
                minDT.TableName = "myDT2";
            }
            else
            {
                maxDT = BackDT2.Copy();
                minDT = BackDT1.Copy();
                maxDT.TableName = "myDT2";
                minDT.TableName = "myDT1";
            }


            #region 对比格式，调整格式

            //调整右侧数据格式
            for (int index = 0; index < maxDT.Rows.Count; index++)
            {
                bool isFind = false;
                DataRow dr1 = maxDT.Rows[index];
                for (int i = 0; i < minDT.Rows.Count; i++)
                {
                    DataRow dr2 = minDT.Rows[i];
                    if (RowCompare(dr1, dr2, Standards))
                    {
                        if (!interaction.ContainsKey(dr1))
                            interaction.Add(dr1, dr2);
                        else
                        {
                            throw new Exception("条件不足，左右文件不是一一对应");
                            //MessageBox.Show("条件不足，左右文件不是一一对应", "提示");
                            //return false;
                        }
                        if (RowCompare(ref dr1, ref dr2, colName))
                        {
                            interaction.Remove(dr1);
                            dr1["对比结果"] = "相同";
                            dr2["对比结果"] = "相同";
                            DataRow dr = combineDT.NewRow();
                            foreach (string col in colName)
                            {
                                dr[col] = dr1[col];
                            }
                            combineDT.Rows.Add(dr);
                        }
                        isFind = true;
                    }
                }

                interaction.Clear();

                if (!isFind)
                {
                    //addIndex.Add(index + 1);
                    dr1["对比结果"] = "无对应行";
                    DataRow newRow = minDT.NewRow();
                    newRow["对比结果"] = "空白行";
                    minDT.Rows.InsertAt(newRow, index + 1);
                    //interaction.Add(dr1, newRow);
                }
            }

            //调整左侧数据格式            
            int idx = 0;
            foreach (DataRow dr2 in minDT.Rows)
            {
                bool isFind = false;
                if (dr2[0].ToString().Equals("空白行"))
                {
                    idx++;
                    continue;
                }
                foreach (DataRow dr1 in maxDT.Rows)
                {
                    if (RowCompare(dr2, dr1, Standards))
                    {
                        if (!interaction.ContainsKey(dr2))
                        {
                            if (dr1["对比结果"].ToString() != "相同")
                                interaction.Add(dr2, dr1);
                        }
                        else
                        {
                            throw new Exception("条件不足，左右文件不是一一对应");
                            //MessageBox.Show("条件不足，左右文件不是一一对应", "提示");
                            //return false;
                        }
                        isFind = true;
                    }
                }
                if (!isFind)
                {
                    dr2["对比结果"] = "无对应行";
                    DataRow newRow = maxDT.NewRow();
                    newRow["对比结果"] = "空白行";
                    maxDT.Rows.InsertAt(newRow, idx);
                    //interaction.Add(dr2, newRow);
                }
                idx++;
            }

            myDT1.Rows.Clear();
            myDT2.Rows.Clear();

            if (maxDT.TableName == "myDT1")
            {
                myDT1 = maxDT;
                myDT2 = minDT;
            }
            else
            {
                myDT1 = minDT;
                myDT2 = maxDT;
            }

            #endregion

            //dgrdfile1.DataSource = myDT1;
            DataBind(dgrdfile1, myDT1, 1);
            //dgrdfile2.DataSource = myDT2;
            DataBind(dgrdfile2, myDT2, 1);
            //dgrgCombine.DataSource = combineDT;
            DataBind(dgrgCombine, combineDT, 1);
            //return true;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            EqualData();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panel4.Visible = false;
            if (e.Error == null)
            {
                dgrdfile1.CellDoubleClick += new DataGridViewCellEventHandler(dgrd_CellDoubleClick);
                dgrdfile2.CellDoubleClick += new DataGridViewCellEventHandler(dgrd_CellDoubleClick);
                dgrdfile1.SelectionChanged += new EventHandler(dgrd_SelectionChanged);
                dgrdfile2.SelectionChanged += new EventHandler(dgrd_SelectionChanged);
                //控制2个列表同步滚动
                dgrdfile1.Scroll += new ScrollEventHandler(dgrd_Scroll);
                dgrdfile2.Scroll += new ScrollEventHandler(dgrd_Scroll);
                btnCombine.Enabled = true;
                btnDataImport.Enabled = true;
            }
            else
            {
                MessageBox.Show(e.Error.Message, "提示");
            }
        }

        #region 对比数据行函数重载

        /// <summary>
        /// 对比2个数据行是否相同
        /// </summary>
        /// <param name="dr1">类型：DataGridViewRow，对照数据1</param>
        /// <param name="dr2">类型：DataGridViewRow，对照数据2</param>
        /// <param name="Standards">对照标准</param>
        /// <returns>true：相同；false：不同</returns>
        private bool RowCompare(DataGridViewRow dr1, DataGridViewRow dr2, List<string> Standards)
        {
            foreach (string standard in Standards)
            {
                if (dr1.Cells[standard].Value.ToString().Trim().ToLower() != dr2.Cells[standard].Value.ToString().Trim().ToLower())
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 对比2个数据行是否相同
        /// </summary>
        /// <param name="dr1">类型：DataRow，对照数据1</param>
        /// <param name="dr2">类型：DataRow，对照数据2</param>
        /// <param name="Standards">对照标准</param>
        /// <returns>true：相同；false：不同</returns>
        private bool RowCompare(ref DataRow dr1, ref DataRow dr2, List<string> Standards)
        {
            bool rval = true;
            int diffCount = 0;
            foreach (string standard in Standards)
            {
                if (dr1[standard].ToString().Trim().ToLower() != dr2[standard].ToString().Trim().ToLower())
                {
                    dr1[standard] = "<color>" + dr1[standard].ToString();
                    dr2[standard] = "<color>" + dr2[standard].ToString();
                    rval = false;
                    diffCount++;
                }
            }
            if (!rval)
                dr1["对比结果"] = dr2["对比结果"] = string.Format("不同处：{0}", diffCount);
            return rval;
        }

        /// <summary>
        /// 对比2个数据行是否相同
        /// </summary>
        /// <param name="dr1">类型：DataRow，对照数据1</param>
        /// <param name="dr2">类型：DataRow，对照数据2</param>
        /// <param name="Standards">对照标准</param>
        /// <returns>true：相同；false：不同</returns>
        private bool RowCompare(DataRow dr1, DataRow dr2, List<string> Standards)
        {
            foreach (string standard in Standards)
            {
                if (dr1[standard].ToString().Trim().ToLower() != dr2[standard].ToString().Trim().ToLower())
                    return false;
            }
            return true;
        }

        #endregion

        #endregion

        private void btnCombine_Click(object sender, EventArgs e)
        {
            CombineSameRow frmCombineRow = new CombineSameRow();
            frmCombineRow.DataRowCompleted += new CombineSameRow.DataRowCompletedHandler(frmCombineRow_DataRowCompleted);
            if (interaction.Count == 0)
                MessageBox.Show("左右所有数据均相同，不需要合并", "提示");
            else
            {
                frmCombineRow.SetVaules(interaction, colName);
                frmCombineRow.ShowDialog();
                rowList.Clear();
            }
        }

        Dictionary<DataRow, DataRow> rowList = new Dictionary<DataRow, DataRow>();

        void frmCombineRow_DataRowCompleted(object sender, DataRowCompletedEventArgs e)
        {
            List<object> val = new List<object>();
            e.DiffRowOne["对比结果"] = e.DiffRowTwo["对比结果"] = "已核对";
            if (!rowList.ContainsKey(e.DiffRowOne))
            {
                DataRow dr = combineDT.NewRow();
                if (e.DiffRowOne["对比结果"].ToString() == "空白行")
                    rowList.Add(e.DiffRowTwo, dr);
                else
                    rowList.Add(e.DiffRowOne, dr);
                combineDT.Rows.Add(dr);
            }
            foreach (string col in colName)
            {
                e.DiffRowOne[col] = e.RowData.Cells[col].Value;
                e.DiffRowTwo[col] = e.RowData.Cells[col].Value;
                rowList[e.DiffRowOne][col] = e.RowData.Cells[col].Value;
            }
            if (interaction.ContainsKey(e.DiffRowOne))
                interaction.Remove(e.DiffRowOne);
            if (interaction.ContainsKey(e.DiffRowTwo))
                interaction.Remove(e.DiffRowTwo);
        }

        private void btnDataImport_Click(object sender, EventArgs e)
        {
            ImportForm importfrm = new ImportForm();
            importfrm.FormBorderStyle = FormBorderStyle.Sizable;
            importfrm.IsExcelSource = false;
            importfrm.DataSource = combineDT;
            importfrm.ShowDialog();
        }
    }
}