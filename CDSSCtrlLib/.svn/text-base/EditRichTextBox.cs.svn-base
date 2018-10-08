using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

/*****************************************
 *
 *  Author:  ZX
 *  Date:    2010-03-24
 *  Description:可编辑RichText控件
 * 
 *****************************************/

namespace CDSSCtrlLib
{
    public partial class EditRichTextBox : UserControl
    {
        #region --Fields/Attributes/Propertys--

        private Stack<string> s_go = new Stack<string>();               ///保留取消撤销值的堆栈
        private Stack<string> s_back = new Stack<string>();             ///保留撤销值的堆栈
        private Popup _popup;                                           ///可浮动控件
        private CDSSRichTextBox txtbox;                                 ///可浮动控件上可编辑的RichText
        public delegate void CloseHandler(object sender, ContentArgs e);///处理菜单关闭的委托
        public event CloseHandler Closing;                              ///控件关闭的事件
        public class ContentArgs : EventArgs
        {
            private string _var;
            public ContentArgs(string m)
            {
                _var = m;
            }
            public string Content
            {
                get
                {
                    return _var;
                }
            }
        }                         ///控件关闭的参数
        public string ReasonResult = "";                                ///推理机得出的结论    

        #endregion

        #region --Constructor--

        public EditRichTextBox(CDSSRichTextBox txtbox)
        {
            InitializeComponent();
            this.txtbox = txtbox;
            RtxtContent.Font = txtbox.Font;
            RtxtContent.Rtf = txtbox.Rtf;
            _popup = new Popup(this);
            _popup.Width = txtbox.Width + panel2.Width;
            _popup.DropShadowEnabled = false;
            _popup.Height = txtbox.Height;
            ChangeUserRegion();
            _popup.Closing += new ToolStripDropDownClosingEventHandler(_popup_Closing);
        }

        #endregion

        #region --Functions--

        protected virtual void OnClosing(ContentArgs e)
        {
            if (Closing != null)
                Closing(this, e);
        }

        /// <summary>
        /// 修改窗体的形状
        /// </summary>
        private void ChangeUserRegion()
        {
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                Point[] points = new Point[6];
                points[0] = new Point(0, 0);
                points[1] = new Point(_popup.Width, 0);
                points[2] = new Point(_popup.Width, panel2.Height);
                points[3] = new Point(panel2.Location.X, panel2.Height);
                points[4] = new Point(panel2.Location.X, Height);
                points[5] = new Point(0, Height);
                path.AddLines(points);
                path.CloseAllFigures();
                Region reg = new Region(path);
                this.Region = reg;
            }
        }

        /// <summary>
        /// 控件的展现
        /// </summary>
        /// <param name="Owner"></param>
        public void Show(Control Owner)
        {
            _popup.Show(Owner, false);
            if (s_go.Count == 0) lbldo.Enabled = false;
            if (s_back.Count == 0) lblundo.Enabled = false;
        }

        #endregion

        #region --Events--

        void _popup_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            ContentArgs ce = new ContentArgs(RtxtContent.Text);
            OnClosing(ce);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _popup.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            s_back.Push(RtxtContent.Rtf);
            lblundo.Enabled = true;
            RtxtContent.Text = ReasonResult;
        }

        private void lblundo_Click(object sender, EventArgs e)
        {
            lblundo.Enabled = s_back.Count == 1 ? false : true;
            s_go.Push(RtxtContent.Rtf);
            RtxtContent.Rtf = s_back.Pop();
            lbldo.Enabled = true;
        }

        private void lbldo_Click(object sender, EventArgs e)
        {
            lbldo.Enabled = s_go.Count == 1 ? false : true;
            s_back.Push(RtxtContent.Rtf);
            RtxtContent.Rtf = s_go.Pop();
            lblundo.Enabled = true;
        }


        #endregion
    }
}
