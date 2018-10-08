using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace CDSS
{
    class PatInfo
    {
        /// <summary>
        /// true:当前病人是新入的;
        /// false:当前病人是从数据库中查询出来的
        /// </summary>
        public static bool bNewPatient = false;

        //是否控制必填项
        public static bool bMustFill = false;

        public static PrintDataSource pds;
        //public static PrintDataSource PrtSource;
        public static int patientid = 0;    //患者编号，外键，BasicInfo表,这些初始化的时间？        
       
       
    }
}
