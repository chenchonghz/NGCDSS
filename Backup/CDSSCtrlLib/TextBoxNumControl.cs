using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CDSSCtrlLib
{
    public partial class TextBoxNumControl : UserControl
    {
        public TextBoxNumControl()
        {
            InitializeComponent();
        }

        private string txtbeforeDecimal;
        private string txtafterDecimal;
        private string[] strText;
        private string strTextbefore;
        private int lthSplText0;
        private int lthSplText1;
        private bool Just;
        private string oldtext;
        private string newtext;

        public override string Text
        {
            get
            {
                return txtNum.Text;
            }
            set
            {
                txtNum.Text = value;
            }
        }

        /// <summary>
        /// 设置数点前后所允许数位最大值的属性
        /// </summary>
        public string DecimalBefore
        {
            get
            {
                return txtbeforeDecimal;
            }
            set
            {
                txtbeforeDecimal = value;
            }
        }

        public string DecimalAfter
        {
            get
            {
                return txtafterDecimal;
            }
            set
            {
                txtafterDecimal = value;
            }
        }
        /// <summary>
        /// 限制输入数字，控制键和小数点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            Just = false;

            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true; //获取或设置一个值，指示是否处理过System.Windows.Forms.Control.KeyPress事件 
            }
            else if (Char.IsDigit(e.KeyChar))
            {
                if (txtNum.Text.IndexOf('.') == -1 && txtNum.Text.Length == Convert.ToInt32(txtbeforeDecimal) && Convert.ToInt32(txtafterDecimal) != 0)
                {
                    Just = true;
                }
                oldtext = txtNum.Text;//获取并保存编辑框当前文本
                int j = txtNum.SelectionStart;//获取并保存编辑框光标位置
                newtext = oldtext.Insert(j, e.KeyChar.ToString());//预览新文本
                DecimalSplit(newtext);
                if (lthSplText0 > Convert.ToInt32(txtbeforeDecimal) || lthSplText1 > Convert.ToInt32(txtafterDecimal)) //判断新文本格式（可按不同的要求进行判断）
                {
                    txtNum.Text = oldtext;
                    if (txtNum.SelectionLength <= txtNum.Text.Length && txtNum.SelectionLength != 0)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                if (e.KeyChar == '.')
                {
                    if (((TextBox)sender).Text.LastIndexOf('.') != -1||Convert.ToInt32(txtafterDecimal)==0)
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// 去除复制粘贴功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxNumControl1_Load(object sender, EventArgs e)
        {
            txtNum.ShortcutsEnabled = false;        
        }
        

        /// <summary>
        /// 限制输入模式为英文，辅助限制数字的程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_ImeModeChanged(object sender, EventArgs e)
        {
            if (txtNum.ImeMode != System.Windows.Forms.ImeMode.Off)
            {
                txtNum.ImeMode = System.Windows.Forms.ImeMode.Off;
            }
        }

        /// <summary>
        /// 判断长度，自动添加小数点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_TextChanged(object sender, EventArgs e)
        {
            txtNum.Text = txtNum.Text.Trim();//清楚空格
            if (Convert.ToInt32(txtafterDecimal) != 0 )
            {
                txtNum.MaxLength = Convert.ToInt32(txtbeforeDecimal) + Convert.ToInt32(txtafterDecimal) + 1;
            }
            else
            {
                txtNum.MaxLength = Convert.ToInt32(txtbeforeDecimal) + Convert.ToInt32(txtafterDecimal);
            }

            if (txtNum.Text.Length <= Convert.ToInt32(txtNum.MaxLength))
            {
                if (Just)
                {
                    EnumerateText(txtNum.Text);
                    txtNum.SelectionStart = txtNum.Text.Length;
                    txtNum.ScrollToCaret();
                }
            }
            RaiseTextChangedEvent(sender, e);
        }

        /// <summary>
        /// 获取小数点前后值
        /// </summary>
        /// <param name="splText"></param>
        private void DecimalSplit(String splText)
        {
            if (txtNum.Text.ToString().Split(new char[] { '.' }).Length == 2)
            {
                strText = splText.Split(new char[] { '.' });
                lthSplText0 = strText[0].Length;
                lthSplText1 = strText[1].Length;
            }
            else
            {
                lthSplText0 = txtNum.Text.Length;
                lthSplText1 = 0;
            }
        }

        /// <summary>
        /// 添加小数点函数
        /// </summary>
        /// <param name="txtText"></param>
        private void EnumerateText(String eumText)
        {
            IEnumerator txtTextEnum = eumText.GetEnumerator();
            string txtEnum = "";
            int CharCount = 0;
            while (txtTextEnum.MoveNext())
            {
                CharCount++;

                if (CharCount < Convert.ToInt32(txtbeforeDecimal))
                {
                    txtEnum = txtEnum + txtTextEnum.Current.ToString();
                }
                if (CharCount == Convert.ToInt32(txtbeforeDecimal))
                {
                    txtEnum = txtEnum + txtTextEnum.Current.ToString() + ".";
                }
                if (CharCount > Convert.ToInt32(txtbeforeDecimal))
                {
                    txtEnum = txtEnum + txtTextEnum.Current.ToString();
                }
            }
            Just = false;
            txtNum.Text = txtEnum;
        }

        /// <summary>
        /// 删零、补零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_Validating(object sender, CancelEventArgs e)
        {
            if (txtNum.Text != String.Empty)   
            {
                txtNum.Text = Convert.ToString(Convert.ToDouble(txtNum.Text));
                if(Convert.ToDouble(txtNum.Text)!=0)
                {
                    DecimalSplit(txtNum.Text);
                    if (lthSplText0 <= Convert.ToInt32(txtbeforeDecimal))
                    {
                        if (txtNum.Text.ToString().Split(new char[] { '.' }).Length == 2)
                        {
                            if (strText[0] != String.Empty)
                            {
                                strTextbefore = Convert.ToString(Convert.ToInt32(strText[0]));
                            }
                            else
                            {
                                strTextbefore = "0";
                            }

                            txtNum.Text = strTextbefore + "." + strText[1];

                            if (lthSplText1 <= Convert.ToInt32(txtafterDecimal))
                            {
                                for (int i = 1; i <= Convert.ToInt32(txtafterDecimal) - lthSplText1; i++)
                                {
                                    txtNum.Text = txtNum.Text + "0";
                                }
                            }
                        }
                        else
                        {
                            txtNum.Text = Convert.ToString(Convert.ToUInt32(txtNum.Text));
                            if (Convert.ToInt32(txtafterDecimal) != 0)
                            {
                                txtNum.Text = txtNum.Text + ".";
                                if (lthSplText1 <= Convert.ToInt32(txtafterDecimal))
                                {
                                    for (int i = 1; i <= Convert.ToInt32(txtafterDecimal) - lthSplText1; i++)
                                    {
                                        txtNum.Text = txtNum.Text + "0";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("超过限制位数，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                
            }
        }

        /// <summary>
        /// TextChanged事件
        /// </summary>
        public event EventHandler DataChanged;
        private void RaiseTextChangedEvent(object sender, EventArgs e)
        {
            EventHandler temp = DataChanged;
            if (temp != null)
                temp(sender, e);
        }
    }
}