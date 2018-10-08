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
         * ����ˣ�XY
         * ���ʱ�䣺20081222
         * ���˵������ӦMainForm�еĴ�ӡ��ť����ʼ����ӡ����
         * ��Ӳ��֣�����ʼ����ӡ��������
         **********************************************************************/
        /// <summary>
        /// ��ʼ����ӡ������
        /// </summary>
        public void ShowPrtReport()
        {
            //�����ӡ����Ĺ��б�����Revised by Jyl,081222
            PatInfo.pds = new PrintDataSource();
            InitPrintDataSource(PatInfo.pds);
            this.PrintSourceBindingSource.DataSource = PatInfo.pds;

            //�����ӱ����ܣ���֧�ָ��ӱ�񲼾֣�Added By ZQY,090325
            this.reportViewer1.LocalReport.SubreportProcessing +=
            new SubreportProcessingEventHandler(SubreportProcessingEventHandler);

            this.reportViewer1.RefreshReport();
        }
        //�����ӱ����ܣ���֧�ָ��ӱ�񲼾֣�Added By ZQY,090325
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
        /// ��ʼ����ӡ����
        /// </summary>
        /// <param name="pds">��ӡ��������Դ</param>
        private void InitPrintDataSource(PrintDataSource pds)
        {
            #region һ����Ϣ
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

            #region ��Ͻ���
            foreach (CDSSOneDiseaseDiagnosedResult dr in GlobalData.DiagnosedResult.DiseaseDiagnosedResultList)
            {
                //if (!string.IsNullOrEmpty(dr.Result.Trim()))
                {
                    if (dr.Name == "�Ǵ�л")
                    {
                        pds.TdxSummary = dr.Result.Contains("����") ? "����" : "�쳣";
                        pds.TdxResult = dr.Result;
                        pds.TdxTreatmentTarget = dr.TreatmentTarget;
                        pds.TdxTreatmentSuggestion = dr.TreatmentSuggestion;
                        
                        if (string.IsNullOrEmpty(dr.Result.Trim()))
                        {
                            pds.TdxLife = dr.SelfCheck;
                        }
                        else
                        {
                            pds.TdxLife = GetSelfCheckSuggestion(dr.SelfCheck, "������ý��飺", "���Ҽ�����")[0];
                            pds.TdxSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "������ý��飺", "���Ҽ�����")[1];
                        }

                        if (GlobalData.AGMInfo.HasAGMAbnormal)
                        {
                            pds.TdxHistory = "�� ";
                        }
                        else
                        {
                            pds.TdxHistory = "��";
                        }

                        foreach(CDSSSymptomsInfo agmSym in GlobalData.AGMInfo.listConfirmedSymptoms)
                        {
                            pds.TdxHistory += agmSym.SymptomsName + ";";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.BG) && double.Parse(GlobalData.LabExamInfo.BG) > 11.1)
                        {
                            pds.TdxLabTest += "���Ѫ��:" + GlobalData.LabExamInfo.BG + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.FBG) && double.Parse(GlobalData.LabExamInfo.FBG) > 7)
                        {
                            pds.TdxLabTest += "�ո�Ѫ��:" + GlobalData.LabExamInfo.FBG + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.TWOHPBG) && double.Parse(GlobalData.LabExamInfo.TWOHPBG) > 11.1)
                        {
                            pds.TdxLabTest += "�ͺ�Ѫ��:" + GlobalData.LabExamInfo.TWOHPBG + "mmol/L;\r\n";
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
                    else if (dr.Name == "֬��л")
                    {
                        pds.ZdxSummary = dr.Result.Contains("����") ? "����" : "�쳣";
                        pds.ZdxResult = dr.Result.Trim();
                        pds.ZdxTreatmentTarget = dr.TreatmentTarget;

                        string[] suggestions = dr.TreatmentSuggestion.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        string suggestion = string.Empty;

                        foreach (string sugg in suggestions)
                        {
                            if (sugg.Contains("���η�ҩ") || sugg.Contains("���"))
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
                        if (dr.Result.Contains("֬����") || dr.Result.Contains("���̴�"))
                        {
                            pds.ZdxLife += "�͵��̴���ʳ;";
                        }

                        if (dr.Result.Contains("����"))
                        {
                            pds.ZdxLife += "��֬��ʳ;";
                        }

                        pds.ZdxSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "���ʽ����", "���Ҽ�����")[1];

                        if (GlobalData.DyslipidemiaInfo.HasDyslipidemia)
                        {
                            pds.ZdxHistory = "�� ";
                        }
                        else
                        {
                            pds.ZdxHistory = "��";
                        }

                        foreach (CDSSSymptomsInfo dysSym in GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms)
                        {
                            pds.ZdxHistory += dysSym.SymptomsName + ";";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.TC) && double.Parse(GlobalData.LabExamInfo.TC) >= 5.7)
                        {
                            pds.ZdxLabTest += "��������:" + GlobalData.LabExamInfo.TC + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.TG) && double.Parse(GlobalData.LabExamInfo.TG) >= 1.7)
                        {
                            pds.ZdxLabTest += "�ܵ��̴�:" + GlobalData.LabExamInfo.TG + "mmol/L;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.HDLC))
                        {
                            if (GlobalData.PatBasicInfo.PatSex == "��" && double.Parse(GlobalData.LabExamInfo.HDLC) < 1.0)
                            {
                                pds.ZdxLabTest += "HDL-ch:" + GlobalData.LabExamInfo.HDLC + "mmol/L;\r\n";
                            }

                            if (GlobalData.PatBasicInfo.PatSex == "Ů" && double.Parse(GlobalData.LabExamInfo.HDLC) < 1.3)
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
                    else if (dr.Name == "Ѫѹ")
                    {
                        pds.XySummary = dr.Result.Contains("����") ? "����" : "�쳣";
                        pds.XyResult = dr.Result;
                        pds.XyTreatmentTarget = dr.TreatmentTarget;
                        pds.XyTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.XyLife = GetSelfCheckSuggestion(dr.SelfCheck, "���ʽ����", "���Ҽ�����")[0];
                        pds.XySelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "���ʽ����", "���Ҽ�����")[1];

                        if (GlobalData.HypertensionInfo.HasHypertension)
                        {
                            pds.XyHistory = "�� ";
                        }
                        else
                        {
                            pds.XyHistory = "��";
                        }

                        foreach (CDSSSymptomsInfo htSym in GlobalData.HypertensionInfo.listHypertensionSymptoms)
                        {
                            pds.XyHistory += htSym.SymptomsName + ";";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.PhysicalInfo.SBP1) && double.Parse(GlobalData.PhysicalInfo.SBP1) >= 130)
                        {
                            pds.XyLabTest += "����ѹ:" + GlobalData.PhysicalInfo.SBP1 + "mmHg;\r\n";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.PhysicalInfo.DBP1) && double.Parse(GlobalData.PhysicalInfo.DBP1) >= 85)
                        {
                            pds.XyLabTest += "����ѹ:" + GlobalData.PhysicalInfo.DBP1 + "mmHg;\r\n";
                        }

                        if (!string.IsNullOrEmpty(pds.XyLabTest))
                        {
                            pds.XyLabTest = pds.XyLabTest.Trim();
                        }
                    }
                    else if (dr.Name == "Ѫ����")
                    {
                        pds.XnsSummary = dr.Result.Contains("����") ? "����" : "�쳣";
                        pds.XnsResult = dr.Result;
                        pds.XnsTreatmentTarget = dr.TreatmentTarget;
                        pds.XnsTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.XnsLife = GetSelfCheckSuggestion(dr.SelfCheck, "���ʽ����", "���Ҽ�����")[0];
                        pds.XnsSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "���ʽ����", "���Ҽ�����")[1];

                        if (GlobalData.HyperuricemiaInfo.HasHyperuricemia)
                        {
                            pds.XnsHistory = "�� ";
                        }
                        else
                        {
                            pds.XnsHistory = "��";
                        }

                        foreach (CDSSSymptomsInfo HuSym in GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms)
                        {
                            pds.XnsHistory += HuSym.SymptomsName + ";";
                        }

                        if (!string.IsNullOrEmpty(GlobalData.LabExamInfo.BUA) && double.Parse(GlobalData.LabExamInfo.BUA) > 440)
                        {
                            pds.XnsLabTest += "Ѫ����:" + GlobalData.LabExamInfo.BUA + "��mol/L;";
                        }
                    }
                    else if (dr.Name == "���ֶ�")
                    {
                        pds.FpdSummary = dr.Result.Contains("����") ? "����" : "�쳣";
                        pds.FpdResult = dr.Result;
                        pds.FpdTreatmentTarget = dr.TreatmentTarget;
                        if (!string.IsNullOrEmpty(GlobalData.PhysicalInfo.Height))
                        {
                            int weight = (int)(25 * double.Parse(GlobalData.PhysicalInfo.Height) * double.Parse(GlobalData.PhysicalInfo.Height) / 10000);
                            pds.FpdTreatmentTarget += "<" + weight.ToString() + "kg ";
                        }
                        pds.FpdTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.FpdLife = "������ʳ����ǿ�˶�";
                        pds.FpdSelfCheck = "�������1��1��";

                        pds.FpdHistory = "��";
                        
                        if (!string.IsNullOrEmpty(GlobalData.PersonalHistoryInfo.MaxWeight) && !string.IsNullOrEmpty(GlobalData.PhysicalInfo.Height))
                        {
                            double bmi = double.Parse(GlobalData.PersonalHistoryInfo.MaxWeight) * 10000 / double.Parse(GlobalData.PhysicalInfo.Height) / double.Parse(GlobalData.PhysicalInfo.Height);
                            if (bmi >= 25)
                            {
                                pds.FpdHistory = "�� ";
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
                            if (GlobalData.PatBasicInfo.PatSex == "��" && double.Parse(GlobalData.PhysicalInfo.WC) >= 90)
                            {
                                pds.FpdLabTest += "��Χ:" + GlobalData.PhysicalInfo.WC + "cm;";
                            }

                            if (GlobalData.PatBasicInfo.PatSex == "Ů" && double.Parse(GlobalData.PhysicalInfo.WC) >= 80)
                            {
                                pds.FpdLabTest += "��Χ:" + GlobalData.PhysicalInfo.WC + "cm;";
                            }
                        }
                        
                    }
                    else if (dr.Name == "������")
                    {
                        pds.DbnSummary = dr.Result.Contains("����") ? "����" : "�쳣";
                        pds.DbnResult = dr.Result;
                        pds.DbnTreatmentTarget = dr.TreatmentTarget;
                        pds.DbnTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.DbnLife = GetSelfCheckSuggestion(dr.SelfCheck, "���ʽ����", "���Ҽ�����")[0];
                        pds.DbnSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "���ʽ����", "���Ҽ�����")[1];
                    }
                    else if (dr.Name == "����")
                    {
                        pds.OtherSummary = dr.Result.Contains("����") ? "����" : "�쳣";
                        pds.OtherResult = dr.Result;
                        pds.OtherTreatmentTarget = dr.TreatmentTarget;
                        pds.OtherTreatmentSuggestion = dr.TreatmentSuggestion;
                        pds.OtherLife = GetSelfCheckSuggestion(dr.SelfCheck, "���ʽ����", "���Ҽ�����")[0];
                        pds.OtherSelfCheck = GetSelfCheckSuggestion(dr.SelfCheck, "���ʽ����", "���Ҽ�����")[1];
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            #endregion

            #region ��ʳ����
            pds.DietType = GlobalData.DietSuggestion.DietType;
            pds.TotalEnergy = GlobalData.DietSuggestion.TotalEnergy + " Kcal/��";
            pds.TotalWater = GlobalData.DietSuggestion.TotalWater + " ����/��";
            //����
            pds.CarboPercent = "����ʳ�� " + GlobalData.DietSuggestion.CarboPercent;
            pds.CarboCount = "�� " + GlobalData.DietSuggestion.CarboCount + " ʳ��ݣ�90��/�ݣ�";
            pds.CerealCount = GlobalData.DietSuggestion.CerealCount + " ��";
            pds.Fruitcount = GlobalData.DietSuggestion.Fruitcount + " ��";
            //��������
            pds.ProteinPercent = "��������ʳ�� " + GlobalData.DietSuggestion.ProteinPercent;
            pds.ProteinCount = "�� " + GlobalData.DietSuggestion.ProteinCount + " ʳ��ݣ�90��/�ݣ�";
            pds.DairyCount = GlobalData.DietSuggestion.DairyCount + " ��";
            pds.EggCount = GlobalData.DietSuggestion.EggCount + " ��";
            pds.MeatCount = GlobalData.DietSuggestion.MeatCount + " ��";
            pds.BeanProductCount = GlobalData.DietSuggestion.BeanProductCount + " ��";
            pds.GreenstuffCount = GlobalData.DietSuggestion.GreenstuffCount + " ��";
            //֬����
            pds.FatPercent = "֬����ʳ��" + GlobalData.DietSuggestion.FatPercent;
            pds.FatCount = "�� " + GlobalData.DietSuggestion.FatCount + " ʳ��ݣ�90��/�ݣ�";
            pds.VegetableOilCount = GlobalData.DietSuggestion.VegetableOilCount + " ��";
            pds.OtherFatFoodCount = GlobalData.DietSuggestion.OtherFatFoodCount + " ��";

            pds.LimitedFood = GlobalData.DietSuggestion.LimitedFood;
            pds.LimitedFood = GlobalData.DietSuggestion.AvoidFood;
            #endregion

            #region �˶�����
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
        /// ��ӡ��ݼ������ô�ӡ�Ի���
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
         * ����ˣ�XY��
         * ���ʱ�䣺20081223��
         * ���˵������ӦMainForm�еĿ�ݼ���ӡ��������ӡ�Ի���
         * ��Ӳ��֣�����ݼ�������ӡ�Ի����ܡ���
         *********************************************************************/
        /// <summary>
        /// ��ݼ�������ӡ�Ի�����
        /// </summary>
        public void ShortCutBtn()
        {
            this.reportViewer1.PrintDialog();
        }

        /// <summary>
        /// ��ӡ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printClass.PrintDialog(this.reportViewer1.LocalReport);
        }
        /// <summary>
        /// ҳ�����ð�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SetUp_Click(object sender, EventArgs e)
        {
            printClass.PageSetUp();
        }

        /// <summary>
        /// ��ӡԤ����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPriview_Click(object sender, EventArgs e)
        {
            printClass.PrintView(this.reportViewer1.LocalReport);            
        }

    }
}