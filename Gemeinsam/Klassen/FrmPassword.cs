using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmPassword : FrmVorlage
    {
        ClsSingeltonFormularManager m_formularManager;
        ClsSingeltonLanguage m_language;
        ClsSingeltonParameter m_parameter;
        List<string> m_users;

        public FrmPassword()
        {
            InitializeComponent();
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
            this.m_language = ClsSingeltonLanguage.CreateInstance();
            this.m_language.AddAllComponents(this);
            this.m_users = new List<string>();
            this.m_users.Clear();
            this.m_users.Add("Ausgeloggt");
            this.m_users.Add("Operator");
            this.m_users.Add("Einrichter");
            this.m_users.Add("Service");
        }

        private void FrmPassword_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.m_parameter.PasswordOk = false;
            this.Close();
        }
 
        private void TbxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.BtnOk_Click(null, null);
            }
        }

        public override void SetLanguage()
        {
            this.CbxUser.Items.Clear();
            foreach(string user in this.m_users)
            {
                this.CbxUser.Items.Add(this.m_language.GetTranslation(user));
            }
            this.CbxUser.SelectedIndex = Convert.ToInt32(this.m_parameter.LastUser);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string password = "";
            password = this.TxtPassword.Text;
            if (password == "DAkkS")
            {
                this.m_parameter.ActualUser = this.CbxUser.SelectedIndex.ToString();
                this.m_formularManager.SetUserRight();
                this.m_parameter.PasswordOk = true;
                this.TxtPassword.Text = "";
                this.Close();
            }
        }

        private void FrmPasswort_Load(object sender, EventArgs e)
        {

        }

        private void CbxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_parameter.LastUser = this.CbxUser.SelectedIndex.ToString();
        }

        private void FrmPassword_Activated(object sender, EventArgs e)
        {
            this.m_parameter.ActualUser = "0";
        }
    }
}
