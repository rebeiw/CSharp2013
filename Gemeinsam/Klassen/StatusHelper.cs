using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper
{
    public class StatusHelper
    {
        private int m_MaxWert;
        public int MaxWert
        {
            get { return m_MaxWert; }
            set { m_MaxWert = value; this.m_ToolStripProgressBar.Maximum = this.m_MaxWert; }
        }
        private int m_Wert;
        public int Wert
        {
            get { return m_Wert; }
            set { m_Wert = value; this.m_ToolStripProgressBar.Value = this.m_Wert; }
        }
        private string m_Text;
        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; this.m_ToolStripTextBox.Text = this.m_Text; Application.DoEvents(); }
        }


        private Form m_Formular;
        private StatusStrip m_Statusbar;
        private ToolStripProgressBar m_ToolStripProgressBar;
        private ToolStripTextBox m_ToolStripTextBox;
        public StatusHelper(Form Formular)
        {
            this.m_Formular = Formular;
            this.m_Statusbar=new StatusStrip();
            this.m_ToolStripProgressBar = new ToolStripProgressBar("ToolStripProgressBar1");
            this.m_ToolStripProgressBar.BackColor = m_Formular.BackColor;
            this.m_ToolStripProgressBar.Value = 0;
            this.m_ToolStripProgressBar.AutoSize = false;
            this.m_ToolStripProgressBar.Width = 300;

            this.m_Statusbar.Items.Add(m_ToolStripProgressBar);

            this.m_ToolStripTextBox = new ToolStripTextBox("ToolStripTextBox1");
            this.m_ToolStripTextBox.AutoSize = false;
            this.m_ToolStripTextBox.BackColor = m_Formular.BackColor;
            this.m_ToolStripTextBox.Font= new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.m_ToolStripTextBox.Width = m_Formular.Width - 50 - this.m_ToolStripProgressBar.Width;

            this.m_Statusbar.Items.Add(m_ToolStripTextBox);
            this.m_Statusbar.Stretch = false;

            this.m_Formular.Controls.Add(this.m_Statusbar);

        }
    }

}
