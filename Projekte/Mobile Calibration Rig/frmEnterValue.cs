using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mobile_Calibration_Rig
{
    public partial class frmEnterValue : Form
    {
        public frmEnterValue()
        {
            InitializeComponent();
        }
        

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                frmMain.MFCust = txtValue.Text;
                Close();
            }
        }
    }
}
