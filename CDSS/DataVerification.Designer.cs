using System.Windows.Forms;
namespace CDSS
{
    partial class DataVerification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataVerification));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeDisplayCloumns = new CDSSCtrlLib.CheckTreeView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDataImport = new System.Windows.Forms.Button();
            this.btnDataCheck = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grbFile2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblRow2 = new System.Windows.Forms.Label();
            this.lblCols2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgrdfile2 = new System.Windows.Forms.DataGridView();
            this.btnOpen2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.grbFile1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRow1 = new System.Windows.Forms.Label();
            this.lblCols1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgrdfile1 = new System.Windows.Forms.DataGridView();
            this.btnOpen1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCombine = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dgrgCombine = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grbFile2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdfile2)).BeginInit();
            this.grbFile1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdfile1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrgCombine)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.groupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox3.Controls.Add(this.treeDisplayCloumns);
            this.groupBox3.ForeColor = System.Drawing.Color.Blue;
            this.groupBox3.Location = new System.Drawing.Point(8, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(183, 482);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "同条记录参照设置";
            // 
            // treeDisplayCloumns
            // 
            this.treeDisplayCloumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeDisplayCloumns.CheckBoxes = true;
            this.treeDisplayCloumns.CheckItemIndex = ((System.Collections.ArrayList)(resources.GetObject("treeDisplayCloumns.CheckItemIndex")));
            this.treeDisplayCloumns.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeDisplayCloumns.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeDisplayCloumns.Location = new System.Drawing.Point(3, 17);
            this.treeDisplayCloumns.Name = "treeDisplayCloumns";
            this.treeDisplayCloumns.Size = new System.Drawing.Size(177, 462);
            this.treeDisplayCloumns.TabIndex = 27;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel文件|*.xls;*.xlsx";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnDataImport);
            this.groupBox4.Controls.Add(this.btnDataCheck);
            this.groupBox4.ForeColor = System.Drawing.Color.Blue;
            this.groupBox4.Location = new System.Drawing.Point(197, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(902, 41);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "操作栏";
            // 
            // btnDataImport
            // 
            this.btnDataImport.Enabled = false;
            this.btnDataImport.ForeColor = System.Drawing.Color.Black;
            this.btnDataImport.Location = new System.Drawing.Point(97, 13);
            this.btnDataImport.Name = "btnDataImport";
            this.btnDataImport.Size = new System.Drawing.Size(75, 23);
            this.btnDataImport.TabIndex = 2;
            this.btnDataImport.Text = "数据导入";
            this.btnDataImport.UseVisualStyleBackColor = true;
            this.btnDataImport.Click += new System.EventHandler(this.btnDataImport_Click);
            // 
            // btnDataCheck
            // 
            this.btnDataCheck.Enabled = false;
            this.btnDataCheck.ForeColor = System.Drawing.Color.Black;
            this.btnDataCheck.Location = new System.Drawing.Point(8, 12);
            this.btnDataCheck.Name = "btnDataCheck";
            this.btnDataCheck.Size = new System.Drawing.Size(75, 23);
            this.btnDataCheck.TabIndex = 1;
            this.btnDataCheck.Text = "数据对比";
            this.btnDataCheck.UseVisualStyleBackColor = true;
            this.btnDataCheck.Click += new System.EventHandler(this.btnDataCheck_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.grbFile2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.grbFile1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(194, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(907, 443);
            this.tableLayoutPanel1.TabIndex = 30;
            // 
            // grbFile2
            // 
            this.grbFile2.Controls.Add(this.panel2);
            this.grbFile2.Controls.Add(this.label3);
            this.grbFile2.Controls.Add(this.dgrdfile2);
            this.grbFile2.Controls.Add(this.btnOpen2);
            this.grbFile2.Controls.Add(this.textBox2);
            this.grbFile2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbFile2.ForeColor = System.Drawing.Color.Blue;
            this.grbFile2.Location = new System.Drawing.Point(479, 3);
            this.grbFile2.Name = "grbFile2";
            this.grbFile2.Size = new System.Drawing.Size(425, 437);
            this.grbFile2.TabIndex = 29;
            this.grbFile2.TabStop = false;
            this.grbFile2.Text = "数据文件2";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.panel2.Controls.Add(this.lblRow2);
            this.panel2.Controls.Add(this.lblCols2);
            this.panel2.Location = new System.Drawing.Point(8, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(411, 42);
            this.panel2.TabIndex = 5;
            // 
            // lblRow2
            // 
            this.lblRow2.AutoSize = true;
            this.lblRow2.ForeColor = System.Drawing.Color.Black;
            this.lblRow2.Location = new System.Drawing.Point(10, 17);
            this.lblRow2.Name = "lblRow2";
            this.lblRow2.Size = new System.Drawing.Size(41, 12);
            this.lblRow2.TabIndex = 6;
            this.lblRow2.Text = "行数：";
            // 
            // lblCols2
            // 
            this.lblCols2.AutoSize = true;
            this.lblCols2.ForeColor = System.Drawing.Color.Black;
            this.lblCols2.Location = new System.Drawing.Point(164, 18);
            this.lblCols2.Name = "lblCols2";
            this.lblCols2.Size = new System.Drawing.Size(41, 12);
            this.lblCols2.TabIndex = 7;
            this.lblCols2.Text = "列数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "文件信息";
            // 
            // dgrdfile2
            // 
            this.dgrdfile2.AllowUserToAddRows = false;
            this.dgrdfile2.AllowUserToDeleteRows = false;
            this.dgrdfile2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdfile2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrdfile2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgrdfile2.Location = new System.Drawing.Point(8, 104);
            this.dgrdfile2.MultiSelect = false;
            this.dgrdfile2.Name = "dgrdfile2";
            this.dgrdfile2.ReadOnly = true;
            this.dgrdfile2.RowHeadersVisible = false;
            this.dgrdfile2.RowTemplate.Height = 23;
            this.dgrdfile2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrdfile2.Size = new System.Drawing.Size(413, 330);
            this.dgrdfile2.TabIndex = 3;
            this.dgrdfile2.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgrdfile1_DataError);
            // 
            // btnOpen2
            // 
            this.btnOpen2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen2.Location = new System.Drawing.Point(383, 19);
            this.btnOpen2.Name = "btnOpen2";
            this.btnOpen2.Size = new System.Drawing.Size(39, 23);
            this.btnOpen2.TabIndex = 1;
            this.btnOpen2.Text = "...";
            this.btnOpen2.UseVisualStyleBackColor = true;
            this.btnOpen2.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(6, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(372, 21);
            this.textBox2.TabIndex = 0;
            // 
            // grbFile1
            // 
            this.grbFile1.Controls.Add(this.panel4);
            this.grbFile1.Controls.Add(this.panel1);
            this.grbFile1.Controls.Add(this.label2);
            this.grbFile1.Controls.Add(this.dgrdfile1);
            this.grbFile1.Controls.Add(this.btnOpen1);
            this.grbFile1.Controls.Add(this.textBox1);
            this.grbFile1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbFile1.ForeColor = System.Drawing.Color.Blue;
            this.grbFile1.Location = new System.Drawing.Point(3, 3);
            this.grbFile1.Name = "grbFile1";
            this.grbFile1.Size = new System.Drawing.Size(425, 437);
            this.grbFile1.TabIndex = 5;
            this.grbFile1.TabStop = false;
            this.grbFile1.Text = "数据文件1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.panel1.Controls.Add(this.lblRow1);
            this.panel1.Controls.Add(this.lblCols1);
            this.panel1.Location = new System.Drawing.Point(8, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 42);
            this.panel1.TabIndex = 4;
            // 
            // lblRow1
            // 
            this.lblRow1.AutoSize = true;
            this.lblRow1.ForeColor = System.Drawing.Color.Black;
            this.lblRow1.Location = new System.Drawing.Point(9, 17);
            this.lblRow1.Name = "lblRow1";
            this.lblRow1.Size = new System.Drawing.Size(41, 12);
            this.lblRow1.TabIndex = 0;
            this.lblRow1.Text = "行数：";
            // 
            // lblCols1
            // 
            this.lblCols1.AutoSize = true;
            this.lblCols1.ForeColor = System.Drawing.Color.Black;
            this.lblCols1.Location = new System.Drawing.Point(163, 18);
            this.lblCols1.Name = "lblCols1";
            this.lblCols1.Size = new System.Drawing.Size(41, 12);
            this.lblCols1.TabIndex = 1;
            this.lblCols1.Text = "列数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "文件信息";
            // 
            // dgrdfile1
            // 
            this.dgrdfile1.AllowUserToAddRows = false;
            this.dgrdfile1.AllowUserToDeleteRows = false;
            this.dgrdfile1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdfile1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrdfile1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgrdfile1.Location = new System.Drawing.Point(6, 104);
            this.dgrdfile1.MultiSelect = false;
            this.dgrdfile1.Name = "dgrdfile1";
            this.dgrdfile1.ReadOnly = true;
            this.dgrdfile1.RowHeadersVisible = false;
            this.dgrdfile1.RowTemplate.Height = 23;
            this.dgrdfile1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrdfile1.Size = new System.Drawing.Size(413, 330);
            this.dgrdfile1.TabIndex = 2;
            this.dgrdfile1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgrdfile1_DataError);
            // 
            // btnOpen1
            // 
            this.btnOpen1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen1.Location = new System.Drawing.Point(382, 19);
            this.btnOpen1.Name = "btnOpen1";
            this.btnOpen1.Size = new System.Drawing.Size(39, 23);
            this.btnOpen1.TabIndex = 1;
            this.btnOpen1.Text = "...";
            this.btnOpen1.UseVisualStyleBackColor = true;
            this.btnOpen1.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(372, 21);
            this.textBox1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCombine);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(434, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(39, 437);
            this.panel3.TabIndex = 30;
            // 
            // btnCombine
            // 
            this.btnCombine.Enabled = false;
            this.btnCombine.Location = new System.Drawing.Point(0, 181);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(40, 23);
            this.btnCombine.TabIndex = 8;
            this.btnCombine.Text = "合并";
            this.btnCombine.UseVisualStyleBackColor = true;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 28);
            this.label1.TabIndex = 7;
            this.label1.Text = "<--->";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // dgrgCombine
            // 
            this.dgrgCombine.AllowUserToAddRows = false;
            this.dgrgCombine.AllowUserToDeleteRows = false;
            this.dgrgCombine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrgCombine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrgCombine.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgrgCombine.Location = new System.Drawing.Point(8, 510);
            this.dgrgCombine.Name = "dgrgCombine";
            this.dgrgCombine.ReadOnly = true;
            this.dgrgCombine.RowHeadersVisible = false;
            this.dgrgCombine.RowTemplate.Height = 23;
            this.dgrgCombine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrgCombine.Size = new System.Drawing.Size(1093, 182);
            this.dgrgCombine.TabIndex = 31;
            this.dgrgCombine.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgrdfile1_DataError);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(12, 494);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "已核对列表";
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Location = new System.Drawing.Point(137, 48);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(351, 98);
            this.panel4.TabIndex = 33;
            this.panel4.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(97, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "数据处理中，请等待...";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(229)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(349, 22);
            this.panel5.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "提示";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DataVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1111, 699);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgrgCombine);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DataVerification";
            this.Text = "数据校验";
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grbFile2.ResumeLayout(false);
            this.grbFile2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdfile2)).EndInit();
            this.grbFile1.ResumeLayout(false);
            this.grbFile1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdfile1)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrgCombine)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private CDSSCtrlLib.CheckTreeView treeDisplayCloumns;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grbFile1;
        private System.Windows.Forms.DataGridView dgrdfile1;
        private System.Windows.Forms.Button btnOpen1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox grbFile2;
        private System.Windows.Forms.DataGridView dgrdfile2;
        private System.Windows.Forms.Button btnOpen2;
        private System.Windows.Forms.TextBox textBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRow1;
        private System.Windows.Forms.Label lblCols1;
        private System.Windows.Forms.Label lblRow2;
        private System.Windows.Forms.Label lblCols2;
        private System.Windows.Forms.DataGridView dgrgCombine;
        private System.Windows.Forms.Button btnDataImport;
        private System.Windows.Forms.Button btnDataCheck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private Panel panel4;
        private Panel panel5;
        private Label label5;
        private Label label6;
    }
}