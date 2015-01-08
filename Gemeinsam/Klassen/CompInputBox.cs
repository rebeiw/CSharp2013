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
    public class CompInputBox : TextBox
    {
        public CompInputBox()
        {
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.TextAlign = HorizontalAlignment.Right;
            Font fnt = new Font("Arial", 18f, System.Drawing.FontStyle.Bold);
            this.Width = 89;
            this.Font = fnt;
        }

        [Category("Default"), Description("")]
        private string m_format;
        public string Format
        {
            set { this.m_format = value; }
            get { return this.m_format; }
        }


        private string m_Symbol;
        public string Symbol
        {
            set { m_Symbol = value; }
            get { return this.m_Symbol; }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 89;
        }
    }
}