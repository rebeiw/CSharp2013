using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Configuration;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Windows.Input;

namespace Helper
{

    public enum LEDState
    {
        LEDOff=0,
        LEDOn
    }

    public class LedRectangle : PictureBox
    {
        public LedRectangle()
        {
            this.Height = 79;
            this.Width = 39;
            this.State = LEDState.LEDOff;
        }
        [Category("Default"), Description("")]
        private LEDState m_State;
        public LEDState State
        {
            set { if (m_State != value) { m_State = value; this.Invalidate(); } }
            get { return this.m_State; }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 79;
            this.Height = 39;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle recLedGradient = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle recLed = new Rectangle(0+2, 0+2, this.Width-4, this.Height-4);
            Brush brushGradient = new LinearGradientBrush(recLedGradient, Color.DarkGray, Color.White, 45);

            pe.Graphics.FillRectangle(brushGradient, recLedGradient);
            if (this.m_State == LEDState.LEDOff)
            {
                pe.Graphics.FillRectangle(Brushes.DarkGreen, recLed);
            }
            if (this.m_State == LEDState.LEDOn)
            {
                pe.Graphics.FillRectangle(Brushes.Yellow, recLed);
            }
            pe.Graphics.DrawArc(new Pen(Brushes.White, 2), 10, 10, 20, 20, 180, 90);

 
        }
    }
}
