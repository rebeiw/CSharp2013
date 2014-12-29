using System;

namespace Helper
{
    public partial class FrmLanguage : Helper.FrmVorlage
    {

        ClsSingeltonFormularManager m_formularManager;//!<Instanz auf die Klasse Formularmanager

        /** 
         * \brief Konstruktor
         * 
         * Funktionsbeschreibung: Erzeugen der Komponenten, Instanz fuer den Formularmanger anlegen und Formular anmelden. 
         */
        public FrmLanguage()
        {
            this.InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
        }

        /** 
         * \brief Formular schliessen
         */
        private void BtnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /** 
         * \brief Sprache umschalten
         */
        private void BtnLanguage_Click(object sender, EventArgs e)
        {
            this.Close();
            CompBitButton btn = (CompBitButton)sender;
            if (btn.Name == "BtnDe")
                GlobalVar.Glb_Language = GlobalVar.Language.De;
            if (btn.Name == "BtnEn")
                GlobalVar.Glb_Language = GlobalVar.Language.En;
            if (btn.Name == "BtnFr")
                GlobalVar.Glb_Language = GlobalVar.Language.Fr;
            if (btn.Name == "BtnSp")
                GlobalVar.Glb_Language = GlobalVar.Language.Sp;
            if (btn.Name == "BtnRu")
                GlobalVar.Glb_Language = GlobalVar.Language.Ru;
            this.m_formularManager.SetLanguage();
        }
    }
}
