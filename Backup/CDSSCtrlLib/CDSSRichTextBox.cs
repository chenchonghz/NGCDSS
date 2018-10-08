using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;//API 命名空间

namespace CDSSCtrlLib
{
    public class CDSSRichTextBox:System.Windows.Forms.RichTextBox   
    {
        public CDSSRichTextBox()
        {
        }

        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);
        /// <summary>
        /// 重载WndProc函数，判断文本框是否只读，只读则隐藏光标
        /// </summary>
        /// <param name="m">message</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (this.ReadOnly)
            {
                HideCaret(m.HWnd);
            }
        }
    }
}
