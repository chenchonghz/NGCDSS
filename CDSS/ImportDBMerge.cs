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
        //Ҫ���뵽�µ����ݿ��е����ݼ�
        DataTable m_dtBeingInsertedItems;

        private string m_newDataDirectory;

        private int successCount = 0;

        public ImportDBMerge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ѡ��Ҫ�ϲ������ݿ��ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OpenNewDB_Click(object sender, EventArgs e)
        {
            this.openFileDialogNewDB.Filter = "���ݿ��ļ�|*.db";
            if (this.openFileDialogNewDB.ShowDialog() == DialogResult.OK)
            {
                this.txt_NewDB.Text = this.openFileDialogNewDB.FileName;                
            }
        }

        /// <summary>
        /// ѡ��Ҫ���������ݿ��ŵ�·�����ļ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OpenExportPath_Click(object sender, EventArgs e)
        {
            this.saveDBFileDialog.Filter = "���ݿ��ļ�|*.db";
            if (this.saveDBFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txt_PathWay.Text = this.saveDBFileDialog.FileName;               
            }
        }
        
        /// <summary>
        /// ����ϲ���ť�����Ӧ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (txt_NewDB.Text=="")
            {
                MessageBox.Show("��ѡ��Ҫ������ļ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //���ϲ���ť����Ϊ���ɵ��
            this.btnMerge.Enabled = false;

            //��ȡ����ť����Ϊ�ɵ��
            this.btn_Cancel.Enabled = true;

            //��listbox�е��������
            this.ListBoxMerge.Items.Clear();

            //���µ����ݿ��·��������Ա����
            this.m_newDataDirectory = this.txt_NewDB.Text.ToString().Trim();

            string oldDataDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "NGCDSS.db";
            //���ݿⱸ��
            string backup_Directory = System.AppDomain.CurrentDomain.BaseDirectory + "NGCDSS_bak.db";
            System.IO.File.Copy(oldDataDirectory, backup_Directory, true);

            //���ɵ����ݿ��еļ�¼���������������ڶ�ȡ��һ�ű���
            DataTable dataFromOldDB = DBAccess.GetNameAndBirthdayRecordSeqDataFromDB(); 

       
            //�����ݿ���������Ϊ�µ�����Դ��ַ
            DBAccess.SetDirectory(m_newDataDirectory);

            //�õ�������Դ�����е��������������ں�RECORDSEQ
            DataTable dtNameAndBirthdayNew = DBAccess.GetNameAndBirthdayRecordSeqDataFromDB();

            ////�����ݿ���������Ϊ�ɵ�����Դ��ַ
            //DBAccess.SetDirectory(oldDataDirectory);
            DataTable dtBeingInsertedItems = new DataTable();
            dtBeingInsertedItems = dtNameAndBirthdayNew.Clone();            
            //���������ͳ������ڽ��в����Ƿ����µĲ������ݣ����У����������һ���±���
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
            string listItem = "����" + dtBeingInsertedItems.Rows.Count+"����¼�����롭";

            ListBoxMerge.Items.Add(listItem);

            this.m_dtBeingInsertedItems = dtBeingInsertedItems;
            
            this.backgroundWorkerDBMerge.RunWorkerAsync();
        }
        
        private void btn_ExportDB_Click(object sender, EventArgs e)
        {
            string sourceFile = System.AppDomain.CurrentDomain.BaseDirectory + "NGCDSS.db";
            this.saveDBFileDialog.Filter = "���ݿ��ļ�|*.db";
            if (this.saveDBFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txt_PathWay.Text = this.saveDBFileDialog.FileName;

                System.IO.File.Copy(sourceFile, this.saveDBFileDialog.FileName,false);
                string announceStr = "���ݵ�����" + this.saveDBFileDialog.FileName + "�ɹ���";
                MessageBox.Show(announceStr, "����", MessageBoxButtons.OK);
            }
        }
        private void backgroundWorkerDBMerge_DoWork(object sender, DoWorkEventArgs e)
        {
            string oldDataDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "NGCDSS.db";
            classSQLServerDBInterface anotherSQLServerDBInterface = new classSQLServerDBInterface(this.m_newDataDirectory);
            //�±��е�recordseq��Ӧ��ÿ�ű��еĸ��ֶ�
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
            string listItem = "�ɹ�����" + successCount + "����¼��";
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