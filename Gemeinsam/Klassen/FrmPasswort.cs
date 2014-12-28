using System;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmPasswort : FrmVorlage
    {
        ClsSingeltonFormularManager frm_mng;

        public FrmPasswort()
        {
            InitializeComponent();
            frm_mng = ClsSingeltonFormularManager.CreateInstance();
            frm_mng.FormularAdd(this, this.Name.ToString());
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

        private void Frm_Passwort_Load(object sender, EventArgs e)
        {

        }
    }
}
