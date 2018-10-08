namespace CDSS
{
    partial class ImportDBMerge
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_NewDB = new System.Windows.Forms.TextBox();
            this.txt_PathWay = new System.Windows.Forms.TextBox();
            this.btn_OpenNewDB = new System.Windows.Forms.Button();
            this.openFileDialogNewDB = new System.Windows.Forms.OpenFileDialog();
            this.btnMerge = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_ExportDB = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ListBoxMerge = new System.Windows.Forms.ListBox();
            this.saveDBFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorkerDBMerge = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择新的数据源：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "请选择要导出的路径：";
            // 
            // txt_NewDB
            // 
            this.txt_NewDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_NewDB.Location = new System.Drawing.Point(18, 78);
            this.txt_NewDB.Name = "txt_NewDB";
            this.txt_NewDB.Size = new System.Drawing.Size(363, 23);
            this.txt_NewDB.TabIndex = 2;
            // 
            // txt_PathWay
            // 
            this.txt_PathWay.Location = new System.Drawing.Point(17, 79);
            this.txt_PathWay.Name = "txt_PathWay";
            this.txt_PathWay.Size = new System.Drawing.Size(393, 23);
            this.txt_PathWay.TabIndex = 3;
            // 
            // btn_OpenNewDB
            // 
            this.btn_OpenNewDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OpenNewDB.Image = global::CDSS.Properties.Resources.打开文件;
            this.btn_OpenNewDB.Location = new System.Drawing.Point(382, 75);
            this.btn_OpenNewDB.Name = "btn_OpenNewDB";
            this.btn_OpenNewDB.Size = new System.Drawing.Size(29, 29);
            this.btn_OpenNewDB.TabIndex = 4;
            this.btn_OpenNewDB.UseVisualStyleBackColor = true;
            this.btn_OpenNewDB.Click += new System.EventHandler(this.btn_OpenNewDB_Click);
            // 
            // openFileDialogNewDB
            // 
            this.openFileDialogNewDB.FileName = "openFileDialog1";
            // 
            // btnMerge
            // 
            this.btnMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMerge.Location = new System.Drawing.Point(417, 71);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(60, 38);
            this.btnMerge.TabIndex = 10;
            this.btnMerge.Text = "合并";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btn_ExportDB);
            this.groupBox1.Controls.Add(this.txt_PathWay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(19, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 611);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导出数据库";
            // 
            // btn_ExportDB
            // 
            this.btn_ExportDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ExportDB.Location = new System.Drawing.Point(416, 71);
            this.btn_ExportDB.Name = "btn_ExportDB";
            this.btn_ExportDB.Size = new System.Drawing.Size(60, 38);
            this.btn_ExportDB.TabIndex = 11;
            this.btn_ExportDB.Text = "导出";
            this.btn_ExportDB.UseVisualStyleBackColor = true;
            this.btn_ExportDB.Click += new System.EventHandler(this.btn_ExportDB_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btn_Cancel);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.ListBoxMerge);
            this.groupBox2.Controls.Add(this.btn_OpenNewDB);
            this.groupBox2.Controls.Add(this.btnMerge);
            this.groupBox2.Controls.Add(this.txt_NewDB);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(523, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(485, 611);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据库合并";
            // 
            // ListBoxMerge
            // 
            this.ListBoxMerge.FormattingEnabled = true;
            this.ListBoxMerge.ItemHeight = 14;
            this.ListBoxMerge.Location = new System.Drawing.Point(24, 134);
            this.ListBoxMerge.Name = "ListBoxMerge";
            this.ListBoxMerge.Size = new System.Drawing.Size(386, 284);
            this.ListBoxMerge.TabIndex = 11;
            // 
            // backgroundWorkerDBMerge
            // 
            this.backgroundWorkerDBMerge.WorkerReportsProgress = true;
            this.backgroundWorkerDBMerge.WorkerSupportsCancellation = true;
            this.backgroundWorkerDBMerge.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDBMerge_DoWork);
            this.backgroundWorkerDBMerge.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDBMerge_WorkerCompleted);
            this.backgroundWorkerDBMerge.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerDBMerge_ProgressChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(24, 455);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(342, 23);
            this.progressBar1.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(372, 459);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 14);
            this.label3.TabIndex = 13;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Enabled = false;
            this.btn_Cancel.Location = new System.Drawing.Point(428, 455);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(51, 23);
            this.btn_Cancel.TabIndex = 14;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // ImportDBMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CDSS.Properties.Resources.Main_Bk;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1026, 653);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportDBMerge";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ImportDBMerge";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_NewDB;
        private System.Windows.Forms.TextBox txt_PathWay;
        private System.Windows.Forms.Button btn_OpenNewDB;
        private System.Windows.Forms.OpenFileDialog openFileDialogNewDB;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_ExportDB;
        private System.Windows.Forms.ListBox ListBoxMerge;
        private System.Windows.Forms.SaveFileDialog saveDBFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDBMerge;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Cancel;


    }
}