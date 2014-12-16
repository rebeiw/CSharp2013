using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class SQLHelper
{

    public enum SQLBuilder
    {
        Normal,
        DeleteSQL,
        NoSpace
    }

    public struct SQLSpalten
    {
        public string Feldname;
        public string Feldtype;
    }

    public struct SQLWerte
    {
        public string Feldname;
        public string Wert;
        public string Feldtype;
    }
    public struct Statistik
    {
        public double Mittelwert;
        public double Varianz;
        public double StandardAbweichung;
        public double Minimun;
        public double Maximun;

    }


    public SqlConnection Con;
    public SqlCommand Cmd;
    private StringBuilder m_SQLBefehl;
    private string m_ConnectionString;
    public string ConnectionString
    {
        get
        {
            return m_ConnectionString;
        }
        set
        {
            m_ConnectionString = value;
        }
    }
    public SQLHelper()
    {

        this.ConnectionString =     "Data Source=WE1S043;" +
                                    "Initial Catalog=Kalibwin;" +
                                    "User ID=Kalibuser;" +
                                    "Password=''";
        this.Open();
    }

    public SQLHelper(string ConnectionString)
    {
        this.ConnectionString = ConnectionString;
        this.Open();
    }

    public void SQLExec(StringBuilder SQLBefehl)
    {
        this.SQLExec(SQLBefehl.ToString());
    }

    public void SQLExec(string SQLBefehl)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = this.ConnectionString;

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = this.Con;
        con.Open();
        cmd.CommandText=SQLBefehl;
        cmd.ExecuteNonQuery();
        this.SQLBuilderLoeschen();
        con.Close();
        cmd = null;
        con = null;
    }
    public void LoescheTabelle(string Tabelle)
    {
        this.SQLExec("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='" + Tabelle + "') drop table " + Tabelle + "");
    }


    private void Open()
    {
        this.m_SQLBefehl = new StringBuilder();
        this.Con=new SqlConnection();
        this.Con.ConnectionString=this.ConnectionString;
        this.Cmd = new SqlCommand();
        this.Cmd.Connection = this.Con;
        this.Con.Open();
    }

    public List<string> GetColumns(string TableName)
    {
        List<string> retval = new List<string>();
        string sqlCmd = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + TableName + "'";
        retval = this.SQL2StringList(sqlCmd);
        return retval;
    }


    public void CheckTable(string TableName)
    {
        this.SQLBuilderHinzufuegen("IF NOT EXISTS", SQLBuilder.DeleteSQL);
        this.SQLBuilderHinzufuegen("(");
        this.SQLBuilderHinzufuegen("SELECT 1 FROM INFORMATION_SCHEMA.TABLES");
        this.SQLBuilderHinzufuegen("WHERE");
        this.SQLBuilderHinzufuegen("TABLE_TYPE='BASE TABLE'");
        this.SQLBuilderHinzufuegen("AND");
        this.SQLBuilderHinzufuegen("TABLE_NAME='",SQLBuilder.NoSpace);
        this.SQLBuilderHinzufuegen(TableName, SQLBuilder.NoSpace);
        this.SQLBuilderHinzufuegen("'");
        this.SQLBuilderHinzufuegen(")");
        this.SQLBuilderHinzufuegen("CREATE TABLE");
        this.SQLBuilderHinzufuegen(TableName);
        this.SQLBuilderHinzufuegen("(");
        this.SQLBuilderHinzufuegen("ID int IDENTITY(1,1) NOT NULL");
        this.SQLBuilderHinzufuegen(")");
        string sql=this.SQLBuilderHinzufuegen("ON [PRIMARY]");
        this.SQLExec(sql);

    }


    public void CheckTableSpalten(string TableName,List<SQLSpalten>Spalten)
    {
        this.CheckTable(TableName);
        foreach (SQLSpalten sqlSpalte in Spalten)
        {
            this.SQLBuilderHinzufuegen("IF NOT EXISTS",SQLBuilder.DeleteSQL);
            this.SQLBuilderHinzufuegen("(");
            this.SQLBuilderHinzufuegen("SELECT 1 COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS");
            this.SQLBuilderHinzufuegen("WHERE TABLE_NAME ='",SQLBuilder.NoSpace);
            this.SQLBuilderHinzufuegen(TableName,SQLBuilder.NoSpace);
            this.SQLBuilderHinzufuegen("'");
            this.SQLBuilderHinzufuegen("AND");
            this.SQLBuilderHinzufuegen("COLUMN_NAME ='",SQLBuilder.NoSpace);
            this.SQLBuilderHinzufuegen(sqlSpalte.Feldname,SQLBuilder.NoSpace);
            this.SQLBuilderHinzufuegen("'");
            this.SQLBuilderHinzufuegen(")");
            this.SQLBuilderHinzufuegen("ALTER TABLE");
            this.SQLBuilderHinzufuegen(TableName);
            this.SQLBuilderHinzufuegen("ADD");
            this.SQLBuilderHinzufuegen(sqlSpalte.Feldname);
            string sql=this.SQLBuilderHinzufuegen(sqlSpalte.Feldtype);
            this.SQLExec(sql);
        }
    }


    public List<string> SQL2StringList(StringBuilder SQLBefehl, bool MitHeader = false)
    {
        List<string> retval = new List<string>();
        retval=this.SQL2StringList(SQLBefehl.ToString(),MitHeader);
        return retval;
    }

    public List<double> SQL2DoubleList(string SQLBefehl)
    {
        List<double> retval = new List<double>();
        List<string> werte = new List<string>();
        werte = this.SQL2StringList(SQLBefehl);
        foreach (string wert in werte)
        {
            double zahl = 0.0;
            double.TryParse(wert, out zahl);
            retval.Add(zahl);
        }
        werte = null;
        return retval;
    }

    public object SQL2List(string SQLBefehl, bool MitHeader = false)
    {
        this.Cmd.CommandText = SQLBefehl;
        SqlDataReader reader = this.Cmd.ExecuteReader();
        object retval = reader;
        return retval;
    }

    public List<string> SQL2StringList(string SQLBefehl, bool MitHeader=false)
    {
        string zeile = "";
        List<string> retval = new List<string>();
        this.Cmd.CommandText = SQLBefehl;
        SqlDataReader reader = this.Cmd.ExecuteReader();

        if (MitHeader)
        {
            zeile = "";

            for (int i = 0; i < reader.FieldCount; i++)
            {
                zeile += reader.GetName(i).ToString();
                if (i < reader.FieldCount - 1)
                {
                    zeile += "\t";
                }
            }
            retval.Add(zeile);
        }
        while (reader.Read())
        {
            zeile = "";
            for (int i = 0; i < reader.FieldCount; i++)
            {
                zeile += reader.GetValue(i).ToString();
                if (i < reader.FieldCount - 1)
                {
                    zeile += "\t";
                }
            }
            retval.Add(zeile);
        }
        reader.Close();
        return retval;
    }

    public List<ExcelDichtHelper.Kontrolle> HoleMesswerteKontrolle(string Link)
    {
        List<ExcelDichtHelper.Kontrolle> retval = new List<ExcelDichtHelper.Kontrolle>();
        StringBuilder sql = new StringBuilder();
        ExcelDichtHelper.Kontrolle Einzelwerte;
        sql.Clear();
        sql.Append("SELECT ");
        sql.Append("Link, ");
        sql.Append("MessNR, ");
        sql.Append("Temperatur1 AS RefTemp, ");
        sql.Append("Dichte AS RefDichte, ");
        sql.Append("VWert7 AS RCDichte ");
        sql.Append("FROM ");
        sql.Append("MW_WERTE_MESSUNG ");
        sql.Append("WHERE ");
        sql.Append("(Link = N'");
        sql.Append(Link);
        sql.Append("') ");
        sql.Append("AND ");
        sql.Append("(Abweichung <> 0) ");
        sql.Append("AND ");
        sql.Append("(VWert7 <> 0) ");
        sql.Append("ORDER BY MessNR");

        this.Cmd.CommandText = sql.ToString();
        SqlDataReader reader = this.Cmd.ExecuteReader();
        int messNr = 1;
        while (reader.Read())
        {
            Einzelwerte.MessNr = messNr;
            Einzelwerte.RefTemp = Convert.ToDouble(reader.GetValue(2).ToString());
            Einzelwerte.RefDichte = Convert.ToDouble(reader.GetValue(3).ToString());
            Einzelwerte.RCDichte = Convert.ToDouble(reader.GetValue(4).ToString());
            retval.Add(Einzelwerte);
            messNr++;
        }
        reader.Close();
        return retval;
    }

    public ExcelDichtHelper.Kopfdaten HoleKopfdaten(string Link)
    {
        ExcelDichtHelper.Kopfdaten retval;
        StringBuilder sql = new StringBuilder();

        sql.Clear();
        sql.Append("SELECT ");
        sql.Append("LINK, ");
        sql.Append("CONVERT(varchar, Datum, 104) AS Datum, ");
        sql.Append("MS_Code AS MCCode, ");
        sql.Append("MConvert AS MCCodeConverter ");
        sql.Append("FROM ");
        sql.Append("dbo.MW_KENNDATEN_EINGABE ");
        sql.Append("WHERE ");
        sql.Append("(Link = N'");
        sql.Append(Link);
        sql.Append("') ");

        this.Cmd.CommandText = sql.ToString();
        SqlDataReader reader = this.Cmd.ExecuteReader();
        reader.Read();
        retval.Link = reader.GetValue(0).ToString();
        retval.Datum = reader.GetValue(1).ToString();
        retval.MSCode = reader.GetValue(2).ToString();
        retval.MSCodeConverter = reader.GetValue(3).ToString();
        reader.Close();

        return retval;
    }

    public ExcelDichtHelper.Justierkonstanten HoleJustierkonstanten(string Link)
    {
        ExcelDichtHelper.Justierkonstanten retval;
        StringBuilder sql = new StringBuilder();

        sql.Clear();
        sql.Append("SELECT  ");
        sql.Append("Link,  ");
        sql.Append("R33 AS FL20,  ");
        sql.Append("R34 AS KD,  ");
        sql.Append("R35 * 10000.0 AS FTC1, ");
        sql.Append("R36 * 10000000.0 AS FTCK ");
        sql.Append("FROM ");
        sql.Append("MW_ERGEBNIS ");
        sql.Append("WHERE ");
        sql.Append("(Link = N'");
        sql.Append(Link);
        sql.Append("') ");

        this.Cmd.CommandText = sql.ToString();
        SqlDataReader reader = this.Cmd.ExecuteReader();
        reader.Read();
        retval.FL20 = Convert.ToDouble(reader.GetValue(1).ToString());
        retval.KD = Convert.ToDouble(reader.GetValue(2).ToString());
        retval.FTC1 = Convert.ToDouble(reader.GetValue(3).ToString());
        retval.FTCK = Convert.ToDouble(reader.GetValue(4).ToString());
        reader.Close();

        return retval;
    }
    public Statistik HoleStatistikWerte(string Tabelle, string Spalte)
    {
        Statistik retval;
        StringBuilder sql = new StringBuilder();
        double[] werte = new double [1];
        sql.Clear();
        sql.Append("SELECT ");
        sql.Append(Spalte);
        sql.Append(" ");
        sql.Append("FROM ");
        sql.Append(Tabelle);
        werte[0] = 0.0;
        

        retval.Mittelwert = 0.0;
        retval.StandardAbweichung = 0.0;
        retval.Varianz = 0.0;
        retval.Minimun = 0.0;
        retval.Maximun = 0.0;

        this.Cmd.CommandText = sql.ToString();
        SqlDataReader reader = this.Cmd.ExecuteReader();
        int messNr = 0;
        while (reader.Read())
        {
            Array.Resize<double>(ref  werte, messNr + 1);
            werte[messNr] = Convert.ToDouble(reader.GetValue(0).ToString());
            messNr++;
        }
        double summe = 0.0;
        int anz = 0;
        summe = werte.Sum();
        anz = werte.Count();
        retval.Mittelwert = summe / anz;

        retval.Mittelwert=werte.Average();
        retval.Minimun = werte.Min();
        retval.Maximun = werte.Max();

        double quadrate=0.0;
        foreach (double wert in werte)
        {
            quadrate+=Math.Pow(wert - retval.Mittelwert, 2);
        }
        retval.Varianz = quadrate / anz;
        retval.StandardAbweichung = Math.Sqrt(retval.Varianz);
        reader.Close();


        return retval;
    }

    public int HoleAnzahlDatensaetze(string Tabelle)
    {
        StringBuilder sql = new StringBuilder();
        int retval = 0;
        sql.Clear();
        sql.Append("SELECT ");
        sql.Append("COUNT(*) AS Anzahl ");
        sql.Append("FROM ");
        sql.Append(Tabelle);
        this.Cmd.CommandText = sql.ToString();
        SqlDataReader reader = this.Cmd.ExecuteReader();
        reader.Read();
        retval = Convert.ToInt32(reader.GetValue(0).ToString());
        reader.Close();
        return retval;

    }

    public List<ExcelDichtHelper.Kennline> HoleMesswerteKennlinie(string Link, string SQLBefehl = "")
    {
        List <ExcelDichtHelper.Kennline> retval=new List<ExcelDichtHelper.Kennline>();
        StringBuilder sql = new StringBuilder();
        ExcelDichtHelper.Kennline Einzelwerte;

        if (SQLBefehl.Length > 0)
        {
            sql.Clear();
            sql.Append(SQLBefehl);
        }
        if (Link.Length > 0)
        {
            string link = Link.Trim();
            sql.Clear();
            sql.Append("SELECT ");
            sql.Append("Link, ");
            sql.Append("MessNR, ");
            sql.Append("VWert10 AS RCTemp, ");
            sql.Append("Temperatur1 AS RefTemp, ");
            sql.Append("Dichte AS RefDichte, ");
            sql.Append("VWert9 AS RCFreq ");
            sql.Append("FROM ");
            sql.Append("MW_WERTE_MESSUNG ");
            sql.Append("WHERE ");
            sql.Append("(Link = N'");
            sql.Append(link);
            sql.Append("') ");
            sql.Append("AND ");
            sql.Append("(Abweichung = 0) ");
            sql.Append("AND ");
            sql.Append("(VWert7 = 0) ");
            sql.Append("ORDER BY MessNR");
        }



        this.Cmd.CommandText = sql.ToString();
        SqlDataReader reader = this.Cmd.ExecuteReader();
        int messNr = 1;
        while (reader.Read())
        {
            Einzelwerte.MessNr = messNr;
            Einzelwerte.RCTemp = Convert.ToDouble(reader.GetValue(2).ToString());
            Einzelwerte.RefTemp = Convert.ToDouble(reader.GetValue(3).ToString());
            Einzelwerte.RefDichte = Convert.ToDouble(reader.GetValue(4).ToString());
            Einzelwerte.RCFreq = Convert.ToDouble(reader.GetValue(5).ToString());
            retval.Add(Einzelwerte);
            messNr++;
        }
        reader.Close();
        return retval;
    }
    public void SQLBuilderLoeschen()
    {
        if (this.m_SQLBefehl != null)
        {
            this.m_SQLBefehl.Clear();
        }
    }

    public string SQLBuilderHinzufuegen(string SQLBefehl, SQLBuilder SQLAction=SQLBuilder.Normal)
    {
        string retval = "";
        if (SQLAction == SQLBuilder.DeleteSQL)
        {
            this.SQLBuilderLoeschen();
        }

        this.m_SQLBefehl.Append(SQLBefehl);
        if ((SQLAction == SQLBuilder.Normal || SQLAction == SQLBuilder.DeleteSQL) && SQLBefehl.Length>0)
        {
            this.m_SQLBefehl.Append(" ");
        }

        retval = m_SQLBefehl.ToString();
        return retval;

    }
    public string HoleDatentyp(string Tabelle, string Spaltenamen)
    {
        string retval = "";
        DataTable dtCol = this.Con.GetSchema("Columns");
        DataRow[] dr = dtCol.Select("TABLE_NAME='" + Tabelle + "'");
        for (int i = 0; i < dr.Count(); i++)
        {
            string spaltenName = dr[i][3].ToString();
            if (spaltenName == Spaltenamen)
            {
                retval = dr[i]["DATA_TYPE"].ToString();
                break;
            }
        }
        return retval;
    }

    public void InsertData(string Tabelle, List<SQLWerte> Werte)
    {
        this.SQLBuilderHinzufuegen("INSERT INTO", SQLBuilder.DeleteSQL);
        this.SQLBuilderHinzufuegen(Tabelle);
        this.SQLBuilderHinzufuegen("(");
        for (int i = 0; i < Werte.Count(); i++)
        {
            this.SQLBuilderHinzufuegen(Werte[i].Feldname);
            if (i < Werte.Count - 1)
            {
                this.SQLBuilderHinzufuegen(",");
            }
        }
        this.SQLBuilderHinzufuegen(")");
        this.SQLBuilderHinzufuegen("VALUES");
        this.SQLBuilderHinzufuegen("(");
        for (int i = 0; i < Werte.Count(); i++)
        {
            string wert = "";
            string datenTyp = "";
            datenTyp = Werte[i].Feldtype;
            if (datenTyp == "nchar")
            {
                wert = "'" + Werte[i].Wert + "'";
            }
            else
            {
                wert = Werte[i].Wert;
            }

            this.SQLBuilderHinzufuegen(wert);
            if (i < Werte.Count - 1)
            {
                this.SQLBuilderHinzufuegen(",");
            }
        }
        this.SQLBuilderHinzufuegen(")");
        this.SQLExec(this.m_SQLBefehl);

    }

    public void UpdateData(string Tabelle, List<SQLWerte> Werte, string Where)
    {
        StringBuilder sql = new StringBuilder();
        this.SQLBuilderHinzufuegen("UPDATE", SQLBuilder.DeleteSQL);
        this.SQLBuilderHinzufuegen(Tabelle);
        this.SQLBuilderHinzufuegen("SET");
        

        int zaehler=0;
        foreach (SQLWerte wert in Werte)
        {
            string wertUpdate = wert.Wert;
            this.SQLBuilderHinzufuegen(wert.Feldname);
            this.SQLBuilderHinzufuegen("=");
            string datenTyp = "";
            datenTyp = wert.Feldtype;

            if ((datenTyp == "nchar") || (datenTyp == "varchar"))
            {
                wertUpdate = "'" + wert.Wert + "'";
            }
            else
            {
                wertUpdate = wert.Wert;
            }
            this.SQLBuilderHinzufuegen(wertUpdate);

            if (zaehler < Werte.Count - 1)
            {
                this.SQLBuilderHinzufuegen(",");
            }
            zaehler++;
        }
        this.SQLBuilderHinzufuegen(Where);
        this.SQLExec(this.m_SQLBefehl);
    }
}
