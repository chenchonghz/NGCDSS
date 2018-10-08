namespace CDSS
{
    partial class Statistic
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
            this.tabControlStatistic = new System.Windows.Forms.TabControl();
            this.tabPageConsult = new System.Windows.Forms.TabPage();
            this.tabPageStatistic = new System.Windows.Forms.TabPage();
            this.tabControlStatistic.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlStatistic
            // 
            this.tabControlStatistic.Controls.Add(this.tabPageConsult);
            this.tabControlStatistic.Controls.Add(this.tabPageStatistic);
            this.tabControlStatistic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlStatistic.Location = new System.Drawing.Point(0, 0);
            this.tabControlStatistic.Name = "tabControlStatistic";
            this.tabControlStatistic.SelectedIndex = 0;
            this.tabControlStatistic.Size = new System.Drawing.Size(919, 555);
            this.tabControlStatistic.TabIndex = 0;
            // 
            // tabPageConsult
            // 
            this.tabPageConsult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPageConsult.Location = new System.Drawing.Point(4, 22);
            this.tabPageConsult.Name = "tabPageConsult";
            this.tabPageConsult.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConsult.Size = new System.Drawing.Size(911, 529);
            this.tabPageConsult.TabIndex = 0;
            this.tabPageConsult.Text = "≤È—Ø";
            this.tabPageConsult.UseVisualStyleBackColor = true;
            // 
            // tabPageStatistic
            // 
            this.tabPageStatistic.Location = new System.Drawing.Point(4, 22);
            this.tabPageStatistic.Name = "tabPageStatistic";
            this.tabPageStatistic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStatistic.Size = new System.Drawing.Size(911, 529);
            this.tabPageStatistic.TabIndex = 1;
            this.tabPageStatistic.Text = "Õ≥º∆";
            this.tabPageStatistic.UseVisualStyleBackColor = true;
            // 
            // Statistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CDSS.Properties.Resources.Main_Bk;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(919, 555);
            this.Controls.Add(this.tabControlStatistic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Statistic";
            this.Text = "Statistic";
            this.tabControlStatistic.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlStatistic;
        private System.Windows.Forms.TabPage tabPageConsult;
        private System.Windows.Forms.TabPage tabPageStatistic;
    }
}