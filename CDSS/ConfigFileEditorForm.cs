using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDSS
{
    public partial class ConfigFileEditorForm : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigFileEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当窗体可见状态变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigFileEditorForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.configFileEditorUserControl1.isCellValueChanged == true)
            {
                DialogResult dr= MessageBox.Show("数据内容未全部保存，是否保存？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    this.configFileEditorUserControl1.Save();
                }
            }
        }
    }
}