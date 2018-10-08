using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using CDSSSystemData;


namespace CDSS
{
    public partial class SuggestionDetails : Form
    {
        private string nodeName;
        private string strMealType;
        private string strMealCount;

        public SuggestionDetails(string strNodeName, string strMealType, string strMealCount, string DetailsFrmName)
        {
            InitializeComponent();
            this.nodeName = strNodeName;
            this.strMealType = strMealType;
            this.strMealCount = strMealCount;
            //add by lch  修复BugDB00005762,根据参数设置详细信息的窗体标题栏文本
            this.Text = DetailsFrmName;

        }

        private void SuggestionDetails_Load(object sender, EventArgs e)
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
                    //2009-9-2:ccj
                    //在显示选择界面前查询原先的选择情况，使界面反映原先的选择情况
                    if (node.Name.Equals(nodeName))
                    {
                        string tempnode = string.Empty;
                        string comparenode = string.Empty;
                        bool state = false;
                        if (nodeName == "FreeSport" || nodeName == "LowSport" || nodeName == "MiddleSport" || nodeName == "HighSport")
                        {
                            tempnode = node.Attributes.Item(0).Value;
                            if (nodeName == "FreeSport")
                            {
                                if (CheckShowState(tempnode, GlobalData.ExerciseSuggestion.NoIntensityExerciseItems))
                                {
                                    state = true;
                                }
                            }
                            else if (nodeName == "LowSport")
                            {
                                if (CheckShowState(tempnode, GlobalData.ExerciseSuggestion.LowIntensityExerciseItems))
                                {
                                    state = true;
                                }
                            }
                            else if (nodeName == "MiddleSport")
                            {
                                if (CheckShowState(tempnode, GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems))
                                {
                                    state = true;
                                }
                            }

                            else if (nodeName == "HighSport")
                            {
                                if (CheckShowState(tempnode, GlobalData.ExerciseSuggestion.HighIntensityExerciseItems))
                                {
                                    state = true;
                                }
                            }

                            this.chklistDetail.Items.Add(tempnode, state);

                        }
                        else
                        {
                            tempnode = node.Attributes.Item(0).Value + "(" + node.Attributes.Item(1).Value + "克/份)";
                            comparenode = node.Attributes.Item(0).Value;
                            if (nodeName == "Cereal")
                            {
                                /*********************************************部分功能描述***************************************************************/
                                //从GlobalData.DietSuggestion.CerealDetail中拆分出
                                //GlobalData.DietSuggestion.CerealBreakfastDetail，
                                //GlobalData.DietSuggestion.CerealLunchDetail
                                //GlobalData.DietSuggestion.CerealSupperDetail
                                //保证这三个变量随时与数据库内的一致

                                Regex breakfastRegx = new Regex(@"早餐\[(?>\[(?<n>)|\](?<-n>)|(?!\[|\]).)*(?(n)(?!))\]");
                                Regex lunchRegx = new Regex(@"午餐\[(?>\[(?<n>)|\](?<-n>)|(?!\[|\]).)*(?(n)(?!))\]");
                                Regex supperRegx = new Regex(@"晚餐\[(?>\[(?<n>)|\](?<-n>)|(?!\[|\]).)*(?(n)(?!))\]");

                                GlobalData.DietSuggestion.CerealBreakfastDetail = breakfastRegx.Match(GlobalData.DietSuggestion.CerealDetail).Groups[0].Value;
                                GlobalData.DietSuggestion.CerealLunchDetail = lunchRegx.Match(GlobalData.DietSuggestion.CerealDetail).Groups[0].Value;
                                GlobalData.DietSuggestion.CerealSupperDetail = supperRegx.Match(GlobalData.DietSuggestion.CerealDetail).Groups[0].Value;
                                /*************************************************************************************************************/

                                //根据餐名在对应的全局变量中查找选中项
                                if (strMealType == "早餐")
                                {
                                    if (CheckShowState(comparenode, GlobalData.DietSuggestion.CerealBreakfastDetail))
                                    {
                                        state = true;
                                    }
                                }
                                if (strMealType == "午餐")
                                {
                                    if (CheckShowState(comparenode, GlobalData.DietSuggestion.CerealLunchDetail))
                                    {
                                        state = true;
                                    }
                                }
                                if (strMealType == "晚餐")
                                {
                                    if (CheckShowState(comparenode, GlobalData.DietSuggestion.CerealSupperDetail))
                                    {
                                        state = true;
                                    }
                                }
                            }
                            else if (nodeName == "Fruit")
                            {
                                if (CheckShowState(comparenode, GlobalData.DietSuggestion.FruitDetail))
                                {
                                    state = true;
                                }
                            }
                            else if (nodeName == "Greenstuff")
                            {
                                /********************************************拆分字符串变量******************************************************/
                                Regex lunchRegx = new Regex(@"午餐\[(?>\[(?<n>)|\](?<-n>)|(?!\[|\]).)*(?(n)(?!))\]");
                                Regex supperRegx = new Regex(@"晚餐\[(?>\[(?<n>)|\](?<-n>)|(?!\[|\]).)*(?(n)(?!))\]");

                                GlobalData.DietSuggestion.GreenstuffLunchDetail = lunchRegx.Match(GlobalData.DietSuggestion.GreenstuffDetail).Groups[0].Value;
                                GlobalData.DietSuggestion.GreenstuffSupperDetail = supperRegx.Match(GlobalData.DietSuggestion.GreenstuffDetail).Groups[0].Value;
                                /******************************************************************************************************************/
                                if (strMealType == "午餐")
                                {
                                    if (CheckShowState(comparenode, GlobalData.DietSuggestion.GreenstuffLunchDetail))
                                    {
                                        state = true;
                                    }
                                }
                                else if (strMealType == "晚餐")
                                {
                                    if (CheckShowState(comparenode, GlobalData.DietSuggestion.GreenstuffSupperDetail))
                                    {
                                        state = true;
                                    }
                                }
                            }
                            else if (nodeName == "Dairy")
                            {
                                if (CheckShowState(comparenode, GlobalData.DietSuggestion.DairyDetail))
                                {
                                    state = true;
                                }
                            }
                            else if (nodeName == "Egg")
                            {
                                if (CheckShowState(comparenode, GlobalData.DietSuggestion.EggDetail))
                                {
                                    state = true;
                                }
                            }
                            else if (nodeName == "BeanProduct")
                            {
                                if (CheckShowState(comparenode, GlobalData.DietSuggestion.BeanProductDetail))
                                {
                                    state = true;
                                }
                            }
                            else if (nodeName == "Meat")
                            {
                                if (CheckShowState(comparenode, GlobalData.DietSuggestion.MeatDetail))
                                {
                                    state = true;
                                }
                            }
                            else if (nodeName == "VegetableOil")
                            {
                                if (CheckShowState(comparenode, GlobalData.DietSuggestion.VegetableOilDetail))
                                {
                                    state = true;
                                }
                            }
                            else if (nodeName == "OtherFatFood")
                            {
                                if (CheckShowState(comparenode, GlobalData.DietSuggestion.OtherFatFoodDetail))
                                {
                                    state = true;
                                }
                            }
                            //修改结束
                            this.chklistDetail.Items.Add(tempnode, state);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("没有数据");
            }

            xmldatadoc = null;
            streamReader.Close();
        }

