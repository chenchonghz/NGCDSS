using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib.MedicineControlLib;
using System.Text.RegularExpressions;

namespace CDSSCtrlLib
{
    /********************************************************************
    Created at:	2009-3
    Author:	lch	
    File Name: MedicineControl
    purpose: 用于CDSS用药信息的显示	
    *********************************************************************/
    public partial class MedicineControl : UserControl
    {
        //修改类成员变量命名
        private MedicineDictionary MedicineDictionary = new MedicineDictionary();
        private List<MedicineInfo> ListMedicineInfo = new List<MedicineInfo>();
        private string m_strDrugType = string.Empty;

        private Color DgvBgColor = new Color();
        private Color DgvColumnHeadersBgColor = new Color();
        private Color DgvRowHeadersBgColor = new Color();
        private Color DgvSelectedRowColor = new Color();
        private Font DgvFont = new Font("宋体", 12F);

        public MedicineControl()
        {
            InitializeComponent();

            #region dataGridView添加列

            DataGridViewComboBoxExColumn DgvcboDrugType = new DataGridViewComboBoxExColumn();
            this.dataGridView1.Columns.Add(DgvcboDrugType);
            DgvcboDrugType.HeaderText = "药品类别";
            DgvcboDrugType.Name = "DgvcboDrugType";
            DgvcboDrugType.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DgvcboDrugType.Width = 90;

            DataGridViewComboBoxExColumn DgvcboDrugName = new DataGridViewComboBoxExColumn();
            this.dataGridView1.Columns.Add(DgvcboDrugName);
            DgvcboDrugName.HeaderText = "药品名称";
            DgvcboDrugName.Name = "DgvcboDrugName";
            DgvcboDrugName.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DgvcboDrugName.Width = 90;

            DataGridViewTextBoxColumn DgvtxtDrugAmount = new DataGridViewTextBoxColumn();
            this.dataGridView1.Columns.Add(DgvtxtDrugAmount);
            DgvtxtDrugAmount.HeaderText = "剂量";
            DgvtxtDrugAmount.Name = "DgvtxtDrugAmount";
            DgvtxtDrugAmount.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            DgvtxtDrugAmount.Width = 60;

            DataGridViewComboBoxExColumn DgvcboDrugUnit = new DataGridViewComboBoxExColumn();
            this.dataGridView1.Columns.Add(DgvcboDrugUnit);
            DgvcboDrugUnit.HeaderText = "单位";
            DgvcboDrugUnit.Name = "DgvcboDrugUnit";
            DgvcboDrugUnit.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DgvcboDrugUnit.Width = 70;

            DataGridViewComboBoxExColumn DgvcboDrugRout = new DataGridViewComboBoxExColumn();
            this.dataGridView1.Columns.Add(DgvcboDrugRout);
            DgvcboDrugRout.HeaderText = "途径";
            DgvcboDrugRout.Name = "DgvcboDrugRout";
            DgvcboDrugRout.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DgvcboDrugRout.Width = 70;

            DataGridViewComboBoxExColumn DgvcboDrugFre = new DataGridViewComboBoxExColumn();
            this.dataGridView1.Columns.Add(DgvcboDrugFre);
            DgvcboDrugFre.HeaderText = "频次";
            DgvcboDrugFre.Name = "DgvcboDrugFre";
            DgvcboDrugFre.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DgvcboDrugFre.Width = 80;

            MaskedTextBoxColumn DgvMaskedTextBoxColumn1 = new MaskedTextBoxColumn();
            DgvMaskedTextBoxColumn1.HeaderText = "用药开始日期";
            DgvMaskedTextBoxColumn1.Mask = "0000-00";
            DgvMaskedTextBoxColumn1.Width = 118;
            DgvMaskedTextBoxColumn1.Name = "DgvMaskedTextBoxColumn1";
            //add by lch 090518 设置排序功能
            DgvMaskedTextBoxColumn1.SortMode=DataGridViewColumnSortMode.Automatic;
            DgvMaskedTextBoxColumn1.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns.Add(DgvMaskedTextBoxColumn1);

            MaskedTextBoxColumn DgvMaskedTextBoxColumn2 = new MaskedTextBoxColumn();
            DgvMaskedTextBoxColumn2.HeaderText = "用药结束日期";
            DgvMaskedTextBoxColumn2.Mask = "0000-00";
            DgvMaskedTextBoxColumn2.Width = 118;
            DgvMaskedTextBoxColumn2.Name = "DgvMaskedTextBoxColumn2";
            //add by lch 090518 设置排序功能
            DgvMaskedTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            DgvMaskedTextBoxColumn2.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns.Add(DgvMaskedTextBoxColumn2);

            #endregion

            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            //该值指示在对应用程序启用了可视样式的情况下，行标题和列标题是否使用用户当前主题的可视样式
            this.dataGridView1.EnableHeadersVisualStyles = false;
        }

