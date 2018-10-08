using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDSS
{
    public partial class ImportTipFrm : Form
    {
        private string RecordSeq;
        public ImportTipFrm(DataTable dt)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }
        public string RecordSEQ
        {
            get{return RecordSeq;}
        }


        //public void Show(DataTable dt)
        //{
        //    dataGridView1.AutoGenerateColumns = false;
        //    dataGridView1.DataSource = dt;
        //    //this.Show();
        //}

        private void Select_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一条记录！");
                return;
            }
            else
                RecordSeq = dataGridView1.SelectedRows[0].Cells["InvisibleCol"].Value.ToString();
            this.DialogResult = DialogResult.OK; 
        }
        private void ImportTipFrm_Load(object sender, EventArgs e)
        {

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;  
        }
    }
}