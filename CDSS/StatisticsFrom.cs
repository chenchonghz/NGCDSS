using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Collections;
using CDSSCtrlLib;
using CDSSDBAccess;
using CDSSSystemData;
using System.Resources;
using System.Reflection;

namespace CDSS
{
    public enum ValuePropertyType
    {
        Average,
        StandardDeviation,
        Max,
        Min,
        Median,
        Quar
    }
    public enum DignosedPropertyType
    {
        NewMorbid,
        NewMorbidRate,
        Morbid,
        MorbidRate,
        Qulified,
        QulifiedRate
    }  
    public partial class  StatisticsForm : InfoFormBaseClass
    {
        private List<Period> TotalPeriods = new List<Period>();

        private string[] valueFiedls;
        private string[] dignosedFields;

        private List<ValuePropertyType> ValueProperties = new List<ValuePropertyType>();
        private List<DignosedPropertyType> DignosedProperties = new List<DignosedPropertyType>();

        private string IntervalType
        {
            get { return comboBoxInterval.Text; }
        }

        private DataTable resultOfValueTable = new DataTable();
        private DataTable resultOfDignosedTable = new DataTable();

        private static System.Resources.ResourceManager resourceManager = new ResourceManager("CDSS.Resource.StatisticsForm", Assembly.GetExecutingAssembly());
        private static string everyyear = resourceManager.GetString("EveryYear");
        private static string everymonth = resourceManager.GetString("EveryMonth");
        private static string everyseason = resourceManager.GetString("EverySeason");
        private static string defineown = resourceManager.GetString("DefinedOwn");  

        private string TempFilePath = Application.StartupPath + "\\UserInfo\\" + GlobalData.UserInfo.UserID+"\\Statistics\\";

        public StatisticsForm()
        {
            InitializeComponent();
        }
        private void GetTimePoints()
        {
            TotalPeriods.Clear();
                            
            DateTime Period1Start, Period1End,Period2Start,Period2End;
            Period1Start = dateTimePicker1.Value;
            Period1End = dateTimePicker2.Value;
            Period2Start = dateTimePicker3.Value;
            Period2End = dateTimePicker4.Value;                      

            if (IntervalType == everyyear)
            {
                int y1 = Period1End.Year - Period1Start.Year;
                int y2 = Period2End.Year - Period2Start.Year;
                
                for (int i = 0; i <  y1; i++)
                {
                    Period singlePeriod;
                    singlePeriod.StartTime = Period1Start.AddYears(i);
                    singlePeriod.EndTime = Period1Start.AddYears(i+1);
                    TotalPeriods.Add(singlePeriod);
                }                
                for (int i = y1; i < y1+y2; i++)
                {
                    Period singlePeriod;
                    singlePeriod.StartTime = Period2Start.AddYears(i-y1);
                    singlePeriod.EndTime = Period2Start.AddYears(i -y1+ 1);
                    TotalPeriods.Add(singlePeriod);                    
                }
            }
            if(IntervalType==everyseason)
            {
                System.TimeSpan ts1 = Period1End - Period1Start;
                System.TimeSpan ts2 = Period2End - Period2Start;
                int n1 = ts1.Days / 90;
                int n2 = ts2.Days / 90;
                for (int i = 0; i < n1; i++)
                {
                    Period singlePeriod;
                    singlePeriod.StartTime = Period1Start.AddMonths(3*i);
                    singlePeriod.EndTime = Period1Start.AddMonths(3*(i + 1));
                    TotalPeriods.Add(singlePeriod);
                }                
                for (int i = n1; i < n1 + n2; i++)
                {
                    Period singlePeriod;
                    singlePeriod.StartTime = Period2Start.AddMonths(3*(i - n1));
                    singlePeriod.EndTime = Period2Start.AddMonths(3 * (i - n1 + 1));
                    TotalPeriods.Add(singlePeriod);
                }
            }
            if(IntervalType==everymonth)
            {
                System.TimeSpan ts1 = Period1End - Period1Start;
                System.TimeSpan ts2 = Period2End - Period2Start;
                 int n1 = ts1.Days / 30;
                 int n2 = ts2.Days / 30;
                 for (int i = 0; i < n1; i++)
                 {
                     Period singlePeriod;
                     singlePeriod.StartTime = Period1Start.AddMonths(i);
                     singlePeriod.EndTime = Period1Start.AddMonths(i + 1);
                     TotalPeriods.Add(singlePeriod);
                 }                 
                 for (int i = n1; i < n1 + n2; i++)
                 {
                     Period singlePeriod;
                     singlePeriod.StartTime = Period2Start.AddMonths(i - n1);
                     singlePeriod.EndTime = Period2Start.AddMonths(i - n1 + 1);
                     TotalPeriods.Add(singlePeriod);
                 } 
            }                       
            if(IntervalType==defineown)
            {
                //自定义即为将输入的点分别作为起始时间点和终止时间点
                Period singlePeriod1,singlePeriod2;
                singlePeriod1.StartTime = Period1Start;
                singlePeriod1.EndTime = Period1End;
                singlePeriod2.StartTime = Period2Start;
                singlePeriod2.EndTime = Period2End;
                TotalPeriods.Add(singlePeriod1);
                TotalPeriods.Add(singlePeriod2);
            }
        }

