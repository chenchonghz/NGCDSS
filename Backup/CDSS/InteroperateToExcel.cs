using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDSS
{
    public partial class InteroperateToExcel : Form
    {
        string DataSourceExcel = "";
        public InteroperateToExcel()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                DataSourceExcel = openFileDialog1.FileName;
                ExcelControl.OpenExcelFile(DataSourceExcel);
                Text = String.Format("Excel文件 — {0}", System.IO.Path.GetFileName(DataSourceExcel));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataSourceExcel = "";
            ExcelControl.CloseExcelFile();
            Text = "Excel文件";
        }

        private void InteroperateToExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            ExcelControl.CloseExcelFile();
            //ExcelControl.CloseExcelApplication();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DataSourceExcel == "")
            {
                if (DialogResult.OK == openFileDialog1.ShowDialog())
                    DataSourceExcel = openFileDialog1.FileName;
            }

        }
    }
}