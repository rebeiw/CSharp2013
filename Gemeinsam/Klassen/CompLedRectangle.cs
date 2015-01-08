using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Helper
{


    public class CompLedRectangle : PictureBox
    {
        public enum CompLedState { LedOff = 0, LedOn }
        private Rectangle m_RecLedGradient;
        private Rectangle m_RecLed;
        private Brush m_BrushGradient;
        private int m_Width;
        private int m_Height;
        private int m_FrameThickness;
        private Pen m_Pen;
        public CompLedRectangle()
        {
            this.m_Height = 35;
            this.m_Width = 89;
            this.SetPropertie();
            this.State = CompLedState.LedOff;
            this.m_RecLedGradient = new Rectangle(0, 0, this.Width, this.Height);
            int frame_thickness_1 = this.m_FrameThickness;
            int frame_thickness_2 = this.m_FrameThickness * 2;

            this.m_RecLed = new Rectangle(frame_thickness_1, frame_thickness_1, this.Width - frame_thickness_2, this.Height - frame_thickness_2);
            this.m_BrushGradient = new LinearGradientBrush(this.m_RecLedGradient, Color.DarkGray, Color.White, 45);
            this.m_Pen=new Pen(Brushes.White, 2);
        }
        [Category("Default"), Description("")]
        private CompLedState m_State;
        public CompLedState State
        {
            set { if (m_State != value) { m_State = value; this.Invalidate(); } }
            get { return this.m_State; }
        }

        private void SetPropertie()
        {
            this.Height = this.m_Height;
            this.Width = this.m_Width;
            this.m_FrameThickness = 2;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.SetPropertie();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pe.Graphics.FillRectangle(this.m_BrushGradient, this.m_RecLedGradient);
            if (this.m_State == CompLedState.LedOff)
            {
                pe.Graphics.FillRectangle(Brushes.DarkGreen, this.m_RecLed);
            }
            if (this.m_State == CompLedState.LedOn)
            {
                pe.Graphics.FillRectangle(Brushes.Yellow, this.m_RecLed);
            }
            int x = (int)(((double)this.Width / 8.0) + 0.5);
            int y = (int)(((double)this.Height / 4.0) + 0.5);

            int width = x * 2;
            int height = y * 2;

            pe.Graphics.DrawArc(this.m_Pen, x, y, width, height, 180, 90);

 
        }


    }
}
