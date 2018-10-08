using System;
using System.Collections.Generic;
using System.Text;

namespace CDSSCtrlLib.MedicineControlLib
{
    /// <summary>
    /// 药品字典
    /// </summary>
    public class MedicineDictionary
    {
        public List<MedicineDict> MedicineDictList = new List<MedicineDict>();
        public List<DrugUnits> DrugUnitsList = new List<DrugUnits>();
        public List<DrugByRoute> DrugByRouteList = new List<DrugByRoute>();
        public List<DrugFrequency> DrugFrequencyList = new List<DrugFrequency>();

        public MedicineDictionary()
        {
            MedicineDictList = new List<MedicineDict>();
            DrugUnitsList = new List<DrugUnits>();
            DrugByRouteList = new List<DrugByRoute>();
            DrugFrequencyList = new List<DrugFrequency>();
        }
        public void Clear()
        {
            MedicineDictList.Clear();
            DrugUnitsList.Clear();
            DrugByRouteList.Clear();
            DrugFrequencyList.Clear();
        }
    }

    /// <summary>
    /// 用药信息
    /// </summary>
    public class MedicineInfo
    {
        /// <summary>
        /// 用药类别
        /// </summary>
        public string DrugType;
        /// <summary>
        /// 用药名称
        /// </summary>
        public string DrugName;
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
        /// <summary>
        /// 用药开始时间
        /// </summary>
        public DateTime DrugBeginTime;
        /// <summary>
        /// 用药结束时间
        /// </summary>
        public DateTime DrugEndTime;
    }
  //  public class MedicineInfoList:List<MedicineInfo>{}
    /// <summary>
    /// 药品类别
    /// </summary>
    public class DrugTypes
    {
        /// <summary>
        /// 药品类别
        /// </summary>
        public string drugType;
    }

    /// <summary>
    /// 药品名称
    /// </summary>
    public class DrugNames
    {
        /// <summary>
        /// 药品名称
        /// </summary>
        public string drugName;
    }

    /// <summary>
    /// 药品名称列表
    /// </summary>
    public class DrugNamesList:List<DrugNames>{}

    /// <summary>
    /// 药品类型以及对应该类别的药品名称列表
    /// </summary>
    public class MedicineDict
    {
        /// <summary>
        /// 药品类别
        /// </summary>
        public string drugType;
        /// <summary>
        /// 药品名称列表
        /// </summary>
        public DrugNamesList drugNamesList;
        public MedicineDict()
        {
            drugType = string.Empty;
            drugNamesList = new DrugNamesList();
        }
        public void Clear()
        {
            drugType = string.Empty;
            drugNamesList.Clear() ;
        }
    }

    /// <summary>
    /// MedicineDic列表
    /// </summary>
   // public class MedicineDictList : List<MedicineDict>{}

    /// <summary>
    /// 用药单位
    /// </summary>
    public class DrugUnits
    {
        /// <summary>
        /// 用药单位
        /// </summary>
        public string drugUnit;
    }
    /// <summary>
    /// 用药单位列表
    /// </summary>
    //public class DrugUnitsList : List<DrugUnits>{}

    /// <summary>
    /// 用药途径
    /// </summary>
    public class DrugByRoute
    {
        /// <summary>
        /// 用药途径
        /// </summary>
        public string drugByRoute;
    }

    /// <summary>
    /// 用药途径列表
    /// </summary>
   // public class DrugByRouteList : List<DrugByRoute>{}

    /// <summary>
    /// 用药频次
    /// </summary>
    public class DrugFrequency
    {
        /// <summary>
        /// 用药频次
        /// </summary>
        public string drugFrequency;
    }

    /// <summary>
    /// 用药频次列表
    /// </summary>
    //public class DrugFrequencyList : List<DrugFrequency>{}
}
