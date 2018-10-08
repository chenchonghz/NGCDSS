using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Reflection;
using System.ComponentModel;
using Microsoft.ReportingServices.Interfaces;
using System.Drawing;

namespace Utilities.PrintFunction
{
    public class PrintClass
    {

        #region 直接打印指定大小报表

        private int m_currentPageIndex;       
        private IList<Stream> m_streams;       
        private PrinterSettings prtSettings;       
        private PageSettings pgSettings;
       
        /// <summary>
        /// Routine to provide to the report renderer, in order to
        /// save an image for each page of the report.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fileNameExtension"></param>
        /// <param name="encoding"></param>
        /// <param name="mimeType"></param>
        /// <param name="willSeek"></param>
        /// <returns></returns>
        private  Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {  
            Stream stream = new FileStream(name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        /// <summary>
        /// Export the given report as an EMF (Enhanced Metafile) file.
        /// </summary>
        /// <param name="report"></param>
 
        private void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>21cm</PageWidth>" +
              "  <PageHeight>29.7cm</PageHeight>" +
              "  <MarginTop>0.5cm</MarginTop>" +
              "  <MarginLeft>0.5cm</MarginLeft>" +
              "  <MarginRight>0.5cm</MarginRight>" +
              "  <MarginBottom>0.5cm</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
            {
                stream.Position = 0;
            }
        }

        /// <summary>
        /// Handler for PrintPageEvents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {           
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);            
            //Rectangle rectangle = new Rectangle(0, 0, 583, 827);
            //ev.Graphics.DrawImage(pageImage, rectangle);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        /// <summary>
        /// Print 
        /// </summary>
        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                return;

            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings = pgSettings;
            printDoc.PrinterSettings = prtSettings;          
            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format("Can't find printer \"{0}\".", "默认打印机!");
                MessageBox.Show(msg, "找不到默认打印机");
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }

        /// <summary>
        /// To Directory Print The Report
        /// </summary>
        /// <param name="report"></param>
        public void DirectPrintReport(LocalReport report )
        {          
            try
            {
                //LocalReport report = this.reportViewer1.LocalReport;
                Export(report);
                m_currentPageIndex = 0;
                Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("在打印过程中出现异常!" + ex.Message);
            }
            finally
            {
                if (m_streams != null)
                {
                    foreach (Stream stream in m_streams)
                    {
                        stream.Close();
                    }
                    m_streams = null;
                }
            }            
        }

        /// <summary>
        /// 显示打印对话框并打印
        /// </summary>
        /// <param name="report"></param>
        public void PrintDialog(LocalReport report)
        {
            PrintDialog prtDialog = new PrintDialog();
            if (prtDialog.ShowDialog() == DialogResult.OK)
            {
                prtSettings = prtDialog.PrinterSettings;
                DirectPrintReport(report);
            }
        }

        /// <summary>
        /// 打印预览功能
        /// </summary>
        /// <param name="report"></param>
        public  void PrintView(LocalReport report)
        {
            try
            {
                PrintPreviewDialog ppDialog = new PrintPreviewDialog();

                //打印预览时必须重新确定相应的打印设置和预览设置
                PrintDocument printDoc = new PrintDocument();
                printDoc.DefaultPageSettings = pgSettings;
                printDoc.PrinterSettings = prtSettings;
                ppDialog.Document = printDoc;
                Export(report);
                m_currentPageIndex = 0;             
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                ppDialog.ShowDialog(); 
                              
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (m_streams != null)
                {
                    foreach (Stream stream in m_streams)
                    {
                        stream.Close();
                    }
                    m_streams = null;
                }
            }
        }

        /// <summary>
        /// 页面设置功能
        /// </summary>
        public  void PageSetUp()
        {
            PageSetupDialog psuDialog = new PageSetupDialog();
            psuDialog.PageSettings = new System.Drawing.Printing.PageSettings();
            if (psuDialog.ShowDialog() == DialogResult.OK)
            {
                pgSettings = psuDialog.PageSettings;
            }
        }
        #endregion

    }
}
