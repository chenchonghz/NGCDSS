using System;
using System.Collections.Generic;
using System.Text;
using CDSSvMRDataDef;
using CDSSSystemData;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Configuration;


namespace CDSSFunction
{
    class DataMapper
    {
        /// <summary>
        /// DataModel code与SystemData映射，并对oIEvMRInput进行赋值
        /// </summary>
        /// <param name="oIEvMRInput"></param>
        /// <returns></returns>
        public static bool MapDataToFact(ref vMRClsDef.IEvMRInput oIEvMRInput)
        {
            //TODO 数据映射具体实现
            for(int i = 0; i < oIEvMRInput.lstInputDataModel.Count; i++)
            {
                for(int j = 0; j < oIEvMRInput.lstInputDataModel[i].lstDataModel.Count; j++)
                {
                    string strStandDataCode
                        = oIEvMRInput.lstInputDataModel[i].lstDataModel[j].strDataCode;

                    //string strClassName 
                    //    = MappingToClassFromXml.ObtainClassNameWithDataCode(strStandDataCode);
                    //if (strClassName != string.Empty)
                    //{
                    //    object objClassDataValue = DynamicMapperToSystemData.GetMapperValue(strClassName);
                    //    if (objClassDataValue != null)
                    //    {
                    //        oIEvMRInput.lstInputDataModel[i].lstDataModel[j].strDataValue
                    //            = objClassDataValue.ToString();
                    //    }
                    //    else
                    //    {
                    //        oIEvMRInput.lstInputDataModel[i].lstDataModel[j].strDataValue
                    //        = "NULL";
                    //    }

                    //}
                    //else
                    //{
                    //    oIEvMRInput.lstInputDataModel[i].lstDataModel[j].strDataValue = "NULL";
                    //}

                    oIEvMRInput.lstInputDataModel[i].lstDataModel[j].strDataValue
                    = TempDataMap.ObtainDataValueWithDataCode(strStandDataCode);
                }
            }
            
            return true;
        }

        public static void MapIEOutputToUI(vMRClsDef.IEvMRInput oIEvMRInput, vMRClsDef.IEvMROutput oIEvMROutput)
        {
            //TODO IE输出结果映射，供界面显示
            foreach(vMRClsDef.OutputInfo oOutputInfo in oIEvMROutput.lstOutputInfo)
            {
                int i;
                for ( i = 0; i < oIEvMRInput.lstInputDataModel.Count; i++)
                {
                    if(oOutputInfo.oTriggeringEvent.oEvent.strEventCNName 
                        == oIEvMRInput.lstInputDataModel[i].oTriggeringEvent.oEvent.strEventCNName)
                    {
                        break;
                    }
                }
                    FormatResult(oIEvMRInput.lstInputDataModel[i], oOutputInfo);
            }
        }

        public static void FormatResult(vMRClsDef.InputDataModel oIEInputDataModel, vMRClsDef.OutputInfo oOutputInfo)
        {
            switch (oOutputInfo.oTriggeringEvent.m_emInferenceResultType)
            {
                case vMRClsDef.EnumInferenceResultType.DIAGNOSIS:
                    FormatDiagnosiResult(oIEInputDataModel, oOutputInfo);
                    FormatExplanation(oIEInputDataModel, oOutputInfo);
                    break;
                case  vMRClsDef.EnumInferenceResultType.MSEVALUATION:
                    FormatMSEvaluationResult(oOutputInfo);
                    break;
                case vMRClsDef.EnumInferenceResultType.THERAPY:
                    FormatTherpayResult(oIEInputDataModel, oOutputInfo);
                    break;
                case vMRClsDef.EnumInferenceResultType.SELFMONITOR:
                    FormatSelfMonitorResult(oIEInputDataModel, oOutputInfo);
                    break;
                case vMRClsDef.EnumInferenceResultType.RISKEVALUATION:
                    FormatMSRiskEvaluationResult(oOutputInfo);
                    break;
                case vMRClsDef.EnumInferenceResultType.DIETARY:
                    FormatDietary(oIEInputDataModel, oOutputInfo);
                    break;
                case vMRClsDef.EnumInferenceResultType.PHYSICALACTIVITY:
                    FormatPhysicalActivity(oIEInputDataModel, oOutputInfo);
                    break;
                default:
                    break;
            }
        }

        public static int DiseaseInfoExisted(vMRClsDef.OutputInfo oOutputInfo)
        {
            for (int i = 0; i < GlobalData.DiagnosedResult.DiseaseDiagnosedResultList.Count; i++)
            {
                if (GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].Name == oOutputInfo.oTriggeringEvent.oDisease.strDiseaseCNName)
                {
                    return i;
                }
            }

            CDSSOneDiseaseDiagnosedResult oOneDiseaseDiagnosedResult = new CDSSOneDiseaseDiagnosedResult();
            oOneDiseaseDiagnosedResult.Name = oOutputInfo.oTriggeringEvent.oDisease.strDiseaseCNName;
            GlobalData.DiagnosedResult.DiseaseDiagnosedResultList.Add(oOneDiseaseDiagnosedResult);

            return GlobalData.DiagnosedResult.DiseaseDiagnosedResultList.Count - 1;
        }

