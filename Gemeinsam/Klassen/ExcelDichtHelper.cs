using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

public class ExcelDichtHelper
{
    public enum KaliTyp
    {
        Kurz=6,
        Lang=9
    }

    public struct Kopfdaten
    {
        public string Link;
        public string Datum;
        public string MSCode;
        public string MSCodeConverter;
    }

    public struct Kennline
    {
        public int MessNr;
        public double RCTemp;
        public double RefTemp;
        public double RefDichte;
        public double RCFreq;
    }

    public struct Kontrolle
    {
        public int MessNr;
        public double RefTemp;
        public double RefDichte;
        public double RCDichte;
    }

    public struct Justierkonstanten
    {
        public double FL20;
        public double KD;
        public double FTC1;
        public double FTCK;
    }

    public double FL20      = 0.0;
    public double KD        = 0.0;
    public double FTC1      = 0.0;
    public double FTCK      = 0.0;

    private string m_ExcelDatei = "";
    private int m_ZeileKennlinieHFE = 0;
    private int m_ZeileKennlinieWasser = 0;
    private int m_ZeileKennlinieUndecan = 0;

    private const int m_SpalteKennlinieRCTemp = 2;
    private const int m_SpalteKennlinieRefTemp = 3;
    private const int m_SpalteKennlinieRefDichte = 5;
    private const int m_SpalteKennlinieRCFreq = 6;

    private int m_ZeileKontrolleHFE = 0;
    private int m_ZeileKontrolleWasser = 0;
    private int m_ZeileKontrolleUndecan = 0;

    private const int m_SpalteKontrolleRefTemp = 1;
    private const int m_SpalteKontrolleRefDichte = 2;
    private const int m_SpalteKontrolleRCDichte = 3;

    private int m_Spalte_Justierkonstanten = 0;
    private int m_Zeile_Justierkonstanten = 0;

    private int m_MessnrHFE=0;

    private int m_Zeile_Kopfdaten = 0;
    private int m_Spalte_Kopfdaten = 0;
    
    private Microsoft.Office.Interop.Excel.Application XlApp;
    private Microsoft.Office.Interop.Excel.Workbook XlWorkBook;
    private Microsoft.Office.Interop.Excel.Worksheet XlWorkSheet;

    public ExcelDichtHelper(ExcelDichtHelper.KaliTyp KaliTyp)
    {
        string pfad = @"C:\Users\YEF05527\Documents\Repos\CSharp\Projekte\DichteAuswertung\Abweichungen\";
        switch (KaliTyp)
        {
            case KaliTyp.Lang:
                this.m_ExcelDatei = pfad + "DK_Lang.xlsm";
                m_ZeileKennlinieHFE = 37;
                m_ZeileKennlinieWasser = 30;
                m_ZeileKennlinieUndecan = 29;
                m_ZeileKontrolleHFE = 18;
                m_ZeileKontrolleWasser = 17;
                m_ZeileKontrolleUndecan = 16;
                m_MessnrHFE = 8;
                m_Zeile_Justierkonstanten = 45;
                m_Spalte_Justierkonstanten = 11;
                m_Zeile_Kopfdaten = 3;
                m_Spalte_Kopfdaten = 2;

                break;
            case KaliTyp.Kurz:
                this.m_ExcelDatei = pfad + "DK_Kurz.xlsm";
                m_ZeileKennlinieHFE = 34;
                m_ZeileKennlinieWasser = 30;
                m_ZeileKennlinieUndecan = 29;
                m_ZeileKontrolleHFE = 18;
                m_ZeileKontrolleWasser = 17;
                m_ZeileKontrolleUndecan = 16;
                m_MessnrHFE = 5;
                m_Zeile_Justierkonstanten = 42;
                m_Spalte_Justierkonstanten = 11;
                m_Zeile_Kopfdaten = 3;
                m_Spalte_Kopfdaten = 2;

                break;
        }
        this.DateiOeffnen();
    }
    ~ExcelDichtHelper()
    {
    }

    public void DateiOeffnen()
    {
        this.XlApp = new Microsoft.Office.Interop.Excel.Application();
        this.XlApp.Visible = true;
        this.XlWorkBook = XlApp.Workbooks.Open(this.m_ExcelDatei);
        this.XlWorkSheet = this.XlWorkBook.Worksheets.get_Item("Übersicht");
    }

    public void ExcelDichtHelperClose()
    {
   
        this.XlWorkBook.Close(false, Type.Missing, Type.Missing);
        this.XlApp.Application.Quit();
        this.XlApp.Quit();

        this.XlWorkSheet = null;
        this.XlWorkBook = null;
        this.XlApp = null;
    }

    public void MakroStarten()
    {
        this.XlApp.Application.Run("Fl20_KD_Bestimmung");
        this.XlApp.Application.Run("Fl20_KD_Bestimmung");
    }

