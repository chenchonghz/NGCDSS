using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CDSSCtrlLib
{
    public partial class FamilyDiseaseHistory : UserControl
    {
        private FamilyDiseaseHistoryDetail frmFDHD = new FamilyDiseaseHistoryDetail();

        public FamilyDiseaseHistory()
        {
            InitializeComponent();
            this.frmFDHD.DataChangeed += this.frmFDHD_DataChanged;
        }

#region ϵͳ�¼�
        private void lblDiseaseName_Click(object sender, EventArgs e)
        {
            
            if (frmFDHD.Visible)
                return;
            this.lblDiseaseName.Image = CDSSCtrlLib.Properties.Resources.DiagnosisSteps_press;
            frmFDHD.Location = this.lblDiseaseName.PointToScreen(new Point(-1, this.lblDiseaseName.Height - 1));
            frmFDHD.Show(this);
            this.Focus();
            this.lblDiseaseName.Image = CDSSCtrlLib.Properties.Resources.DiagnosisSteps_normal;
          
        }

        private void FamilyDiseaseHistory_Leave(object sender, EventArgs e)
        {
            if (frmFDHD.Visible)
                frmFDHD.Visible = false;
        }
#endregion

#region �û��¼�
        /// <summary>
        /// �����Ӵ�������ݸ����¼�����ת�����ϲ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFDHD_DataChanged(object sender, EventArgs e)
        {
            RaiseDataChangedEvent(this, e);
        }

        /// <summary>
        /// ֪ͨ�ϲ������Ѿ�����
        /// </summary>
        [Description("�û������˸ÿؼ������ϵ�����֮����")]
        public event EventHandler DataChanged;
        private void RaiseDataChangedEvent(object sender, EventArgs e)
        {
            EventHandler temp = DataChanged;
            if (temp != null)
                temp(sender, e);
        }

#endregion


#region ����������


        /// <summary>
        /// ��ȡ�����ü�������
        /// </summary>
        [Category("�û�����"),
         Description("��ȡ�����ü�������"),
         DefaultValue("��������")]
        public string DiseaseName
        {
            get
            {
                return this.lblDiseaseName.Text;
            }
            set
            {
                this.lblDiseaseName.Text = value;
                this.frmFDHD.Title = value;
            }
        }

        /// <summary>
        /// �����ֵܽ�����
        /// </summary>
        [Category("�û�����"),
        Description("�����ֵܽ�����"),
         DefaultValue(0)]
        public int SisBrotherCount
        {
            set
            {
                this.frmFDHD.SisBrotherCount = value;
            }
        }

        /// <summary>
        /// ������Ů��
        /// </summary>
        [Category("�û�����"),
        Description("������Ů��"),
         DefaultValue(0)]
        public int ChildrenCount
        {
            set
            {
                this.frmFDHD.ChildrenCount = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ø����Ƿ��иü���
        /// </summary>
        [Category("�û�����"),
        Description("��ȡ�����ø����Ƿ��иü���"),
         DefaultValue(false)]
        public bool FatherHas
        {
            get
            {
                return this.frmFDHD.FatherHas;
            }
            set
            {
                this.frmFDHD.FatherHas = value;
            }
        }

        /// <summary>
        /// ��ȡ������ĸ���Ƿ��иü���
        /// </summary>
        [Category("�û�����"),
        Description("��ȡ������ĸ���Ƿ��иü���"),
         DefaultValue(false)]
        public bool MatherHas
        {
            get
            {
                return this.frmFDHD.MatherHas;
            }
            set
            {
                this.frmFDHD.MatherHas = value;
            }
        }

        /// <summary>
        /// ��ȡ�������ֵܽ����Ƿ��иü���
        /// </summary>
        [Category("�û�����"),
        Description("��ȡ�������ֵܽ����Ƿ��иü���"),
         DefaultValue(false)]
        public bool SisBrotherHas
        {
            get
            {
                return this.frmFDHD.SisBrotherHas;
            }
            set
            {
                this.frmFDHD.SisBrotherHas = value;
            }
        }

        /// <summary>
        /// ��ȡ�������ֵܽ����л��иü���������
        /// </summary>
        [Category("�û�����"),
        Description("��ȡ�������ֵܽ����л��иü�����������"),
         DefaultValue(0)]
        public int SisBrotherHasCount
        {
            get
            {
                return this.frmFDHD.SisBrotherHasCount;
            }
            set
            {
                this.frmFDHD.SisBrotherHasCount = value;
            }
        }

        /// <summary>
        /// ��ȡ��������Ů�Ƿ��иü���
        /// </summary>
        [Category("�û�����"),
        Description("��ȡ��������Ů�Ƿ��иü���"),
         DefaultValue(false)]
        public bool ChildrenHas
        {
            get
            {
                return this.frmFDHD.ChildrenHas;
            }
            set
            {
                this.frmFDHD.ChildrenHas = value;
            }
        }

        /// <summary>
        /// ��ȡ��������Ů�л��иü���������
        /// </summary>
        [Category("�û�����"),
        Description("��ȡ��������Ů�л��иü�����������"),
         DefaultValue(0)]
        public int ChildrenHasCount
        {
            get
            {
                return this.frmFDHD.ChildrenHasCount;
            }
            set
            {
                this.frmFDHD.ChildrenHasCount = value;
            }
        }

        /// <summary>
        /// ��ȡ���������������Ա�Ƿ��иü���
        /// </summary>
        [Category("�û�����"),
        Description("��ȡ���������������Ա�Ƿ��иü���"),
         DefaultValue(false)]
        public bool OtherHas
        {
            get
            {
                return this.frmFDHD.OtherHas;
            }
            set
            {
                this.frmFDHD.OtherHas = value;
            }
        }

        /// <summary>
        /// ��ȡ���������������Ա�л��иü�������ϸ���
        /// </summary>
        [Category("�û�����"),
        Description("��ȡ���������������Ա�л��иü�������ϸ���"),
         DefaultValue("")]
        public string OtherHasDetail
        {
            get
            {
                return this.frmFDHD.OtherHasDetail;
            }
            set
            {
                this.frmFDHD.OtherHasDetail = value;
            }
        }

#endregion

#region ���ܺ���
        /// <summary>
        /// ��ս�������
        /// </summary>
        public void ClearData()
        {
            this.SisBrotherCount = 0;
            this.ChildrenCount = 0;
            this.FatherHas = false;
            this.MatherHas = false;
            this.SisBrotherHas = false;
            this.SisBrotherHasCount = 0;
            this.ChildrenHas = false;
            this.ChildrenHasCount = 0;
            this.OtherHas = false;
            this.OtherHasDetail = string.Empty;
        }
#endregion

        private void lblDiseaseName_MouseEnter(object sender, EventArgs e)
        {
            this.lblDiseaseName.Image = CDSSCtrlLib.Properties.Resources.DiagnosisSteps_over;
        }

        private void lblDiseaseName_MouseLeave(object sender, EventArgs e)
        {
            this.lblDiseaseName.Image = CDSSCtrlLib.Properties.Resources.DiagnosisSteps_normal;
        }

    }
}
