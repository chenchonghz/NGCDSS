namespace CDSS
{
    partial class LipidsDisorder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LipidsDisorder));
            this.panel_LD = new System.Windows.Forms.Panel();
            this.panel_DrugType = new System.Windows.Forms.Panel();
            this.medicineControl_tzy = new CDSSCtrlLib.MedicineControl();
            this.panel_DrugYN = new System.Windows.Forms.Panel();
            this.rbt_DrugN = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rbt_DrugY = new System.Windows.Forms.RadioButton();
            this.panel_LDYN = new System.Windows.Forms.Panel();
            this.dateTimePicker_LDDate = new CDSSCtrlLib.DateTimeControl();
            this.panel_LDType = new System.Windows.Forms.Panel();
            this.checkBox_TC = new System.Windows.Forms.CheckBox();
            this.checkBox_HDLC = new System.Windows.Forms.CheckBox();
            this.checkBox_LDLC = new System.Windows.Forms.CheckBox();
            this.checkBox_TG = new System.Windows.Forms.CheckBox();
            this.rbt_LDN = new System.Windows.Forms.RadioButton();
            this.rbt_LDY = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_LD.SuspendLayout();
            this.panel_DrugType.SuspendLayout();
            this.panel_DrugYN.SuspendLayout();
            this.panel_LDYN.SuspendLayout();
            this.panel_LDType.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_LD
            // 
            this.panel_LD.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_LD.BackgroundImage")));
            this.panel_LD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_LD.Controls.Add(this.panel_DrugType);
            this.panel_LD.Controls.Add(this.panel_DrugYN);
            this.panel_LD.Controls.Add(this.panel_LDYN);
            this.panel_LD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LD.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel_LD.Location = new System.Drawing.Point(10, 10);
            this.panel_LD.Name = "panel_LD";
            this.panel_LD.Size = new System.Drawing.Size(743, 463);
            this.panel_LD.TabIndex = 0;
            // 
            // panel_DrugType
            // 
            this.panel_DrugType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_DrugType.BackgroundImage")));
            this.panel_DrugType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_DrugType.Controls.Add(this.medicineControl_tzy);
            this.panel_DrugType.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_DrugType.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel_DrugType.Location = new System.Drawing.Point(0, 128);
            this.panel_DrugType.Name = "panel_DrugType";
            this.panel_DrugType.Size = new System.Drawing.Size(743, 267);
            this.panel_DrugType.TabIndex = 2;
            // 
            // medicineControl_tzy
            // 
            this.medicineControl_tzy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.medicineControl_tzy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.medicineControl_tzy.Location = new System.Drawing.Point(0, 0);
            this.medicineControl_tzy.MedicineControlBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.medicineControl_tzy.MedicineControlColumnheadersBackColor = System.Drawing.SystemColors.Control;
            this.medicineControl_tzy.MedicineControlFont = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.medicineControl_tzy.MedicineControlRowheadersBackColor = System.Drawing.SystemColors.Control;
            this.medicineControl_tzy.MedicineControlSelectedRowColor = System.Drawing.Color.Silver;
            this.medicineControl_tzy.Name = "medicineControl_tzy";
            this.medicineControl_tzy.Size = new System.Drawing.Size(721, 133);
            this.medicineControl_tzy.TabIndex = 1;
            this.medicineControl_tzy.ValueChanged += new System.EventHandler(this.DataModified);
            // 
            // panel_DrugYN
            // 
            this.panel_DrugYN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_DrugYN.BackgroundImage")));
            this.panel_DrugYN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_DrugYN.Controls.Add(this.rbt_DrugN);
            this.panel_DrugYN.Controls.Add(this.label4);
            this.panel_DrugYN.Controls.Add(this.rbt_DrugY);
            this.panel_DrugYN.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_DrugYN.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel_DrugYN.Location = new System.Drawing.Point(0, 98);
            this.panel_DrugYN.Name = "panel_DrugYN";
            this.panel_DrugYN.Size = new System.Drawing.Size(743, 30);
            this.panel_DrugYN.TabIndex = 1;
            // 
            // rbt_DrugN
            // 
            this.rbt_DrugN.AutoSize = true;
            this.rbt_DrugN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.rbt_DrugN.Checked = true;
            this.rbt_DrugN.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbt_DrugN.Location = new System.Drawing.Point(182, 10);
            this.rbt_DrugN.Name = "rbt_DrugN";
            this.rbt_DrugN.Size = new System.Drawing.Size(39, 18);
            this.rbt_DrugN.TabIndex = 2;
            this.rbt_DrugN.TabStop = true;
            this.rbt_DrugN.Text = "ÎÞ";
            this.rbt_DrugN.UseVisualStyleBackColor = false;
            this.rbt_DrugN.CheckedChanged += new System.EventHandler(this.rbt_DrugN_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label4.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(39, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "µ÷Ö¬Ò©£º";
            // 
            // rbt_DrugY
            // 
            this.rbt_DrugY.AutoSize = true;
            this.rbt_DrugY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.rbt_DrugY.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbt_DrugY.Location = new System.Drawing.Point(120, 10);
            this.rbt_DrugY.Name = "rbt_DrugY";
            this.rbt_DrugY.Size = new System.Drawing.Size(39, 18);
            this.rbt_DrugY.TabIndex = 1;
            this.rbt_DrugY.Text = "ÓÐ";
            this.rbt_DrugY.UseVisualStyleBackColor = false;
            // 
            // panel_LDYN
            // 
            this.panel_LDYN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_LDYN.BackgroundImage")));
            this.panel_LDYN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_LDYN.Controls.Add(this.dateTimePicker_LDDate);
            this.panel_LDYN.Controls.Add(this.panel_LDType);
            this.panel_LDYN.Controls.Add(this.rbt_LDN);
            this.panel_LDYN.Controls.Add(this.rbt_LDY);
            this.panel_LDYN.Controls.Add(this.label5);
            this.panel_LDYN.Controls.Add(this.label1);
            this.panel_LDYN.Controls.Add(this.label2);
            this.panel_LDYN.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_LDYN.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel_LDYN.Location = new System.Drawing.Point(0, 0);
            this.panel_LDYN.Name = "panel_LDYN";
            this.panel_LDYN.Size = new System.Drawing.Size(743, 98);
            this.panel_LDYN.TabIndex = 0;
            // 
            // dateTimePicker_LDDate
            // 
            this.dateTimePicker_LDDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dateTimePicker_LDDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.dateTimePicker_LDDate.Enabled = false;
            this.dateTimePicker_LDDate.Location = new System.Drawing.Point(377, 13);
            this.dateTimePicker_LDDate.Month = null;
            this.dateTimePicker_LDDate.Name = "dateTimePicker_LDDate";
            this.dateTimePicker_LDDate.Size = new System.Drawing.Size(123, 23);
            this.dateTimePicker_LDDate.TabIndex = 4;
            this.dateTimePicker_LDDate.Value = new System.DateTime(2010, 12, 1, 0, 0, 0, 0);
            this.dateTimePicker_LDDate.Year = null;
            this.dateTimePicker_LDDate.ValueChanged += new System.EventHandler(this.DataModified);
            // 
            // panel_LDType
            // 
            this.panel_LDType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_LDType.BackgroundImage")));
            this.panel_LDType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_LDType.Controls.Add(this.checkBox_TC);
            this.panel_LDType.Controls.Add(this.checkBox_HDLC);
            this.panel_LDType.Controls.Add(this.checkBox_LDLC);
            this.panel_LDType.Controls.Add(this.checkBox_TG);
            this.panel_LDType.Location = new System.Drawing.Point(108, 38);
            this.panel_LDType.Name = "panel_LDType";
            this.panel_LDType.Size = new System.Drawing.Size(427, 59);
            this.panel_LDType.TabIndex = 6;
            // 
            // checkBox_TC
            // 
            this.checkBox_TC.AutoSize = true;
            this.checkBox_TC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBox_TC.Location = new System.Drawing.Point(12, 5);
            this.checkBox_TC.Name = "checkBox_TC";
            this.checkBox_TC.Size = new System.Drawing.Size(82, 18);
            this.checkBox_TC.TabIndex = 0;
            this.checkBox_TC.Text = "¸ßµ¨¹Ì´¼";
            this.checkBox_TC.UseVisualStyleBackColor = false;
            this.checkBox_TC.CheckedChanged += new System.EventHandler(this.DataModified);
            // 
            // checkBox_HDLC
            // 
            this.checkBox_HDLC.AutoSize = true;
            this.checkBox_HDLC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBox_HDLC.Location = new System.Drawing.Point(189, 5);
            this.checkBox_HDLC.Name = "checkBox_HDLC";
            this.checkBox_HDLC.Size = new System.Drawing.Size(194, 18);
            this.checkBox_HDLC.TabIndex = 1;
            this.checkBox_HDLC.Text = "µÍ¸ßÃÜ¶ÈÖ¬µ°°×µ¨¹Ì´¼ÑªÖ¢";
            this.checkBox_HDLC.UseVisualStyleBackColor = false;
            this.checkBox_HDLC.CheckedChanged += new System.EventHandler(this.DataModified);
            // 
            // checkBox_LDLC
            // 
            this.checkBox_LDLC.AutoSize = true;
            this.checkBox_LDLC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBox_LDLC.Location = new System.Drawing.Point(189, 30);
            this.checkBox_LDLC.Name = "checkBox_LDLC";
            this.checkBox_LDLC.Size = new System.Drawing.Size(194, 18);
            this.checkBox_LDLC.TabIndex = 3;
            this.checkBox_LDLC.Text = "¸ßµÍÃÜ¶ÈÖ¬µ°°×µ¨¹Ì´¼ÑªÖ¢";
            this.checkBox_LDLC.UseVisualStyleBackColor = false;
            this.checkBox_LDLC.CheckedChanged += new System.EventHandler(this.DataModified);
            // 
            // checkBox_TG
            // 
            this.checkBox_TG.AutoSize = true;
            this.checkBox_TG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBox_TG.Location = new System.Drawing.Point(12, 30);
            this.checkBox_TG.Name = "checkBox_TG";
            this.checkBox_TG.Size = new System.Drawing.Size(124, 18);
            this.checkBox_TG.TabIndex = 2;
            this.checkBox_TG.Text = "¸ß¸ÊÓÍÈýõ¥ÑªÖ¢";
            this.checkBox_TG.UseVisualStyleBackColor = false;
            this.checkBox_TG.CheckedChanged += new System.EventHandler(this.DataModified);
            // 
            // rbt_LDN
            // 
            this.rbt_LDN.AutoSize = true;
            this.rbt_LDN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.rbt_LDN.Checked = true;
            this.rbt_LDN.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbt_LDN.Location = new System.Drawing.Point(182, 13);
            this.rbt_LDN.Name = "rbt_LDN";
            this.rbt_LDN.Size = new System.Drawing.Size(39, 18);
            this.rbt_LDN.TabIndex = 2;
            this.rbt_LDN.TabStop = true;
            this.rbt_LDN.Text = "ÎÞ";
            this.rbt_LDN.UseVisualStyleBackColor = false;
            this.rbt_LDN.CheckedChanged += new System.EventHandler(this.rbt_LDN_CheckedChanged);
            // 
            // rbt_LDY
            // 
            this.rbt_LDY.AutoSize = true;
            this.rbt_LDY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.rbt_LDY.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbt_LDY.Location = new System.Drawing.Point(120, 13);
            this.rbt_LDY.Name = "rbt_LDY";
            this.rbt_LDY.Size = new System.Drawing.Size(39, 18);
            this.rbt_LDY.TabIndex = 1;
            this.rbt_LDY.Text = "ÓÐ";
            this.rbt_LDY.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label5.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(53, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "ÀàÐÍ£º";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "ÑªÖ¬ÎÉÂÒ£º";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(294, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "·¢ÏÖÊ±¼ä£º";
            // 
            // LipidsDisorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(763, 483);
            this.Controls.Add(this.panel_LD);
            this.DoubleBuffered = true;
            this.Name = "LipidsDisorder";
            this.Text = "ÑªÖ¬ÎÉÂÒ";
            this.UseNextButton = true;
            this.Load += new System.EventHandler(this.LipidsDisorder_Load);
            this.VisibleChanged += new System.EventHandler(this.LipidsDisorder_VisibleChanged);
            this.Controls.SetChildIndex(this.panel_LD, 0);
            this.panel_LD.ResumeLayout(false);
            this.panel_DrugType.ResumeLayout(false);
            this.panel_DrugYN.ResumeLayout(false);
            this.panel_DrugYN.PerformLayout();
            this.panel_LDYN.ResumeLayout(false);
            this.panel_LDYN.PerformLayout();
            this.panel_LDType.ResumeLayout(false);
            this.panel_LDType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_LD;
        private System.Windows.Forms.Panel panel_LDYN;
        private System.Windows.Forms.RadioButton rbt_LDN;
        private System.Windows.Forms.RadioButton rbt_LDY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_DrugType;
        private System.Windows.Forms.Panel panel_DrugYN;
        private System.Windows.Forms.RadioButton rbt_DrugN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbt_DrugY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox_HDLC;
        private System.Windows.Forms.CheckBox checkBox_TG;
        private System.Windows.Forms.CheckBox checkBox_TC;
        private System.Windows.Forms.CheckBox checkBox_LDLC;
        private System.Windows.Forms.Panel panel_LDType;
        private CDSSCtrlLib.DateTimeControl dateTimePicker_LDDate;
        private CDSSCtrlLib.MedicineControl medicineControl_tzy;
    }
}