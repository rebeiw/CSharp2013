using System;

namespace Helper
{
    public partial class FrmVorlageMenu : Helper.FrmVorlage
    {
        private ClsSingeltonFormularManager m_formularManager;

        private bool m_ShowClose;
        public bool ShowClose
        {
            set { if (this.m_ShowClose != value) { this.m_ShowClose = value; } }
            get { return this.m_ShowClose; }
        }


        public FrmVorlageMenu()
        {
            InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance();
        }


        protected void BtnClose_Click(object sender, EventArgs e)
        {
            if (this.ShowClose)
            {
                this.m_formularManager.FormularShow("FrmProgEnd");
            }
            else
            {
                this.Close();
            }
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            this.ShowMenu();
        }

        private void BtnKeyboard_Click(object sender, EventArgs e)
        {
            m_formularManager.FormularShow("FrmKeyBoard");
        }
    }
}
