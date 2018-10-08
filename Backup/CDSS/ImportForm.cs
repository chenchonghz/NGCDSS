using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using CDSSCtrlLib;
using CDSSSystemData;
using CDSSFunction;
using CDSSDBAccess;
using CDSSCLIPSEngine;
using Utilities.RecordTreeNodeStateFuction;

namespace CDSS
{
    public partial class ImportForm : CDSSCtrlLib.InfoFormBaseClass
    {
        #region �������Ӧ����
        private DataTable myDT;
        private DataTable failDT;
        private List<string> enList = new List<string>();
        private List<string> cnList = new List<string>();
        private bool isExcelSource = true;

        //�����¼�ý�����CheckTreeNode��ѡ��״̬��XML�ļ�·��
        private string filePath = "RecordImportFormTreeNodeState.xml";
        private RecordTreeNodeState recordTreeNodeState = new RecordTreeNodeState();
        
        public bool IsExcelSource
        {
            get { return isExcelSource; }
            set
            {
                panel1.Visible = value;
                dataGridView1.Visible = !value;
                isExcelSource = value;
            }
        }

        private DataTable dataSource;

        public DataTable DataSource
        {
            get { return dataSource; }
            set
            {
                dataGridView1.DataSource = value;
                dataSource = value;
            }
        }

        private DataTable FailDT
        {
            get
            {
                if (failDT == null)
                {
                    failDT = myDT.Clone();
                    failDT.Columns.Add("ʧ��ԭ��");
                    failDT.Rows.Clear();
                }
                return failDT;
            }
        }

        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public ImportForm()
        {
            InitializeComponent();
            CreateNodes();

            //�жϱ����¼CheckTreeNode״̬���ļ��Ƿ���������ڼ�����Ӧ��״̬
            if(File.Exists (filePath ))
            {
                recordTreeNodeState.LoadState(this.treeDisplayCloumns, filePath);
            }
        }

