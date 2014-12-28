
namespace Helper
{
    public partial class FrmKeyBoard : Helper.FrmVorlageMenu
    {
        ClsSingeltonFormularManager m_formularManager;

        public FrmKeyBoard()
        {
            InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
        }
    }
}
