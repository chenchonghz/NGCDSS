using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSSystemData;
using CDSSDBAccess;

namespace CDSS
{
    public partial class Basicinfo : InfoFormBaseClass
    {
        public bool bLoaded = false;    //标记数据是否已经加载过

        #region 系统事件
        public Basicinfo()
        {
            InitializeComponent();
            //修改by Jyl，081223，bugB4控制出生日期不能大于当前的日期
            dTimeP_birth.MaxDate = DateTime.Now;
        }

        private void cmb_national_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_national.SelectedIndex == 7)
                cmb_national.Text = "";
        }

        /**************************************************************************
        *  修改人：XY；
        * 修改时间：20081221；
        * 修改说明：数据必填项是否输入完整判断及提示功能；
        * 修改部分：修改“确定按钮单击事件”。
        ***************************************************************************/
        /// <summary>
        /// 确定按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckPatName()) return;
            if (!lblOldName.Text.Equals("*"))//不是新病人
            {
                if (lblOldName.Text != txb_name.Text.Trim())
                    if (DialogResult.Yes != MessageBox.Show("病人名字变更：\r\n" + lblOldName.Text + "->" + txb_name.Text, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        return;
                string sqlstr = "select TOP 1 RecordSEQ from CDSS_RecordHistory  where PatSEQ =" + lblPatSEQ.Text + " order by RecordSEQ desc";//修改语句
                string recordseq = DBAccess.GetStringScalar(sqlstr);
                if (recordseq != "")
                {
                    DBAccess.GetOnlyDataFromDB(Convert.ToInt16(recordseq));
                    PatInfo.bNewPatient = false;//设置非新入病人标志
                }
            }
            else
            {
                PatInfo.bNewPatient = true; //设置新病人标志
            }

            LoadDataFromUIToVar();
            if (ForbidNon())
            {
                //修改提示信息，对出生日期特别提醒，Revised by XY,20081222.
                MessageBox.Show("*为必填项，请填写！\r\n\r\n（注：出生日期不能为当前日期。）", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult = DialogResult.OK;

            /********************************************************************************
            *作用：用户点击新入病人界面的【确定】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "新入病人";
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

            this.Close();

        }

        /// <summary>
        /// 检查是否有同名病人
        /// false表示需要确认为新增同名病人
        /// true表示不需确认同名病人
        /// </summary>
        /// <returns></returns>
        private bool CheckPatName()
        {
            DESClass DESClass = new DESClass();
            if (lblPatSEQ.Text != "*") return false;
            string sql = "select * from CDSS_PatBasicInfo where PatName= '" + DESClass.DESEncrypt(txb_name.Text.ToString().Trim()) + "'";
            DataTable patDT = DBAccess.GetDataSet(sql);
            if (patDT.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("已存在同名的病人，请确认是否新增该名病人？\r\n选择“否”可显示重名病人选项", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    return false;
                else
                {
                    ShowPatByName();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 当作为子窗体加载时，隐藏确定和取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Basicinfo_Load(object sender, EventArgs e)
        {
            if (this.TopLevel == false)
            {
                this.pnlButtonBar.Hide();
                //add by zx 100414 医生查询病人是否存在
                txb_PatID.KeyDown -= new KeyEventHandler(txb_PatID_KeyDown);
                this.txb_name.KeyDown -= new KeyEventHandler(txb_PatID_KeyDown);
            }
            else
            {
                //add by lch 090318 修复BugDB00005639 设置就诊时间显示为年月日形式
                this.txb_time.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //add by lch 090318 修复ugDB00005642，必填项都设置一个默认值。
                this.cmb_national.SelectedIndex = 0;
                this.cmb_sex.SelectedIndex = 0;
                //add by zx 100414 医生查询病人是否存在
                this.txb_PatID.KeyDown += new KeyEventHandler(txb_PatID_KeyDown);
                this.txb_name.KeyDown += new KeyEventHandler(txb_PatID_KeyDown);
            }

        }

        private void Basicinfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.TopLevel)
                return; //如果是作为新入页面使用,则直接返回

            if (this.Visible)
            {
                if (!bLoaded)
                {
                    LoadDataFromVarToUI(); //在窗体显示出来的时候加载数据
                    txb_PatID.Enabled = false;
                }
            }
        }

        #endregion

        #region 用户事件
        /// <summary>
        /// 当页面内容改变时引发该事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e"></param>
        protected void DataModified(object sender, EventArgs e)
        {
            //如果不是作为子窗体或者是子窗体但在这之前数据未保存过,则直接返回
            if (this.TopLevel || (!this.TopLevel && IsModified) || (!this.TopLevel && !bLoaded))
                return;

            //引发事件,通知父窗体更新保存按钮状态
            RaiseDataChangedEvent(this, e);
            IsModified = true;
        }

        /// <summary>
        /// 病人基本信息更改事件，用于通知mainform更新病人信息栏
        /// </summary>
        public event CustomEventHandle PatBasicInfoChanged;
        protected void RaisePatBasicInfoChangedEvent()
        {
            CustomEventHandle temp = PatBasicInfoChanged;
            if (temp != null)
                temp();
        }


        #endregion

        #region 功能函数
        /**************************************************************************
        * 修改人：XY；
        * 修改时间：20081221；
        * 修改说明：数据必填项是否输入完整判断及提示功能；
        * 修改部分：添加“判断必选项是否为空函数”。
        * 备注：出生日期判断标准不能为当前日期。目前只进行判断，需要完善为：
                可提示“患者当前输入出生日期为系统时间，请确认是否正确？”，
                如果医生点击“确定”,允许输入为系统时间。若“取消”,则需要医生重新输入。
        ***************************************************************************/
        /// <summary>
        /// 判断必选项是否为空函数
        /// </summary>
        /// <returns></returns>
        public bool ForbidNon()
        {
            if (!PatInfo.bMustFill)
            {
                return false;
            }
            else
            {
                LoadDataFromVarToUI();
                txb_name.Text = txb_name.Text.Trim();//去掉文本框中空格, Revised by XY,20081222.
                if (txb_name.Text == "" || cmb_national.Text == "" || cmb_sex.Text == "" || dTimeP_birth.Value.Date == DateTime.Now.Date)
                    return true;
                else
                    return false;
            }
        }

        public override void LoadDataFromUIToVar()
        {
            if ((!this.TopLevel && !IsModified) || (!this.TopLevel && PatInfo.bNewPatient && !bLoaded))
                return;    //在子窗体已保存的情况下就不需要再保存了

            //病人基本信息
            if (PatInfo.bNewPatient)
                GlobalData.PatBasicInfo.PatSEQ = this.lblPatSEQ.Text.Trim();
            GlobalData.PatBasicInfo.PatAddress = this.txb_Address.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatBirthCity = this.txb_city.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatBirthday = DateTime.Parse(this.dTimeP_birth.Value.ToShortDateString().Trim());
            GlobalData.PatBasicInfo.PatBirthProvince = this.cmb_province.Text.ToString().Trim();
            if (this.cmb_ChildCount.Text.ToString() != "")
            {
                GlobalData.PatBasicInfo.PatChildCount = int.Parse(this.cmb_ChildCount.Text.ToString().Trim());
            }
            else
            {
                GlobalData.PatBasicInfo.PatChildCount = 0;
            }
            GlobalData.PatBasicInfo.PatEducationLevel = this.cmb_edu.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatID = this.txb_PatID.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatIncome = this.cmb_income.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatIncomeSource = this.cmb_moneysource.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatName = this.txb_name.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatNational = this.cmb_national.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatPhone = this.txb_phone.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatProfessional = this.cmb_Professional.Text.ToString().Trim();
            GlobalData.PatBasicInfo.PatSex = this.cmb_sex.Text.ToString().Trim();
            if (this.cmb_SiblingsCount.Text.ToString() != "")
            {
                GlobalData.PatBasicInfo.PatSiblingsCount = int.Parse(this.cmb_SiblingsCount.Text.ToString().Trim());
            }
            else
            {
                GlobalData.PatBasicInfo.PatSiblingsCount = 0;
            }
            GlobalData.PatBasicInfo.PatTreatmentCost = this.cmb_costs.Text.ToString().Trim();
            if (this.txb_time.Text != "")
            {
                GlobalData.PatBasicInfo.PatVisitDateTime = DateTime.Parse(this.txb_time.Text.ToString().Trim());
            }
            else
            {
                GlobalData.PatBasicInfo.PatVisitDateTime = DateTime.Now;
            }
            GlobalData.PatBasicInfo.PatZipcode = this.txb_zipcode.Text.ToString().Trim();

            //索引信息
            GlobalData.RecordInfo.UserID = GlobalData.UserInfo.UserID;
            GlobalData.RecordInfo.RecordDateTime = DateTime.Now;
            GlobalData.RecordInfo.IEVersion = "";
            GlobalData.RecordInfo.InferenceState = "";
            GlobalData.RecordInfo.KnowledgeVersion = "";

            RaisePatBasicInfoChangedEvent();    //修改病人信息栏的信息

            IsModified = false;
        }

        public override void LoadDataFromVarToUI()
        {
            this.txb_name.Text = GlobalData.PatBasicInfo.PatName.ToString();
            this.txb_time.Text = GlobalData.PatBasicInfo.PatVisitDateTime.ToString("yyyy-MM-dd");

            string tmpSex = GlobalData.PatBasicInfo.PatSex.ToString().Trim();
            if (tmpSex == "")
                this.cmb_sex.SelectedIndex = -1;
            else
                this.cmb_sex.Text = tmpSex;

            this.cmb_edu.Text = GlobalData.PatBasicInfo.PatEducationLevel.ToString();
            this.cmb_national.Text = GlobalData.PatBasicInfo.PatNational.ToString();
            this.cmb_moneysource.Text = GlobalData.PatBasicInfo.PatIncomeSource.ToString();
            this.dTimeP_birth.Text = GlobalData.PatBasicInfo.PatBirthday.ToString();
            this.cmb_costs.Text = GlobalData.PatBasicInfo.PatTreatmentCost.ToString();
            this.txb_phone.Text = GlobalData.PatBasicInfo.PatPhone.ToString();
            this.txb_zipcode.Text = GlobalData.PatBasicInfo.PatZipcode.ToString();

            string tmpChildCount = GlobalData.PatBasicInfo.PatChildCount.ToString().Trim();
            //Bug5734, Revised By ZQY 090327
            if (tmpChildCount == "" || tmpChildCount == "0")
                this.cmb_ChildCount.SelectedIndex = -1;
            else
                this.cmb_ChildCount.Text = tmpChildCount;

            this.cmb_Professional.Text = GlobalData.PatBasicInfo.PatProfessional.ToString();

            string tmpSiblingsCount = GlobalData.PatBasicInfo.PatSiblingsCount.ToString().Trim();
            //Bug5734, Revised By ZQY 090327
            if (tmpSiblingsCount == "" || tmpSiblingsCount == "0")
                this.cmb_SiblingsCount.SelectedIndex = -1;
            else
                this.cmb_SiblingsCount.Text = tmpSiblingsCount;

            this.cmb_income.Text = GlobalData.PatBasicInfo.PatIncome.ToString();
            this.cmb_province.Text = GlobalData.PatBasicInfo.PatBirthProvince.ToString();
            this.txb_city.Text = GlobalData.PatBasicInfo.PatBirthCity.ToString();
            this.txb_Address.Text = GlobalData.PatBasicInfo.PatAddress.ToString();
            this.txb_PatID.Text = GlobalData.PatBasicInfo.PatID.ToString();

            //设置数据已加载标志
            this.bLoaded = true;
        }

        /// <summary>
        /// 限定只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// 清空页面内容
        /// </summary>
        /// 该函数意义不大，应该可以删除 After NG CDSSBuild003，ZQY 090327
        public void ClearData()
        {
            foreach (System.Windows.Forms.Control count in this.pnlBasicInfo.Controls)
            {
                if (count is TextBox)
                    count.Text = String.Empty;
                if (count is ComboBox)
                    count.Text = String.Empty;
                if (count is MaskedTextBox)
                    count.Text = String.Empty;
            }
            //add by lch 090406 清空界面数据，使得ComboBox的DropDownStyle属性为DropDownList的无选中项
            this.cmb_sex.SelectedIndex = -1;
            this.cmb_ChildCount.SelectedIndex = -1;
            this.cmb_SiblingsCount.SelectedIndex = -1;

            //修改by Jyl，081223，bugB4控制出生日期不能大于当前的日期
            dTimeP_birth.MaxDate = DateTime.Now;

            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
        }

        #endregion

        private void txb_zipcode_MaskChanged(object sender, EventArgs e)
        {
            if (this.txb_zipcode.Text.Length <= 6)
                return;
            else
            {
                this.txb_zipcode.SelectionLength = 6;
                this.txb_zipcode.ScrollToCaret();
            }
        }

        // add by lch 090318 修复BugDB00005647，BugDB00005645,限制文本输入长度
        /// <summary>
        /// 邮政编码的文本判断，超过6位则失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb_zipcode_TextChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (this.txb_zipcode.Text.Length <= 6)
                return;
            else
            {
                this.txb_zipcode.Text = this.txb_zipcode.Text.Substring(0, 6);
                this.btnOK.Focus();
            }
        }
        // add by lch 090318 修复BugDB00005647,BugDB00005645，限制文本输入长度
        /// <summary>
        /// 联系电话的文本判断，超过12位则失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb_phone_TextChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (this.txb_phone.Text.Length <= 12)
                return;
            else
            {
                this.txb_phone.Text = this.txb_phone.Text.Substring(0, 12);
                this.cmb_costs.Focus();
            }
        }


        /**************************************************************************
        * 修改人：ZX；
        * 修改时间：20100414；
        * 修改说明：医生输入病人ID按回车键查询该患者的信息
        ***************************************************************************/
        private void txb_PatID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox s = sender as TextBox;
                if (s.Name == "txb_PatID")
                    PatIsExist();
                else
                    ShowPatByName();
            }
        }

        /// <summary>
        /// 确认病人是否存在
        /// 若存在，则从数据库中提取病人基本信息将各项填满
        /// 若不存在，则由医生手动填写病人基本信息
        /// </summary>
        private void PatIsExist()
        {
            if (txb_PatID.Text.ToString().Trim() == String.Empty) return;
            string sql = "select * from CDSS_PatBasicInfo where PatID= '" + txb_PatID.Text.ToString().Trim() + "'";
            DataTable patTable = DBAccess.GetDataSet(sql);
            if (patTable.Rows.Count == 0)
            {
                MessageBox.Show("查无该患者ID的患者信息，请输入", "提示");
                lblPatSEQ.Text = "*";
            }
            else
            {
                FillUIFromDB(patTable.Rows[0]);
            }
        }

        /// <summary>
        /// 从数据库中取病人信息填充界面
        /// </summary>
        private void FillUIFromDB(DataRow dr)
        {
            DESClass DESClass = new DESClass();
            string PatName = DESClass.DESDecrypt(dr["PatName"].ToString());

            if (PatName != "")
            {
                if (PatName.Trim().ToString().IndexOf('\0') > 0)
                {
                    this.lblOldName.Text = this.txb_name.Text = PatName.Substring(0, PatName.Trim().ToString().IndexOf('\0'));
                }
                else
                {
                    this.lblOldName.Text = this.txb_name.Text = PatName;
                }
            }
            else
            {
                this.lblOldName.Text = this.txb_name.Text = PatName;
            }

            string tmpSex = dr["PatSex"].ToString().Trim();
            if (tmpSex == "")
                this.cmb_sex.SelectedIndex = -1;
            else
                this.cmb_sex.Text = tmpSex;

            this.cmb_edu.Text = dr["PatEducationLevel"].ToString();
            this.cmb_national.Text = dr["PatNational"].ToString();
            this.cmb_moneysource.Text = dr["PatIncomeSource"].ToString();

            string PatBirthday = DESClass.DESDecrypt(dr["PatBirthday"].ToString());
            if (PatBirthday != "")
            {
                this.dTimeP_birth.Text = DateTime.Parse(PatBirthday).ToString("yyyy-MM-dd");
            }
            else
            {
                this.dTimeP_birth.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd");
            }

            this.cmb_costs.Text = dr["PatTreatmentCost"].ToString();
            this.txb_phone.Text = dr["PatPhone"].ToString();
            this.txb_zipcode.Text = dr["PatZipcode"].ToString();

            string tmpChildCount = dr["PatChildCount"].ToString().Trim();

            if (tmpChildCount == "" || tmpChildCount == "0")
                this.cmb_ChildCount.SelectedIndex = -1;
            else
                this.cmb_ChildCount.Text = tmpChildCount;

            this.cmb_Professional.Text = dr["PatProfessional"].ToString();

            string tmpSiblingsCount = dr["PatSiblingsCount"].ToString().Trim();

            if (tmpSiblingsCount == "" || tmpSiblingsCount == "0")
                this.cmb_SiblingsCount.SelectedIndex = -1;
            else
                this.cmb_SiblingsCount.Text = tmpSiblingsCount;

            this.cmb_income.Text = dr["PatIncome"].ToString();
            this.cmb_province.Text = dr["PatBirthProvince"].ToString();
            this.txb_city.Text = dr["PatBirthCity"].ToString();
            this.txb_Address.Text = dr["PatAddress"].ToString();
            this.txb_PatID.Text = dr["PatID"].ToString();
            this.txb_PatID.Enabled = false;
            this.lblPatSEQ.Text = dr["PatSEQ"].ToString();
        }

        /// <summary>
        /// 显示重名候选框
        /// </summary>
        private void ShowPatByName()
        {
            if (txb_name.Text.ToString().Trim() == String.Empty) return;
            DESClass DESClass = new DESClass();
            ListView list = new ListView();
            list.Columns.Add("序号", 40, HorizontalAlignment.Center);
            list.Columns.Add("姓名", 90, HorizontalAlignment.Center);
            list.Columns.Add("出生日期", 90, HorizontalAlignment.Center);
            list.FullRowSelect = true;
            list.MultiSelect = false;
            list.View = View.Details;

            string sql = "select * from CDSS_PatBasicInfo where PatName= '" + DESClass.DESEncrypt(txb_name.Text.ToString().Trim()) + "'";
            DataTable patTable = DBAccess.GetDataSet(sql);

            for (int i = 0; i < patTable.Rows.Count; i++)
            {
                DataRow dr = patTable.Rows[i];
                ListViewItem item = new ListViewItem();
                item.Tag = dr;
                item.Text = (i + 1).ToString();
                string name = DESClass.DESDecrypt(dr["PatName"].ToString()).Replace("\0", "") + "(" + dr["PatSex"].ToString().Trim() + ")";
                item.SubItems.Add(name);
                item.SubItems.Add(Convert.ToDateTime(DESClass.DESDecrypt(dr["PatBirthday"].ToString()).Replace("\0", "")).ToString("yyyy-MM-dd"));
                list.Items.Add(item);
            }

            list.Click += delegate(object sender, EventArgs e)
            {
                FillUIFromDB(list.SelectedItems[0].Tag as DataRow);
            };

            list.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (list.SelectedItems.Count > 0)
                    {
                        FillUIFromDB(list.SelectedItems[0].Tag as DataRow);
                    }
                }
            };

            ToolStripControlHost listhost = new ToolStripControlHost(list);
            ToolStripDropDown dropDown = new ToolStripDropDown();
            dropDown.Opacity = 0.8;
            dropDown.Width = 225;
            dropDown.DropShadowEnabled = false;
            dropDown.Items.Add(listhost);
            listhost.Size = new Size(225 - 2, 200);
            dropDown.Show(txb_name, new Point(txb_name.Width, 0));

        }
    }
}