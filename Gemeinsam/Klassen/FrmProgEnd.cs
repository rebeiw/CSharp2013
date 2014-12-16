using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmProgEnd : Helper.FrmVorlage
    {

        ClsFormularManager frm_mng;

        public FrmProgEnd()
        {
            InitializeComponent();
            frm_mng = ClsFormularManager.CreateInstance();
            frm_mng.FormularAdd(this, this.Name.ToString());
        }

        private void FrmProgEnd_Load(object sender, EventArgs e)
        {

        }

        private void BTN_Ok_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BTN_Abbruch_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
