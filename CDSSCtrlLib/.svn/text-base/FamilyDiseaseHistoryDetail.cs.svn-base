using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDSSCtrlLib
{
    public partial class FamilyDiseaseHistoryDetail : Form
    {
        public FamilyDiseaseHistoryDetail()
        {
            InitializeComponent();
            this.SisBrotherCount = 0;
            this.ChildrenCount = 0;
        }



#region 公开的属性
        /// <summary>
        /// 设置标题
        /// </summary>
        public String Title
        {
            set
            {
                this.lblTitle.Text = "家族";
                this.lblTitle.Text += value;
                this.lblTitle.Text += "史：";
            }
        }

        /// <summary>
        /// 设置兄弟姐妹数
        /// </summary>
        private int nSisBrotherCount;
        public int SisBrotherCount
        {
            set
            {
                nSisBrotherCount = value;
                if (nSisBrotherCount != this.cmbSisBrotherHasCount.Items.Count - 1)
                {
                    this.cmbSisBrotherHasCount.Items.Clear();
                    for (int i = 0; i <= nSisBrotherCount; ++i)
                        this.cmbSisBrotherHasCount.Items.Add(i);
                    if (nSisBrotherCount < 1)
                    {
                        this.chkSisBrother.Checked = false;
                        this.pnlSisBrother.Enabled = false;
                    }
                    else
                    {
                        this.pnlSisBrother.Enabled = true;
                    }
                    this.cmbSisBrotherHasCount.Text = "0";
                }
            }
        }

        /// <summary>
        /// 设置子女数
        /// </summary>
        private int nChildrenCount;
        public int ChildrenCount
        {
            set
            {
                nChildrenCount = value;
                if (nChildrenCount != this.cmbChildrenHasCount.Items.Count-1)
                {
                    this.cmbChildrenHasCount.Items.Clear();
                    for (int i = 0; i <= nChildrenCount; ++i)
                        this.cmbChildrenHasCount.Items.Add(i);
                    if (nChildrenCount < 1)
                    {
                        this.chkChildren.Checked = false;
                        this.pnlChildren.Enabled = false;
                    }
                    else
                    {
                        this.pnlChildren.Enabled = true;
                    }
                    this.cmbChildrenHasCount.Text = "0";
                }
            }
        }

        /// <summary>
        /// 获取或设置“父亲”复选框的状态
        /// </summary>
        public bool FatherHas
        {
            get
            {
                return this.chkFather.Checked;
            }
            set
            {
                this.chkFather.Checked = value;
            }
        }

        /// <summary>
        /// 获取或设置“母亲”复选框的状态
        /// </summary>
        public bool MatherHas
        {
            get
            {
                return this.chkMother.Checked;
            }
            set
            {
                this.chkMother.Checked = value;
            }
        }

        /// <summary>
        /// 获取或设置“兄弟姐妹”复选框的状态
        /// </summary>
        public bool SisBrotherHas
        {
            get
            {
                return this.chkSisBrother.Checked;
            }
            set
            {
                this.chkSisBrother.Checked = value;
                this.cmbSisBrotherHasCount.Enabled = value;
                if (!value)
                    this.cmbSisBrotherHasCount.Text = "0";
            }
        }

        /// <summary>
        /// 获取或设置兄弟姐妹中患此病的人数
        /// </summary>
        public int SisBrotherHasCount
        {
            get
            {
                if (this.cmbSisBrotherHasCount.Text == string.Empty)
                    return 0;
                else
                    return int.Parse(this.cmbSisBrotherHasCount.Text);
            }
            set
            {
                if (value > nSisBrotherCount)
                    value = 0;
                this.cmbSisBrotherHasCount.Text = value.ToString();
            }
        }

        /// <summary>
        /// 获取或设置“子女”复选框的状态
        /// </summary>
        public bool ChildrenHas
        {
            get
            {
                return this.chkChildren.Checked;
            }
            set
            {
                this.chkChildren.Checked = value;
                this.cmbChildrenHasCount.Enabled = value;
                if (!value)
                    this.cmbChildrenHasCount.Text = "0";
            }
        }
        
        /// <summary>
        /// 获取或设置子女中患此病的人数
        /// </summary>
        public int ChildrenHasCount
        {
            get
            {
                if (this.cmbChildrenHasCount.Text == string.Empty)
                    return 0;
                else
                    return int.Parse(this.cmbChildrenHasCount.Text);
            }
            set
            {
                if (value > nChildrenCount)
                    value = 0;
                this.cmbChildrenHasCount.Text = value.ToString();
            }
        }

        /// <summary>
        /// 获取或设置“其他”复选框的状态
        /// </summary>
        public bool OtherHas
        {
            get
            {
                return this.chkOther.Checked;
            }
            set
            {
                this.chkOther.Checked = value;
                this.txtOtherDetail.Enabled = value;
            }
        }

        /// <summary>
        /// 获取或设置其他类型人员患此病的详细情况
        /// </summary>
        public string OtherHasDetail
        {
            get
            {
                return this.txtOtherDetail.Text;
            }
            set
            {
                this.txtOtherDetail.Text = value;
            }
        }

#endregion

#region 系统事件

        /// <summary>
        /// 点击“关闭”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_Click(object sender, EventArgs e)
        {
            if (this.Visible)
                this.Hide();
        }

        /// <summary>
        /// 鼠标进入关闭按钮区域，将文本改成红色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_MouseEnter(object sender, EventArgs e)
        {
            this.lblClose.ForeColor = Color.Red;
        }

        /// <summary>
        /// 鼠标离开关闭按钮区域，将文本改成黑色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_MouseLeave(object sender, EventArgs e)
        {
            this.lblClose.ForeColor = Color.Black;
        }

        /// <summary>
        /// 点击兄弟姐妹复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSisBrother_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            this.cmbSisBrotherHasCount.Enabled = this.chkSisBrother.Checked;
            if (!this.cmbSisBrotherHasCount.Enabled)
                this.cmbSisBrotherHasCount.Text = "0";
        }

        /// <summary>
        /// 点击子女复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkChildren_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            this.cmbChildrenHasCount.Enabled = this.chkChildren.Checked;
            if (!this.cmbChildrenHasCount.Enabled)
                this.cmbChildrenHasCount.Text = "0";
        }

        /// <summary>
        /// 点击其他复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            this.txtOtherDetail.Enabled = this.chkOther.Checked;
            if (!this.txtOtherDetail.Enabled)
                this.txtOtherDetail.Text = String.Empty;
        }

        
        /// <summary>
        /// 可见性发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FamilyDiseaseHistoryDetail_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible && bModified)
                RaiseDataChangedEvent(this, e);
        }

#endregion

#region 用户事件
        private bool bModified = false;
        protected void DataModified(object sender, EventArgs e)
        {
            if (!bModified)
                bModified = true;
        }

        /// <summary>
        /// 数据已更改事件，用于通知父窗口
        /// </summary>
        public event EventHandler DataChangeed;
        private void RaiseDataChangedEvent(object sender, EventArgs e)
        {
            EventHandler temp = DataChangeed;
            if (temp != null)
                temp(sender, e);
        }

#endregion

    }
}