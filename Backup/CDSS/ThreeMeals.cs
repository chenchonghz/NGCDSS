using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSSystemData;
using System.Xml;
using System.IO;

namespace CDSS
{
    public partial class ThreeMeals : Form
    {
        private string strNodeName;
        private string strBreakfastCount;
        private string strLunchCount;
        private string strSupperCount;
        private string strCerealCount;
        //add by lch  �޸�BugDB00005762
        private string strDetailsFrmName;

        public ThreeMeals(string strNodeName, string DetailsFrmName)
        {
            InitializeComponent();
            this.strNodeName = strNodeName;
            //add by lch  �޸�BugDB00005762
            this.strDetailsFrmName = DetailsFrmName;
            strCerealCount = GlobalData.DietSuggestion.CerealCount;
            btn_Breakfast.Visible = true;
            btn_Lunch.Visible = true;
            btn_Supper.Visible = true;
            GetThreeBtnText();
        }

        private void GetThreeBtnText()
        {
            if(strNodeName == "Cereal")
            {
                string datafile = "SuggestionDetails.xml";
                StreamReader streamReader = new StreamReader(".\\" + datafile);
                XmlDataDocument xmldatadoc = new XmlDataDocument();
                xmldatadoc.DataSet.ReadXml(streamReader);
                XmlNodeList xmlnodelists = xmldatadoc.FirstChild.ChildNodes;
                if (xmlnodelists.Count > 0)
                {
                    foreach (XmlNode node in xmlnodelists)
                    {
                        if (node.Name.Equals("ThreeMeals"))
                        {
                            //this.chklistDetail.Items.Add(node.Attributes.Item(0).Value + "(" + node.Attributes.Item(1).Value + "��/��)");
                            if(node.Attributes.Item(0).Value == strCerealCount)
                            {
                                btn_Breakfast.Text = "���" + node.Attributes.Item(1).Value + "��";
                                strBreakfastCount = node.Attributes.Item(1).Value;
                                btn_Lunch.Text = "���" + node.Attributes.Item(2).Value + "��";
                                strLunchCount = node.Attributes.Item(2).Value;
                                btn_Supper.Text = "���" + node.Attributes.Item(3).Value + "��";
                                strSupperCount = node.Attributes.Item(3).Value;
                                break;
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("û������");
                }
                xmldatadoc = null;
                streamReader.Close();
            }

            else if(strNodeName == "Greenstuff")
            {
                btn_Lunch.Text = "���0.5��";
                strLunchCount = "0.5";
                btn_Supper.Text = "���0.5��";
                strSupperCount = "0.5";
                btn_Breakfast.Visible = false;
            }
        }

        private void btn_Breakfast_Click(object sender, EventArgs e)
        {
            //revised by lch  �޸�BugDB00005762
            SuggestionDetails oSuggestionDetails = new SuggestionDetails(strNodeName, "���", strBreakfastCount, strDetailsFrmName);
            DialogResult drSuggestionDetails = oSuggestionDetails.ShowDialog();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Lunch_Click(object sender, EventArgs e)
        {
            //revised by lch  �޸�BugDB00005762
            SuggestionDetails oSuggestionDetails = new SuggestionDetails(strNodeName, "���", strLunchCount, strDetailsFrmName);
            DialogResult drSuggestionDetails = oSuggestionDetails.ShowDialog();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Supper_Click(object sender, EventArgs e)
        {
            //revised by lch  �޸�BugDB00005762
            SuggestionDetails oSuggestionDetails = new SuggestionDetails(strNodeName, "���", strSupperCount, strDetailsFrmName);
            DialogResult drSuggestionDetails = oSuggestionDetails.ShowDialog();
            DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}