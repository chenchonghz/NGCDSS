namespace CDSSCtrlLib
{
    partial class DiagnosisResult
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblShowDiagnosisSteps = new System.Windows.Forms.Label();
            this.lblShowDetail = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtxTreatmentTarget = new CDSSCtrlLib.CDSSRichTextBox();
            this.rtxTreatmentSuggestion = new CDSSCtrlLib.CDSSRichTextBox();
            this.rtxRemark = new CDSSCtrlLib.CDSSRichTextBox();
            this.pnlSummary.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.pnlSummary.Controls.Add(this.lblResult);
            this.pnlSummary.Controls.Add(this.lblShowDiagnosisSteps);
            this.pnlSummary.Controls.Add(this.lblShowDetail);
            this.pnlSummary.Controls.Add(this.lblName);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(4, 4);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(665, 26);
            this.pnlSummary.TabIndex = 0;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.lblResult.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.Location = new System.Drawing.Point(90, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(488, 26);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "2型糖尿病";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShowDiagnosisSteps
            // 
            this.lblShowDiagnosisSteps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowDiagnosisSteps.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblShowDiagnosisSteps.Image = global::CDSSCtrlLib.Properties.Resources.DiagnosisSteps_normal;
            this.lblShowDiagnosisSteps.Location = new System.Drawing.Point(577, 0);
            this.lblShowDiagnosisSteps.Name = "lblShowDiagnosisSteps";
            this.lblShowDiagnosisSteps.Size = new System.Drawing.Size(88, 26);
            this.lblShowDiagnosisSteps.TabIndex = 3;
            this.lblShowDiagnosisSteps.Text = "诊断过程";
            this.lblShowDiagnosisSteps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblShowDiagnosisSteps.MouseLeave += new System.EventHandler(this.lblShowDiagnosisSteps_MouseLeave);
            this.lblShowDiagnosisSteps.Click += new System.EventHandler(this.lblShowDiagnosisSteps_Click);
            this.lblShowDiagnosisSteps.MouseEnter += new System.EventHandler(this.lblShowDiagnosisSteps_MouseEnter);
            // 
            // lblShowDetail
            // 
            this.lblShowDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.lblShowDetail.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblShowDetail.Image = global::CDSSCtrlLib.Properties.Resources.Shrink_disable;
            this.lblShowDetail.Location = new System.Drawing.Point(65, 0);
            this.lblShowDetail.Name = "lblShowDetail";
            this.lblShowDetail.Size = new System.Drawing.Size(23, 26);
            this.lblShowDetail.TabIndex = 1;
            this.lblShowDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblShowDetail.MouseLeave += new System.EventHandler(this.lblShowDetail_MouseLeave);
            this.lblShowDetail.Click += new System.EventHandler(this.lblShowDetail_Click);
            this.lblShowDetail.MouseEnter += new System.EventHandler(this.lblShowDetail_MouseEnter);
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblName.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.Red;
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(65, 26);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "糖代谢";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.pnlDetail.Controls.Add(this.tableLayoutPanel1);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(4, 30);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(665, 138);
            this.pnlDetail.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(665, 138);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rtxTreatmentTarget);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(89, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(2);
            this.panel3.Size = new System.Drawing.Size(187, 132);
            this.panel3.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(220)))), ((int)(((byte)(235)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(2, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 30);
            this.label5.TabIndex = 0;
            this.label5.Text = "治疗目标：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rtxTreatmentSuggestion);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(282, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(2);
            this.panel4.Size = new System.Drawing.Size(187, 132);
            this.panel4.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(220)))), ((int)(((byte)(235)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "治疗建议：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rtxRemark);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(475, 3);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(2);
            this.panel5.Size = new System.Drawing.Size(187, 132);
            this.panel5.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(220)))), ((int)(((byte)(235)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(2, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(183, 30);
            this.label9.TabIndex = 0;
            this.label9.Text = "备注：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 132);
            this.panel1.TabIndex = 3;
            // 
            // rtxTreatmentTarget
            // 
            this.rtxTreatmentTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(220)))), ((int)(((byte)(235)))));
            this.rtxTreatmentTarget.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxTreatmentTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxTreatmentTarget.Location = new System.Drawing.Point(2, 32);
            this.rtxTreatmentTarget.Name = "rtxTreatmentTarget";
            this.rtxTreatmentTarget.ReadOnly = true;
            this.rtxTreatmentTarget.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxTreatmentTarget.Size = new System.Drawing.Size(183, 98);
            this.rtxTreatmentTarget.TabIndex = 1;
            this.rtxTreatmentTarget.Text = "血糖：\n空腹 < 6.2 mmol/L\n餐后2h < 8.0 mmol/L\nHbA1c < 6.5%";
            // 
            // rtxTreatmentSuggestion
            // 
            this.rtxTreatmentSuggestion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(220)))), ((int)(((byte)(235)))));
            this.rtxTreatmentSuggestion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxTreatmentSuggestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxTreatmentSuggestion.Location = new System.Drawing.Point(2, 32);
            this.rtxTreatmentSuggestion.Name = "rtxTreatmentSuggestion";
            this.rtxTreatmentSuggestion.ReadOnly = true;
            this.rtxTreatmentSuggestion.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxTreatmentSuggestion.Size = new System.Drawing.Size(183, 98);
            this.rtxTreatmentSuggestion.TabIndex = 2;
            this.rtxTreatmentSuggestion.Text = "降糖药物：二甲双胍 0.5 3次/日\n盐酸吡格列酮 15mg 1次/日";
            // 
            // rtxRemark
            // 
            this.rtxRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(220)))), ((int)(((byte)(235)))));
            this.rtxRemark.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxRemark.Location = new System.Drawing.Point(2, 32);
            this.rtxRemark.Name = "rtxRemark";
            this.rtxRemark.ReadOnly = true;
            this.rtxRemark.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxRemark.Size = new System.Drawing.Size(183, 98);
            this.rtxRemark.TabIndex = 3;
            this.rtxRemark.Text = "自我血糖监测 > 2次/日\n自我血糖监测 > 2次/日\n自我血糖监测 > 2次/日\n自我血糖监测 > 2次/日\n自我血糖监测 > 2次/日\n自我血糖监测 > 2次" +
                "/日\n自我血糖监测 > 2次/日\n自我血糖监测 > 2次/日\n自我血糖监测 > 2次/日\n自我血糖监测 > 2次/日";
            // 
            // DiagnosisResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.pnlDetail);
            this.Controls.Add(this.pnlSummary);
            this.DoubleBuffered = true;
            this.Name = "DiagnosisResult";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(673, 172);
            this.Load += new System.EventHandler(this.DiagnosisResult_Load);
            this.pnlSummary.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblShowDiagnosisSteps;
        private System.Windows.Forms.Label lblShowDetail;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private CDSSRichTextBox rtxTreatmentTarget;
        private CDSSRichTextBox rtxTreatmentSuggestion;
        private CDSSRichTextBox rtxRemark;


    }
}
