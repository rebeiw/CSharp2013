using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmVorlage : Form
    {
        private ClsPLC glb_plc;
        private ClsFormularManager frmMng;

        public FrmVorlage()
        {
            InitializeComponent();
            glb_plc = ClsPLC.CreateInstance();
            frmMng = ClsFormularManager.CreateInstance();
        }
        public void ShowMenu()
        {
            frmMng.SetFormPrintScreen(this);
            frmMng.FormularShow("FRM_Menu");
        }
        private void FrmVorlage_Load(object sender, EventArgs e)
        {

        }
        public virtual void SetUserRight()
        {
        }
        public virtual void SetLanguage()
        {
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
