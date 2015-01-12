using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
namespace Datenauswertung
{
    public static class HelperDatenauswertung
    {
        
        public static void asd()
        {
            SQLHelper sqlHelper = new SQLHelper(GlobalVar.Glb_SQLConnecton);
            List<string>daten=new List<string>();
            List<SQLHelper.SQLWerte>update=new List<SQLHelper.SQLWerte>();
            SQLHelper.SQLWerte zeile;
            daten = sqlHelper.SQL2StringList("select id,date,time from maschdaten", false);

            foreach (string data in daten)
            {
                string[] spalten = data.Split('\t');


                string neuesDatum = "";

                string datum = spalten[1].Substring(0, 10);

                neuesDatum = datum.Substring(6, 4) + "-"+datum.Substring(3, 2) + "-"+datum.Substring(0, 2);
                update.Clear();
                zeile.Feldname = "da";
                zeile.Feldtype = "";
                zeile.Wert = "'" + neuesDatum + " " + spalten[2] + "'";
                update.Add(zeile);
                sqlHelper.UpdateData("maschdaten", update, "where ID=" + spalten[0]);

            }
            sqlHelper=null;
        }
    }


}
