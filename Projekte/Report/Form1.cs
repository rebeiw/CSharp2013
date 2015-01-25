using Helper;
using System;
using System.Windows.Forms;


namespace Report
{
    public partial class Form1 : FrmVorlage
    {

        ClsPDF m_ClsPdf;
        public Form1()
        {
            InitializeComponent();
            this.m_ClsPdf = new ClsPDF();
            this.m_ClsPdf.AddPage();

            this.m_ClsPdf.PrintHead("");

            this.m_ClsPdf.ShowDocument();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Dispose();
        }

    }
}
