
namespace Helper
{
    public partial class FrmKeyBoard : Helper.FrmVorlageMenu
    {
        ClsSingeltonFormularManager m_formularManager;//!<Objekt auf die Klasse FormularManager

        /** 
         * \brief Konstruktor
         * 
         * Funktionsbeschreibung: Komponenten erzeugen und an den Formularmanager anmelden  
         */

        public FrmKeyBoard()
        {
            InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
        }
    }
}
