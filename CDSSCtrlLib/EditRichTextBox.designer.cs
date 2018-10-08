namespace CDSSCtrlLib
{
    partial class EditRichTextBox
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRichTextBox));
            this.panel1 = new System.Windows.Forms.Panel();
            this.RtxtContent = new CDSSCtrlLib.CDSSRichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lbldo = new System.Windows.Forms.Label();
            this.lblundo = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.RtxtContent);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 120);
            this.panel1.TabIndex = 0;
            // 
            // RtxtContent
            // 
            this.RtxtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RtxtContent.Location = new System.Drawing.Point(0, 0);
            this.RtxtContent.Name = "RtxtContent";
            this.RtxtContent.Size = new System.Drawing.Size(140, 120);
            this.RtxtContent.TabIndex = 1;
            this.RtxtContent.Text = "";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.lbldo);
            this.panel2.Controls.Add(this.lblundo);
            this.panel2.Location = new System.Drawing.Point(140, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(50, 69);
            this.panel2.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "建议";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button3_Click);
            // 
            // lbldo
            // 
            this.lbldo.Image = ((System.Drawing.Image)(resources.GetObject("lbldo.Image")));
            this.lbldo.Location = new System.Drawing.Point(25, 1);
            this.lbldo.Name = "lbldo";
            this.lbldo.Size = new System.Drawing.Size(21, 21);
            this.lbldo.TabIndex = 1;
            this.lbldo.Click += new System.EventHandler(this.lbldo_Click);
            // 
            // lblundo
            // 
            this.lblundo.Image = ((System.Drawing.Image)(resources.GetObject("lblundo.Image")));
            this.lblundo.Location = new System.Drawing.Point(6, 1);
            this.lblundo.Name = "lblundo";
            this.lblundo.Size = new System.Drawing.Size(21, 21);
            this.lblundo.TabIndex = 0;
            this.lblundo.Click += new System.EventHandler(this.lblundo_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(3, 44);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // EditRichTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "EditRichTextBox";
            this.Size = new System.Drawing.Size(190, 120);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbldo;
        private System.Windows.Forms.Label lblundo;
        private System.Windows.Forms.Button button1;
        private CDSSRichTextBox RtxtContent;
        private System.Windows.Forms.Button btnClose;


    }
}
