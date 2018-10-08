using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSSystemData;
using Microsoft.Reporting.WinForms;
using Utilities.PrintFunction;

namespace CDSS
{
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();
        }

        private PrintClass printClass = new PrintClass();

        //private PrintDataSource pds;

        /**********************************************************************
         * 添加人：XY
         * 添加时间：20081222
         * 添加说明：响应MainForm中的打印按钮，初始化打印报表
         * 添加部分：“初始化打印报表函数”
         **********************************************************************/
        /// <summary>
        /// 初始化打印报表函数
        /// </summary>
        public void ShowPrtReport()
        {
            //传入打印所需的公有变量，Revised by Jyl,081222
            PatInfo.pds = new PrintDataSource();
            InitPrintDataSource(PatInfo.pds);
            this.PrintSourceBindingSource.DataSource = PatInfo.pds;

            //增加子报表功能，以支持复杂表格布局，Added By ZQY,090325
            this.reportViewer1.LocalReport.SubreportProcessing +=
            new SubreportProcessingEventHandler(SubreportProcessingEventHandler);

            this.reportViewer1.RefreshReport();
        }
        //增加子报表功能，以支持复杂表格布局，Added By ZQY,090325
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("CDSS_PrintDataSource",  this.PrintSourceBindingSource));
        }

        private string[] GetSelfCheckSuggestion(string selfCheck, string lifeString, string selfCheckString)
        {
            string[] result = new string[] {string.Empty, string.Empty};
            int lifeIndex = selfCheck.IndexOf(lifeString);
            int selfCheckIndex = selfCheck.IndexOf(selfCheckString);

            if (lifeIndex >= 0 && lifeIndex < selfCheckIndex)
            {
                result[0] = selfCheck.Substring(lifeIndex + lifeString.Length, selfCheckIndex - lifeIndex - lifeString.Length).Trim();
                result[1] = selfCheck.Substring(selfCheckIndex + selfCheckString.Length).Trim();
            }
            else if (selfCheckIndex >= 0 && lifeIndex > selfCheckIndex)
            {
                result[1] = selfCheck.Substring(selfCheckIndex + selfCheckString.Length, lifeIndex - selfCheckIndex - selfCheckString.Length).Trim();
                result[0] = selfCheck.Substring(lifeIndex + lifeString.Length).Trim();
            }

            if (lifeIndex < 0)
            {
                result[1] = selfCheck.Replace(selfCheckString, string.Empty).Trim();
            }

            if (selfCheckIndex < 0)
            {
                result[0] = selfCheck.Replace(lifeString, string.Empty).Trim();
            }

            return result;
        }

        /// <summary>
        /// 初始化打印数据
        /// </summary>
        /// <param name="pds">打印所需数据源</param>
        private void InitPrintDataSource(PrintDataSource pds)
        {
            #region 一般信息
            pds.PatName = GlobalData.PatBasicInfo.PatName;
            int Age = 0;
            try
            {
                Age= DateTime.Now.Year - Convert.ToDateTime(GlobalData.PatBasicInfo.PatBirthday).Year;
            }
            catch (System.Exception e)
            {
            	Age = -1;
            }
            if (Age > 150 || Age < 0)
                pds.PatAge = "";
            else
                pds.PatAge = Age.ToString();
            pds.PatSex = GlobalData.PatBasicInfo.PatSex;
            pds.PatFrom = GlobalData.PatBasicInfo.PatBirthProvince + GlobalData.PatBasicInfo.PatBirthCity;
            pds.Doctor = GlobalData.UserInfo.UserName;
            pds.OpDate = DateTime.Now.ToShortDateString();
            #endregion

            #region 诊断结论
            foreach (CDSSOneDiseaseDiagnosedResult dr in GlobalData.DiagnosedResult.DiseaseDiagnosedResultList)
            {
                //if (!string.IsNullOrEmpty(dr.Result.Trim()))
                {
                    if (dr.Name == "糖代谢")
                    {
                        pds.TdxSummary = dr.Result.Contains("正常") ? "正常" : "异常";
                        pds.TdxResult = dr.Result;
                        pds.TdxTreatmentTarget = dr.TreatmentTarget;
                        pds.TdxTreatmentSuggestion = dr.TreatmentSuggestion;
                        
                        if (string.IsNullOrEmpty(dr.Result.Trim()))
                        {
                            pds.TdxLife = dr.SelfCheck;
                        }
                        else
                        {
                            pds.TdxLife = GetSelfCheckSuggestion(dr.SelfCheck, "糖尿病随访建议：", "自我监测管理：")[0];
                            pds.TdxSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "糖尿病随访建议：", "自我监测管理：")[1];
                        }

                        if (GlobalData.AGMInfo.HasAGMAbnormal)
                        {
                            pds.TdxHistory = "有 ";
                        }
                        else
                        {
                            pds.TdxHistory = "无";
                        }

                        foreach(CDSSSymptomsInfo agmSym in GlobalData.AGMInfo.listConfirmedSymptoms)
                        {
                            pds.TdxHistory += agmSym.SymptomsName + ";";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.BG) && double.Parse(GlobalData.LabExamInfo.BG) > 11.1)
                        {
                            pds.TdxLabTest += "随机血糖:" + GlobalData.LabExamInfo.BG + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.FBG) && double.Parse(GlobalData.LabExamInfo.FBG) > 7)
                        {
                            pds.TdxLabTest += "空腹血糖:" + GlobalData.LabExamInfo.FBG + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.TWOHPBG) && double.Parse(GlobalData.LabExamInfo.TWOHPBG) > 11.1)
                        {
                            pds.TdxLabTest += "餐后血糖:" + GlobalData.LabExamInfo.TWOHPBG + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.HBA1C) && double.Parse(GlobalData.LabExamInfo.HBA1C) > 6.5)
                        {
                            pds.TdxLabTest += "HbAlc:" + GlobalData.LabExamInfo.HBA1C + "%;\r\n";
                        }

                        if (!string.IsNullOrEmpty(pds.TdxLabTest))
                        {
                            pds.TdxLabTest = pds.TdxLabTest.Trim();
                        }
                    }
                    else if (dr.Name == "脂代谢")
                    {
                        pds.ZdxSummary = dr.Result.Contains("正常") ? "正常" : "异常";
                        pds.ZdxResult = dr.Result.Trim();
                        pds.ZdxTreatmentTarget = dr.TreatmentTarget;

                        string[] suggestions = dr.TreatmentSuggestion.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        string suggestion = string.Empty;

                        foreach (string sugg in suggestions)
                        {
                            if (sugg.Contains("初次服药") || sugg.Contains("达标"))
                            {
                                break;
                            }

                            if (!suggestion.Contains(sugg))
                            {
                                suggestion += sugg;
                            }
                        }

                        pds.ZdxTreatmentSuggestion = suggestion;
                        pds.ZdxLife = string.Empty;
                        if (dr.Result.Contains("脂蛋白") || dr.Result.Contains("胆固醇"))
                        {
                            pds.ZdxLife += "低胆固醇饮食;";
                        }

                        if (dr.Result.Contains("甘油"))
                        {
                            pds.ZdxLife += "低脂饮食;";
                        }

                        pds.ZdxSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "生活方式管理：", "自我监测管理：")[1];

                        if (GlobalData.DyslipidemiaInfo.HasDyslipidemia)
                        {
                            pds.ZdxHistory = "有 ";
                        }
                        else
                        {
                            pds.ZdxHistory = "无";
                        }

                        foreach (CDSSSymptomsInfo dysSym in GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms)
                        {
                            pds.ZdxHistory += dysSym.SymptomsName + ";";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.TC) && double.Parse(GlobalData.LabExamInfo.TC) >= 5.7)
                        {
                            pds.ZdxLabTest += "甘油三酯:" + GlobalData.LabExamInfo.TC + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.TG) && double.Parse(GlobalData.LabExamInfo.TG) >= 1.7)
                        {
                            pds.ZdxLabTest += "总胆固醇:" + GlobalData.LabExamInfo.TG + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.HDLC))
                        {
                            if (GlobalData.PatBasicInfo.PatSex == "男" && double.Parse(GlobalData.LabExamInfo.HDLC) < 1.0)
                            {
                                pds.ZdxLabTest += "HDL-ch:" + GlobalData.LabExamInfo.HDLC + "mmol/L;\r\n";
                            }

                            if (GlobalData.PatBasicInfo.PatSex == "女" && double.Parse(GlobalData.LabExamInfo.HDLC) < 1.3)
                            {
                                pds.ZdxLabTest += "HDL-ch:" + GlobalData.LabExamInfo.HDLC + "mmol/L;\r\n";
                            }
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.LDLC) && double.Parse(GlobalData.LabExamInfo.LDLC) > 3.64)
                        {
                            pds.ZdxLabTest += "LDL-ch:" + GlobalData.LabExamInfo.LDLC + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(pds.ZdxLabTest))
                        {
                            pds.ZdxLabTest = pds.ZdxLabTest.Trim();
                        }
                    }
                    else if (dr.Name == "血压")
                    {
                        pds.XySummary = dr.Result.Contains("正常") ? "正常" : "异常";
                        pds.XyResult = dr.Result;
                        pds.XyTreatmentTarget = dr.TreatmentTarget;
                        pds.XyTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.XyLife = GetSelfCheckSuggestion(dr.SelfCheck, "生活方式管理：", "自我监测管理：")[0];
                        pds.XySelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "生活方式管理：", "自我监测管理：")[1];

                        if (GlobalData.HypertensionInfo.HasHypertension)
                        {
                            pds.XyHistory = "有 ";
                        }
                        else
                        {
                            pds.XyHistory = "无";
                        }

                        foreach (CDSSSymptomsInfo htSym in GlobalData.HypertensionInfo.listHypertensionSymptoms)
                        {
                            pds.XyHistory += htSym.SymptomsName + ";";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.PhysicalInfo.SBP1) && double.Parse(GlobalData.PhysicalInfo.SBP1) >= 130)
                        {
                            pds.XyLabTest += "收缩压:" + GlobalData.PhysicalInfo.SBP1 + "mmHg;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.PhysicalInfo.DBP1) && double.Parse(GlobalData.PhysicalInfo.DBP1) >= 85)
                        {
                            pds.XyLabTest += "舒张压:" + GlobalData.PhysicalInfo.DBP1 + "mmHg;\r\n";
                        }

                        if (!string.IsNullOrEmpty(pds.XyLabTest))
                        {
                            pds.XyLabTest = pds.XyLabTest.Trim();
                        }
                    }
                    else if (dr.Name == "血尿酸")
                    {
                        pds.XnsSummary = dr.Result.Contains("正常") ? "正常" : "异常";
                        pds.XnsResult = dr.Result;
                        pds.XnsTreatmentTarget = dr.TreatmentTarget;
                        pds.XnsTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.XnsLife = GetSelfCheckSuggestion(dr.SelfCheck, "生活方式管理：", "自我监测管理：")[0];
                        pds.XnsSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "生活方式管理：", "自我监测管理：")[1];

                        if (GlobalData.HyperuricemiaInfo.HasHyperuricemia)
                        {
                            pds.XnsHistory = "有 ";
                        }
                        else
                        {
                            pds.XnsHistory = "无";
                        }

                        foreach (CDSSSymptomsInfo HuSym in GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms)
                        {
                            pds.XnsHistory += HuSym.SymptomsName + ";";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.BUA) && double.Parse(GlobalData.LabExamInfo.BUA) > 440)
                        {
                            pds.XnsLabTest += "血尿酸:" + GlobalData.LabExamInfo.BUA + "μmol/L;";
                        }
                    }
                    else if (dr.Name == "肥胖度")
                    {
                        pds.FpdSummary = dr.Result.Contains("正常") ? "正常" : "异常";
                        pds.FpdResult = dr.Result;
                        pds.FpdTreatmentTarget = dr.TreatmentTarget;
                        if (!string.IsNullOrEmpty(GlobalData.PhysicalInfo.Height))
                        {
                            int weight = (int)(25 * double.Parse(GlobalData.PhysicalInfo.Height) * double.Parse(GlobalData.PhysicalInfo.Height) / 10000);
                            pds.FpdTreatmentTarget += "<" + weight.ToString() + "kg ";
                        }
                        pds.FpdTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.FpdLife = "调整饮食，加强运动";
                        pds.FpdSelfCheck = "监测体重1周1次";

                        pds.FpdHistory = "无";
                        
                        if (!string.IsNullOrEmpty(GlobalData.PersonalHistoryInfo.MaxWeight) && !string.IsNullOrEmpty(GlobalData.PhysicalInfo.Height))
                        {
                            double bmi = double.Parse(GlobalData.PersonalHistoryInfo.MaxWeight) * 10000 / double.Parse(GlobalData.PhysicalInfo.Height) / double.Parse(GlobalData.PhysicalInfo.Height);
                            if (bmi >= 25)
                            {
                                pds.FpdHistory = "有 ";
                            }
                        }

                        if (!string.IsNullOrEmpty(GlobalData.PhysicalInfo.Weigh) && !string.IsNullOrEmpty(GlobalData.PhysicalInfo.Height))
                        {
                            double bmi = double.Parse(GlobalData.PhysicalInfo.Weigh) * 10000 / double.Parse(GlobalData.PhysicalInfo.Height) / double.Parse(GlobalData.PhysicalInfo.Height);
                            if (bmi >= 25)
                            {
                                pds.FpdLabTest += "BMI:" + bmi.ToString("00.0") + "kg/m2;";
                            }
                        }

                        if (!string.IsNullOrEmpty(GlobalData.PhysicalInfo.WC))
                        {
                            if (GlobalData.PatBasicInfo.PatSex == "男" && double.Parse(GlobalData.PhysicalInfo.WC) >= 90)
                            {
                                pds.FpdLabTest += "腰围:" + GlobalData.PhysicalInfo.WC + "cm;";
                            }

                            if (GlobalData.PatBasicInfo.PatSex == "女" && double.Parse(GlobalData.PhysicalInfo.WC) >= 80)
                            {
                                pds.FpdLabTest += "腰围:" + GlobalData.PhysicalInfo.WC + "cm;";
                            }
                        }
                        
                    }
                    else if (dr.Name == "蛋白尿")
                    {
                        pds.DbnSummary = dr.Result.Contains("正常") ? "正常" : "异常";
                        pds.DbnResult = dr.Result;
                        pds.DbnTreatmentTarget = dr.TreatmentTarget;
                        pds.DbnTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.DbnLife = GetSelfCheckSuggestion(dr.SelfCheck, "生活方式管理：", "自我监测管理：")[0];
                        pds.DbnSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "生活方式管理：", "自我监测管理：")[1];
                    }
                    else if (dr.Name == "其他")
                    {
                        pds.OtherSummary = dr.Result.Contains("正常") ? "正常" : "异常";
                        pds.OtherResult = dr.Result;
                        pds.OtherTreatmentTarget = dr.TreatmentTarget;
                        pds.OtherTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.OtherLife = GetSelfCheckSuggestion(dr.SelfCheck, "生活方式管理：", "自我监测管理：")[0];
                        pds.OtherSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "生活方式管理：", "自我监测管理：")[1];
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            #endregion

            #region 膳食处方
            pds.DietType = GlobalData.DietSuggestion.DietType;
            pds.TotalEnergy = GlobalData.DietSuggestion.TotalEnergy + " Kcal/日";
            pds.TotalWater = GlobalData.DietSuggestion.TotalWater + " 毫升/日";
            //糖类
            pds.CarboPercent = "糖类食物 " + GlobalData.DietSuggestion.CarboPercent;
            pds.CarboCount = "总 " + GlobalData.DietSuggestion.CarboCount + " 食物份（90大卡/份）";
            pds.CerealCount = GlobalData.DietSuggestion.CerealCount + " 份";
            pds.Fruitcount = GlobalData.DietSuggestion.Fruitcount + " 份";
            //蛋白质类
            pds.ProteinPercent = "蛋白质类食物 " + GlobalData.DietSuggestion.ProteinPercent;
            pds.ProteinCount = "总 " + GlobalData.DietSuggestion.ProteinCount + " 食物份（90大卡/份）";
            pds.DairyCount = GlobalData.DietSuggestion.DairyCount + " 份";
            pds.EggCount = GlobalData.DietSuggestion.EggCount + " 份";
            pds.MeatCount = GlobalData.DietSuggestion.MeatCount + " 份";
            pds.BeanProductCount = GlobalData.DietSuggestion.BeanProductCount + " 份";
            pds.GreenstuffCount = GlobalData.DietSuggestion.GreenstuffCount + " 份";
            //脂肪类
            pds.FatPercent = "脂肪类食物" + GlobalData.DietSuggestion.FatPercent;
            pds.FatCount = "总 " + GlobalData.DietSuggestion.FatCount + " 食物份（90大卡/份）";
            pds.VegetableOilCount = GlobalData.DietSuggestion.VegetableOilCount + " 份";
            pds.OtherFatFoodCount = GlobalData.DietSuggestion.OtherFatFoodCount + " 份";

            pds.LimitedFood = GlobalData.DietSuggestion.LimitedFood;
            pds.LimitedFood = GlobalData.DietSuggestion.AvoidFood;
            #endregion

            #region 运动建议
            pds.ExAim = GlobalData.ExerciseSuggestion.ExerciseTarget;
            pds.ExEnergyConsum = GlobalData.ExerciseSuggestion.EnergyCost;
            pds.ExPlan1 = GlobalData.ExerciseSuggestion.NoIntensityExercise;
            pds.ExChoice1 = GlobalData.ExerciseSuggestion.NoIntensityExerciseItems;
            pds.ExPlan2 = GlobalData.ExerciseSuggestion.LowIntensityExercise;
            pds.ExChoice2 = GlobalData.ExerciseSuggestion.LowIntensityExerciseItems;
            pds.ExPlan3 = GlobalData.ExerciseSuggestion.MiddleIntensityExercise;
            pds.ExChoice3 = GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems;
            pds.ExPlan4 = GlobalData.ExerciseSuggestion.HighIntensityExercise;
            pds.ExChoice4 = GlobalData.ExerciseSuggestion.HighIntensityExerciseItems;
            #endregion
        }

        /// <summary>
        /// 打印快捷键，调用打印对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
            {
                reportViewer1.PrintDialog();
            }
        }
        /**********************************************************************
         * 添加人：XY；
         * 添加时间：20081223；
         * 添加说明：响应MainForm中的快捷键打印，弹出打印对话框；
         * 添加部分：“快捷键弹出打印对话框功能”。
         *********************************************************************/
        /// <summary>
        /// 快捷键弹出打印对话框功能
        /// </summary>
        public void ShortCutBtn()
        {
            this.reportViewer1.PrintDialog();
        }

        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printClass.PrintDialog(this.reportViewer1.LocalReport);
        }
        /// <summary>
        /// 页面设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SetUp_Click(object sender, EventArgs e)
        {
            printClass.PageSetUp();
        }

        /// <summary>
        /// 打印预览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPriview_Click(object sender, EventArgs e)
        {
            printClass.PrintView(this.reportViewer1.LocalReport);            
        }

    }
}