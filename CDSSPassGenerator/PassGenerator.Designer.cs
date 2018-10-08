namespace CDSSUserRegist
{
    partial class PassGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassGenerator));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txbMd5Pwd = new System.Windows.Forms.TextBox();
            this.PwdMD5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txb_UserPwd = new System.Windows.Forms.TextBox();
            this.btn_Regist = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.btn_Regist);
            this.panel1.Location = new System.Drawing.Point(12, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 187);
            this.panel1.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.37037F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.62963F));
            this.tableLayoutPanel1.Controls.Add(this.txbMd5Pwd, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.PwdMD5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txb_UserPwd, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(324, 109);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txbMd5Pwd
            // 
            this.txbMd5Pwd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbMd5Pwd.Location = new System.Drawing.Point(69, 70);
            this.txbMd5Pwd.Name = "txbMd5Pwd";
            this.txbMd5Pwd.Size = new System.Drawing.Size(246, 21);
            this.txbMd5Pwd.TabIndex = 6;
            // 
            // PwdMD5
            // 
            this.PwdMD5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PwdMD5.AutoSize = true;
            this.PwdMD5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.PwdMD5.Location = new System.Drawing.Point(10, 74);
            this.PwdMD5.Name = "PwdMD5";
            this.PwdMD5.Size = new System.Drawing.Size(53, 12);
            this.PwdMD5.TabIndex = 4;
            this.PwdMD5.Text = "加  密：";
            this.PwdMD5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.label4.Location = new System.Drawing.Point(10, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "明　码：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txb_UserPwd
            // 
            this.txb_UserPwd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txb_UserPwd.Location = new System.Drawing.Point(69, 15);
            this.txb_UserPwd.Name = "txb_UserPwd";
            this.txb_UserPwd.Size = new System.Drawing.Size(246, 21);
            this.txb_UserPwd.TabIndex = 3;
            // 
            // btn_Regist
            // 
            this.btn_Regist.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Regist.BackgroundImage")));
            this.btn_Regist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Regist.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Regist.Location = new System.Drawing.Point(147, 151);
            this.btn_Regist.Name = "btn_Regist";
            this.btn_Regist.Size = new System.Drawing.Size(46, 23);
            this.btn_Regist.TabIndex = 0;
            this.btn_Regist.Text = "加密";
            this.btn_Regist.UseVisualStyleBackColor = true;
            this.btn_Regist.Click += new System.EventHandler(this.btn_Regist_Click);
            // 
            // PassGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(368, 213);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PassGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密码生成器";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_UserPwd;
        public System.Windows.Forms.Button btn_Regist;
        private System.Windows.Forms.TextBox txbMd5Pwd;
        private System.Windows.Forms.Label PwdMD5;
    }
}