        public static void FormatDiagnosiResult(vMRClsDef.InputDataModel oIEInputDataModel, vMRClsDef.OutputInfo oOutputInfo)
        {
            int iResultIndex = DiseaseInfoExisted(oOutputInfo);

            string result = string.Empty;
            string tmpresult = string.Empty;
            for (int i = 0; i < oOutputInfo.oInference.lstStructedInferMessage.Count; i++)
            {
                tmpresult = oOutputInfo.oInference.lstStructedInferMessage[i].strDataValue; 
   
                //BugDB00005720 revised by wbf 2009-03-26
                if(oOutputInfo.oInference.lstStructedInferMessage[i].strDataName == "Fat_Diagnose")
                {
                    if(tmpresult == "NO")
                    {
                        result += "正常 ";
                    }
                    //else
                    //{
                    //    result += MapStructedInfoToCN(tmpresult) + " ";  
                    //}
                } 
                if (tmpresult != "")
                    result = result + MapStructedInfoToCN(tmpresult) + " ";                                                          
            }
            GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[iResultIndex].Result = result;
            FormatShortDataInfo(oIEInputDataModel, oOutputInfo, ref GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[iResultIndex].DataNeeded);
        }

        public static void FormatMSEvaluationResult(vMRClsDef.OutputInfo oOutputInfo)
        {
            if(oOutputInfo.oInference.lstStructedInferMessage.Count == 1)
            {
                GlobalData.DiagnosedResult.HasMS = MapStructedInfoToCN(oOutputInfo.oInference.lstStructedInferMessage[0].strDataValue);         
            }
        }

        public static void FormatMSRiskEvaluationResult(vMRClsDef.OutputInfo oOutputInfo)
        {
            GlobalData.DiagnosedResult.RiskDegree = string.Empty;
            GlobalData.DiagnosedResult.RiskDegreeCode = string.Empty;
            for(int i =0; i < oOutputInfo.oInference.lstStructedInferMessage.Count; i++ )
            {
                if(oOutputInfo.oInference.lstStructedInferMessage[i].strDataName == "risk_score")
                {
                    if (oOutputInfo.oInference.lstStructedInferMessage[i].strDataValue != "NULL")
                    {
                        GlobalData.DiagnosedResult.RiskDegreeCode = oOutputInfo.oInference.lstStructedInferMessage[i].strDataValue;
                    }
                }
                else if(oOutputInfo.oInference.lstStructedInferMessage[i].strDataName == "MSRiskDegree")
                {
                    GlobalData.DiagnosedResult.RiskDegree = MapStructedInfoToCN(oOutputInfo.oInference.lstStructedInferMessage[i].strDataValue);
                }
            }
        }

        public static string MapStructedInfoToCN(string Result)
        {
            switch (Result)
            {
                case "T1DM":
                    return "1型糖尿病";
                case "T2DM":
                    return "2型糖尿病";
                case "DM":
                    return "有糖尿病";
                case "IGT":
                    return "IGT(糖耐量低减)";
                case "IFG":
                    return "IFG(空腹血糖受损)";
                case "IGR":
                    return "IGR，糖耐量低减，并且空腹血糖受损。";
                case "DM_NoneType":
                    return "有糖尿病，但无法分型。";
                case "DM_Normal":
                    return "糖代谢正常。";
                case "Dyslipidemia_TC":
                    return "高胆固醇血症";
                case "Dyslipidemia_TG":
                    return "高甘油三脂血症";
                case "Dyslipidemia_HDLch":
                    return "低高密度脂蛋白血症";
                case "Dyslipidemia_LDLch":
                    return "高低密度脂蛋白血症";
                case "Dyslipidemia_Normal1":
                    return "血脂正常";
                case "Fat":
                    return "肥胖";
                case "Hypertension":
                    return "高血压";
                case "First_Stage":
                    return "高血压1级";
                case "Second_Stage":
                    return "高血压2级";
                case "Third_Stage":
                    return "高血压3级";
                case "pre_Stage":
                    return "高血压前期";
                case "risk_medium":
                    return "高血压危险度:中危";
                case "risk_low":
                    return "高血压危险度:低危";
                case "risk_high":
                    return "高血压危险度:高危";
                case "risk_very_high":
                    return "高血压危险度:极高危";
                case "Hypertension_Normal":
                    return "血压正常";
                case "primary":
                    return "原发";
                case "secondary":
                    return "继发";
                case "Gouty":
                    return "痛风";
                case "Hyperuricaemia":
                    return "高尿酸血症";
                case "HUAcute":
                    return "急性痛风";
                case "HUA_Normal":
                    return "血尿酸正常";
                case "LOW":
                    return "低危";
                case "MIDDLE":
                    return "中危";
                case "HIGH":
                    return "高危";
                case "VERYHIGH":
                    return "极高危";
                case "NO":
                    return " ";
                case "Metabolic_Syndrome":
                    return "有";
                default:
                    return "";
            }
        }        

        public static void FormatTherpayResult(vMRClsDef.InputDataModel oIEInputDataModel, vMRClsDef.OutputInfo oOutputInfo)
        {
            int iResultIndex = DiseaseInfoExisted(oOutputInfo);

            string strResult = string.Empty;
            if (oOutputInfo.oInference.lstUnstructedInferenceMessage.Count != 0)
            {
                for (int i = 0; i < oOutputInfo.oInference.lstUnstructedInferenceMessage.Count; i++)
                {
                    strResult += oOutputInfo.oInference.lstUnstructedInferenceMessage[i].strUnStructMessage + Environment.NewLine;
                }
            }

            //if (oOutputInfo.oInference.lstStructedInferMessage.Count != 0)
            //{
            //    string result = string.Empty;
            //    string tmpresult = string.Empty;
            //    for (int i = 0; i < oOutputInfo.oInference.lstStructedInferMessage.Count; i++)
            //    {
            //        result = oOutputInfo.oInference.lstStructedInferMessage[i].strDataCNName;
            //        tmpresult = oOutputInfo.oInference.lstStructedInferMessage[i].strDataValue;
            //        if (tmpresult != "NULL")
            //        {
            //            strResult += result + MapStructedInfoToCN(tmpresult) + "\n";
            //        }
            //    }
            //}

            GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[iResultIndex].TreatmentSuggestion += strResult;
            //BugDB00005702 revised by wbf 2009-03-25 
            FormatShortDataInfo(oIEInputDataModel, oOutputInfo, ref GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[iResultIndex].DataNeeded);
        }