        private string[] GetValueFields()
        {
            string[] ValueFields = new string[checkedListBoxValue.CheckedItems.Count];
            string[] TotalFields ={ "BG", "FBG", "TwoHPBG", "FoodCount", "OGTTFBG", "OGTTPBG",
                "TC", "TG", "HDLC", "LDLC", "CR", "AlanineAminotransferase", "UN", 
                "AspartateAminotransferase", "ALBCR","US","UrinaryProtein","NTT","UPH","UUA",
                "HBA1C","BCL","BUA","BKA","BNA","BCO2CP","BGA","BP","SerumTotalProtein","SerumAlbumin",
            "FastingInsulin","FastingCPeptide","PostprandialInsulin","PostprandialCPeptide","ICA","GDA65",
                "Height","Weigh","WC","HC","HR","SBP1","DBP1","SBP2","DBP2"};
            int i = 0;
            //在checklistbox中复选了的参数，它们的Index保存在checkedindices
            //中，将它们逐个取出
            foreach (int indexChecked in checkedListBoxValue.CheckedIndices)
            {
                ValueFields[i]=TotalFields[indexChecked];
                i++;
            }
            return ValueFields;
        }
        private string[] GetDignosedFields()
        {
            string[] DignosedFields = new string[checkedListBoxDigno.CheckedItems.Count];
            for (int i = 0; i < checkedListBoxDigno.CheckedItems.Count;i++ )
            {
                DignosedFields[i] = checkedListBoxDigno.CheckedItems[i].ToString();
            }
            return DignosedFields;
        }
        private List<ValuePropertyType> GetValueProperties()
        {
            List<ValuePropertyType> ValueProperties = new List<ValuePropertyType>();
                        
            if (checkBoxAverage.Checked == true)
            {
                ValueProperties.Add(ValuePropertyType.Average);
            }
            if (checkBoxStandard.Checked == true)
            {
                ValueProperties.Add(ValuePropertyType.StandardDeviation);
            }
            if (checkBoxMax.Checked == true)
            {
                ValueProperties.Add(ValuePropertyType.Max);
            }
            if (checkBoxMin.Checked == true)
            {
                ValueProperties.Add(ValuePropertyType.Min);
            }
            if (checkBoxMedian.Checked == true)
            {
                ValueProperties.Add(ValuePropertyType.Median);
            }
            if (checkBoxQuar.Checked == true)
            {
                ValueProperties.Add(ValuePropertyType.Quar);
            }
            return ValueProperties;
        }
        private List<DignosedPropertyType> GetDignosedProperties()
        {
            List<DignosedPropertyType> DignosedProperties = new List<DignosedPropertyType>();
            
            if (checkBoxNewMorbi.Checked == true)
            {
                DignosedProperties.Add(DignosedPropertyType.NewMorbid);
            }
            if (checkBoxNewMorbiRate.Checked == true)
            {
                DignosedProperties.Add(DignosedPropertyType.NewMorbidRate);
            }
            if (checkBoxMorbi.Checked == true)
            {
                DignosedProperties.Add(DignosedPropertyType.Morbid);
            }
            if (checkBoxMorbiRate.Checked == true)
            {
                DignosedProperties.Add(DignosedPropertyType.MorbidRate);
            }
            if (checkBox12.Checked == true)
            {
                DignosedProperties.Add(DignosedPropertyType.Qulified);
            }
            if (checkBox9.Checked == true)
            {
                DignosedProperties.Add(DignosedPropertyType.QulifiedRate);
            }
            return DignosedProperties;
        }

#region 查询条件控件的方法

