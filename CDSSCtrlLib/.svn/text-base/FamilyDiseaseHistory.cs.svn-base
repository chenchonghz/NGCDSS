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

#region 系统事件
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

#region 用户事件
        /// <summary>
        /// 接收子窗体的数据更改事件，并转发给上层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFDHD_DataChanged(object sender, EventArgs e)
        {
            RaiseDataChangedEvent(this, e);
        }

        /// <summary>
        /// 通知上层数据已经更改
        /// </summary>
        [Description("用户更改了该控件界面上的数据之后发生")]
        public event EventHandler DataChanged;
        private void RaiseDataChangedEvent(object sender, EventArgs e)
        {
            EventHandler temp = DataChanged;
            if (temp != null)
                temp(sender, e);
        }

#endregion


#region 公开的属性


        /// <summary>
        /// 读取或设置疾病名称
        /// </summary>
        [Category("用户数据"),
         Description("读取或设置疾病名称"),
         DefaultValue("疾病名称")]
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
        /// 设置兄弟姐妹数
        /// </summary>
        [Category("用户数据"),
        Description("设置兄弟姐妹数"),
         DefaultValue(0)]
        public int SisBrotherCount
        {
            set
            {
                this.frmFDHD.SisBrotherCount = value;
            }
        }

        /// <summary>
        /// 设置子女数
        /// </summary>
        [Category("用户数据"),
        Description("设置子女数"),
         DefaultValue(0)]
        public int ChildrenCount
        {
            set
            {
                this.frmFDHD.ChildrenCount = value;
            }
        }

        /// <summary>
        /// 获取或设置父亲是否患有该疾病
        /// </summary>
        [Category("用户数据"),
        Description("获取或设置父亲是否患有该疾病"),
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
        /// 获取或设置母亲是否患有该疾病
        /// </summary>
        [Category("用户数据"),
        Description("获取或设置母亲是否患有该疾病"),
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
        /// 获取或设置兄弟姐妹是否患有该疾病
        /// </summary>
        [Category("用户数据"),
        Description("获取或设置兄弟姐妹是否患有该疾病"),
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
        /// 获取或设置兄弟姐妹中患有该疾病的人数
        /// </summary>
        [Category("用户数据"),
        Description("获取或设置兄弟姐妹中患有该疾病的人数病"),
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
        /// 获取或设置子女是否患有该疾病
        /// </summary>
        [Category("用户数据"),
        Description("获取或设置子女是否患有该疾病"),
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
        /// 获取或设置子女中患有该疾病的人数
        /// </summary>
        [Category("用户数据"),
        Description("获取或设置子女中患有该疾病的人数病"),
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
        /// 获取或设置其他相关人员是否患有该疾病
        /// </summary>
        [Category("用户数据"),
        Description("获取或设置其他相关人员是否患有该疾病"),
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
        /// 获取或设置其他相关人员中患有该疾病的详细情况
        /// </summary>
        [Category("用户数据"),
        Description("获取或设置其他相关人员中患有该疾病的详细情况"),
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

#region 功能函数
        /// <summary>
        /// 清空界面数据
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