        /// <summary>
        /// ��������������
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
                        {
                            root.Nodes.Add(node);
                            cnList.Add(dr["cnName"].ToString());
                        }
                    }
                }
                if (root.Nodes.Count > 0)
                    treeDisplayCloumns.Nodes.Add(root);
            }
        }

        /// <summary>
        /// ��¼TreeNode��״̬
        /// </summary>
        public void RecordTreeNodeState()
        {
            recordTreeNodeState.RecordState(this.treeDisplayCloumns, filePath);
        }      

        /// <summary>
        /// ȷ���������Ƿ�ѡ��
        /// </summary>
        private bool NodesIsChecked()
        {
            foreach (CheckTreeNode root in treeDisplayCloumns.Nodes)
            {
                foreach (CheckTreeNode child in root.Nodes)
                {
                    if (child.Checked)
                        return true;
                }
            }
            return false;
        }

        private void buttonSetFilePath_Click(object sender, EventArgs e)
        {
            if (this.openFileDialogExcel.ShowDialog() == DialogResult.OK)
            {
                this.textBoxFilePath.Text = this.openFileDialogExcel.FileName;
                if (File.Exists(this.textBoxFilePath.Text))
                {
                    try
                    {
                        OleDBAccess();
                    }
                    catch
                    {
                        MessageBox.Show("�������󣬿���ԭ��ѡ����ļ����ܲ�����ȷ��Excel�ļ�", "��ʾ");
                        return;
                    }
                    //failDT = myDT.Clone();
                    //failDT.Columns.Add("ʧ��ԭ��");
                    //failDT.Rows.Clear();
                    FieldExist(true);
                    ExcelControl.OpenExcelFile(textBoxFilePath.Text.Trim());
                    btnClose.Enabled = btnShowMaxExcel.Enabled = true;
                }
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (IsExcelSource)
            {
                ExcelControl.ExcelSave();
                if (!NodesIsChecked())
                {
                    MessageBox.Show("�������ü�¼��������", "��ʾ");
                    return;
                }
                if (File.Exists(this.textBoxFilePath.Text))
                {
                    this.checkBoxReason.Enabled = false;
                    this.textBoxFilePath.Enabled = false;
                    this.buttonImport.Enabled = false;
                    this.buttonSetFilePath.Enabled = false;
                    this.backgroundWorkerImport.RunWorkerAsync();
                    //Import_DoWork();
                    FailDT.Rows.Clear();
                }
            }
            else
            {
                this.backgroundWorkerImport.RunWorkerAsync();
                //Import_DoWork();
                this.buttonImport.Enabled = false;
                if (myDT != null)
                    FailDT.Rows.Clear();
            }
        }

        /// <summary>
        /// ����ֶ��Ƿ��޸�
        /// </summary>
        private void FieldExist(bool showOrNot)
        {
            enList.Clear();
            string errorCol = "";
            foreach (CheckTreeNode root in treeDisplayCloumns.Nodes)
            {
                foreach (CheckTreeNode child in root.Nodes)
                {
                    child.ForeColor = Color.Gray;
                    root.ForeColor = Color.Gray;
                }
            }
            for (int i = 0; i < myDT.Columns.Count; i++)
            {
                bool isFind = false;
                foreach (CheckTreeNode root in treeDisplayCloumns.Nodes)
                {
                    foreach (CheckTreeNode child in root.Nodes)
                    {
                        if (child.Text == myDT.Columns[i].ColumnName)
                        {
                            child.ForeColor = Color.Red;
                            root.ForeColor = Color.Red;
                            enList.Add(child.Name);
                            try
                            {
                                myDT.Columns[i].ColumnName = child.Name;
                            }
                            catch
                            {
                                myDT.Columns[i].ColumnName = child.Name + i;
                            }

                            isFind = true;
                            break;
                        }
                    }
                    if (isFind)
                        break;
                }
                if (!isFind)
                {
                    errorCol += "[" + myDT.Columns[i].ColumnName + "]\r\n";
                    enList.Add(myDT.Columns[i].ColumnName);
                }
            }
            if (showOrNot)
                if (errorCol.Length != 0)
                    MessageBox.Show(errorCol + "��ǰѡȡExcel�ļ����������޷���Ϊ��ͬ��¼�ж�����\r\n���������е����ݿ����޷����롣", "��ʾ");
        }

        public void OleDBAccess()
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this.textBoxFilePath.Text + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            OleDbConnection myConn = new OleDbConnection(strConn);
            string sqlstr = "";
            try
            {
                myConn.Open();
                DataTable colsMsg = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                if (colsMsg.Rows.Count == 0)
                {
                    MessageBox.Show("��ǰѡ���ļ������ϵ���ģ���ʽ��������ѡ��", "��ʾ");
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

        public void ImportOneData(DataRow dr)
        {
            int tag=0;
            string UserID = GlobalData.UserInfo.UserID;
            //DataTable RecoredDT = new DataTable();
            DESClass DESClass = new DESClass();
            GlobalData.Clear();
            try
            {
                DataTable RecoredDT = DBAccess.MatchingRecord(dr, treeDisplayCloumns);
                switch (RecoredDT.Rows.Count)
                {
                    case 0:
                        tag = 0;
                        break;
                    case 1:
                        tag = 1;
                        GlobalData.RecordInfo.RecordSeq = Convert.ToInt32(RecoredDT.Rows[0][0]);
                        break;
                    default:
                        //val = RecoredDT.Rows.Count;
                        foreach (DataRow recordRow in RecoredDT.Rows)
                        {
                            if (recordRow["PatName"] != DBNull.Value)
                            {
                                recordRow["PatName"] = DESClass.DESDecrypt(recordRow["PatName"].ToString());
                            }
                            if (recordRow["PatBirthday"] != DBNull.Value)
                            {
                                recordRow["PatBirthday"] = DESClass.DESDecrypt(recordRow["PatBirthday"].ToString());
                            }
                        }
                        ImportTipFrm tip = new ImportTipFrm(RecoredDT);
                        //tip.Show(RecoredDT);
                        if (tip.ShowDialog() == DialogResult.Cancel)
                        {
                            tag = RecoredDT.Rows.Count;
                        }
                        else
                        {
                            GlobalData.RecordInfo.RecordSeq = Convert.ToInt32(tip.RecordSEQ);
                            tag = 1;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (tag == 0)
            {
                //����
                GlobalData.PatBasicInfo.PatSEQ = "*";
                GlobalData.RecordInfo.UserID = UserID;
                for (int i = 0; i < enList.ToArray().Length; i++)
                {
                    DataToGlobalData.FillGlobalData(enList.ToArray()[i], dr);
                }
                DBAccess.SaveDataToDB();
            }
            else if (tag == 1)
            {
                DBAccess.GetDataFromDB();
                for (int i = 0; i < enList.ToArray().Length; i++)
                {
                    if (dr[i].ToString()!=""&& DataToGlobalData.FindingGlobalData(enList.ToArray()[i]) != dr[i].ToString())
                    {
                        DataToGlobalData.FillGlobalData(enList.ToArray()[i], dr);
                    }                   
                }
                DBAccess.SaveDataToDB();
            }
            //else
            //{
            //    //��ʾ�ж��ƥ����
            //    DataRow tmpDR = FailDT.NewRow();
            //    tmpDR.ItemArray = dr.ItemArray;
            //    tmpDR["ʧ��ԭ��"] = "�������㣬���ڿ�����ͬ��¼";
            //    FailDT.Rows.Add(tmpDR);
            //}
        }

        /// <summary>
        /// ������ʾ���
        /// </summary>
        private void DoReasoning()
        {
            GlobalData.DiagnosedResult.Clear();

            List<FunctionTypeDef.EventModels> lstEventModels = new List<FunctionTypeDef.EventModels>();
            CDSSFunction.Interface.ObtainInfernceEvents(ref lstEventModels);
            CDSSFunction.Interface.SetInferenceNeededEvents(lstEventModels);
            CDSSFunction.Interface.InfernceExplanationExecute();

            /*****************************************************************
             * add by zx 20100330
             * ��������ó�����һͬ����ReasoningDiseaseDiagnosedResultList��DiseaseDiagnosedResultList     
             *****************************************************************/
            foreach (CDSSOneDiseaseDiagnosedResult result in GlobalData.DiagnosedResult.DiseaseDiagnosedResultList)
            {
                GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList.Add(result.Clone());
            }

            DBAccess.SaveDataToDB();
        }

        private void backgroundWorkerImport_DoWork(object sender, DoWorkEventArgs e)
        {
            //this.OleDBAccess();
            if (IsExcelSource)
            {
                if (this.myDT != null)
                {
                    OleDBAccess();
                    FieldExist(false);
                }
            }
            else
            {
                myDT = dataSource.Copy();
                for (int i = 0; i < myDT.Columns.Count; i++)
                {
                    bool isFind = false;
                    foreach (CheckTreeNode root in treeDisplayCloumns.Nodes)
                    {
                        foreach (CheckTreeNode child in root.Nodes)
                        {
                            if (child.Text == myDT.Columns[i].ColumnName)
                            {
                                child.ForeColor = Color.Red;
                                root.ForeColor = Color.Red;
                                enList.Add(child.Name);
                                try
                                {
                                    myDT.Columns[i].ColumnName = child.Name;
                                }
                                catch
                                {
                                    myDT.Columns[i].ColumnName = child.Name + i;
                                }

                                isFind = true;
                                break;
                            }
                        }
                        if (isFind)
                            break;
                    }
                    if (!isFind)
                    {
                        enList.Add(myDT.Columns[i].ColumnName);
                    }
                }
            }
            if (myDT == null) { throw new Exception("������������Դ��"); }
            if (myDT.Rows.Count == 0) { throw new Exception("��ǰ�޿ɵ������ݣ�"); }
            if (this.myDT.Rows.Count <= 0)
            {
                throw new Exception("δ�ܵ������ݣ�");
            }
            int successCount = 0;
            if (this.checkBoxReason.Checked)
            {
                foreach (DataRow dr in myDT.Rows)
                {
                    try
                    {
                        this.ImportOneData(dr);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "��ʾ");
                        if (ex.Message == "����������ͬ��¼�ж����ݣ���ѡ���ɫ����ѡ�")
                            throw ex;
                        else
                        {
                            DataRow tmpDR = FailDT.NewRow();
                            tmpDR.ItemArray = dr.ItemArray;
                            tmpDR["ʧ��ԭ��"] = ex.Message;
                            FailDT.Rows.Add(tmpDR);
                        }
                    }
                    this.DoReasoning();
                    successCount++;
                    GlobalData.Clear();
                    this.backgroundWorkerImport.ReportProgress(successCount * 100 / this.myDT.Rows.Count, successCount);
                }
            }
            else
            {
                foreach (DataRow dr in myDT.Rows)
                {
                    try
                    {
                        this.ImportOneData(dr);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "��ʾ");
                        //throw ex;
                        if (ex.Message == "����������ͬ��¼�ж����ݣ���ѡ���ɫ����ѡ�")
                            throw ex;
                        else
                        {
                            DataRow tmpDR = FailDT.NewRow();
                            tmpDR.ItemArray = dr.ItemArray;
                            tmpDR["ʧ��ԭ��"] = ex.Message;
                            FailDT.Rows.Add(tmpDR);
                        }
                    }
                    successCount++;
                    GlobalData.Clear();
                    this.backgroundWorkerImport.ReportProgress(successCount * 100 / this.myDT.Rows.Count, successCount);
                }
            }
        }
        private void Import_DoWork()
        {
            //this.OleDBAccess();
            if (IsExcelSource)
            {
                if (this.myDT != null)
                {
                    OleDBAccess();
                    FieldExist(false);
                }
            }
            else
            {
                myDT = dataSource.Copy();
                for (int i = 0; i < myDT.Columns.Count; i++)
                {
                    bool isFind = false;
                    foreach (CheckTreeNode root in treeDisplayCloumns.Nodes)
                    {
                        foreach (CheckTreeNode child in root.Nodes)
                        {
                            if (child.Text == myDT.Columns[i].ColumnName)
                            {
                                child.ForeColor = Color.Red;
                                root.ForeColor = Color.Red;
                                enList.Add(child.Name);
                                try
                                {
                                    myDT.Columns[i].ColumnName = child.Name;
                                }
                                catch
                                {
                                    myDT.Columns[i].ColumnName = child.Name + i;
                                }

                                isFind = true;
                                break;
                            }
                        }
                        if (isFind)
                            break;
                    }
                    if (!isFind)
                    {
                        enList.Add(myDT.Columns[i].ColumnName);
                    }
                }
            }
            if (myDT == null) { throw new Exception("������������Դ��"); }
            if (myDT.Rows.Count == 0) { throw new Exception("��ǰ�޿ɵ������ݣ�"); }
            if (this.myDT.Rows.Count <= 0)
            {
                throw new Exception("δ�ܵ������ݣ�");
            }
            int successCount = 0;
            if (this.checkBoxReason.Checked)
            {
                foreach (DataRow dr in myDT.Rows)
                {
                    try
                    {
                        this.ImportOneData(dr);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "��ʾ");
                        if (ex.Message == "����������ͬ��¼�ж����ݣ���ѡ���ɫ����ѡ�")
                            throw ex;
                        else
                        {
                            DataRow tmpDR = FailDT.NewRow();
                            tmpDR.ItemArray = dr.ItemArray;
                            tmpDR["ʧ��ԭ��"] = ex.Message;
                            FailDT.Rows.Add(tmpDR);
                        }
                    }
                    this.DoReasoning();
                    successCount++;
                    GlobalData.Clear();
                    this.backgroundWorkerImport.ReportProgress(successCount * 100 / this.myDT.Rows.Count, successCount);
                }
            }
            else
            {
                foreach (DataRow dr in myDT.Rows)
                {
                    try
                    {
                        this.ImportOneData(dr);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "��ʾ");
                        //throw ex;
                        if (ex.Message == "����������ͬ��¼�ж����ݣ���ѡ���ɫ����ѡ�")
                            throw ex;
                        else
                        {
                            DataRow tmpDR = FailDT.NewRow();
                            tmpDR.ItemArray = dr.ItemArray;
                            tmpDR["ʧ��ԭ��"] = ex.Message;
                            FailDT.Rows.Add(tmpDR);
                        }
                    }
                    successCount++;
                    GlobalData.Clear();
                    this.backgroundWorkerImport.ReportProgress(successCount * 100 / this.myDT.Rows.Count, successCount);
                }
            }
        }

        private void backgroundWorkerImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string labelFormat = "{0}/{1}";
            this.progressBarImporting.Value = e.ProgressPercentage;
            int fileNum = int.Parse(e.UserState.ToString());
            this.labelProgress.Text = string.Format(labelFormat, fileNum.ToString(), this.myDT.Rows.Count);
        }

        private void backgroundWorkerImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.checkBoxReason.Enabled = true;
            this.textBoxFilePath.Enabled = true;
            this.buttonImport.Enabled = true;
            this.buttonSetFilePath.Enabled = true;
            if (e.Error != null)
                MessageBox.Show(e.Error.Message, "��ʾ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ExcelControl.CloseExcelFile())
            {
                btnClose.Enabled = btnShowMaxExcel.Enabled = false;
                textBoxFilePath.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InteroperateToExcel frmInterXls = new InteroperateToExcel();
            if (File.Exists(textBoxFilePath.Text))
            {
                if (ExcelControl.CloseExcelFile())
                {
                    frmInterXls.ExcelControl.OpenExcelFile(textBoxFilePath.Text.Trim());
                    frmInterXls.ShowDialog();
                }
            }
            if (frmInterXls.DialogResult == DialogResult.OK)
                ExcelControl.OpenExcelFile(textBoxFilePath.Text);
        }

        private void btnNewExcel_Click(object sender, EventArgs e)
        {
            ExcelControl.NewExcelFile(cnList.ToArray());
            btnClose.Enabled = true;
            textBoxFilePath.Text = "";
        }
    }
}

