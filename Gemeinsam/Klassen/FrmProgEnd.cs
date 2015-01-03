using System;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmProgEnd : Helper.FrmVorlage
    {

        ClsSingeltonFormularManager m_formularManager;
        ClsSingeltonLanguage m_language;

        public FrmProgEnd()
        {
            this.InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
            this.m_language = ClsSingeltonLanguage.CreateInstance();
            this.m_language.AddAllComponents(this);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProgEnd_Load(object sender, EventArgs e)
        {

        }
    }
}
