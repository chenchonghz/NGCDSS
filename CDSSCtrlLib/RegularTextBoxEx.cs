using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using System.Drawing.Design;

namespace CDSSCtrlLib
{
    [ToolboxBitmap(typeof(TextBox))]
    public class RegularTextBoxEx : TextBox
    {
        #region --Fields/Attributes/Propertys--

        private const int WM_PAINT = 0xF;//WM_PAINT消息ID
        private string _emptyTextTip;
        private string _tagSign = "";
        private Color _emptyTextTipColor = Color.DarkGray;
        private Dictionary<string, string> regularinfo = new Dictionary<string, string>();
        protected string _typeName;
        private List<string> _regular = new List<string>();
        private string oldText;
        private bool _isText = false;

        /// <summary>
        /// TextBox的Text属性为空是的提示字符
        /// </summary>
        [DefaultValue("")]
        public string EmptyTextTip
        {
            get { return _emptyTextTip; }
            set
            {
                _emptyTextTip = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 是否是文本
        /// 如果true，属性RealVaule值将为'{0}'
        /// 否则，属性RealVaule值与Text属性相同
        /// </summary>
        public bool IsText
        {
            get { return _isText; }
            set { _isText = value; }
        }

        /// <summary>
        /// Text的真实值
        /// </summary>
        public string RealVaule
        {
            get
            {
                if (_isText)
                    return "'" + Text + "'";
                else
                    return Text;
            }
            set
            {
                if (_isText)
                    Text = value.Substring(1, value.Trim().Length - 2);
                else
                    Text = value;
            }
        }

        /// <summary>
        /// 分割Regular的标志
        /// </summary>
        public string TagSign
        {
            get { return _tagSign; }
            set { _tagSign = value; }
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            get { return _typeName; }
            set
            {
                _typeName = value;
                Text = string.Empty;
                if (ChangeRegular() == string.Empty)
                    EmptyTextTip = "请输入查询条件";
                else
                    EmptyTextTip = ChangeRegular().Split(TagSign[0])[1];
            }
        }

        /// <summary>
        /// 空字符提示文本颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray")]
        public Color EmptyTextTipColor
        {
            get { return _emptyTextTipColor; }
            set
            {
                _emptyTextTipColor = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 编辑多个正则表达式，用作字符串数组
        /// </summary>
        [Category("Data")]
        [Localizable(true)]
        [Description("编辑多个正则表达式，用作字符串数组")]
        [Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        typeof(UITypeEditor)),
        RefreshProperties(RefreshProperties.Repaint),
        NotifyParentProperty(true)]
        public string[] Regular
        {
            get { return _regular.ToArray(); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Regular");
                _regular.Clear();
                _regular.AddRange(value);
                initRegularInfo();
            }
        }

        #endregion

        #region --Constructor--

        /// <summary>
        /// 支持正则表达式的TextBox控件
        /// </summary>
        public RegularTextBoxEx()
            : base()
        {

        }

        #endregion

        #region Functions

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
                WmPaint(ref m);
        }

        private void WmPaint(ref Message m)
        {
            using (Graphics g = Graphics.FromHwnd(base.Handle))
            {
                if (Text.Length == 0 &&
                    !string.IsNullOrEmpty(_emptyTextTip) &&
                    !Focused)
                {
                    TextFormatFlags format = TextFormatFlags.EndEllipsis |
                        TextFormatFlags.VerticalCenter;

                    if (RightToLeft == RightToLeft.Yes)
                    {
                        format |= TextFormatFlags.RightToLeft
                            | TextFormatFlags.Right;
                    }

                    TextRenderer.DrawText(g,
                        _emptyTextTip,
                        Font,
                        base.ClientRectangle,
                        _emptyTextTipColor,
                        format);
                }
            }
        }

        private void initRegularInfo()
        {
            regularinfo.Clear();
            if (TagSign == "") return;
            foreach (string str in _regular.ToArray())
            {
                string[] tmp = str.Split(TagSign[0]);
                regularinfo.Add(tmp[0], tmp[1] + _tagSign + tmp[2]);
            }
        }

        protected string ChangeRegular()
        {
            oldText = string.Empty;
            string text = "";
            if (TagSign == "") return string.Empty;
            if (TypeName == null) return string.Empty;

            initRegularInfo();
            if (regularinfo.ContainsKey(TypeName))
            {
                text = regularinfo[_typeName];
            }
            else
            {
                text = TagSign[0] + "请输入任意值";
            }
            return text;
        }

        #endregion

        #region Events

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (ChangeRegular() == string.Empty) return;
            string pattern = ChangeRegular().Split(TagSign[0])[0];
            base.OnTextChanged(e);
            {
                Match m = Regex.Match(Text, pattern); // 匹配正则表达式 
                if (!m.Success) // 输入的不是数字 
                {
                    Text = oldText; // textBox内容不变 // 将光标定位到文本框的最后 
                    SelectionStart = Text.Length;
                    BackColor = Color.Pink;
                }
                else // 输入的是数字 
                {
                    oldText = Text; // 将现在textBox的值保存下来 
                    BackColor = Color.White;
                }
            }
        }

        #endregion

    }
}
