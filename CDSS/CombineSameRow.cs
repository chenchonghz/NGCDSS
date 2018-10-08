using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib.MedicineControlLib;
using System.Collections;

namespace CDSS
{
    public partial class CombineSameRow : Form
    {
        public struct CombineRow
        {
            public DataRow RowSourceOne;
            public DataRow RowSourceTwo;
            public List<string> DiffColumnName;
            public int TotalDiffColumnCount;
            public DataGridViewRow CombRow;
            public bool IsChecked;
        };

        int TotalDiff = 0;

        //存放所有列名
        private List<string> colNames = new List<string>();

        //不同数据行的集合
        private Dictionary<DataRow, DataRow> DiffRowCollection = new Dictionary<DataRow, DataRow>();

        public delegate void DataRowCompletedHandler(object sender, DataRowCompletedEventArgs e);

        public event DataRowCompletedHandler DataRowCompleted;

        public virtual void OnDataRowCompleted(DataGridViewRow dt, DataRowCompletedEventArgs e)
        {
            if (DataRowCompleted != null)
                DataRowCompleted(dt, e);
        }

        public CombineSameRow()
        {
            InitializeComponent();
        }

        public void SetVaules(Dictionary<DataRow, DataRow> RowCollection, List<string> colName)
        {
            DiffRowCollection = RowCollection;
            this.colNames = colName;
            lblCount.Text = string.Format("已核对项：{0}/{1}", TotalDiff, RowCollection.Count);
        }

        private void RowCompare()
        {
            foreach (KeyValuePair<DataRow, DataRow> row in DiffRowCollection)
            {
                CombineRow comRow = new CombineRow();
                comRow.RowSourceOne = row.Key;
                comRow.RowSourceTwo = row.Value;
                List<string> diffColumnNames = new List<string>();
                DataGridViewRow dr = new DataGridViewRow();
                foreach (string col in colNames)
                {
                    if (comRow.RowSourceOne[col].ToString() != comRow.RowSourceTwo[col].ToString())
                    {
                        diffColumnNames.Add(col);
                    }
                    if (diffColumnNames.Contains(col))
                    {
                        DataGridViewComboEditBoxColumn diffColumn = new DataGridViewComboEditBoxColumn();
                        diffColumn.Name = col;
                        diffColumn.HeaderText = col;
                        diffColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                        diffColumn.DataPropertyName = col;
                        if (!dataGridView1.Columns.Contains(col))
                            dataGridView1.Columns.Add(diffColumn);
                        DataGridViewComboEditBoxCell cell = new DataGridViewComboEditBoxCell();
                        cell.Style.BackColor = Color.LightPink;
                        cell.Items.Add(comRow.RowSourceOne[col].ToString().Replace("<color>", ""));
                        cell.Items.Add(comRow.RowSourceTwo[col].ToString().Replace("<color>", ""));
                        cell.Value = "";
                        dr.Cells.Add(cell);
                    }
                    else
                    {
                        DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                        column.Name = col;
                        column.HeaderText = col;
                        column.DataPropertyName = col;
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        if (!dataGridView1.Columns.Contains(col))
                            dataGridView1.Columns.Add(column);
                        DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                        cell.Value = comRow.RowSourceOne[col];
                        dr.Cells.Add(cell);
                    }
                }
                comRow.IsChecked = false;
                comRow.DiffColumnName = diffColumnNames;
                comRow.TotalDiffColumnCount = comRow.DiffColumnName.Count;
                comRow.CombRow = dr;
                dr.Tag = comRow;
                dataGridView1.Rows.Add(dr);
            }
        }

