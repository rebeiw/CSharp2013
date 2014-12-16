using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{

    public static class FuncPLC
    {
        public static void ReadBit(libnodave.daveConnection dc, string Adresse)
        {
            byte[] db51 = new byte[2];
            int anz= db51.Count();
            dc.readManyBytes(libnodave.daveDB, 51, 0, anz, db51);
        }
        public static string SymbolName(object sender)
        {
            string retval = "";
            string btnType = sender.GetType().ToString();
            if (btnType == "Helper.BitButton")
            {
                BitButton btnObj = (BitButton)sender;
                retval = btnObj.Symbol;
            }
            if (btnType == "Helper.RotaBitButton")
            {
                RotaBitButton btnObj = (RotaBitButton)sender;
                retval = btnObj.Symbol;
            }

            if (btnType == "Helper.InputBox")
            {
                InputBox btnObj = (InputBox)sender;
                retval = btnObj.Symbol;
            }


            return retval;
        }


    }
}
