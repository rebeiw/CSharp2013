using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Helper;
namespace PrjKalibSimu
{
    public partial class FrmService : Helper.FrmVorlageMenu
    {
        public struct DatenService
        {
            public string SymbolName;
            public string SymbolTabText;
            public string SymbolKommentar;
            public string SymbolEinheit;
            public int Zeile;
            public int Spalte;
            public int Seite;

        }
        private TabControl m_TabControlDaten;

        private ClsFormularManager m_glb_FormularManager;
        private ClsPLC m_glb_PLC;
        private ClsDataBinding m_glb_DataBinding;
        private Dictionary<string, string> m_Services;
        private Dictionary<string, int> m_TabInfo;

        private List<DatenService>m_Symboldaten;
        private List<GroupBox> m_GroupBoxDaten;

        public FrmService()
        {
            InitializeComponent();
            this.m_glb_FormularManager = ClsFormularManager.CreateInstance();
            this.m_glb_FormularManager.FormularAdd(this, this.Name.ToString());
            this.m_glb_PLC = ClsPLC.CreateInstance();
            this.m_glb_DataBinding = ClsDataBinding.CreateInstance();
            this.m_Services = new Dictionary<string, string>();
            this.m_TabInfo = new Dictionary<string, int>();
            this.m_Symboldaten = new List<DatenService>();
            this.m_GroupBoxDaten = new List<GroupBox>();

            this.LoadPlc();
            this.CreatePageControl();
            this.CreateTabControl();
            this.CreateSymbole();
        }


        private void FrmService_Load(object sender, EventArgs e)
        {

        }
        private void CreatePage(string Text)
        {
            int pageNumber = this.m_TabControlDaten.TabPages.Count;
            string textPage = Text;
            this.m_TabControlDaten.TabPages.Add(textPage);
            this.m_TabControlDaten.TabPages[pageNumber].Name = FuncGeneral.GetControlName(this.m_TabControlDaten.TabPages[pageNumber]);
            this.m_TabControlDaten.TabPages[pageNumber].BackColor = Color.Silver;
            int breite = this.m_TabControlDaten.Width - GlobalVar.Const_ControlSpacing * 3-2;
            int hoehe = this.m_TabControlDaten.Height - this.m_TabControlDaten.ItemSize.Height- GlobalVar.Const_ControlSpacing * 3 - 2;

            GroupBox gb=this.CreateGB(pageNumber);
            gb.Text = Text;
            gb.Left = GlobalVar.Const_ControlSpacing;
            gb.Top = GlobalVar.Const_ControlSpacing;
            gb.Width = breite;
            gb.Height = hoehe;
            this.m_GroupBoxDaten.Add(gb);
        }

        private void CreateSymbole()
        {
            DatenService datenService;
            foreach (KeyValuePair<string, string> service in this.m_Services)
            {
                string symbolName=service.Key;
                string symbolKommentar = service.Value;
                datenService = this.ParseCommentService(symbolName,symbolKommentar);
                m_Symboldaten.Add(datenService);
                int seite = datenService.Seite;
                GroupBox gb = this.m_GroupBoxDaten[seite];


                Label lbl = this.CreateLBL(gb);
                lbl.Left = GlobalVar.Const_ControlSpacing;
                lbl.Top = datenService.Zeile * (GlobalVar.Const_Service_TextHeigt +GlobalVar.Const_Service_LblSpacing) + GlobalVar.Const_ControlSpacing*3;
                lbl.Text = datenService.SymbolKommentar;
                lbl.Width = GlobalVar.Const_Service_TextBreite;
                lbl.Height = GlobalVar.Const_Service_TextHeigt;

                InputBox inp = this.CreateInputBox(gb);
                inp.Top = lbl.Top;
                inp.Left = lbl.Left + lbl.Width + GlobalVar.Const_ControlSpacing + datenService.Spalte * (79 + GlobalVar.Const_Service_LblSpacing);
                inp.Symbol = datenService.SymbolName;
                inp.Format = "0.00";

                inp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputBox1_KeyPress);
                this.m_glb_DataBinding.AddList(this.Name, inp.Name, "Text", datenService.SymbolName);
            }


        }

