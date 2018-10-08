using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace CDSSCtrlLib
{
    public partial class WinExcelControl : UserControl
    {
        #region "API usage declarations"

        [DllImport("user32.dll")]
        public static extern int FindWindow(string strclassName, string strWindowName);

        [DllImport("user32.dll")]
        static extern int SetParent(int hWndChild, int hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
            int hWnd,               // handle to window
            int hWndInsertAfter,    // placement-order handle
            int X,                  // horizontal position
            int Y,                  // vertical position
            int cx,                 // width
            int cy,                 // height
            uint uFlags             // window-positioning options
            );

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        static extern bool MoveWindow(
            int Wnd,
            int X,
            int Y,
            int Width,
            int Height,
            bool Repaint
            );

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        static extern Int32 DrawMenuBar(
            Int32 hWnd
            );

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        static extern Int32 GetMenuItemCount(
            Int32 hMenu
            );

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        static extern Int32 GetSystemMenu(
            Int32 hWnd,
            bool Revert
            );

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        static extern Int32 RemoveMenu(
            Int32 hMenu,
            Int32 nPosition,
            Int32 wFlags
            );

        private const int MF_BYPOSITION = 0x400;
        private const int MF_REMOVE = 0x1000;


        const int SWP_DRAWFRAME = 0x20;
        const int SWP_NOMOVE = 0x2;
        const int SWP_NOSIZE = 0x1;
        const int SWP_NOZORDER = 0x4;

        #endregion

        #region Variables

        private Microsoft.Office.Interop.Excel.Application excelApplication = null;
        private Microsoft.Office.Interop.Excel.Workbooks excelWorkBooks = null;
        private Microsoft.Office.Interop.Excel.Workbook excelWorkBook = null;
        private Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = null;
        private int excelWnd;

        #endregion

        public WinExcelControl()
        {
            InitializeComponent();
            excelApplication = null;//Excel Application Object
            excelWorkBooks = null;//Workbooks
            excelWorkBook = null;//Excel Workbook Object
            excelWorkSheet = null;//Excel Worksheet Object            
        }

        /// <summary>
        /// �ؼ���С�仯�ĵ�������
        /// </summary>
        private void OnResize()
        {
            int borderWidth = SystemInformation.Border3DSize.Width;
            int borderHeight = SystemInformation.Border3DSize.Height;
            int captionHeight = SystemInformation.CaptionHeight;
            int statusHeight = SystemInformation.ToolWindowCaptionHeight;
            MoveWindow(
                excelWnd,
                -2 * borderWidth,
                -2 * borderHeight - captionHeight,
                this.Bounds.Width + 4 * borderWidth,
                this.Bounds.Height + captionHeight + 4 * borderHeight + statusHeight,
                true);

        }

        protected override void OnResize(EventArgs e)
        {
            OnResize();
            base.OnResize(e);
        }

        /// <summary>
        /// ��Excel�ļ�
        /// </summary>
        private bool ShowExcel()
        {
            if (excelApplication == null)
            {
                excelApplication = new Microsoft.Office.Interop.Excel.Application();
                try
                {
                    excelWnd = excelApplication.Hwnd;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("����Excel������������رա�", "��ʾ");
                    return false;
                }
                if (excelWnd == 0) excelWnd = FindWindow("XLMAIN", excelApplication.Caption);
                if (excelWnd != 0)
                {
                    try
                    {

                        ShowWindow(excelWnd, 0);

                        excelWorkBooks = excelApplication.Workbooks;

                        excelWnd = excelApplication.Hwnd;

                        excelApplication.DisplayAlerts = false;

                        excelApplication.Visible = true;

                        //��ExcelǶ��winform
                        SetParent(excelWnd, this.Handle.ToInt32());

                        //��Ƕ���Excel���ж�λ
                        SetWindowPos(excelWnd, this.Handle.ToInt32(), 0, 0, this.Bounds.Width, this.Bounds.Height, SWP_NOZORDER | SWP_NOMOVE | SWP_DRAWFRAME | SWP_NOSIZE);

                        OnResize();

                        //ȥ����������ʾ
                        int hMenu = GetSystemMenu(excelWnd, false);
                        if (hMenu > 0)
                        {
                            int menuItemCount = GetMenuItemCount(hMenu);
                            RemoveMenu(hMenu, menuItemCount - 1, MF_REMOVE | MF_BYPOSITION);
                            RemoveMenu(hMenu, menuItemCount - 2, MF_REMOVE | MF_BYPOSITION);
                            RemoveMenu(hMenu, menuItemCount - 3, MF_REMOVE | MF_BYPOSITION);
                            RemoveMenu(hMenu, menuItemCount - 4, MF_REMOVE | MF_BYPOSITION);
                            RemoveMenu(hMenu, menuItemCount - 5, MF_REMOVE | MF_BYPOSITION);
                            RemoveMenu(hMenu, menuItemCount - 6, MF_REMOVE | MF_BYPOSITION);
                            RemoveMenu(hMenu, menuItemCount - 7, MF_REMOVE | MF_BYPOSITION);
                            RemoveMenu(hMenu, menuItemCount - 8, MF_REMOVE | MF_BYPOSITION);
                            DrawMenuBar(excelWnd);
                        }
                        return true;
                    }
                    catch (Exception e)
                    {
                        //CloseExcelApplication();
                        MessageBox.Show("(1)û�а�װExcel 2003��(2)��û�а�װExcel 2003 .NET �ɱ����֧�֣�\n��ϸ��Ϣ��"
                            + e.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                    return false;
            }
            else
                return true;
        }

        public bool OpenExcelFile(string excelOpenFileName)
        {
            ShowExcel();
            if (excelWorkBook != null)
            {
                try
                {
                    if (!excelWorkBook.Saved)
                        excelWorkBook.Save();
                    excelWorkBook.Close(null, null, null);
                    excelWorkBook = null;
                }
                catch { }
            }

            //����ļ��Ƿ����
            if (excelOpenFileName == "")
            {
                MessageBox.Show("��ѡ���ļ���", "��ʾ");
                return false;
            }
            if (!File.Exists(excelOpenFileName))
            {
                MessageBox.Show(excelOpenFileName + "���ļ������ڣ�", "��ʾ");
                return false;
            }
            try
            {
                excelWorkBooks = excelApplication.Workbooks;

                excelWorkBook = ((Microsoft.Office.Interop.Excel.Workbook)excelWorkBooks.Open(excelOpenFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("(1)û�а�װExcel 2003��(2)��û�а�װExcel 2003 .NET �ɱ����֧�֣�\n��ϸ��Ϣ��"
                    + e.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public bool NewExcelFile(params string[] columnNames)
        {
            ShowExcel();
            if (excelWorkBook != null)
            {
                if (excelWorkBook.Path == "")
                {
                    SaveFileDialog savedia = new SaveFileDialog();
                    savedia.Filter = "Excel files|*.xlsx;*.xls|All files|*.*";
                    if (savedia.ShowDialog() == DialogResult.OK)
                    {
                        excelWorkBook.SaveAs(savedia.FileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    }
                    else
                    {
                        excelWorkBook.Save();
                    }
                }
                excelWorkBook.Close(null, null, null);
                excelWorkBook = null;
            }
            try
            {
                excelWorkBooks = excelApplication.Workbooks;

                excelWorkBook = ((Microsoft.Office.Interop.Excel.Workbook)excelWorkBooks.Add(true)); ;
                excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.ActiveSheet;
                for (int i = 0; i < columnNames.Length; i++)
                {
                    excelWorkSheet.Cells[1, i + 1] = columnNames[i];

                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("(1)û�а�װExcel 2003��(2)��û�а�װExcel 2003 .NET �ɱ����֧�֣�\n��ϸ��Ϣ��"
                    + e.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public void ExcelSave()
        {
            if (excelWorkBook != null)
            {
                excelWorkBook.Save();
            }
        }

        /// <summary>        
        /// �ر�Excel�ļ�
        /// </summary>
        public bool CloseExcelFile()
        {
            //try
            //{
            //    excelApplication.Workbooks.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "��ʾ");
            //}
            bool isClose = false;
            bool Cancel = false;
            if (excelWorkBook != null)
            {
                try
                {
                    if (!excelWorkBook.Saved)
                        switch (MessageBox.Show("�ļ����޸ģ��Ƿ񱣴棿", "��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.Yes:
                                excelWorkBook.Save();
                                break;
                            case DialogResult.No:
                                break;
                            case DialogResult.Cancel:
                                Cancel = true;
                                break;
                        }
                    if (!Cancel)
                    {
                        excelWorkBook.Close(null, null, null);
                        excelWorkBook = null;
                        CloseExcelApplication();
                        isClose = true;
                    }
                }
                catch
                {
                    CloseExcelApplication();
                }
            }
            else
            {
                CloseExcelApplication();
            }
            return isClose;
        }

        /// <summary>
        /// �ر�Excel�ļ����ͷŶ������һ��Ҫ���ô˺���������������쳣
        /// </summary>
        /// <param></param> 
        public void CloseExcelApplication()
        {
            try
            {
                excelWorkBooks = null;
                excelWorkBook = null;
                excelWorkSheet = null;
                if (excelApplication != null)
                {
                    excelApplication.Workbooks.Close();
                    excelApplication.Quit();
                    excelApplication = null;

                }
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


    }
}
