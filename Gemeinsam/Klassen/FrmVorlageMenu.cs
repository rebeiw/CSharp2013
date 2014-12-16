using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmVorlageMenu : Helper.FrmVorlage
    {
        private ClsFormularManager frmMng;

        private bool m_ShowClose;
        public bool ShowClose
        {
            set { if (m_ShowClose != value) { m_ShowClose = value; } }
            get { return this.m_ShowClose; }
        }


        public FrmVorlageMenu()
        {
            InitializeComponent();
            frmMng = ClsFormularManager.CreateInstance();
        }


        private void BTN_Close_Click(object sender, EventArgs e)
        {
            if (this.ShowClose)
            {
                frmMng.FormularShow("FrmProgEnd");
            }
            else
            {
                this.Close();
            }
        }

        private void BTN_Menu_Click(object sender, EventArgs e)
        {
            this.ShowMenu();
        }

        private void FrmVorlageMenu_Load(object sender, EventArgs e)
        {

        }

        private void BTN_Keyboard_Click(object sender, EventArgs e)
        {
            frmMng.FormularShow("FrmKeyBoard");
        }
    }
}
