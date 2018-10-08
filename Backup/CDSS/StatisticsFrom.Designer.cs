namespace CDSS
{
    partial class StatisticsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsForm));
            this.dataGridViewDigno = new System.Windows.Forms.DataGridView();
            this.labelGroupResult = new System.Windows.Forms.Label();
            this.buttonGrouping = new System.Windows.Forms.Button();
            this.buttonClean = new System.Windows.Forms.Button();
            this.buttonTempManage = new System.Windows.Forms.Button();
            this.groupBoxTimeSet = new System.Windows.Forms.GroupBox();
            this.comboBoxInterval = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxStatiConSet = new System.Windows.Forms.GroupBox();
            this.btnAdd1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxContrConSet = new System.Windows.Forms.GroupBox();
            this.btnAdd2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxValuePara = new System.Windows.Forms.GroupBox();
            this.checkBoxQuar = new System.Windows.Forms.CheckBox();
            this.checkBoxMedian = new System.Windows.Forms.CheckBox();
            this.checkBoxMin = new System.Windows.Forms.CheckBox();
            this.checkBoxMax = new System.Windows.Forms.CheckBox();
            this.checkBoxStandard = new System.Windows.Forms.CheckBox();
            this.checkBoxAverage = new System.Windows.Forms.CheckBox();
            this.checkedListBoxValue = new System.Windows.Forms.CheckedListBox();
            this.groupBoxDignoPara = new System.Windows.Forms.GroupBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBoxMorbiRate = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBoxMorbi = new System.Windows.Forms.CheckBox();
            this.checkBoxNewMorbi = new System.Windows.Forms.CheckBox();
            this.checkBoxNewMorbiRate = new System.Windows.Forms.CheckBox();
            this.checkedListBoxDigno = new System.Windows.Forms.CheckedListBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.dataGridViewValue = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDigno)).BeginInit();
            this.groupBoxTimeSet.SuspendLayout();
            this.groupBoxStatiConSet.SuspendLayout();
            this.groupBoxContrConSet.SuspendLayout();
            this.groupBoxValuePara.SuspendLayout();
            this.groupBoxDignoPara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDigno
            // 
            this.dataGridViewDigno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewDigno.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDigno.Location = new System.Drawing.Point(523, 515);
            this.dataGridViewDigno.Name = "dataGridViewDigno";
            this.dataGridViewDigno.RowTemplate.Height = 23;
            this.dataGridViewDigno.Size = new System.Drawing.Size(480, 152);
            this.dataGridViewDigno.TabIndex = 1;
            // 
            // labelGroupResult
            // 
            this.labelGroupResult.AutoSize = true;
            this.labelGroupResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.labelGroupResult.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelGroupResult.Location = new System.Drawing.Point(21, 488);
            this.labelGroupResult.Name = "labelGroupResult";
            this.labelGroupResult.Size = new System.Drawing.Size(119, 14);
            this.labelGroupResult.TabIndex = 2;
            this.labelGroupResult.Text = "数值参数统计结果";
            // 
            // buttonGrouping
            // 
            this.buttonGrouping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonGrouping.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonGrouping.Location = new System.Drawing.Point(897, 114);
            this.buttonGrouping.Name = "buttonGrouping";
            this.buttonGrouping.Size = new System.Drawing.Size(87, 27);
            this.buttonGrouping.TabIndex = 3;
            this.buttonGrouping.Text = "统计";
            this.buttonGrouping.UseVisualStyleBackColor = true;
            this.buttonGrouping.Click += new System.EventHandler(this.buttonGrouping_Click);
            // 
            // buttonClean
            // 
            this.buttonClean.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClean.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonClean.Location = new System.Drawing.Point(897, 449);
            this.buttonClean.Name = "buttonClean";
            this.buttonClean.Size = new System.Drawing.Size(87, 27);
            this.buttonClean.TabIndex = 3;
            this.buttonClean.Text = "清除";
            this.buttonClean.UseVisualStyleBackColor = true;
            this.buttonClean.Click += new System.EventHandler(this.buttonClean_Click);
            // 
            // buttonTempManage
            // 
            this.buttonTempManage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonTempManage.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTempManage.Location = new System.Drawing.Point(897, 382);
            this.buttonTempManage.Name = "buttonTempManage";
            this.buttonTempManage.Size = new System.Drawing.Size(87, 27);
            this.buttonTempManage.TabIndex = 3;
            this.buttonTempManage.Text = "模板管理";
            this.buttonTempManage.UseVisualStyleBackColor = true;
            this.buttonTempManage.Click += new System.EventHandler(this.buttonTempManage_Click);
            // 
            // groupBoxTimeSet
            // 
            this.groupBoxTimeSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBoxTimeSet.Controls.Add(this.comboBoxInterval);
            this.groupBoxTimeSet.Controls.Add(this.label4);
            this.groupBoxTimeSet.Controls.Add(this.label2);
            this.groupBoxTimeSet.Controls.Add(this.dateTimePicker4);
            this.groupBoxTimeSet.Controls.Add(this.dateTimePicker2);
            this.groupBoxTimeSet.Controls.Add(this.dateTimePicker3);
            this.groupBoxTimeSet.Controls.Add(this.dateTimePicker1);
            this.groupBoxTimeSet.Controls.Add(this.label3);
            this.groupBoxTimeSet.Controls.Add(this.label1);
            this.groupBoxTimeSet.Location = new System.Drawing.Point(24, 15);
            this.groupBoxTimeSet.Name = "groupBoxTimeSet";
            this.groupBoxTimeSet.Size = new System.Drawing.Size(942, 70);
            this.groupBoxTimeSet.TabIndex = 4;
            this.groupBoxTimeSet.TabStop = false;
            this.groupBoxTimeSet.Text = "时间设置";
            // 
            // comboBoxInterval
            // 
            this.comboBoxInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterval.FormattingEnabled = true;
            this.comboBoxInterval.Items.AddRange(new object[] {
            "每年",
            "每季度",
            "每月",
            "自定义"});
            this.comboBoxInterval.Location = new System.Drawing.Point(781, 27);
            this.comboBoxInterval.Name = "comboBoxInterval";
            this.comboBoxInterval.Size = new System.Drawing.Size(137, 22);
            this.comboBoxInterval.TabIndex = 3;
            this.comboBoxInterval.SelectedIndexChanged += new System.EventHandler(this.comboBoxInterval_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label4.Location = new System.Drawing.Point(605, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "～";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label2.Location = new System.Drawing.Point(225, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "～";
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Location = new System.Drawing.Point(632, 26);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(124, 23);
            this.dateTimePicker4.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(252, 26);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(124, 23);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(473, 26);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(124, 23);
            this.dateTimePicker3.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(93, 26);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(124, 23);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label3.Location = new System.Drawing.Point(390, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "基准时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "统计时间：";
            // 
            // groupBoxStatiConSet
            // 
            this.groupBoxStatiConSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBoxStatiConSet.Controls.Add(this.btnAdd1);
            this.groupBoxStatiConSet.Controls.Add(this.flowLayoutPanel1);
            this.groupBoxStatiConSet.Location = new System.Drawing.Point(24, 89);
            this.groupBoxStatiConSet.Name = "groupBoxStatiConSet";
            this.groupBoxStatiConSet.Size = new System.Drawing.Size(859, 132);
            this.groupBoxStatiConSet.TabIndex = 0;
            this.groupBoxStatiConSet.TabStop = false;
            this.groupBoxStatiConSet.Text = "统计组条件设置";
            // 
            // btnAdd1
            // 
            this.btnAdd1.BackgroundImage = global::CDSS.Properties.Resources.小添加按钮_normal;
            this.btnAdd1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd1.Location = new System.Drawing.Point(818, 48);
            this.btnAdd1.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd1.Name = "btnAdd1";
            this.btnAdd1.Size = new System.Drawing.Size(31, 33);
            this.btnAdd1.TabIndex = 68;
            this.btnAdd1.UseVisualStyleBackColor = true;
            this.btnAdd1.Click += new System.EventHandler(this.btnAdd1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(7, 23);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(806, 91);
            this.flowLayoutPanel1.TabIndex = 67;
            // 
            // groupBoxContrConSet
            // 
            this.groupBoxContrConSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBoxContrConSet.Controls.Add(this.btnAdd2);
            this.groupBoxContrConSet.Controls.Add(this.flowLayoutPanel2);
            this.groupBoxContrConSet.Location = new System.Drawing.Point(24, 225);
            this.groupBoxContrConSet.Name = "groupBoxContrConSet";
            this.groupBoxContrConSet.Size = new System.Drawing.Size(859, 132);
            this.groupBoxContrConSet.TabIndex = 0;
            this.groupBoxContrConSet.TabStop = false;
            this.groupBoxContrConSet.Text = "对照组条件设置";
            // 
            // btnAdd2
            // 
            this.btnAdd2.BackgroundImage = global::CDSS.Properties.Resources.小添加按钮_normal;
            this.btnAdd2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd2.Location = new System.Drawing.Point(818, 44);
            this.btnAdd2.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd2.Name = "btnAdd2";
            this.btnAdd2.Size = new System.Drawing.Size(31, 34);
            this.btnAdd2.TabIndex = 68;
            this.btnAdd2.UseVisualStyleBackColor = true;
            this.btnAdd2.Click += new System.EventHandler(this.btnAdd2_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(7, 23);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(806, 91);
            this.flowLayoutPanel2.TabIndex = 67;
            // 
            // groupBoxValuePara
            // 
            this.groupBoxValuePara.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBoxValuePara.Controls.Add(this.checkBoxQuar);
            this.groupBoxValuePara.Controls.Add(this.checkBoxMedian);
            this.groupBoxValuePara.Controls.Add(this.checkBoxMin);
            this.groupBoxValuePara.Controls.Add(this.checkBoxMax);
            this.groupBoxValuePara.Controls.Add(this.checkBoxStandard);
            this.groupBoxValuePara.Controls.Add(this.checkBoxAverage);
            this.groupBoxValuePara.Controls.Add(this.checkedListBoxValue);
            this.groupBoxValuePara.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxValuePara.Location = new System.Drawing.Point(24, 365);
            this.groupBoxValuePara.Name = "groupBoxValuePara";
            this.groupBoxValuePara.Size = new System.Drawing.Size(415, 107);
            this.groupBoxValuePara.TabIndex = 5;
            this.groupBoxValuePara.TabStop = false;
            this.groupBoxValuePara.Text = "数值参数";
            // 
            // checkBoxQuar
            // 
            this.checkBoxQuar.AutoSize = true;
            this.checkBoxQuar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxQuar.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxQuar.Location = new System.Drawing.Point(326, 72);
            this.checkBoxQuar.Name = "checkBoxQuar";
            this.checkBoxQuar.Size = new System.Drawing.Size(68, 18);
            this.checkBoxQuar.TabIndex = 1;
            this.checkBoxQuar.Text = "四分差";
            this.checkBoxQuar.UseVisualStyleBackColor = false;
            // 
            // checkBoxMedian
            // 
            this.checkBoxMedian.AutoSize = true;
            this.checkBoxMedian.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxMedian.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxMedian.Location = new System.Drawing.Point(326, 48);
            this.checkBoxMedian.Name = "checkBoxMedian";
            this.checkBoxMedian.Size = new System.Drawing.Size(68, 18);
            this.checkBoxMedian.TabIndex = 1;
            this.checkBoxMedian.Text = "中位数";
            this.checkBoxMedian.UseVisualStyleBackColor = false;
            // 
            // checkBoxMin
            // 
            this.checkBoxMin.AutoSize = true;
            this.checkBoxMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxMin.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxMin.Location = new System.Drawing.Point(326, 24);
            this.checkBoxMin.Name = "checkBoxMin";
            this.checkBoxMin.Size = new System.Drawing.Size(68, 18);
            this.checkBoxMin.TabIndex = 1;
            this.checkBoxMin.Text = "最小值";
            this.checkBoxMin.UseVisualStyleBackColor = false;
            // 
            // checkBoxMax
            // 
            this.checkBoxMax.AutoSize = true;
            this.checkBoxMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxMax.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxMax.Location = new System.Drawing.Point(241, 72);
            this.checkBoxMax.Name = "checkBoxMax";
            this.checkBoxMax.Size = new System.Drawing.Size(68, 18);
            this.checkBoxMax.TabIndex = 1;
            this.checkBoxMax.Text = "最大值";
            this.checkBoxMax.UseVisualStyleBackColor = false;
            // 
            // checkBoxStandard
            // 
            this.checkBoxStandard.AutoSize = true;
            this.checkBoxStandard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxStandard.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxStandard.Location = new System.Drawing.Point(241, 48);
            this.checkBoxStandard.Name = "checkBoxStandard";
            this.checkBoxStandard.Size = new System.Drawing.Size(68, 18);
            this.checkBoxStandard.TabIndex = 1;
            this.checkBoxStandard.Text = "标准差";
            this.checkBoxStandard.UseVisualStyleBackColor = false;
            // 
            // checkBoxAverage
            // 
            this.checkBoxAverage.AutoSize = true;
            this.checkBoxAverage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxAverage.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxAverage.Location = new System.Drawing.Point(241, 24);
            this.checkBoxAverage.Name = "checkBoxAverage";
            this.checkBoxAverage.Size = new System.Drawing.Size(68, 18);
            this.checkBoxAverage.TabIndex = 1;
            this.checkBoxAverage.Text = "平均值";
            this.checkBoxAverage.UseVisualStyleBackColor = false;
            // 
            // checkedListBoxValue
            // 
            this.checkedListBoxValue.CheckOnClick = true;
            this.checkedListBoxValue.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkedListBoxValue.FormattingEnabled = true;
            this.checkedListBoxValue.Items.AddRange(new object[] {
            "随机血糖",
            "空腹血糖",
            "餐后血糖",
            "进餐主食数量",
            "OGTT空腹血糖",
            "OGTT餐后血糖",
            "总胆固醇",
            "甘油三酯",
            "HDL-ch",
            "LDL-ch",
            "肌酐",
            "丙氨酸氨基转移酶",
            "尿素氮",
            "天冬氨酸氨基转移酶",
            "Alb/Cr",
            "尿糖",
            "尿蛋白定量",
            "尿酮体",
            "尿Ph",
            "尿尿酸",
            "HbA1c",
            "血氯",
            "血尿酸",
            "血钾",
            "血钠",
            "血CO2CP",
            "血钙",
            "血磷",
            "血清总蛋白",
            "血清白蛋白",
            "空腹胰岛素",
            "空腹C肽",
            "餐后胰岛素",
            "餐后C肽",
            "ICA",
            "GDA65",
            "身高",
            "体重",
            "腰围",
            "臀围",
            "脉（心）率",
            "收缩压1",
            "舒张压1",
            "收缩压2",
            "舒张压2"});
            this.checkedListBoxValue.Location = new System.Drawing.Point(24, 23);
            this.checkedListBoxValue.Name = "checkedListBoxValue";
            this.checkedListBoxValue.Size = new System.Drawing.Size(200, 76);
            this.checkedListBoxValue.TabIndex = 0;
            // 
            // groupBoxDignoPara
            // 
            this.groupBoxDignoPara.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBoxDignoPara.Controls.Add(this.checkBox9);
            this.groupBoxDignoPara.Controls.Add(this.checkBoxMorbiRate);
            this.groupBoxDignoPara.Controls.Add(this.checkBox12);
            this.groupBoxDignoPara.Controls.Add(this.checkBoxMorbi);
            this.groupBoxDignoPara.Controls.Add(this.checkBoxNewMorbi);
            this.groupBoxDignoPara.Controls.Add(this.checkBoxNewMorbiRate);
            this.groupBoxDignoPara.Controls.Add(this.checkedListBoxDigno);
            this.groupBoxDignoPara.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxDignoPara.Location = new System.Drawing.Point(468, 365);
            this.groupBoxDignoPara.Name = "groupBoxDignoPara";
            this.groupBoxDignoPara.Size = new System.Drawing.Size(415, 107);
            this.groupBoxDignoPara.TabIndex = 5;
            this.groupBoxDignoPara.TabStop = false;
            this.groupBoxDignoPara.Text = "诊断参数";
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBox9.Enabled = false;
            this.checkBox9.Location = new System.Drawing.Point(321, 72);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(68, 18);
            this.checkBox9.TabIndex = 1;
            this.checkBox9.Text = "达标率";
            this.checkBox9.UseVisualStyleBackColor = false;
            // 
            // checkBoxMorbiRate
            // 
            this.checkBoxMorbiRate.AutoSize = true;
            this.checkBoxMorbiRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxMorbiRate.Location = new System.Drawing.Point(321, 48);
            this.checkBoxMorbiRate.Name = "checkBoxMorbiRate";
            this.checkBoxMorbiRate.Size = new System.Drawing.Size(68, 18);
            this.checkBoxMorbiRate.TabIndex = 1;
            this.checkBoxMorbiRate.Text = "患病率";
            this.checkBoxMorbiRate.UseVisualStyleBackColor = false;
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBox12.Enabled = false;
            this.checkBox12.Location = new System.Drawing.Point(221, 72);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(82, 18);
            this.checkBox12.TabIndex = 1;
            this.checkBox12.Text = "达标人数";
            this.checkBox12.UseVisualStyleBackColor = false;
            // 
            // checkBoxMorbi
            // 
            this.checkBoxMorbi.AutoSize = true;
            this.checkBoxMorbi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxMorbi.Location = new System.Drawing.Point(221, 48);
            this.checkBoxMorbi.Name = "checkBoxMorbi";
            this.checkBoxMorbi.Size = new System.Drawing.Size(82, 18);
            this.checkBoxMorbi.TabIndex = 1;
            this.checkBoxMorbi.Text = "患病人数";
            this.checkBoxMorbi.UseVisualStyleBackColor = false;
            // 
            // checkBoxNewMorbi
            // 
            this.checkBoxNewMorbi.AutoSize = true;
            this.checkBoxNewMorbi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxNewMorbi.Location = new System.Drawing.Point(221, 24);
            this.checkBoxNewMorbi.Name = "checkBoxNewMorbi";
            this.checkBoxNewMorbi.Size = new System.Drawing.Size(82, 18);
            this.checkBoxNewMorbi.TabIndex = 1;
            this.checkBoxNewMorbi.Text = "新发人数";
            this.checkBoxNewMorbi.UseVisualStyleBackColor = false;
            // 
            // checkBoxNewMorbiRate
            // 
            this.checkBoxNewMorbiRate.AutoSize = true;
            this.checkBoxNewMorbiRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.checkBoxNewMorbiRate.Location = new System.Drawing.Point(321, 24);
            this.checkBoxNewMorbiRate.Name = "checkBoxNewMorbiRate";
            this.checkBoxNewMorbiRate.Size = new System.Drawing.Size(68, 18);
            this.checkBoxNewMorbiRate.TabIndex = 1;
            this.checkBoxNewMorbiRate.Text = "发病率";
            this.checkBoxNewMorbiRate.UseVisualStyleBackColor = false;
            // 
            // checkedListBoxDigno
            // 
            this.checkedListBoxDigno.CheckOnClick = true;
            this.checkedListBoxDigno.FormattingEnabled = true;
            this.checkedListBoxDigno.Items.AddRange(new object[] {
            "1型糖尿病",
            "2型糖尿病",
            "IGT",
            "IFG",
            "高甘油三脂血症",
            "高低密度脂蛋白血症",
            "低高密度脂蛋白血症",
            "高胆固醇血症",
            "高尿酸血症",
            "高血压",
            "肥胖",
            "代谢综合征"});
            this.checkedListBoxDigno.Location = new System.Drawing.Point(23, 23);
            this.checkedListBoxDigno.Name = "checkedListBoxDigno";
            this.checkedListBoxDigno.Size = new System.Drawing.Size(185, 76);
            this.checkedListBoxDigno.TabIndex = 0;
            // 
            // buttonExport
            // 
            this.buttonExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExport.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonExport.Location = new System.Drawing.Point(897, 181);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(87, 27);
            this.buttonExport.TabIndex = 3;
            this.buttonExport.Text = "导出";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonReport.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonReport.Location = new System.Drawing.Point(897, 248);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(87, 27);
            this.buttonReport.TabIndex = 3;
            this.buttonReport.Text = "报表";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // dataGridViewValue
            // 
            this.dataGridViewValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewValue.Location = new System.Drawing.Point(24, 515);
            this.dataGridViewValue.Name = "dataGridViewValue";
            this.dataGridViewValue.RowTemplate.Height = 23;
            this.dataGridViewValue.Size = new System.Drawing.Size(480, 154);
            this.dataGridViewValue.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label5.Location = new System.Drawing.Point(520, 488);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "诊断参数统计结果";
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveTemplate.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveTemplate.Location = new System.Drawing.Point(897, 315);
            this.btnSaveTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(87, 27);
            this.btnSaveTemplate.TabIndex = 8;
            this.btnSaveTemplate.Text = "保存模板";
            this.btnSaveTemplate.UseVisualStyleBackColor = true;
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // errorProvider2
            // 
            this.errorProvider2.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider2.ContainerControl = this;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1137, 684);
            this.Controls.Add(this.btnSaveTemplate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridViewValue);
            this.Controls.Add(this.groupBoxDignoPara);
            this.Controls.Add(this.groupBoxValuePara);
            this.Controls.Add(this.groupBoxTimeSet);
            this.Controls.Add(this.labelGroupResult);
            this.Controls.Add(this.dataGridViewDigno);
            this.Controls.Add(this.buttonTempManage);
            this.Controls.Add(this.groupBoxContrConSet);
            this.Controls.Add(this.groupBoxStatiConSet);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonClean);
            this.Controls.Add(this.buttonGrouping);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "StatisticsForm";
            this.Padding = new System.Windows.Forms.Padding(15, 14, 15, 14);
            this.Text = "条件分组";
            this.Load += new System.EventHandler(this.ConditionGrouping_Load);
            this.Controls.SetChildIndex(this.buttonGrouping, 0);
            this.Controls.SetChildIndex(this.buttonClean, 0);
            this.Controls.SetChildIndex(this.buttonExport, 0);
            this.Controls.SetChildIndex(this.buttonReport, 0);
            this.Controls.SetChildIndex(this.groupBoxStatiConSet, 0);
            this.Controls.SetChildIndex(this.groupBoxContrConSet, 0);
            this.Controls.SetChildIndex(this.buttonTempManage, 0);
            this.Controls.SetChildIndex(this.dataGridViewDigno, 0);
            this.Controls.SetChildIndex(this.labelGroupResult, 0);
            this.Controls.SetChildIndex(this.groupBoxTimeSet, 0);
            this.Controls.SetChildIndex(this.groupBoxValuePara, 0);
            this.Controls.SetChildIndex(this.groupBoxDignoPara, 0);
            this.Controls.SetChildIndex(this.dataGridViewValue, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.btnSaveTemplate, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDigno)).EndInit();
            this.groupBoxTimeSet.ResumeLayout(false);
            this.groupBoxTimeSet.PerformLayout();
            this.groupBoxStatiConSet.ResumeLayout(false);
            this.groupBoxContrConSet.ResumeLayout(false);
            this.groupBoxValuePara.ResumeLayout(false);
            this.groupBoxValuePara.PerformLayout();
            this.groupBoxDignoPara.ResumeLayout(false);
            this.groupBoxDignoPara.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDigno;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelGroupResult;
        private System.Windows.Forms.Button buttonGrouping;
        private System.Windows.Forms.Button buttonClean;
        private System.Windows.Forms.Button buttonTempManage;
        private System.Windows.Forms.GroupBox groupBoxTimeSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.GroupBox groupBoxStatiConSet;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxContrConSet;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBoxValuePara;
        private System.Windows.Forms.CheckBox checkBoxQuar;
        private System.Windows.Forms.CheckBox checkBoxMedian;
        private System.Windows.Forms.CheckBox checkBoxMin;
        private System.Windows.Forms.CheckBox checkBoxMax;
        private System.Windows.Forms.CheckBox checkBoxStandard;
        private System.Windows.Forms.CheckBox checkBoxAverage;
        private System.Windows.Forms.CheckedListBox checkedListBoxValue;
        private System.Windows.Forms.GroupBox groupBoxDignoPara;
        private System.Windows.Forms.CheckBox checkBoxNewMorbiRate;
        private System.Windows.Forms.CheckedListBox checkedListBoxDigno;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBoxMorbiRate;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.CheckBox checkBoxMorbi;
        private System.Windows.Forms.CheckBox checkBoxNewMorbi;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.DataGridView dataGridViewValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAdd1;
        private System.Windows.Forms.Button btnAdd2;
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.ErrorProvider errorProvider2;



    }
}