        #region 功能函数
        /// <summary>
        /// 获取所有列表信息
        /// </summary>
        private void GetData()
        {
            if (ListMedicineInfo.Count > 0)
            {
                ListMedicineInfo.Clear();
            }
            else
            {
                ListMedicineInfo = new List<MedicineInfo>();
            }

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                MedicineInfo oMedicineInfo = new MedicineInfo();

                if ((this.dataGridView1.Rows[i].Cells["DgvcboDrugType"].Value.Equals("") || this.dataGridView1.Rows[i].Cells["DgvcboDrugType"].Value == null)
                    && (this.dataGridView1.Rows[i].Cells["DgvcboDrugName"].Value.Equals("") || this.dataGridView1.Rows[i].Cells["DgvcboDrugName"].Value == null))
                {
                    continue;
                }
                oMedicineInfo.DrugType = this.dataGridView1.Rows[i].Cells["DgvcboDrugType"].Value.ToString();
                oMedicineInfo.DrugName = this.dataGridView1.Rows[i].Cells["DgvcboDrugName"].Value.ToString();
                oMedicineInfo.DrugAmount = this.dataGridView1.Rows[i].Cells["DgvtxtDrugAmount"].Value.ToString();
                
                //单位
                if (this.dataGridView1.Rows[i].Cells["DgvcboDrugUnit"].Value.Equals("") || this.dataGridView1.Rows[i].Cells["DgvcboDrugUnit"].Value == null)
                {
                    oMedicineInfo.DrugUnits = ""; 
                }
                else
                {
                    oMedicineInfo.DrugUnits = this.dataGridView1.Rows[i].Cells["DgvcboDrugUnit"].Value.ToString();
                }
                //途径
                if (this.dataGridView1.Rows[i].Cells["DgvcboDrugRout"].Value.Equals("") || this.dataGridView1.Rows[i].Cells["DgvcboDrugRout"].Value == null)
                {
                    oMedicineInfo.DrugByRoute = "";
                }
                else
                {
                    oMedicineInfo.DrugByRoute = this.dataGridView1.Rows[i].Cells["DgvcboDrugRout"].Value.ToString();
                }
                //频次
                if (this.dataGridView1.Rows[i].Cells["DgvcboDrugFre"].Value.Equals("") || this.dataGridView1.Rows[i].Cells["DgvcboDrugFre"].Value == null)
                {
                    oMedicineInfo.DrugFrequency = "";
                }
                else
                {
                    oMedicineInfo.DrugFrequency = this.dataGridView1.Rows[i].Cells["DgvcboDrugFre"].Value.ToString();                   
                }

                oMedicineInfo.DrugBeginTime = DateTime.Parse(this.dataGridView1.Rows[i].Cells["DgvMaskedTextBoxColumn1"].Value.ToString());
                oMedicineInfo.DrugEndTime = DateTime.Parse(this.dataGridView1.Rows[i].Cells["DgvMaskedTextBoxColumn2"].Value.ToString());

                ListMedicineInfo.Add(oMedicineInfo);
            }
        }


