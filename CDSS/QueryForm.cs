using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CDSSCtrlLib;
using CDSSDBAccess;
using CDSSSystemData;

namespace CDSS
{
    public partial class QueryForm : InfoFormBaseClass
    {
        //事件定义
        public event CustomEventHandle ShowInfoEnterPageEvent;
        public DataSet dsResult = new DataSet();
        private DataTable TableResult;
        private int oldPatSelect = 0;
        private DataTable patientDataResult;

        private Dictionary<string, Color> chartColorDict = new Dictionary<string, Color>();

        public QueryForm()
        {
            InitializeComponent();
        }

        private void QueryForm_Load(object sender, EventArgs e)
        {
            //出生日期
            this.rdoBirthAll.Checked = true;
            this.dtpBirthFrom.Enabled = false;
            this.dtpBirthTo.Enabled = false;
            //就诊日期
            this.rdoVisitAll.Checked = true;
            this.dtpVisitFrom.Enabled = false;
            this.dtpVisitTo.Enabled = false;
            //性别
            this.rdoSexAll.Checked = true;
            //危险度评估积分
            this.rdoRiskScoreAll.Checked = true;
            this.txtRiskScoreFrom.Enabled = false;
            this.txtRiskScoreTo.Enabled = false;
            //查询结果列表

            //所有列举中对齐，ZQY 090325
            //Chang By ZX，100415 将查询到病人基本信息与病史分开显示
            this.lvPatBasic.Columns.Add("病人ID", 85, HorizontalAlignment.Center);
            this.lvPatBasic.Columns.Add("姓名", 80, HorizontalAlignment.Center);
            this.lvPatBasic.Columns.Add("性别", 45, HorizontalAlignment.Center);
            this.lvPatHistory.Columns.Add("就诊时间", 90/*160*/, HorizontalAlignment.Center);
            this.lvPatHistory.Columns.Add("代谢综合征", 90/*95*/, HorizontalAlignment.Center);
            this.lvPatHistory.Columns.Add("危险度", 60, HorizontalAlignment.Center);
            this.lvPatHistory.Columns.Add("危险度评估积分", 120, HorizontalAlignment.Center);
            this.lvPatHistory.Columns.Add("诊断结论", 75/*150*/, HorizontalAlignment.Center);
            this.lvPatHistory.Columns.Add("缺失信息", 273, HorizontalAlignment.Left);

            //this.lvResult.Columns.Add("RecordSEQ", 80);

            this.medicalGraph.GraphPane.Title.Text = "近期趋势图";
            this.medicalGraph.GraphPane.XAxis.Type = ZedGraph.AxisType.Date;
            this.medicalGraph.GraphPane.XAxis.Scale.Format = "yyyy-MM-dd";
            this.medicalGraph.GraphPane.XAxis.Title.Text = "时间";
            this.medicalGraph.GraphPane.YAxis.Title.Text = string.Empty;
            this.medicalGraph.GraphPane.Y2Axis.IsVisible = true;
            this.medicalGraph.GraphPane.Y2Axis.Title.Text = string.Empty;

            //设置默认焦点，Added By ZQY,090325
            txtName.Focus();

            // 设置默认颜色
            this.chartColorDict.Add(this.checkBoxBMI.Name, Color.Blue);
            this.chartColorDict.Add(this.checkBoxDBP1.Name, Color.DarkSalmon);
            this.chartColorDict.Add(this.checkBoxSBP1.Name, Color.YellowGreen);
            this.chartColorDict.Add(this.checkBoxFBG.Name, Color.DeepPink);
            this.chartColorDict.Add(this.checkBoxTwoHPBG.Name, Color.Purple);
            this.chartColorDict.Add(this.checkBoxHDLC.Name, Color.DarkViolet);
            this.chartColorDict.Add(this.checkBoxLDLC.Name, Color.DodgerBlue);
            this.chartColorDict.Add(this.checkBoxTC.Name, Color.DarkCyan);
            this.chartColorDict.Add(this.checkBoxTG.Name, Color.DarkSeaGreen);

            this.OnItemDataChanged();
        }

