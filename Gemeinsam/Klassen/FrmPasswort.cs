using System;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmPasswort : FrmVorlage
    {
        ClsSingeltonFormularManager frm_mng;
        ClsSingeltonLanguage m_Language;

        public FrmPasswort()
        {
            InitializeComponent();
            this.frm_mng = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
            this.m_Language = ClsSingeltonLanguage.CreateInstance();
            this.m_Language.AddAllComponents(this);
        }

        private void FRM_Password_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_Abbruch_Click(object sender, EventArgs e)
        {
            GlobalVar.Glb_Passwort_ok = false;
            this.Close();
        }
 
        private void TB_Passwort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.BTN_Ok_Click(null, null);
            }
        }

        private void BTN_Ok_Click(object sender, EventArgs e)
        {
            string password = "";
            password = this.TB_Passwort.Text;
            if (password == "DAkkS")
            {
                frm_mng.SetUserRight();
                GlobalVar.Glb_Passwort_ok = true;
                this.TB_Passwort.Text = "";
                this.Close();
            }
        }

        private void FrmPasswort_Load(object sender, EventArgs e)
        {

        }
    }
}
