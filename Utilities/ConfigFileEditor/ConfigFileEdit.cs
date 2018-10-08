using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Utilities.ConfigFileEditor
{
    public abstract class ConfigFileEdit
    {
        //将要进行编辑的配置文件转换为DataSet形式
        public abstract DataSet ConfigFileToDataSet(string filePath);

        //文件编辑后保存相应的变化
        public abstract void SaveFileChange(DataSet ds, string filePath);
    }
}