        private void rdoBirthPrecision_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoBirthPrecision.Checked)
            {
                this.dtpBirthFrom.Enabled = true;
                this.dtpBirthTo.Enabled = false;
            }
        }

        private void rdoBirthRange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoBirthRange.Checked)
            {
                this.dtpBirthFrom.Enabled = true;
                this.dtpBirthTo.Enabled = true;
            }
        }

        private void rdoBirthAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoBirthAll.Checked)
            {
                this.dtpBirthFrom.Enabled = false;
                this.dtpBirthTo.Enabled = false;
            }
        }

        private void rdoVisitPrecision_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoVisitPrecision.Checked)
            {
                this.dtpVisitFrom.Enabled = true;
                this.dtpVisitTo.Enabled = false;
            }
        }

        private void rdoVisitRange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoVisitRange.Checked)
            {
                this.dtpVisitFrom.Enabled = true;
                this.dtpVisitTo.Enabled = true;
            }
        }

        private void rdoVisitAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoVisitAll.Checked)
            {
                this.dtpVisitFrom.Enabled = false;
                this.dtpVisitTo.Enabled = false;
            }
        }
        /******************************************************
                * Revise History：
                * 2008-12-22 杨艳 修改日期条件的组织方式
        ******************************************************/
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //获取查询条件
            CDSSDBAccess.QueryCondition qc = new CDSSDBAccess.QueryCondition();
            //UserID
            qc.UserID = GlobalData.UserInfo.UserID;
            //2009-8-27:ccj
            //添加ID查询
            qc.PatID = this.ID_num.Text;
            //修改结束
            //姓名        
            qc.strName = this.txtName.Text;
            //性别
            if (this.rdoSexMale.Checked)
                qc.strSex = "男";
            else if (this.rdoSexFemale.Checked)
                qc.strSex = "女";
            else
                qc.strSex = string.Empty;
            //日期舍去时间，传入日期即可
            //截止日期，取这一天的24点，即次日的零点
            //Comment by 杨艳，2008-12-22

            //出生日期
            if (this.dtpBirthFrom.Enabled)
                qc.dtBirthDayFrom = this.dtpBirthFrom.Value.Date;
            if (this.dtpBirthTo.Enabled)
                qc.dtBirthDayTo = this.dtpBirthTo.Value.Date.AddDays(1);
            if (this.dtpVisitFrom.Enabled)
                qc.dtVisitFrom = this.dtpVisitFrom.Value.Date;
            if (this.dtpVisitTo.Enabled)
                qc.dtVisitTo = this.dtpVisitTo.Value.Date.AddDays(1);
           
                qc.strResult = string.Empty;
            //危险度评估积分
            if (this.txtRiskScoreFrom.Enabled)
                qc.strRiskScoreFrom = this.txtRiskScoreFrom.Text;
            if (this.txtRiskScoreTo.Enabled)
                qc.strRiskScoreTo = this.txtRiskScoreTo.Text;

            dsResult.Clear();

            //将查询结果显示到列表中           
            lvPatHistory.Items.Clear();
            lvPatBasic.Items.Clear();
            TableResult = DBAccess.Query(qc);
            if (TableResult.Rows.Count > 0)
            {
                //add by lch 090616 解密病人姓名和出生日期。
                //实例化加密解密类
                DESClass DESClass = new DESClass();

                string tmp = string.Empty;
                lblCount.Text = TableResult.Rows.Count.ToString();

                PatBasicBind(TableResult);
                //foreach (DataRow dr in TableResult.Rows)
                //{
                //    int recordStatus = DBAccess.GetRecordStatus((int)dr.ItemArray[0]);
                //    string[] cols = new string[11];
                //    //2008-8-27:ccj
                //    //添加显示病人ID项以及数据完整标识项
                //    cols[0] = dr["PatID"].ToString();

                //    //去除不必要的空格，Modified By ZQY 090325
                //    cols[1] = DESClass.DESDecrypt(dr["PatName"].ToString().Trim());//解密姓名
                //    cols[2] = dr["PatSex"].ToString().Trim();
                //    cols[3] = Convert.ToDateTime(DESClass.DESDecrypt(dr["PatBirthday"].ToString())).ToShortDateString();//解密生日 
                //    cols[4] = dr["PatBirthProvince"].ToString().Trim() + dr["PatBirthCity"].ToString().Trim();
                //    cols[5] = ((DateTime)dr["PatVisitDateTime"]).Date.ToShortDateString();
                //    cols[10] = DBAccess.GetNeededData((int)dr.ItemArray[0]);
                //    //cols[9] = dr["RecordSEQ"].ToString();
                //    ListViewItem item1 = new ListViewItem(cols);
                //    item1.Tag = dr["RecordSEQ"].ToString();
                //    //item1.Tag = dr["PatID"].ToString();
                //    if (!string.IsNullOrEmpty(cols[10]) && recordStatus == 0)
                //    {
                //        item1.ForeColor = Color.Red;
                //    }
                //    else
                //    {
                //        item1.ForeColor = Color.FromName("WindowText");
                //    }

                //    lvPatHistory.Items.Add(item1);
                //}

                this.OnItemDataChanged();
            }
        }

        /********************************************************************************
        *作用：两个ListView的数据绑定
        *add by ZX 100415
        *********************************************************************************/
        private void PatBasicBind(DataTable dt)
        {
            lvPatBasic.Items.Clear();
            Dictionary<string, DataRow> patbasic = new Dictionary<string, DataRow>();
            DESClass DESClass = new DESClass();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!patbasic.ContainsKey(dt.Rows[i]["PatSEQ"].ToString()))
                    patbasic.Add(dt.Rows[i]["PatSEQ"].ToString(), dt.Rows[i]);
            }
            lblCount.Text = patbasic.Count.ToString();
            foreach (KeyValuePair<string, DataRow> dr in patbasic)
            {
                string[] cols = new string[3];
                cols[0] = dr.Value["PatID"].ToString();
                cols[1] = DESClass.DESDecrypt(dr.Value["PatName"].ToString().Trim());//解密姓名
                cols[2] = dr.Value["PatSex"].ToString().Trim();
                string str = dr.Value["PatVisitDateTime"].GetType().ToString();
                ListViewItem item1 = new ListViewItem(cols);
                item1.Tag = dr.Value;
                lvPatBasic.Items.Add(item1);
            }
            if (lvPatBasic.Items.Count > 0)
            {
                if (oldPatSelect >= lvPatBasic.Items.Count)
                    oldPatSelect = 0;
                lvPatBasic.Items[oldPatSelect].Selected = true;
                DataRow dr = lvPatBasic.SelectedItems[0].Tag as DataRow;
                PatHistoryBind(dt, dr);
            }
        }

        /// <summary>
        /// 就诊病例史数据绑定
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="row"></param>
        private void PatHistoryBind(DataTable dt, DataRow row)
        {
            DESClass DESClass = new DESClass();
            lvPatHistory.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (dr["PatSEQ"].ToString() == row["PatSEQ"].ToString())
                {
                    int recordStatus = DBAccess.GetRecordStatus(Convert.ToInt32(dr.ItemArray[0]));
                    string[] cols = new string[6];

                    cols[0] = ((DateTime)dr["PatVisitDateTime"]).Date.ToShortTimeString();
                    cols[1] = DBAccess.GetNeededResult(Convert.ToInt32(dr.ItemArray[0]), "代谢综合征");
                    cols[2] = DBAccess.GetNeededResult(Convert.ToInt32(dr.ItemArray[0]), "危险度");
                    cols[3] = DBAccess.GetNeededResult(Convert.ToInt32(dr.ItemArray[0]), "危险度积分");
                    cols[5] = DBAccess.GetNeededData(Convert.ToInt32(dr.ItemArray[0]));

                    ListViewItem item1 = new ListViewItem(cols);
                    item1.Tag = dr["RecordSEQ"].ToString();
                    if (!string.IsNullOrEmpty(cols[5]) && recordStatus == 0)
                    {
                        item1.ForeColor = Color.Red;
                    }
                    else
                    {
                        item1.ForeColor = Color.FromName("WindowText");
                    }
                    lvPatHistory.Items.Add(item1);
                }
            }

            this.patientDataResult = DBAccess.GetPatientData(dt.Rows[this.lvPatBasic.SelectedIndices[0]]["PatSEQ"].ToString());
        }

        private void lvPatBasic_Click(object sender, EventArgs e)
        {
            if (lvPatBasic.Items.Count > 0)
            {
                DataRow dr = lvPatBasic.SelectedItems[0].Tag as DataRow;
                PatHistoryBind(TableResult, dr);
                oldPatSelect = lvPatBasic.SelectedItems[0].Index;

                this.ChangePatientData();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtName.Text = "";
            this.rdoSexAll.Checked = true;
            this.rdoVisitAll.Checked = true;
            this.rdoBirthAll.Checked = true;
            this.dtpBirthFrom.Value = DateTime.Now;
            this.dtpBirthTo.Value = DateTime.Now;
            this.dtpVisitFrom.Value = DateTime.Now;
            this.dtpVisitTo.Value = DateTime.Now;
            this.rdoRiskScoreAll.Checked = true;
            this.txtRiskScoreFrom.Text = string.Empty;
            this.txtRiskScoreTo.Text = string.Empty;
            if (TableResult != null)
                TableResult.Clear();
            lvPatBasic.Items.Clear();
            dsResult.Clear();
            lvPatHistory.Items.Clear();
            lblCount.Text = "0";

            //设置默认焦点，Added By ZQY,090325
            txtName.Focus();

            this.OnItemDataChanged();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            bool Haselected = false;
            //int iRowCurr = lvResult.SelectedItems[0].Index;
            //if(iRowCurr==-1)

            foreach (ListViewItem oitem in lvPatHistory.Items)
            {
                if (oitem.Selected)
                {
                    Haselected = true;
                    //PatInfo.RecordSEQ =oitem.Tag.ToString();
                    GlobalData.RecordInfo.RecordSeq = int.Parse(oitem.Tag.ToString());
                    break;
                }
            }
            if (Haselected)
            {
                ShowInfoEnterPage();
            }
            this.Cursor = Cursors.Default;

            /********************************************************************************
            *作用：用户点击查询界面的【浏览】按钮，记录该操作日志
            *add by lch 090615
            *********************************************************************************/
            CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
            CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
            CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
            CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
            CDSSOperationLog.OperationName = "查询浏览";
            CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
            GlobalData.OperationLog.Add(CDSSOperationLog);

            DBAccess.SaveAllOperationLog(CDSSOperationLog);

        }

        /// <summary>
        /// 该函数用于引发事件
        /// </summary>
        private void ShowInfoEnterPage()
        {
            CustomEventHandle temp = ShowInfoEnterPageEvent;
            if (temp != null)
                temp();
        }

        /******************************************************
        * Revise History：
        * 2008-12-23 杨艳 修改实现方法，以利于代码维护
        ******************************************************/
        private void lvResult_DoubleClick(object sender, EventArgs e)
        {
            this.btnBrowse.PerformClick();
        }

        private void rdoRiskScoreAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoRiskScoreAll.Checked)
            {
                this.txtRiskScoreFrom.Enabled = false;
                this.txtRiskScoreTo.Enabled = false;
            }
        }

        private void rdoRiskScoreRange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoRiskScoreRange.Checked)
            {
                this.txtRiskScoreFrom.Enabled = true;
                this.txtRiskScoreTo.Enabled = true;
            }
        }

        private void rdoRiskScorePrecision_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoRiskScorePrecision.Checked)
            {
                this.txtRiskScoreFrom.Enabled = true;
                this.txtRiskScoreTo.Enabled = false;
            }
        }

        private void toolStripMenuItem_Invalid_Click(object sender, EventArgs e)
        {
            if (this.lvPatHistory.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("作废病人记录将会使该条记录永久不可见。\r\n确认作废吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    DBAccess.SetRecordStatus(int.Parse(this.lvPatHistory.SelectedItems[0].Tag.ToString()), 2);
                    btnQuery.PerformClick();

                    CDSSOperationLog CDSSOperationLog = new CDSSOperationLog();
                    CDSSOperationLog.OperationUserID = GlobalData.UserInfo.UserID;
                    CDSSOperationLog.OperationTime = DateTime.Parse(DBAccess.GetServerCurrentTime());
                    CDSSOperationLog.OperationStep = GlobalData.OperationLog.Count + 1;
                    CDSSOperationLog.OperationName = "作废病人记录";
                    CDSSOperationLog.OperationDescription = "RecordSEQ为:" + GlobalData.RecordInfo.RecordSeq.ToString();
                    GlobalData.OperationLog.Add(CDSSOperationLog);

                    DBAccess.SaveAllOperationLog(CDSSOperationLog);
                }
            }
        }

        private void toolStripMenuItem_ShowState_Click(object sender, EventArgs e)
        {
            if (this.toolStripMenuItem_ShowState.Checked == true)
            {
                if (this.lvPatHistory.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem selectedList in this.lvPatHistory.SelectedItems)
                    {
                        DBAccess.SetRecordStatus(int.Parse(selectedList.Tag.ToString()), 1);
                    }
                }
            }
            else
            {
                if (this.lvPatHistory.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem selectedList in this.lvPatHistory.SelectedItems)
                    {
                        DBAccess.SetRecordStatus(int.Parse(selectedList.Tag.ToString()), 0);
                    }
                }
            }
            PatHistoryBind(TableResult, lvPatBasic.SelectedItems[0].Tag as DataRow);
        }

        private DataRow GetDRByRecordSEQ(string recordSEQ)
        {
            DataRow row = null;
            foreach (DataRow dr in TableResult.Rows)
            {
                if (dr["RecordSEQ"].ToString() == recordSEQ)
                {
                    row = dr;
                    return row;
                }
                else
                    row = null;
            }
            return row;
        }

        private void contextMenuStrip_Result_Opening(object sender, CancelEventArgs e)
        {
            if (this.lvPatHistory.SelectedItems.Count == 1)
            {
                this.toolStripMenuItem_Invalid.Enabled = true;
            }
            else
            {
                this.toolStripMenuItem_Invalid.Enabled = false;
            }

            if (this.lvPatHistory.SelectedItems.Count > 0)
            {
                this.toolStripMenuItem_ShowState.Enabled = true;
                this.toolStripMenuItem_ShowState.Checked = true;
                foreach (ListViewItem selectedList in this.lvPatHistory.SelectedItems)
                {
                    if (DBAccess.GetRecordStatus(int.Parse(selectedList.Tag.ToString())) == 1)
                    {
                        this.toolStripMenuItem_ShowState.Checked = false;
                        break;
                    }
                }
            }
        }

        public void PatBasicClear()
        {
            lvPatBasic.Items.Clear();
            this.OnItemDataChanged();
        }

        private void DrawItemLine(string colEnName, string colCnName, Color itemColor, DataTable dt)
        {
            if (this.medicalGraph.GraphPane.CurveList[colCnName] == null)
            {
                ZedGraph.PointPairList dataList = new ZedGraph.PointPairList();
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(row[colEnName].ToString()))
                    {
                        dataList.Add(Convert.ToDateTime(row["PatVisitDateTime"].ToString()).ToOADate(), double.Parse(row[colEnName].ToString()));
                    }
                }

                ZedGraph.LineItem lineItem = this.medicalGraph.GraphPane.AddCurve(colCnName, dataList, itemColor);

                if (colCnName == "收缩压" || colCnName == "舒张压" || colCnName == "BMI")
                {
                    lineItem.IsY2Axis = true;
                }

                this.medicalGraph.AxisChange();
                this.medicalGraph.Refresh();
            }
        }

        private void DeleteItemLine(string colCnName)
        {
            this.medicalGraph.GraphPane.CurveList.Remove(this.medicalGraph.GraphPane.CurveList[colCnName]);
            this.medicalGraph.AxisChange();
            this.medicalGraph.Refresh();
        }

        private void checkBoxPatientData_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checker = sender as CheckBox;
            string colEnName = checker.Name.Substring(8);
            string colCnName = checker.Text;

            if (checker.Checked)
            {
                Color itemColor = this.chartColorDict[checker.Name];
                this.DrawItemLine(colEnName, colCnName, itemColor, this.patientDataResult);
            }
            else
            {
                this.DeleteItemLine(colCnName);
            }
            
        }

        private void ChangePatientData()
        {
            CheckBox[] checkBoxGroup = new CheckBox[] {
                this.checkBoxBMI,
                this.checkBoxDBP1,
                this.checkBoxFBG,
                this.checkBoxHDLC,
                this.checkBoxLDLC,
                this.checkBoxSBP1,
                this.checkBoxTC,
                this.checkBoxTG,
                this.checkBoxTwoHPBG};

            foreach (CheckBox checker in checkBoxGroup)
            {
                this.DeleteItemLine(checker.Text);
                this.checkBoxPatientData_CheckedChanged(checker, new EventArgs());
            }
        }

        private void OnItemDataChanged()
        {
            if (this.lvPatBasic.Items.Count == 0)
            {
                this.checkBoxBMI.Enabled =
                    this.checkBoxDBP1.Enabled =
                    this.checkBoxFBG.Enabled =
                    this.checkBoxHDLC.Enabled =
                    this.checkBoxLDLC.Enabled =
                    this.checkBoxSBP1.Enabled =
                    this.checkBoxTC.Enabled =
                    this.checkBoxTG.Enabled =
                    this.checkBoxTwoHPBG.Enabled =
                    this.medicalGraph.Enabled = false;

                this.medicalGraph.GraphPane.CurveList.Clear();
                this.medicalGraph.AxisChange();
                this.medicalGraph.Refresh();
            }
            else
            {
                this.checkBoxBMI.Enabled =
                    this.checkBoxDBP1.Enabled =
                    this.checkBoxFBG.Enabled =
                    this.checkBoxHDLC.Enabled =
                    this.checkBoxLDLC.Enabled =
                    this.checkBoxSBP1.Enabled =
                    this.checkBoxTC.Enabled =
                    this.checkBoxTG.Enabled =
                    this.checkBoxTwoHPBG.Enabled =
                    this.medicalGraph.Enabled = true;
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void lvPatBasic_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void lvPatHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}