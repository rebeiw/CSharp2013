using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.IO;

namespace Helper
{
    public partial class FrmKeyBoard : Helper.FrmVorlageMenu
    {


        ClsFormularManager frm_mng;

        public FrmKeyBoard()
        {
            InitializeComponent();
            frm_mng = ClsFormularManager.CreateInstance();
            frm_mng.FormularAdd(this, this.Name.ToString());
        }

        private void FrmKeyBoard_Load(object sender, EventArgs e)
        {
        }

        private void bitButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
