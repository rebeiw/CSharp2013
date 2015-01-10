using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;

namespace ClassDevelopment
{
    public partial class FrmO2Curve : FrmVorlageMenu
    {
        private ClsSingeltonFormularManager m_formularManager;
        public FrmO2Curve()
        {
            InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name);
        }

        private void FrmO2Curve_Load(object sender, EventArgs e)
        {
            int off=200;
            for(int i=0;i<11;i++)
            {
                string format = "{0:0.00}";

                this.CreateLabel(200, i * 20 + off, 60, 20, String.Format(format, 10-i)+"-", this.tabPage1);
            }
            this.CreateMultiBar(260, 213, 50, 200, this.tabPage1);

        }

        private void compMultiBar1_Click(object sender, EventArgs e)
        {

        }

        private void compBitButton1_MouseDown(object sender, MouseEventArgs e)
        {
            this.compMultiBar1.Select = true;
        }

        private void compInputBox1_Enter(object sender, EventArgs e)
        {

        }

        private void compMultiBar3_Click(object sender, EventArgs e)
        {

        }
    }
}
