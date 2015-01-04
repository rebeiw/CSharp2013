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
    public class CompTxtBox : TextBox
    {
        public enum CompTxtBoxSize { Normal, Small };
        public enum CompTxtBoxType { ProcessVariable, Setpoint };


        private Font m_FontNormal;
        private Font m_FontSmall;
        private StringFormat m_StringFormat;

        private int m_WidthNormal;
        private int m_WidthSmall;
        private SolidBrush m_DrawBrush;

        public CompTxtBox()
        {
            this.SetStyle(
                            System.Windows.Forms.ControlStyles.UserPaint |
                            System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                            System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer
                            , true
                         );


            this.BackColor = Color.Black;
            this.ForeColor = Color.Yellow;
            this.Enabled = false;

            this.m_StringFormat = new StringFormat();
            this.m_StringFormat.Alignment = StringAlignment.Far;
            this.m_StringFormat.LineAlignment = StringAlignment.Far;
            this.m_DrawBrush = new SolidBrush(this.ForeColor);
            this.m_FontNormal = new Font("Arial", 18f, System.Drawing.FontStyle.Bold);
            this.m_FontSmall = new Font("Arial", 14f, System.Drawing.FontStyle.Bold);
            this.Error = false;
            this.m_WidthSmall = 59;
            this.m_WidthNormal = 89;
            this.TextboxSize = CompTxtBoxSize.Normal;
            this.TextSymbol = "";
            this.SetPropertie();

        }
        [Category("Default"), Description("")]
        private string m_Format;
        public string Format
        {
            set { if (m_Format != value) { m_Format = value; this.Invalidate(); } }
            get { return this.m_Format; }
        }

        [Category("Default"), Description("")]
        private string m_TextSymbol;
        public string TextSymbol
        {
            set { if (m_TextSymbol != value) { m_TextSymbol = value; this.Invalidate(); } }
            get { return this.m_TextSymbol; }
        }

        [Category("Default"), Description("")]
        private CompTxtBoxType m_TextBoxType;
        public CompTxtBoxType TextBoxType
        {
            set { if (m_TextBoxType != value) { m_TextBoxType = value; this.SetPropertie(); this.Invalidate(); } }
            get { return this.m_TextBoxType; }
        }
        [Category("Default"), Description("")]
        private CompTxtBoxSize m_TextboxSize;
        public CompTxtBoxSize TextboxSize
        {
            set { if (m_TextboxSize != value) { m_TextboxSize = value; this.SetPropertie(); this.Invalidate(); } }
            get { return this.m_TextboxSize; }
        }
        [Category("Default"), Description("")]
        private bool m_Error;
        public bool Error
        {
            set { if (m_Error != value) { m_Error = value; this.Invalidate(); } }
            get { return this.m_Error; }
        }

        private void SetPropertie()
        {
            if (this.m_TextboxSize == CompTxtBoxSize.Normal)
            {
                this.Font = this.m_FontNormal;
                this.Width = this.m_WidthNormal;
            }
            if (this.m_TextboxSize == CompTxtBoxSize.Small)
            {
                this.Font = this.m_FontSmall;
                this.Width = this.m_WidthSmall;
            }
            if (this.m_TextBoxType == CompTxtBoxType.ProcessVariable)
            {
                this.ForeColor = Color.Yellow;
            }
            if (this.m_TextBoxType == CompTxtBoxType.Setpoint)
            {
                this.ForeColor = Color.White;
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.SetPropertie();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (this.m_Error)
            {
                m_DrawBrush.Color = Color.Red;
                pe.Graphics.DrawString("Error", Font, m_DrawBrush, this.ClientRectangle);
            }
            else
            {
                double wert = 0.0;
                string format = "{0:" + this.m_Format + "}";
                Double.TryParse(this.Text, out wert);
                this.Text = String.Format(format, wert);
                this.m_DrawBrush.Color = this.ForeColor;
                pe.Graphics.DrawString(this.Text, this.Font, this.m_DrawBrush, this.ClientRectangle, this.m_StringFormat);
            }
        }
    }
}