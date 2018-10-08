using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CDSSCtrlLib
{
    public partial class DateTimeControl : UserControl
    {     
        public DateTimeControl()
        {
            InitializeComponent();
        }

        private bool JUST;
        private string txtYear;
        private string txtMonth;
        private DateTime txtDateDt;
        private string txtDateStr;

        /// <summary>
        /// 设置年月，年/月属性
        /// </summary>
        public string Year
        {
            get
            {
                return txtYear;
            }
            set
            {
                txtYear = value;
                textBox1.Text = txtYear;
                if (YearControl(txtYear))
                {
                    textBox1.Text = txtYear;
                }
                else
                {
                    textBox1.Text = Convert.ToString(DateTime.Now.Year);
                }
            }
        }

        public string Month
        {
            get
            {
                return txtMonth;
            }
            set
            {
                if (textBox2.Text != String.Empty)
                {
                    txtMonth = value;
                    textBox2.Text = txtMonth;
                }
                else
                {
                    textBox2.Text = DateTime.Now.Month.ToString();
                }
            }
        }
        /// <summary>
        /// DateTime型的年月
        /// </summary>
        public DateTime Value
        {
            get
            {

                if (DTConvert(textBox1.Text, textBox2.Text))
                {
                    txtDateDt = Convert.ToDateTime(String.Format("{0}/{1}/{2}", textBox2.Text, "01", textBox1.Text));
                }
                return txtDateDt;
            }
            set
            {
                txtDateDt = value;
                textBox1.Text = Convert.ToString(txtDateDt.Year);
                textBox2.Text = Convert.ToString(txtDateDt.Month);
            }
        }
        /// <summary>
        /// String型的年月
        /// </summary>
        public override string Text
        {
            get
            {
                if (textBox1.Text != String.Empty)
                {
                    if (textBox2.Text != String.Empty)
                    {
                        txtDateStr = String.Format("{0}/{1}", textBox1.Text, textBox2.Text);
                    }
                    else
                    {
                        txtDateStr = String.Format("{0}", textBox1.Text);
                    }
                    return txtDateStr;
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                txtDateStr = value;
                textBox1.Text = Convert.ToString(Convert.ToDateTime(txtDateStr).Year);
                textBox2.Text = Convert.ToString(Convert.ToDateTime(txtDateStr).Month);

            }
        }

        //DateTime myDateTime = DateTime.Now;

        private bool DTConvert(string dtconvert1, string dtconvert2)
        {
            if (dtconvert1 != String.Empty && dtconvert2 != String.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true; //获取或设置一个值，指示是否处理过System.Windows.Forms.Control.KeyPress事件 
            }
        }
        
        /// <summary>
        /// 限制输入模式为英文，辅助限制数字的程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_ImeModeChanged(object sender, EventArgs e)
        {
            if (textBox1.ImeMode != System.Windows.Forms.ImeMode.Off||textBox2.ImeMode!=System.Windows.Forms.ImeMode.Off)
            {
                textBox1.ImeMode = System.Windows.Forms.ImeMode.Off;
                textBox2.ImeMode = System.Windows.Forms.ImeMode.Off;
            }
        }

        /// <summary>
        /// 去除复制粘贴功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimeControl_Load(object sender, EventArgs e)
        {
            textBox1.ShortcutsEnabled = false;
            textBox2.ShortcutsEnabled = false;
        }


        /// <summary>
        /// 限制输入年份,月份
        /// </summary>
        /// <param name="strInputYear"></param>
        /// <returns></returns>

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ///清除空格

            textBox1.Text = textBox1.Text.Trim();

            ///调用YearControl函数，限制输入年份；移动焦点
            if (textBox1.Text != String.Empty)
            {
                if (textBox1.Text.Length == 4)
                {
                    if (YearControl(textBox1.Text.ToString()) == false)
                    {
                        textBox1.Text = String.Empty;
                    }
                    else
                    {
                        textBox2.Focus();
                    }
                }
            }

            RaiseValueChangedEvent(sender, e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ///清除空格
            
            textBox2.Text = textBox2.Text.Trim();

            if (textBox2.Text != String.Empty)
            {
                if ((Convert.ToInt32(textBox2.Text) < 1) || (Convert.ToInt32(textBox2.Text) > 12))
                {
                    textBox2.Text = String.Empty;
                }
            }

                RaiseValueChangedEvent(sender, e);
            
        }
        
        /// <summary>
        /// 限制输入年份的函数
        /// </summary>
        /// <param name="strInputYear"></param>
        /// <returns></returns>
        private bool YearControl(string strInputYear)
        {
            if (strInputYear != String.Empty)
            {
                if ((Convert.ToInt32(strInputYear) < DateTime.Now.Year - 100) || (Convert.ToInt32(strInputYear) > DateTime.Now.Year + 1))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// ValueChanged事件
        /// </summary>
        public event EventHandler ValueChanged;
        private void RaiseValueChangedEvent(object sender, EventArgs e)
        {
            EventHandler temp = ValueChanged;
            if (temp != null)
                temp(sender, e);
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (textBox1.Text != String.Empty)
            {
                if (textBox1.Text.Length != 4)
                {
                    textBox1.Text = Convert.ToString(DateTime.Now.Year);
                }
            }
            else
            {
                textBox1.Text = Convert.ToString(DateTime.Now.Year);
            }
            //textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right; 
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (textBox2.Text != String.Empty)
            {
                if (textBox2.Text.Length != 1 && textBox2.Text.Length != 2)
                {
                    textBox2.Text = "1";
                }
            }
            else
            {
                textBox2.Text = "1";
            }
            //textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right; ;
        }



    }
}