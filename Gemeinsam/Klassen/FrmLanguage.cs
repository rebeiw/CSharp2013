using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmLanguage : Helper.FrmVorlage
    {

        ClsFormularManager frm_mng;

        public FrmLanguage()
        {
            InitializeComponent();
            frm_mng = ClsFormularManager.CreateInstance();
            frm_mng.FormularAdd(this, this.Name.ToString());
        }

        private void BTN_Ende_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLanguage_Load(object sender, EventArgs e)
        {

        }

        private void BTN_Sprache_Click(object sender, EventArgs e)
        {
            this.Close();
            RotaBitButton btn = (RotaBitButton)sender;
            if (btn.Name == "BTN_De")
                GlobalVar.Glb_Language = GlobalVar.Language.De;
            if (btn.Name == "BTN_En")
                GlobalVar.Glb_Language = GlobalVar.Language.En;
            if (btn.Name == "BTN_Fr")
                GlobalVar.Glb_Language = GlobalVar.Language.Fr;
            if (btn.Name == "BTN_Sp")
                GlobalVar.Glb_Language = GlobalVar.Language.Sp;
            if (btn.Name == "BTN_Ru")
                GlobalVar.Glb_Language = GlobalVar.Language.Ru;
            frm_mng.SetLanguage();
            
        }
    }
}
