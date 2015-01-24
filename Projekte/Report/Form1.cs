using Helper;
using System;
using System.Windows.Forms;


namespace Report
{
    public partial class Form1 : Form
    {

        ClsPDF m_ClsPdf;
        public Form1()
        {
            InitializeComponent();
            this.m_ClsPdf = new ClsPDF();
            this.m_ClsPdf.AddPage();

            for (int i = 0; i < 10;i++ )
            {
                this.m_ClsPdf.DrawString("Wieber Juergen " + i.ToString() , 0, i);
            }
            this.m_ClsPdf.AddPage();
            for (int i = 0; i < 10; i++)
            {
                this.m_ClsPdf.DrawString("Wieber Christine " + i.ToString(), 5, i);
            }

            this.m_ClsPdf.SetPage(0);
            for (int i = 0; i < 10; i++)
            {
                this.m_ClsPdf.DrawString("Wieber Christine " + i.ToString(), 5, i+10);
            }
            this.m_ClsPdf.ShowDocument();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
