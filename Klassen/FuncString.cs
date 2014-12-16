using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class FuncString
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="FillChar"></param>
        /// <param name="NumberOf"></param>
        /// <returns></returns>
        public static string FillForward(string Text,string FillChar, int NumberOf)
        {
            string retval           = "";
            int length              = Text.Length;
            int number              = NumberOf - length;
            char fillChar           = Convert.ToChar(FillChar);
            string fill             = new string(fillChar, number);
            retval                  = fill + Text;

            return retval;
        }
        public static string ClearSpace(string Text)
        {
            string retval = "";
            for (int i = 0; i < Text.Length; i++)
            {
                string zeichen = Text.Substring(i, 1);
                if(zeichen!=" ")
                {
                    retval+=zeichen;
                }
            }
            return retval;
            
        }

        public static string GetTimestamp()
        {
            string retval = "";
            DateTime timestamp = DateTime.Now;
            string uhrzeit = "'" + timestamp.ToLongTimeString() + "'";
            string datum = "'" + timestamp.Year + "-" + timestamp.Month + "-" + timestamp.Day + "'";
            retval = datum + " " + uhrzeit;
            return retval;
        }

        public static string ConvertDate2En(string Datum)
        {
            string retval = "";
            retval = Datum.Substring(6, 4) + "-" + Datum.Substring(3, 2) + "-" + Datum.Substring(0, 2) + Datum.Substring(10); ;
            return retval;
        }


        public static int GetAsc(char Input)
        {
            int retval;
            retval=Convert.ToInt32(Input);
            return retval;

            
        }

        public static string GetOnlyNumeric(string Input)
        {
            string retval="";
            char[] zeichen = Input.ToCharArray();
            int anz=zeichen.Count();
            for (int i = 0; i < anz; i++)
            {
                if (GetAsc(zeichen[i]) >= 48 && GetAsc(zeichen[i])<=57)
                {
                    retval += zeichen[i];
                }
            }
            return retval;
        }

        public static string Byte2BitString(byte Input)
        {
            string retval = "";
            int cvrt = Convert.ToInt32(Input);
            for (int i = 0; i < 8; i++)
            {
                int valence = Convert.ToInt32(Math.Pow(2, i)) & Input;
                if (valence > 0)
                {
                    retval += "1";
                }
                else
                {
                    retval += "0";
                }
            }
            return retval;
        }

    }
}