        public static void FormatSelfMonitorResult(vMRClsDef.InputDataModel oIEInputDataModel, vMRClsDef.OutputInfo oOutputInfo)
        {
            int iResultIndex = DiseaseInfoExisted(oOutputInfo);

            string strResult = string.Empty;
            if (oOutputInfo.oInference.lstUnstructedInferenceMessage.Count != 0)
            {
                for (int i = 0; i < oOutputInfo.oInference.lstUnstructedInferenceMessage.Count; i++)
                {
                    strResult += oOutputInfo.oInference.lstUnstructedInferenceMessage[i].strUnStructMessage + Environment.NewLine;
                }
            }

            GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[iResultIndex].SelfCheck = strResult;
            FormatShortDataInfo(oIEInputDataModel, oOutputInfo, ref GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[iResultIndex].DataNeeded);
        }

        public static void FormatExplanation(vMRClsDef.InputDataModel oIEInputDataModel, vMRClsDef.OutputInfo oOutputInfo)
        {
            int iResultIndex = DiseaseInfoExisted(oOutputInfo);

            string strResult = string.Empty;
            for (int i = 0; i < oOutputInfo.oExplanation.lstClipsInterpretation.Count; i++)
            {
                strResult
                    += GetInterpData(oIEInputDataModel, oOutputInfo.oExplanation.lstClipsInterpretation[i], oOutputInfo)
                    + Environment.NewLine
                    + oOutputInfo.oExplanation.lstClipsInterpretation[i].strInterpretationIndex + Environment.NewLine
                    + GetRecomm(oOutputInfo.oExplanation.lstClipsInterpretation[i].lstRecomm) + Environment.NewLine;
            }
            GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[iResultIndex].DiagnosisSteps = strResult;
        }