        private void CombineSameRow_Load(object sender, EventArgs e)
        {
            RowCompare();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow drow = new DataGridViewRow();
            if (dataGridView1.SelectedRows.Count == 0)
            {
                if (dataGridView1.SelectedCells.Count == 0)
                    return;
                else
                    drow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            }
            else
                drow = dataGridView1.SelectedRows[0];
            CombineRow comRow = (CombineRow)drow.Tag;
            lblTip.Text = string.Format("当前选中行待合并项：{0}/{1}", comRow.TotalDiffColumnCount - comRow.DiffColumnName.Count, comRow.TotalDiffColumnCount);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value == null) return;
            CombineRow comRow = (CombineRow)dataGridView1.Rows[e.RowIndex].Tag;
            if (!dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().Equals(""))
            {
                comRow.DiffColumnName.Remove(dataGridView1.Columns[e.ColumnIndex].Name);
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.LightGreen;
            }
            if (comRow.DiffColumnName.Count == 0)
            {
                if (!comRow.IsChecked)
                    TotalDiff++;
                comRow.IsChecked = true;
                dataGridView1.Rows[e.RowIndex].Tag = comRow;
                DataRowCompletedEventArgs ee = new DataRowCompletedEventArgs((CombineRow)comRow);
                OnDataRowCompleted(dataGridView1.Rows[e.RowIndex], ee);
                lblCount.Text = string.Format("已核对项：{0}/{1}", TotalDiff >= dataGridView1.RowCount ? dataGridView1.RowCount : TotalDiff, dataGridView1.RowCount);
            }
            lblTip.Text = string.Format("当前选中行待合并项：{0}/{1}", comRow.TotalDiffColumnCount - comRow.DiffColumnName.Count, comRow.TotalDiffColumnCount);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Dictionary<string, List<DataGridViewRow>> filterRow = new Dictionary<string, List<DataGridViewRow>>();

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                dataGridView1.Enabled = false;
                filterRow.Clear();
                comboBox1.Items.Clear();
                List<DataGridViewRow> totalRow = new List<DataGridViewRow>();
                filterRow.Add("全部", totalRow);
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string key = dataGridView1.Rows[i].Cells[e.ColumnIndex].Value.ToString();
                    if (filterRow.ContainsKey(key))
                    {
                        filterRow[key].Add(dataGridView1.Rows[i]);
                    }
                    else
                    {
                        List<DataGridViewRow> rows = new List<DataGridViewRow>();
                        rows.Add(dataGridView1.Rows[i]);
                        filterRow.Add(key, rows);
                    }
                    filterRow["全部"].Add(dataGridView1.Rows[i]);
                }

                string[] val = new string[filterRow.Keys.Count];
                filterRow.Keys.CopyTo(val, 0);
                comboBox1.Items.AddRange(val);
                if (dataGridView1.Tag != null || e.ColumnIndex.ToString() == lblColumnIndex.Text)
                    comboBox1.SelectedItem = dataGridView1.Tag;
                else
                    comboBox1.SelectedIndex = -1;
                panel1.Left = (this.Width - panel1.Width) / 2;
                panel1.Top = (this.Height - panel1.Height) / 4;
                panel1.Visible = true;
                lblColumnIndex.Text = e.ColumnIndex.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            dataGridView1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string column in colNames)
            {
                dataGridView1.Columns[column].HeaderText = column;
            }

            foreach (DataGridViewRow row in filterRow["全部"])
            {
                row.Visible = false;
            }

            foreach (DataGridViewRow row in filterRow[comboBox1.Text])
            {
                row.Visible = true;
            }
            dataGridView1.Tag = comboBox1.SelectedItem;
            if (comboBox1.Text != "全部")
                dataGridView1.Columns[int.Parse(lblColumnIndex.Text)].HeaderText =
                    string.Format(dataGridView1.Columns[int.Parse(lblColumnIndex.Text)].HeaderText + "\r\n【{0}】", comboBox1.Text);
            panel1.Visible = false;
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
        }
    }

    public class DataRowCompletedEventArgs : EventArgs
    {
        public DataRowCompletedEventArgs(CombineSameRow.CombineRow RowMsg)
        {
            this.diffRowOne = RowMsg.RowSourceOne;
            this.diffRowTwo = RowMsg.RowSourceTwo;
            this.rowData = RowMsg.CombRow;
        }

        private DataGridViewRow rowData;

        public DataGridViewRow RowData
        {
            get { return rowData; }
        }

        private DataRow diffRowOne;

        public DataRow DiffRowOne
        {
            get { return diffRowOne; }
        }

        private DataRow diffRowTwo;

        public DataRow DiffRowTwo
        {
            get { return diffRowTwo; }
        }

    }
}