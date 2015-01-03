using System.Windows.Forms;

namespace Helper
{
    public partial class FrmVorlage : Form
    {
        private ClsSingeltonFormularManager m_formularManager;

        public FrmVorlage()
        {
            InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance();
        }

        public void ShowMenu()
        {
            this.m_formularManager.SetFormPrintScreen(this);
            this.m_formularManager.FormularShow("FrmMenu");
        }

        public virtual void SetUserRight()
        {
        }

        public virtual void SetLanguage()
        {
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void FrmVorlage_Load(object sender, System.EventArgs e)
        {

        }
    }
}