        public static string GetInterpData(vMRClsDef.InputDataModel oIEInputDataModel, vMRClsDef.CLIPSInterpretation oClipsInterpretation, vMRClsDef.OutputInfo oOutputInfo)
        {
            string strInterpData = string.Empty;
            foreach (string strFactUsed in oClipsInterpretation.lstFactUsed)
            {
                string strFactResult = String.Empty;
                string strFactCNName = String.Empty;

                int i;
                for (i = 0; i < oIEInputDataModel.lstDataModel.Count; i++)
                {
                    if(strFactUsed == oIEInputDataModel.lstDataModel[i].strDataName)
                    {
                        strFactCNName = oIEInputDataModel.lstDataModel[i].strDataCNName;
                        strFactResult = GetResultCNValueByEnValue(oIEInputDataModel.lstDataModel[i].strDataValue);

                        if(strFactResult == "NULL")
                        {
                            foreach(StructedIEResult.StructedConclude oStructedConclude in StructedIEResult.lstStructedConclude)
                            {
                                if(oStructedConclude.oEventModel.strEventCNName == oOutputInfo.oTriggeringEvent.oEvent.strEventCNName)
                                {
                                    foreach(vMRClsDef.DataModel oDataModel in oStructedConclude.lstConclude)
                                    {
                                        if(oDataModel.strDataName == strFactUsed)
                                        {
                                            strFactResult = GetResultCNValueByEnValue(oDataModel.strDataValue);
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                //BugDB00005681 revised by wbf 2009-03-25
                if(strFactResult == "NULL")
                {
                    strFactResult = "不详";
                }
                strInterpData += strFactCNName + "  " + strFactResult + Environment.NewLine;
            }

            return strInterpData;
        }

        private static string GetResultCNValueByEnValue(string strDataENValue)
        {
            switch (strDataENValue)
            {
                case "female":
                    return "女";
                case "male":
                    return "男";
                //case CDSSProxyCommonDef.CONST_STRING_POSITIVE:
                //    return "阳性";
                //case CDSSProxyCommonDef.CONST_STRING_NEGATIVE:
                //    return "阴性";
                case "YES":
                    return "是";
                case "NO":
                    return "否";
                case "UNKNOWN":
                    return "不详";
                case "Normal":
                    return "正常";
                case "Abnormal":
                    return "异常";
                //add by wbf 081231 Bug C25
                case "First_Stage":
                    return "1级";
                case "Second_Stage":
                    return "2级";
                case "Third_Stage":
                    return "3级";
                case "pre_Stage":
                    return "前期";
                default:
                    return strDataENValue;
            }
        }

        public static string GetRecomm(List<string> lstRecomm)
        {
            //TODO:
            string strRecommondation;
            if (lstRecomm.Count > 0)
            {
                strRecommondation = "=====>" + Environment.NewLine;
            }
            else
            {
                strRecommondation = "";
            }

            foreach (string strRecomm in lstRecomm)
            {
                strRecommondation += strRecomm + Environment.NewLine;
            }
            return strRecommondation;
        }

        public static void FormatTarget()
        {
            for (int i = 0; i < GlobalData.DiagnosedResult.DiseaseDiagnosedResultList.Count; i++)
            {
                GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].TreatmentTarget = GetMSTarget(GlobalData.DiagnosedResult.DiseaseDiagnosedResultList[i].Name);
            }
        }

        public static string GetMSTarget(string strDiseaseName)
        {
            switch (strDiseaseName)
            {
                case "糖代谢":
                    //BugDB00005672 revised by wbf 2009-03-26
                    return "空腹:<6.2mmol/L;餐后2h:<8.0mmol/L;HbA1c:<6.5%";
                case "脂代谢":
                    return "低密度脂蛋白胆固醇:<2.6mmol/L;甘油三酯:<1.5mmol/L";
                case "血压":
                    return "收缩压:<130 mmHg;舒张压:<80mmHg";
                case "血尿酸":
                    return "血尿酸:<400umol/L";
                case "肥胖度":
                    return "1.近期目标:减轻5%体重; 2.长期目标:控制体重";
                case "蛋白尿":
                    return "减少白蛋白尿:<30mg/g";
                case "其他":
                    return "降低血液高凝状态";
                default:
                    return string.Empty;

            }
        }

        public static void FormatDietary(vMRClsDef.InputDataModel oIEInputDataModel, vMRClsDef.OutputInfo oOutputInfo)
        {
            string MaxWater = string.Empty;
            string MinWater = string.Empty;
            GlobalData.DietSuggestion.Clear();

            FormatShortDataInfo(oIEInputDataModel, oOutputInfo, ref GlobalData.DietSuggestion.DataNeeded);

            for (int i = 0; i < oOutputInfo.oInference.lstStructedInferMessage.Count; i++ )
            {
                if (oOutputInfo.oInference.lstStructedInferMessage[i].strDataValue == "NULL")
                    oOutputInfo.oInference.lstStructedInferMessage[i].strDataValue = string.Empty;
            }

                if (GlobalData.DietSuggestion.DataNeeded == string.Empty)
                {
                    foreach (vMRClsDef.DataModel oDietray in oOutputInfo.oInference.lstStructedInferMessage)
                    {
                        if (oDietray.strDataName == "DietaryType")
                        {
                            GlobalData.DietSuggestion.DietType = FormatDietaryType(oDietray.strDataValue);
                        }
                        else if (oDietray.strDataName == "TKcal")
                        {
                            GlobalData.DietSuggestion.TotalEnergy = oDietray.strDataValue;
                        }
                        //else if (oDietray.strDataName == "FatAmount")
                        //{
                        //    GlobalData.DietSuggestion.FatCount = oDietray.strDataValue;
                        //}
                        //else if (oDietray.strDataName == "CarboAmount")
                        //{
                        //    GlobalData.DietSuggestion.CarboCount = oDietray.strDataValue;
                        //}
                        //else if (oDietray.strDataName == "MeatAmount")
                        //{
                        //    GlobalData.DietSuggestion.MeatCount = oDietray.strDataValue;
                        //}
                        else if (oDietray.strDataName == "MilkAmount")
                        {
                            GlobalData.DietSuggestion.DairyCount = "1.5";
                        }
                        else if (oDietray.strDataName == "EggAmount")
                        {
                            GlobalData.DietSuggestion.EggCount = oDietray.strDataValue;
                        }
                        else if (oDietray.strDataName == "FatRatio")
                        {
                            GlobalData.DietSuggestion.FatPercent = oDietray.strDataValue + "%";
                        }
                        else if (oDietray.strDataName == "CarboRatio")
                        {
                            GlobalData.DietSuggestion.CarboPercent = oDietray.strDataValue + "%";
                        }
                        else if (oDietray.strDataName == "ProteinRatio")
                        {
                            GlobalData.DietSuggestion.ProteinPercent = oDietray.strDataValue + "%";
                        }
                        else if (oDietray.strDataName == "MainFood")
                        {
                            //GlobalData.DietSuggestion.CerealCount = oDietray.strDataValue;
                            try
                            {
                                GlobalData.DietSuggestion.CerealCount
                                    = (System.Math.Ceiling(Convert.ToDouble(oDietray.strDataValue))).ToString();
                            }
                            catch
                            {
                                GlobalData.DietSuggestion.CerealCount = string.Empty;
                            }
                        }
                        else if (oDietray.strDataName == "Fruit")
                        {
                            GlobalData.DietSuggestion.Fruitcount = oDietray.strDataValue;
                        }
                        else if (oDietray.strDataName == "Vegetable")
                        {
                            GlobalData.DietSuggestion.GreenstuffCount = oDietray.strDataValue;
                        }
                        else if (oDietray.strDataName == "Meat")
                        {
                            GlobalData.DietSuggestion.MeatCount = oDietray.strDataValue;
                        }
                        else if (oDietray.strDataName == "TotalFat")
                        {
                            GlobalData.DietSuggestion.FatCount = oDietray.strDataValue;
                        }
                        else if (oDietray.strDataName == "Oil")
                        {
                            GlobalData.DietSuggestion.OtherFatFoodCount = "0.5";
                            try
                            {
                                GlobalData.DietSuggestion.VegetableOilCount = Convert.ToString(Convert.ToDouble(oDietray.strDataValue) - 0.5);
                            }
                            catch
                            {
                                GlobalData.DietSuggestion.VegetableOilCount = string.Empty;
                            }

                        }
                        else if (oDietray.strDataName == "MaxWater")
                        {
                            if (!string.IsNullOrEmpty(oDietray.strDataValue))
                            {
                                int maxWater = int.Parse(oDietray.strDataValue);
                                maxWater = (int)System.Math.Round((double)maxWater * 2 / 1000);
                                maxWater = maxWater * 1000 / 2;
                                MaxWater = "～" + maxWater.ToString();
                            }
                        }
                        else if (oDietray.strDataName == "MinWater")
                        {
                            if (!string.IsNullOrEmpty(oDietray.strDataValue))
                            {
                                int minWater = int.Parse(oDietray.strDataValue);
                                minWater = (int)System.Math.Round((double)minWater * 2 / 1000);
                                minWater = minWater * 1000 / 2;
                                MinWater = minWater.ToString();
                            }
                        }
                        else if (oDietray.strDataName == "Salt")
                        {
                            GlobalData.DietSuggestion.LimitedFood += "食用盐" + oDietray.strDataValue + "克/日; ";
                        }
                        else if (oDietray.strDataName == "TotalCarbo")
                        {
                            // GlobalData.DietSuggestion.CarboCount = oDietray.strDataValue;

                            try
                            {
                                GlobalData.DietSuggestion.CarboCount
                                    = (System.Math.Ceiling(Convert.ToDouble(oDietray.strDataValue))).ToString();
                            }
                            catch
                            {
                                GlobalData.DietSuggestion.CarboCount = string.Empty;
                            }
                        }
                        else if (oDietray.strDataName == "TotalProtein")
                        {
                            GlobalData.DietSuggestion.ProteinCount = oDietray.strDataValue;
                        }
                        else if (oDietray.strDataName == "Nut")
                        {
                            GlobalData.DietSuggestion.LimitedFood += FormatNut(oDietray.strDataValue);
                        }
                    }

                    GlobalData.DietSuggestion.TotalWater = MinWater + MaxWater;

                    for (int i = 0; i < oOutputInfo.oInference.lstUnstructedInferenceMessage.Count; i++)
                    {
                        GlobalData.DietSuggestion.AvoidFood +=
                            oOutputInfo.oInference.lstUnstructedInferenceMessage[i].strUnStructMessage + "; ";
                    }
                }     
        }

        public static string FormatDietaryType(string strDietaryTypeCode)
        {
            string strDietaryType = String.Empty;
            if (strDietaryTypeCode.Length > 1)
            {
                foreach (char code in strDietaryTypeCode)
                {
                    switch (code)
                    {
                        case '1':
                            strDietaryType += "糖尿病-";
                            break;
                        case '2':
                            strDietaryType += "低脂-";
                            break;
                        case '3':
                            strDietaryType += "低盐-";
                            break;
                        case '4':
                            strDietaryType += "低嘌呤-";
                            break;
                        case '5':
                            strDietaryType += "低胆固醇-";
                            break;
                        default:
                            break;
                    }
                }
                strDietaryType += "普食";
            }

            return strDietaryType;
        }

        public static string FormatNut(string strNutValue)
        {
            if(strNutValue == "YES")
            {
                return "干果少于15g;  ";
            }
            else
            {
                return string.Empty;
            }
        }

        public static void FormatPhysicalActivity(vMRClsDef.InputDataModel oIEInputDataModel, vMRClsDef.OutputInfo oOutputInfo)
        {
            GlobalData.ExerciseSuggestion.Clear();
            string strPhysicalAcitivityAim = string.Empty;
            bool bSportTypeFree = true;
            bool bSportTypeLow = false;
            bool bSportTypeMiddle = false;
            bool bSportTypeHigh = false;

            bool bStrengthenPhysique = false;
            bool bReduceWeight = false;
            bool bReduceBG = false;

            double dReduceWeightCal = 0;
            double dReduceBGCal = 0;

            double dWeight = 0;

            double dFreeEnergyConsume = 0.035;
            double dLowEnergyConsume = 0.07;
            double dMiddleEnergyConsume = 0.14;
            double dHighEnergyConsume = 0.23;

            FormatShortDataInfo(oIEInputDataModel, oOutputInfo, ref GlobalData.ExerciseSuggestion.DataNeeded);

            if (GlobalData.ExerciseSuggestion.DataNeeded == string.Empty)
            {

                foreach (vMRClsDef.DataModel oPhysicalActivity in oOutputInfo.oInference.lstStructedInferMessage)
                {

                    if (oPhysicalActivity.strDataName == "StrengthenPhysique")
                    {
                        if (oPhysicalActivity.strDataValue == "YES")
                        {
                            bStrengthenPhysique = true;
                            strPhysicalAcitivityAim += "增强体质； ";
                        }
                    }
                    else if (oPhysicalActivity.strDataName == "ReduceWeight")
                    {
                        if (oPhysicalActivity.strDataValue == "YES")
                        {
                            bReduceWeight = true;
                            strPhysicalAcitivityAim += "减轻体重； ";
                        }
                    }
                    else if (oPhysicalActivity.strDataName == "ReduceBG")
                    {
                        if (oPhysicalActivity.strDataValue == "YES")
                        {
                            bReduceBG = true;
                            strPhysicalAcitivityAim += "降低餐后血糖； ";
                        }
                    }

                    else if (oPhysicalActivity.strDataName == "SportTypeLow")
                    {
                        if (oPhysicalActivity.strDataValue == "YES")
                        {
                            bSportTypeLow = true;
                        }
                    }

                    else if (oPhysicalActivity.strDataName == "SportTypeMiddle")
                    {
                        if (oPhysicalActivity.strDataValue == "YES")
                        {
                            bSportTypeMiddle = true;
                        }
                    }

                    else if (oPhysicalActivity.strDataName == "SportTypeHigh")
                    {
                        if (oPhysicalActivity.strDataValue == "YES")
                        {
                            bSportTypeHigh = true;
                        }
                    }

                    else if (oPhysicalActivity.strDataName == "ReduceWeightCal")
                    {
                        try
                        {
                            dReduceWeightCal = Convert.ToDouble(oPhysicalActivity.strDataValue);
                        }
                        catch
                        {
                            dReduceWeightCal = 0;
                        }
                    }

                    else if (oPhysicalActivity.strDataName == "ReduceBGCal")
                    {
                        try
                        {
                            dReduceBGCal = Convert.ToDouble(oPhysicalActivity.strDataValue);
                        }
                        catch
                        {
                            dReduceBGCal = 0;
                        }
                    }

                    else if (oPhysicalActivity.strDataName == "Weight")
                    {
                        try
                        {
                            dWeight = Convert.ToDouble(oPhysicalActivity.strDataValue);
                        }
                        catch
                        {
                            dWeight = 0;
                        }
                    }
                }

                GlobalData.ExerciseSuggestion.ExerciseTarget = strPhysicalAcitivityAim;

                if (bStrengthenPhysique == true)
                {
                    GlobalData.ExerciseSuggestion.EnergyCost += "每周180分钟运动";
                    GlobalData.ExerciseSuggestion.NoIntensityExercise += "每周180分钟运动";
                    GlobalData.ExerciseSuggestion.LowIntensityExercise += "每周180分钟运动";
                    GlobalData.ExerciseSuggestion.MiddleIntensityExercise += "每周180分钟运动";
                    //GlobalData.ExerciseSuggestion.HighIntensityExercise += "每周180分钟运动";

                }

                double dTotalEnergyConsume = dReduceWeightCal + dReduceBGCal;

                if (bReduceWeight || bReduceBG)
                {
                    GlobalData.ExerciseSuggestion.EnergyCost +=
                        Convert.ToString(dTotalEnergyConsume * 7) + "kcal/周，" + dTotalEnergyConsume.ToString() + "kcal/日";

                    if (bSportTypeFree)
                    {
                        if (bReduceWeight)
                        {
                            int iReduceWeight = (Convert.ToInt32(dReduceWeightCal / dFreeEnergyConsume / dWeight) / 5) * 5;
                            GlobalData.ExerciseSuggestion.NoIntensityExercise += "3餐后" + iReduceWeight.ToString() + "分钟； ";
                        }
                        if (bReduceBG)
                        {
                            int iReduceBG = (Convert.ToInt32(dReduceBGCal / dFreeEnergyConsume / dWeight) / 5) * 5;
                            GlobalData.ExerciseSuggestion.NoIntensityExercise += "每日" + iReduceBG.ToString() + "分钟； ";
                        }
                    }

                    if (bSportTypeLow)
                    {
                        if (bReduceWeight)
                        {
                            int iReduceWeight = (Convert.ToInt32(dReduceWeightCal / dLowEnergyConsume / dWeight) / 5) * 5;
                            GlobalData.ExerciseSuggestion.LowIntensityExercise += "3餐后" + iReduceWeight.ToString() + "分钟； ";
                        }
                        if (bReduceBG)
                        {
                            int iReduceBG = (Convert.ToInt32(dReduceBGCal / dLowEnergyConsume / dWeight) / 5) * 5;
                            GlobalData.ExerciseSuggestion.LowIntensityExercise += "每日" + iReduceBG.ToString() + "分钟； ";
                        }
                    }

                    if (bSportTypeMiddle)
                    {
                        if (bReduceWeight)
                        {
                            int iReduceWeight = (Convert.ToInt32(dReduceWeightCal / dMiddleEnergyConsume / dWeight) / 5) * 5;
                            GlobalData.ExerciseSuggestion.MiddleIntensityExercise += "3餐后" + iReduceWeight.ToString() + "分钟； ";
                        }
                        if (bReduceBG)
                        {
                            int iReduceBG = (Convert.ToInt32(dReduceBGCal / dMiddleEnergyConsume / dWeight) / 5) * 5;
                            GlobalData.ExerciseSuggestion.MiddleIntensityExercise += "每日" + iReduceBG.ToString() + "分钟； ";
                        }
                    }

                    if (bSportTypeHigh)
                    {
                        if (bReduceWeight)
                        {
                            int iReduceWeight = (Convert.ToInt32(dReduceWeightCal / dHighEnergyConsume / dWeight) / 5) * 5;
                            GlobalData.ExerciseSuggestion.HighIntensityExercise += "3餐后" + iReduceWeight.ToString() + "分钟； ";
                        }
                        if (bReduceBG)
                        {
                            int iReduceBG = (Convert.ToInt32(dReduceBGCal / dHighEnergyConsume / dWeight) / 5) * 5;
                            GlobalData.ExerciseSuggestion.HighIntensityExercise += "每日" + iReduceBG.ToString() + "分钟； ";
                        }
                    }
                }
            }

        }

        public static void FormatShortDataInfo(vMRClsDef.InputDataModel oIEInputDataModel, vMRClsDef.OutputInfo oOutputInfo, ref string strExistShortData)
        {
            //string strShortData = string.Empty;
            for(int i = 0; i < oOutputInfo.lstShortDataModel.Count; i++)
            {
                string strDataName = oOutputInfo.lstShortDataModel[i].strDataName;
                foreach(vMRClsDef.DataModel oDataModel in oIEInputDataModel.lstDataModel)
                {
                    //BugDB00005686 revised by wbf 2009-03-26
                    if(oDataModel.strDataName == strDataName)
                    {
                        if (!strExistShortData.Contains(oDataModel.strDataCNName))
                        {
                            strExistShortData += oDataModel.strDataCNName + " ";
                        }
                    }
                }
            }
        }

        public static vMRClsDef.EnumInferenceType MapInferTypeFuntionDeftovMR(
            FunctionTypeDef.EnumInferenceType em_InferenceType)
        {
            switch(em_InferenceType)
            {
                case FunctionTypeDef.EnumInferenceType.PRIMARY:
                    return vMRClsDef.EnumInferenceType.PRIMARY;
                case FunctionTypeDef.EnumInferenceType.NEEDSECONDTIME:
                    return vMRClsDef.EnumInferenceType.NEEDSECONDTIME;
                case FunctionTypeDef.EnumInferenceType.SECONDTIME:
                    return vMRClsDef.EnumInferenceType.SECONDTIME;
                default:
                    return vMRClsDef.EnumInferenceType.PRIMARY;
            }
        }

        public static vMRClsDef.EnumInferenceResultType MapInferResultTypeFuntiontovMR(
            FunctionTypeDef.EnumInferenceResultType em_InferResultType)
        {
            switch(em_InferResultType)
            {
                case FunctionTypeDef.EnumInferenceResultType.DIAGNOSIS:
                    return vMRClsDef.EnumInferenceResultType.DIAGNOSIS;
                case FunctionTypeDef.EnumInferenceResultType.THERAPY:
                    return vMRClsDef.EnumInferenceResultType.THERAPY;
                case FunctionTypeDef.EnumInferenceResultType.SELFMONITOR:
                    return vMRClsDef.EnumInferenceResultType.SELFMONITOR;
                case FunctionTypeDef.EnumInferenceResultType.MSEVALUATION:
                    return vMRClsDef.EnumInferenceResultType.MSEVALUATION;
                case FunctionTypeDef.EnumInferenceResultType.RISKEVALUATION:
                    return vMRClsDef.EnumInferenceResultType.RISKEVALUATION;
                case FunctionTypeDef.EnumInferenceResultType.ADVERSEEVENT:
                    return vMRClsDef.EnumInferenceResultType.ADVERSEEVENT;
                case FunctionTypeDef.EnumInferenceResultType.DIETARY:
                    return vMRClsDef.EnumInferenceResultType.DIETARY;
                case FunctionTypeDef.EnumInferenceResultType.PHYSICALACTIVITY:
                    return vMRClsDef.EnumInferenceResultType.PHYSICALACTIVITY;
                case FunctionTypeDef.EnumInferenceResultType.OTHER:
                    return vMRClsDef.EnumInferenceResultType.OTHER;
                default:
                    return vMRClsDef.EnumInferenceResultType.OTHER;
            }
        }

        public static FunctionTypeDef.EnumInferenceResultType MapInferResultTypevMRtoFunction(
            vMRClsDef.EnumInferenceResultType em_vMRInferResult)
        {
            switch(em_vMRInferResult)
            {
                case vMRClsDef.EnumInferenceResultType.DIAGNOSIS:
                    return FunctionTypeDef.EnumInferenceResultType.DIAGNOSIS;
                case vMRClsDef.EnumInferenceResultType.THERAPY:
                    return FunctionTypeDef.EnumInferenceResultType.THERAPY;
                case vMRClsDef.EnumInferenceResultType.SELFMONITOR:
                    return FunctionTypeDef.EnumInferenceResultType.SELFMONITOR;
                case vMRClsDef.EnumInferenceResultType.MSEVALUATION:
                    return FunctionTypeDef.EnumInferenceResultType.MSEVALUATION;
                case vMRClsDef.EnumInferenceResultType.RISKEVALUATION:
                    return FunctionTypeDef.EnumInferenceResultType.RISKEVALUATION;
                case vMRClsDef.EnumInferenceResultType.DIETARY:
                    return FunctionTypeDef.EnumInferenceResultType.DIETARY;
                case vMRClsDef.EnumInferenceResultType.PHYSICALACTIVITY:
                    return FunctionTypeDef.EnumInferenceResultType.PHYSICALACTIVITY;
                default:
                    return FunctionTypeDef.EnumInferenceResultType.OTHER;
             }

        }
    }


    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public class DynamicMapperToSystemData
    {
        public const string CONST_IMPORT_CDSS_SYSTEM_DATA_DLL = "CDSSSystemData.dll";
        public const string CONST_IMPORT_CDSS_SYSTEM_DATA_NAMESPACE = "CDSSSystemData";
        public const string CONST_IMPORT_SYSTEM_DLL = "System.dll";
        public const string CONST_IMPORT_SYSTEM_NAMESPACE = "System";
        public const string CONST_GENERATECODE_DLL = "ObtainSystemData.dll";
        public const string CONST_GENERATECODE_NAMESPACE = "CDSSDynamicCode";
        public const string CONST_GENERATECODE_CLASSNAME = "ObtainSystemData";
        public const string CONST_GENERATECODE_METHODNAME_OBTAIN_SYSTEM_DATA
            = "GetDataValue";

        /// <summary>
        /// 根据映射的类名获取CDSSSystemData.GlobalData中成员数据
        /// 返回值类型不定，由CDSSSystemData.GlobalData中成员数据类型决定
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetMapperValue(string strClassName)
        {
            CodeSnippetCompileUnit unit = ConstructAndCompileCode(strClassName);

            ICodeCompiler compiler = new CSharpCodeProvider().CreateCompiler();
            CompilerParameters para = new CompilerParameters();
            para.ReferencedAssemblies.Add(CONST_IMPORT_SYSTEM_DLL);
            para.ReferencedAssemblies.Add(CONST_IMPORT_CDSS_SYSTEM_DATA_DLL);
            para.GenerateInMemory = true;
            para.GenerateExecutable = false;

            CompilerResults cr = compiler.CompileAssemblyFromDom(para, unit);

            if (cr.Errors.Count > 0)
            {
                string str = "编译文件出错： " + cr.PathToAssembly + ": \r\n";
                foreach (CompilerError ce in cr.Errors)
                    str = ce.ToString();
                return false;
            }
            else
            {
                Assembly asm = cr.CompiledAssembly;
                Type type = asm.GetType(
                    CONST_GENERATECODE_NAMESPACE + "." + CONST_GENERATECODE_CLASSNAME);
                MethodInfo mi = type.GetMethod(
                    CONST_GENERATECODE_METHODNAME_OBTAIN_SYSTEM_DATA, BindingFlags.Public | BindingFlags.Instance);
                object obj = asm.CreateInstance(
                    CONST_GENERATECODE_NAMESPACE + "." + CONST_GENERATECODE_CLASSNAME);
                return mi.Invoke(obj, null);
            }

        }

        /// <summary>
        /// 构建获取CDSSSystemData GlobalData中数据的动态code,并编译运行
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static CodeSnippetCompileUnit ConstructAndCompileCode(string value)
        {
            CodeNamespace CurrentNameSpace
                = InitializeNameSpace(CONST_GENERATECODE_NAMESPACE);
            CodeTypeDeclaration ctd = CreateClass(CONST_GENERATECODE_CLASSNAME);
            CurrentNameSpace.Types.Add(ctd);
            CodeMemberMethod mtd = CreateMethod(
                CONST_GENERATECODE_METHODNAME_OBTAIN_SYSTEM_DATA);
            ctd.Members.Add(mtd);

            mtd.Statements.Add(new CodeSnippetExpression("return" + "\n" + value));

            CSharpCodeProvider provider = new CSharpCodeProvider();
            ICodeGenerator codeGen = provider.CreateGenerator();
            string codeSnippet = GenerateCode(codeGen, CurrentNameSpace);

            CodeSnippetCompileUnit unit = new CodeSnippetCompileUnit(codeSnippet);
            return unit;
        }

        /// <summary>
        /// 初始化动态code的NameSpace
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private static CodeNamespace InitializeNameSpace(string Name)
        {
            CodeNamespace CurrentNameSpace = new CodeNamespace(Name);
            CurrentNameSpace.Imports.Add(
                new CodeNamespaceImport(CONST_IMPORT_SYSTEM_NAMESPACE));
            CurrentNameSpace.Imports.Add(
                new CodeNamespaceImport(CONST_IMPORT_CDSS_SYSTEM_DATA_NAMESPACE));
            return CurrentNameSpace;
        }

        /// <summary>
        /// 创建类
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private static CodeTypeDeclaration CreateClass(string Name)
        {
            CodeTypeDeclaration ctd = new CodeTypeDeclaration(Name);
            ctd.IsClass = true;
            ctd.Attributes = MemberAttributes.Public;
            return ctd;
        }

        /// <summary>
        /// 创建方法
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <returns></returns>
        private static CodeMemberMethod CreateMethod(string strMethodName)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = strMethodName;
            method.Attributes = MemberAttributes.Public;
            method.ReturnType = new CodeTypeReference("System.Object");
            return method;
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="CodeGenerator"></param>
        /// <param name="CurrentNameSpace"></param>
        /// <returns></returns>
        private static string GenerateCode(ICodeGenerator CodeGenerator,
            CodeNamespace CurrentNameSpace)
        {
            // CodeGeneratorOptions允许我们指定各种供代码生成器 
            // 使用的格式化选项 
            CodeGeneratorOptions cop = new CodeGeneratorOptions();
            // 指定格式：花括号的位置 
            cop.BracingStyle = "C";
            // 指定格式：代码块的缩进方式 
            cop.IndentString = " ";
            StringBuilder sbCode = new StringBuilder();
            StringWriter sw = new StringWriter(sbCode);
            CodeGenerator.GenerateCodeFromNamespace(CurrentNameSpace, sw, cop);
            return sbCode.ToString();
        }
    }   

    public class MappingToClassFromXml
    {
        private static XmlDocument m_xmldoc;
        private static XmlNamespaceManager m_xmlManager;

        public static void OpenXmlDoc(string strFileName)
        {
            m_xmldoc = new XmlDocument();
            m_xmldoc.Load(strFileName);
            m_xmlManager = new XmlNamespaceManager(m_xmldoc.NameTable);
        }

        //查询名为Element_Node的所有节点
        private static XmlNodeList FindMulInstance()
        {
            string strNodePath;     //被查询实例的路径	
            XmlNodeList nlInstance = null;    //所有instance的列表
            strNodePath = string.Format(
                "/{0}/{1}", "Mapping", "DataMapper");		//xpath

            nlInstance = m_xmldoc.SelectNodes(strNodePath);
            return nlInstance;
        }

        public static string ObtainClassNameWithDataCode(string strDataCode)
        {
            OpenXmlDoc(ConfigurationSettings.AppSettings["DATAMAPPING_FILEPATH"]);
            XmlNodeList nl_DataMapper = FindMulInstance();
            foreach (XmlNode n_DataMapper in nl_DataMapper)
            {
                XmlNode n_StandardData
                    = n_DataMapper.SelectSingleNode("StdData");
                string strStandDataCode 
                    = n_StandardData.SelectSingleNode("StdDataCode").InnerText;
                if (strStandDataCode == strDataCode)
                {
                    XmlNode n_Class = n_DataMapper.SelectSingleNode("Class");
                    return n_Class.SelectSingleNode("ClsName").InnerText;
                }
            }
            return string.Empty;
        }
    }
}
