using System;
using System.Collections.Generic;
using System.Text;
using CDSSSystemData;
using System.Data;

namespace CDSS
{
    public class DataToGlobalData
    {
        public static void FillGlobalData(string colName, DataRow dr)
        {
            #region 字段映射表
            switch (colName)
            {
                //病人基本信息
                case "PatSEQ":
                    GlobalData.PatBasicInfo.PatSEQ = dr["PatSEQ"].ToString();
                    break;
                case "PatVisitDateTime":
                    GlobalData.PatBasicInfo.PatVisitDateTime = Convert.ToDateTime(dr["PatVisitDateTime"].ToString());
                    break;
                case "PatID":
                    GlobalData.PatBasicInfo.PatID = dr["PatID"].ToString();
                    break;
                case "PatName":
                    GlobalData.PatBasicInfo.PatName = dr["PatName"].ToString();
                    break;
                case "PatSex":
                    GlobalData.PatBasicInfo.PatSex = dr["PatSex"].ToString();
                    break;
                case "PatEducationLevel":
                    GlobalData.PatBasicInfo.PatEducationLevel = dr["PatEducationLevel"].ToString();
                    break;
                case "PatNational":
                    GlobalData.PatBasicInfo.PatNational = dr["PatNational"].ToString();
                    break;
                case "PatIncomeSource":
                    GlobalData.PatBasicInfo.PatIncomeSource = dr["PatIncomeSource"].ToString();
                    break;
                case "PatProfessional":
                    GlobalData.PatBasicInfo.PatProfessional = dr["PatProfessional"].ToString();
                    break;
                case "PatTreatmentCost":
                    GlobalData.PatBasicInfo.PatTreatmentCost = dr["PatTreatmentCost"].ToString();
                    break;
                case "PatIncome":
                    GlobalData.PatBasicInfo.PatIncome = dr["PatIncome"].ToString();
                    break;
                case "PatZipcode":
                    GlobalData.PatBasicInfo.PatZipcode = dr["PatZipcode"].ToString();
                    break;
                case "PatBirthday":
                    GlobalData.PatBasicInfo.PatBirthday = Convert.ToDateTime(dr["PatBirthday"].ToString());
                    break;
                case "PatPhone":
                    GlobalData.PatBasicInfo.PatPhone = dr["PatPhone"].ToString();
                    break;
                case "PatBirthProvince":
                    GlobalData.PatBasicInfo.PatBirthProvince = dr["PatBirthProvince"].ToString();
                    break;
                case "PatBirthCity":
                    GlobalData.PatBasicInfo.PatBirthCity = dr["PatBirthCity"].ToString();
                    break;
                case "PatAddress":
                    GlobalData.PatBasicInfo.PatAddress = dr["PatAddress"].ToString();
                    break;
                case "PatChildCount":
                    GlobalData.PatBasicInfo.PatChildCount = Convert.ToInt32(dr["PatChildCount"].ToString());
                    break;
                case "PatSiblingsCount":
                    GlobalData.PatBasicInfo.PatSiblingsCount = Convert.ToInt32(dr["PatSiblingsCount"].ToString());
                    break;

                #region --现病史信息--
                
                case "糖尿病史":
                    if (dr["糖尿病史"].ToString() == "1")
                    {
                        GlobalData.AGMInfo.HasAGMAbnormal = true;
                    }
                    else
                        GlobalData.AGMInfo.HasAGMAbnormal = false;
                    break;
                case "高血压史":
                    if (dr["高血压史"].ToString() == "1")
                    {
                        GlobalData.HypertensionInfo.HasHypertension = true;
                    }
                    else
                        GlobalData.HypertensionInfo.HasHypertension = false;
                    break;
                case "高脂血症史":
                    if (dr["高脂血症史"].ToString() == "1")
                    {
                        GlobalData.DyslipidemiaInfo.HasDyslipidemia = true;
                    }
                    else
                        GlobalData.DyslipidemiaInfo.HasDyslipidemia = false;
                    break;
                case "高尿酸血症史":
                    if (dr["高尿酸血症史"].ToString() == "1")
                    {
                        GlobalData.HyperuricemiaInfo.HasHyperuricemia = true;
                    }
                    else
                        GlobalData.HyperuricemiaInfo.HasHyperuricemia = false;
                    break;
                case "肿瘤史":
                    if (dr["肿瘤史"].ToString() == "1")
                    {
                        GlobalData.OtherDiseaseHistoryInfo.HasCancer = true;
                    }
                    else
                        GlobalData.OtherDiseaseHistoryInfo.HasCancer = false;
                    break;
                case "肾衰史":
                    if (dr["肾衰史"].ToString() == "1")
                    {　
                      
                        GlobalData.NephropathyInfo.HasRenalAbnormal = true;
                    }
                    else
                        GlobalData.NephropathyInfo.HasRenalAbnormal = false;
                    break;

                //糖代谢异常信息
                case "HasAGMAbnormal":
                    GlobalData.AGMInfo.HasAGMAbnormal = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasAGMAbnormal"].ToString()) ? "0" : dr["HasAGMAbnormal"].ToString()));
                    break;
                case "AbnormalDetectedDateTime":
                    GlobalData.AGMInfo.AbnormalDetectedDateTime = Convert.ToDateTime(dr["AbnormalDetectedDateTime"].ToString());
                    break;
                case "listConfirmedSymptoms":
                    //GlobalData.AGMInfo.listConfirmedSymptoms=dr["listConfirmedSymptoms"].ToString();
                    break;
                case "listAcuteSymptoms":
                    //GlobalData.AGMInfo.listAcuteSymptoms=dr["listAcuteSymptoms"].ToString();
                    break;
                case "listChronicSymptoms":
                    //GlobalData.AGMInfo.listChronicSymptoms=dr["listChronicSymptoms"].ToString();
                    break;
                case "listHypogMedicineInfo":
                    //GlobalData.AGMInfo.listHypogMedicineInfo=dr["listHypogMedicineInfo"].ToString();
                    break;
                case "listChineseMedicineInfo":
                    //GlobalData.AGMInfo.listChineseMedicineInfo=dr["listChineseMedicineInfo"].ToString();
                    break;
                case "listInsulinMedicineInfo":
                    //GlobalData.AGMInfo.listInsulinMedicineInfo=dr["listInsulinMedicineInfo"].ToString();
                    break;


                //高血压信息
                case "HasHypertension":
                    GlobalData.HypertensionInfo.HasHypertension = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasHypertension"].ToString()) ? "0" : dr["HasHypertension"].ToString()));
                    break;
                case "listHypertensionSymptoms":
                    //GlobalData.HypertensionInfo.listHypertensionSymptoms=dr["listHypertensionSymptoms"].ToString();
                    break;
                case "MaxSBP":
                    GlobalData.HypertensionInfo.MaxSBP = dr["MaxSBP"].ToString();
                    break;
                case "MaxDBP":
                    GlobalData.HypertensionInfo.MaxDBP = dr["MaxDBP"].ToString();
                    break;
                case "MinSBP":
                    GlobalData.HypertensionInfo.MinSBP = dr["MinSBP"].ToString();
                    break;
                case "MinDBP":
                    GlobalData.HypertensionInfo.MinDBP = dr["MinDBP"].ToString();
                    break;
                case "listStepDownWestMedicine":
                    //GlobalData.HypertensionInfo.listStepDownWestMedicine=dr["listStepDownWestMedicine"].ToString();
                    break;
                case "listStepDownChineseMedication":
                    //GlobalData.HypertensionInfo.listStepDownChineseMedication=dr["listStepDownChineseMedication"].ToString();
                    break;
                case "BPControlFromYear":
                    GlobalData.HypertensionInfo.BPControlFromYear = dr["BPControlFromYear"].ToString();
                    break;
                case "BPControlToYear":
                    GlobalData.HypertensionInfo.BPControlToYear = dr["BPControlToYear"].ToString();
                    break;
                case "PeacetimeMinSBP":
                    GlobalData.HypertensionInfo.PeacetimeMinSBP = dr["PeacetimeMinSBP"].ToString();
                    break;
                case "PeacetimeMaxSBP":
                    GlobalData.HypertensionInfo.PeacetimeMaxSBP = dr["PeacetimeMaxSBP"].ToString();
                    break;
                case "PeacetimeMinDBP":
                    GlobalData.HypertensionInfo.PeacetimeMinDBP = dr["PeacetimeMinDBP"].ToString();
                    break;
                case "PeacetimeMaxDBP":
                    GlobalData.HypertensionInfo.PeacetimeMaxDBP = dr["PeacetimeMaxDBP"].ToString();
                    break;


                //血脂紊乱信息
                case "HasDyslipidemia":
                    GlobalData.DyslipidemiaInfo.HasDyslipidemia = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasDyslipidemia"].ToString()) ? "0" : dr["HasDyslipidemia"].ToString()));
                    break;
                case "listDyslipidemiaSymptoms":
                    //GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms=dr["listDyslipidemiaSymptoms"].ToString();
                    break;
                case "listLipidlowerDrugs":
                    //GlobalData.DyslipidemiaInfo.listLipidlowerDrugs=dr["listLipidlowerDrugs"].ToString();
                    break;

                //高尿酸血症信息
                case "HasHyperuricemia":
                    GlobalData.HyperuricemiaInfo.HasHyperuricemia = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasHyperuricemia"].ToString()) ? "0" : dr["HasHyperuricemia"].ToString()));
                    break;
                case "HyperuricemiaType":
                    GlobalData.HyperuricemiaInfo.HyperuricemiaType = dr["HyperuricemiaType"].ToString();
                    break;
                case "listHyperuricemiaSymptoms":
                    //GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms=dr["listHyperuricemiaSymptoms"].ToString();
                    break;
                case "HasGoutyArthritis":
                    GlobalData.HyperuricemiaInfo.HasGoutyArthritis = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasGoutyArthritis"].ToString()) ? "0" : dr["HasGoutyArthritis"].ToString()));
                    break;
                case "GoutyArthritisDetectedDateTime":
                    GlobalData.HyperuricemiaInfo.GoutyArthritisDetectedDateTime = Convert.ToDateTime(dr["GoutyArthritisDetectedDateTime"].ToString());
                    break;
                case "HasTophus":
                    GlobalData.HyperuricemiaInfo.HasTophus = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasTophus"].ToString()) ? "0" : dr["HasTophus"].ToString()));
                    break;
                case "TophusPart":
                    GlobalData.HyperuricemiaInfo.TophusPart = dr["TophusPart"].ToString();
                    break;
                case "listUricAcidlowerDrugs":
                    //GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs=dr["listUricAcidlowerDrugs"].ToString();
                    break;
                case "HasJointSwelling":
                    GlobalData.HyperuricemiaInfo.HasJointSwelling = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasJointSwelling"].ToString()) ? "0" : dr["HasJointSwelling"].ToString()));
                    break;


                //非糖尿病肾脏疾病
                case "HasNephropathy":
                    GlobalData.NephropathyInfo.HasNephropathy = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasNephropathy"].ToString()) ? "0" : dr["HasNephropathy"].ToString()));
                    break;
                case "listNephropathySymptoms":
                    //GlobalData.NephropathyInfo.listNephropathySymptoms=dr["listNephropathySymptoms"].ToString();
                    break;
                case "MAXCreatinine":
                    GlobalData.NephropathyInfo.MAXCreatinine = dr["MAXCreatinine"].ToString();
                    break;
                case "MAXBloodUrea":
                    GlobalData.NephropathyInfo.MAXBloodUrea = dr["MAXBloodUrea"].ToString();
                    break;
                case "HasRenalAbnormal":
                    GlobalData.NephropathyInfo.HasRenalAbnormal = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasRenalAbnormal"].ToString()) ? "0" : dr["HasRenalAbnormal"].ToString()));
                    break;
                case "RenalAbnormalDetectedDateTime":
                    GlobalData.NephropathyInfo.RenalAbnormalDetectedDateTime = Convert.ToDateTime(dr["RenalAbnormalDetectedDateTime"].ToString());
                    break;


                //其他疾病史
                case "listCoronaryHeartDisease":
                    //GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease=dr["listCoronaryHeartDisease"].ToString();
                    break;
                case "listCerebrovascularDisease":
                    //GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease=dr["listCerebrovascularDisease"].ToString();
                    break;
                case "HasCholecystitis":
                    GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasCholecystitis"].ToString()) ? "0" : dr["HasCholecystitis"].ToString()));
                    break;
                case "CholecystitisDetectedDateTime":
                    GlobalData.OtherDiseaseHistoryInfo.CholecystitisDetectedDateTime = Convert.ToDateTime(dr["CholecystitisDetectedDateTime"].ToString());
                    break;
                case "HasGallbladderSurgery":
                    GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasGallbladderSurgery"].ToString()) ? "0" : dr["HasGallbladderSurgery"].ToString()));
                    break;
                case "GallbladderSurgeryDateTime":
                    GlobalData.OtherDiseaseHistoryInfo.GallbladderSurgeryDateTime = Convert.ToDateTime(dr["GallbladderSurgeryDateTime"].ToString());
                    break;
                case "listPancreatitis":
                    //GlobalData.OtherDiseaseHistoryInfo.listPancreatitis=dr["listPancreatitis"].ToString();
                    break;
                case "HasCancer":
                    GlobalData.OtherDiseaseHistoryInfo.HasCancer = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasCancer"].ToString()) ? "0" : dr["HasCancer"].ToString()));
                    break;
                case "CancerDetectedDateTime":
                    GlobalData.OtherDiseaseHistoryInfo.CancerDetectedDateTime = Convert.ToDateTime(dr["CancerDetectedDateTime"].ToString());
                    break;
                case "CancerPart":
                    GlobalData.OtherDiseaseHistoryInfo.CancerPart = dr["CancerPart"].ToString();
                    break;
                case "CancerPrognosis":
                    GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis = dr["CancerPrognosis"].ToString();
                    break;
                case "OtherDisease":
                    GlobalData.OtherDiseaseHistoryInfo.OtherDisease = dr["OtherDisease"].ToString();
                    break;
                case "OtherDiseaseDetectedDateTime":
                    GlobalData.OtherDiseaseHistoryInfo.OtherDiseaseDetectedDateTime = Convert.ToDateTime(dr["OtherDiseaseDetectedDateTime"].ToString());
                    break;

                #endregion


                //个人史信息
                case "MaxWeight":
                    GlobalData.PersonalHistoryInfo.MaxWeight = dr["MaxWeight"].ToString();
                    break;
                case "MinWeight":
                    GlobalData.PersonalHistoryInfo.MinWeight = dr["MinWeight"].ToString();
                    break;
                case "MaxWeightAge":
                    GlobalData.PersonalHistoryInfo.MaxWeightAge = dr["MaxWeightAge"].ToString();
                    break;
                case "MaxWeightLastedYears":
                    GlobalData.PersonalHistoryInfo.MaxWeightLastedYears = dr["MaxWeightLastedYears"].ToString();
                    break;
                case "IsSmokeing":
                    GlobalData.PersonalHistoryInfo.IsSmokeing = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["IsSmokeing"].ToString()) ? "0" : dr["IsSmokeing"].ToString()));
                    break;
                case "SmokingAgeBegin":
                    GlobalData.PersonalHistoryInfo.SmokingAgeBegin = dr["SmokingAgeBegin"].ToString();
                    break;
                case "SmokingFrequency":
                    GlobalData.PersonalHistoryInfo.SmokingFrequency = dr["SmokingFrequency"].ToString();
                    break;
                case "RecentSmokingFrequency":
                    GlobalData.PersonalHistoryInfo.RecentSmokingFrequency = dr["RecentSmokingFrequency"].ToString();
                    break;
                case "SmokingAgeEnd":
                    GlobalData.PersonalHistoryInfo.SmokingAgeEnd = dr["SmokingAgeEnd"].ToString();
                    break;
                case "IsDrinking":
                    GlobalData.PersonalHistoryInfo.IsDrinking = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["IsDrinking"].ToString()) ? "0" : dr["IsDrinking"].ToString()));
                    break;
                case "DrinkingAgeBegin":
                    GlobalData.PersonalHistoryInfo.DrinkingAgeBegin = dr["DrinkingAgeBegin"].ToString();
                    break;
                case "DrinkingAgeEnd":
                    GlobalData.PersonalHistoryInfo.DrinkingAgeEnd = dr["DrinkingAgeEnd"].ToString();
                    break;
                case "listDrinkingInfo":
                    //GlobalData.PersonalHistoryInfo.listDrinkingInfo=dr["listDrinkingInfo"].ToString();
                    break;
                case "HasControlDiet":
                    GlobalData.PersonalHistoryInfo.HasControlDiet = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasControlDiet"].ToString()) ? "0" : dr["HasControlDiet"].ToString()));
                    break;
                case "MainFoodAmount":
                    GlobalData.PersonalHistoryInfo.MainFoodAmount = dr["MainFoodAmount"].ToString();
                    break;
                case "OilAmount":
                    GlobalData.PersonalHistoryInfo.OilAmount = dr["OilAmount"].ToString();
                    break;
                case "ProteinAmount":
                    GlobalData.PersonalHistoryInfo.ProteinAmount = dr["ProteinAmount"].ToString();
                    break;
                case "HasBearing":
                    GlobalData.PersonalHistoryInfo.HasBearing = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasBearing"].ToString()) ? "0" : dr["HasBearing"].ToString()));
                    break;
                case "HasGDM":
                    GlobalData.PersonalHistoryInfo.HasGDM = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasGDM"].ToString()) ? "0" : dr["HasGDM"].ToString()));
                    break;
                case "GDMAgeBegin":
                    GlobalData.PersonalHistoryInfo.GDMAgeBegin = dr["GDMAgeBegin"].ToString();
                    break;
                case "IsNeonateHeavierThan4Kg":
                    GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["IsNeonateHeavierThan4Kg"].ToString()) ? "0" : dr["IsNeonateHeavierThan4Kg"].ToString()));
                    break;
                case "NeonateCount":
                    GlobalData.PersonalHistoryInfo.NeonateCount = dr["NeonateCount"].ToString();
                    break;
                case "BearingAge1":
                    GlobalData.PersonalHistoryInfo.BearingAge1 = dr["BearingAge1"].ToString();
                    break;
                case "NeonateWeight1":
                    GlobalData.PersonalHistoryInfo.NeonateWeight1 = dr["NeonateWeight1"].ToString();
                    break;
                case "BearingAge2":
                    GlobalData.PersonalHistoryInfo.BearingAge2 = dr["BearingAge2"].ToString();
                    break;
                case "NeonateWeight2":
                    GlobalData.PersonalHistoryInfo.NeonateWeight2 = dr["NeonateWeight2"].ToString();
                    break;
                case "HasExerciseRecent":
                    GlobalData.PersonalHistoryInfo.HasExerciseRecent = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasExerciseRecent"].ToString()) ? "0" : dr["HasExerciseRecent"].ToString()));
                    break;
                case "listExerciseInfo":
                    //GlobalData.PersonalHistoryInfo.listExerciseInfo=dr["listExerciseInfo"].ToString();
                    break;

                #region 家族史信息
                
                case "糖尿病家族史":
                    if (dr["糖尿病家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "糖尿病,";
                    }
                    break;
                case "高血压家族史":
                    if (dr["高血压家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "高血压,";
                    }
                    break;
                case "高脂血症家族史":
                    if (dr["高脂血症家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "高脂血症,";
                    }
                    break;
                case "高尿酸血症家族史":
                    if (dr["高尿酸血症家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "高尿酸血症,";
                    }
                    break;
                case "冠心病家族史":
                    if (dr["冠心病家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "冠心病,";
                    }
                    break;
                case "心梗家族史":
                    if (dr["心梗家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "心梗,";
                    }
                    break;
                case "脑出血家族史":
                    if (dr["脑出血家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "脑出血,";
                    }
                    break;
                case "脑血栓家族史":
                    if (dr["脑血栓家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "脑血栓,";
                    }
                    break;
                case "胆囊炎家族史":
                    if (dr["胆囊炎家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "胆囊炎,";
                    }
                    break;
                case "胆石症家族史":
                    if (dr["胆石症家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "胆石症,";
                    }
                    break;
                case "肾脏病家族史":
                    if (dr["肾脏病家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "肾脏病,";
                    }
                    break;
                case "肿瘤家族史":
                    if (dr["肿瘤家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "肿瘤,";
                    }
                    break;
                case "肥胖家族史":
                    if (dr["肥胖家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "肥胖,";
                    }
                    break;
                case "其他家族史":
                    if (dr["其他家族史"].ToString() == "1")
                    {
                        GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "其他,";
                    }
                    break;

                //家族疾病史信息
                case "FatherHistory":
                    GlobalData.FamilyDiseaseHistoryInfo.FatherHistory = dr["FatherHistory"].ToString();
                    break;
                case "MotherHistory":
                    GlobalData.FamilyDiseaseHistoryInfo.MotherHistory = dr["MotherHistory"].ToString();
                    break;
                case "SiblingsHistory":
                    GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory = dr["SiblingsHistory"].ToString();
                    break;
                case "ChildrenHistory":
                    GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory = dr["ChildrenHistory"].ToString();
                    break;
                case "OtherHistory":
                    GlobalData.FamilyDiseaseHistoryInfo.OtherHistory = dr["OtherHistory"].ToString();
                    break;

                #endregion

                //体格检查信息
                case "Height":
                    GlobalData.PhysicalInfo.Height = dr["Height"].ToString();
                    break;
                case "Weigh":
                    GlobalData.PhysicalInfo.Weigh = dr["Weigh"].ToString();
                    break;
                case "WC":
                    GlobalData.PhysicalInfo.WC = dr["WC"].ToString();
                    break;
                case "HC":
                    GlobalData.PhysicalInfo.HC = dr["HC"].ToString();
                    break;
                case "HR":
                    GlobalData.PhysicalInfo.HR = dr["HR"].ToString();
                    break;
                case "HasDyskinesia":
                    GlobalData.PhysicalInfo.HasDyskinesia = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasDyskinesia"].ToString()) ? "0" : dr["HasDyskinesia"].ToString()));
                    break;
                case "DyskinesiaPart":
                    GlobalData.PhysicalInfo.DyskinesiaPart = dr["DyskinesiaPart"].ToString();
                    break;
                case "SBP1":
                    GlobalData.PhysicalInfo.SBP1 = dr["SBP1"].ToString();
                    break;
                case "DBP1":
                    GlobalData.PhysicalInfo.DBP1 = dr["DBP1"].ToString();
                    break;
                case "SBP2":
                    GlobalData.PhysicalInfo.SBP2 = dr["SBP2"].ToString();
                    break;
                case "DBP2":
                    GlobalData.PhysicalInfo.DBP2 = dr["DBP2"].ToString();
                    break;


                //实验室检查信息
                case "LabExamDateTime":
                    GlobalData.LabExamInfo.LabExamDateTime = Convert.ToDateTime(dr["LabExamDateTime"].ToString());
                    break;
                case "BG":
                    GlobalData.LabExamInfo.BG = dr["BG"].ToString();
                    break;
                case "FBG":
                    GlobalData.LabExamInfo.FBG = dr["FBG"].ToString();
                    break;
                case "TWOHPBG":
                    GlobalData.LabExamInfo.TWOHPBG = dr["TWOHPBG"].ToString();
                    break;
                case "FoodCount":
                    GlobalData.LabExamInfo.FoodCount = dr["FoodCount"].ToString();
                    break;
                case "OGTTFBG":
                    GlobalData.LabExamInfo.OGTTFBG = dr["OGTTFBG"].ToString();
                    break;
                case "OGTTPBG":
                    GlobalData.LabExamInfo.OGTTPBG = dr["OGTTPBG"].ToString();
                    break;
                case "BeforeBreakfast":
                    GlobalData.LabExamInfo.BeforeBreakfast = dr["BeforeBreakfast"].ToString();
                    break;
                case "AfterBreakfast":
                    GlobalData.LabExamInfo.AfterBreakfast = dr["AfterBreakfast"].ToString();
                    break;
                case "BeforeLunch":
                    GlobalData.LabExamInfo.BeforeLunch = dr["BeforeLunch"].ToString();
                    break;
                case "AfterLunch":
                    GlobalData.LabExamInfo.AfterLunch = dr["AfterLunch"].ToString();
                    break;
                case "BeforeSupper":
                    GlobalData.LabExamInfo.BeforeSupper = dr["BeforeSupper"].ToString();
                    break;
                case "AfterSupper":
                    GlobalData.LabExamInfo.AfterSupper = dr["AfterSupper"].ToString();
                    break;
                case "BeforeSleep":
                    GlobalData.LabExamInfo.BeforeSleep = dr["BeforeSleep"].ToString();
                    break;
                case "LC":
                    GlobalData.LabExamInfo.LC = dr["LC"].ToString();
                    break;
                case "TC":
                    GlobalData.LabExamInfo.TC = dr["TC"].ToString();
                    break;
                case "HDLC":
                    GlobalData.LabExamInfo.HDLC = dr["HDLC"].ToString();
                    break;
                case "TG":
                    GlobalData.LabExamInfo.TG = dr["TG"].ToString();
                    break;
                case "LDLC":
                    GlobalData.LabExamInfo.LDLC = dr["LDLC"].ToString();
                    break;
                case "CR":
                    GlobalData.LabExamInfo.CR = dr["CR"].ToString();
                    break;
                case "AlanineAminotransferase":
                    GlobalData.LabExamInfo.AlanineAminotransferase = dr["AlanineAminotransferase"].ToString();
                    break;
                case "UN":
                    GlobalData.LabExamInfo.UN = dr["UN"].ToString();
                    break;
                case "AspartateAminotransferase":
                    GlobalData.LabExamInfo.AspartateAminotransferase = dr["AspartateAminotransferase"].ToString();
                    break;
                case "ALBCR":
                    GlobalData.LabExamInfo.ALBCR = dr["ALBCR"].ToString();
                    break;
                case "US":
                    GlobalData.LabExamInfo.US = dr["US"].ToString();
                    break;
                case "UrinaryProtein":
                    GlobalData.LabExamInfo.UrinaryProtein = dr["UrinaryProtein"].ToString();
                    break;
                case "NTT":
                    GlobalData.LabExamInfo.NTT = dr["NTT"].ToString();
                    break;
                case "UPH":
                    GlobalData.LabExamInfo.UPH = dr["UPH"].ToString();
                    break;
                case "UUA":
                    GlobalData.LabExamInfo.UUA = dr["UUA"].ToString();
                    break;
                case "HBA1C":
                    GlobalData.LabExamInfo.HBA1C = dr["HBA1C"].ToString();
                    break;
                case "BCL":
                    GlobalData.LabExamInfo.BCL = dr["BCL"].ToString();
                    break;
                case "BUA":
                    GlobalData.LabExamInfo.BUA = dr["BUA"].ToString();
                    break;
                case "BKA":
                    GlobalData.LabExamInfo.BKA = dr["BKA"].ToString();
                    break;
                case "BNA":
                    GlobalData.LabExamInfo.BNA = dr["BNA"].ToString();
                    break;
                case "BCO2CP":
                    GlobalData.LabExamInfo.BCO2CP = dr["BCO2CP"].ToString();
                    break;
                case "BGA":
                    GlobalData.LabExamInfo.BGA = dr["BGA"].ToString();
                    break;
                case "BP":
                    GlobalData.LabExamInfo.BP = dr["BP"].ToString();
                    break;
                case "SerumTotalProtein":
                    GlobalData.LabExamInfo.SerumTotalProtein = dr["SerumTotalProtein"].ToString();
                    break;
                case "SerumAlbumin":
                    GlobalData.LabExamInfo.SerumAlbumin = dr["SerumAlbumin"].ToString();
                    break;
                case "FastingInsulin":
                    GlobalData.LabExamInfo.FastingInsulin = dr["FastingInsulin"].ToString();
                    break;
                case "FastingCPeptide":
                    GlobalData.LabExamInfo.FastingCPeptide = dr["FastingCPeptide"].ToString();
                    break;
                case "PostprandialInsulin":
                    GlobalData.LabExamInfo.PostprandialInsulin = dr["PostprandialInsulin"].ToString();
                    break;
                case "PostprandialCPeptide":
                    GlobalData.LabExamInfo.PostprandialCPeptide = dr["PostprandialCPeptide"].ToString();
                    break;
                case "ICA":
                    GlobalData.LabExamInfo.ICA = dr["ICA"].ToString();
                    break;
                case "GDA65":
                    GlobalData.LabExamInfo.GDA65 = dr["GDA65"].ToString();
                    break;


                //其他检查信息
                case "HasECGAbnormal":
                    GlobalData.OtherExamInfo.HasECGAbnormal = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasECGAbnormal"].ToString()) ? "0" : dr["HasECGAbnormal"].ToString()));
                    break;
                case "ECGAbnormalType":
                    GlobalData.OtherExamInfo.ECGAbnormalType = dr["ECGAbnormalType"].ToString();
                    break;
                case "listVascularUltrasound":
                    //GlobalData.OtherExamInfo.listVascularUltrasound=dr["listVascularUltrasound"].ToString();
                    break;
                case "listOtherExamAbnormal":
                    //GlobalData.OtherExamInfo.listOtherExamAbnormal=dr["listOtherExamAbnormal"].ToString();
                    break;


                //诊断结论
                case "HasMS":
                    GlobalData.DiagnosedResult.HasMS = dr["HasMS"].ToString();
                    break;
                case "RiskDegreeCode":
                    GlobalData.DiagnosedResult.RiskDegreeCode = dr["RiskDegreeCode"].ToString();
                    break;
                case "RiskDegree":
                    GlobalData.DiagnosedResult.RiskDegree = dr["RiskDegree"].ToString();
                    break;
                case "DiseaseDiagnosedResultList":
                    //GlobalData.DiagnosedResult.DiseaseDiagnosedResultList=dr["DiseaseDiagnosedResultList"].ToString();
                    break;
                case "ReasoningDiseaseDiagnosedResultList":
                    //GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList=dr["ReasoningDiseaseDiagnosedResultList"].ToString();
                    break;


                //膳食处方
                case "DietType":
                    GlobalData.DietSuggestion.DietType = dr["DietType"].ToString();
                    break;
                case "TotalEnergy":
                    GlobalData.DietSuggestion.TotalEnergy = dr["TotalEnergy"].ToString();
                    break;
                case "TotalWater":
                    GlobalData.DietSuggestion.TotalWater = dr["TotalWater"].ToString();
                    break;
                case "CarboPercent":
                    GlobalData.DietSuggestion.CarboPercent = dr["CarboPercent"].ToString();
                    break;
                case "CarboCount":
                    GlobalData.DietSuggestion.CarboCount = dr["CarboCount"].ToString();
                    break;
                case "CerealCount":
                    GlobalData.DietSuggestion.CerealCount = dr["CerealCount"].ToString();
                    break;
                case "CerealDetail":
                    GlobalData.DietSuggestion.CerealDetail = dr["CerealDetail"].ToString();
                    break;
                case "CerealBreakfastDetail":
                    GlobalData.DietSuggestion.CerealBreakfastDetail = dr["CerealBreakfastDetail"].ToString();
                    break;
                case "CerealLunchDetail":
                    GlobalData.DietSuggestion.CerealLunchDetail = dr["CerealLunchDetail"].ToString();
                    break;
                case "CerealSupperDetail":
                    GlobalData.DietSuggestion.CerealSupperDetail = dr["CerealSupperDetail"].ToString();
                    break;
                case "Fruitcount":
                    GlobalData.DietSuggestion.Fruitcount = dr["Fruitcount"].ToString();
                    break;
                case "FruitDetail":
                    GlobalData.DietSuggestion.FruitDetail = dr["FruitDetail"].ToString();
                    break;
                case "GreenstuffCount":
                    GlobalData.DietSuggestion.GreenstuffCount = dr["GreenstuffCount"].ToString();
                    break;
                case "GreenstuffLunchDetail":
                    GlobalData.DietSuggestion.GreenstuffLunchDetail = dr["GreenstuffLunchDetail"].ToString();
                    break;
                case "GreenstuffSupperDetail":
                    GlobalData.DietSuggestion.GreenstuffSupperDetail = dr["GreenstuffSupperDetail"].ToString();
                    break;
                case "GreenstuffDetail":
                    GlobalData.DietSuggestion.GreenstuffDetail = dr["GreenstuffDetail"].ToString();
                    break;
                case "ProteinPercent":
                    GlobalData.DietSuggestion.ProteinPercent = dr["ProteinPercent"].ToString();
                    break;
                case "ProteinCount":
                    GlobalData.DietSuggestion.ProteinCount = dr["ProteinCount"].ToString();
                    break;
                case "DairyCount":
                    GlobalData.DietSuggestion.DairyCount = dr["DairyCount"].ToString();
                    break;
                case "DairyDetail":
                    GlobalData.DietSuggestion.DairyDetail = dr["DairyDetail"].ToString();
                    break;
                case "EggCount":
                    GlobalData.DietSuggestion.EggCount = dr["EggCount"].ToString();
                    break;
                case "EggDetail":
                    GlobalData.DietSuggestion.EggDetail = dr["EggDetail"].ToString();
                    break;
                case "MeatCount":
                    GlobalData.DietSuggestion.MeatCount = dr["MeatCount"].ToString();
                    break;
                case "MeatDetail":
                    GlobalData.DietSuggestion.MeatDetail = dr["MeatDetail"].ToString();
                    break;
                case "BeanProductCount":
                    GlobalData.DietSuggestion.BeanProductCount = dr["BeanProductCount"].ToString();
                    break;
                case "BeanProductDetail":
                    GlobalData.DietSuggestion.BeanProductDetail = dr["BeanProductDetail"].ToString();
                    break;
                case "FatPercent":
                    GlobalData.DietSuggestion.FatPercent = dr["FatPercent"].ToString();
                    break;
                case "FatCount":
                    GlobalData.DietSuggestion.FatCount = dr["FatCount"].ToString();
                    break;
                case "VegetableOilCount":
                    GlobalData.DietSuggestion.VegetableOilCount = dr["VegetableOilCount"].ToString();
                    break;
                case "VegetableOilDetail":
                    GlobalData.DietSuggestion.VegetableOilDetail = dr["VegetableOilDetail"].ToString();
                    break;
                case "OtherFatFoodCount":
                    GlobalData.DietSuggestion.OtherFatFoodCount = dr["OtherFatFoodCount"].ToString();
                    break;
                case "OtherFatFoodDetail":
                    GlobalData.DietSuggestion.OtherFatFoodDetail = dr["OtherFatFoodDetail"].ToString();
                    break;
                case "LimitedFood":
                    GlobalData.DietSuggestion.LimitedFood = dr["LimitedFood"].ToString();
                    break;
                case "AvoidFood":
                    GlobalData.DietSuggestion.AvoidFood = dr["AvoidFood"].ToString();
                    break;
                case "DietSuggestion.DataNeeded":
                    GlobalData.DietSuggestion.DataNeeded = dr["DietSuggestion.DataNeeded"].ToString();
                    break;


                //运动建议
                case "ExerciseTarget":
                    GlobalData.ExerciseSuggestion.ExerciseTarget = dr["ExerciseTarget"].ToString();
                    break;
                case "EnergyCost":
                    GlobalData.ExerciseSuggestion.EnergyCost = dr["EnergyCost"].ToString();
                    break;
                case "NoIntensityExercise":
                    GlobalData.ExerciseSuggestion.NoIntensityExercise = dr["NoIntensityExercise"].ToString();
                    break;
                case "NoIntensityExerciseItems":
                    GlobalData.ExerciseSuggestion.NoIntensityExerciseItems = dr["NoIntensityExerciseItems"].ToString();
                    break;
                case "LowIntensityExercise":
                    GlobalData.ExerciseSuggestion.LowIntensityExercise = dr["LowIntensityExercise"].ToString();
                    break;
                case "LowIntensityExerciseItems":
                    GlobalData.ExerciseSuggestion.LowIntensityExerciseItems = dr["LowIntensityExerciseItems"].ToString();
                    break;
                case "MiddleIntensityExercise":
                    GlobalData.ExerciseSuggestion.MiddleIntensityExercise = dr["MiddleIntensityExercise"].ToString();
                    break;
                case "MiddleIntensityExerciseItems":
                    GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems = dr["MiddleIntensityExerciseItems"].ToString();
                    break;
                case "HighIntensityExercise":
                    GlobalData.ExerciseSuggestion.HighIntensityExercise = dr["HighIntensityExercise"].ToString();
                    break;
                case "HighIntensityExerciseItems":
                    GlobalData.ExerciseSuggestion.HighIntensityExerciseItems = dr["HighIntensityExerciseItems"].ToString();
                    break;
                case "ExerciseSuggestion.DataNeeded":
                    GlobalData.ExerciseSuggestion.DataNeeded = dr["ExerciseSuggestion.DataNeeded"].ToString();
                    break;
                default:
                    break;
            }

            #endregion
        }
        public static string FindingGlobalData(string colName) 
        {
            string value = "";
            //string[] history = GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Split(",");
            //string joinedHistory=history.
            #region 字段映射表
            switch (colName)
            {
                //病人基本信息
                case "PatSEQ":
                    value=GlobalData.PatBasicInfo.PatSEQ;
                    break;
                case "PatVisitDateTime":
                    //VisitDateTime需要进一步确定是否这样
                    value=GlobalData.PatBasicInfo.PatVisitDateTime.ToShortDateString(); 
                    break;
                case "PatID":
                    value = GlobalData.PatBasicInfo.PatID;
                    break;
                case "PatName":
                    value = GlobalData.PatBasicInfo.PatName;
                    break;
                case "PatSex":
                    value = GlobalData.PatBasicInfo.PatSex ;
                    break;
                case "PatEducationLevel":
                    value = GlobalData.PatBasicInfo.PatEducationLevel;
                    break;
                case "PatNational":
                    value = GlobalData.PatBasicInfo.PatNational;
                    break;
                case "PatIncomeSource":
                    value = GlobalData.PatBasicInfo.PatIncomeSource;
                    break;
                case "PatProfessional":
                    value = GlobalData.PatBasicInfo.PatProfessional;
                    break;
                case "PatTreatmentCost":
                    value = GlobalData.PatBasicInfo.PatTreatmentCost;
                    break;
                case "PatIncome":
                    value = GlobalData.PatBasicInfo.PatIncome;
                    break;
                case "PatZipcode":
                    value = GlobalData.PatBasicInfo.PatZipcode ;
                    break;
                case "PatBirthday":
                    value = GlobalData.PatBasicInfo.PatBirthday.ToShortDateString();
                    break;
                case "PatPhone":
                    value = GlobalData.PatBasicInfo.PatPhone;
                    break;
                case "PatBirthProvince":
                    value = GlobalData.PatBasicInfo.PatBirthProvince;
                    break;
                case "PatBirthCity":
                    value = GlobalData.PatBasicInfo.PatBirthCity ;
                    break;
                case "PatAddress":
                    value = GlobalData.PatBasicInfo.PatAddress ;
                    break;
                case "PatChildCount":
                    value = GlobalData.PatBasicInfo.PatChildCount.ToString();
                    break;
                case "PatSiblingsCount":
                    value = GlobalData.PatBasicInfo.PatSiblingsCount.ToString();
                    break;

                #region --现病史信息--

                case "糖尿病史":
                    if (GlobalData.AGMInfo.HasAGMAbnormal == true)
                    {
                        value = "1";
                    }
                    break;
                case "高血压史":
                    if (GlobalData.HypertensionInfo.HasHypertension == true)
                    {
                        value = "1";
                    }
                    break;
                case "高脂血症史":
                    if ( GlobalData.DyslipidemiaInfo.HasDyslipidemia == true)
                    {
                        value = "1";
                    }
                    break;
                case "高尿酸血症史":
                    if (GlobalData.HyperuricemiaInfo.HasHyperuricemia == true)
                    {
                        value = "1"; 
                    }
                    break;
                case "肿瘤史":
                    if (GlobalData.OtherDiseaseHistoryInfo.HasCancer == true)
                    {
                        value = "1"; 
                    }
                    break;
                case "肾衰史":
                    if (GlobalData.NephropathyInfo.HasRenalAbnormal == true)
                    {
                        value = "1";
                    }
                    break;

                //糖代谢异常信息
                case "HasAGMAbnormal":
                    if (GlobalData.AGMInfo.HasAGMAbnormal == true)
                    {
                        value = "1";
                    }
                    break;
                case "AbnormalDetectedDateTime":
                    //GlobalData.AGMInfo.AbnormalDetectedDateTime = Convert.ToDateTime(dr["AbnormalDetectedDateTime"].ToString());
                    value = GlobalData.AGMInfo.AbnormalDetectedDateTime.ToShortTimeString();
                    break;
                case "listConfirmedSymptoms":
                    //GlobalData.AGMInfo.listConfirmedSymptoms=dr["listConfirmedSymptoms"].ToString();
                    break;
                case "listAcuteSymptoms":
                    //GlobalData.AGMInfo.listAcuteSymptoms=dr["listAcuteSymptoms"].ToString();
                    break;
                case "listChronicSymptoms":
                    //GlobalData.AGMInfo.listChronicSymptoms=dr["listChronicSymptoms"].ToString();
                    break;
                case "listHypogMedicineInfo":
                    //GlobalData.AGMInfo.listHypogMedicineInfo=dr["listHypogMedicineInfo"].ToString();
                    break;
                case "listChineseMedicineInfo":
                    //GlobalData.AGMInfo.listChineseMedicineInfo=dr["listChineseMedicineInfo"].ToString();
                    break;
                case "listInsulinMedicineInfo":
                    //GlobalData.AGMInfo.listInsulinMedicineInfo=dr["listInsulinMedicineInfo"].ToString();
                    break;


                //高血压信息
                case "HasHypertension":
                    //GlobalData.HypertensionInfo.HasHypertension = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasHypertension"].ToString()) ? "0" : dr["HasHypertension"].ToString()));
                    if (GlobalData.HypertensionInfo.HasHypertension == true)
                    {
                        value = "1";
                    }
                    break;
                case "listHypertensionSymptoms":
                    //GlobalData.HypertensionInfo.listHypertensionSymptoms=dr["listHypertensionSymptoms"].ToString();
                    break;
                case "MaxSBP":
                    value = GlobalData.HypertensionInfo.MaxSBP;
                    break;
                case "MaxDBP":
                    value = GlobalData.HypertensionInfo.MaxDBP;
                    break;
                case "MinSBP":
                    value = GlobalData.HypertensionInfo.MinSBP ;
                    break;
                case "MinDBP":
                    value = GlobalData.HypertensionInfo.MinDBP ;
                    break;
                case "listStepDownWestMedicine":
                    //GlobalData.HypertensionInfo.listStepDownWestMedicine=dr["listStepDownWestMedicine"].ToString();
                    break;
                case "listStepDownChineseMedication":
                    //GlobalData.HypertensionInfo.listStepDownChineseMedication=dr["listStepDownChineseMedication"].ToString();
                    break;
                case "BPControlFromYear":
                    value = GlobalData.HypertensionInfo.BPControlFromYear ;
                    break;
                case "BPControlToYear":
                    value = GlobalData.HypertensionInfo.BPControlToYear;
                    break;
                case "PeacetimeMinSBP":
                    value = GlobalData.HypertensionInfo.PeacetimeMinSBP;
                    break;
                case "PeacetimeMaxSBP":
                    value = GlobalData.HypertensionInfo.PeacetimeMaxSBP;
                    break;
                case "PeacetimeMinDBP":
                    value = GlobalData.HypertensionInfo.PeacetimeMinDBP;
                    break;
                case "PeacetimeMaxDBP":
                    value = GlobalData.HypertensionInfo.PeacetimeMaxDBP;
                    break;


                //血脂紊乱信息
                case "HasDyslipidemia":
                    //GlobalData.DyslipidemiaInfo.HasDyslipidemia = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasDyslipidemia"].ToString()) ? "0" : dr["HasDyslipidemia"].ToString()));
                    if (GlobalData.DyslipidemiaInfo.HasDyslipidemia == true)
                    {
                        value = "1";
                    }
                    break;
                case "listDyslipidemiaSymptoms":
                    //GlobalData.DyslipidemiaInfo.listDyslipidemiaSymptoms=dr["listDyslipidemiaSymptoms"].ToString();
                    break;
                case "listLipidlowerDrugs":
                    //GlobalData.DyslipidemiaInfo.listLipidlowerDrugs=dr["listLipidlowerDrugs"].ToString();
                    break;

                //高尿酸血症信息
                case "HasHyperuricemia":
                    //GlobalData.HyperuricemiaInfo.HasHyperuricemia = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasHyperuricemia"].ToString()) ? "0" : dr["HasHyperuricemia"].ToString()));
                    if (GlobalData.HyperuricemiaInfo.HasHyperuricemia == true)
                    {
                        value = "1";
                    }
                    break;
                case "HyperuricemiaType":
                    value=GlobalData.HyperuricemiaInfo.HyperuricemiaType;
                    break;
                case "listHyperuricemiaSymptoms":
                    //GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms=dr["listHyperuricemiaSymptoms"].ToString();
                    value = GlobalData.HyperuricemiaInfo.listHyperuricemiaSymptoms.ToString();
                    break;
                case "HasGoutyArthritis":
                    //GlobalData.HyperuricemiaInfo.HasGoutyArthritis = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasGoutyArthritis"].ToString()) ? "0" : dr["HasGoutyArthritis"].ToString()));
                    break;
                case "GoutyArthritisDetectedDateTime":
                    value=GlobalData.HyperuricemiaInfo.GoutyArthritisDetectedDateTime.ToShortTimeString(); 
                    break;
                case "HasTophus":
                    //GlobalData.HyperuricemiaInfo.HasTophus = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasTophus"].ToString()) ? "0" : dr["HasTophus"].ToString()));
                    break;
                case "TophusPart":
                    value=GlobalData.HyperuricemiaInfo.TophusPart;
                    break;
                case "listUricAcidlowerDrugs":
                    //GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs=dr["listUricAcidlowerDrugs"].ToString();
                    value = GlobalData.HyperuricemiaInfo.listUricAcidlowerDrugs.ToString();
                    break;
                case "HasJointSwelling":
                    //GlobalData.HyperuricemiaInfo.HasJointSwelling = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasJointSwelling"].ToString()) ? "0" : dr["HasJointSwelling"].ToString()));
                    break;


                //非糖尿病肾脏疾病
                case "HasNephropathy":
                    //GlobalData.NephropathyInfo.HasNephropathy = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasNephropathy"].ToString()) ? "0" : dr["HasNephropathy"].ToString()));
                    break;
                case "listNephropathySymptoms":
                    //GlobalData.NephropathyInfo.listNephropathySymptoms=dr["listNephropathySymptoms"].ToString();
                    break;
                case "MAXCreatinine":
                    value=GlobalData.NephropathyInfo.MAXCreatinine;
                    break;
                case "MAXBloodUrea":
                    value=GlobalData.NephropathyInfo.MAXBloodUrea;
                    break;
                case "HasRenalAbnormal":
                    //GlobalData.NephropathyInfo.HasRenalAbnormal = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasRenalAbnormal"].ToString()) ? "0" : dr["HasRenalAbnormal"].ToString()));
                    break;
                case "RenalAbnormalDetectedDateTime":
                    value = GlobalData.NephropathyInfo.RenalAbnormalDetectedDateTime.ToString();
                    break;


                //其他疾病史
                case "listCoronaryHeartDisease":
                    //GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease=dr["listCoronaryHeartDisease"].ToString();
                    value = GlobalData.OtherDiseaseHistoryInfo.listCoronaryHeartDisease.ToString();
                    break;
                case "listCerebrovascularDisease":
                    //GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease=dr["listCerebrovascularDisease"].ToString();
                    value = GlobalData.OtherDiseaseHistoryInfo.listCerebrovascularDisease.ToString();
                    break;
                case "HasCholecystitis":
                    //GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasCholecystitis"].ToString()) ? "0" : dr["HasCholecystitis"].ToString()));
                    value = GlobalData.OtherDiseaseHistoryInfo.HasCholecystitis.ToString();
                    break;
                case "CholecystitisDetectedDateTime":
                    value=GlobalData.OtherDiseaseHistoryInfo.CholecystitisDetectedDateTime.ToString();
                    break;
                case "HasGallbladderSurgery":
                    //GlobalData.OtherDiseaseHistoryInfo.HasGallbladderSurgery = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasGallbladderSurgery"].ToString()) ? "0" : dr["HasGallbladderSurgery"].ToString()));
                    break;
                case "GallbladderSurgeryDateTime":
                    value = GlobalData.OtherDiseaseHistoryInfo.GallbladderSurgeryDateTime.ToString();
                    break;
                case "listPancreatitis":
                    //GlobalData.OtherDiseaseHistoryInfo.listPancreatitis=dr["listPancreatitis"].ToString();
                    value = GlobalData.OtherDiseaseHistoryInfo.listPancreatitis.ToString();
                    break;
                case "HasCancer":
                    //GlobalData.OtherDiseaseHistoryInfo.HasCancer = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasCancer"].ToString()) ? "0" : dr["HasCancer"].ToString()));
                    break;
                case "CancerDetectedDateTime":
                    value = GlobalData.OtherDiseaseHistoryInfo.CancerDetectedDateTime .ToShortDateString();
                    break;
                case "CancerPart":
                    value = GlobalData.OtherDiseaseHistoryInfo.CancerPart;
                    break;
                case "CancerPrognosis":
                    value = GlobalData.OtherDiseaseHistoryInfo.CancerPrognosis;
                    break;
                case "OtherDisease":
                    value = GlobalData.OtherDiseaseHistoryInfo.OtherDisease ;
                    break;
                case "OtherDiseaseDetectedDateTime":
                    value = GlobalData.OtherDiseaseHistoryInfo.OtherDiseaseDetectedDateTime .ToString();
                    break;

                #endregion


                //个人史信息
                case "MaxWeight":
                    value = GlobalData.PersonalHistoryInfo.MaxWeight;
                    break;
                case "MinWeight":
                    value = GlobalData.PersonalHistoryInfo.MinWeight ;
                    break;
                case "MaxWeightAge":
                    value = GlobalData.PersonalHistoryInfo.MaxWeightAge ;
                    break;
                case "MaxWeightLastedYears":
                    value = GlobalData.PersonalHistoryInfo.MaxWeightLastedYears ;
                    break;
                case "IsSmokeing":
                    //GlobalData.PersonalHistoryInfo.IsSmokeing = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["IsSmokeing"].ToString()) ? "0" : dr["IsSmokeing"].ToString()));
                    break;
                case "SmokingAgeBegin":
                    value = GlobalData.PersonalHistoryInfo.SmokingAgeBegin;
                    break;
                case "SmokingFrequency":
                    value = GlobalData.PersonalHistoryInfo.SmokingFrequency;
                    break;
                case "RecentSmokingFrequency":
                    value = GlobalData.PersonalHistoryInfo.RecentSmokingFrequency;
                    break;
                case "SmokingAgeEnd":
                    value = GlobalData.PersonalHistoryInfo.SmokingAgeEnd ;
                    break;
                case "IsDrinking":
                    //GlobalData.PersonalHistoryInfo.IsDrinking = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["IsDrinking"].ToString()) ? "0" : dr["IsDrinking"].ToString()));
                    break;
                case "DrinkingAgeBegin":
                    value = GlobalData.PersonalHistoryInfo.DrinkingAgeBegin;
                    break;
                case "DrinkingAgeEnd":
                    value = GlobalData.PersonalHistoryInfo.DrinkingAgeEnd ;
                    break;
                case "listDrinkingInfo":
                    //GlobalData.PersonalHistoryInfo.listDrinkingInfo=dr["listDrinkingInfo"].ToString();
                    break;
                case "HasControlDiet":
                    //GlobalData.PersonalHistoryInfo.HasControlDiet = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasControlDiet"].ToString()) ? "0" : dr["HasControlDiet"].ToString()));
                    break;
                case "MainFoodAmount":
                    value = GlobalData.PersonalHistoryInfo.MainFoodAmount;
                    break;
                case "OilAmount":
                    value = GlobalData.PersonalHistoryInfo.OilAmount;
                    break;
                case "ProteinAmount":
                    value = GlobalData.PersonalHistoryInfo.ProteinAmount;
                    break;
                case "HasBearing":
                    //GlobalData.PersonalHistoryInfo.HasBearing = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasBearing"].ToString()) ? "0" : dr["HasBearing"].ToString()));
                    break;
                case "HasGDM":
                    //GlobalData.PersonalHistoryInfo.HasGDM = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasGDM"].ToString()) ? "0" : dr["HasGDM"].ToString()));
                    break;
                case "GDMAgeBegin":
                    value = GlobalData.PersonalHistoryInfo.GDMAgeBegin;
                    break;
                case "IsNeonateHeavierThan4Kg":
                    //GlobalData.PersonalHistoryInfo.IsNeonateHeavierThan4Kg = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["IsNeonateHeavierThan4Kg"].ToString()) ? "0" : dr["IsNeonateHeavierThan4Kg"].ToString()));
                    break;
                case "NeonateCount":
                    value = GlobalData.PersonalHistoryInfo.NeonateCount;
                    break;
                case "BearingAge1":
                    value = GlobalData.PersonalHistoryInfo.BearingAge1;
                    break;
                case "NeonateWeight1":
                    value = GlobalData.PersonalHistoryInfo.NeonateWeight1;
                    break;
                case "BearingAge2":
                    value = GlobalData.PersonalHistoryInfo.BearingAge2 ;
                    break;
                case "NeonateWeight2":
                    value = GlobalData.PersonalHistoryInfo.NeonateWeight2;
                    break;
                case "HasExerciseRecent":
                    //GlobalData.PersonalHistoryInfo.HasExerciseRecent = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasExerciseRecent"].ToString()) ? "0" : dr["HasExerciseRecent"].ToString()));
                    break;
                case "listExerciseInfo":
                    //GlobalData.PersonalHistoryInfo.listExerciseInfo=dr["listExerciseInfo"].ToString();
                    break;

                #region 家族史信息

                    
                case "糖尿病家族史":
                    //if (dr["糖尿病家族史"].ToString() == "1")
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("糖尿病"))
                    {
                        //GlobalData.FamilyDiseaseHistoryInfo.OtherHistory += "糖尿病,";
                        value = "1";
                    }
                    break;
                case "高血压家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("高血压"))
                    {
                        value = "1";
                    }
                    break;
                case "高脂血症家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("高脂血症"))
                    {
                        value = "1";
                    }
                    break;
                case "高尿酸血症家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("高尿酸血症"))
                    {
                        value = "1";
                    }
                    break;
                case "冠心病家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("冠心病"))
                    {
                        value = "1";
                    }
                    break;
                case "心梗家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("心梗"))
                    {
                        value = "1";
                    }
                    break;
                case "脑出血家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("脑出血"))
                    {
                        value = "1";
                    }
                    break;
                case "脑血栓家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("脑血栓"))
                    {
                        value = "1";
                    }
                    break;
                case "胆囊炎家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("胆囊炎"))
                    {
                        value = "1";
                    }
                    break;
                case "胆石症家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("胆石症"))
                    {
                        value = "1"; 
                    }
                    break;
                case "肾脏病家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("肾脏病"))
                    {
                        value = "1";
                    }
                    break;
                case "肿瘤家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("肿瘤"))
                    {
                        value = "1";
                    }
                    break;
                case "肥胖家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("肥胖"))
                    {
                        value = "1";
                    }
                    break;
                case "其他家族史":
                    if (GlobalData.FamilyDiseaseHistoryInfo.OtherHistory.Contains("其他"))
                    {
                        value = "1";
                    }
                    break;

                //家族疾病史信息
                case "FatherHistory":
                    value = GlobalData.FamilyDiseaseHistoryInfo.FatherHistory ;
                    break;
                case "MotherHistory":
                    value = GlobalData.FamilyDiseaseHistoryInfo.MotherHistory ;
                    break;
                case "SiblingsHistory":
                    value = GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory;
                    break;
                case "ChildrenHistory":
                    value = GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory;
                    break;
                case "OtherHistory":
                    value = GlobalData.FamilyDiseaseHistoryInfo.OtherHistory;
                    break;

                #endregion

                //体格检查信息
                case "Height":
                    value = GlobalData.PhysicalInfo.Height;
                    break;
                case "Weigh":
                    value = GlobalData.PhysicalInfo.Weigh;
                    break;
                case "WC":
                    value = GlobalData.PhysicalInfo.WC;
                    break;
                case "HC":
                    value = GlobalData.PhysicalInfo.HC ;
                    break;
                case "HR":
                    value = GlobalData.PhysicalInfo.HR;
                    break;
                case "HasDyskinesia":
                    //GlobalData.PhysicalInfo.HasDyskinesia = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasDyskinesia"].ToString()) ? "0" : dr["HasDyskinesia"].ToString()));
                    break;
                case "DyskinesiaPart":
                    value = GlobalData.PhysicalInfo.DyskinesiaPart;
                    break;
                case "SBP1":
                    value = GlobalData.PhysicalInfo.SBP1;
                    break;
                case "DBP1":
                    value = GlobalData.PhysicalInfo.DBP1;
                    break;
                case "SBP2":
                    value = GlobalData.PhysicalInfo.SBP2;
                    break;
                case "DBP2":
                    value = GlobalData.PhysicalInfo.DBP2;
                    break;


                //实验室检查信息
                case "LabExamDateTime":
                    value = GlobalData.LabExamInfo.LabExamDateTime.ToString();
                    break;
                case "BG":
                    value = GlobalData.LabExamInfo.BG;
                    break;
                case "FBG":
                    value = GlobalData.LabExamInfo.FBG;
                    break;
                case "TWOHPBG":
                    value = GlobalData.LabExamInfo.TWOHPBG;
                    break;
                case "FoodCount":
                    value = GlobalData.LabExamInfo.FoodCount;
                    break;
                case "OGTTFBG":
                    value = GlobalData.LabExamInfo.OGTTFBG;
                    break;
                case "OGTTPBG":
                    value = GlobalData.LabExamInfo.OGTTPBG;
                    break;
                case "BeforeBreakfast":
                    value = GlobalData.LabExamInfo.BeforeBreakfast;
                    break;
                case "AfterBreakfast":
                    value = GlobalData.LabExamInfo.AfterBreakfast;
                    break;
                case "BeforeLunch":
                    value = GlobalData.LabExamInfo.BeforeLunch;
                    break;
                case "AfterLunch":
                    value = GlobalData.LabExamInfo.AfterLunch;
                    break;
                case "BeforeSupper":
                    value = GlobalData.LabExamInfo.BeforeSupper;
                    break;
                case "AfterSupper":
                    value = GlobalData.LabExamInfo.AfterSupper;
                    break;
                case "BeforeSleep":
                    value = GlobalData.LabExamInfo.BeforeSleep;
                    break;
                case "LC":
                    value = GlobalData.LabExamInfo.LC;
                    break;
                case "TC":
                    value = GlobalData.LabExamInfo.TC ;
                    break;
                case "HDLC":
                    value = GlobalData.LabExamInfo.HDLC;
                    break;
                case "TG":
                    value = GlobalData.LabExamInfo.TG;
                    break;
                case "LDLC":
                    value = GlobalData.LabExamInfo.LDLC;
                    break;
                case "CR":
                    value = GlobalData.LabExamInfo.CR ;
                    break;
                case "AlanineAminotransferase":
                    value = GlobalData.LabExamInfo.AlanineAminotransferase;
                    break;
                case "UN":
                    value = GlobalData.LabExamInfo.UN;
                    break;
                case "AspartateAminotransferase":
                    value = GlobalData.LabExamInfo.AspartateAminotransferase;
                    break;
                case "ALBCR":
                    value = GlobalData.LabExamInfo.ALBCR;
                    break;
                case "US":
                    value = GlobalData.LabExamInfo.US ;
                    break;
                case "UrinaryProtein":
                    value = GlobalData.LabExamInfo.UrinaryProtein;
                    break;
                case "NTT":
                    value = GlobalData.LabExamInfo.NTT;
                    break;
                case "UPH":
                    value = GlobalData.LabExamInfo.UPH;
                    break;
                case "UUA":
                    value = GlobalData.LabExamInfo.UUA ;
                    break;
                case "HBA1C":
                    value = GlobalData.LabExamInfo.HBA1C;
                    break;
                case "BCL":
                    value = GlobalData.LabExamInfo.BCL;
                    break;
                case "BUA":
                    value = GlobalData.LabExamInfo.BUA ;
                    break;
                case "BKA":
                    value = GlobalData.LabExamInfo.BKA;
                    break;
                case "BNA":
                    value = GlobalData.LabExamInfo.BNA ;
                    break;
                case "BCO2CP":
                    value = GlobalData.LabExamInfo.BCO2CP;
                    break;
                case "BGA":
                    value = GlobalData.LabExamInfo.BGA ;
                    break;
                case "BP":
                    value = GlobalData.LabExamInfo.BP;
                    break;
                case "SerumTotalProtein":
                    value = GlobalData.LabExamInfo.SerumTotalProtein;
                    break;
                case "SerumAlbumin":
                    value = GlobalData.LabExamInfo.SerumAlbumin;
                    break;
                case "FastingInsulin":
                    value = GlobalData.LabExamInfo.FastingInsulin;
                    break;
                case "FastingCPeptide":
                    value = GlobalData.LabExamInfo.FastingCPeptide;
                    break;
                case "PostprandialInsulin":
                    value = GlobalData.LabExamInfo.PostprandialInsulin;
                    break;
                case "PostprandialCPeptide":
                    value = GlobalData.LabExamInfo.PostprandialCPeptide;
                    break;
                case "ICA":
                    value = GlobalData.LabExamInfo.ICA;
                    break;
                case "GDA65":
                    value = GlobalData.LabExamInfo.GDA65;
                    break;


                //其他检查信息
                case "HasECGAbnormal":
                    //GlobalData.OtherExamInfo.HasECGAbnormal = Convert.ToBoolean(int.Parse(string.IsNullOrEmpty(dr["HasECGAbnormal"].ToString()) ? "0" : dr["HasECGAbnormal"].ToString()));
                    break;
                case "ECGAbnormalType":
                    value = GlobalData.OtherExamInfo.ECGAbnormalType;
                    break;
                case "listVascularUltrasound":
                    //GlobalData.OtherExamInfo.listVascularUltrasound=dr["listVascularUltrasound"].ToString();
                    break;
                case "listOtherExamAbnormal":
                    //GlobalData.OtherExamInfo.listOtherExamAbnormal=dr["listOtherExamAbnormal"].ToString();
                    break;


                //诊断结论
                case "HasMS":
                    value = GlobalData.DiagnosedResult.HasMS ;
                    break;
                case "RiskDegreeCode":
                    value = GlobalData.DiagnosedResult.RiskDegreeCode;
                    break;
                case "RiskDegree":
                    value = GlobalData.DiagnosedResult.RiskDegree;
                    break;
                case "DiseaseDiagnosedResultList":
                    //GlobalData.DiagnosedResult.DiseaseDiagnosedResultList=dr["DiseaseDiagnosedResultList"].ToString();
                    break;
                case "ReasoningDiseaseDiagnosedResultList":
                    //GlobalData.DiagnosedResult.ReasoningDiseaseDiagnosedResultList=dr["ReasoningDiseaseDiagnosedResultList"].ToString();
                    break;


                //膳食处方
                case "DietType":
                    value = GlobalData.DietSuggestion.DietType ;
                    break;
                case "TotalEnergy":
                    value = GlobalData.DietSuggestion.TotalEnergy;
                    break;
                case "TotalWater":
                    value = GlobalData.DietSuggestion.TotalWater;
                    break;
                case "CarboPercent":
                    value = GlobalData.DietSuggestion.CarboPercent ;
                    break;
                case "CarboCount":
                    value = GlobalData.DietSuggestion.CarboCount;
                    break;
                case "CerealCount":
                    value = GlobalData.DietSuggestion.CerealCount;
                    break;
                case "CerealDetail":
                    value = GlobalData.DietSuggestion.CerealDetail;
                    break;
                case "CerealBreakfastDetail":
                    value = GlobalData.DietSuggestion.CerealBreakfastDetail;
                    break;
                case "CerealLunchDetail":
                    value = GlobalData.DietSuggestion.CerealLunchDetail;
                    break;
                case "CerealSupperDetail":
                    value = GlobalData.DietSuggestion.CerealSupperDetail;
                    break;
                case "Fruitcount":
                    value = GlobalData.DietSuggestion.Fruitcount;
                    break;
                case "FruitDetail":
                    value = GlobalData.DietSuggestion.FruitDetail;
                    break;
                case "GreenstuffCount":
                    value = GlobalData.DietSuggestion.GreenstuffCount;
                    break;
                case "GreenstuffLunchDetail":
                    value = GlobalData.DietSuggestion.GreenstuffLunchDetail;
                    break;
                case "GreenstuffSupperDetail":
                    value = GlobalData.DietSuggestion.GreenstuffSupperDetail;
                    break;
                case "GreenstuffDetail":
                    value = GlobalData.DietSuggestion.GreenstuffDetail;
                    break;
                case "ProteinPercent":
                    value = GlobalData.DietSuggestion.ProteinPercent;
                    break;
                case "ProteinCount":
                    value = GlobalData.DietSuggestion.ProteinCount;
                    break;
                case "DairyCount":
                    value = GlobalData.DietSuggestion.DairyCount;
                    break;
                case "DairyDetail":
                    value = GlobalData.DietSuggestion.DairyDetail;
                    break;
                case "EggCount":
                    value = GlobalData.DietSuggestion.EggCount;
                    break;
                case "EggDetail":
                    value = GlobalData.DietSuggestion.EggDetail;
                    break;
                case "MeatCount":
                    value = GlobalData.DietSuggestion.MeatCount;
                    break;
                case "MeatDetail":
                    value = GlobalData.DietSuggestion.MeatDetail;
                    break;
                case "BeanProductCount":
                    value = GlobalData.DietSuggestion.BeanProductCount;
                    break;
                case "BeanProductDetail":
                    value = GlobalData.DietSuggestion.BeanProductDetail;
                    break;
                case "FatPercent":
                    value = GlobalData.DietSuggestion.FatPercent;
                    break;
                case "FatCount":
                    value = GlobalData.DietSuggestion.FatCount;
                    break;
                case "VegetableOilCount":
                    value = GlobalData.DietSuggestion.VegetableOilCount;
                    break;
                case "VegetableOilDetail":
                    value = GlobalData.DietSuggestion.VegetableOilDetail;
                    break;
                case "OtherFatFoodCount":
                    value = GlobalData.DietSuggestion.OtherFatFoodCount;
                    break;
                case "OtherFatFoodDetail":
                    value = GlobalData.DietSuggestion.OtherFatFoodDetail;
                    break;
                case "LimitedFood":
                    value = GlobalData.DietSuggestion.LimitedFood;
                    break;
                case "AvoidFood":
                    value = GlobalData.DietSuggestion.AvoidFood;
                    break;
                case "DietSuggestion.DataNeeded":
                    value = GlobalData.DietSuggestion.DataNeeded;
                    break;


                //运动建议
                case "ExerciseTarget":
                    value = GlobalData.ExerciseSuggestion.ExerciseTarget;
                    break;
                case "EnergyCost":
                    value = GlobalData.ExerciseSuggestion.EnergyCost;
                    break;
                case "NoIntensityExercise":
                    value = GlobalData.ExerciseSuggestion.NoIntensityExercise ;
                    break;
                case "NoIntensityExerciseItems":
                    value = GlobalData.ExerciseSuggestion.NoIntensityExerciseItems ;
                    break;
                case "LowIntensityExercise":
                    value = GlobalData.ExerciseSuggestion.LowIntensityExercise;
                    break;
                case "LowIntensityExerciseItems":
                    value = GlobalData.ExerciseSuggestion.LowIntensityExerciseItems ;
                    break;
                case "MiddleIntensityExercise":
                    value = GlobalData.ExerciseSuggestion.MiddleIntensityExercise;
                    break;
                case "MiddleIntensityExerciseItems":
                    value = GlobalData.ExerciseSuggestion.MiddleIntensityExerciseItems;
                    break;
                case "HighIntensityExercise":
                    value = GlobalData.ExerciseSuggestion.HighIntensityExercise ;
                    break;
                case "HighIntensityExerciseItems":
                    value = GlobalData.ExerciseSuggestion.HighIntensityExerciseItems ;
                    break;
                case "ExerciseSuggestion.DataNeeded":
                    value = GlobalData.ExerciseSuggestion.DataNeeded ;
                    break;
                default:
                    break;
            }
            return value;
            #endregion
        }
    }
}
