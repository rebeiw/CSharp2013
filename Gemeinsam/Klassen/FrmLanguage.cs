using System;

namespace Helper
{
    public partial class FrmLanguage : Helper.FrmVorlage
    {

        private ClsSingeltonFormularManager m_formularManager;//!<Instanz auf die Klasse Formularmanager
        private ClsSingeltonLanguage m_language;//!<Instanz auf die Klasse Spracheumschaltung
        private ClsSingeltonParameter m_parameter;

        /** 
         * \brief Konstruktor
         * 
         * Funktionsbeschreibung: Erzeugen der Komponenten, Instanz fuer den Formularmanger anlegen und Formular anmelden. 
         */
        public FrmLanguage()
        {
            this.InitializeComponent();
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
            this.m_language = ClsSingeltonLanguage.CreateInstance();
            this.m_language.AddAllComponents(this);
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
                this.m_parameter.Language = "De";
            if (btn.Name == "BtnEn")
                this.m_parameter.Language = "En";
            if (btn.Name == "BtnFr")
                this.m_parameter.Language = "Fr";
            if (btn.Name == "BtnSp")
                this.m_parameter.Language = "Sp";
            if (btn.Name == "BtnRu")
                this.m_parameter.Language = "Ru";
            this.m_language.SetLanguage();
            this.m_formularManager.SetLanguage();
        }

        private void FrmLanguage_Load(object sender, EventArgs e)
        {

        }
    }
}
