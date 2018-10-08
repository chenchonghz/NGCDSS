namespace CDSSCtrlLib
{
    partial class FamilyDiseaseHistory
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
            this.lblDiseaseName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDiseaseName
            // 
            this.lblDiseaseName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(239)))));
            this.lblDiseaseName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiseaseName.Font = new System.Drawing.Font("ËÎÌå", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiseaseName.Image = global::CDSSCtrlLib.Properties.Resources.DiagnosisSteps_normal;
            this.lblDiseaseName.Location = new System.Drawing.Point(0, 0);
            this.lblDiseaseName.Name = "lblDiseaseName";
            this.lblDiseaseName.Size = new System.Drawing.Size(91, 27);
            this.lblDiseaseName.TabIndex = 0;
            this.lblDiseaseName.Text = "¼²²¡Ãû³Æ";
            this.lblDiseaseName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDiseaseName.MouseLeave += new System.EventHandler(this.lblDiseaseName_MouseLeave);
            this.lblDiseaseName.Click += new System.EventHandler(this.lblDiseaseName_Click);
            this.lblDiseaseName.MouseEnter += new System.EventHandler(this.lblDiseaseName_MouseEnter);
            // 
            // FamilyDiseaseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDiseaseName);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "FamilyDiseaseHistory";
            this.Size = new System.Drawing.Size(91, 27);
            this.Leave += new System.EventHandler(this.FamilyDiseaseHistory_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDiseaseName;
    }
}
