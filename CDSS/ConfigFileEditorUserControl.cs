using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Utilities.ConfigFileEditor;

namespace CDSS
{
    public partial class ConfigFileEditorUserControl : UserControl
    {
        public ConfigFileEditorUserControl()
        {
            InitializeComponent();            
        }

        private int selectedIndex;
        private string filePath, fileType;       
        private DataSet ds;

        //用来判断DataGridView控件内容是否改动
        public bool isCellValueChanged = false;

        //在修改DataGridView进行提交时需要BindingSource作为中介
        private BindingSource bs = new BindingSource();
        private ConfigFileEditFactory factory = new ConfigFileEditFactory();


        #region 定义的一些方法
        /// <summary>
        /// 解析饮食运动对饮字典成XmlNodeList
        /// </summary>
        /// <param name="dictionaryPath"></param>
        /// <returns></returns>
        private XmlNodeList GetFoodSportSettingsDictionary(string dictionaryPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dictionaryPath);
            XmlNode root = doc.DocumentElement;
            XmlNodeList nodelist = doc.SelectSingleNode(root.Name).ChildNodes;
            return nodelist;
        }
        /// <summary>
        /// ListBox1显示饮食运动设置对应文件框架
        /// 其中通过相应的FoodSportSettingsDictionary.xml实现了将英文转换成中文
        /// </summary>
        /// <param name="ds"></param>
        private void DisplayFileFrame(DataSet ds, ListBox listBox)
        {
            bool judgeDictoinaryIntegrity = false;
            listBox.Items.Clear();
            foreach (DataTable dt in ds.Tables)
            {
                XmlNodeList nodelist = GetFoodSportSettingsDictionary(Application.StartupPath + "\\Resource\\ConfigFileEditorDictoinary.xml");
                foreach (XmlNode node in nodelist)
                {
                    XmlElement ele = (XmlElement)node;
                    if (dt.TableName == ele.Name)
                    {
                        string itemName = ele.InnerText;
                        listBox.Items.Add(itemName);
                        judgeDictoinaryIntegrity = true;
                    }
                }
            }
            if (judgeDictoinaryIntegrity == false)
            {
                MessageBox.Show("ConfigFileEditorDictionary.xml不完整请确定并加以完善", "提示");
            }
        }

        /// <summary>
        /// DataGridView显示文件内容
        /// </summary>
        /// <param name="ds"></param>
        private void DisplayFileContent(DataSet ds, DataGridView dataGridView)
        {
            dataGridView.DataSource = ds;
            dataGridView.DataMember = ds.Tables[selectedIndex].TableName;
            bs.DataSource = ds;
            dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
            dataGridView.CurrentCell.Selected = true;
        }

        /// <summary>
        /// 删除一记录
        /// </summary>
        /// <param name="dataGridView"></param>
        public void DeleteRow(DataGridView dataGridView)
        {
            try
            {
                dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Selected = true;
                DataRowView drv = (DataRowView)dataGridView.SelectedRows[0].DataBoundItem;
                drv.Row.Delete();
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 添加新记录
        /// </summary>
        public void AddRow(DataGridView dataGridView)
        {
            //DataGridView上添加此时相当于修改最后空着的一行了
            dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0].Selected = true;
            dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0];
            dataGridView.BeginEdit(true);

            //dataset上变化放到保存菜单中    
        }
        #endregion

        /// <summary>
        /// 用户控件加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigFileEditorUserControl_Load(object sender, EventArgs e)
        {
            try
            {
                //控制相应按钮状态
                this.btnAdd.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnSave.Enabled = false;

                //显示配置文件框架结构
                filePath = Application.StartupPath + "\\SuggestionDetails.xml";
                fileType = ".xml";
                ConfigFileEdit configFileEdit = factory.CreatConfigFileEdit(fileType);
                ds = configFileEdit.ConfigFileToDataSet(filePath);
                DisplayFileFrame(ds, this.listBox1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("找不到相应的配置文件请与管理员联系"+ex.Message,"提示");
            }
        }

        /// <summary>
        ///  选择listbox1相应选项时的动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //修改按钮状态
            this.btnAdd.Enabled = true;
            this.btnDelete.Enabled = true;
            this.btnSave.Enabled = true;

            selectedIndex = this.listBox1.SelectedIndex;
            DisplayFileContent(this.ds, this.dataGridView1);           
        }        

        /// <summary>
        /// 删除该记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            DeleteRow(this.dataGridView1);
            this.isCellValueChanged = true;
        }
        
        /// <summary>
        /// 添加新记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
              AddRow(this.dataGridView1);             
        }            
        
        /// <summary>
        ///  保存相应的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void btnSave_Click_1(object sender, EventArgs e)
        {
            //提交DataGridView上的修改到ds       
            bs.EndEdit();
            this.dataGridView1.DataSource = bs;

            ConfigFileEdit configFileEdit = factory.CreatConfigFileEdit(fileType);
            configFileEdit.SaveFileChange(ds, filePath);
            MessageBox.Show("保存成功", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.isCellValueChanged = false;
        }

        /// <summary>
        /// 当DataGridView单元格内容变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.isCellValueChanged = true;
        }
       
        /// <summary>
        /// 构造一公共方法供其他界面调用保存相应的修改
        /// </summary>
        public void Save()
        {
            //提交DataGridView上的修改到ds       
            bs.EndEdit();
            this.dataGridView1.DataSource = bs;

            ConfigFileEdit configFileEdit = factory.CreatConfigFileEdit(fileType);
            configFileEdit.SaveFileChange(ds, filePath);          
            this.isCellValueChanged = false;
        }       
    }
}
