using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{

    public static class FuncPLC
    {
        public static string SymbolName(object sender)
        {
            string retval = "";
            string btnType = sender.GetType().ToString();
            if (btnType == "Helper.CompBitButton")
            {
                CompBitButton btnObj = (CompBitButton)sender;
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
