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
    public class InputBox : TextBox
    {
        public InputBox()
        {
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.TextAlign = HorizontalAlignment.Right;
            Font fnt = new Font("Arial", 18f, System.Drawing.FontStyle.Bold);
            this.Width = 79;
            this.Font = fnt;
        }

        [Category("Default"), Description("")]
        private string m_format;
        public string Format
        {
            set { if (m_format != value) { m_format = value; this.Invalidate(); } }
            get { return this.m_format; }
        }


        private string m_Symbol;
        public string Symbol
        {
            set { m_Symbol = value; }
            get { return this.m_Symbol; }
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 79;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                double wert = 0.0;
                string format = "{0:" + this.m_format + "}";
                bool testen = Double.TryParse(this.Text, out wert);
                if (testen)
                {
                    this.Text = String.Format(format, wert);
                }
                else
                {
                    this.Text = String.Format(format, wert);
                }
                this.BackColor = Color.White;
            }
            base.OnKeyPress(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {

            this.BackColor = SystemColors.Highlight;
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.BackColor = Color.White;
            base.OnLostFocus(e);
        }
    }
}