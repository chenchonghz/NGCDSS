using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDSS
{
    public partial class ProgressForm : Form
    {
        private int Milestone;

        public ProgressForm()
        {
            InitializeComponent();
            this.Cursor = Cursors.WaitCursor;
        }

        public void ShowProgressWindow(Form Owner)
        {
            this.Show(Owner);
        }

        public void HideProgressWindow()
        {
            this.Hide();
        }

        public void SetMilestone(int nMilestone)
        {
            this.Milestone = nMilestone;
        }

        
        public void SetDetailInfo(string DetailInfo)
        {
            this.lblDetailInfo.Text = DetailInfo;
        }


        private void ProgressForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (this.Parent != null)
                    this.Parent.Enabled = false;
                this.lblDetailInfo.Text = "«Î…‘∫Ú";
                this.progressBar1.Value = 0;
                this.Milestone = 0;
                this.timer1.Enabled = true;
            }
            else
            {
                if (this.Parent != null)
                    this.Parent.Enabled = true;                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;

            if (this.progressBar1.Value == this.progressBar1.Maximum)
                this.Visible = false;
                
            if (this.progressBar1.Value < this.Milestone)
                this.progressBar1.PerformStep();

            this.timer1.Enabled = true;
        }
    }
}