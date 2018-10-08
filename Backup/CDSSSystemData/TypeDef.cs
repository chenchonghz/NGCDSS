using System;
using System.Collections.Generic;
using System.Text;

namespace CDSSSystemData
{
    /// <summary>
    /// 当前登录用户信息
    /// </summary>
    public class CDSSUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName;
        /// <summary>
        /// 所属部门
        /// </summary>
        public string Department;
        /// <summary>
        /// 职务
        /// </summary>
        public string Title;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone;
        /// <summary>
        /// 单位
        /// </summary>
        public string Company;
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string MailAddress;
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LastLoginTime;
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginFrequency;
        /// <summary>
        /// 登录时连接DB的时间
        /// </summary>
        public DateTime LoginConnDBTime;
        /// <summary>
        /// 保存案例的时间
        /// </summary>
        public DateTime SaveCaseTime;
        /// <summary>
        /// 当前软件版本信息
        /// </summary>
        public string CurrentAppVer;

        public CDSSUserInfo()
        {
            Clear();
        }

        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            this.UserID = String.Empty;
            this.UserName = String.Empty;
            this.Department = String.Empty;
            this.Title = String.Empty;
            this.Phone = String.Empty;
            this.Company = String.Empty;
            this.MailAddress = String.Empty;
            this.LastLoginTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            this.LoginFrequency = 0;
            this.LoginConnDBTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            this.SaveCaseTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            CurrentAppVer = String.Empty;
        }
    }


    /// <summary>
    /// 病人基本信息
    /// </summary>
    public class CDSSPatBasicInfo
    {   
        /// <summary>
        /// 病人唯一识别号
        /// </summary>
        public string PatSEQ;

        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime PatVisitDateTime;
        /// <summary>
        /// 病人ID
        /// </summary>
        public string PatID;
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatName;
        /// <summary>
        /// 性别
        /// </summary>
        public string PatSex;
        /// <summary>
        /// 教育程度
        /// </summary>
        public string PatEducationLevel;
        /// <summary>
        /// 籍贯
        /// </summary>
        public string PatNational;
        /// <summary>
        /// 收入来源
        /// </summary>
        public string PatIncomeSource;
        /// <summary>
        /// 职业
        /// </summary>
        public string PatProfessional;
        /// <summary>
        /// 治疗消费
        /// </summary>
        public string PatTreatmentCost;
        /// <summary>
        /// 收入
        /// </summary>
        public string PatIncome;
        /// <summary>
        /// 邮编
        /// </summary>
        public string PatZipcode;
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime PatBirthday;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PatPhone;
        /// <summary>
        /// 省份
        /// </summary>
        public string PatBirthProvince;
        /// <summary>
        /// 城市
        /// </summary>
        public string PatBirthCity;
        /// <summary>
        /// 联系地址
        /// </summary>
        public string PatAddress;
        /// <summary>
        /// 子女数
        /// </summary>
        public int PatChildCount;
        /// <summary>
        /// 兄弟姐妹数
        /// </summary>
        public int PatSiblingsCount;

        public CDSSPatBasicInfo()
        {
            Clear();
        }

        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            PatSEQ = String.Empty;
            PatVisitDateTime = DateTime.Now.Date;
            PatID = String.Empty;
            PatName = String.Empty;
            PatSex = String.Empty;
            PatEducationLevel = String.Empty;
            PatNational = String.Empty;
            PatIncomeSource = String.Empty;
            PatProfessional = String.Empty;
            PatTreatmentCost = String.Empty;
            PatIncome = String.Empty;
            PatZipcode = String.Empty;
            PatBirthday = DateTime.Now.Date;
            PatPhone = String.Empty;
            PatBirthProvince = String.Empty;
            PatBirthCity = String.Empty;
            PatAddress = String.Empty;
            PatChildCount = 0;
            PatSiblingsCount = 0;
        }
    }


    /// <summary>
    /// 症状信息
    /// </summary>
    public class CDSSSymptomsInfo
    {
        /// <summary>
        ///  症状类型名称
        /// </summary>
        public string SymptomsName;
        /// <summary>
        /// 症状发现时间
        /// </summary>
        public DateTime SymptomsDetectedDateTime;
        /// <summary>
        /// 症状表现（或结果、部位）
        /// </summary>
        public string DiseaseResult;

        public CDSSSymptomsInfo()
        {
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            SymptomsName = String.Empty;
            SymptomsDetectedDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            DiseaseResult = String.Empty;
        }
    }


    /// <summary>
    /// 用药信息
    /// </summary>
    public class CDSSMedicineInfo
    {
        /// <summary>
        /// 药品类别
        /// </summary>
        public string Drugtype;
        /// <summary>
        /// 药品名称
        /// </summary>
        public string DrugNames;
        /// <summary>
        /// 用药开始时间
        /// </summary>
        public DateTime DrugBeginTime;
        /// <summary>
        /// 用药结束时间
        /// </summary>
        public DateTime DrugEndTime;
        /// <summary>
        /// 用药剂量
        /// </summary>
        public string DrugAmount;
        /// <summary>
        /// 用药单位
        /// </summary>
        public string DrugUnits;
        /// <summary>
        /// 用药途径
        /// </summary>
        public string DrugByRoute;
        /// <summary>
        /// 用药频次
        /// </summary>
        public string DrugFrequency;


        public CDSSMedicineInfo()
        {
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            DrugNames = String.Empty;
            DrugBeginTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            DrugEndTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            DrugAmount = String.Empty;
            DrugUnits = String.Empty;
            DrugByRoute = String.Empty;
            DrugFrequency = String.Empty;
        }
    }


    /// <summary>
    /// 糖代谢异常信息
    /// </summary>
    public class CDSSAGMInfo
    {
        /// <summary>
        /// 糖代谢是否异常
        /// </summary>
        public bool HasAGMAbnormal;
        /// <summary>
        /// 异常发现时间
        /// </summary>
        public DateTime AbnormalDetectedDateTime;
        /// <summary>
        /// 确诊类型
        /// </summary>
        public List<CDSSSymptomsInfo> listConfirmedSymptoms;
        /// <summary>
        /// 急性并发症类型
        /// </summary>
        public List<CDSSSymptomsInfo> listAcuteSymptoms;
        /// <summary>
        /// 慢性并发症类型
        /// </summary>
        public List<CDSSSymptomsInfo> listChronicSymptoms;
        /// <summary>
        /// 降糖药用药信息
        /// </summary>
        public List<CDSSMedicineInfo> listHypogMedicineInfo;
        /// <summary>
        /// 中成药用药信息
        /// </summary>
        public List<CDSSMedicineInfo> listChineseMedicineInfo;
        /// <summary>
        /// 胰岛素用药信息
        /// </summary>
        public List<CDSSMedicineInfo> listInsulinMedicineInfo;

        public CDSSAGMInfo()
        {
            HasAGMAbnormal = false;
            AbnormalDetectedDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            listConfirmedSymptoms = new List<CDSSSymptomsInfo>();
            listAcuteSymptoms = new List<CDSSSymptomsInfo>();
            listChronicSymptoms = new List<CDSSSymptomsInfo>();
            listHypogMedicineInfo = new List<CDSSMedicineInfo>();
            listChineseMedicineInfo = new List<CDSSMedicineInfo>();
            listInsulinMedicineInfo = new List<CDSSMedicineInfo>();
            Clear();
        }
        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            HasAGMAbnormal = false;
            AbnormalDetectedDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            listConfirmedSymptoms.Clear();
            listAcuteSymptoms.Clear();
            listChronicSymptoms.Clear();
            listHypogMedicineInfo.Clear();
            listChineseMedicineInfo.Clear();
            listInsulinMedicineInfo.Clear();
        }
    }


    /// <summary>
    /// 高血压信息
    /// </summary>
    public class CDSSHypertensionInfo
    {
        /// <summary>
        /// 是否有高血压
        /// </summary>
        public bool HasHypertension;
        /// <summary>
        /// 高血压类型
        /// </summary>
        public List<CDSSSymptomsInfo> listHypertensionSymptoms;
        /// <summary>
        /// 最高收缩压
        /// </summary>
        public string MaxSBP;
        /// <summary>
        /// 最高舒张压
        /// </summary>
        public string MaxDBP;
        /// <summary>
        /// 最低收缩压
        /// </summary>
        public string MinSBP;
        /// <summary>
        /// 最低舒张压
        /// </summary>
        public string MinDBP;
        /// <summary>
        /// 降压西药用药信息
        /// </summary>
        public List<CDSSMedicineInfo> listStepDownWestMedicine;
        /// <summary>
        /// 中成药用药信息
        /// </summary>
        public List<CDSSMedicineInfo> listStepDownChineseMedication;
        /// <summary>
        /// 平时血压控制 起始年份
        /// </summary>
        public string BPControlFromYear;
        /// <summary>
        /// 平时血压控制 截止年份
        /// </summary>
        public string BPControlToYear;
        /// <summary>
        /// 平时血压收缩压：最小值
        /// </summary>
        public string PeacetimeMinSBP;
        /// <summary>
        ///  平时血压收缩压：最大值
        /// </summary>
        public string PeacetimeMaxSBP;
        /// <summary>
        /// 平时血压舒张压：最小值
        /// </summary>
        public string PeacetimeMinDBP;
        /// <summary>
        /// 平时血压舒张压：最大值
        /// </summary>
        public string PeacetimeMaxDBP;

        public CDSSHypertensionInfo()
        {
            listHypertensionSymptoms = new List<CDSSSymptomsInfo>();
            listStepDownWestMedicine = new List<CDSSMedicineInfo>();
            listStepDownChineseMedication = new List<CDSSMedicineInfo>();
            Clear();
        }

        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            HasHypertension = false;
            listHypertensionSymptoms.Clear();
            MaxSBP = String.Empty;
            MaxDBP = String.Empty;
            MinSBP = String.Empty;
            MinDBP = String.Empty;
            listStepDownWestMedicine.Clear();
            listStepDownChineseMedication.Clear();
            BPControlFromYear = String.Empty;
            BPControlToYear = String.Empty;
            PeacetimeMinSBP = String.Empty;
            PeacetimeMaxSBP = String.Empty;
            PeacetimeMinDBP = String.Empty;
            PeacetimeMaxDBP = String.Empty;
        }
    }


    /// <summary>
    /// 血脂紊乱信息
    /// </summary>
    public class CDSSDyslipidemiaInfo
    {
        /// <summary>
        /// 是否有血脂紊乱
        /// </summary>
        public bool HasDyslipidemia;
        /// <summary>
        /// 血脂紊乱类型
        /// </summary>
        public List<CDSSSymptomsInfo> listDyslipidemiaSymptoms;
        /// <summary>
        /// 调脂药用药信息
        /// </summary>
        public List<CDSSMedicineInfo> listLipidlowerDrugs;

        public CDSSDyslipidemiaInfo()
        {
            listDyslipidemiaSymptoms = new List<CDSSSymptomsInfo>();
            listLipidlowerDrugs = new List<CDSSMedicineInfo>();
            Clear();
        }

        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            HasDyslipidemia = false;
            listDyslipidemiaSymptoms.Clear();
            listLipidlowerDrugs.Clear();
        }
    }


    /// <summary>
    /// 高尿酸血症信息
    /// </summary>
    public class CDSSHyperuricemiaInfo
    {
        /// <summary>
        /// 是否有高尿酸血症
        /// </summary>
        public bool HasHyperuricemia;
        /// <summary>
        /// 高尿酸血症类型：继发还是原发
        /// </summary>
        public string HyperuricemiaType;
        /// <summary>
        /// 高尿酸血症确诊类型
        /// </summary>
        public List<CDSSSymptomsInfo> listHyperuricemiaSymptoms;
        /// <summary>
        /// 是否有痛风关节炎
        /// </summary>
        public bool HasGoutyArthritis;
        /// <summary>
        /// 痛风关节炎时间
        /// </summary>
        public DateTime GoutyArthritisDetectedDateTime;
        /// <summary>
        /// 是否有痛风石
        /// </summary>
        public bool HasTophus;
        /// <summary>
        /// 痛风石部位
        /// </summary>
        public string TophusPart;
        /// <summary>
        /// 降尿酸药信息
        /// </summary>
        public List<CDSSMedicineInfo> listUricAcidlowerDrugs;
        /// <summary>
        /// 是否关节红肿
        /// </summary>
        public bool HasJointSwelling;

        public CDSSHyperuricemiaInfo()
        {
            listHyperuricemiaSymptoms = new List<CDSSSymptomsInfo>();
            GoutyArthritisDetectedDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            listUricAcidlowerDrugs = new List<CDSSMedicineInfo>();
            Clear();
        }

        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            HasHyperuricemia = false;
            HyperuricemiaType = String.Empty;
            listHyperuricemiaSymptoms.Clear();
            HasGoutyArthritis = false;
            GoutyArthritisDetectedDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            HasTophus = false;
            TophusPart = String.Empty;
            listUricAcidlowerDrugs.Clear();
            HasJointSwelling = false;
        }
    }


    /// <summary>
    /// 非糖尿病肾脏疾病
    /// </summary>
    public class CDSSNephropathyInfo
    {
        /// <summary>
        /// 是否有非糖尿病肾脏疾病
        /// </summary>
        public bool HasNephropathy;
        /// <summary>
        /// 非糖尿病肾脏疾病类型
        /// </summary>
        public List<CDSSSymptomsInfo> listNephropathySymptoms;
        /// <summary>
        /// 最高血肌酐值
        /// </summary>
        public string MAXCreatinine;
        /// <summary>
        /// 最高血尿素值
        /// </summary>
        public string MAXBloodUrea;
        /// <summary>
        /// 是否肾功能异常
        /// </summary>
        public bool HasRenalAbnormal;
        /// <summary>
        /// 肾功能异常时间
        /// </summary>
        public DateTime RenalAbnormalDetectedDateTime;

        public CDSSNephropathyInfo()
        {
            listNephropathySymptoms = new List<CDSSSymptomsInfo>();
            Clear();
        }

        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            HasNephropathy = false;
            listNephropathySymptoms.Clear();
            MAXCreatinine = String.Empty;
            MAXBloodUrea = String.Empty;
            HasRenalAbnormal = false;
            RenalAbnormalDetectedDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
        }
    }


    /// <summary>
    /// 其他疾病史
    /// </summary>
    public class CDSSOtherDiseaseHistoryInfo
    {
        /// <summary>
        /// 冠心病类型Coronary Heart Disease
        /// </summary>
        public List<CDSSSymptomsInfo> listCoronaryHeartDisease;
        /// <summary>
        /// 脑血管疾病类型
        /// </summary>
        public List<CDSSSymptomsInfo> listCerebrovascularDisease;
        /// <summary>
        /// 是否有胆囊炎、胆石症
        /// </summary>
        public bool HasCholecystitis;
        /// <summary>
        /// 胆囊炎、胆石症发现时间
        /// </summary>
        public DateTime CholecystitisDetectedDateTime;
        /// <summary>
        /// 是否有做胆囊摘除手术
        /// </summary>
        public bool HasGallbladderSurgery;
        /// <summary>
        /// 胆囊摘除术时间
        /// </summary>
        public DateTime GallbladderSurgeryDateTime;
        /// <summary>
        /// 胰腺炎类型
        /// </summary>
        public List<CDSSSymptomsInfo> listPancreatitis;
        /// <summary>
        /// 是否有恶性肿瘤
        /// </summary>
        public bool HasCancer;
        /// <summary>
        /// 恶性肿瘤发现时间
        /// </summary>
        public DateTime CancerDetectedDateTime;
        /// <summary>
        /// 恶性肿瘤部位
        /// </summary>
        public string CancerPart;
        /// <summary>
        /// 恶性肿瘤预后结果
        /// </summary>
        public string CancerPrognosis;
        /// <summary>
        /// 其他疾病
        /// </summary>
        public string OtherDisease;
        /// <summary>
        /// 其他疾病发现时间
        /// </summary>
        public DateTime OtherDiseaseDetectedDateTime;

        public CDSSOtherDiseaseHistoryInfo()
        {
            listCoronaryHeartDisease = new List<CDSSSymptomsInfo>();
            listCerebrovascularDisease = new List<CDSSSymptomsInfo>();
            listPancreatitis = new List<CDSSSymptomsInfo>();
            Clear();
        }

        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            listCoronaryHeartDisease.Clear();
            listCerebrovascularDisease.Clear();
            HasCholecystitis = false;
            CholecystitisDetectedDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            HasGallbladderSurgery = false;
            GallbladderSurgeryDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            listPancreatitis.Clear();
            HasCancer = false;
            CancerDetectedDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
            CancerPart = String.Empty;
            CancerPrognosis = String.Empty;
            OtherDisease = String.Empty;
            OtherDiseaseDetectedDateTime = DateTime.Parse(DateTime.Now.AddMonths(-DateTime.Now.Month + 1).ToString("yyyy-MM"));
        }
    }


    /// <summary>
    /// 饮酒信息
    /// </summary>
    public class CDSSDrinkingInfo
    {
        /// <summary>
        /// 饮酒种类
        /// </summary>
        public string DrinkingType;
        /// <summary>
        /// 次/周
        /// </summary>
        public string TimesOneWeek;
        /// <summary>
        /// ml/次
        /// </summary>
        public string AmountOneTime;

        public CDSSDrinkingInfo()
        {
            DrinkingType = String.Empty;
            TimesOneWeek = String.Empty;
            AmountOneTime = String.Empty;
        }
    }


    /// <summary>
    /// 运动信息
    /// </summary>
    public class CDSSExerciseInfo
    {
        /// <summary>
        /// 运动项目
        /// </summary>
        public string ExerciseType;
        /// <summary>
        /// 日/周
        /// </summary>
        public string DaysOneWeek;
        /// <summary>
        /// 小时/日
        /// </summary>
        public string LastedHourOneDay;

        public CDSSExerciseInfo()
        {
            ExerciseType = String.Empty;
            DaysOneWeek = String.Empty;
            LastedHourOneDay = String.Empty;
        }
    }


    /// <summary>
    /// 个人史信息
    /// </summary>
    public class CDSSPersonalHistoryInfo
    {
        /// <summary>
        /// 最高体重
        /// </summary>
        public string MaxWeight;
        /// <summary>
        /// 最低体重
        /// </summary>
        public string MinWeight;
        /// <summary>
        /// 最高体重年龄
        /// </summary>
        public string MaxWeightAge;
        /// <summary>
        /// 最高体重持续时间(年）
        /// </summary>
        public string MaxWeightLastedYears;
        /// <summary>
        /// 是否有吸烟史
        /// </summary>
        public bool IsSmokeing;
        /// <summary>
        /// 吸烟起始年龄
        /// </summary>
        public string SmokingAgeBegin;
        /// <summary>
        /// 吸烟量范围
        /// </summary>
        public string SmokingFrequency;
        /// <summary>
        /// 近期吸烟量
        /// </summary>
        public string RecentSmokingFrequency;
        /// <summary>
        /// 戒烟年龄
        /// </summary>
        public string SmokingAgeEnd;
        /// <summary>
        /// 是否有饮酒史
        /// </summary>
        public bool IsDrinking;
        /// <summary>
        /// 饮酒起始年龄
        /// </summary>
        public string DrinkingAgeBegin;
        /// <summary>
        /// 戒酒年龄
        /// </summary>
        public string DrinkingAgeEnd;
        /// <summary>
        /// 饮酒详细情况
        /// </summary>
        public List<CDSSDrinkingInfo> listDrinkingInfo;
        /// <summary>
        /// 是否控制饮食
        /// </summary>
        public bool HasControlDiet;
        /// <summary>
        /// 每日主食（两）
        /// </summary>
        public string MainFoodAmount;
        /// <summary>
        /// 每日植物油数量（g）
        /// </summary>
        public string OilAmount;
        /// <summary>
        /// 每日蛋白量（两）
        /// </summary>
        public string ProteinAmount;
        /// <summary>
        /// 是否生育
        /// </summary>
        public bool HasBearing;
        /// <summary>
        /// 是否有妊娠糖尿病
        /// </summary>
        public bool HasGDM;
        /// <summary>
        /// 妊娠糖尿病起始年龄
        /// </summary>
        public string GDMAgeBegin;
        /// <summary>
        /// 胎儿是否>=4KG
        /// </summary>
        public bool IsNeonateHeavierThan4Kg;
        /// <summary>
        /// 胎儿数量
        /// </summary>
        public string NeonateCount;
        /// <summary>
        /// 生育年龄1
        /// </summary>
        public string BearingAge1;
        /// <summary>
        /// 胎儿重1
        /// </summary>
        public string NeonateWeight1;
        /// <summary>
        /// 生育年龄2
        /// </summary>
        public string BearingAge2;
        /// <summary>
        /// 胎儿重2
        /// </summary>
        public string NeonateWeight2;
        /// <summary>
        /// 近期是否有运动
        /// </summary>
        public bool HasExerciseRecent;
        /// <summary>
        /// 运动详细情况
        /// </summary>
        public List<CDSSExerciseInfo> listExerciseInfo;

        public CDSSPersonalHistoryInfo()
        {
            listDrinkingInfo = new List<CDSSDrinkingInfo>();
            listExerciseInfo = new List<CDSSExerciseInfo>();
            Clear();
        }

        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            MaxWeight = String.Empty;
            MinWeight = String.Empty;
            MaxWeightAge = String.Empty;
            MaxWeightLastedYears = String.Empty;
            IsSmokeing = false;
            SmokingAgeBegin = String.Empty;
            SmokingFrequency = "-";
            RecentSmokingFrequency = String.Empty;
            SmokingAgeEnd = String.Empty;
            IsDrinking = false;
            DrinkingAgeBegin = String.Empty;
            DrinkingAgeEnd = String.Empty;
            listDrinkingInfo.Clear();
            HasControlDiet = false;
            MainFoodAmount = String.Empty;
            OilAmount = String.Empty;
            ProteinAmount = String.Empty;
            HasBearing = false;
            HasGDM = false;
            GDMAgeBegin = String.Empty;
            IsNeonateHeavierThan4Kg = false;
            NeonateCount = String.Empty;
            BearingAge1 = String.Empty;
            NeonateWeight1 = String.Empty;
            BearingAge2 = String.Empty;
            NeonateWeight2 = String.Empty;
            HasExerciseRecent = false;
            listExerciseInfo.Clear();
        }
    }


    /// <summary>
    /// 家族疾病史信息
    /// </summary>
    public class CDSSFamilyDiseaseHistoryInfo
    {
        /// <summary>
        /// 父亲的疾病史
        /// </summary>
        public string FatherHistory;
        /// <summary>
        /// 母亲的疾病史
        /// </summary>
        public string MotherHistory;
        /// <summary>
        /// 兄弟姐妹的疾病史
        /// </summary>
        public string SiblingsHistory;
        /// <summary>
        /// 子女的疾病史
        /// </summary>
        public string ChildrenHistory;
        /// <summary>
        /// 其他家人的疾病史
        /// </summary>
        public string OtherHistory;

        public CDSSFamilyDiseaseHistoryInfo()
        {
            Clear();
        }

        /// <summary>
        ///恢复默认值
        /// </summary>
        public void Clear()
        {
            FatherHistory = String.Empty;
            MotherHistory = String.Empty;
            SiblingsHistory = String.Empty;
            ChildrenHistory = String.Empty;
            OtherHistory = String.Empty;
        }
    }


    /// <summary>
    /// 体格检查信息
    /// </summary>
    public class CDSSPhysicalInfo
    {
        /// <summary>
        /// 身高
        /// </summary>
        public string Height;
        /// <summary>
        /// 体重
        /// </summary>
        public string Weigh;
        /// <summary>
        /// 腰围
        /// </summary>
        public string WC;
        /// <summary>
        /// 臀围
        /// </summary>
        public string HC;
        /// <summary>
        /// 脉心率
        /// </summary>
        public string HR;
        /// <summary>
        /// 是否有运动障碍
        /// </summary>
        public bool HasDyskinesia;
        /// <summary>
        /// 运动障碍部位
        /// </summary>
        public string DyskinesiaPart;
        /// <summary>
        /// 收缩压1
        /// </summary>
        public string SBP1;
        /// <summary>
        /// 舒张压1
        /// </summary>
        public string DBP1;
        /// <summary>
        /// 收缩压2
        /// </summary>
        public string SBP2;
        /// <summary>
        /// 舒张压2
        /// </summary>
        public string DBP2;

        public CDSSPhysicalInfo()
        {
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            Height = String.Empty;
            Weigh = String.Empty;
            WC = String.Empty;
            HC = String.Empty;
            HR = String.Empty;
            HasDyskinesia = false;
            DyskinesiaPart = String.Empty;
            SBP1 = String.Empty;
            DBP1 = String.Empty;
            SBP2 = String.Empty;
            DBP2 = String.Empty;
        }
    }


    /// <summary>
    /// 实验室检查信息
    /// </summary>
    public class CDSSLabExamInfo
    {
        /// <summary>
        /// 实验室检查日期
        /// </summary>
        public DateTime LabExamDateTime;
        /// <summary>
        /// 随机血糖
        /// </summary>
        public string BG;
        /// <summary>
        /// 空腹血糖
        /// </summary>
        public string FBG;
        /// <summary>
        /// 餐后2H血糖
        /// </summary>
        public string TWOHPBG;
        /// <summary>
        /// 进餐主食数量
        /// </summary>
        public string FoodCount;
        /// <summary>
        /// OGTT空腹血糖
        /// </summary>
        public string OGTTFBG;
        /// <summary>
        /// OGTT餐后血糖
        /// </summary>
        public string OGTTPBG;
        /// <summary>
        /// 早餐前自测血糖
        /// </summary>
        public string BeforeBreakfast;
        /// <summary>
        /// 早餐后2H自测血糖
        /// </summary>
        public string AfterBreakfast;
        /// <summary>
        /// 午餐前自测血糖
        /// </summary>
        public string BeforeLunch;
        /// <summary>
        /// 午餐后2H自测血糖
        /// </summary>
        public string AfterLunch;
        /// <summary>
        /// 晚餐前自测血糖
        /// </summary>
        public string BeforeSupper;
        /// <summary>
        /// 晚餐后2H自测血糖
        /// </summary>
        public string AfterSupper;
        /// <summary>
        /// 晚睡前自测血糖
        /// </summary>
        public string BeforeSleep;
        /// <summary>
        /// 凌晨自测血糖
        /// </summary>
        public string LC;
        /// <summary>
        /// 总胆固醇
        /// </summary>
        public string TC;
        /// <summary>
        /// HDL-CH
        /// </summary>
        public string HDLC;
        /// <summary>
        /// 甘油三酯
        /// </summary>
        public string TG;
        /// <summary>
        /// LDL-CH
        /// </summary>
        public string LDLC;
        /// <summary>
        /// 肌酐
        /// </summary>
        public string CR;
        /// <summary>
        /// 丙氨酸氨基转移酶
        /// </summary>
        public string AlanineAminotransferase;
        /// <summary>
        /// 尿素氮
        /// </summary>
        public string UN;
        /// <summary>
        /// 天冬氨酸氨基转移酶
        /// </summary>
        public string AspartateAminotransferase;
        /// <summary>
        /// ALB/CR
        /// </summary>
        public string ALBCR;
        /// <summary>
        /// 尿糖
        /// </summary>
        public string US;
        /// <summary>
        /// 尿蛋白定量
        /// </summary>
        public string UrinaryProtein;
        /// <summary>
        /// 尿酮体
        /// </summary>
        public string NTT;
        /// <summary>
        /// 尿PH
        /// </summary>
        public string UPH;
        /// <summary>
        /// 尿尿酸
        /// </summary>
        public string UUA;
        /// <summary>
        /// 糖化血红蛋白
        /// </summary>
        public string HBA1C;
        /// <summary>
        /// 血氯
        /// </summary>
        public string BCL;
        /// <summary>
        /// 血尿酸
        /// </summary>
        public string BUA;
        /// <summary>
        /// 血钾
        /// </summary>
        public string BKA;
        /// <summary>
        /// 血钠
        /// </summary>
        public string BNA;
        /// <summary>
        /// 血CO2CP
        /// </summary>
        public string BCO2CP;
        /// <summary>
        /// 血钙
        /// </summary>
        public string BGA;
        /// <summary>
        /// 血磷
        /// </summary>
        public string BP;
        /// <summary>
        /// 血清总蛋白
        /// </summary>
        public string SerumTotalProtein;
        /// <summary>
        /// 血清白蛋白
        /// </summary>
        public string SerumAlbumin;
        /// <summary>
        /// 空腹胰岛素
        /// </summary>
        public string FastingInsulin;
        /// <summary>
        /// 空腹C肽
        /// </summary>
        public string FastingCPeptide;
        /// <summary>
        /// 餐后胰岛素
        /// </summary>
        public string PostprandialInsulin;
        /// <summary>
        /// 餐后C肽
        /// </summary>
        public string PostprandialCPeptide;
        /// <summary>
        /// ICA
        /// </summary>
        public string ICA;
        /// <summary>
        /// GDA65
        /// </summary>
        public string GDA65;
        /// <summary>
        /// 总胆红素
        /// </summary>
        public string Tbil;
        /// <summary>
        /// 甲胎蛋白
        /// </summary>
        public string AFP;
        /// <summary>
        /// 癌胚抗原
        /// </summary>
        public string CEA;

        public CDSSLabExamInfo()
        {
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            LabExamDateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            BG = String.Empty;
            FBG = String.Empty;
            TWOHPBG = String.Empty;
            FoodCount = String.Empty;
            OGTTFBG = String.Empty;
            OGTTPBG = String.Empty;
            BeforeBreakfast = "-";
            AfterBreakfast = "-";
            BeforeLunch = "-";
            AfterLunch = "-";
            BeforeSupper = "-";
            AfterSupper = "-";
            BeforeSleep = "-";
            LC = "-";
            TC = String.Empty;
            HDLC = String.Empty;
            TG = String.Empty;
            LDLC = String.Empty;
            CR = String.Empty;
            AlanineAminotransferase = String.Empty;
            UN = String.Empty;
            AspartateAminotransferase = String.Empty;
            ALBCR = String.Empty;
            US = String.Empty;
            UrinaryProtein = String.Empty;
            NTT = String.Empty;
            UPH = String.Empty;
            UUA = String.Empty;
            HBA1C = String.Empty;
            BCL = String.Empty;
            BUA = String.Empty;
            BKA = String.Empty;
            BNA = String.Empty;
            BCO2CP = String.Empty;
            BGA = String.Empty;
            BP = String.Empty;
            SerumTotalProtein = String.Empty;
            SerumAlbumin = String.Empty;
            FastingInsulin = String.Empty;
            FastingCPeptide = String.Empty;
            PostprandialInsulin = String.Empty;
            PostprandialCPeptide = String.Empty;
            ICA = String.Empty;
            GDA65 = String.Empty;
            Tbil = String.Empty;
            AFP = String.Empty;
            CEA = String.Empty;
        }
    }


    /// <summary>
    /// 血管超声异常信息
    /// </summary>
    public class CDSSVascularUltrasound
    {
        /// <summary>
        /// 血管超声异常类型
        /// </summary>
        public string VascularAbnormalType;
        /// <summary>
        /// 血管超声异常部位
        /// </summary>
        public string VascularAbnormalPart;

        public CDSSVascularUltrasound()
        {
            VascularAbnormalType = String.Empty;
            VascularAbnormalPart = String.Empty;
        }
    }


    /// <summary>
    /// 其他检查异常信息
    /// </summary>
    public class CDSSOtherExamAbnormal
    {
        /// <summary>
        /// 有异常检查项目名
        /// </summary>
        public string ExamItemName;
        /// <summary>
        /// 检查结果
        /// </summary>
        public string ExamResult;

        public CDSSOtherExamAbnormal()
        {
            ExamItemName = String.Empty;
            ExamResult = String.Empty;
        }
    }


    /// <summary>
    /// 其他检查信息
    /// </summary>
    public class CDSSOtherExamInfo
    {
        /// <summary>
        /// 心电图是否有异常
        /// </summary>
        public bool HasECGAbnormal;
        /// <summary>
        /// 心电图异常类型
        /// </summary>
        public string ECGAbnormalType;
        /// <summary>
        /// 血管超声异常结果
        /// </summary>
        public List<CDSSVascularUltrasound> listVascularUltrasound;
        /// <summary>
        /// 其他检查结果
        /// </summary>
        public List<CDSSOtherExamAbnormal> listOtherExamAbnormal;

        public CDSSOtherExamInfo()
        {
            listVascularUltrasound = new List<CDSSVascularUltrasound>();
            listOtherExamAbnormal = new List<CDSSOtherExamAbnormal>();
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            HasECGAbnormal = false;
            ECGAbnormalType = String.Empty;
            listVascularUltrasound.Clear();
            listOtherExamAbnormal.Clear();
        }
    }


    /// <summary>
    /// 一种疾病的诊断结论
    /// </summary>
    public class CDSSOneDiseaseDiagnosedResult
    {
        /// <summary>
        /// 疾病名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 结论
        /// </summary>
        public string Result;
        /// <summary>
        /// 治疗目标
        /// </summary>
        public string TreatmentTarget;
        /// <summary>
        /// 治疗建议
        /// </summary>
        public string TreatmentSuggestion;
        /// <summary>
        /// 自我监测
        /// </summary>
        public string SelfCheck;
        /// <summary>
        /// 所缺数据
        /// </summary>
        public string DataNeeded;
        /// <summary>
        /// 诊断过程
        /// </summary>
        public string DiagnosisSteps;

        public CDSSOneDiseaseDiagnosedResult()
        {
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            Name = String.Empty;
            Result = String.Empty;
            TreatmentTarget = String.Empty;
            TreatmentSuggestion = String.Empty;
            SelfCheck = String.Empty;
            DataNeeded = String.Empty;
            DiagnosisSteps = String.Empty;
        }

        public CDSSOneDiseaseDiagnosedResult Clone()
        {
            CDSSOneDiseaseDiagnosedResult newResult = new CDSSOneDiseaseDiagnosedResult();
            newResult.Name = this.Name;
            newResult.Result = this.Result;
            newResult.TreatmentTarget = this.TreatmentTarget;
            newResult.TreatmentSuggestion = this.TreatmentSuggestion;
            newResult.SelfCheck = this.SelfCheck;
            newResult.DataNeeded = this.DataNeeded;
            newResult.DiagnosisSteps = this.DiagnosisSteps;

            return newResult;
        }
    }


    /// <summary>
    /// 多种疾病诊断结论列表
    /// </summary>
    public class CDSSDiseaseDiagnosedResultList : List<CDSSOneDiseaseDiagnosedResult> { }


    /// <summary>
    /// 诊断结论
    /// </summary>
    public class CDSSDiagnosedResult
    {
        /// <summary>
        /// 是否有代谢综合征
        /// </summary>
        public string HasMS;
        /// <summary>
        /// 代谢综合征危险度积分
        /// </summary>
        public string RiskDegreeCode;
        /// <summary>
        /// 代谢综合征危险度
        /// </summary>
        public string RiskDegree;
        /// <summary>
        /// 各种疾病的推理结果(医生确认后的结论)
        /// </summary>
        public CDSSDiseaseDiagnosedResultList DiseaseDiagnosedResultList;
        /// <summary>
        /// 各种疾病的推理结果(推理机得出的结论)
        /// </summary>
        public CDSSDiseaseDiagnosedResultList ReasoningDiseaseDiagnosedResultList;

        public CDSSDiagnosedResult()
        {
            DiseaseDiagnosedResultList = new CDSSDiseaseDiagnosedResultList();
            ReasoningDiseaseDiagnosedResultList = new CDSSDiseaseDiagnosedResultList();
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            HasMS = String.Empty;
            RiskDegreeCode = String.Empty;
            RiskDegree = String.Empty;
            DiseaseDiagnosedResultList.Clear();
            ReasoningDiseaseDiagnosedResultList.Clear();
        }        


    }

    /// <summary>
    /// 膳食处方
    /// </summary>
    public class CDSSDietSuggestion
    {
        /// <summary>
        /// 膳食类别
        /// </summary>
        public string DietType;
        /// <summary>
        /// 总热量
        /// </summary>
        public string TotalEnergy;
        /// <summary>
        /// 饮水量
        /// </summary>
        public string TotalWater;
        /// <summary>
        /// 糖类所占百分比
        /// </summary>
        public string CarboPercent;
        /// <summary>
        /// 糖类食物份数
        /// </summary>
        public string CarboCount;
        /// <summary>
        /// 谷薯份数
        /// </summary>
        public string CerealCount;
        /// <summary>
        /// 谷薯详细信息
        /// </summary>
        public string CerealDetail;

        public string CerealBreakfastDetail;
        public string CerealLunchDetail;
        public string CerealSupperDetail;

        /// <summary>
        /// 水果份数
        /// </summary>
        public string Fruitcount;
        /// <summary>
        /// 水果详细信息
        /// </summary>
        public string FruitDetail;
        /// <summary>
        /// 蔬菜份数
        /// </summary>
        public string GreenstuffCount;

        public string GreenstuffLunchDetail;
        public string GreenstuffSupperDetail;
        /// <summary>
        /// 蔬菜详细信息
        /// </summary>
        public string GreenstuffDetail;
        /// <summary>
        /// 蛋白质类所占百分比
        /// </summary>
        public string ProteinPercent;
        /// <summary>
        /// 蛋白质类食物份数
        /// </summary>
        public string ProteinCount;
        /// <summary>
        /// 奶制品份数
        /// </summary>
        public string DairyCount;
        /// <summary>
        /// 奶制品详细信息
        /// </summary>
        public string DairyDetail;
        /// <summary>
        /// 蛋类份数
        /// </summary>
        public string EggCount;
        /// <summary>
        /// 蛋类详细信息
        /// </summary>
        public string EggDetail;
        /// <summary>
        /// 畜禽鱼虾份数
        /// </summary>
        public string MeatCount;
        /// <summary>
        /// 畜禽鱼虾详细信息
        /// </summary>
        public string MeatDetail;
        /// <summary>
        /// 豆制品份数
        /// </summary>
        public string BeanProductCount;
        /// <summary>
        /// 豆制口详细信息
        /// </summary>
        public string BeanProductDetail;
        /// <summary>
        /// 脂肪类所占百分比
        /// </summary>
        public string FatPercent;
        /// <summary>
        /// 脂肪类食物份数
        /// </summary>
        public string FatCount;
        /// <summary>
        /// 植物油份数
        /// </summary>
        public string VegetableOilCount;
        /// <summary>
        /// 植物油详细信息
        /// </summary>
        public string VegetableOilDetail;
        /// <summary>
        /// 其他脂肪类食物份数
        /// </summary>
        public string OtherFatFoodCount;
        /// <summary>
        /// 其他脂肪类食物详细信息
        /// </summary>
        public string OtherFatFoodDetail;
        /// <summary>
        /// 限量食品
        /// </summary>
        public string LimitedFood;
        /// <summary>
        /// 忌食食品
        /// </summary>
        public string AvoidFood;
        /// <summary>
        /// 所缺数据
        /// </summary>
        public string DataNeeded;

        public CDSSDietSuggestion()
        {
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            DietType = String.Empty;
            TotalEnergy = String.Empty;
            TotalWater = String.Empty;
            CarboPercent = String.Empty;
            CarboCount = String.Empty;
            CerealCount = String.Empty;
            CerealDetail = String.Empty;
            CerealBreakfastDetail = String.Empty;
            CerealLunchDetail = String.Empty;
            CerealSupperDetail = String.Empty;
            Fruitcount = String.Empty;
            FruitDetail = String.Empty;
            GreenstuffCount = String.Empty;
            GreenstuffDetail = String.Empty;
            GreenstuffLunchDetail = String.Empty;
            GreenstuffSupperDetail = String.Empty;
            ProteinPercent = String.Empty;
            ProteinCount = String.Empty;
            DairyCount = String.Empty;
            DairyDetail = String.Empty;
            EggCount = String.Empty;
            EggDetail = String.Empty;
            MeatCount = String.Empty;
            MeatDetail = String.Empty;
            BeanProductCount = String.Empty;
            BeanProductDetail = String.Empty;
            FatPercent = String.Empty;
            FatCount = String.Empty;
            VegetableOilCount = String.Empty;
            VegetableOilDetail = String.Empty;
            OtherFatFoodCount = String.Empty;
            OtherFatFoodDetail = String.Empty;
            LimitedFood = String.Empty;
            AvoidFood = String.Empty;
            DataNeeded = String.Empty;
        }
    }


    /// <summary>
    /// 运动建议
    /// </summary>
    public class CDSSExerciseSuggestion
    {
        /// <summary>
        /// 运动目标
        /// </summary>
        public string ExerciseTarget;
        /// <summary>
        /// 运动耗能
        /// </summary>
        public string EnergyCost;
        /// <summary>
        /// 休闲运动方案
        /// </summary>
        public string NoIntensityExercise;
        /// <summary>
        /// 休闲运动方案包括的项目
        /// </summary>
        public string NoIntensityExerciseItems;
        /// <summary>
        /// 轻度运动方案
        /// </summary>
        public string LowIntensityExercise;
        /// <summary>
        /// 轻度运动方案包括的项目
        /// </summary>
        public string LowIntensityExerciseItems;
        /// <summary>
        /// 中度运动方案
        /// </summary>
        public string MiddleIntensityExercise;
        /// <summary>
        /// 中度运动方案包括的项目
        /// </summary>
        public string MiddleIntensityExerciseItems;
        /// <summary>
        /// 强度运动方案
        /// </summary>
        public string HighIntensityExercise;
        /// <summary>
        /// 强度运动方案包括的项目
        /// </summary>
        public string HighIntensityExerciseItems;
        /// <summary>
        /// 所缺数据
        /// </summary>
        public string DataNeeded;

        public CDSSExerciseSuggestion()
        {
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            ExerciseTarget = String.Empty;
            EnergyCost = String.Empty;

            //BugDB00005705 revised by wbf 2009-03-25
            NoIntensityExercise = String.Empty;
            NoIntensityExerciseItems = String.Empty;

            LowIntensityExercise = String.Empty;
            LowIntensityExerciseItems = String.Empty;
            MiddleIntensityExercise = String.Empty;
            MiddleIntensityExerciseItems = String.Empty;
            HighIntensityExercise = String.Empty;
            HighIntensityExerciseItems = String.Empty;
            DataNeeded = String.Empty;
        }
    }


    /// <summary>
    /// 索引信息
    /// </summary>
    public class CDSSRecordInfo
    {
        /// <summary>
        /// 记录序号
        /// </summary>
        public int RecordSeq;
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordDateTime;
        /// <summary>
        /// 生成该条记录的医生
        /// </summary>
        public string UserID;
        /// <summary>
        /// 记录状态：是否完成推理
        /// </summary>
        public string InferenceState;
        /// <summary>
        /// 与该条记录相关的知识库版本
        /// </summary>
        public string KnowledgeVersion;
        /// <summary>
        /// 与该条记录相关的推理机版本
        /// </summary>
        public string IEVersion;

        public CDSSRecordInfo()
        {
            Clear();
        }

        /// <summary>
        /// 恢复默认值
        /// </summary>
        public void Clear()
        {
            RecordSeq = 0;
            RecordDateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            UserID = String.Empty;
            InferenceState = String.Empty;
            KnowledgeVersion = String.Empty;
            IEVersion = String.Empty;
        }
    }

    /// <summary>
    /// 用户操作日志
    /// </summary>
    public class CDSSOperationLog
    {
        public string OperationUserID;
        public int OperationStep;
        public DateTime OperationTime;
        public string OperationDescription;
        public string OperationName;

        public CDSSOperationLog()
        {
            Clear();
        }
        public void Clear()
        {
            OperationUserID = string.Empty;
            OperationStep = 0;
            OperationTime = DateTime.Now;
            OperationDescription = string.Empty;
            OperationName = string.Empty;
        }
    }
}
