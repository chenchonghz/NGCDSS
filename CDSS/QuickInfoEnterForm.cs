using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utilities.DesignUIWithXmlFuction;
using CDSSCtrlLib;
using System.Xml;
using CDSSSystemData;
using System.Reflection;

namespace CDSS
{
    public partial class QuickInfoEnterForm : InfoFormBaseClass 
    {
        public QuickInfoEnterForm()
        {
            InitializeComponent();           
        }

        private  bool bLoaded = false;    //标记数据是否已经加载过        
        private DesignUIWithXml designUIWithXml = new DesignUIWithXml(); 

        #region 用户事件

        /// <summary>
        /// 界面加载时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuickInfoEnterForm_Load(object sender, EventArgs e)
        {
            designUIWithXml.XmlToUI(this.pnlQuickInfoEnter);
        }

        /// <summary>
        /// 窗体Visible属性变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuickInfoEnterForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                //在窗体显示出来的时候加载数据
                LoadDataFromVarToUI();
            }      
        }
      
        #endregion

        #region 功能函数

        /// <summary>
        /// 从全局变量加载数据到界面
        /// </summary>
        public override void LoadDataFromVarToUI()
        {
            ////如果界面数据没有变化返回
            //if (!IsModified)
            //    return;

            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + "\\Resource\\ControlInfo.xml");
            XmlNodeList nodeList = doc.SelectNodes("/Control//GroupBox/TextBox");
            foreach (XmlNode node in nodeList)
            {
                XmlElement ele = (XmlElement)node;
                //找到动态添加的相应控件
                Control[] textBox = this.pnlQuickInfoEnter.Controls.Find(ele.Attributes[0].InnerXml, true);
                //textBox[0]. DataChanged += new System.EventHandler(this.DataModified);
                textBox[0].TextChanged += new EventHandler(QuickInfoEnterForm_TextChanged);
                

                //找到控件对应全局变量并将其赋值
                string str = ele.Attributes[6].InnerXml;
                string[] strArray = str.Split(new char[] { '.' });            
                Type type = typeof(CDSSSystemData.GlobalData);
                FieldInfo fieldInfo = type.GetField(strArray[0]);
                object parameter = fieldInfo.GetValue(null);
                type = parameter.GetType();
                fieldInfo = type.GetField(strArray[1]);
                textBox[0].Text = fieldInfo.GetValue(parameter).ToString();
            }

            //设置数据已加载标志
            this.bLoaded = true;
        }

        /// <summary>
        /// 页面内容改变时（TextBox内容改变时）事件的处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void QuickInfoEnterForm_TextChanged(object sender, EventArgs e)
        {
            if (IsModified || (!PatInfo.bNewPatient && !bLoaded))
                return;
                        
            IsModified = true;

            InfoEnter.judgeTextChanged = true;
            
        }

        /// <summary>
        /// 将界面数据保存至变量
        /// </summary>
        public override void LoadDataFromUIToVar()
        {
            ////如果界面数据没有变化返回
            //if (!IsModified)
            //    return;

            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + "\\Resource\\ControlInfo.xml");
            XmlNodeList nodeList=doc.SelectNodes ("/Control//GroupBox/TextBox");
            foreach (XmlNode node in nodeList)
            {
                XmlElement ele = (XmlElement)node;
                //找到动态添加的相应控件
                Control[] textBox = this.pnlQuickInfoEnter.Controls.Find(ele.Attributes[0].InnerXml, true);

                //找到控件对应全局变量并将其赋值
                string str = ele.Attributes[6].InnerXml;
                string[] strArray = str.Split(new char[] { '.'});
                Type type = typeof(CDSSSystemData.GlobalData);
                FieldInfo fieldInfo = type.GetField(strArray[0]);
                object parameter = fieldInfo.GetValue(null);
                type= parameter.GetType();
                fieldInfo = type.GetField(strArray[1]);
                object result;
                switch (fieldInfo.FieldType.Name)
                {
                    case "Boolean":
                        if (textBox[0].Text.Contains("是") || textBox[0].Text.Contains("有") || textBox[0].Text.Contains("1") || textBox[0].Text.Trim().ToLower() == "yes" || textBox[0].Text.Trim().ToLower() == "true")
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }

                        break;
                    default:
                        result = textBox[0].Text;
                        break;
                }
                
                fieldInfo.SetValue(parameter, result);
            }
            IsModified = false;
        }

        /// <summary>
        /// 清空页面内容
        /// </summary>
        public void ClearData()
        {
            foreach (Control control in this.pnlQuickInfoEnter.Controls)
            {
                foreach (Control childControl in control.Controls)
                {
                    if (childControl is TextBox)
                        childControl.Text = String.Empty;
                    if (childControl is RichTextBox)
                        childControl.Text = String.Empty;
                    if (control is ComboBox)
                        control.Text = String.Empty;
                }              
            }  

            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
        }

        /// <summary>
        /// 清空函数
        /// </summary>
        /// <param name="Controls"></param>
        private void ResetControl(Control.ControlCollection Controls)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    control.Text = String.Empty;
                }
                if (control is ComboBox)
                {
                    control.Text = String.Empty;
                }
                if (control is CDSSCtrlLib.DateTimeControl)
                {
                    ((CDSSCtrlLib.DateTimeControl)control).Value = new DateTime(DateTime.Now.Year, 1, 1);
                }
                else if (control is Panel)
                {
                    ResetControl(control.Controls);
                }
                else if (control is TableLayoutPanel)
                {
                    ResetControl(control.Controls);
                }
            }
        }

        #endregion

       

    }
}