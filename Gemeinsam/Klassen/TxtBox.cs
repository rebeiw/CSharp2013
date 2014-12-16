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
    public class TxtBox : TextBox
    {
        public TxtBox()
        {
            this.SetStyle(
                            System.Windows.Forms.ControlStyles.UserPaint | 
                            System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                            System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer
                            , true
                         );
            this.BackColor = Color.Black;
            this.ForeColor = Color.Yellow;
            Font fnt = new Font("Arial", 18f, System.Drawing.FontStyle.Bold);
            this.Enabled = false;
            this.m_error = false;
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
        [Category("Default"), Description("")]
        private bool m_error;
        public bool Error
        {
            set { if (m_error != value) { m_error = value; this.Invalidate(); } }
            get { return this.m_error; }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 79;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (m_error)
            {
                StringFormat strFormat = new StringFormat();
                SolidBrush drawBrush = new SolidBrush(Color.Red);
                pe.Graphics.DrawString("Error", Font, drawBrush, this.ClientRectangle);
            }
            else
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
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Far;
                strFormat.LineAlignment = StringAlignment.Far;
                SolidBrush drawBrush = new SolidBrush(ForeColor);
                pe.Graphics.DrawString(Text, Font, drawBrush, this.ClientRectangle, strFormat);
            }
        }
    }
}