        /// <summary>
        /// 创建相应查询条件控件
        /// </summary>
        private void CreateQueryBuilder(FlowLayoutPanel  flowLayoutPanel)
        {
            QueryBuilder qbCtl = new QueryBuilder();
            int count = flowLayoutPanel.Controls.Count;
            count++;
            qbCtl.Name = "queryBuilder" + count;
            qbCtl.Delete += new QueryBuilder.DeleteHandler(qbCtl_Delete);
            qbCtl.AutoValidate = AutoValidate.EnableAllowFocusChange;
            qbCtl.Validating += new CancelEventHandler(qbCtl_Validating);
            flowLayoutPanel.Controls.Add(qbCtl);
            flowLayoutPanel.ScrollControlIntoView(qbCtl);
            LogicControl(flowLayoutPanel);
        }

        private void qbCtl_Delete(object sender, EventArgs e)
        {
            QueryBuilder qb = (QueryBuilder)sender;
            FlowLayoutPanel flowPanel = (FlowLayoutPanel)qb.Parent;
            flowPanel.Controls.Remove(qb);
            for (int i = 1; i <= flowPanel.Controls.Count; i++)
            {
                QueryBuilder ctl = (QueryBuilder)flowPanel.Controls[i - 1];
                ctl.Name = "queryBuilder" + i;
            }
            LogicControl(flowPanel);
        }

        private void qbCtl_Validating(object sender, CancelEventArgs e)
        {
            CheckQuery(sender as QueryBuilder);
        }
        private bool CheckQuery(QueryBuilder qb)
        {
            errorProvider2.Clear();

            if (qb.cmbName.SelectNode == null || qb.cmbName.Text == string.Empty)
                errorProvider2.SetError(qb.cmbName, "请选择相应的条件");
            if (qb.cmbJdge.Text == string.Empty)
                errorProvider2.SetError(qb.cmbJdge, "请选择比较符号");
            if (qb.txtValue.Text == string.Empty)
                errorProvider2.SetError(qb.txtValue, "请按照规则填写查询值");
            if (qb.cmbLgc.Text == string.Empty)
                errorProvider2.SetError(qb.cmbLgc, "请选择逻辑符号");

            return false;
        }
        /// <summary>
        /// 控制最后一个控件逻辑关系只有“无”，其他的只有“与或”；
        /// </summary>
        private void LogicControl(FlowLayoutPanel flowLayoutPanel)
        {
            foreach (QueryBuilder var in flowLayoutPanel.Controls)
            {
                if (var.Name == "queryBuilder" + flowLayoutPanel.Controls.Count)
                {
                    string old = var.cmbLgc.Text;
                    var.cmbLgc.Items.Clear();
                    var.cmbLgc.Items.AddRange(new object[] { "无" });
                    var.cmbLgc.SelectedIndex = 0;
                    var.cmbLgc.Text = old;
                }
                else
                {
                    string old = var.cmbLgc.Text;
                    var.cmbLgc.Items.Clear();
                    var.cmbLgc.Items.AddRange(new object[] { "且", "或"});
                    var.cmbLgc.Text = old;
                }
            }
        }
        private bool CheckQuery(FlowLayoutPanel[] flowPanels)
        {
            errorProvider2.Clear();
            bool qtag = true;            
            foreach (FlowLayoutPanel flowPanel in flowPanels)
            {
                foreach (Control var in flowPanel.Controls)
                {
                    QueryBuilder qb = var as QueryBuilder;

                    if (qb.cmbName.SelectNode == null || qb.cmbName.Text == string.Empty)
                    {
                        errorProvider2.SetError(qb.cmbName, "请选择相应的条件");
                        qtag = false;
                    }
                    if (qb.cmbJdge.Text == string.Empty)
                    {
                        errorProvider2.SetError(qb.cmbJdge, "请选择比较符号");
                        qtag = false;
                    }
                    if (qb.txtValue.Text == string.Empty)
                    {
                        errorProvider2.SetError(qb.txtValue, "请按照规则填写查询值");
                        qtag = false;
                    }
                    if (qb.cmbLgc.Text == string.Empty)
                    {
                        errorProvider2.SetError(qb.cmbLgc, "请选择逻辑符号");
                        qtag = false;
                    }
                }
            }            
            return qtag;
        }
        private void btnAdd1_Click(object sender, EventArgs e)
        {
            CreateQueryBuilder(flowLayoutPanel1);
        }
        private void btnAdd2_Click(object sender, EventArgs e)
        {
            CreateQueryBuilder(flowLayoutPanel2);
        }
        
        
#endregion