    public void Ausfuellen_Justierkonstanten(Justierkonstanten Justierkonstanten)
    {
        XlWorkSheet.Cells[m_Zeile_Justierkonstanten + 0, m_Spalte_Justierkonstanten].Value=Justierkonstanten.FL20;
        XlWorkSheet.Cells[m_Zeile_Justierkonstanten + 1, m_Spalte_Justierkonstanten].Value=Justierkonstanten.KD;
        XlWorkSheet.Cells[m_Zeile_Justierkonstanten + 2, m_Spalte_Justierkonstanten].Value=Justierkonstanten.FTC1;
        XlWorkSheet.Cells[m_Zeile_Justierkonstanten + 3, m_Spalte_Justierkonstanten].Value=Justierkonstanten.FTCK;
    }

    public void Ausfuellen_Kopfdaten(Kopfdaten Kopfdaten)
    {
        XlWorkSheet.Cells[m_Zeile_Kopfdaten + 0, m_Spalte_Kopfdaten].Value = Kopfdaten.Link;
        XlWorkSheet.Cells[m_Zeile_Kopfdaten + 1, m_Spalte_Kopfdaten].Value = Kopfdaten.Datum;
        XlWorkSheet.Cells[m_Zeile_Kopfdaten + 6, m_Spalte_Kopfdaten].Value = Kopfdaten.MSCode;
        XlWorkSheet.Cells[m_Zeile_Kopfdaten + 8, m_Spalte_Kopfdaten].Value = Kopfdaten.MSCodeConverter;
    }
    
    public Justierkonstanten Hole_Justierkonstanten()
    {
        Justierkonstanten retval;

        retval.FL20 = Convert.ToDouble(XlWorkSheet.Cells[m_Zeile_Justierkonstanten + 0, m_Spalte_Justierkonstanten].Value);
        retval.KD = Convert.ToDouble(XlWorkSheet.Cells[m_Zeile_Justierkonstanten + 1, m_Spalte_Justierkonstanten].Value);
        retval.FTC1 = Convert.ToDouble(XlWorkSheet.Cells[m_Zeile_Justierkonstanten + 2, m_Spalte_Justierkonstanten].Value);
        retval.FTCK = Convert.ToDouble(XlWorkSheet.Cells[m_Zeile_Justierkonstanten + 3, m_Spalte_Justierkonstanten].Value);
        return retval;
    }

    public void Ausfuellen_Kontrolle(List<ExcelDichtHelper.Kontrolle> WerteKontrolle)
    {
        foreach (ExcelDichtHelper.Kontrolle Messwert in WerteKontrolle)
        {
            int zeile = 0;
            if (Messwert.MessNr == 2)
            {
                zeile =m_ZeileKontrolleWasser;
            }
            if (Messwert.MessNr == 3)
            {
                zeile = m_ZeileKontrolleHFE;
            }
            if (Messwert.MessNr == 1)
            {
                zeile = m_ZeileKontrolleUndecan;
            }
            this.XlWorkSheet.Cells[zeile, m_SpalteKontrolleRefTemp] = Messwert.RefTemp;
            this.XlWorkSheet.Cells[zeile, m_SpalteKontrolleRefDichte] = Messwert.RefDichte;
            this.XlWorkSheet.Cells[zeile, m_SpalteKontrolleRCDichte] = Messwert.RCDichte;
        }

    }

    public bool HoleBit20GradWasser()
    {
        bool retval = true;
        bool[] statusSoll = {true,false,true,true,true,true,true,true,true};
        bool[] status = new bool[9];
        for (int i = 0; i < status.Count(); i++)
        {
            status[i] = Convert.ToBoolean(this.XlWorkSheet.Cells[m_ZeileKennlinieUndecan+i, 17].Value);
            if (status[i] != statusSoll[i])
            {
                retval=false;
                break;
            }
        }
        return retval;
    }



    public void Ausfuellen_Kennlinie(List<ExcelDichtHelper.Kennline> WerteKennline)
    {
        foreach (ExcelDichtHelper.Kennline Messwert in WerteKennline)
        {
            int zeile = 0;
            if (Messwert.MessNr < m_MessnrHFE)
            {
                zeile = Messwert.MessNr - 1 + m_ZeileKennlinieWasser;
            }
            if (Messwert.MessNr == m_MessnrHFE)
            {
                zeile = m_ZeileKennlinieHFE;
            }
            if (Messwert.MessNr > m_MessnrHFE)
            {
                zeile = m_ZeileKennlinieUndecan;
            }
            this.XlWorkSheet.Cells[zeile, m_SpalteKennlinieRCFreq] = Messwert.RCFreq;
            this.XlWorkSheet.Cells[zeile, m_SpalteKennlinieRCTemp] = Messwert.RCTemp;
            this.XlWorkSheet.Cells[zeile, m_SpalteKennlinieRefDichte] = Messwert.RefDichte;
            this.XlWorkSheet.Cells[zeile, m_SpalteKennlinieRefTemp] = Messwert.RefTemp;
        }

    }
    public void Setze_KalibrierType(int AnzahlMessung)
    {
    }

}