        /// <summary>
        /// 返回原先窗口各项的选中情况
        /// </summary>
        /// <param name="comparenod">查询项目名称</param>
        /// <param name="showString">showString为查询对象</param>
        /// <returns></returns>
        private bool CheckShowState(string compareNod,string showString)
        {
            return showString.Contains(compareNod);
        }
     
        public void ThreeMealsChoose(ref string strMeal)
        {
            if (this.chklistDetail.CheckedItems.Count > 0)
            {
                strMeal = strMealType + "[";
                for (int i = 0; i < this.chklistDetail.CheckedItems.Count; i++)
                {
                    string strChekcItem = this.chklistDetail.CheckedItems[i].ToString();

                    string[] SS = strChekcItem.Split(new string[] { "(" }, StringSplitOptions.None);

                    if(SS.Length == 2)
                    {
                        string strMealName = SS[0];
                        string strMealNum = SS[1].Substring(0, SS[1].IndexOf("克"));

                        try
                        {
                            strMealNum = (((System.Math.Ceiling(Convert.ToDouble(strMealNum) * Convert.ToDouble(strMealCount)) / 5) * 5)).ToString();
                        }

                        catch
                        {
                            strMealNum = "0";
                        }
               
                        if (i != this.chklistDetail.CheckedItems.Count - 1)
                        {
                            strMeal += strMealName + strMealNum + "克" + "," + "或 ";
                        }
                        else
                        {
                            strMeal += strMealName + strMealNum + "克" + "]";
                        }
                    }
                    else
                    {
                         strMeal += string.Empty;
                    }                    
                }
            }
            ////2009-9-2:ccj
            ////使没有选中栏的情况下显示为空
            else
            {
                strMeal = string.Empty;
            }
            ////修改结束
        }

