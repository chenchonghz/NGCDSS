using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CDSSCtrlLib.MedicineControlLib
{
    public class DataGridViewComboBoxExColumn : DataGridViewColumn
    {
        private object dataSoruce = null;

        public object DataSource
        {
            get { return dataSoruce; }
            set { dataSoruce = value; }
        }
        ////private  ComboBox.ObjectCollection items = null;

        //public override ComboBox.ObjectCollection Items
        //{
        //    get { return items; }
        //    set { items = value; }
        //}

       
        private string valueMember;

        public string ValueMember
        {
            get { return valueMember; }
            set { valueMember = value; }
        }
        private string displayMember;

        public string DisplayMember
        {
            get { return displayMember; }
            set { displayMember = value; }
        }        

        public DataGridViewComboBoxExColumn()
            : base(new DataGridViewComboBoxExCell())
        {

        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewComboBoxExCell)))
                {
                    throw new InvalidCastException("is not DataGridViewComboxExCell");
                }
                base.CellTemplate = value;
            }
        }
        private DataGridViewComboBoxExCell ComboBoxCellTemplate
        {
            get
            {
                return (DataGridViewComboBoxExCell)this.CellTemplate;
            }
        }
    }
}
