using System;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmProgEnd : Helper.FrmVorlage
    {

        ClsSingeltonFormularManager m_formularManager;

        public FrmProgEnd()
        {
            this.InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance();
            this.m_formularManager.FormularAdd(this, this.Name.ToString());
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