        private void ConditionGrouping_Load(object sender, EventArgs e)
        {
            CreateQueryBuilder(flowLayoutPanel1);
            CreateQueryBuilder(flowLayoutPanel2);
        }

        
        private void buttonGrouping_Click(object sender, EventArgs e)
        {
            resultOfValueTable.Clear();
            resultOfDignosedTable.Clear();
                                    
            if (IntervalType=="")
            {
                MessageBox.Show("请选择时间的分组类型！自定义为不分组！");                
                return;
            }
            if (checkedListBoxValue.CheckedIndices.Count == 0 && checkedListBoxDigno.CheckedIndices.Count == 0)
            {
                MessageBox.Show("请选择要统计的参数！");                
                return;
            }
            if (!CheckProperties())
            {
                MessageBox.Show("请选择要统计的参数的属性！");
                return;
            }
            FlowLayoutPanel[] flowLayoutPanels = new FlowLayoutPanel[2] { flowLayoutPanel1, flowLayoutPanel2 };
            if (!CheckQuery(flowLayoutPanels))
            {
                return;
            }
            string[] childConditionQuery = new string[2];
            childConditionQuery[0] = DBAccess.InitSQLStr(flowLayoutPanel1);
            childConditionQuery[1] = DBAccess.InitSQLStr(flowLayoutPanel2);

            GetTimePoints();
            valueFiedls = GetValueFields();
            dignosedFields = GetDignosedFields();
            ValueProperties = GetValueProperties();
            DignosedProperties = GetDignosedProperties();
            StatisticMethods StatisticMethodsObject = new StatisticMethods();

            if (valueFiedls.Length!=0)
            {
                resultOfValueTable = StatisticMethodsObject.GetValueParaStatistics(TotalPeriods, childConditionQuery,valueFiedls,ValueProperties,IntervalType);
                dataGridViewValue.DataSource = resultOfValueTable;
            }
            if (dignosedFields.Length!=0)
            {
                resultOfDignosedTable = StatisticMethodsObject.GetDignosedParaStatistics(TotalPeriods, childConditionQuery,dignosedFields,DignosedProperties,IntervalType);
                dataGridViewDigno.DataSource = resultOfDignosedTable;
            }          
            
        }
        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (resultOfDignosedTable.Rows.Count!=0||resultOfValueTable.Rows.Count!=0)
            {
                string StatisticsCondition = "";
                string ContrastCondition = "";
                StatisticsCondition = GetConditonString(flowLayoutPanel1);
                ContrastCondition = GetConditonString(flowLayoutPanel2);
                CRViewForm CRViewResult = new CRViewForm(resultOfDignosedTable,resultOfValueTable,StatisticsCondition,ContrastCondition);
                CRViewResult.Show();
            }
            else
                MessageBox.Show("没有统计结果，无法生成报表！");            
        }
        public string GetConditonString(FlowLayoutPanel panel)
        {
            string conditonString = "";
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                QueryBuilder qb = (QueryBuilder)panel.Controls[i];
                QueryBuilder.SQLStr msg = qb.ShowSQLStr();
                conditonString += msg.ConditionName + msg.Sign + msg.CheckValue + msg.LogicSign;
            }
            return conditonString;
        }
        private void buttonClean_Click(object sender, EventArgs e)
        {
            Clean();           
        }
        private void Clean()
        {
            for (int j = 0; j < checkedListBoxValue.Items.Count; j++)
                checkedListBoxValue.SetItemChecked(j, false);

            for (int j = 0; j < checkedListBoxDigno.Items.Count; j++)
                checkedListBoxDigno.SetItemChecked(j, false);

            checkBoxAverage.Checked = false;
            checkBoxStandard.Checked = false;
            checkBoxMax.Checked = false;
            checkBoxMin.Checked = false;
            checkBoxMedian.Checked = false;
            checkBoxQuar.Checked = false;

            checkBoxNewMorbiRate.Checked = false;
            checkBoxMorbiRate.Checked = false;
            checkBoxNewMorbi.Checked = false;
            checkBoxMorbi.Checked = false;

            comboBoxInterval.SelectedIndex = -1;
            
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;

            dataGridViewValue.DataSource = null;
            dataGridViewDigno.DataSource = null;
            resultOfDignosedTable.Clear();
            resultOfValueTable.Clear();
            int countOfPanel1=flowLayoutPanel1.Controls.Count;
            int countOfPanel2=flowLayoutPanel2.Controls.Count;
            for (int i = 0; i < countOfPanel1; i++)
            {
                flowLayoutPanel1.Controls.Remove(flowLayoutPanel1.Controls[0]);
            }
            for (int i = 0; i < countOfPanel2; i++)
            {
                flowLayoutPanel2.Controls.Remove(flowLayoutPanel2.Controls[0]);
            }                       
        }
        private void buttonExport_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (resultOfValueTable.Rows.Count!=0)
            {
                resultOfValueTable.TableName = "数值参数统计结果";
                ds.Tables.Add(resultOfValueTable.Copy());
            }
            if (resultOfDignosedTable.Rows.Count!=0)
            {
                resultOfDignosedTable.TableName="诊断参数统计结果";
                ds.Tables.Add(resultOfDignosedTable.Copy());
            }
            foreach(DataTable dt in ds.Tables)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = dt.TableName + "另存为";
                dlg.RestoreDirectory = true;
                dlg.Filter = "CSV文件(*.csv)|*.csv|文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextWriter writer = null;
                    try
                    {
                        string text = "";
                        writer = new StreamWriter(dlg.FileName, false, Encoding.Default);
                        writer.WriteLine(dt.TableName);
                        int count = dt.Columns.Count;
                        string[] fields = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            DataColumn column = dt.Columns[i];
                            fields[i] = column.ColumnName;
                        }
                        text = string.Join(",", fields);
                        writer.WriteLine(text);
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                if (row[i] == null)
                                {
                                    fields[i] = "";
                                    continue;
                                }
                                if (row[i] == DBNull.Value)
                                {
                                    fields[i] = "";
                                    continue;
                                }
                                string field = row[i].ToString();
                                field = field.Replace("\"", "\"\"");
                                if (field.IndexOf(',') >= 0)
                                {
                                    fields[i] = string.Format("\"{0}\"", field);
                                    continue;
                                }
                                if (field.IndexOf('\r') >= 0)
                                {
                                    fields[i] = string.Format("\"{0}\"", field);
                                    continue;
                                }
                                if (field.IndexOf('\n') >= 0)
                                {
                                    fields[i] = string.Format("\"{0}\"", field);
                                    continue;
                                }
                                if (field.IndexOf('\"') >= 0)
                                {
                                    fields[i] = string.Format("\"{0}\"", field);
                                    continue;
                                }
                                if (field != field.Trim())
                                {
                                    fields[i] = string.Format("\"{0}\"", field);
                                    continue;
                                }
                                fields[i] = row[i].ToString();
                            }
                            text = string.Join(",", fields);
                            writer.WriteLine(text);
                        }
                        MessageBox.Show("导出成功", "提示");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败", "提示");
                        return;
                    }
                    finally
                    {
                        if (writer != null)
                        {
                            writer.Close();
                        }
                    }
                }
        }
    }

        private void comboBoxInterval_SelectedIndexChanged(object sender, EventArgs e)
        {

            //IntervalType = comboBoxInterval.Text;
            DateTime Period1Start, Period1End, Period2Start, Period2End;
            Period1Start = dateTimePicker1.Value;
            Period1End = dateTimePicker2.Value;
            Period2Start = dateTimePicker3.Value;
            Period2End = dateTimePicker4.Value;

            if (IntervalType==everyyear)
            {                    
                if ((Period1End.Year - Period1Start.Year) <= 0 || (Period2End.Year - Period2Start.Year) <= 0)
                {
                    MessageBox.Show("无法在一年内统计该年的数据！");
                    comboBoxInterval.SelectedIndex = -1;
                    return;
                }
                if ((Period1Start.Month != Period1End.Month) || (Period2Start.Month != Period2End.Month) || (Period1Start.Day != Period1End.Day) || (Period2Start.Day != Period2End.Day))
                {
                    MessageBox.Show("月份或日期与隔年的月份或日期不匹配，无法按年统计！");
                    comboBoxInterval.SelectedIndex = -1;
                    return;
                }
            }
                   
            if(IntervalType==everyseason)
            {
                if (!((Period1Start < Period1End) && (Period2Start < Period2End)))
                {
                    MessageBox.Show("您选择的不是时间段！");
                    comboBoxInterval.SelectedIndex = -1;
                    return;
                }
                System.TimeSpan ts1 = Period1End - Period1Start;
                System.TimeSpan ts2 = Period2End - Period2Start;
                if ((ts1.Days < 90) || (ts2.Days < 90))
                {
                    MessageBox.Show("您选择的时间段低于一个季度，无法按季度统计！如果您选择的时间段不是一个季度，将自动统计从开始起的每三个月的记录。");
                    comboBoxInterval.SelectedIndex = -1;
                    return;
                }
            }
            if(IntervalType==everymonth)        
            {   
                if (!((Period1Start < Period1End) && (Period2Start < Period2End)))
                {
                    MessageBox.Show("您选择的不是时间段！");
                    comboBoxInterval.SelectedIndex = -1;
                    return;
                }
                System.TimeSpan ts1 = Period1End - Period1Start;
                System.TimeSpan ts2 = Period2End - Period2Start;
                if ((ts1.Days < 30) || (ts2.Days < 30))
                {
                    MessageBox.Show("您选择的时间段低于一个月，无法按月统计！如果您选择的时间段不是一个月，将自动统计从开始起的每个月的记录。");
                    comboBoxInterval.SelectedIndex = -1;
                    return;
                }
            }
            if(IntervalType==defineown)   
            {    
                if (!((Period1Start < Period1End) && (Period2Start < Period2End)))
                {
                    MessageBox.Show("您选择的不是时间段！");
                    comboBoxInterval.SelectedIndex = -1;
                    return;
                }                    
            }
            return;
        }

        private void buttonTempManage_Click(object sender, EventArgs e)
        {
            TemplateManage manageFrm = new TemplateManage(TempFilePath);
            manageFrm.ShowDialog();
            DataSet ds = manageFrm.TemplateDataSet;
            if (ds.Tables.Count!=0)
            {
                LoadTemplate(ds);
            }
        }
        private bool LoadTemplate(DataSet DataSource)
        {
            Clean();
            if (DataSource.Tables.Contains("StatisticGroupCondition"))
            {
                for (int i = 0; i < DataSource.Tables["StatisticGroupCondition"].Rows.Count; i++)
                {
                    DataRow dr = DataSource.Tables["StatisticGroupCondition"].Rows[i];
                    QueryBuilder.SQLStr msg = new QueryBuilder.SQLStr();
                    msg.ConditionName = dr["colCnName"].ToString();
                    msg.Condition = dr["colName"].ToString();
                    msg.Sign = dr["sign"].ToString();
                    msg.TableName = dr["tableName"].ToString();
                    msg.LogicSign = dr["logicSign"].ToString();
                    msg.CheckValue = dr["vaule"].ToString();
                    msg.ConditionType = dr["colType"].ToString();

                    QueryBuilder qbCtl = new QueryBuilder();
                    qbCtl.Delete += new QueryBuilder.DeleteHandler(qbCtl_Delete);
                    qbCtl.Name = "queryBuilder" + (i + 1);
                    qbCtl.FillQueryBuilder(msg);
                    flowLayoutPanel1.Controls.Add(qbCtl);
                }
                LogicControl(flowLayoutPanel1);
            }
            if (DataSource.Tables.Contains("ContrastGroupCondition"))
            {
                for (int i = 0; i < DataSource.Tables["ContrastGroupCondition"].Rows.Count; i++)
                {
                    DataRow dr = DataSource.Tables["ContrastGroupCondition"].Rows[i];
                    QueryBuilder.SQLStr msg = new QueryBuilder.SQLStr();
                    msg.ConditionName = dr["colCnName"].ToString();
                    msg.Condition = dr["colName"].ToString();
                    msg.Sign = dr["sign"].ToString();
                    msg.TableName = dr["tableName"].ToString();
                    msg.LogicSign = dr["logicSign"].ToString();
                    msg.CheckValue = dr["vaule"].ToString();
                    msg.ConditionType = dr["colType"].ToString();

                    QueryBuilder qbCtl = new QueryBuilder();
                    qbCtl.Delete += new QueryBuilder.DeleteHandler(qbCtl_Delete);
                    qbCtl.Name = "queryBuilder" + (i + 1);
                    qbCtl.FillQueryBuilder(msg);
                    flowLayoutPanel2.Controls.Add(qbCtl);
                }
                LogicControl(flowLayoutPanel2);
            }
            if(DataSource.Tables.Contains("tempInfo"))
            {
                string time = DataSource.Tables["tempInfo"].Rows[0]["timepoints"].ToString();
                string[] times = time.Split(',');
                dateTimePicker1.Value = DateTime.Parse(times[0]);
                dateTimePicker2.Value = DateTime.Parse(times[1]);
                dateTimePicker3.Value = DateTime.Parse(times[2]);
                dateTimePicker4.Value = DateTime.Parse(times[3]);

                int interval = Convert.ToInt16(DataSource.Tables["tempInfo"].Rows[0]["intervaltype"]);
                comboBoxInterval.SelectedIndex = interval;
                string valueSelected = DataSource.Tables["tempInfo"].Rows[0]["valueSelected"].ToString();
                for (int i = 0; i < valueSelected.Split(',').Length; i++)
                {
                    if (valueSelected.Split(',')[i] != "")
                    {
                        int k = Convert.ToInt16(valueSelected.Split(',')[i]);
                        checkedListBoxValue.SetItemChecked(k, true);
                    }
                }
                string dignoSelected = DataSource.Tables["tempInfo"].Rows[0]["dignosedSelected"].ToString();
                for (int i = 0; i < dignoSelected.Split(',').Length; i++)
                {
                    if (dignoSelected.Split(',')[i] != "")
                    {
                        int k = Convert.ToInt16(dignoSelected.Split(',')[i]);
                        checkedListBoxDigno.SetItemChecked(k, true);
                    }
                }
                string valueProper = DataSource.Tables["tempInfo"].Rows[0]["valueProperSelected"].ToString();
                string[] valuePropers = valueProper.Split(',');
                for (int i = 0; i < valueProper.Split(',').Length - 1; i++)
                {
                    if (valuePropers[i] == ValuePropertyType.Average.ToString())
                    {
                        checkBoxAverage.Checked = true;
                    }
                    if (valuePropers[i] == ValuePropertyType.StandardDeviation.ToString())
                    {
                        checkBoxStandard.Checked = true;
                    }
                    if (valuePropers[i] == ValuePropertyType.Max.ToString())
                    {
                        checkBoxMax.Checked = true;
                    }
                    if (valuePropers[i] == ValuePropertyType.Min.ToString())
                    {
                        checkBoxMin.Checked = true;
                    }
                    if (valuePropers[i] == ValuePropertyType.Median.ToString())
                    {
                        checkBoxMedian.Checked = true;
                    }
                    if (valuePropers[i] == ValuePropertyType.Quar.ToString())
                    {
                        checkBoxQuar.Checked = true;
                    }
                }
                string dignoProper = DataSource.Tables["tempInfo"].Rows[0]["dignoProperSelected"].ToString();
                string[] dignoPropers = dignoProper.Split(',');
                for (int i = 0; i < dignoProper.Split(',').Length - 1; i++)
                {
                    if (dignoPropers[i] == DignosedPropertyType.NewMorbid.ToString())
                    {
                        checkBoxNewMorbi.Checked = true;
                    }
                    if (dignoPropers[i] == DignosedPropertyType.NewMorbidRate.ToString())
                    {
                        checkBoxNewMorbiRate.Checked = true;
                    }
                    if (dignoPropers[i] == DignosedPropertyType.Morbid.ToString())
                    {
                        checkBoxMorbi.Checked = true;
                    }
                    if (dignoPropers[i] == DignosedPropertyType.MorbidRate.ToString())
                    {
                        checkBoxMorbiRate.Checked = true;
                    }
                }
            }                        
            return true;
        }
        private bool CheckProperties()
        {
            if (checkedListBoxValue.CheckedIndices.Count != 0)
            {
                if (checkBoxAverage.Checked == true)
                {
                    return true;                    
                }
                if (checkBoxStandard.Checked == true)
                {
                    return true;                    
                } 
                if (checkBoxMax.Checked == true)
                {
                    return true;                    
                } 
                if (checkBoxMedian.Checked == true)
                {
                    return true;                     
                } 
                if (checkBoxMin.Checked == true)
                {
                    return true;                    
                }
                if (checkBoxQuar.Checked == true)
                {
                    return true;                    
                }
                return false;
            }
            if (checkedListBoxDigno.CheckedIndices.Count != 0)
            {
                if (checkBoxNewMorbiRate.Checked == true)
                {
                    return true; 
                }
                if (checkBoxMorbiRate.Checked == true)
                {
                    return true; 
                }
                if (checkBoxNewMorbi.Checked == true)
                {
                    return true; 
                }
                if (checkBoxMorbi.Checked == true)
                {
                    return true; 
                }
                return false;
            }
            return false;
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel[] flowLayoutPanels = new FlowLayoutPanel[2] { flowLayoutPanel1, flowLayoutPanel2 };
            if (!CheckQuery(flowLayoutPanels))
            {
                return;
            }
            if (!File.Exists(TempFilePath))
            {
                Directory.CreateDirectory(TempFilePath);
            }
            SaveTemplate(TempFilePath + "\\");
        }
        private void SaveTemplate(string filePath)
        {
            AddSelectTemplate getTemplateNameFrm = new AddSelectTemplate();
            getTemplateNameFrm.IsNew = true;
            getTemplateNameFrm.FilePath = filePath;
            getTemplateNameFrm.ShowDialog();
            if (getTemplateNameFrm.DialogResult == DialogResult.OK)
            {
                DataSet ds = GetTemplateDataSet(getTemplateNameFrm.txtTemplateName.Text.Trim(), getTemplateNameFrm.txtRemark.Text.Trim());
                ds.WriteXml(getTemplateNameFrm.FileName);
            }
        }
        private DataSet GetTemplateDataSet(string name, string remark)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //
            dt.TableName = "StatisticGroupCondition";
            DataColumn[] columns = new DataColumn[] { 
                        new DataColumn("colCnName"),
                        new DataColumn("colName"), 
                        new DataColumn("tableName"), 
                        new DataColumn("colType"), 
                        new DataColumn("sign"), 
                        new DataColumn("vaule"), 
                        new DataColumn("logicSign")};
            dt.Columns.AddRange(columns);
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                QueryBuilder.SQLStr msg = ((QueryBuilder)flowLayoutPanel1.Controls[i]).ShowSQLStr();
                dt.Rows.Add(new object[] { msg.ConditionName, msg.Condition, msg.TableName, msg.ConditionType, msg.Sign, msg.CheckValue, msg.LogicSign });
            }
            ds.Tables.Add(dt);

            dt = dt.Clone();
            dt.TableName = "ContrastGroupCondition";
            for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
            {
                QueryBuilder.SQLStr msg = ((QueryBuilder)flowLayoutPanel2.Controls[i]).ShowSQLStr();
                dt.Rows.Add(new object[] { msg.ConditionName, msg.Condition, msg.TableName, msg.ConditionType, msg.Sign, msg.CheckValue, msg.LogicSign });
            }
            ds.Tables.Add(dt);

            dt = new DataTable();

            dt.TableName = "tempInfo";
            columns = new DataColumn[] { 
                        new DataColumn("tempName"), 
                        new DataColumn("tempRemark"), 
                        new DataColumn("timepoints"),
                        new DataColumn("intervaltype"),
                        new DataColumn("valueSelected"),
                        new DataColumn("dignosedSelected"),
                        new DataColumn("valueProperSelected"),
                        new DataColumn("dignoProperSelected")};
            dt.Columns.AddRange(columns);
            dt.Rows.Add(new object[] { name, remark,GetTimePointsSelect(),comboBoxInterval.SelectedIndex.ToString() ,
                GetValueSelect(), GetDignosedSelect(), GetValuePropertySelect(),GetDignoPropertySelect()});
            ds.Tables.Add(dt);

            return ds;
        }
        private string GetValueSelect()
        {
            string valueIndices="";
            for (int i = 0; i < checkedListBoxValue.CheckedIndices.Count;i++ )
            {
                valueIndices += checkedListBoxValue.CheckedIndices[i].ToString();
                if (i != checkedListBoxValue.CheckedIndices.Count - 1)
                {
                    valueIndices += ",";
                }
            }
            return valueIndices;
        }
        private string GetDignosedSelect()
        {
            string dignoIndices = "";
            for (int i = 0; i < checkedListBoxDigno.CheckedIndices.Count; i++)
            {
                dignoIndices += checkedListBoxDigno.CheckedIndices[i].ToString();
                if (i != checkedListBoxDigno.CheckedIndices.Count - 1)
                {
                    dignoIndices += ",";
                }
            }
            return dignoIndices;
        }
        private string GetValuePropertySelect()
        {
            List<ValuePropertyType> ValuePropertiesList = GetValueProperties();
            List<string> strValues=new List<string>();
            foreach (ValuePropertyType valueProper in ValuePropertiesList)
            {
                strValues.Add(valueProper.ToString());
            }            
            string valueSelectProperty = string.Join(",", strValues.ToArray());
            return valueSelectProperty;
        }
        private string GetDignoPropertySelect()
        {
            List<DignosedPropertyType> DignosedPropertiesList = GetDignosedProperties();
            List<string> strDignos = new List<string>();
            foreach (DignosedPropertyType dignoProper in DignosedPropertiesList)
            {
                strDignos.Add(dignoProper.ToString());
            }                       
            string dignoSelectProperty = string.Join(",", strDignos.ToArray());
            return dignoSelectProperty;
        }
        private string GetTimePointsSelect()
        {
            string times="";
            DateTime Period1Start, Period1End, Period2Start, Period2End;
            Period1Start = dateTimePicker1.Value;
            Period1End = dateTimePicker2.Value;
            Period2Start = dateTimePicker3.Value;
            Period2End = dateTimePicker4.Value;
            times += Period1Start.ToString() + ",";
            times += Period1End.ToString() + ",";
            times += Period2Start.ToString() + ",";
            times += Period2End.ToString();
            return times;
        }
    }
}