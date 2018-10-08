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

        ////��ȡ����MAC��ַ
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

        //���CPU�����к�
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

        //���ɻ�����
        public static string getMNum()
        {
            try
            {
                //���Ӳ�����к�
                string strNum = GetDiskVolumeSerialNumber();
                //MD5��������32λ
                string strMNum = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strNum, "MD5");
                return strMNum;
            }
            catch
            {
                return "00000000000000000000000000000000";
            }

        }

        private static int[] intCode = new int[127];//�洢��Կ
        private static int[] intNumber = new int[33];//��������Asciiֵ
        private static char[] Charcode = new char[33];//�洢��������

        private static void setIntCode()//�����鸳ֵС��10����
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }

        //����ע���� 
        public static string getRNum()
        {
            setIntCode();//��ʼ��127λ����
            for (int i = 1; i < Charcode.Length; i++)//�ѻ��������������
            {
                Charcode[i] = Convert.ToChar(getMNum().Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)//���ַ���ASCIIֵ����һ���������С�
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
            }
            string strAsciiName = "";//���ڴ洢ע����
            for (int j = 1; j < intNumber.Length; j++)
            {
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//�ж��ַ�ASCIIֵ�Ƿ�0��9֮��
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//�ж��ַ�ASCIIֵ�Ƿ�A��Z֮��
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//�ж��ַ�ASCIIֵ�Ƿ�a��z֮��
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else//�ж��ַ�ASCIIֵ�������Ϸ�Χ��
                {
                    if (intNumber[j] > 122)//�ж��ַ�ASCIIֵ�Ƿ����z
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
        /// ���ע��
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
