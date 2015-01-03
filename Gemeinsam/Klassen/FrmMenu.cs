using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Helper
{
    /** 
    * \brief Formular Menu
    * 
    * Funktionsbeschreibung: Erzeugen der Buttonliste und am Formularmanager anmelden 
    * <img src="../Doku/FrmMenu.png"> 
    */
    public partial class FrmMenu : FrmVorlage
    {
        ClsSingeltonFormularManager m_formularManager;//!<Objekt auf die erzeugte Instanz
        ClsSingeltonLanguage m_language;
        List<CompBitButton> m_buttonList;//!<Liste fuer die Buttons im Menu
        /** 
        * \brief Konstruktor
        * 
        * Funktionsbeschreibung: Erzeugen der Buttonliste und am Formularmanager anmelden 
        */
        public FrmMenu()
        {
            this.InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
            this.m_language = ClsSingeltonLanguage.CreateInstance();
            this.m_language.AddAllComponents(this);
            this.m_buttonList = new List<CompBitButton>();
            this.ClrButtons();
        }

        /** 
        * \brief Button User setzen
        */
        private void FrmMenu_Activated(object sender, EventArgs e)
        {
            this.BtnUser.PictureNumber = Convert.ToInt32(GlobalVar.Glb_Passwort_ok);
        }

        /** 
        * \brief Formular bei Deaktivierung schliessen
        */
        private void FrmMenu_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        /** 
        * \brief Default Buttons fuer das Menu erzeugen
        */
        public void ClrButtons()
        {
            this.m_buttonList.Clear();
            this.m_buttonList.Add(this.BtnEnd);
            this.m_buttonList.Add(this.BtnUser);
            this.m_buttonList.Add(this.BtnLanguage);
            this.m_buttonList.Add(this.BtnPrintScreen);
            this.m_buttonList.Add(this.BtnKeyBoard);
            this.m_buttonList.Add(this.BtnQuit);
            this.SetButtons();
        }

        /** 
        * \brief Alle Buttons in der Liste im Menu erzeugen
        */
        private void SetButtons()
        {
            double anzSpalten = 0.0;
            anzSpalten = (double)((double)this.m_buttonList.Count-1.0) / 6.0;

            int dRound = (int)anzSpalten+1;
            int breite = (int)dRound * 89 + 10;
            this.GbxMenu.Width = breite;
            this.GbxMenu.Left=5;
            this.Width = breite + 26;
            int btnId=0;
            foreach (CompBitButton bitBtn in m_buttonList)
            {
                double sp = anzSpalten - (int)((double)btnId / 6.0);
                int ze = btnId - (int)((double)btnId / 6.0) * 6;
                int possp = (int)sp * 89 + 10;
                int posze = ze * 54 + 15;
                bitBtn.Left = possp;
                bitBtn.Top = posze;
                btnId++;
            }
        }

        public override void SetLanguage()
        {
            base.SetLanguage();
            if (GlobalVar.Glb_Language == GlobalVar.Language.De)
                this.BtnLanguage.Picture_0 = CompBitButtonStyle.btg_De;
            if (GlobalVar.Glb_Language == GlobalVar.Language.En)
                this.BtnLanguage.Picture_0 = CompBitButtonStyle.btg_En;
            if (GlobalVar.Glb_Language == GlobalVar.Language.Fr)
                this.BtnLanguage.Picture_0 = CompBitButtonStyle.btg_Fr;
            if (GlobalVar.Glb_Language == GlobalVar.Language.Sp)
                this.BtnLanguage.Picture_0 = CompBitButtonStyle.btg_Sp;
            if (GlobalVar.Glb_Language == GlobalVar.Language.Ru)
                this.BtnLanguage.Picture_0 = CompBitButtonStyle.btg_Ru;
        }

        public void AddBitButtonCloseAll()
        {
            CompBitButton btn = new CompBitButton();
            btn.Picture_0 = CompBitButtonStyle.btg_DeleteForm;
            btn.Click += new System.EventHandler(this.BtnCloseAll_Click);

            this.m_buttonList.Add(btn);
            this.GbxMenu.Controls.Add(btn);
            this.SetButtons();
        }

        public void AddBitButton(CompBitButtonStyle ButtonStyle, string Formularname)
        {
            CompBitButton btn = new CompBitButton();
            btn.Picture_0 = ButtonStyle;
            btn.Formular = Formularname;
            btn.Click += new System.EventHandler(this.BtnShowFormular_Click);

            this.m_buttonList.Add(btn);
            this.GbxMenu.Controls.Add(btn);
            this.SetButtons();
        }


        /** 
        * \brief Hardcopy vonm ausgewaehlten Formular erstellen
        */
        private void BtnPrintScreen_Click(object sender, EventArgs e)
        {
            this.Close();
            this.m_formularManager.m_printScreen.BringToFront();
            Application.DoEvents();
            Graphics graph = null;
            Bitmap bmp = new Bitmap(this.m_formularManager.m_printScreen.Width, this.m_formularManager.m_printScreen.Height);
            graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(this.m_formularManager.m_printScreen.Left, this.m_formularManager.m_printScreen.Top, 0, 0, bmp.Size);
            string path = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = m_formularManager.m_printScreen.Name + ".png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                bmp.Save(path);
            }
            saveFileDialog.Dispose();
            bmp.Dispose();
            graph.Dispose();
        }

        /** 
        * \brief Formular fuer den Button anzeigen
        */
        private void BtnShowFormular_Click(object sender, EventArgs e)
        {
            CompBitButton btn = (CompBitButton)sender;
            this.m_formularManager.FormularShow(btn.Formular);
        }

        /** 
        * \brief Formular Sprachauswahl anzeigen
        */
        private void BtnLanguage_Click(object sender, EventArgs e)
        {
            this.Close();
            m_formularManager.FormularShow("FrmLanguage");
        }

        /** 
        * \brief Formular Tastatur anzeigen
        */
        private void BtnKeyBoard_Click(object sender, EventArgs e)
        {
            this.Close();
            m_formularManager.FormularShow("FrmKeyBoard");
        }
        /** 
        * \brief Formular Passwort anzeigen
        */
        private void BtnUser_Click(object sender, EventArgs e)
        {
            m_formularManager.FormularShow("FrmPasswort");
        }

        /** 
        * \brief Formular beenden
        */
        private void BtnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /** 
        * \brief Alle geoeffneten Formulare schliessen
        */
        private void BtnCloseAll_Click(object sender, EventArgs e)
        {
            this.m_formularManager.FormularCloseAll();
        }
        /** 
        * \brief Application beenden
        */
        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {

        }

    }
}
