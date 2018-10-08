namespace CDSS
{
    partial class ThreeMeals
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
            this.btn_Breakfast = new System.Windows.Forms.Button();
            this.btn_Lunch = new System.Windows.Forms.Button();
            this.btn_Supper = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Breakfast
            // 
            this.btn_Breakfast.Location = new System.Drawing.Point(12, 12);
            this.btn_Breakfast.Name = "btn_Breakfast";
            this.btn_Breakfast.Size = new System.Drawing.Size(132, 31);
            this.btn_Breakfast.TabIndex = 0;
            this.btn_Breakfast.UseVisualStyleBackColor = true;
            this.btn_Breakfast.Click += new System.EventHandler(this.btn_Breakfast_Click);
            // 
            // btn_Lunch
            // 
            this.btn_Lunch.Location = new System.Drawing.Point(12, 49);
            this.btn_Lunch.Name = "btn_Lunch";
            this.btn_Lunch.Size = new System.Drawing.Size(132, 31);
            this.btn_Lunch.TabIndex = 0;
            this.btn_Lunch.UseVisualStyleBackColor = true;
            this.btn_Lunch.Click += new System.EventHandler(this.btn_Lunch_Click);
            // 
            // btn_Supper
            // 
            this.btn_Supper.Location = new System.Drawing.Point(12, 86);
            this.btn_Supper.Name = "btn_Supper";
            this.btn_Supper.Size = new System.Drawing.Size(132, 31);
            this.btn_Supper.TabIndex = 0;
            this.btn_Supper.UseVisualStyleBackColor = true;
            this.btn_Supper.Click += new System.EventHandler(this.btn_Supper_Click);
            // 
            // ThreeMeals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(157, 126);
            this.Controls.Add(this.btn_Supper);
            this.Controls.Add(this.btn_Lunch);
            this.Controls.Add(this.btn_Breakfast);
            this.Name = "ThreeMeals";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Èý²Í·ÖÅä";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Breakfast;
        private System.Windows.Forms.Button btn_Lunch;
        private System.Windows.Forms.Button btn_Supper;
    }
}