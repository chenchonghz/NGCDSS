namespace CDSS
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            this.labelFilePath = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.buttonSetFilePath = new System.Windows.Forms.Button();
            this.checkBoxReason = new System.Windows.Forms.CheckBox();
            this.buttonImport = new System.Windows.Forms.Button();
            this.progressBarImporting = new System.Windows.Forms.ProgressBar();
            this.openFileDialogExcel = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorkerImport = new System.ComponentModel.BackgroundWorker();
            this.labelProgress = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeDisplayCloumns = new CDSSCtrlLib.CheckTreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ExcelControl = new CDSSCtrlLib.WinExcelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNewExcel = new System.Windows.Forms.Button();
            this.btnShowMaxExcel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.labelFilePath.Location = new System.Drawing.Point(8, 10);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(89, 12);
            this.labelFilePath.TabIndex = 0;
            this.labelFilePath.Text = "待导入的文件：";
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Enabled = false;
            this.textBoxFilePath.Location = new System.Drawing.Point(103, 6);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(266, 21);
            this.textBoxFilePath.TabIndex = 1;
            // 
            // buttonSetFilePath
            // 
            this.buttonSetFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSetFilePath.Location = new System.Drawing.Point(374, 5);
            this.buttonSetFilePath.Name = "buttonSetFilePath";
            this.buttonSetFilePath.Size = new System.Drawing.Size(74, 23);
            this.buttonSetFilePath.TabIndex = 2;
            this.buttonSetFilePath.Text = "打开Excel";
            this.buttonSetFilePath.UseVisualStyleBackColor = true;
            this.buttonSetFilePath.Click += new System.EventHandler(this.buttonSetFilePath_Click);
            // 
            // checkBoxReason
            // 
            this.checkBoxReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxReason.AutoSize = true;
            this.checkBoxReason.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxReason.Location = new System.Drawing.Point(119, 524);
            this.checkBoxReason.Name = "checkBoxReason";
            this.checkBoxReason.Size = new System.Drawing.Size(96, 16);
            this.checkBoxReason.TabIndex = 3;
            this.checkBoxReason.Text = "导入时即推理";
            this.checkBoxReason.UseVisualStyleBackColor = false;
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonImport.Location = new System.Drawing.Point(10, 520);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 4;
            this.buttonImport.Text = "导入";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // progressBarImporting
            // 
            this.progressBarImporting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBarImporting.Location = new System.Drawing.Point(5, 485);
            this.progressBarImporting.Name = "progressBarImporting";
            this.progressBarImporting.Size = new System.Drawing.Size(172, 23);
            this.progressBarImporting.TabIndex = 5;
            // 
            // openFileDialogExcel
            // 
            this.openFileDialogExcel.Filter = "Excel files|*.xlsx;*.xls|All files|*.*";
            // 
            // backgroundWorkerImport
            // 
            this.backgroundWorkerImport.WorkerReportsProgress = true;
            this.backgroundWorkerImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerImport_DoWork);
            this.backgroundWorkerImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerImport_RunWorkerCompleted);
            this.backgroundWorkerImport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerImport_ProgressChanged);
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgress.AutoSize = true;
            this.labelProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.labelProgress.Location = new System.Drawing.Point(187, 491);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(23, 12);
            this.labelProgress.TabIndex = 0;
            this.labelProgress.Text = "0/0";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox2.Controls.Add(this.treeDisplayCloumns);
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(5, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 473);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "同条记录参照设置";
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
            this.treeDisplayCloumns.Size = new System.Drawing.Size(202, 453);
            this.treeDisplayCloumns.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 124);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导入功能说明";
            this.groupBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 104);
            this.label1.TabIndex = 29;
            this.label1.Text = "一、导入前先设置判断是否为同条记录的判断标志\r\n\r\n二、红色选项为数据源中有效列名称\r\n\r\n三、若提示判断依据不足，请设置多个判断依据";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.labelProgress);
            this.splitContainer1.Panel1.Controls.Add(this.progressBarImporting);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxReason);
            this.splitContainer1.Panel1.Controls.Add(this.buttonImport);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.ExcelControl);
            this.splitContainer1.Size = new System.Drawing.Size(984, 554);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 30;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(753, 554);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.Visible = false;
            // 
            // ExcelControl
            // 
            this.ExcelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExcelControl.Location = new System.Drawing.Point(0, 0);
            this.ExcelControl.Name = "ExcelControl";
            this.ExcelControl.Size = new System.Drawing.Size(753, 554);
            this.ExcelControl.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnNewExcel);
            this.panel1.Controls.Add(this.btnShowMaxExcel);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.labelFilePath);
            this.panel1.Controls.Add(this.textBoxFilePath);
            this.panel1.Controls.Add(this.buttonSetFilePath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 33);
            this.panel1.TabIndex = 32;
            // 
            // btnNewExcel
            // 
            this.btnNewExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewExcel.Location = new System.Drawing.Point(461, 5);
            this.btnNewExcel.Name = "btnNewExcel";
            this.btnNewExcel.Size = new System.Drawing.Size(77, 23);
            this.btnNewExcel.TabIndex = 30;
            this.btnNewExcel.Text = "创建Excel";
            this.btnNewExcel.UseVisualStyleBackColor = true;
            this.btnNewExcel.Click += new System.EventHandler(this.btnNewExcel_Click);
            // 
            // btnShowMaxExcel
            // 
            this.btnShowMaxExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowMaxExcel.Enabled = false;
            this.btnShowMaxExcel.Location = new System.Drawing.Point(636, 5);
            this.btnShowMaxExcel.Name = "btnShowMaxExcel";
            this.btnShowMaxExcel.Size = new System.Drawing.Size(77, 23);
            this.btnShowMaxExcel.TabIndex = 29;
            this.btnShowMaxExcel.Text = "大窗口显示";
            this.btnShowMaxExcel.UseVisualStyleBackColor = true;
            this.btnShowMaxExcel.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(550, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 23);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "关闭Excel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1004, 607);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据导入";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button buttonSetFilePath;
        private System.Windows.Forms.CheckBox checkBoxReason;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.ProgressBar progressBarImporting;
        private System.Windows.Forms.OpenFileDialog openFileDialogExcel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerImport;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private CDSSCtrlLib.CheckTreeView treeDisplayCloumns;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnShowMaxExcel;
        public CDSSCtrlLib.WinExcelControl ExcelControl;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnNewExcel;
    }
}
