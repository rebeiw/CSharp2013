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

    public enum LEDType
    {
        Normal = 0,
        Error,
        Release
    }

    public class LedRound : PictureBox
    {
        private Rectangle recLedGradient;
        private Rectangle recLed;
        private Point centerPoint;
        Color[] colors;

        public event EventHandler ValueChanged;

        ~LedRound()
        {
            GC.Collect();
        
        }

        [Category("Default"), Description("")]
        private LEDType m_Type;
        public LEDType Type
        {
            set { if (m_Type != value) { m_Type = value; this.Invalidate(); } }
            get { return this.m_Type; }
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
            this.Width = 25;
            this.Height =25;
        }

        public LedRound()
        {
            this.Width = 25;
            this.Height = 25;
            this.State = LEDState.LEDOff;
            this.Type = LEDType.Normal;

            recLedGradient = new Rectangle();
            recLed = new Rectangle();
            centerPoint = new Point(0, 0);
            colors = new Color[] { Color.White };
            
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.recLedGradient.X = 0;
            this.recLedGradient.Y = 0;
            this.recLedGradient.Width = this.Width;
            this.recLedGradient.Height = this.Height;

            this.recLed.X = 0+1;
            this.recLed.Y = 0+1;
            this.recLed.Width = this.Width-2;
            this.recLed.Height = this.Height-2;



            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(recLedGradient);
            PathGradientBrush pgb = new PathGradientBrush(gp);

            pgb.CenterColor = Color.Gray;
            pgb.CenterPoint = centerPoint;
            pgb.SurroundColors = colors;
            pe.Graphics.FillEllipse(pgb, recLedGradient);

            if (this.m_State == LEDState.LEDOff)
            {
                this.OnValueChanged(EventArgs.Empty);

                if (this.Type == LEDType.Normal)
                {
                    pe.Graphics.FillEllipse(Brushes.DarkGreen, recLed);
                }
                if (this.Type == LEDType.Error)
                {
                    pe.Graphics.FillEllipse(Brushes.Lime, recLed);
                }
                if (this.Type == LEDType.Release)
                {
                    pe.Graphics.FillEllipse(Brushes.Red, recLed);
                }
            }
            if (this.m_State == LEDState.LEDOn)
            {
                this.OnValueChanged(EventArgs.Empty);
                if (this.Type == LEDType.Normal)
                {
                    pe.Graphics.FillEllipse(Brushes.Yellow, recLed);
                }
                if (this.Type == LEDType.Error)
                {
                    pe.Graphics.FillEllipse(Brushes.Red, recLed);
                }
                if (this.Type == LEDType.Release)
                {
                    pe.Graphics.FillEllipse(Brushes.Lime, recLed);
                }
                
            }
            pe.Graphics.DrawArc(new Pen(Brushes.White,2), 6, 6, 13, 13, 180, 90);
            gp = null;
            pgb = null;
            //GC.Collect();
         }

        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler = this.ValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}
