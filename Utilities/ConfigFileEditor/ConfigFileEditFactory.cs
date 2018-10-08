using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.ConfigFileEditor
{
    public class ConfigFileEditFactory
    {
        /// <summary>
        /// 根据fileType创建相应的ConfigFileEdit对象
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public ConfigFileEdit CreatConfigFileEdit(string fileType)
        {
            switch (fileType)
            {
                case ".xml":
                    return new XmlConfigFileEdit();
                case ".ini":
                    return new IniConfigFileEdit();
                default:
                    throw new NotImplementedException ("该文件编辑器还不能处理该类型的配置文件");
            }
        }    
    }
}
