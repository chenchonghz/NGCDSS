using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CDSSCtrlLib
{
    public partial class QueryBuilder : UserControl
    {
        #region --Fields/Attributes/Propertys--

        public delegate void DeleteHandler(object sender, EventArgs e);//委托              

        private ValueTypes valueType = ValueTypes.Text;

        public enum ValueTypes { Text, Num, Date, Bool, Other };

        public ValueTypes ValueType
        {
            get { return valueType; }
            set
            {
                switch (value)
                {
                    case ValueTypes.Text:
                        cmbJdge.Items.Clear();
                        cmbJdge.Items.AddRange(new object[] { "≠", "＝" });
                        cmbJdge.SelectedIndex = 1;
                        txtValue.IsText = true;
                        break;
                    case ValueTypes.Num:
                        cmbJdge.Items.Clear();
                        cmbJdge.Items.AddRange(new object[] { "<", "≤", "≠", "＝", "≥", ">" });
                        cmbJdge.SelectedIndex = 3;
                        txtValue.IsText = false;
                        break;
                    case ValueTypes.Date:
                        cmbJdge.Items.Clear();
                        cmbJdge.Items.AddRange(new object[] { "<", "≤", "≠", "＝", "≥", ">" });
                        cmbJdge.SelectedIndex = 3;
                        txtValue.IsText = true;
                        break;
                    case ValueTypes.Bool:
                        cmbJdge.Items.Clear();
                        cmbJdge.Items.AddRange(new object[] { "≠", "＝" });
                        cmbJdge.SelectedIndex = 1;
                        txtValue.IsText = true;
                        break;
                    case ValueTypes.Other:
                        cmbJdge.Items.Clear();
                        cmbJdge.Items.AddRange(new object[] { "≠", "＝" });
                        cmbJdge.SelectedIndex = 1;
                        txtValue.IsText = true;
                        break;
                    default:
                        break;
                }
                valueType = value;
            }
        }

        /// <summary>
        /// 查询控件返回的结构
        /// </summary>
        public struct SQLStr
        {
            /// <summary>
            /// 查询条件中文名
            /// </summary>
            public string ConditionName;
            /// <summary>
            /// 查询条件字段名
            /// </summary>
            public string Condition;
            /// <summary>
            /// 判断符号
            /// </summary>
            public string Sign;
            /// <summary>
            /// 条件数值
            /// </summary>
            public string CheckValue;
            /// <summary>
            /// 条件间的逻辑关系
            /// </summary>
            public string LogicSign;
            /// <summary>
            /// 条件所在表名
            /// </summary>
            public string TableName;
            /// <summary>
            /// 查询条件数据类型
            /// </summary>
            public string ConditionType;
            /// <summary>
            /// 查询语句类型
            /// </summary>
            public string SqlDemo;
        };

        public event DeleteHandler Delete;

        #endregion

        #region --Constructor--

        public QueryBuilder()
        {
            InitializeComponent();
            CreateNodes();
            toolTip1.SetToolTip(cmbName, "项目名称");
            toolTip1.SetToolTip(txtValue, "项目数值");
        }

        #endregion

        #region --Functions--

        protected virtual void OnDelete(EventArgs e)
        {
            if (Delete != null)
                Delete(this, e);
        }

        private bool enabled = true;
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                cmbJdge.Enabled = value;
                cmbLgc.Enabled = value;
                cmbName.Enabled = value;
                txtValue.Enabled = value;
                btnDel.Enabled = value;
                enabled = value;
            }
        }

        private void CreateNodes()
        {
            TreeNode root, node;
            cmbName.TreeView.Nodes.Clear();
            DataSet ds = new DataSet();
            ds.ReadXml(Application.StartupPath + "\\Resource\\TreeConfig.xml");
            foreach (DataTable dt in ds.Tables)
            {
                string[] tmp = dt.TableName.Split('!');
                root = new TreeNode(tmp[1]);
                root.Name = tmp[0];
                cmbName.TreeView.Nodes.Add(root);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["canSelect"].ToString() == "0")
                    {
                        node = new TreeNode(dr["cnName"].ToString());
                        node.Name = dr["enName"].ToString();
                        node.Tag = new string[] { tmp[0], dr["tag"].ToString(), dr["JoinSqlStr"].ToString() };
                        root.Nodes.Add(node);
                    }
                }
            }
        }

        public SQLStr ShowSQLStr()
        {
            TreeNode selectedNode = cmbName.Tag as TreeNode;
            SQLStr mystruct = new SQLStr();
            if (null == selectedNode)
            {
                mystruct.ConditionName = "";
                mystruct.Condition = "";
                mystruct.TableName = "";
                mystruct.ConditionType = "";
                mystruct.Sign = "";
                mystruct.CheckValue = "";
                mystruct.LogicSign = "";
                mystruct.SqlDemo = "";
                return mystruct;
            }
            else
            {
                mystruct.ConditionName = selectedNode.Text;
                mystruct.Condition = selectedNode.Name;
                mystruct.TableName = ((string[])selectedNode.Tag)[0];
                mystruct.ConditionType = ((string[])selectedNode.Tag)[1];
                mystruct.Sign = TxtToSign(cmbJdge.Text);
                mystruct.CheckValue = txtValue.RealVaule.Trim();
                mystruct.LogicSign = TxtToSign(cmbLgc.Text);
                mystruct.SqlDemo = ((string[])selectedNode.Tag)[2];
                return mystruct;
            }
        }

        /// <summary>
        /// 填充此项查询条件
        /// </summary>
        public void FillQueryBuilder(SQLStr dataStruct)
        {
            cmbLgc.Items.AddRange(new object[] { "无", "且", "或" });
            //TreeNode node = new TreeNode(dataStruct.ConditionName);
            //node.Name = dataStruct.Condition;
            //node.Tag = dataStruct.TableName + "," + dataStruct.ConditionType;
            cmbName.GetSelectNode(dataStruct.TableName, dataStruct.Condition);
            //cmbName.SelectNode = node;
            //cmbName_SelectedIndexChanged(null, null);
            cmbJdge.Text = SignToTxt(dataStruct.Sign);
            txtValue.RealVaule = dataStruct.CheckValue;
            cmbLgc.Text = SignToTxt(dataStruct.LogicSign);
        }

        /// <summary>
        /// 将combobox中的文字转换为逻辑符号
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private string TxtToSign(string txt)
        {
            string sign = "";
            Dictionary<string, string> txtDict = new Dictionary<string, string>();
            txtDict.Add("<", " < ");
            txtDict.Add("≤", " <= ");
            txtDict.Add("≠", " <> ");
            txtDict.Add("＝", " = ");
            txtDict.Add("≥", " >= ");
            txtDict.Add(">", " > ");
            txtDict.Add("无", " ");
            txtDict.Add("且", " and ");
            txtDict.Add("或", " or ");
            txtDict.Add("", "");
            if (txtDict.ContainsKey(txt))
                sign = txtDict[txt];
            return sign;
        }

        /// <summary>
        /// 将逻辑符号转换为combobox中的文字
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private string SignToTxt(string txt)
        {
            string sign = "";
            Dictionary<string, string> txtDict = new Dictionary<string, string>();
            txtDict.Add(" < ", "<");
            txtDict.Add(" <= ", "≤");
            txtDict.Add(" <> ", "≠");
            txtDict.Add(" = ", "＝");
            txtDict.Add(" >= ", "≥");
            txtDict.Add(" > ", ">");
            txtDict.Add(" ", "无");
            txtDict.Add(" and ", "且");
            txtDict.Add(" or ", "或");
            txtDict.Add("", "");
            if (txtDict.ContainsKey(txt))
                sign = txtDict[txt];
            return sign;
        }

        #endregion

        #region --Events--

        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除此查询条件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OnDelete(e);
            }
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode selectedNode = cmbName.Tag as TreeNode;
            if (selectedNode == null) return;
            string typeStr = ((string[])selectedNode.Tag)[1];
            switch (typeStr)
            {
                case "文本":
                    ValueType = ValueTypes.Text;
                    break;
                case "数字":
                    ValueType = ValueTypes.Num;
                    break;
                case "日期":
                    ValueType = ValueTypes.Date;
                    break;
                case "布尔":
                    ValueType = ValueTypes.Bool;
                    break;
                case "其他":
                    ValueType = ValueTypes.Other;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
