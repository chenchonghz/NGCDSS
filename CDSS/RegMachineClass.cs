using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Microsoft.Win32;


namespace CDSS
{
    public static class RegMachineClass
    {
        private static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mcHD = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc = mcHD.GetInstances();
            string strID = null;
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["SerialNumber"].Value.ToString().Trim();
                break;
            }
            return strID;
        }

        ////获取网卡MAC地址
        //private static string getNetCardMacAddress()
        //{
        //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    ManagementObjectCollection moc = mc.GetInstances();
        //    string str = "";
        //    foreach (ManagementObject mob in moc)
        //    {
        //        //MessageBox.Show(mob.GetText(TextFormat.Mof));
        //        if ((bool)mob["IPEnabled"] == true)
        //        {
        //            str = mob["MacAddress"].ToString().Replace(":", "").Trim();
        //        }
        //    }
        //    return str;
        //}

        //获得CPU的序列号
        //private static string getCpu()
        //{
        //    string strCpu = null;
        //    ManagementClass myCpu = new ManagementClass("win32_Processor");
        //    ManagementObjectCollection myCpuConnection = myCpu.GetInstances();


        //    foreach (ManagementObject myObject in myCpuConnection)
        //    {
        //        strCpu = myObject.Properties["Processorid"].Value.ToString();
        //        break;
        //    }
        //    return strCpu;
        //}

        //生成机器码
        public static string getMNum()
        {
            try
            {
                //获得硬盘序列号
                string strNum = GetDiskVolumeSerialNumber();
                //MD5加密生成32位
                string strMNum = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strNum, "MD5");
                return strMNum;
            }
            catch
            {
                return "00000000000000000000000000000000";
            }

        }

        private static int[] intCode = new int[127];//存储密钥
        private static int[] intNumber = new int[33];//存机器码的Ascii值
        private static char[] Charcode = new char[33];//存储机器码字

        private static void setIntCode()//给数组赋值小于10的数
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }

        //生成注册码 
        public static string getRNum()
        {
            setIntCode();//初始化127位数组
            for (int i = 1; i < Charcode.Length; i++)//把机器码存入数组中
            {
                Charcode[i] = Convert.ToChar(getMNum().Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
            }
            string strAsciiName = "";//用于存储注册码
            for (int j = 1; j < intNumber.Length; j++)
            {
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else//判断字符ASCII值不在以上范围内
                {
                    if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString();
                    }
                    else
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                    }
                }
            }
            Reg = strAsciiName;
            return strAsciiName;
        }

        public static string Reg = "";

        /// <summary>
        /// 检查注册
        /// </summary>
        public static bool CheckRegist()
        {
            getRNum();
            string RegNum = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\NGCDSS", "RegNum", null);
            if (RegNum == null)
                return false;
            if (RegNum == Reg)
                return true;
            else
                return true;
        }
    }
}
