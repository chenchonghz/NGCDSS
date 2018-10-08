using System;
using System.Collections.Generic;
using System.Text;
using CDSSSystemData;

    /***************************************************
     * �����ˣ� XY
     * ����ʱ�䣺2009.3.16
     * �������ݣ�ӳ��ģ��
     * ����˵�������ݴ��������
     * *************************************************/

namespace CDSSFunction
{    
    class TempDataMap
    {
        #region ӳ��ģ��
        public static string ObtainDataValueWithDataCode(string strDataCode)
        {
            switch (strDataCode)
            {
                case "000001"://�ո�Ѫ��
                    return MappingGlobaString(GlobalData.LabExamInfo.FBG);

                case "000002"://�ͺ�2hѪ��
                    return MappingGlobaString(GlobalData.LabExamInfo.TWOHPBG);

                case "000003"://HBA1C
                    return MappingGlobaString(GlobalData.LabExamInfo.HBA1C);

                case "000005"://OGTTѪ��
                    return MappingGlobaString(GlobalData.LabExamInfo.OGTTFBG);

                case "000006"://�ؽں���
                    return MappingGlobaBool(GlobalData.HyperuricemiaInfo.HasJointSwelling);

                case "000007"://OFTT�ͺ�Ѫ��
                    return MappingGlobaString(GlobalData.LabExamInfo.OGTTPBG);

                case "000011"://������ҩ
                    return MappingGlobaCount(GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs.Count);

                case "000014"://����
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

                case "000018"://������
                    return MappingGlobaString(GlobalData.LabExamInfo.UUA);
                
                case "000019"://�Ա�
                    if (GlobalData.PatBasicInfo.PatSex.Trim() == "��")
                    {
                        return "male";
                    }
                    else if (GlobalData.PatBasicInfo.PatSex.Trim() == "Ů")
                    {
                        return "female";
                    }
                    else
                    {
                        return "NULL";
                    }

                case "000021"://ICA�ȵ�ϸ������ⶨ����
                    if (GlobalData.LabExamInfo.ICA.ToString() != "����")
                    {
                        return "NO";
                    }
                    else
                    {
                        return "YES";
                    }

                case "000022"://��Χ
                    return MappingGlobaString(GlobalData.PhysicalInfo.WC);

                case "000028"://��Ph
                    return MappingGlobaString(GlobalData.LabExamInfo.UPH);

                case "000030"://TG��������
                    return MappingGlobaString(GlobalData.LabExamInfo.TG);

                case "000031"://TC�ܵ��̴�
                    return MappingGlobaString(GlobalData.LabExamInfo.TC);
                
                case "000032"://HDLC���ܶ�֬���׵��̴�                    
                    return MappingGlobaString(GlobalData.LabExamInfo.HDLC);

                case "000033"://LDLC���ܶ�֬���׵��̴�
                    return MappingGlobaString(GlobalData.LabExamInfo.LDLC);

                case "000040"://ʹ�粡ʷ
                    if (GlobalData.HyperuricemiaInfo.HasGoutyArthritis == true || GlobalData.HyperuricemiaInfo.HasTophus == true)
                    {
                        return "YES";
                    }
                    else
                    {
                        return "NO";
                    }

                case "000041"://�����Სʷ
                    return MappingGlobaBool(GlobalData.HyperuricemiaInfo.HasHyperuricemia);
                    
                case "000045"://Ѫ����
                    return MappingGlobaString(GlobalData.LabExamInfo.BUA);

                case "000050"://GDA65���Ȱ�������ø��������
                    if (GlobalData.LabExamInfo.GDA65 != "����")
                    {
                        return "NO";
                    }
                    else
                    {
                        return "YES";
                    }

                case "000052"://������Ѫ֢��Ͻ���
                    return MappingDignoseConclusion("HUA_Diagnose");

                case "000054"://������ԭ���̷���Ͻ���
                    return MappingDignoseConclusion("HUA_Diagnose_PS");

                case "000059"://��Ѫѹ�ּ���Ͻ���
                    return MappingDignoseConclusion("hypertension_Diagnose_Stage");                    

                case "000064"://��ѪѹΣ�ն���Ͻ���
                    return MappingDignoseConclusion("Hyperuricaemia_Diagnose_risklevel");

                case "000069"://����ѹ��ǰֵ
                    return JudgeMaxBP("", "", GlobalData.PhysicalInfo.DBP1, GlobalData.PhysicalInfo.DBP2);                    

                case "000070"://����ѹ��ǰֵ
                    return JudgeMaxBP("", "", GlobalData.PhysicalInfo.SBP1, GlobalData.PhysicalInfo.SBP2);                
                    
                case "000071"://����ʷ
                    return MappingGlobaBool(GlobalData.PersonalHistoryInfo.IsSmokeing);

                case "000072"://��Ѫ�ܲ�����ʷ
                    if (MappingGlobaFamilyDH("���Ĳ�") == "YES" || MappingGlobaFamilyDH("�Ĺ�") == "YES")
                    {
                        return "YES";
                    }
                    else 
                    {
                        return "NO";
                    }

                case "000073"://ȱ�������
                    if (GlobalData.PersonalHistoryInfo.HasExerciseRecent == false)
                    {
                        return "YES";
                    }
                    else
                    {
                        for (int i = 0; i < GlobalData.PersonalHistoryInfo.listExerciseInfo.Count; i++)
                        {
                            if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "�Դ�")
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

                case "000077"://���༲��
                    return MappingGlobaBool(GlobalData.NephropathyInfo.HasNephropathy);

                case "000078"://����Ѫ�ܲ���
                    return MappingGlobaListSymptoms(GlobalData.AGMInfo.listChronicSymptoms,"���������񾭲���");

                case "000079"://����Ĥ����
                    return MappingGlobaListSymptoms(GlobalData.AGMInfo.listChronicSymptoms,"��������Ĥ����"); 

                case "000080"://�Թ���
                    if (MappingGlobaListSymptoms(GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease,"��Ѫ˨")=="YES"|| MappingGlobaListSymptoms(GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease,"TIA")=="YES")
                    {
                        return "YES";
                    }
                    else 
                    {
                    return "NO";
                    }


                case "000081"://��������
                    return MappingGlobaBool(GlobalData.OtherDiseaseHistoryInfo.HasCancer);

                case "000084"://���༲��
                    return MappingGlobaCount(GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.Count);

                case "000085"://Ѫ֬���Ҳ�ʷ
                    return MappingGlobaBool(GlobalData.DyslipidemiaInfo.HasDyslipidemia);

                case "000090"://����
                    return MappingGlobaString(GlobalData.LabExamInfo.CR);

                case "000091"://�׵��׼�����
                    return MappingGlobaString(GlobalData.LabExamInfo.ALBCR);

                case "000092"://����ѹ���ֵ
                    return JudgeMaxBP(GlobalData.HypertensionInfo.MaxDBP,GlobalData.HypertensionInfo.PeacetimeMaxDBP,GlobalData.PhysicalInfo.DBP1,GlobalData.PhysicalInfo.DBP2);
                    
                case "000093"://����ѹ���ֵ
                    return JudgeMaxBP(GlobalData.HypertensionInfo.MaxSBP, GlobalData.HypertensionInfo.PeacetimeMaxSBP, GlobalData.PhysicalInfo.SBP1, GlobalData.PhysicalInfo.SBP2);

                case "000094"://��Ѫѹ��ʷ
                    return MappingGlobaBool(GlobalData.HypertensionInfo.HasHypertension);

                case "000101"://����ʷ
                    return MappingGlobaBool(GlobalData.AGMInfo.HasAGMAbnormal);

                case "000102"://��������
                    if (GlobalData.PersonalHistoryInfo.HasBearing == true)
                    {
                        return "YES";
                    } 
                    else
                    {
                        for (int i = 0; i < GlobalData.AGMInfo.listConfirmedSymptoms.Count; i++)
                        {
                            if (GlobalData.AGMInfo.listConfirmedSymptoms[i].SymptomsName.Trim() == "��������")
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
                    

                case "000103"://���򲡼���ʷ
                    return MappingGlobaFamilyDH("����");

                case "000104"://�޴����ʷ
                    return MappingGlobaBool(GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg);

                case "000105"://���ֲ�ʷ
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

                case "000107"://1������
                    return MappingAGMConfirmed("1������");

                case "000108"://2������
                    return MappingAGMConfirmed("2������");

                case "000110"://�ߵ��ܶ�֬����Ѫ֢��ʷ
                    return MappingGlobaListSymptoms(GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms,"�ߵ��ܶ�֬���׵��̴�Ѫ֢");

                case "000111"://�͸��ܶ�֬����Ѫ֢��ʷ
                    return MappingGlobaListSymptoms(GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms,"�͸��ܶ�֬���׵��̴�Ѫ֢");

                case "000112"://�ߵ��̴���ʷ
                    return MappingGlobaListSymptoms(GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms,"�ߵ��̴�");

                case "000113"://�߸���������ʷ
                    return MappingGlobaListSymptoms(GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms,"�߸�������Ѫ֢");

                case "000114": //��֬ҩ
                    return MappingGlobaCount(GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Count);

                case "000115": //������
                    for (int i = 0; i < GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Count; i++)
                    {
                        if (GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].Drugtype.Trim() == "������")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "000116": //��͡��
                    for (int i = 0; i < GlobalData.DyslipidemiaInfo.listLipidlowerDrugs.Count; i++)
                    {
                        if (GlobalData.DyslipidemiaInfo.listLipidlowerDrugs[i].Drugtype.Trim() == "��͡��")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                //-------------------------------- Added by Ns ----------------------------------------------------------------

                case "000303": //����ҩ
                    if (MappingGlobaCount(GlobalData.AGMInfo.listInsulinMedicineInfo.Count) == "YES" || MappingGlobaCount(GlobalData.AGMInfo.listChineseMedicineInfo.Count) == "YES" || MappingGlobaCount(GlobalData.AGMInfo.listHypogMedicineInfo.Count) == "YES")
                    {
                        return "YES";
                    }
                    else
                    {
                        return "NO";
                    }

                case "000305": //�ȵ���
                    return MappingGlobaCount(GlobalData.AGMInfo.listInsulinMedicineInfo.Count);

                case "003031": //AGI
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "����ø���Ƽ�")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "003032": //˫����
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "˫����")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "003033": //������
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "��������")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "003034": //����ͪ��
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "����ͪ��")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                case "003035": //��������
                    for (int i = 0; i < GlobalData.AGMInfo.listHypogMedicineInfo.Count; i++)
                    {
                        if (GlobalData.AGMInfo.listHypogMedicineInfo[i].Drugtype.Trim() == "��������")
                        {
                            return "YES";
                        }
                    }
                    return "NO";

                //-------------------------------------------------------------------------------------------------------------

                case "000117"://Σ�ն�����
                    return MappingDignoseConclusion("risk_score");

                case "000118"://��ѹҩ
                    if (MappingGlobaCount(GlobalData.HypertensionInfo.listStepDownWestMedicine.Count)=="YES"||MappingGlobaCount(GlobalData.HypertensionInfo.listStepDownChineseMedication.Count)=="YES")
                    {
                        return "YES";
                    }
                    else
                    {
                        return "NO";
                    }

                case "000119"://������
                    return MappingGlobaString(GlobalData.PhysicalInfo.HR);

                case "000120"://��������խ(��)
                    return MappingGlobaListSymptoms(GlobalData.HypertensionInfo.listHypertensionSymptoms,"��������խ(��)");

                case "000122"://�������쳣
                    return MappingGlobaBool(GlobalData.NephropathyInfo.HasRenalAbnormal);

                case "000123"://���������
                    if (GlobalData.PatBasicInfo.PatVisitDateTime.Year.ToString() != string.Empty && GlobalData.PatBasicInfo.PatBirthday.Year.ToString() != string.Empty && GlobalData.PhysicalInfo.Weigh != string.Empty && GlobalData.LabExamInfo.CR != string.Empty && GlobalData.PatBasicInfo.PatSex != "")
                    {
                        //BugDB00005689 revised by wbf 2009-03-25
                        try
                        {
                            if (GlobalData.PatBasicInfo.PatSex.Trim() == "��")
                            {
                                double Ccr = (140 -
                                    ((Convert.ToInt32(System.DateTime.Now.Year.ToString())
                                    - Convert.ToInt32(GlobalData.PatBasicInfo.PatBirthday.Year.ToString()))))
                                    * 88.4 * Convert.ToDouble(GlobalData.PhysicalInfo.Weigh) / 72 /
                                    Convert.ToDouble(GlobalData.LabExamInfo.CR);
                                Ccr = Math.Round(Ccr, 2);
                                return Ccr.ToString();
                            }
                            else if(GlobalData.PatBasicInfo.PatSex.Trim() == "Ů")
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

                case "000124"://��������խ(˫)
                    return MappingGlobaListSymptoms(GlobalData.HypertensionInfo.listHypertensionSymptoms,"��������խ(˫)");


                case "000125"://��������
                    return MappingGlobaListSymptoms(GlobalData.AGMInfo.listChronicSymptoms,"��������");                    

                /**************************************************************************************************
                 * ����ˣ�XY
                 * ������ڣ�2009.3.17
                 * ������ݣ���Ͻ��ۺ���ʳ�˶������ӳ��
                 * ���˵����������Ͻ��ۺ�ȫ������ʳ�˶�������Datacode��ȫ������7λ��ע������ǰ��datacode������
                 * ************************************************************************************************/

                case "000203"://��Ѫѹ��Ͻ���
                    return MappingDignoseConclusion("Hypertension_Diagnose");

                case "000204"://��Ѫѹԭ���̷���Ͻ���
                    return MappingDignoseConclusion("Hypertension_Diagnose_PS");

                case "000205"://�����ἱ����Ͻ���
                    return MappingDignoseConclusion("HUA_Diagnose_Acute");

                case "000206"://���ֽ���
                    return MappingDignoseConclusion("Fat_Diagnose");

                case "000211"://��л�ۺ�������
                    return MappingDignoseConclusion("Metabolic_Syndrome_Conclude");

                case "000369"://������Ͻ���
                    return MappingDignoseConclusion("DM_Diagnose");

                case "000370"://TC��Ͻ���
                    return MappingDignoseConclusion("Dyslipidemia_Diagnose_TC");

                case "000378"://TG��Ͻ���
                    return MappingDignoseConclusion("Dyslipidemia_Diagnose_TG");

                case "000379"://HDLC��Ͻ���
                    return MappingDignoseConclusion("Dyslipidemia_Diagnose_HDLC");

                case "000380"://LDLC��Ͻ���
                    return MappingDignoseConclusion("Dyslipidemia_Diagnose_LDLC");

                case "000381"://Ѫ֬�쳣���
                    return MappingDignoseConclusion("Dyslipidemia_Diagnosed");

                case "000383"://������ʹ����Ͻ���
                    return MappingDignoseConclusion("HUA_Diagnose_Gouty");

                case "000401"://TG���
                    return MappingDignoseConclusion("TG_Reach_Standard");

                case "000402"://TG���η�ҩ�����������͡�ࣩ
                    return MappingDignoseConclusion("TG_First_Drug");

                case "000403"://TC���
                    return MappingDignoseConclusion("TC_Reach_Standard");

                case "000404"://TC���η�ҩ�����������͡�ࣩ
                    return MappingDignoseConclusion("TC_First_Drug");

                case "000405"://LDL-ch���
                    return MappingDignoseConclusion("LDLch_Reach_Standard");

                case "000406"://LDLch���η�ҩ�����������͡�ࣩ
                    return MappingDignoseConclusion("LDLch_First_Drug");

                case "000407"://HDL-ch���
                    return MappingDignoseConclusion("HDLch_Reach_Standard");

                case "000408"://HDLch���η�ҩ�����������͡�ࣩ
                    return MappingDignoseConclusion("HDLch_First_Drug");

                case "000550"://IFG��ʷ
                    return MappingAGMConfirmed("IFG");

                case "000551"://IGT��ʷ
                    return MappingAGMConfirmed("IGT");

                case "000552"://IGR��ʷ
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

                case "0100036"://�˶�����
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
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "�Դ�")
                        {
                            //TempSportType = TempSportType + "SICKBED";
                            int iTempSportType = 1;
                            if (iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "���л")
                        {
                            //TempSportType = TempSportType + "FREEDEGREE";
                            int iTempSportType = 2;
                            if (iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "��Ȼ")
                        {
                            //TempSportType = TempSportType + "LOWDEGREE";
                            int iTempSportType = 3;
                            if (iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "�жȻ")
                        {
                            //TempSportType = TempSportType + "MIDDLEDEGREE";
                            int iTempSportType = 4;
                            if (iSportType < iTempSportType)
                            {
                                iSportType = iTempSportType;
                            }
                        }
                        if (GlobalData.PersonalHistoryInfo.listExerciseInfo[i].ExerciseType.Trim() == "ǿ�Ȼ")
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

                case "0100037"://���
                    return MappingGlobaString(GlobalData.PhysicalInfo.Height);

                case "0100038"://����
                    return MappingGlobaString(GlobalData.PhysicalInfo.Weigh);                    

                case "0100039"://����״̬
                    if (GlobalData.OtherDiseaseHistoryInfo.HasCancer == true)
                    {
                        return "SPECIAL"; 
                    }
                    else
                    {
                        for (int i = 0; i < GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.Count; i++)
                        {
                            if (GlobalData.OtherDiseaseHistoryInfo.listPancreatitis[i].SymptomsName.Trim() == "����������")
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
                    

                case "0100040"://�򵰰׶���
                    return MappingGlobaString(GlobalData.LabExamInfo.UrinaryProtein);

                case "0100041"://��Ѫѹ����ʷ
                    return MappingGlobaFamilyDH("��Ѫѹ");

                case "0100042"://��֬Ѫ֢����ʷ
                    return MappingGlobaFamilyDH("��Ѫ֢֬");

                case "0100043"://������Ѫ֢����ʷ
                    return MappingGlobaFamilyDH("������Ѫ֢");

                case "0100044"://���Ĳ�����ʷ
                    return MappingGlobaFamilyDH("���Ĳ�");

                case "0100045"://�Ĺ�Ѫ֢ʷ
                    return MappingGlobaFamilyDH("�Ĺ�");

                case "0100046"://��֫Ѫ�ܲ���
                    return MappingGlobaListSymptoms(GlobalData.AGMInfo.listChronicSymptoms, "������ΧѪ�ܲ���");

                case "0100047"://���Ѫ��
                    return MappingGlobaString(GlobalData.LabExamInfo.BG);

                case "0100048"://�˶��ϰ�
                    return MappingGlobaBool(GlobalData.PhysicalInfo.HasDyskinesia);

                default:
                    return string.Empty;
            }
        }
        
        #endregion

        #region ���ܺ���
        /**************************************************************************************************
         * �޸��ˣ�XY
         * �޸����ڣ�2009.3.20
         * �޸����ݣ���ԭ�е��ظ�����д�ɹ��ܺ���
         * �޸�˵��������ȫ�ֱ���ֵ�Ĳ�ͬ���ͷ����д������ӳ�䣻��Ͻ��۵���ӳ��
         * ************************************************************************************************/

        /// <summary>
        /// ֱ��ӳ��ȫ�ֱ���string�͵�ֵ�������
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
        /// ֱ��ӳ��ȫ�ֱ���bool�͵�ֵ�������
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
        /// ֱ��ӳ��ȫ�ֱ���int�͵�ֵ�������
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
        /// ֱ��ӳ��ȫ�ֱ���֢״��Ϣ��ֵ�������
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
        /// ֱ��ӳ��ȫ�ֱ����м��弲��ʷ�����ݵ������
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
        /// ӳ����Ͻ��۵������
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
        /// ӳ������ȷ������
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
        /// ����BMI����
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
        /// �ж�Ѫѹ���ֵ����
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
        /// �ж���Χ�Ƿ񳬱꺯��
        /// </summary>
        /// <param name="TempWC"></param>
        /// <param name="TempPatSex"></param>
        /// <returns></returns>
        private static string JudgeWC(string TempWC, string TempPatSex)
        {
            if (TempWC != "")
            {
                if (TempPatSex == "��")
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