        /// <summary>
        /// 绑定所有列表信息
        /// </summary>
        private void DataBind()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < ListMedicineInfo.Count; i++)
            {
                DataGridViewRow Row = new DataGridViewRow();

                foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                {
                    Row.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);  //给行添加空单元格

                }

                Row.Height = 20;
                this.dataGridView1.Rows.Add(Row);
                //添加完一个空行，再给单元格绑定数据
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["DgvcboDrugType"].Value = ListMedicineInfo[i].DrugType;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["DgvcboDrugName"].Value = ListMedicineInfo[i].DrugName;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["DgvtxtDrugAmount"].Value = ListMedicineInfo[i].DrugAmount;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["DgvcboDrugUnit"].Value = ListMedicineInfo[i].DrugUnits;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["DgvcboDrugRout"].Value = ListMedicineInfo[i].DrugByRoute;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["DgvcboDrugFre"].Value = ListMedicineInfo[i].DrugFrequency;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["DgvMaskedTextBoxColumn1"].Value = ListMedicineInfo[i].DrugBeginTime.ToString("yyyy-MM");
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["DgvMaskedTextBoxColumn2"].Value = ListMedicineInfo[i].DrugEndTime.ToString("yyyy-MM");
            }

        }


        /// <summary>
        /// 删除所选中行
        /// </summary>
        private void DeleteRow(object sender, CancelEventArgs ee)
        {
            RaiseRowDeletingEvent(sender, ee);
            if (!ee.Cancel)
            {
                int deletedCount = dataGridView1.SelectedRows.Count;
                //在属性中设置DataGridView.AllowUserToDeleteRows =false,即不触发UserDeletingRow事件，则在程序中循环删除选择行
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(r);
                }
                RaiseValueChangedEvent(sender, ee);
            }           
        }


        #endregion

        #region 公开方法

        /// <summary>
        /// 设置控件下拉框信息
        /// </summary>
        /// <param name="oMedicineDictionary"></param>
        public void SetMedicineDictionary(MedicineDictionary oMedicineDictionary)
        {
            this.MedicineDictionary = oMedicineDictionary;

            /*以下代码绑定Datagridview下拉列表框数据*/
            //药品类别
            List<string> listDrugType = new List<string>();
            for (int j = 0; j < oMedicineDictionary.MedicineDictList.Count; j++)
            {
                listDrugType.Add(oMedicineDictionary.MedicineDictList[j].drugType);
            }
            if (this.dataGridView1.Columns["DgvcboDrugType"] is DataGridViewComboBoxExColumn)
            {
                DataGridViewComboBoxExColumn col = this.dataGridView1.Columns["DgvcboDrugType"] as DataGridViewComboBoxExColumn;
                col.DataSource = listDrugType;
                col.Selected = false;
            }
            //单位
            List<string> listDrugUnits = new List<string>();
            for (int j = 0; j < MedicineDictionary.DrugUnitsList.Count; j++)
            {
                listDrugUnits.Add(MedicineDictionary.DrugUnitsList[j].drugUnit);
            }
            if (this.dataGridView1.Columns["DgvcboDrugUnit"] is DataGridViewComboBoxExColumn)
            {
                DataGridViewComboBoxExColumn col = this.dataGridView1.Columns["DgvcboDrugUnit"] as DataGridViewComboBoxExColumn;
                col.DataSource = listDrugUnits;
                col.Selected = false;
            }
            //途径
            List<string> listByRoute = new List<string>();
            for (int j = 0; j < MedicineDictionary.DrugByRouteList.Count; j++)
            {
                listByRoute.Add(MedicineDictionary.DrugByRouteList[j].drugByRoute);
            }
            if (this.dataGridView1.Columns["DgvcboDrugRout"] is DataGridViewComboBoxExColumn)
            {
                DataGridViewComboBoxExColumn col = this.dataGridView1.Columns["DgvcboDrugRout"] as DataGridViewComboBoxExColumn;
                col.DataSource = listByRoute;
                col.Selected = false;
            }
            //频次
            List<string> listDrugFrequency = new List<string>();
            for (int j = 0; j < MedicineDictionary.DrugFrequencyList.Count; j++)
            {
                listDrugFrequency.Add(MedicineDictionary.DrugFrequencyList[j].drugFrequency);
            }
            if (this.dataGridView1.Columns["DgvcboDrugFre"] is DataGridViewComboBoxExColumn)
            {
                DataGridViewComboBoxExColumn col = this.dataGridView1.Columns["DgvcboDrugFre"] as DataGridViewComboBoxExColumn;
                col.DataSource = listDrugFrequency;
                col.Selected = false;
            }
            
        }

        /// <summary>
        /// 设置控件单元格具体内容
        /// </summary>
        /// <param name="oMedicineInfoList"></param>
        public void SetMedicineInfo(List<MedicineInfo> MedicineInfoList)
        {            
            //if (MedicineInfoList.Count >0)
            {
                this.ListMedicineInfo = MedicineInfoList;
                //绑定历史用药信息
                DataBind();
            }
        }
        /// <summary>
        /// 读取控件单元格具体内容
        /// </summary>
        /// <returns></returns>
        public List<MedicineInfo> GetMedicineInfo()
        {
            //TODO:返回List是否合适？引用的问题？
            if (this.dataGridView1.Rows.Count > 0)
            {
                //获取所有列表信息
                GetData();
            }
            return ListMedicineInfo;
        }
        #endregion

        #region 公开属性

        /// <summary>
        /// 设置控件背景颜色
        /// </summary>
        [Category("用户控件属性"),
        Description("读取或设置控件背景颜色"),
        DefaultValue("205, 228, 236")]
        public Color MedicineControlBgColor
        {
            get
            {
                DgvBgColor = this.dataGridView1.BackgroundColor;
                return DgvBgColor;
            }
            set
            {
                DgvBgColor = value;
                this.dataGridView1.BackgroundColor = DgvBgColor;
            }
        }

        /// <summary>
        /// 设置控件列标题的背景颜色
        /// </summary>
        [Category("用户控件属性"),
        Description("读取或设置控件列标题的背景颜色"),
       DefaultValue("Control")]
        public Color MedicineControlColumnheadersBackColor
        {
            get
            {
                DgvColumnHeadersBgColor = this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor;
                return DgvColumnHeadersBgColor;
            }
            set
            {
                DgvColumnHeadersBgColor = value;
                this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = DgvColumnHeadersBgColor;
            }
        }

        /// <summary>
        /// 设置控件行标题的背景颜色
        /// </summary>
        [Category("用户控件属性"),
        Description("读取或设置控件行标题的背景颜色"),
       DefaultValue("Control")]
        public Color MedicineControlRowheadersBackColor
        {
            get
            {
                DgvRowHeadersBgColor = this.dataGridView1.RowHeadersDefaultCellStyle.BackColor;
                return DgvRowHeadersBgColor;
            }
            set
            {
                DgvRowHeadersBgColor = value;
                this.dataGridView1.RowHeadersDefaultCellStyle.BackColor = DgvRowHeadersBgColor;
            }
        }

        /// <summary>
        /// 设置选中行的背景颜色
        /// </summary>
        [Category("用户控件属性"),
        Description("读取或设置选中行的背景颜色"),
       DefaultValue("Silver")]
        public Color MedicineControlSelectedRowColor
        {
            get
            {
                DgvSelectedRowColor = this.dataGridView1.RowsDefaultCellStyle.SelectionBackColor;

                return DgvSelectedRowColor;
            }
            set
            {
                DgvSelectedRowColor = value;
                this.dataGridView1.RowsDefaultCellStyle.SelectionBackColor = DgvSelectedRowColor;
                this.dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = DgvSelectedRowColor;
            }
        }

        /// <summary>
        /// 设置控件字体
        /// </summary>
        [Category("用户控件属性"),
       Description("读取或设置控件字体"),
       DefaultValue("宋体, 10.5pt")]
        public Font MedicineControlFont
        {
            get
            {
                DgvFont = this.dataGridView1.Font;
                return DgvFont;
            }
            set
            {
                DgvFont = value;
                this.dataGridView1.Font = DgvFont;
            }
        }

        #endregion

        /// <summary>
        /// 编辑dataGridView的单元格时触发,根据不同的单元格绑定下拉列表信息和判断日期输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //药品名称
            if (this.dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                ComboBox combobox = e.Control as ComboBox;
                string DrugType = string.Empty;
                combobox.Items.Clear();

                if (this.dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    DrugType = this.dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                }
                else
                {
                    DrugType = string.Empty;
                    return;
                }

                for (int j = 0; j < MedicineDictionary.MedicineDictList.Count; j++)
                {
                    if (MedicineDictionary.MedicineDictList[j].drugType.Equals(DrugType))
                    {
                        for (int i = 0; i < MedicineDictionary.MedicineDictList[j].drugNamesList.Count; i++)
                        {
                            combobox.Items.Add(MedicineDictionary.MedicineDictList[j].drugNamesList[i].drugName);

                        }
                        break;
                    }
                }

            }
        }

        /// <summary>
        /// 在单元格编辑完后执行，判断药品类别、用药开始时间、用药结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //药品类别
            string DrugType = string.Empty;
            if (e.ColumnIndex == 0)
            {
                if (this.dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    DrugType = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();//编辑之后的值
                    if (!DrugType.Equals(m_strDrugType))
                    {
                        //add by lch 090318 修复 BugDB00005636，选择药品类别之后，判断一下当前的药品名称是否属于该类别，如果属于就不需要清空，不属于的话就清空。
                        for (int j = 0; j < MedicineDictionary.MedicineDictList.Count; j++)
                        {
                            string DrugName = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                            if (DrugType.Equals(MedicineDictionary.MedicineDictList[j].drugType))
                            {
                                for (int i = 0; i < MedicineDictionary.MedicineDictList[j].drugNamesList.Count; i++)
                                {
                                    if (DrugName.Equals(MedicineDictionary.MedicineDictList[j].drugNamesList[i].drugName))
                                        return;
                                }
                            }

                        }
                        this.dataGridView1.CurrentRow.Cells[1].Value = "";

                    }
                    else
                        return;
                }
                else
                    return;

            }

            //用药开始时间\用药结束时间
            else if (e.ColumnIndex == 6||e.ColumnIndex == 7)
            {
                JudgeDateValidity();
            }
        }
        /// <summary>
        /// 判断日期合法性
        /// </summary>
        private void JudgeDateValidity()
        {
            int currentRow = this.dataGridView1.CurrentRow.Index;

            if (!(this.dataGridView1.CurrentCell.Value == null))
            {
                string Time = this.dataGridView1.CurrentCell.Value.ToString().Trim();


                if (!Time.Equals(""))
                {
                    if (Time.Equals("-"))
                    {
                        this.dataGridView1.CurrentCell.Value = (DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
                        return;
                    }

                    /* revised by lch 090319 修复BugDB00005660,限制年份和日期的输入有效性
                     * 年份为1000-9999
                       月份为1-12  */
                    int year = 0;
                    int month = 0;
                    try
                    {
                        year = int.Parse(Time.Substring(0, Time.IndexOf('-')));
                        month = int.Parse(Time.Substring(Time.IndexOf('-') + 1));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("日期设置不正确！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.dataGridView1.CurrentCell.Value = (DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
                        return;
                    }
                    if (year <1000 || year >9999)
                    {
                        MessageBox.Show("年份范围在1000至9999年之间！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (month <1 || month > 12)
                        {
                            this.dataGridView1.CurrentCell.Value = (DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
                        }
                        else
                            this.dataGridView1.CurrentCell.Value = DateTime.Now.Year + "-" + month.ToString();

                        return;
                    }
                    if (month < 1 || month > 12)
                    {
                        MessageBox.Show("月份范围在01至12月之间！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.dataGridView1.CurrentCell.Value = year.ToString() + "-01";
                        return;
                    }
                }
                else
                    return;
            }
            else
                return;
        }

        /// <summary>
        /// 判断编辑列是否是下拉框，是则显示下拉信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                this.dataGridView1.BeginEdit(true);//将当前的单元格置于编辑模式下
                //this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
                //以下代码用于点击下拉框的单元格时，下拉框马上出现
                //if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
                //{
                //    if (this.dataGridView1.EditingControl != null && this.dataGridView1.EditingControl is ComboBox)
                //    {
                //        this.dataGridView1.BeginEdit(true);
                //        ////ComboBox comboBox = (ComboBox)this.dataGridView1.EditingControl;
                //        ////comboBox.DroppedDown = true;
                //    }
                //}
            }
        }

        #region 删 除

        /// <summary>
        /// 判断键盘按键是否是“Delete”，删除选中数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //Delete按键
            if (e.KeyCode == Keys.Delete)
            {
                //调用功能函数【删除所选中行】
                CancelEventArgs ee = new CancelEventArgs();               
                DeleteRow(sender, ee);
            }
        }


        /// <summary>
        /// 点击右键菜单的【删除】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //调用功能函数【删除所选中行】
            CancelEventArgs ee = new CancelEventArgs();
            DeleteRow(sender, ee);
        }

        /// <summary>
        /// 右键点击行，出现菜单，选择删除，问题：一次只能删一行？？
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex > -1)
                {
                    DataGridViewRow dataGridViewRow1 = dataGridView1.Rows[e.RowIndex];
                    //清除dataGridView1之前所选中的行，再将右键点击的行选中
                    dataGridView1.ClearSelection();
                    dataGridViewRow1.Selected = true;
                    this.dataGridView1.Rows[e.RowIndex].Selected = true;

                    this.dataGridView1.ContextMenuStrip = contextMenuStrip1;
                }
            }
        }
        /// <summary>
        /// 删除，删除选中行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count <= 0)
                return;
            else
            {
                CancelEventArgs ee = new CancelEventArgs();
                DeleteRow(sender, ee);
            }
        }

        #endregion

        #region 添加新行
        /// <summary>
        /// 增加，添加新行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //如果dataGridView1没有行，直接添加一行，如果有，需要判断最后一行是否为空行（判断除了日期列的其他列是否为空）
            if (this.dataGridView1.Rows.Count == 0)
            {
                AddNewRow(0);
            }
            else
            {
                int rowcount = this.dataGridView1.Rows.Count;
                if ((this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugType"].Value.Equals("") || this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugType"].Value == null)
                    && (this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugName"].Value.Equals("") || this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugName"].Value == null)
                    && (this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugUnit"].Value.Equals("") || this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugUnit"].Value == null)
                    && (this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugRout"].Value.Equals("") || this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugRout"].Value == null)
                    && (this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugFre"].Value.Equals("") || this.dataGridView1.Rows[rowcount - 1].Cells["DgvcboDrugFre"].Value == null))
                {
                    return;
                }
                AddNewRow(rowcount);
            }
            RaiseValueChangedEvent(sender, e);
        }
        /// <summary>
        /// 添加新行函数
        /// </summary>
        /// <param name="rowIndex">添加的行所在的行的索引</param>
        private void AddNewRow(int rowIndex)
        {
            DataGridViewRow Row = new DataGridViewRow();
            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
            {
                Row.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);  //给行添加单元格
            }
            Row.Height = 20;
            this.dataGridView1.Rows.Add(Row);

            this.dataGridView1.Rows[rowIndex].Cells["DgvcboDrugType"].Value = "";
            this.dataGridView1.Rows[rowIndex].Cells["DgvcboDrugName"].Value = "";
            this.dataGridView1.Rows[rowIndex].Cells["DgvtxtDrugAmount"].Value = "";
            this.dataGridView1.Rows[rowIndex].Cells["DgvcboDrugUnit"].Value = "";
            this.dataGridView1.Rows[rowIndex].Cells["DgvcboDrugRout"].Value = "";
            this.dataGridView1.Rows[rowIndex].Cells["DgvcboDrugFre"].Value = "";
            //设置日期为今年的1月份
            this.dataGridView1.Rows[rowIndex].Cells["DgvMaskedTextBoxColumn1"].Value = (DateTime.Now.AddMonths(1 - DateTime.Now.Month).ToString("yyyy-MM"));
            this.dataGridView1.Rows[rowIndex].Cells["DgvMaskedTextBoxColumn2"].Value = (DateTime.Now.AddMonths(1 - DateTime.Now.Month).ToString("yyyy-MM"));

            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex].Cells["DgvcboDrugType"];
            this.dataGridView1.Rows[rowIndex].Selected = true;
            this.dataGridView1.BeginEdit(true);
        }
        #endregion

        

        #region 用户事件

        /// <summary>
        /// 用户删除事件
        /// </summary>
        [Category("用户事件"),
        Description("用户删除了该控件界面上的数据时发生")]
        public event CancelEventHandler RowDeleting;
        private void RaiseRowDeletingEvent(object sender, CancelEventArgs e)
        {
            CancelEventHandler temp = RowDeleting;
            if (temp != null)
                temp(sender, e);
        }

        /// <summary>
        /// ValueChanged事件
        /// </summary>
        /// [Category("用户事件"),
        [Category("用户事件"),
        Description("单元格数据变化后触发")]
        public event EventHandler ValueChanged;
        private void RaiseValueChangedEvent(object sender, EventArgs e)
        {
            EventHandler temp = ValueChanged;
            if (temp != null)
                temp(sender, e);
        }
        #endregion
        /// <summary>
        /// 单元格数据变化后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            RaiseValueChangedEvent(sender, e);
        }


    }
}
