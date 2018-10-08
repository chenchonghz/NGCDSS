using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace CDSS
{
    public partial class AddSelectTemplate : Form
    {
        public AddSelectTemplate()
        {
            InitializeComponent();
        }

        private string oldfileName = "";
        private string oldfileMark = "";
        private bool isNew = false;
        private string filePath = "";
        private string fileName = "";

        /// <summary>
        /// 文件存放的路径
        /// </summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        /// <summary>
        /// 返回文件名（含地址）
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtTemplateName.Text == "")
            {
                MessageBox.Show("模板名称不能为空", "提示");
                return;
            }
            if (oldfileName == txtTemplateName.Text && oldfileMark == txtRemark.Text)
            {
                this.Close();
                return;
            }
            if (File.Exists(filePath + txtTemplateName.Text + ".xml"))
            {
                if (DialogResult.Yes == MessageBox.Show("已存在同名文件，是否覆盖原文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    this.fileName = filePath + txtTemplateName.Text + ".xml";
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                this.fileName = filePath + txtTemplateName.Text + ".xml";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtTemplateName.SelectAll();
            this.Close();
        }

        private void txtTemplateName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string sign = "?\"//\\<>*|:";
            if (sign.IndexOf(e.KeyChar) > -1)
                e.Handled = true;
        }

        private void AddSelectTemplate_Load(object sender, EventArgs e)
        {
            if (!isNew)
            {
                oldfileName = txtTemplateName.Text;
                oldfileMark = txtRemark.Text;
            }
        }
    }
}