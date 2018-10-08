using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDSSCtrlLib
{
    /// <summary>
    /// ������Ϣ¼�봰���и��Ӵ���Ļ���
    /// </summary>
    public partial class InfoFormBaseClass : Form
    {
        private Color ControlBackColor = System.Drawing.Color.FromArgb(205, 228, 236);
        private Color ControlBackColorLighter = System.Drawing.Color.FromArgb(185, 208, 216);

        public delegate void ShowNextPageEvent(object source, string formName);
        public event ShowNextPageEvent ShowNextPage;

        public InfoFormBaseClass()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ѡ���Ƿ�ʹ����һҳ��ť
        /// </summary>
        /// <value>
        /// ʹ��Ϊtrue
        /// </value>
        [Category("Use NextPage Button"),
        Description("Flag. If true, use nextButton, otherwise."),
        DefaultValue(false),
        TypeConverter(typeof(bool)),
        Editor("System.Boolean", typeof(bool))]
        public bool UseNextButton
        {
            get 
            {
                if (this.buttonNextPage.Visible && this.AcceptButton == this.buttonNextPage)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set 
            {
                if (value)
                {
                    this.buttonNextPage.Visible = true;
                    this.AcceptButton = this.buttonNextPage;
                }
                else
                {
                    this.buttonNextPage.Visible = false;
                    this.AcceptButton = null;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.UseNextButton)
            {
                this.buttonNextPage.BringToFront();
                this.buttonNextPage.Location = new Point(this.Location.X + this.Size.Width - this.buttonNextPage.Size.Width - 50, this.Location.Y + this.Size.Height - this.buttonNextPage.Size.Height - 20);
            }
                
            base.OnPaint(e);
        }

        /// <summary>
        /// ���洰�������
        /// </summary>
        public virtual void LoadDataFromUIToVar()
        {

        }


        //��Ϊ��InfoEnter�����ж�Ӧ��������ؽ����ж������˸÷�������û��Override���ιʽ���ע�͵�
        ///// <summary>
        ///// ��ս�������
        ///// </summary>
        //public virtual void ClearData()
        //{
            
        //}

        /// <summary>
        /// ���ص�ǰ��������
        /// </summary>
        public virtual void LoadDataFromVarToUI()
        {

        }


        /// <summary>
        /// IsModified����,����ָʾ������������޷����ı�
        /// </summary>
        private bool isModified = false;
        public  bool IsModified
        {
            get
            {
                return isModified;
            }
            set
            {
                isModified = value;
            }
        }


        /// <summary>
        /// LastErrorInfo���ԣ����ڱ������ĳ�����Ϣ
        /// </summary>
        private string lastErrorInfo;
        public string LastErrorInfo
        {
            get
            {
                return lastErrorInfo;
            }
            set
            {
                lastErrorInfo = value;
            }
        }

        public event EventHandler DataChanged;
        protected void RaiseDataChangedEvent(object sender, EventArgs e)
        {
            EventHandler temp = DataChanged;
            if (temp != null)
                temp(sender, e);
        }

        //Created by ZQY 2009/1/2
        private void RenderControls(Control.ControlCollection Controls)
        {
            foreach (Control control in Controls)
            {

                if (control is Panel)
                {
                    //((Panel)control).BackgroundImage = CDSSCtrlLib.Resources.Main_Bk;
                    ((Panel)control).BackgroundImageLayout = ImageLayout.Stretch;

                    RenderControls(control.Controls);
                }
                if (control is GroupBox)
                {
                    //((GroupBox)control).BackgroundImage = CDSSCtrlLib.Resources.Main_Bk;
                    ((GroupBox)control).BackgroundImageLayout = ImageLayout.Stretch;

                    RenderControls(control.Controls);
                }
                else if (control is Label)
                {
                    ((Label)control).BackColor = ControlBackColor;
                }
                else if (control is RichTextBox)
                {
                    ((RichTextBox)control).BackColor = ControlBackColorLighter;
                }
                else if (control is UserControl)
                {
                    ((UserControl)control).BackColor = ControlBackColor;
                    RenderControls(control.Controls);
                }
                else if (control is RadioButton)
                {
                    ((RadioButton)control).BackColor = ControlBackColor;
                }
                else if (control is Button)
                {
                    //((Button)control).BackgroundImage = CDSSCtrlLib.Resources.large_btn_bk;
                    ((Button)control).BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (control is CheckBox)
                {
                    if (((CheckBox)control).Appearance == Appearance.Button)
                    {
                        //((CheckBox)control).BackgroundImage = CDSSCtrlLib.Resources.large_btn_bk;
                        ((CheckBox)control).BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else
                    {
                        ((CheckBox)control).BackColor = ControlBackColor;
                    }
                }
            }

        }
        //ֻ�����״���ʾ����ʱ�Ż����� Shown �¼������ִ�е���С������󻯡���ԭ�����ء���ʾ����Ч�������»��Ʋ����������������¼���
        //Created by ZQY 2009/1/2
        private void InfoFormBaseClass_Shown(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //this.BackgroundImage = CDSS.Properties.Resources.Main_Bk;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            RenderControls(this.Controls);
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            if (this.ShowNextPage != null)
            {
                this.ShowNextPage.Invoke(this, this.Name);
            }
        }  
    }
}