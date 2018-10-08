using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace Utilities.DesignUIWithXmlFuction
{
    public class DesignUIWithXml
    {
        //用来判断加载的是否是第一个GroupBox
        bool judgeCount = true;

        /// <summary>
        /// 通过Xml文件动态生成相应的界面
        /// </summary>
        /// <param name="panel">用来承载控件的容器</param>
        public void XmlToUI(Panel panel)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Application .StartupPath +"\\Resource\\ControlInfo.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList rootNodeList = root.ChildNodes;
            Control groupBox = new Control();
            foreach (XmlNode rootNode in rootNodeList)
            {
                XmlElement ele = (XmlElement)rootNode;
                if (judgeCount == false)
                {
                    ele.Attributes[2].InnerXml = "2," + (groupBox.Location.Y + groupBox.Height + 16).ToString();
                }
                judgeCount = false;
                groupBox = (Control)this.LoadControl(ele);
                panel.Controls.Add(groupBox);

                XmlNodeList gbNodeList = ele.ChildNodes;
                foreach (XmlNode gbNode in gbNodeList)
                {
                    XmlElement eleChild = (XmlElement)gbNode;
                    Control control = (Control)this.LoadControl(eleChild);
                    groupBox.Controls.Add(control);
                }
            }
            judgeCount = true;
        }

        /// <summary>
        /// 加载相应的控件
        /// </summary>
        /// <param name="ele"></param>
        /// <returns></returns>
        private object LoadControl(XmlElement ele)
        {
            string controlTypeInfo = ele.Name;
            Type controlType = Type.GetType("System.Windows.Forms." + controlTypeInfo + ", System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            object controlInstance = Activator.CreateInstance(controlType);
            foreach (XmlAttribute xmlAttribute in ele.Attributes)
            {
                string propertyName = xmlAttribute.Name;
                object propertyInfo = xmlAttribute.InnerXml;
                PropertyInfo property = controlType.GetProperty(propertyName);
                if (propertyName == "Location")
                {
                    string str = xmlAttribute.InnerXml;
                    string[] strArray = str.Split(new char[] { ',' });
                    propertyInfo = (Point)new Point(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]));
                }
                if (propertyName == "Size")
                {
                    string str = xmlAttribute.InnerXml;
                    string[] strArray = str.Split(new char[] { ',' });
                    propertyInfo = (Size)new Size(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]));
                }
                if (propertyName == "AutoSize")
                {
                    propertyInfo = Convert.ToBoolean(propertyInfo);
                }
                if (propertyName == "TabIndex")
                {
                    propertyInfo = Convert.ToInt32(propertyInfo);
                }
                if (propertyName == "BackColor")
                {
                    string str = xmlAttribute.InnerXml;
                    string[] strArray = str.Split(new char[] { ',' });
                    propertyInfo = (Color)System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt32(strArray[0]))))), ((int)(((byte)(Convert.ToInt32(strArray[1]))))), ((int)(((byte)(Convert.ToInt32(strArray[2]))))));
                }
                if (propertyName == "Font")
                {
                    float f = (float)Convert.ToDouble(propertyInfo);
                    propertyInfo = (Font)new System.Drawing.Font("SimSun", f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                if (propertyName == "Anchor")
                {
                    string str = xmlAttribute.InnerXml;
                    string[] strArray = str.Split(new char[] { ',' });
                    List<AnchorStyles> anchorStyles=new List<AnchorStyles>(strArray.Length ) ;                    
                    foreach (string str1 in strArray)
                    {
                        if (str1 == "Left")
                        {
                            anchorStyles.Add(AnchorStyles.Left);
                        }
                        if (str1 == "Right")
                        {
                            anchorStyles.Add(AnchorStyles.Right);
                        }
                        if (str1 == "Top")
                        {
                            anchorStyles.Add(AnchorStyles.Top);
                        }
                        if (str1 == "Bottom")
                        {
                            anchorStyles.Add(AnchorStyles.Bottom);
                        }                      
                    }
                    if (strArray.Length == 1)
                    {
                        propertyInfo = anchorStyles[0];
                    }
                    if (strArray.Length == 2)
                    {
                        propertyInfo = (AnchorStyles)(anchorStyles[0] | anchorStyles[1]);
                    }
                    if (strArray.Length == 3)
                    {
                        propertyInfo = (AnchorStyles)(anchorStyles[0] | anchorStyles[1] | anchorStyles[2] );
                    }
                    if (strArray.Length == 4)
                    {
                        propertyInfo = (AnchorStyles)(anchorStyles[0] | anchorStyles[1]| anchorStyles[3] | anchorStyles[4]);
                    }
                }
                if (propertyName == "Tag")
                {
                    propertyInfo = null;
                }

                property.SetValue(controlInstance, propertyInfo, null);
            }
            return controlInstance;
        }
    }
}
