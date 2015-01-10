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
    public partial class FrmRelease : FrmParameter
    {
        private ClsSingeltonFormularManager m_formularManager;
        public FrmRelease()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 600;

            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name);
            this.CreateFormular(FormularType.Release);

        }

        private void FrmRelease_Load(object sender, EventArgs e)
        {

        }
    }
}
