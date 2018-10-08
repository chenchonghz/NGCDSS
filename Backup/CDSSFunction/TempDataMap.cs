using System;
using System.Collections.Generic;
using System.Text;
using CDSSSystemData;

    /***************************************************
     * 创建人： XY
     * 创建时间：2009.3.16
     * 创建内容：映射模块
     * 创建说明：数据传入推理机
     * *************************************************/

namespace CDSSFunction
{    
    class TempDataMap
    {
        #region 映射模块
        public static string ObtainDataValueWithDataCode(string strDataCode)
        {
            switch (strDataCode)
            {
                case "000001"://空腹血糖
                    return MappingGlobaString(GlobalData.LabExamInfo.FBG);

                case "000002"://餐后2h血糖
                    return MappingGlobaString(GlobalData.LabExamInfo.TWOHPBG);

                case "000003"://HBA1C
                    return MappingGlobaString(GlobalData.LabExamInfo.HBA1C);

                case "000005"://OGTT血糖
                    return MappingGlobaString(GlobalData.LabExamInfo.OGTTFBG);

                case "000006"://关节红肿
                    return MappingGlobaBool(GlobalData.HyperuricemiaInfo.HasJointSwelling);

                case "000007"://OFTT餐后血糖
                    return MappingGlobaString(GlobalData.LabExamInfo.OGTTPBG);

                case "000011"://降尿酸药
                    return MappingGlobaCount(GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs.Count);

                case "000014"://年龄
                    if (GlobalData.PatBasicInfo.PatVisitDateTime.Year.ToString() == string.Empty || GlobalData.PatBasicInfo.PatBirthday.Year.ToString() == string.Empty)
                    {
                        return "NULL";
                    }
                    else
                    {
                        try
                        {
                            return Convert.ToString(Convert.ToInt32(GlobalData.PatBasicInfo.PatVisitDateTime.Year.ToString()) - Convert.ToInt32(GlobalData.PatBasicInfo.PatBirthday.Year.ToString()));
                        }
                        catch
                        {
                            return "NULL";
                        }
                    }

                case"000017"://BMI
                    return ComputeBMI(GlobalData.PhysicalInfo.Weigh, GlobalData.PhysicalInfo.Height);               

                case "000018"://尿尿酸
                    return MappingGlobaString(GlobalData.LabExamInfo.UUA);
                
                case "000019"://性别
                    if (GlobalData.PatBasicInfo.PatSex.Trim() == "男")
                    {
                        return "male";
                    }
                    else if (GlobalData.PatBasicInfo.PatSex.Trim() == "女")
                    {
                        return "female";
                    }
                    else
                    {
                        return "NULL";
                    }

                case "000021"://ICA胰岛细胞抗体测定阳性
                    if (GlobalData.LabExamInfo.ICA.ToString() != "阳性")
                    {
                        return "NO";
                    }
                    else
                    {
                        return "YES";
                    }

                case "000022"://腰围
                    return MappingGlobaString(GlobalData.PhysicalInfo.WC);

                case "000028"://尿Ph
                    return MappingGlobaString(GlobalData.LabExamInfo.UPH);

                case "000030"://TG甘油三酯
                    return MappingGlobaString(GlobalData.LabExamInfo.TG);

                case "000031"://TC总胆固醇
                    return MappingGlobaString(GlobalData.LabExamInfo.TC);
                
                case "000032"://HDLC高密度脂蛋白胆固醇                    
                    return MappingGlobaString(GlobalData.LabExamInfo.HDLC);

                case "000033"://LDLC低密度脂蛋白胆固醇
                    return MappingGlobaString(GlobalData.LabExamInfo.LDLC);

                case "000040"://痛风病史
                    if (GlobalData.HyperuricemiaInfo.HasGoutyArthritis == true || GlobalData.HyperuricemiaInfo.HasTophus == true)
                    {
                        return "YES";
                    }
                    else
                    {
                        return "NO";
                    }

                case "000041"://高尿酸病史
                    return MappingGlobaBool(GlobalData.HyperuricemiaInfo.HasHyperuricemia);
                    
                case "000045"://血尿酸
                    return MappingGlobaString(GlobalData.LabExamInfo.BUA);

                case "000050"://GDA65抗谷氨酸脱羧酶抗体阳性
                    if (GlobalData.LabExamInfo.GDA65 != "阳性")
                    {
                        return "NO";
                    }
                    else
                    {
                        return "YES";
                    }

                case "000052"://高尿酸血症诊断结论
                    return MappingDignoseConclusion("HUA_Diagnose");

                case "000054"://高尿酸原发继发诊断结论
                    return MappingDignoseConclusion("HUA_Diagnose_PS");

                case "000059"://高血压分级诊断结论
                    return MappingDignoseConclusion("hypertension_Diagnose_Stage");                    

                case "000064"://高血压危险度诊断结论
                    return MappingDignoseConclusion("Hyperuricaemia_Diagnose_risklevel");

                case "000069"://舒张压当前值
                    return JudgeMaxBP("", "", GlobalData.PhysicalInfo.DBP1, GlobalData.PhysicalInfo.DBP2);                    

                case "000070"://收缩压当前值
                    return JudgeMaxBP("", "", GlobalData.PhysicalInfo.SBP1, GlobalData.PhysicalInfo.SBP2);                
                    
                case "000071"://吸烟史
                    return MappingGlobaBool(GlobalData.PersonalHistoryInfo.IsSmokeing);

                case "000072"://心血管病家族史
                    if (MappingGlobaFamilyDH("冠心病") == "YES" || MappingGlobaFamilyDH("心梗") == "YES")
                    {
                        return "YES";
                    }
                    else 
                    {
                        return "NO";
                    }

                case "000073"://缺乏体力活动
                    if (GlobalData.PersonalHistoryInfo.HasExerciseRecent == false)
                    {
                        return "YES";
                    }
                    else
                    {
                        for (int i = 0; i < GlobalData.PersonalHistoryInfo.listExerciseInfo.Count; i++)
                        {
                            if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "卧床")
                            {
                                return "YES";
                            }
                            else 
                            {
                                return "NO";
                            }
                        }
                        return "NO";
                    }

                case "000077"://肾脏疾病
                    return MappingGlobaBool(GlobalData.NephropathyInfo.HasNephropathy);

                case "000078"://外周血管病变
                    return MappingGlobaListSymptoms(GlobalData.AGMInfo.listChronicSymptoms,"糖尿病外周神经病变");

                case "000079"://视网膜病变
                    return MappingGlobaListSymptoms(GlobalData.AGMInfo.listChronicSymptoms,"糖尿病视网膜病变"); 

                case "000080"://脑梗塞
                    if (MappingGlobaListSymptoms(GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease,"脑血栓")=="YES"|| MappingGlobaListSymptoms(GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease,"TIA")=="YES")
                    {
                        return "YES";
                    }
                    else 
                    {
                    return "NO";
                    }


                case "000081"://恶性肿瘤
                    return MappingGlobaBool(GlobalData.OtherDiseaseHistoryInfo.HasCancer);

                case "000084"://心脏疾病
                    return MappingGlobaCount(GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Count);

                case "000085"://血脂紊乱病史
                    return MappingGlobaBool(GlobalData.DyslipidemiaInfo.HasDyslipidemia);

                case "000090"://肌酐
                    return MappingGlobaString(GlobalData.LabExamInfo.CR);

                case "000091"://白蛋白肌酐比
                    return MappingGlobaString(GlobalData.LabExamInfo.ALBCR);

                case "000092"://舒张压最高值
                    return JudgeMaxBP(GlobalData.HypertensionInfo.MaxDBP,GlobalData.HypertensionInfo.PeacetimeMaxDBP,GlobalData.PhysicalInfo.DBP1,GlobalData.PhysicalInfo.DBP2);
                    
                case "000093"://收缩压最高值
                    return JudgeMaxBP(GlobalData.HypertensionInfo.MaxSBP, GlobalData.HypertensionInfo.PeacetimeMaxSBP, GlobalData.PhysicalInfo.SBP1, GlobalData.PhysicalInfo.SBP2);

                case "000094"://高血压病史
                    return MappingGlobaBool(GlobalData.HypertensionInfo.HasHypertension);

                case "000101"://糖尿病史
                    return MappingGlobaBool(GlobalData.AGMInfo.HasAGMAbnormal);

                case "000102"://妊娠糖尿病
                    if (GlobalData.PersonalHistoryInfo.HasBearing == true)
                    {
                        return "YES";
                    } 
                    else
                    {
                        for (int i = 0; i < GlobalData.AGMInfo.listConfirmedSymptoms.Count; i++)
                        {
                            if (GlobalData.AGMInfo.listConfirmedSymptoms[i].SymptomsName.Trim() == "妊娠糖尿病")
                            {
                                return "YES";
                            }
                            else
                            {
                                return "NO";
                            }
                        }
                        return "NO";
                    }
                    

                case "000103"://糖尿病家族史
                    return MappingGlobaFamilyDH("糖尿病");

                case "000104"://巨大产儿史
                    return MappingGlobaBool(GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg);

                case "000105"://肥胖病史
                    if (ComputeBMI(GlobalData.PhysicalInfo.Weigh,GlobalData.PhysicalInfo.Height) != "NULL")
                    {
                        try
                        {
                            if (Convert.ToDouble(ComputeBMI(GlobalData.PhysicalInfo.Weigh,GlobalData.PhysicalInfo.Height))> 25 )
                            {
                                return "YES";
                            }
                            else
                            {
                                return JudgeWC(GlobalData.PhysicalInfo.WC, GlobalData.PatBasicInfo.PatSex);
                            }
                        }
                        catch 
                        {
                            return "NO";
                        }                            
                    }
                    else
                    {
                        return JudgeWC(GlobalData.PhysicalInfo.WC, GlobalData.PatBasicInfo.PatSex);
                    }

                case "000107"://1型糖尿病
                    return MappingAGMConfirmed("1型糖尿病");

                case "000108"://2型糖尿病
                    return MappingAGMConfirmed("2型糖尿病");

                case "000110"://高低密度脂蛋白血症病史
                    return MappingGlobaListSymptoms(GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms,"高低密度脂蛋白胆固醇血症");

                case "000111"://低高密度脂蛋白血症病史
                    return MappingGlobaListSymptoms(GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms,"低高密度脂蛋白胆固醇血症");

                case "000112"://高胆固醇病史
                    return MappingGlobaListSymptoms(GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms,"高胆固醇");

                case "000113"://高甘油三酯病史
                    return MappingGlobaListSymptoms(GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms,"高甘油三酯血症");

                case "000114": //调脂药
                    return MappingGlobaCount(GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Count);

                case "000115": //贝特类
                    for (int i = 0; i < GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Count; i++)
                    {
                        if (GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].Drugtype.Trim() == "贝特类")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "000116": //他汀类
                    for (int i = 0; i < GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Count; i++)
                    {
                        if (GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].Drugtype.Trim() == "他汀类")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                //-------------------------------- Added by Ns ----------------------------------------------------------------

                case "000303": //降糖药
                    if (MappingGlobaCount(GlobalData.AGMInfo.listInsulinMedicineInfo.Count) == "YES" || MappingGlobaCount(GlobalData.AGMInfo.listChineseMedicineInfo.Count) == "YES" || MappingGlobaCount(GlobalData.AGMInfo.listHypogMedicineInfo.Count) == "YES")
                    {
                        return "YES";
                    }
                    else
                    {
                        return "NO";
                    }

                case "000305": //胰岛素
                    return MappingGlobaCount(GlobalData.AGMInfo.listInsulinMedicineInfo.Count);

                case "003031": //AGI
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "糖苷酶抑制剂")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "003032": //双胍类
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "双胍类")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "003033": //磺脲类
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "磺酰脲类")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "003034": //格列酮类
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "格列酮类")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "003035": //格列奈类
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "格列奈类")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                //-------------------------------------------------------------------------------------------------------------

                case "000117"://危险度评分
                    return MappingDignoseConclusion("risk_score");

                case "000118"://降压药
                    if (MappingGlobaCount(GlobalData.HypertensionInfo.listStepDownWestMedicine.Count)=="YES"||MappingGlobaCount(GlobalData.HypertensionInfo.listStepDownChineseMedication.Count)=="YES")
                    {
                        return "YES";
                    }
                    else
                    {
                        return "NO";
                    }

                case "000119"://脉心率
                    return MappingGlobaString(GlobalData.PhysicalInfo.HR);

                case "000120"://肾动脉狭窄(单)
                    return MappingGlobaListSymptoms(GlobalData.HypertensionInfo.listHypertensionSymptoms,"肾动脉狭窄(单)");

                case "000122"://肾功能异常
                    return MappingGlobaBool(GlobalData.NephropathyInfo.HasRenalAbnormal);

                case "000123"://肌酐清除率
                    if (GlobalData.PatBasicInfo.PatVisitDateTime.Year.ToString() != string.Empty && GlobalData.PatBasicInfo.PatBirthday.Year.ToString() != string.Empty && GlobalData.PhysicalInfo.Weigh != string.Empty && GlobalData.LabExamInfo.CR != string.Empty && GlobalData.PatBasicInfo.PatSex != "")
                    {
                        //BugDB00005689 revised by wbf 2009-03-25
                        try
                        {
                            if (GlobalData.PatBasicInfo.PatSex.Trim() == "男")
                            {
                                double Ccr = (140 -
                                    ((Convert.ToInt32(System.DateTime.Now.Year.ToString())
                                    - Convert.ToInt32(GlobalData.PatBasicInfo.PatBirthday.Year.ToString()))))
                                    * 88.4 * Convert.ToDouble(GlobalData.PhysicalInfo.Weigh) / 72 /
                                    Convert.ToDouble(GlobalData.LabExamInfo.CR);
                                Ccr = Math.Round(Ccr, 2);
                                return Ccr.ToString();
                            }
                            else if(GlobalData.PatBasicInfo.PatSex.Trim() == "女")
                            {
                                double Ccr = (140 -
                                    ((Convert.ToInt32(System.DateTime.Now.Year.ToString())
                                    - Convert.ToInt32(GlobalData.PatBasicInfo.PatBirthday.Year.ToString()))))
                                    * 88.4 * Convert.ToDouble(GlobalData.PhysicalInfo.Weigh) / 85 /
                                    Convert.ToDouble(GlobalData.LabExamInfo.CR);
                                Ccr = Math.Round(Ccr, 2);
                                return Ccr.ToString();
                            } 
                            else
                            {
                                return "NULL";
                            }
                        }
                        catch
                        {
                            return "NULL";
                        }
                    }
                    else
                    {
                        return "NULL";
                    }

                case "000124"://肾动脉狭窄(双)
                    return MappingGlobaListSymptoms(GlobalData.HypertensionInfo.listHypertensionSymptoms,"肾动脉狭窄(双)");


                case "000125"://糖尿病肾病
                    return MappingGlobaListSymptoms(GlobalData.AGMInfo.listChronicSymptoms,"糖尿病肾病");                    

                /**************************************************************************************************
                 * 添加人：XY
                 * 添加日期：2009.3.17
                 * 添加内容：诊断结论和饮食运动建议的映射
                 * 添加说明：部分诊断结论和全部的饮食运动新添了Datacode，全部采用7位，注意与以前的datacode相区分
                 * ************************************************************************************************/

                case "000203"://高血压诊断结论
                    return MappingDignoseConclusion("Hypertension_Diagnose");

                case "000204"://高血压原发继发诊断结论
                    return MappingDignoseConclusion("Hypertension_Diagnose_PS");

                case "000205"://高尿酸急性诊断结论
                    return MappingDignoseConclusion("HUA_Diagnose_Acute");

                case "000206"://肥胖结论
                    return MappingDignoseConclusion("Fat_Diagnose");

                case "000211"://代谢综合征结论
                    return MappingDignoseConclusion("Metabolic_Syndrome_Conclude");

                case "000369"://糖尿病诊断结论
                    return MappingDignoseConclusion("DM_Diagnose");

                case "000370"://TC诊断结论
                    return MappingDignoseConclusion("Dyslipidemia_Diagnose_TC");

                case "000378"://TG诊断结论
                    return MappingDignoseConclusion("Dyslipidemia_Diagnose_TG");

                case "000379"://HDLC诊断结论
                    return MappingDignoseConclusion("Dyslipidemia_Diagnose_HDLC");

                case "000380"://LDLC诊断结论
                    return MappingDignoseConclusion("Dyslipidemia_Diagnose_LDLC");

                case "000381"://血脂异常诊断
                    return MappingDignoseConclusion("Dyslipidemia_Diagnosed");

                case "000383"://高尿酸痛风诊断结论
                    return MappingDignoseConclusion("HUA_Diagnose_Gouty");

                case "000401"://TG达标
                    return MappingDignoseConclusion("TG_Reach_Standard");

                case "000402"://TG初次服药（贝特类或他汀类）
                    return MappingDignoseConclusion("TG_First_Drug");

                case "000403"://TC达标
                    return MappingDignoseConclusion("TC_Reach_Standard");

                case "000404"://TC初次服药（贝特类或他汀类）
                    return MappingDignoseConclusion("TC_First_Drug");

                case "000405"://LDL-ch达标
                    return MappingDignoseConclusion("LDLch_Reach_Standard");

                case "000406"://LDLch初次服药（贝特类或他汀类）
                    return MappingDignoseConclusion("LDLch_First_Drug");

                case "000407"://HDL-ch达标
                    return MappingDignoseConclusion("HDLch_Reach_Standard");

                case "000408"://HDLch初次服药（贝特类或他汀类）
                    return MappingDignoseConclusion("HDLch_First_Drug");

                case "000550"://IFG病史
                    return MappingAGMConfirmed("IFG");

                case "000551"://IGT病史
                    return MappingAGMConfirmed("IGT");

                case "000552"://IGR病史
                    return MappingAGMConfirmed("IGR");

                case "0000001"://DietaryType
                    return MappingDignoseConclusion("DietaryType");

                case "0000002"://TKcal
                    return MappingDignoseConclusion("TKcal");

                case "0000003"://FatAmount
                    return MappingDignoseConclusion("FatAmount");

                case "0000004"://CarboAmount
                    return MappingDignoseConclusion("CarboAmount");

                case "0000005"://MeatAmount
                    return MappingDignoseConclusion("MeatAmount");

                case "0000006"://MilkAmount
                    return MappingDignoseConclusion("MilkAmount");

                case "0000007"://EggAmount
                    return MappingDignoseConclusion("EggAmount");

                case "0000008"://FatRatio
                    return MappingDignoseConclusion("FatRatio");

                case "0000009"://CarboRatio
                    return MappingDignoseConclusion("CarboRatio");

                case "0000010"://ProteinRatio
                    return MappingDignoseConclusion("ProteinRatio");

                case "0000011"://MainFood
                    return MappingDignoseConclusion("MainFood");

                case "0000012"://Fruit
                    return MappingDignoseConclusion("Fruit");

                case "0000013"://Vegetable
                    return MappingDignoseConclusion("Vegetable");

                case "0000014"://Meat
                    return MappingDignoseConclusion("Meat");

                case "0000015"://TotalFat
                    return MappingDignoseConclusion("TotalFat");

                case "0000016"://Oil
                    return MappingDignoseConclusion("Oil");

                case "0000017"://MaxWater
                    return MappingDignoseConclusion("MaxWater");

                case "0000018"://MinWater
                    return MappingDignoseConclusion("MinWater");

                case "0000019"://Salt
                    return MappingDignoseConclusion("Salt");

                case "0000020"://TotalCarbo
                    return MappingDignoseConclusion("TotalCarbo");

                case "0000021"://TotalProtein
                    return MappingDignoseConclusion("TotalProtein");

                case "0000022"://Nut
                    return MappingDignoseConclusion("Nut");

                case "0000023"://StrengthenPhysique
                    return MappingDignoseConclusion("StrengthenPhysique");

                case "0000024"://ReduceWeight
                    return MappingDignoseConclusion("ReduceWeight");

                case "0000025"://ReduceBG
                    return MappingDignoseConclusion("ReduceBG");

                case "0000026"://SportTypeLow
                    return MappingDignoseConclusion("SportTypeLow");

                case "0000027"://SportTypeMiddle
                    return MappingDignoseConclusion("SportTypeMiddle");

                case "0000028"://SportTypeHigh
                    return MappingDignoseConclusion("SportTypeHigh");

                case "0000029"://ReduceWeightCal
                    return MappingDignoseConclusion("ReduceWeightCal");

                case "0000030"://ReduceBGCal
                    return MappingDignoseConclusion("ReduceBGCal");

                case "0000031"://Weight
                    return MappingDignoseConclusion("Weight");

                case "0000032"://LEnergyConsumption
                    return MappingDignoseConclusion("LEnergyConsumption");

                case "0000033"://MEnergyConsumption
                    return MappingDignoseConclusion("MEnergyConsumption");

                case "0000034"://HEnergyConsumption
                    return MappingDignoseConclusion("HEnergyConsumption");

                case "0000035"://MSRiskDegree
                    return MappingDignoseConclusion("MSRiskDegree");

                case "0100036"://运动类型
                    int iSportType = 0;
                    for (int i = 0; i < GlobalData.PersonalHistoryInfo.listExerciseInfo.Count; i++)
                    {
                        if (GlobalData.PersonalHistoryInfo.HasExerciseRecent == false)
                        {
                            //TempSportType =TempSportType +"NO";
                            int iTempSportType = 0;
                            if(iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "卧床")
                        {
                            //TempSportType = TempSportType + "SICKBED";
                            int iTempSportType = 1;
                            if (iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "休闲活动")
                        {
                            //TempSportType = TempSportType + "FREEDEGREE";
                            int iTempSportType = 2;
                            if (iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "轻度活动")
                        {
                            //TempSportType = TempSportType + "LOWDEGREE";
                            int iTempSportType = 3;
                            if (iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "中度活动")
                        {
                            //TempSportType = TempSportType + "MIDDLEDEGREE";
                            int iTempSportType = 4;
                            if (iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "强度活动")
                        {
                            //TempSportType = TempSportType + "HIGHDEGREE";
                            int iTempSportType = 5;
                            if (iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                    }

                    switch (iSportType)
                    {
                        case 0:
                            return "NO";
                        case 1:
                            return "SICKBED";
                        case 2:
                            return "FREEDEGREE";
                        case 3:
                            return "LOWDEGREE";
                        case 4:
                            return "MIDDLEDEGREE";
                        case 5:
                            return "HIGHDEGREE";
                        default:
                            return "NO";
                    }

                case "0100037"://身高
                    return MappingGlobaString(GlobalData.PhysicalInfo.Height);

                case "0100038"://体重
                    return MappingGlobaString(GlobalData.PhysicalInfo.Weigh);                    

                case "0100039"://疾病状态
                    if (GlobalData.OtherDiseaseHistoryInfo.HasCancer == true)
                    {
                        return "SPECIAL"; 
                    }
                    else
                    {
                        for (int i = 0; i < GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Count; i++)
                        {
                            if (GlobalData.OtherDiseaseHistoryInfo.listPancreatitis[i].SymptomsName.Trim() == "急性胰腺炎")
                            {
                                return "SPECIAL";
                            }
                            else
                            {
                                return "COMMON";
                            }
                        }
                        return "COMMON";
                    }
                    

                case "0100040"://尿蛋白定量
                    return MappingGlobaString(GlobalData.LabExamInfo.UrinaryProtein);

                case "0100041"://高血压家族史
                    return MappingGlobaFamilyDH("高血压");

                case "0100042"://高脂血症家族史
                    return MappingGlobaFamilyDH("高血脂症");

                case "0100043"://高尿酸血症家族史
                    return MappingGlobaFamilyDH("高尿酸血症");

                case "0100044"://冠心病家族史
                    return MappingGlobaFamilyDH("冠心病");

                case "0100045"://心梗血症史
                    return MappingGlobaFamilyDH("心梗");

                case "0100046"://下肢血管病变
                    return MappingGlobaListSymptoms(GlobalData.AGMInfo.listChronicSymptoms, "糖尿病周围血管病变");

                case "0100047"://随机血糖
                    return MappingGlobaString(GlobalData.LabExamInfo.BG);

                case "0100048"://运动障碍
                    return MappingGlobaBool(GlobalData.PhysicalInfo.HasDyskinesia);

                default:
                    return string.Empty;
            }
        }
        
        #endregion

        #region 功能函数
        /**************************************************************************************************
         * 修改人：XY
         * 修改日期：2009.3.20
         * 修改内容：将原有的重复代码写成功能函数
         * 修改说明：根据全局变量值的不同类型分类编写，进行映射；诊断结论单独映射
         * ************************************************************************************************/

        /// <summary>
        /// 直接映射全局变量string型的值到推理机
        /// </summary>
        /// <param name="GlobaStingMapper"></param>
        /// <returns></returns>
        private static string MappingGlobaString(string GlobaStringMapper)
        {
            if (GlobaStringMapper == string.Empty)
            {
                return "NULL";
            }
            else
            {
                return GlobaStringMapper;
            }
        }

        /// <summary>
        /// 直接映射全局变量bool型的值到推理机
        /// </summary>
        /// <param name="GlobaBoolMapper"></param>
        /// <returns></returns>
        private static string MappingGlobaBool(bool GlobaBoolMapper)
        {
            if (GlobaBoolMapper == true)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }

        /// <summary>
        /// 直接映射全局变量int型的值到推理机
        /// </summary>
        /// <param name="GlobaCountMapper"></param>
        /// <returns></returns>
        private static string MappingGlobaCount(int GlobaCountMapper)
        {
            if (GlobaCountMapper > 0)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }

        /// <summary>
        /// 直接映射全局变量症状信息的值到推理机
        /// </summary>
        /// <param name="ListSymptoms"></param>
        /// <param name="ListStringMapper"></param>
        /// <returns></returns>
        private static string MappingGlobaListSymptoms(List<CDSSSymptomsInfo> ListSymptoms, string ListStringMapper)
        {
            for (int i = 0; i < ListSymptoms.Count; i++)
            {
                if (ListSymptoms[i].SymptomsName.Trim() == ListStringMapper)
                {
                    return "YES";
                }
            }
            return "NO";
        }

        /// <summary>
        /// 直接映射全局变量中家族疾病史的数据到推理机
        /// </summary>
        /// <param name="GlobaFamilyDHMapper"></param>
        /// <returns></returns>
        private static string MappingGlobaFamilyDH(string GlobaFamilyDHMapper)
        {
            if (GlobalData.FamilyDiseaseHistoryInfo.FatherHistory != null || GlobalData.FamilyDiseaseHistoryInfo.MotherHistory != null || GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory != null || GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory != null || GlobalData.FamilyDiseaseHistoryInfo.OtherHistory != null)
            {
                if (GlobalData.FamilyDiseaseHistoryInfo.FatherHistory.Contains(GlobaFamilyDHMapper))
                {
                    return "YES";
                }
                else if (GlobalData.FamilyDiseaseHistoryInfo.MotherHistory.Contains(GlobaFamilyDHMapper))
                {
                    return "YES";
                }

                else if (GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory.Contains(GlobaFamilyDHMapper))
                {
                    return "YES";
                }

                else if (GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory.Contains(GlobaFamilyDHMapper))
                {
                    return "YES";
                }

                else if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains(GlobaFamilyDHMapper))
                {
                    return "YES";
                }
                else
                {
                    return "NO";
                }
            }
            else
            {
                return "NO";
            }
        }

        /// <summary>
        /// 映射诊断结论到推理机
        /// </summary>
        /// <param name="DignoseConclusionMapper"></param>
        /// <returns></returns>
        private static string MappingDignoseConclusion(string DignoseConclusionMapper)
        {
            for (int i = 0; i < StructedIEResult.lstStructedConclude.Count; i++)
            {
                for (int j = 0; j < StructedIEResult.lstStructedConclude[i].lstConclude.Count; j++)
                {
                    if (StructedIEResult.lstStructedConclude[i].lstConclude[j].strDataName == DignoseConclusionMapper)
                    {
                        if (StructedIEResult.lstStructedConclude[i].lstConclude[j].strDataValue != "NULL")
                        {
                            return StructedIEResult.lstStructedConclude[i].lstConclude[j].strDataValue;
                        }
                        else
                        {
                            return "NO";
                        }
                        
                    }
                }
            }
            return "NO";
        }

        /// <summary>
        /// 映射糖尿病确诊类型
        /// </summary>
        /// <param name="AGMConfirmedMapper"></param>
        /// <returns></returns>
        private static string MappingAGMConfirmed(string AGMConfirmedMapper)
        {
            if (GlobalData.AGMInfo.listConfirmedSymptoms.Count != 0)
            {
                DateTime TempDetecteTime = GlobalData.AGMInfo.listConfirmedSymptoms[0].SymptomsDetectedDateTime;
                string TempName = GlobalData.AGMInfo.listConfirmedSymptoms[0].SymptomsName;
                for (int i = 1; i < GlobalData.AGMInfo.listConfirmedSymptoms.Count; i++)
                {
                    if (TempDetecteTime <= GlobalData.AGMInfo.listConfirmedSymptoms[i].SymptomsDetectedDateTime)
                    {
                        TempName = GlobalData.AGMInfo.listConfirmedSymptoms[i].SymptomsName;
                    }
                }
                if (TempName == AGMConfirmedMapper)
                {
                    return "YES";
                }
                else
                {
                    return "NO";
                }
            }
            else 
            {
                return "NO";
            }
        }

        /// <summary>
        /// 计算BMI函数
        /// </summary>
        /// <param name="TempWeigh"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        private static string ComputeBMI(string TempWeigh, string TempHeight)
        {
            if (TempWeigh == string.Empty || TempHeight == string.Empty)
            {
                return "NULL";
            }
            else
            {
                try
                {
                    double dBMI = Convert.ToDouble(TempWeigh) / Convert.ToDouble(TempHeight) / Convert.ToDouble(TempHeight) * 10000;
                    dBMI = System.Math.Round(dBMI, 2);
                    return dBMI.ToString();
                }
                catch
                {
                    return "NULL";
                }
            }
        }
        /// <summary>
        /// 判断血压最高值函数
        /// </summary>
        /// <param name="HyperMaxBP"></param>
        /// <param name="HyperPeacetimeMaxBP"></param>
        /// <param name="PhyBP1"></param>
        /// <param name="PhyBP2"></param>
        /// <returns></returns>
        private static string JudgeMaxBP(string HyperMaxBP, string HyperPeacetimeMaxBP,string PhyBP1,string PhyBP2)
        {
            int TempMaxBP;
            try
            {
                if (HyperMaxBP != "")
                {
                    TempMaxBP = Convert.ToInt32(HyperMaxBP);
                }
                else
                {
                    //BugDB00005711 reivsed by wbf 2009-03-26
                    TempMaxBP = -1;
                }
                if (HyperPeacetimeMaxBP != "")
                {
                    if (Convert.ToInt32(HyperPeacetimeMaxBP) >= TempMaxBP)
                    {
                        TempMaxBP = Convert.ToInt32(HyperPeacetimeMaxBP);
                    }
                }
                if (PhyBP1 != "")
                {
                    if (Convert.ToInt32(PhyBP1) >= TempMaxBP)
                    {
                        TempMaxBP = Convert.ToInt32(PhyBP1);
                    }
                }
                if (PhyBP2 != "")
                {
                    if (Convert.ToInt32(PhyBP2) >= TempMaxBP)
                    {
                        TempMaxBP = Convert.ToInt32(PhyBP2);
                    }
                }
            }
            catch
            {
                return "NULL";
            }
            //BugDB00005711 reivsed by wbf 2009-03-26
            if (TempMaxBP == -1)
            {
                return "NULL";
            }
            else
            {
                return TempMaxBP.ToString();
            }
        }

        /// <summary>
        /// 判断腰围是否超标函数
        /// </summary>
        /// <param name="TempWC"></param>
        /// <param name="TempPatSex"></param>
        /// <returns></returns>
        private static string JudgeWC(string TempWC, string TempPatSex)
        {
            if (TempWC != "")
            {
                if (TempPatSex == "男")
                {
                    try
                    {
                        if (Convert.ToDouble(TempWC) > 90)
                        {
                            return "YES";
                        }
                        else
                        {
                            return "NO";
                        }
                    }
                    catch
                    {
                        return "NO";
                    }
                }
                else
                {
                    try
                    {
                        if (Convert.ToDouble(TempWC) > 80)
                        {
                            return "YES";
                        }
                        else
                        {
                            return "NO";
                        }
                    }
                    catch
                    {
                        return "NO";
                    }
                }
            }
            else
            {
                return "NO";
            }
        }
        #endregion
    }
}
