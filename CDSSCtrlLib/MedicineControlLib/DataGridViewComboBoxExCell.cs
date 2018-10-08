using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CDSSCtrlLib.MedicineControlLib
{
    public class DataGridViewComboBoxExCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            DataGridViewComboBoxExEditingControl clt = DataGridView.EditingControl as DataGridViewComboBoxExEditingControl;

            DataGridViewComboBoxExColumn col = (DataGridViewComboBoxExColumn)OwningColumn;

            clt.DataSource = col.DataSource;
            clt.DisplayMember = col.DisplayMember;
            clt.ValueMember = col.ValueMember;

            clt.Text = Convert.ToString(this.Value);
        }

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewComboBoxExEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return "";
            }
        }
    }
}
