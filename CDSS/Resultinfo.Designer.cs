namespace CDSS
{
    partial class Resultinfo
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resultinfo));
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblReReason = new System.Windows.Forms.Label();
            this.lblRiskDegree = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRiskDegreeCode = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlResultDetail = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.diagnosisResult1 = new CDSSCtrlLib.DiagnosisResult();
            this.diagnosisResult7 = new CDSSCtrlLib.DiagnosisResult();
            this.diagnosisResult2 = new CDSSCtrlLib.DiagnosisResult();
            this.diagnosisResult6 = new CDSSCtrlLib.DiagnosisResult();
            this.diagnosisResult3 = new CDSSCtrlLib.DiagnosisResult();
            this.diagnosisResult5 = new CDSSCtrlLib.DiagnosisResult();
            this.diagnosisResult4 = new CDSSCtrlLib.DiagnosisResult();
            this.pnlTitle.SuspendLayout();
            this.pnlResultDetail.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTitle.BackgroundImage")));
            this.pnlTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTitle.Controls.Add(this.lblRiskDegreeCode);
            this.pnlTitle.Controls.Add(this.lblRiskDegree);
            this.pnlTitle.Controls.Add(this.lblReReason);
            this.pnlTitle.Controls.Add(this.lblResult);
            this.pnlTitle.Controls.Add(this.label1);
            this.pnlTitle.Controls.Add(this.label5);
            this.pnlTitle.Controls.Add(this.label3);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(15, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(900, 41);
            this.pnlTitle.TabIndex = 1;
            // 
            // lblReReason
            // 
            this.lblReReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReReason.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblReReason.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReReason.Image = global::CDSS.Properties.Resources.Button_normal;
            this.lblReReason.Location = new System.Drawing.Point(784, 5);
            this.lblReReason.Name = "lblReReason";
            this.lblReReason.Size = new System.Drawing.Size(92, 32);
            this.lblReReason.TabIndex = 6;
            this.lblReReason.Text = "结论刷新";
            this.lblReReason.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblReReason.MouseLeave += new System.EventHandler(this.lblReReason_MouseLeave);
            this.lblReReason.Click += new System.EventHandler(this.lblReReason_Click);
            this.lblReReason.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblReReason_MouseDown);
            this.lblReReason.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblReReason_MouseUp);
            this.lblReReason.MouseEnter += new System.EventHandler(this.lblReReason_MouseEnter);
            // 
            // lblRiskDegree
            // 
            this.lblRiskDegree.AutoSize = true;
            this.lblRiskDegree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.lblRiskDegree.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRiskDegree.Location = new System.Drawing.Point(518, 13);
            this.lblRiskDegree.Name = "lblRiskDegree";
            this.lblRiskDegree.Size = new System.Drawing.Size(40, 16);
            this.lblRiskDegree.TabIndex = 5;
            this.lblRiskDegree.Text = "轻微";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.lblResult.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.Location = new System.Drawing.Point(114, 13);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(24, 16);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "有";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(453, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "危险度：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRiskDegreeCode
            // 
            this.lblRiskDegreeCode.AutoSize = true;
            this.lblRiskDegreeCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.lblRiskDegreeCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRiskDegreeCode.Location = new System.Drawing.Point(371, 13);
            this.lblRiskDegreeCode.Name = "lblRiskDegreeCode";
            this.lblRiskDegreeCode.Size = new System.Drawing.Size(16, 16);
            this.lblRiskDegreeCode.TabIndex = 1;
            this.lblRiskDegreeCode.Text = "5";
            this.lblRiskDegreeCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(192, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "代谢综合征危险度积分：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "代谢综合征：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlResultDetail
            // 
            this.pnlResultDetail.AutoScroll = true;
            this.pnlResultDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.pnlResultDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlResultDetail.Controls.Add(this.tableLayoutPanel1);
            this.pnlResultDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultDetail.Location = new System.Drawing.Point(15, 41);
            this.pnlResultDetail.Name = "pnlResultDetail";
            this.pnlResultDetail.Padding = new System.Windows.Forms.Padding(15);
            this.pnlResultDetail.Size = new System.Drawing.Size(900, 501);
            this.pnlResultDetail.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.diagnosisResult1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.diagnosisResult7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.diagnosisResult2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.diagnosisResult6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.diagnosisResult3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.diagnosisResult5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.diagnosisResult4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(870, 266);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // diagnosisResult1
            // 
            this.diagnosisResult1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.diagnosisResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagnosisResult1.Location = new System.Drawing.Point(3, 3);
            this.diagnosisResult1.Name = "diagnosisResult1";
            this.diagnosisResult1.Padding = new System.Windows.Forms.Padding(3);
            this.diagnosisResult1.Size = new System.Drawing.Size(864, 32);
            this.diagnosisResult1.TabIndex = 0;
            this.diagnosisResult1.ShowDiagnosisSteps += new CDSSCtrlLib.ShowDiagnosisStepsEventHandler(this.DiagnosisResult_ShowDiagnosisSteps);
            // 
            // diagnosisResult7
            // 
            this.diagnosisResult7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.diagnosisResult7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagnosisResult7.Location = new System.Drawing.Point(3, 231);
            this.diagnosisResult7.Name = "diagnosisResult7";
            this.diagnosisResult7.Padding = new System.Windows.Forms.Padding(3);
            this.diagnosisResult7.Size = new System.Drawing.Size(864, 32);
            this.diagnosisResult7.TabIndex = 6;
            this.diagnosisResult7.ShowDiagnosisSteps += new CDSSCtrlLib.ShowDiagnosisStepsEventHandler(this.DiagnosisResult_ShowDiagnosisSteps);
            // 
            // diagnosisResult2
            // 
            this.diagnosisResult2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.diagnosisResult2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagnosisResult2.Location = new System.Drawing.Point(3, 41);
            this.diagnosisResult2.Name = "diagnosisResult2";
            this.diagnosisResult2.Padding = new System.Windows.Forms.Padding(3);
            this.diagnosisResult2.Size = new System.Drawing.Size(864, 32);
            this.diagnosisResult2.TabIndex = 1;
            this.diagnosisResult2.ShowDiagnosisSteps += new CDSSCtrlLib.ShowDiagnosisStepsEventHandler(this.DiagnosisResult_ShowDiagnosisSteps);
            // 
            // diagnosisResult6
            // 
            this.diagnosisResult6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.diagnosisResult6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagnosisResult6.Location = new System.Drawing.Point(3, 193);
            this.diagnosisResult6.Name = "diagnosisResult6";
            this.diagnosisResult6.Padding = new System.Windows.Forms.Padding(3);
            this.diagnosisResult6.Size = new System.Drawing.Size(864, 32);
            this.diagnosisResult6.TabIndex = 5;
            this.diagnosisResult6.ShowDiagnosisSteps += new CDSSCtrlLib.ShowDiagnosisStepsEventHandler(this.DiagnosisResult_ShowDiagnosisSteps);
            // 
            // diagnosisResult3
            // 
            this.diagnosisResult3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.diagnosisResult3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagnosisResult3.Location = new System.Drawing.Point(3, 79);
            this.diagnosisResult3.Name = "diagnosisResult3";
            this.diagnosisResult3.Padding = new System.Windows.Forms.Padding(3);
            this.diagnosisResult3.Size = new System.Drawing.Size(864, 32);
            this.diagnosisResult3.TabIndex = 2;
            this.diagnosisResult3.ShowDiagnosisSteps += new CDSSCtrlLib.ShowDiagnosisStepsEventHandler(this.DiagnosisResult_ShowDiagnosisSteps);
            // 
            // diagnosisResult5
            // 
            this.diagnosisResult5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.diagnosisResult5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagnosisResult5.Location = new System.Drawing.Point(3, 155);
            this.diagnosisResult5.Name = "diagnosisResult5";
            this.diagnosisResult5.Padding = new System.Windows.Forms.Padding(3);
            this.diagnosisResult5.Size = new System.Drawing.Size(864, 32);
            this.diagnosisResult5.TabIndex = 4;
            this.diagnosisResult5.ShowDiagnosisSteps += new CDSSCtrlLib.ShowDiagnosisStepsEventHandler(this.DiagnosisResult_ShowDiagnosisSteps);
            // 
            // diagnosisResult4
            // 
            this.diagnosisResult4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.diagnosisResult4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagnosisResult4.Location = new System.Drawing.Point(3, 117);
            this.diagnosisResult4.Name = "diagnosisResult4";
            this.diagnosisResult4.Padding = new System.Windows.Forms.Padding(3);
            this.diagnosisResult4.Size = new System.Drawing.Size(864, 32);
            this.diagnosisResult4.TabIndex = 3;
            this.diagnosisResult4.ShowDiagnosisSteps += new CDSSCtrlLib.ShowDiagnosisStepsEventHandler(this.DiagnosisResult_ShowDiagnosisSteps);
            // 
            // Resultinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(930, 557);
            this.Controls.Add(this.pnlResultDetail);
            this.Controls.Add(this.pnlTitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Resultinfo";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.Text = "诊断结论";
            this.Load += new System.EventHandler(this.Resultinfo_Load);
            this.VisibleChanged += new System.EventHandler(this.Resultinfo_VisibleChanged);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlResultDetail.ResumeLayout(false);
            this.pnlResultDetail.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Panel pnlResultDetail;
        private System.Windows.Forms.Label lblRiskDegree;
        private System.Windows.Forms.Label lblReReason;
        private CDSSCtrlLib.DiagnosisResult diagnosisResult3;
        private CDSSCtrlLib.DiagnosisResult diagnosisResult2;
        private CDSSCtrlLib.DiagnosisResult diagnosisResult1;
        private CDSSCtrlLib.DiagnosisResult diagnosisResult7;
        private CDSSCtrlLib.DiagnosisResult diagnosisResult6;
        private CDSSCtrlLib.DiagnosisResult diagnosisResult5;
        private CDSSCtrlLib.DiagnosisResult diagnosisResult4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRiskDegreeCode;
        private System.Windows.Forms.Label label3;
    }
}