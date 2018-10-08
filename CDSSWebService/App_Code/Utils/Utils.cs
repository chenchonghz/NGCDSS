using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Utils
/// </summary>
namespace Utils
{
    public static class Security {
        public static string Md5Security(string pwd)
        {
            string pwd_MD5;  //加密后数据
            pwd_MD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
            return pwd_MD5;
        }
    }
}