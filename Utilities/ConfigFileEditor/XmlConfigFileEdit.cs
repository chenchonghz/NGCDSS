using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Utilities.ConfigFileEditor
{
    public class XmlConfigFileEdit:ConfigFileEdit 
    {
        public override DataSet ConfigFileToDataSet(string filePath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(filePath);
            return ds;
        }

        public override void SaveFileChange(DataSet ds, string filePath)
        {
            try
            {
                if (ds != null)
                {
                    ds.WriteXml(filePath);
                }
            }
            catch (System.IO.IOException e)
            {
                throw e;
            }
        }
    }
}
