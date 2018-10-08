namespace CDSS
{
    partial class HighLevelQueryForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HighLevelQueryForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tempList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnExprt = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.RecordSEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HistoryRecordStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeDisplayCloumns = new CDSSCtrlLib.CheckTreeView();
            this.LstboxRslt = new System.Windows.Forms.ListView();
            this.BtnMdlMng = new System.Windows.Forms.Button();
            this.BtnClr = new System.Windows.Forms.Button();
            this.BtnSrch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.BtnOpen = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tempList
            // 
            this.tempList.Name = "tempList";
            this.tempList.Size = new System.Drawing.Size(61, 4);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "CSV(逗号分隔)|*.csv|所有文件|*.*";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(699, 438);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 47;
            this.button1.Text = "绘图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // BtnExprt
            // 
            this.BtnExprt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExprt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnExprt.BackgroundImage")));
            this.BtnExprt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExprt.Enabled = false;
            this.BtnExprt.Location = new System.Drawing.Point(800, 438);
            this.BtnExprt.Name = "BtnExprt";
            this.BtnExprt.Size = new System.Drawing.Size(86, 23);
            this.BtnExprt.TabIndex = 46;
            this.BtnExprt.Text = "导出";
            this.BtnExprt.UseVisualStyleBackColor = true;
            this.BtnExprt.Click += new System.EventHandler(this.BtnExprt_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.LstboxRslt);
            this.panel1.Location = new System.Drawing.Point(0, 212);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(896, 224);
            this.panel1.TabIndex = 45;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordSEQ,
            this.HistoryRecordStatus});
            this.dataGridView1.Location = new System.Drawing.Point(167, 10);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(719, 204);
            this.dataGridView1.TabIndex = 26;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // RecordSEQ
            // 
            this.RecordSEQ.DataPropertyName = "RecordSEQ";
            this.RecordSEQ.FillWeight = 1F;
            this.RecordSEQ.HeaderText = "RecordSEQ";
            this.RecordSEQ.Name = "RecordSEQ";
            this.RecordSEQ.ReadOnly = true;
            this.RecordSEQ.Visible = false;
            this.RecordSEQ.Width = 5;
            // 
            // HistoryRecordStatus
            // 
            this.HistoryRecordStatus.DataPropertyName = "HistoryRecordStatus";
            this.HistoryRecordStatus.HeaderText = "HistoryRecordStatus";
            this.HistoryRecordStatus.Name = "HistoryRecordStatus";
            this.HistoryRecordStatus.ReadOnly = true;
            this.HistoryRecordStatus.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox2.Controls.Add(this.treeDisplayCloumns);
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(0, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 212);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "待显示项选择";
            // 
            // treeDisplayCloumns
            // 
            this.treeDisplayCloumns.CheckBoxes = true;
            this.treeDisplayCloumns.CheckItemIndex = ((System.Collections.ArrayList)(resources.GetObject("treeDisplayCloumns.CheckItemIndex")));
            this.treeDisplayCloumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDisplayCloumns.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeDisplayCloumns.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeDisplayCloumns.Location = new System.Drawing.Point(3, 17);
            this.treeDisplayCloumns.Name = "treeDisplayCloumns";
            this.treeDisplayCloumns.Size = new System.Drawing.Size(159, 192);
            this.treeDisplayCloumns.TabIndex = 27;
            // 
            // LstboxRslt
            // 
            this.LstboxRslt.AllowColumnReorder = true;
            this.LstboxRslt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LstboxRslt.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LstboxRslt.FullRowSelect = true;
            this.LstboxRslt.GridLines = true;
            this.LstboxRslt.Location = new System.Drawing.Point(167, 10);
            this.LstboxRslt.Name = "LstboxRslt";
            this.LstboxRslt.Size = new System.Drawing.Size(719, 204);
            this.LstboxRslt.TabIndex = 24;
            this.LstboxRslt.UseCompatibleStateImageBehavior = false;
            this.LstboxRslt.View = System.Windows.Forms.View.Details;
            // 
            // BtnMdlMng
            // 
            this.BtnMdlMng.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnMdlMng.BackgroundImage")));
            this.BtnMdlMng.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnMdlMng.Location = new System.Drawing.Point(839, 165);
            this.BtnMdlMng.Name = "BtnMdlMng";
            this.BtnMdlMng.Size = new System.Drawing.Size(86, 23);
            this.BtnMdlMng.TabIndex = 44;
            this.BtnMdlMng.Text = "模板管理(&D)";
            this.BtnMdlMng.UseVisualStyleBackColor = true;
            this.BtnMdlMng.Click += new System.EventHandler(this.BtnMdlMng_Click);
            // 
            // BtnClr
            // 
            this.BtnClr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnClr.BackgroundImage")));
            this.BtnClr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClr.Location = new System.Drawing.Point(839, 110);
            this.BtnClr.Name = "BtnClr";
            this.BtnClr.Size = new System.Drawing.Size(86, 23);
            this.BtnClr.TabIndex = 43;
            this.BtnClr.Text = "清空(&C)";
            this.BtnClr.UseVisualStyleBackColor = true;
            this.BtnClr.Click += new System.EventHandler(this.BtnClr_Click);
            // 
            // BtnSrch
            // 
            this.BtnSrch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnSrch.BackgroundImage")));
            this.BtnSrch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSrch.Location = new System.Drawing.Point(839, 52);
            this.BtnSrch.Name = "BtnSrch";
            this.BtnSrch.Size = new System.Drawing.Size(86, 23);
            this.BtnSrch.TabIndex = 42;
            this.BtnSrch.Text = "查询(&Q)";
            this.BtnSrch.UseVisualStyleBackColor = true;
            this.BtnSrch.Click += new System.EventHandler(this.BtnSrch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.panel9);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.lblTemplateName);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(798, 208);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "高级查询";
            // 
            // panel9
            // 
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.BtnOpen);
            this.panel9.Controls.Add(this.BtnSave);
            this.panel9.Controls.Add(this.BtnAdd);
            this.panel9.Controls.Add(this.flowPanel);
            this.panel9.Location = new System.Drawing.Point(6, 32);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(786, 176);
            this.panel9.TabIndex = 66;
            // 
            // BtnOpen
            // 
            this.BtnOpen.BackColor = System.Drawing.Color.Transparent;
            this.BtnOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOpen.FlatAppearance.BorderSize = 0;
            this.BtnOpen.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("BtnOpen.Image")));
            this.BtnOpen.Location = new System.Drawing.Point(741, 129);
            this.BtnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Size = new System.Drawing.Size(25, 25);
            this.BtnOpen.TabIndex = 66;
            this.BtnOpen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnOpen.UseVisualStyleBackColor = false;
            this.BtnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Transparent;
            this.BtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnSave.BackgroundImage")));
            this.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnSave.Location = new System.Drawing.Point(741, 74);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(0);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(25, 25);
            this.BtnSave.TabIndex = 65;
            this.BtnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.Transparent;
            this.BtnAdd.BackgroundImage = global::CDSS.Properties.Resources.小添加按钮_normal;
            this.BtnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdd.FlatAppearance.BorderSize = 0;
            this.BtnAdd.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnAdd.Location = new System.Drawing.Point(741, 17);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(25, 25);
            this.BtnAdd.TabIndex = 64;
            this.BtnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // flowPanel
            // 
            this.flowPanel.AutoScroll = true;
            this.flowPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowPanel.Location = new System.Drawing.Point(0, 0);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(723, 176);
            this.flowPanel.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(6, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 65;
            this.label16.Text = "模板名称：";
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.AutoSize = true;
            this.lblTemplateName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.lblTemplateName.ForeColor = System.Drawing.Color.Black;
            this.lblTemplateName.Location = new System.Drawing.Point(75, 18);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(83, 12);
            this.lblTemplateName.TabIndex = 64;
            this.lblTemplateName.Text = "新建 查询模板";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.lblCount.Location = new System.Drawing.Point(106, 444);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(11, 12);
            this.lblCount.TabIndex = 49;
            this.lblCount.Text = "0";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label7.Location = new System.Drawing.Point(13, 444);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 48;
            this.label7.Text = "查到的记录数：";
            // 
            // HighLevelQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(908, 470);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnExprt);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnMdlMng);
            this.Controls.Add(this.BtnClr);
            this.Controls.Add(this.BtnSrch);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "HighLevelQueryForm";
            this.Text = "HighLevelQueryForm";
            this.Load += new System.EventHandler(this.HighLevelQueryForm_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.BtnSrch, 0);
            this.Controls.SetChildIndex(this.BtnClr, 0);
            this.Controls.SetChildIndex(this.BtnMdlMng, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.BtnExprt, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.lblCount, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip tempList;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnExprt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordSEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn HistoryRecordStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ListView LstboxRslt;
        private System.Windows.Forms.Button BtnMdlMng;
        private System.Windows.Forms.Button BtnClr;
        private System.Windows.Forms.Button BtnSrch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button BtnOpen;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label7;
        public CDSSCtrlLib.CheckTreeView treeDisplayCloumns;
    }
}