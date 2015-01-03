
namespace Helper
{
    public partial class FrmKeyBoard : Helper.FrmVorlageMenu
    {
        private ClsSingeltonFormularManager m_formularManager;//!<Objekt auf die Klasse FormularManager
        private ClsSingeltonLanguage m_language;

        /** 
         * \brief Konstruktor
         * 
         * Funktionsbeschreibung: Komponenten erzeugen und an den Formularmanager anmelden  
         */

        public FrmKeyBoard()
        {
            InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
            this.m_language = ClsSingeltonLanguage.CreateInstance();
            this.m_language.AddAllComponents(this);
        }

        private void FrmKeyBoard_Load(object sender, System.EventArgs e)
        {

        }
    }
}
