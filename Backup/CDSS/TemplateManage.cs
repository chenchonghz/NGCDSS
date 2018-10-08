using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CDSSSystemData;

namespace CDSS
{
    public partial class TemplateManage : Form
    {
        string TempFilePath = Application.StartupPath + "\\UserInfo\\" + GlobalData.UserInfo.UserID;

        DataSet templateDataSet = new DataSet();

        public TemplateManage()
        {
            InitializeComponent();
        }
        public TemplateManage(string temp)
        {
            TempFilePath = temp;
            InitializeComponent();
        }

        public DataSet TemplateDataSet
        {
            get { return templateDataSet; }
        }

        private void TemplateManage_Load(object sender, EventArgs e)
        {
            if (!File.Exists(TempFilePath))
            {
                Directory.CreateDirectory(TempFilePath);
            }
            LoadTemplateMessage();
        }

        /// <summary>
        /// 将模板信息加载到DataGridView中显示
        /// </summary>
        private void LoadTemplateMessage()
        {
            DataTable dt = new DataTable();
            DirectoryInfo dir = new DirectoryInfo(TempFilePath);
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            dt.Columns.Add("tempName");
            dt.Columns.Add("tempRemark");
            dt.Columns.Add("fileName");
            foreach (FileSystemInfo file in files)
            {
                if (file.Extension.ToLower() == ".xml")
                {
                    DataSet ds = new DataSet();

                    ds.ReadXml(file.FullName);
                    if (ds.Tables.Count > 1)
                    {
                        if (ds.Tables["tempInfo"].Rows[0]["tempName"].ToString() != file.Name.Replace(".xml", "")) continue;

                        dt.Rows.Add(ds.Tables["tempInfo"].Rows[0]["tempName"].ToString(),
                            ds.Tables["tempInfo"].Rows[0]["tempRemark"].ToString(),
                            file.FullName);
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["tempName"].ToString() != file.Name.Replace(".xml", "")) continue;

                        dt.Rows.Add(ds.Tables[0].Rows[0]["tempName"].ToString(),
                            ds.Tables[0].Rows[0]["tempRemark"].ToString(),
                            file.FullName);
                    }
                }
            }
            dgrdTemplate.DataSource = dt;
            lblCount.Text = dgrdTemplate.Rows.Count.ToString();
            if (dgrdTemplate.Rows.Count == 0)
                btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认删除模板？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                foreach (DataGridViewRow dr in dgrdTemplate.SelectedRows)
                {
                    File.Delete(dr.Cells["隐藏列"].Value.ToString());
                    dgrdTemplate.Rows.Remove(dr);
                }
            }
            lblCount.Text = dgrdTemplate.Rows.Count.ToString();
            if (dgrdTemplate.Rows.Count == 0)
                btnDelete.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgrdTemplate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddSelectTemplate FrmTempMsg = new AddSelectTemplate();

            FrmTempMsg.Text = "修改模板信息";
            FrmTempMsg.txtTemplateName.Text = dgrdTemplate.SelectedRows[0].Cells["模板名称"].Value.ToString();
            FrmTempMsg.txtRemark.Text = dgrdTemplate.SelectedRows[0].Cells["模板描述"].Value.ToString();
            FrmTempMsg.FilePath = TempFilePath + "\\";
            FrmTempMsg.ShowDialog();

            if (DialogResult.OK == FrmTempMsg.DialogResult)
            {
                dgrdTemplate.SelectedRows[0].Cells["模板名称"].Value = FrmTempMsg.txtTemplateName.Text;
                dgrdTemplate.SelectedRows[0].Cells["模板描述"].Value = FrmTempMsg.txtRemark.Text;
                dgrdTemplate.SelectedRows[0].Tag = "change";//标记此行已修改
                DataSet ds = new DataSet();
                DataGridViewRow dr = dgrdTemplate.Rows[e.RowIndex];

                ds.ReadXml(dr.Cells["隐藏列"].Value.ToString());//读取原有文件
                if (ds.Tables.Count > 1)
                {
                    ds.Tables["tempInfo"].Rows[0]["tempName"] = dr.Cells["模板名称"].Value.ToString();
                    ds.Tables["tempInfo"].Rows[0]["tempRemark"] = dr.Cells["模板描述"].Value.ToString();
                }
                else
                {
                    ds.Tables[0].Rows[0]["tempName"] = dr.Cells["模板名称"].Value.ToString();
                    ds.Tables[0].Rows[0]["tempRemark"] = dr.Cells["模板描述"].Value.ToString();
                }
                File.Delete(dr.Cells["隐藏列"].Value.ToString());//删除原有文件
                ds.WriteXml(TempFilePath + "\\" + dr.Cells["模板名称"].Value.ToString() + ".xml");//保存新文件
                dgrdTemplate.SelectedRows[0].Cells["隐藏列"].Value = Path.GetDirectoryName(dgrdTemplate.SelectedRows[0].Cells["隐藏列"].Value.ToString())
                    + "\\" + FrmTempMsg.txtTemplateName.Text + ".xml";
            }
            LoadTemplateMessage();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if(dgrdTemplate.SelectedRows.Count==0)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                foreach (DataGridViewRow dr in dgrdTemplate.SelectedRows)
                {
                    //File.Delete(dr.Cells["隐藏列"].Value.ToString());
                    //dgrdTemplate.Rows.Remove(dr);
                    DataSet ds = new DataSet();
                    string fileName = dr.Cells["隐藏列"].Value.ToString();
                    ds.ReadXml(fileName);
                    templateDataSet = ds;
                }
            }
            this.Close();
        }
    }
}