        public void MealChoose(ref string strMeal)
        {
            if (this.chklistDetail.CheckedItems.Count > 0)
            {
                strMeal = string.Empty;
                for (int i = 0; i < this.chklistDetail.CheckedItems.Count; i++)
                {
                    string strChekcItem = this.chklistDetail.CheckedItems[i].ToString();

                    string[] SS = strChekcItem.Split(new string[] { "(" }, StringSplitOptions.None);
                    if(SS.Length == 2)
                    {
                        string strMealName = SS[0];
                        string strMealNum = SS[1].Substring(0, SS[1].IndexOf("克"));

                        try
                        {
                            strMealNum = (((System.Math.Ceiling(Convert.ToDouble(strMealNum) * Convert.ToDouble(strMealCount)) / 5) * 5)).ToString();
                        }

                        catch
                        {
                            strMealNum = "0";
                        }

                        if (i != this.chklistDetail.CheckedItems.Count - 1)
                        {
                            strMeal += strMealName + strMealNum + "克" + "," + "或 ";
                        }
                        else
                        {
                            strMeal += strMealName + strMealNum + "克";
                        }
                    }
                    else
                    {
                        strMeal += string.Empty;
                    }
                    

                    
                }
            }
            ////2009-9-2:ccj
            ////使没有选中栏的情况下显示为空
            else
            {
                strMeal = string.Empty;
            }
            ////修改结束
        }

        public void SportChoose(ref string strSport)
        {
            if (this.chklistDetail.CheckedItems.Count > 0)
            {
                strSport = string.Empty;
                for (int i = 0; i < this.chklistDetail.CheckedItems.Count; i++)
                {
                    string strChekcItem = this.chklistDetail.CheckedItems[i].ToString();

                    if (i != this.chklistDetail.CheckedItems.Count - 1)
                    {
                        strSport += strChekcItem + "," + "或 ";
                    }
                    else
                    {
                        strSport += strChekcItem;
                    }
                }
            }
            ////2009-9-2:ccj
            ////使没有选中栏的情况下显示为空
            else
            {
                strSport = string.Empty;
            }
            ////修改结束
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (nodeName)
            {
                case "Cereal":
                    if(strMealType == "早餐")
                    {
                        ThreeMealsChoose(ref GlobalData.DietSuggestion.CerealBreakfastDetail);
                    }
                    else if(strMealType == "午餐")
                    {
                        ThreeMealsChoose(ref GlobalData.DietSuggestion.CerealLunchDetail);
                    }
                    else if(strMealType == "晚餐")
                    {
                        ThreeMealsChoose(ref GlobalData.DietSuggestion.CerealSupperDetail);
                    }

                    GlobalData.DietSuggestion.CerealDetail = 
                        GlobalData.DietSuggestion.CerealBreakfastDetail + " "
                    + GlobalData.DietSuggestion.CerealLunchDetail + " "
                    + GlobalData.DietSuggestion.CerealSupperDetail;
                    break;
                case "Fruit":
                    MealChoose(ref GlobalData.DietSuggestion.FruitDetail);
                    break;
                case "Greenstuff":
                    if(strMealType == "午餐")
                    {
                        ThreeMealsChoose(ref GlobalData.DietSuggestion.GreenstuffLunchDetail);
                    }
                    else if(strMealType == "晚餐")
                    {
                        ThreeMealsChoose(ref GlobalData.DietSuggestion.GreenstuffSupperDetail);
                    }

                    GlobalData.DietSuggestion.GreenstuffDetail =
                       GlobalData.DietSuggestion.GreenstuffLunchDetail + " "
                   + GlobalData.DietSuggestion.GreenstuffSupperDetail;
                    break;
                case "Dairy":
                    MealChoose(ref GlobalData.DietSuggestion.DairyDetail);
                    break;
                case "Egg":
                    MealChoose(ref GlobalData.DietSuggestion.EggDetail);
                    break;
                case "Meat":
                    MealChoose(ref GlobalData.DietSuggestion.MeatDetail);
                    break;
                case "BeanProduct":
                    MealChoose(ref GlobalData.DietSuggestion.BeanProductDetail);
                    break;
                case "VegetableOil":
                    MealChoose(ref GlobalData.DietSuggestion.VegetableOilDetail);
                    break;
                case "OtherFatFood":
                    MealChoose(ref GlobalData.DietSuggestion.OtherFatFoodDetail);
                    break;

                case "FreeSport":
                    SportChoose(ref GlobalData.ExerciseSuggestion.NoIntensityExerciseItems);
                    break;
                case "LowSport":
                    SportChoose(ref GlobalData.ExerciseSuggestion.LowIntensityExerciseItems);
                    break;
                case "MiddleSport":
                    SportChoose(ref GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems);
                    break;
                case "HighSport":
                    SportChoose(ref GlobalData.ExerciseSuggestion.HighIntensityExerciseItems);
                    break;
            }
            DialogResult = DialogResult.OK;
            this.Close();
            
        }

    }
}