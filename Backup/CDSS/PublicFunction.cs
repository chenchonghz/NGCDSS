using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CDSS
{
    public class PublicFunction
    {
        //InfoEnter frminfoenter;
        public Form NowForm;
        public Form OldForm;

        public static bool IsFloat(string result)
        {
            int length = result.Length;
            for (int i = 0; i < length; i++)
            {
                if (!Char.IsDigit(result[0]))
                    return false;
                if (Char.IsDigit(result[i]) || result[i] == '.')
                    continue;
                else
                    return false;
            }
            return true;
        }      

        public static bool IsInt(string result)
        {
            int length = result.Length;
            for (int i = 0; i < length; i++)
            {
                if (!Char.IsDigit(result[0]))
                    return false;
                if (Char.IsDigit(result[i]))
                    continue;
                else
                    return false;
            }
            return true;
        }

        public static void FormChangeProcedure()
        {
            //PatInfo.NowForm
            //PatInfo.OldForm

        }

        public static void Basicinfofromchange()
        {
            
            


        }
    }
}
