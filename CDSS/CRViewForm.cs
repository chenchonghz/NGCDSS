using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDSS
{
    public partial class CRViewForm : Form
    {
        DataTable resultOfDignosedTable = new DataTable();
        DataTable resultOfValueTable = new DataTable();
        string conditon1;
        string conditon2;

        public CRViewForm(DataTable table1,DataTable table2,string condition1,string condition2)
        {
            InitializeComponent();
            this.resultOfDignosedTable = table1;
            this.resultOfValueTable = table2;
            this.conditon1 = condition1;
            this.conditon2 = condition2;
        }
        private void CRViewForm_Load(object sender, EventArgs e)
        {
            CrystalReport1 cr = new CrystalReport1();
            GroupingDataSet ds = new GroupingDataSet();

            string valueProperties = "";
            string dignoProperties = "";
            for (int k = 3; k < resultOfValueTable.Columns.Count; k++)
            {
                valueProperties += resultOfValueTable.Columns[k].ToString() + " ";
            }
            for (int k = 3; k < resultOfDignosedTable.Columns.Count; k++)
            {
                dignoProperties += resultOfDignosedTable.Columns[k].ToString() + " ";
            }
            DataTable whole=new DataTable();
            if (resultOfValueTable.Rows.Count == 0)
            {
                whole.Columns.Add("时间");
                whole.Columns.Add("组别");
            }
            else
            {
                whole = resultOfValueTable.Copy();
            }
            int i = 0;
            while (whole.Columns.Count < 9)
            {
                whole.Columns.Add(Convert.ToString(i++));
            }
            whole.Merge(resultOfDignosedTable);
            while (whole.Columns.Count < 16)
            {
                whole.Columns.Add(Convert.ToString(i++));
            }            
            whole.Columns.Add("参数类型");
            foreach (DataRow r in whole.Rows)
            {
                if (!Convert.IsDBNull(r[2]))
                {
                    r["参数类型"] = "数值参数诊断      " + valueProperties;
                }
                else
                    r["参数类型"] = "诊断参数诊断      " + dignoProperties;
            }
            foreach (DataRow r in whole.Rows)
            {
                if (r[1].ToString()=="统计组")
                {
                    r[1] = r[1].ToString() + " " + conditon1;
                }
                else
                    r[1] = r[1].ToString() + " " + conditon2;
            }
            
            foreach (DataRow r in whole.Rows)
            {
                DataRow drq = ds.ResultDataTable.NewRow();
                drq.ItemArray = r.ItemArray;//这是加入的是第一行
                ds.ResultDataTable.Rows.Add(drq);
            }
            cr.SetDataSource(ds);
            
            this.crystalReportViewer1.ReportSource = cr;
            this.crystalReportViewer1.Refresh();
        }
    }
}