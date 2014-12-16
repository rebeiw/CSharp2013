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
    public enum SwitchState
    {
        Off=0,
        On
    }
    [ToolboxItem(true)]
    [ToolboxBitmapAttribute(typeof(ToggleSwitch),"ToggleSwitch.bmp")]
    public class ToggleSwitch : PictureBox
    {
        public ToggleSwitch()
        {
            this.Width = 79;
            this.Height = 39;
        }

        [Category("Default"), Description("")]
        private SwitchState m_State;
        public SwitchState State
        {
            set { if (m_State != value) { m_State = value; this.Invalidate(); } }
            get { return this.m_State; }
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
            this.Width = 79;
            this.Height = 39;
        }
        protected override void OnClick(EventArgs e)
        {
            if (this.State == SwitchState.Off)
            {
                this.State = SwitchState.On;
            }
            else
            {
                this.State = SwitchState.Off;
            }
            base.OnClick(e);
            this.Invalidate();
        }



        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Pen penBlack = new Pen(Brushes.Black, 1);
            Pen penWhite = new Pen(Brushes.White, 1);
            Pen penGray = new Pen(Brushes.DarkGray, 1);
            Pen penLightGray = new Pen(Brushes.LightGray, 1);

            float width = this.Width;
            float height = this.Height;
            
            PointF p0 = new PointF( 0.0F, 0.0F);
            PointF p1 = new PointF( 0.0F,37.0F);
            PointF p2 = new PointF(77.0F, 37.0F);
            PointF p3 = new PointF(77.0F, 2.0F);
            PointF p4 = new PointF(76.0F, 0.0F);


            PointF p5 = new PointF( 1.0F, 1.0F);
            PointF p6 = new PointF( 1.0F,36.0F);
            PointF p7 = new PointF(75.0F, 1.0F);

            PointF p8 = new PointF( 0.0F,38.0F);
            PointF p9 = new PointF(78.0F,38.0F);
            PointF p10 = new PointF(78.0F, 1.0F);

            Rectangle recSwitch = new Rectangle(0, 0, 79, 39);

            pe.Graphics.FillRectangle(Brushes.LightGray, recSwitch);

            pe.Graphics.DrawLine(penGray, p4, p0);
            pe.Graphics.DrawLine(penGray, p0, p1);
            pe.Graphics.DrawLine(penGray, p1, p2);
            pe.Graphics.DrawLine(penGray, p2, p3);

            pe.Graphics.DrawLine(penBlack, p5, p7);
            pe.Graphics.DrawLine(penBlack, p5, p6);

            pe.Graphics.DrawLine(penWhite, p8, p9);
            pe.Graphics.DrawLine(penWhite, p9, p10);

            float off = 0;

            if (this.State == SwitchState.Off)
            {
                off = 0;
            }
            if (this.State == SwitchState.On)
            {
                off = 50;
            }

            p0.X = 3+off; p0.Y = 4;
            p1.X = 3 + off; p1.Y = 33;
            p2.X = 24 + off; p2.Y = 33;
            p3.X = 24 + off; p3.Y = 6;
            p4.X = 23 + off; p4.Y = 4;

            pe.Graphics.DrawLine(penGray, p4, p0);
            pe.Graphics.DrawLine(penGray, p0, p1);
            pe.Graphics.DrawLine(penGray, p1, p2);
            pe.Graphics.DrawLine(penGray, p2, p3);

            p0.X = 4 + off; p0.Y = 5;
            p1.X = 4 + off; p1.Y = 32;
            p2.X = 23 + off; p2.Y = 32;
            p3.X = 23 + off; p3.Y = 6;
            p4.X = 22 + off; p4.Y = 5;

            pe.Graphics.DrawLine(penWhite, p4, p0);
            pe.Graphics.DrawLine(penWhite, p0, p1);
            pe.Graphics.DrawLine(penWhite, p1, p2);
            pe.Graphics.DrawLine(penWhite, p2, p3);

            p0.X = 4 + off; p0.Y = 34;
            p1.X =25 + off; p1.Y = 34;
            p2.X = 25 + off; p2.Y = 5;
            pe.Graphics.DrawLine(penBlack, p0, p1);
            pe.Graphics.DrawLine(penBlack, p1, p2);

            p0.X = 5 + off; p0.Y = 6;
            p1.X = 5 + off; p1.Y = 31;
            p2.X = 22 + off; p2.Y = 31;
            p3.X = 22 + off; p3.Y = 6;

            RectangleF recBtn = new RectangleF(p0.X, p0.Y, p2.X - p1.X, p2.Y - p3.Y);
            pe.Graphics.DrawRectangle(penWhite, p0.X, p0.Y, p2.X - p1.X, p2.Y - p3.Y);
            pe.Graphics.FillRectangle(Brushes.White, recBtn);
            for (int i = 0; i < 6; i++)
            {
                p0.X = 7 + off+i*3; p0.Y = 6;
                p1.X = 7 + off + i * 3; p1.Y = 31;
                pe.Graphics.DrawLine(penBlack, p0, p1);
            }

        }
    }

}