        private void inputBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputBox obj = (InputBox)sender;
            if (e.KeyChar == 13)
            {
                string symbol = FuncPLC.SymbolName(sender);

                this.m_glb_PLC.Write(symbol, obj.Text);
            }
        }


        private DatenService ParseCommentService(string SymboleName,string Comment)
        {
            DatenService retval;
            string[] commentData = Comment.Split(':');
            string comment = commentData[1];

            retval.SymbolTabText = commentData[0];
            retval.SymbolName = SymboleName;
            retval.Seite = this.m_TabInfo[retval.SymbolTabText];
            retval.SymbolEinheit = FuncGeneral.Parse("\\[EH.*\\]", ref comment);
            int.TryParse(FuncString.GetOnlyNumeric(FuncGeneral.Parse("\\[ZE[0-9]\\]", ref comment)), out retval.Zeile);
            int.TryParse(FuncString.GetOnlyNumeric(FuncGeneral.Parse("\\[SP[0-9]\\]", ref comment)), out retval.Spalte);
            retval.SymbolKommentar = comment;

            return retval;
        }

        private GroupBox CreateGB(int PageNr)
        {
            GroupBox gb = new GroupBox();
            gb.Name = FuncGeneral.GetControlName(gb);
            this.m_TabControlDaten.TabPages[PageNr].Controls.Add(gb);
            return gb;
        }

        private InputBox CreateInputBox(Control c)
        {
            InputBox inp = new InputBox();
            inp.Name = FuncGeneral.GetControlName(inp);
            c.Controls.Add(inp);
            return inp;
        }


        private Label CreateLBL(Control c, System.Drawing.ContentAlignment Aligment = System.Drawing.ContentAlignment.MiddleRight)
        {
            Label lbl = new Label();
            lbl.Name = FuncGeneral.GetControlName(lbl);
            lbl.AutoSize = false;
            lbl.Text = lbl.Name;
            lbl.TextAlign = Aligment;
            c.Controls.Add(lbl);

            return lbl;
        }



        private void CreateTabControl()
        {
            int seite = 0;
            foreach (PLCDatas data in m_glb_PLC.Daten)
            {
                if (data.DatabaseNumber == 59)
                {
                    m_Services.Add("DB59." + data.Symbolname, data.Comment);
                    string[] commentData = data.Comment.Split(':');
                    string tabText = commentData[0];
                    if (!this.m_TabInfo.ContainsKey(tabText))
                    {
                        this.m_TabInfo.Add(tabText, seite);
                        seite++;
                    }
                }
            }
            foreach (KeyValuePair<string, int> tabInfo in this.m_TabInfo)
            {
                string txt=tabInfo.Key;
                this.CreatePage(txt);
            }
        }

        private void CreatePageControl()
        {
            int tabControlWidth = this.GB_Datenausgabe.Width - (2 * GlobalVar.Const_ControlSpacing + 1);     // 2 *  Abstand + 1 pixel
            int tabControlHeight = this.GB_Datenausgabe.Height - 25;            // Top + Abstand
            this.m_TabControlDaten = new TabControl();
            this.m_TabControlDaten.Top = 20;
            this.m_TabControlDaten.Left = GlobalVar.Const_ControlSpacing;
            this.m_TabControlDaten.Width = tabControlWidth;
            this.m_TabControlDaten.Height = tabControlHeight;
            this.m_TabControlDaten.ItemSize = new System.Drawing.Size(0, 39);
            this.m_TabControlDaten.Name = FuncGeneral.GetControlName(this.m_TabControlDaten);
            this.GB_Datenausgabe.Controls.Add(this.m_TabControlDaten);
        }

        private void LoadPlc()
        {
            m_glb_PLC.AddList("DB59.DBD0", "P1_Qmin_1", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE0]");
            m_glb_PLC.AddList("DB59.DBD4", "P1_Qmax_1", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE0]");
            m_glb_PLC.AddList("DB59.DBD8", "P1_Drehzahl_1", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE0]");
            m_glb_PLC.AddList("DB59.DBD12", "P1_Bypass_1", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE0]");
            m_glb_PLC.AddList("DB59.DBD16", "P1_Qmin_2", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE1]");
            m_glb_PLC.AddList("DB59.DBD20", "P1_Qmax_2", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE1]");
            m_glb_PLC.AddList("DB59.DBD24", "P1_Drehzahl_2", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE1]");
            m_glb_PLC.AddList("DB59.DBD28", "P1_Bypass_2", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE1]");
            m_glb_PLC.AddList("DB59.DBD32", "P1_Qmin_3", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE2]");
            m_glb_PLC.AddList("DB59.DBD36", "P1_Qmax_3", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE2]");
            m_glb_PLC.AddList("DB59.DBD40", "P1_Drehzahl_3", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE2]");
            m_glb_PLC.AddList("DB59.DBD44", "P1_Bypass_3", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE2]");
            m_glb_PLC.AddList("DB59.DBD48", "P1_Qmin_4", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE3]");
            m_glb_PLC.AddList("DB59.DBD52", "P1_Qmax_4", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE3]");
            m_glb_PLC.AddList("DB59.DBD56", "P1_Drehzahl_4", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE3]");
            m_glb_PLC.AddList("DB59.DBD60", "P1_Bypass_4", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE3]");
            m_glb_PLC.AddList("DB59.DBD64", "P1_Qmin_5", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE4]");
            m_glb_PLC.AddList("DB59.DBD68", "P1_Qmax_5", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE4]");
            m_glb_PLC.AddList("DB59.DBD72", "P1_Drehzahl_5", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE4]");
            m_glb_PLC.AddList("DB59.DBD76", "P1_Bypass_5", "REAL", "Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE4]");
            m_glb_PLC.AddList("DB59.DBD80", "P1_Drehzahl_Man_1", "REAL", "Pumpe 1 Drehzahlen:Drehzahl 1[ZE0][EH%]");
            m_glb_PLC.AddList("DB59.DBD84", "P1_Drehzahl_Man_2", "REAL", "Pumpe 1 Drehzahlen:Drehzahl 2[ZE0][EH%]");
            m_glb_PLC.AddList("DB59.DBD88", "P1_Drehzahl_Man_3", "REAL", "Pumpe 1 Drehzahlen:Drehzahl 3[ZE0][EH%]");
            m_glb_PLC.AddList("DB59.DBD92", "P1_Drehzahl_Man_4", "REAL", "Pumpe 1 Drehzahlen:Drehzahl 4[ZE0][EH%]");
            m_glb_PLC.AddList("DB59.DBD96", "P1_Drehzahl_Man_5", "REAL", "Pumpe 1 Drehzahlen:Drehzahl 5[ZE0][EH%]");
            m_glb_PLC.AddList("DB59.DBD100", "P1_Drehzahl_Man_6", "REAL", "Pumpe 1 Drehzahlen:Drehzahl 6[ZE0][EH%]");
            m_glb_PLC.AddList("DB59.DBD104", "P1_Druck_Man_1", "REAL", "Pumpe 1 Druecke:Druck 1[ZE0][EHbar]");
            m_glb_PLC.AddList("DB59.DBD108", "P1_Druck_Man_2", "REAL", "Pumpe 1 Druecke:Druck 2[ZE0][EHbar]");
            m_glb_PLC.AddList("DB59.DBD112", "P1_Druck_Man_3", "REAL", "Pumpe 1 Druecke:Druck 3[ZE0][EHbar]");

        }


    }
}
