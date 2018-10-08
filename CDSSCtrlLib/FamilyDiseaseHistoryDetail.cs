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



#region ����������
        /// <summary>
        /// ���ñ���
        /// </summary>
        public String Title
        {
            set
            {
                this.lblTitle.Text = "����";
                this.lblTitle.Text += value;
                this.lblTitle.Text += "ʷ��";
            }
        }

        /// <summary>
        /// �����ֵܽ�����
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
        /// ������Ů��
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
        /// ��ȡ�����á����ס���ѡ���״̬
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
        /// ��ȡ�����á�ĸ�ס���ѡ���״̬
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
        /// ��ȡ�����á��ֵܽ��á���ѡ���״̬
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
        /// ��ȡ�������ֵܽ����л��˲�������
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
        /// ��ȡ�����á���Ů����ѡ���״̬
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
        /// ��ȡ��������Ů�л��˲�������
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
        /// ��ȡ�����á���������ѡ���״̬
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
        /// ��ȡ����������������Ա���˲�����ϸ���
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

#region ϵͳ�¼�

        /// <summary>
        /// ������رա�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_Click(object sender, EventArgs e)
        {
            if (this.Visible)
                this.Hide();
        }

        /// <summary>
        /// ������رհ�ť���򣬽��ı��ĳɺ�ɫ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_MouseEnter(object sender, EventArgs e)
        {
            this.lblClose.ForeColor = Color.Red;
        }

        /// <summary>
        /// ����뿪�رհ�ť���򣬽��ı��ĳɺ�ɫ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_MouseLeave(object sender, EventArgs e)
        {
            this.lblClose.ForeColor = Color.Black;
        }

        /// <summary>
        /// ����ֵܽ��ø�ѡ��
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
        /// �����Ů��ѡ��
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
        /// ���������ѡ��
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
        /// �ɼ��Է����仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FamilyDiseaseHistoryDetail_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible && bModified)
                RaiseDataChangedEvent(this, e);
        }

#endregion

#region �û��¼�
        private bool bModified = false;
        protected void DataModified(object sender, EventArgs e)
        {
            if (!bModified)
                bModified = true;
        }

        /// <summary>
        /// �����Ѹ����¼�������֪ͨ������
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