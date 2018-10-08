using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;

namespace CDSSCLIPSEngine
{
    public static class ClipsConfig
    {
        public static String testName = "123";
        /// <summary>
        /// ∂¡»°≈‰÷√Œƒº˛
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <returns></returns>
        public static string ReadConfig(string strNodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigurationManager.AppSettings["CLIPSAPP_PATH"]);
            string strNodepath = string.Format("/{0}/{1}/{2}", "CLIPSApp", "Config", strNodeName);
            XmlNode n_Node = xmlDoc.SelectSingleNode(strNodepath);
            return n_Node.InnerText;
        }
    }
}
