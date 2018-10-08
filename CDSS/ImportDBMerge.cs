using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSDBAccess;

namespace CDSS
{
    public partial class ImportDBMerge : Form
    {
        //要插入到新的数据库中的数据集
        DataTable m_dtBeingInsertedItems;

        private string m_newDataDirectory;

        private int successCount = 0;

        public ImportDBMerge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择要合并的数据库文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OpenNewDB_Click(object sender, EventArgs e)
        {
            this.openFileDialogNewDB.Filter = "数据库文件|*.db";
            if (this.openFileDialogNewDB.ShowDialog() == DialogResult.OK)
            {
                this.txt_NewDB.Text = this.openFileDialogNewDB.FileName;                
            }
        }

        /// <summary>
        /// 选择要导出的数据库存放的路径及文件名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OpenExportPath_Click(object sender, EventArgs e)
        {
            this.saveDBFileDialog.Filter = "数据库文件|*.db";
            if (this.saveDBFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txt_PathWay.Text = this.saveDBFileDialog.FileName;               
            }
        }
        
        /// <summary>
        /// 点击合并按钮后的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (txt_NewDB.Text=="")
            {
                MessageBox.Show("请选择要导入的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //将合并按钮设置为不可点击
            this.btnMerge.Enabled = false;

            //将取消按钮设置为可点击
            this.btn_Cancel.Enabled = true;

            //将listbox中的数据清空
            this.ListBoxMerge.Items.Clear();

            //将新的数据库的路径赋给成员变量
            this.m_newDataDirectory = this.txt_NewDB.Text.ToString().Trim();

            string oldDataDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "NGCDSS.db";
            //数据库备份
            string backup_Directory = System.AppDomain.CurrentDomain.BaseDirectory + "NGCDSS_bak.db";
            System.IO.File.Copy(oldDataDirectory, backup_Directory, true);

            //将旧的数据库中的记录的姓名、出生日期读取到一张表中
            DataTable dataFromOldDB = DBAccess.GetNameAndBirthdayRecordSeqDataFromDB(); 

       
            //将数据库连接设置为新的数据源地址
            DBAccess.SetDirectory(m_newDataDirectory);

            //得到新数据源中所有的姓名、出生日期和RECORDSEQ
            DataTable dtNameAndBirthdayNew = DBAccess.GetNameAndBirthdayRecordSeqDataFromDB();

            ////将数据库连接设置为旧的数据源地址
            //DBAccess.SetDirectory(oldDataDirectory);
            DataTable dtBeingInsertedItems = new DataTable();
            dtBeingInsertedItems = dtNameAndBirthdayNew.Clone();            
            //根据姓名和出生日期进行查找是否有新的病人数据，若有，则将其放置在一张新表中
            foreach (DataRow r in dtNameAndBirthdayNew.Rows)
            {
                foreach (DataRow rn in dataFromOldDB.Rows)
                {
                    if ((r["PatName"] != rn["PatName"] || r["PatBirthday"] != rn["PatBirthday"]))
                    {
                        continue;
                    }
                    dataFromOldDB.Rows.Remove(rn);
                    break;
                }
                dtBeingInsertedItems.Rows.Add(r.ItemArray);
            }
            DBAccess.SetDirectory("");
            string listItem = "共有" + dtBeingInsertedItems.Rows.Count+"条记录将插入…";

            ListBoxMerge.Items.Add(listItem);

            this.m_dtBeingInsertedItems = dtBeingInsertedItems;
            
            this.backgroundWorkerDBMerge.RunWorkerAsync();
        }
        
        private void btn_ExportDB_Click(object sender, EventArgs e)
        {
            string sourceFile = System.AppDomain.CurrentDomain.BaseDirectory + "NGCDSS.db";
            this.saveDBFileDialog.Filter = "数据库文件|*.db";
            if (this.saveDBFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txt_PathWay.Text = this.saveDBFileDialog.FileName;

                System.IO.File.Copy(sourceFile, this.saveDBFileDialog.FileName,false);
                string announceStr = "数据导出到" + this.saveDBFileDialog.FileName + "成功！";
                MessageBox.Show(announceStr, "提醒", MessageBoxButtons.OK);
            }
        }
        private void backgroundWorkerDBMerge_DoWork(object sender, DoWorkEventArgs e)
        {
            string oldDataDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "NGCDSS.db";
            classSQLServerDBInterface anotherSQLServerDBInterface = new classSQLServerDBInterface(this.m_newDataDirectory);
            //新表中的recordseq对应于每张表中的该字段
            successCount = 0;
            foreach (DataRow dr in this.m_dtBeingInsertedItems.Rows)
            {
                if (this.backgroundWorkerDBMerge.CancellationPending==true)
                {
                    return;
                }
                DBAccess.SetGlobalRecordSEQ(int.Parse(dr["RecordSEQ"].ToString()));
                DBAccess.GetDataFromDB(anotherSQLServerDBInterface);
                DBAccess.SetGlobalPatSEQToNew();
                DBAccess.SetGlobalRecordSEQToNew();
                if (DBAccess.SaveDataToDB())
                {
                    successCount++;
                }
                this.backgroundWorkerDBMerge.ReportProgress(successCount * 100 / this.m_dtBeingInsertedItems.Rows.Count, successCount);
                //DBAccess.CloseDB();
            }
        }

        private void backgroundWorkerDBMerge_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string labelFormat = "{0}/{1}";
            this.progressBar1.Value = e.ProgressPercentage;
            int fileNum = int.Parse(e.UserState.ToString());
            this.label3.Text = string.Format(labelFormat, fileNum.ToString(), this.m_dtBeingInsertedItems.Rows.Count);
        }

        private void backgroundWorkerDBMerge_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string listItem = "成功插入" + successCount + "条记录。";
            ListBoxMerge.Items.Add(listItem);
            this.btnMerge.Enabled = true;
            this.btn_Cancel.Enabled = false;
            DBAccess.SetDirectory("");

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.backgroundWorkerDBMerge.CancelAsync();
        }
    }
}