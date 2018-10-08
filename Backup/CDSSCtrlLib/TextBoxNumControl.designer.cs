namespace CDSSCtrlLib
{
    partial class TextBoxNumControl
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
            this.txtNum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtNum
            // 
            this.txtNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNum.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNum.Location = new System.Drawing.Point(0, 0);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(101, 21);
            this.txtNum.TabIndex = 0;
            this.txtNum.ImeModeChanged += new System.EventHandler(this.txtNum_ImeModeChanged);
            this.txtNum.TextChanged += new System.EventHandler(this.txtNum_TextChanged);
            this.txtNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNum_KeyPress);
            this.txtNum.Validating += new System.ComponentModel.CancelEventHandler(this.txtNum_Validating);
            // 
            // TextBoxNumControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNum);
            this.Name = "TextBoxNumControl";
            this.Size = new System.Drawing.Size(101, 22);
            this.Load += new System.EventHandler(this.TextBoxNumControl1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNum;
    }
}
