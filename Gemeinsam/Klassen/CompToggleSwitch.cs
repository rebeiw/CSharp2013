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
    [ToolboxItem(true)]
    [ToolboxBitmapAttribute(typeof(CompToggleSwitch),"ToggleSwitch.bmp")]
    public class CompToggleSwitch : PictureBox
    {
        public enum CompToggleSwitchState
        {
            Off = 0,
            On
        }

        bool m_FlagDrawRec;
        public CompToggleSwitch()
        {
            this.m_FlagDrawRec = false;
            this.Width = 89;
            this.Height = 35;
            this.State = CompToggleSwitchState.Off;
            this.DoSwitch = true;
        }

        [Category("Default"), Description("")]
        private CompToggleSwitchState m_State;
        public CompToggleSwitchState State
        {
            set { if (m_State != value) { m_State = value; this.Invalidate(); } }
            get { return this.m_State; }
        }

        [Category("Default"), Description("")]
        private bool m_DoSwitch;
        public bool DoSwitch
        {
            set { if (m_DoSwitch != value) { m_DoSwitch = value; } }
            get { return this.m_DoSwitch; }
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
            this.Height = 35;
        }
        protected override void OnClick(EventArgs e)
        {
            if(this.DoSwitch)
            {
                if (this.State == CompToggleSwitchState.Off)
                {
                    this.State = CompToggleSwitchState.On;
                }
                else
                {
                    this.State = CompToggleSwitchState.Off;
                }
            }
            base.OnClick(e);
            this.Invalidate();
        }

        /** 
        * \brief Rahmen zeichnen aktivieren
        * 
        */
        protected override void OnMouseDown(MouseEventArgs pe)
        {
            base.OnMouseDown(pe);
            this.m_FlagDrawRec = true;
            this.Invalidate();
        }
        /** 
        * \brief Rahmen zeichnen deaktivieren
        * 
        */
        protected override void OnMouseUp(MouseEventArgs pe)
        {
            base.OnMouseUp(pe);
            this.m_FlagDrawRec = false;
            this.Invalidate();
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Pen penBlack = new Pen(Brushes.Black, 1);
            Pen penWhite = new Pen(Brushes.White, 1);
            Pen penGray = new Pen(Brushes.DarkGray, 1);
            Pen penBlue = new Pen(Color.DarkBlue);

            Pen penLightGray = new Pen(Brushes.LightGray, 1);

            float width = this.Width;
            float height = this.Height;
            
            PointF p0 = new PointF( 0.0F, 0.0F);
            PointF p1 = new PointF( 0.0F,33.0F);
            PointF p2 = new PointF(87.0F, 33.0F);
            PointF p3 = new PointF(87.0F, 2.0F);
            PointF p4 = new PointF(86.0F, 0.0F);


            PointF p5 = new PointF( 1.0F, 1.0F);
            PointF p6 = new PointF( 1.0F,32.0F);
            PointF p7 = new PointF(85.0F, 1.0F);

            PointF p8 = new PointF( 0.0F,34.0F);
            PointF p9 = new PointF(88.0F,34.0F);
            PointF p10 = new PointF(88.0F, 1.0F);

            Rectangle recSwitch = new Rectangle(0, 0, 89, 35);

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

            if (this.State == CompToggleSwitchState.Off)
            {
                off = 0;
            }
            if (this.State == CompToggleSwitchState.On)
            {
                off = 60;
            }

            p0.X = 3+off; p0.Y = 4;
            p1.X = 3 + off; p1.Y = 29;
            p2.X = 24 + off; p2.Y = 29;
            p3.X = 24 + off; p3.Y = 6;
            p4.X = 23 + off; p4.Y = 4;

            pe.Graphics.DrawLine(penGray, p4, p0);
            pe.Graphics.DrawLine(penGray, p0, p1);
            pe.Graphics.DrawLine(penGray, p1, p2);
            pe.Graphics.DrawLine(penGray, p2, p3);

            p0.X = 4 + off; p0.Y = 5;
            p1.X = 4 + off; p1.Y = 28;
            p2.X = 23 + off; p2.Y = 28;
            p3.X = 23 + off; p3.Y = 6;
            p4.X = 22 + off; p4.Y = 5;

            pe.Graphics.DrawLine(penWhite, p4, p0);
            pe.Graphics.DrawLine(penWhite, p0, p1);
            pe.Graphics.DrawLine(penWhite, p1, p2);
            pe.Graphics.DrawLine(penWhite, p2, p3);

            p0.X = 4 + off; p0.Y = 30;
            p1.X =25 + off; p1.Y = 30;
            p2.X = 25 + off; p2.Y = 5;
            pe.Graphics.DrawLine(penBlack, p0, p1);
            pe.Graphics.DrawLine(penBlack, p1, p2);

            p0.X = 5 + off; p0.Y = 6;
            p1.X = 5 + off; p1.Y = 27;
            p2.X = 22 + off; p2.Y = 27;
            p3.X = 22 + off; p3.Y = 6;

            RectangleF recBtn = new RectangleF(p0.X, p0.Y, p2.X - p1.X, p2.Y - p3.Y);
            pe.Graphics.DrawRectangle(penWhite, p0.X, p0.Y, p2.X - p1.X, p2.Y - p3.Y);
            pe.Graphics.FillRectangle(Brushes.White, recBtn);
            for (int i = 0; i < 6; i++)
            {
                p0.X = 7 + off+i*3; p0.Y = 6;
                p1.X = 7 + off + i * 3; p1.Y = 27;
                pe.Graphics.DrawLine(penBlack, p0, p1);
            }

            if (this.m_FlagDrawRec)
            {
                pe.Graphics.DrawRectangle(penBlue, recSwitch);
            }


        }
    }

}