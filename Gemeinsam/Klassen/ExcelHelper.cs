using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Helper;

public enum XlHelperTypeMinMax
{
    MinType = 1,
    MaxType,
    AvgType
}

public class ExcelHelper
{
    public Microsoft.Office.Interop.Excel.Application XlApp=null;
    public Microsoft.Office.Interop.Excel.Workbook XlWorkBook = null;
    public Microsoft.Office.Interop.Excel.Worksheet XlWorkSheet = null;
    public Microsoft.Office.Interop.Excel.Chart XlChart = null;
    public int AnzYWerte = 0;

    private bool CanClose;

    public ExcelHelper()
    {
        this.XlApp = new Microsoft.Office.Interop.Excel.Application();
        this.XlApp.Visible = true;
        this.XlApp.DisplayAlerts = false;
    }

    public void ExcelHelperClose()
    {
        if (this.CanClose)
        {
            this.XlApp.Workbooks.Close();
            this.XlApp.Quit();
            this.XlWorkSheet = null;
            this.XlWorkBook = null;
            this.XlApp = null;
        }
    }

    private double m_Y_MinWert;
    public double YMinwert
    {
        get { return m_Y_MinWert; }
        set { m_Y_MinWert = value; this.XlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue).MinimumScale = m_Y_MinWert; }
    }

    private double m_Y_MaxWert;
    public double YMaxwert
    {
        get { return m_Y_MaxWert; }
        set { m_Y_MaxWert = value; this.XlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue).MaximumScale = m_Y_MaxWert; }
    }

    private double m_X_MinWert;
    public double XMinwert
    {
        get { return m_X_MinWert; }
        set { m_X_MinWert = value; this.XlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory).MinimumScale=m_X_MinWert; }
    }

    private double m_X_MaxWert;
    public double XMaxwert
    {
        get { return m_X_MaxWert; }
        set { m_X_MaxWert = value; this.XlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory).MaximumScale = m_X_MaxWert; }
    }

    private double m_Crosses_At;
    public double CrossesAt
    {
        get { return m_Crosses_At; }
        set { m_Crosses_At = value; this.set_CrossesAt();
        }
    }

    private string teste;
    public string Teste
    {
        get { return teste; }
        set { teste = value; }
    }

    private void set_CrossesAt()
    {
        this.XlChart.Axes(2).CrossesAt = m_Crosses_At;
    }

    public void Datei_Oeffnen(string Datei)
    {
        this.XlWorkBook = XlApp.Workbooks.Open(Datei);
        this.XlWorkSheet = this.XlWorkBook.Worksheets[1];
        this.CanClose = true;
    }

    public void Workbook_Hinzufuegen()
    {
        this.XlWorkBook = XlApp.Workbooks.Add();
        this.XlWorkSheet = this.XlWorkBook.Worksheets[1];
        this.CanClose = true;
    }

    public void Chart_Hinzufuegen(string DiagrammTitle, string TextXAchse, string TextYAchse, Microsoft.Office.Interop.Excel.XlChartType ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlXYScatter)
    {
        this.CanClose = true;
        this.XlWorkBook = XlApp.Workbooks.Add();

        this.XlChart = XlWorkBook.Charts.Add();
        this.XlChart.HasTitle = true;
        this.XlChart.ChartTitle.Text = DiagrammTitle;
        this.XlChart.ChartType = ChartType;


        this.XlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary).HasTitle = true;
        this.XlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary).AxisTitle.Text = TextYAchse;

        this.XlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary).HasTitle = true;
        this.XlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary).AxisTitle.Text = TextXAchse;
    }

    public void Setze_TabelleBlatt(string Name)
    {
        this.XlWorkSheet=this.XlWorkBook.Worksheets.get_Item(Name);
    }

    public void Setze_TabelleBlattName(string AlterName,string Name)
    {
        this.Setze_TabelleBlatt(AlterName);
        this.XlWorkSheet.Name = Name;
    }


    public string Hole_Wert(int Zeile, int Spalte)
    {
        string retval = "";
        retval = Convert.ToString(this.XlWorkSheet.Cells[Zeile, Spalte].Value);
        return retval;
    }


    public void Setze_Wert(int Zeile, int Spalte,string Wert)
    {
        try
        {
            this.XlWorkSheet.Cells[Zeile, Spalte] = Wert;
        }
        catch 
        {
            MessageBox.Show("Fehler bei Ausgabe der Spalte " + Spalte + ", Zeile " + Zeile);
        }
    }

    public void Min_Max_Achse_hinzufuegen(double MinMaxWert, int AnzYWerte,XlHelperTypeMinMax MinMax)
    {
        Microsoft.Office.Interop.Excel.Series minMaxSerie;
        double[] minMaxWerte = new double[AnzYWerte];
        int farbe = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbBlack;
        for (int i = 0; i < AnzYWerte; i++)
        {
            minMaxWerte[i] = MinMaxWert;
        }
        string legende = "";
        if (MinMax == XlHelperTypeMinMax.MaxType)
        {
            legende = "Max";
            farbe = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
        }
        if (MinMax == XlHelperTypeMinMax.MinType)
        {
            legende = "Min";
            farbe = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
        }
        if (MinMax == XlHelperTypeMinMax.AvgType)
        {
            legende = "Avg";
            farbe = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;
        }


        minMaxSerie = this.Y_Achse_Hinzufuegen(minMaxWerte, legende);
        minMaxSerie.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleNone;
        minMaxSerie.Format.Line.Visible = MsoTriState.msoTrue;
        minMaxSerie.Format.Line.Weight = 1;
        minMaxSerie.Format.Line.Transparency = 0;
        minMaxSerie.Format.Line.ForeColor.RGB = farbe;
        minMaxSerie.Format.Line.ForeColor.TintAndShade = 0;
        minMaxSerie.Format.Line.ForeColor.Brightness = 0;
    }


    public void Min_Max_Achse_hinzufuegen(double MinMaxWert, XlHelperTypeMinMax MinMax)
    {
        Microsoft.Office.Interop.Excel.Series minMaxSerie;
        double[] minMaxWerte = new double[this.AnzYWerte];
        int farbe = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbBlack;
        for (int i = 0; i < this.AnzYWerte; i++)
        {
            minMaxWerte[i] = MinMaxWert;
        }
        string legende = "";
        if (MinMax == XlHelperTypeMinMax.MaxType)
        {
            legende = "Max";
            farbe=(int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
        }
        if (MinMax == XlHelperTypeMinMax.MinType)
        {
            legende = "Min";
            farbe=(int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
        }
        if (MinMax == XlHelperTypeMinMax.AvgType)
        {
            legende = "Avg";
            farbe=(int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;
        }


        minMaxSerie = this.Y_Achse_Hinzufuegen(minMaxWerte, legende);
        minMaxSerie.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleNone;
        minMaxSerie.Format.Line.Visible = MsoTriState.msoTrue;
        minMaxSerie.Format.Line.Weight = 1;
        minMaxSerie.Format.Line.Transparency = 0;
        minMaxSerie.Format.Line.ForeColor.RGB = farbe;
        minMaxSerie.Format.Line.ForeColor.TintAndShade = 0;
        minMaxSerie.Format.Line.ForeColor.Brightness = 0;
    }

    public void SheetDrucken(string Drucker)
    {
        XlWorkSheet.PrintOutEx(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Drucker);
    }

    public void ChartDrucken(string Drucker)
    {
        XlApp.Charts.PrintOutEx(Type.Missing,Type.Missing,Type.Missing,Type.Missing,Drucker);
    }
    public void DateiSpeichern(string Dateiname)
    {
        try
        {
            this.XlWorkBook.SaveAs(Dateiname);
        }
        catch
        {
            System.Windows.Forms.MessageBox.Show("Fehler beim Speichern der Datei '" + Dateiname + "'", "Fehler", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

    }

    public string ErstelleWerteReihe(string[] Werte)
    {
        string retval = "";
        int anzWerte = Werte.Count();
        retval = "={";
        for (int i = 0; i < anzWerte; i++)
        {
            string wandeln = Convert.ToString(Werte[i]);
            wandeln = wandeln.Replace(",", ".");
            wandeln = "\"" + wandeln + "\"";
            retval = retval + wandeln;
            retval.Replace(",", ".");
            if (i != anzWerte - 1)
            {
                retval = retval + ",";
            }
        }
        retval = retval + "}";
        return retval;
    }

    public string ErstelleWerteReihe(double[] Werte)
    {
        string retval = "";
        int anzWerte = Werte.Count();
        retval = "={";
        for (int i = 0; i < anzWerte; i++)
        {
            string wandeln = Convert.ToString(Werte[i]);
            wandeln = wandeln.Replace(",", ".");
            retval = retval + wandeln;
            retval.Replace(",", ".");
            if (i != anzWerte - 1)
            {
                retval = retval + ",";
            }
        }
        retval = retval + "}";
        return retval;
    }

    public Microsoft.Office.Interop.Excel.Series Y_Achse_Hinzufuegen(double[] Werte, string[] XWerte, string LegendeAchse)
    {
        Microsoft.Office.Interop.Excel.Series serie;
        int anzWerte = Werte.Count();
        serie = this.XlChart.SeriesCollection().NewSeries();
        serie.HasLeaderLines = true;
        serie.Name = LegendeAchse;
        serie.Values = this.ErstelleWerteReihe(Werte);
        string werteReihe=this.ErstelleWerteReihe(XWerte);
        serie.XValues = werteReihe;
        if (anzWerte > this.AnzYWerte)
        {
            this.AnzYWerte = anzWerte;
        }
        return serie;
    }


    public Microsoft.Office.Interop.Excel.Series Y_Achse_Hinzufuegen(double[] Werte, double[] XWerte, string LegendeAchse)
    {
        Microsoft.Office.Interop.Excel.Series serie;
        int anzWerte = Werte.Count();
        serie = this.XlChart.SeriesCollection().NewSeries();
        serie.HasLeaderLines = true;
        serie.Name = LegendeAchse;
        serie.Values = this.ErstelleWerteReihe(Werte);
        serie.XValues = this.ErstelleWerteReihe(XWerte);
        if (anzWerte > this.AnzYWerte)
        {
            this.AnzYWerte = anzWerte;
        }
        return serie;
    }

    public Microsoft.Office.Interop.Excel.Series Y_Achse_Hinzufuegen(double[] Werte, string LegendeAchse)
    {
        Microsoft.Office.Interop.Excel.Series serie;
        int anzWerte = Werte.Count();
        serie = this.XlChart.SeriesCollection().NewSeries();
        serie.HasLeaderLines = true;
        serie.Name = LegendeAchse;

        serie.Values = this.ErstelleWerteReihe(Werte);
       
        if (anzWerte > this.AnzYWerte)
        {
            this.AnzYWerte = anzWerte;
        }
        return serie;
    }

    public void Hinzufuegen_Statistik(SQLHelper.Statistik Statistik)
    {
        string text = "_" + "\n" + "x=" + Statistik.Mittelwert.ToString() + "\n" + "s²=" + Statistik.Varianz.ToString() + "\n" + "s=" + Statistik.StandardAbweichung.ToString() + "\n" + "min=" + Statistik.Minimun.ToString() + "\n" + "max=" + Statistik.Maximun.ToString();
        XlChart.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 550, 50, 300, 300).TextFrame.Characters().Text=text;
    }

    public void SQL2Excel(string SQLBefehl,string Connection="")
    {
        SQLHelper sqlHelper;
        if (Connection.Length > 0)
        {
            sqlHelper = new SQLHelper(Connection);
        }
        else
        {
            sqlHelper = new SQLHelper();
        }
        sqlHelper.Cmd.CommandText = SQLBefehl;
        SqlDataReader reader = sqlHelper.Cmd.ExecuteReader();

        int anz =reader.RecordsAffected;
        int anzCol = reader.FieldCount;
        int anzRow = 1;
        var data = new object[anzRow, anzCol];

        int row = 0;
        anzRow = 0;
        bool first_start = true;
        while (reader.Read())
        {
            if (first_start == true)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    data[row, i] = reader.GetName(i);
                }
                first_start = false;
                row++;
                anzRow++;
                FuncGeneral.Resize2D(ref data, row + 1, anzCol);
            }
            for (int i = 0; i < reader.FieldCount; i++)
            {
                data[row,i]=  reader.GetValue(i);
            }
            row++;
            anzRow++;
            FuncGeneral.Resize2D(ref data, row+1, anzCol);

        }
        reader.Close();

        Microsoft.Office.Interop.Excel.Range range1 = this.XlWorkSheet.Cells[1, 1];
        Microsoft.Office.Interop.Excel.Range range2 = this.XlWorkSheet.Cells[anzRow, anzCol];

        Microsoft.Office.Interop.Excel.Range range = this.XlWorkSheet.get_Range(range1, range2);

        range.Value2 = data;

        sqlHelper = null;
    }

    private bool PruefenNurZahlen(string Wert)
    {
        bool retval = false;
        string wert = "";
        foreach (char zeichen in Wert)
        {
            int ascii=Convert.ToInt16(zeichen);
            if ((ascii > 42 && ascii < 47) || (ascii > 47 && ascii < 58)||(ascii ==69))
            {
                wert += zeichen;
            }
        }
        if (wert == Wert)
        {
            retval = true;
        }
        else
        {
            retval = false;
        }
        return retval;
    }
}
