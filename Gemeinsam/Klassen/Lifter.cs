using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using System.Drawing.Configuration;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;


namespace Helper
{

    public enum LifterState
    {
        Down=0,
        Sinking,
        Lifting,
        Up,
        Error
    }

    public class Lifter : PictureBox
    {
        private float VerstaerkungX;
        private float VerstaerkungY;
        private float Breite = 4.0F;
        private float Hoehe = 12.0F;

        private List<PointF> UrsprungsKoordinaden;
        private PointF CentrePoint=new PointF();
        private SolidBrush brushTriangle = new SolidBrush(Color.Blue);
        private Pen penOutline = new Pen(Color.Blue, 1);

        [Category("Default"), Description("")]
        private LifterState m_State;
        public LifterState State
        {
            set { if (m_State != value) { m_State = value; this.Invalidate(); } }
            get { return this.m_State; }
        }

        public Lifter()
        {
            this.Height = 129;
            this.Width = 44;
            this.BackColor = Color.Transparent;

            this.UrsprungsKoordinaden = new List<PointF>();
        }
        ~Lifter()
        {
            this.UrsprungsKoordinaden = null;
        }

        private void AddUrsprungsKoordinaden(float X, float Y)
        {
            this.VerstaerkungX = (float)this.Width / this.Breite;
            this.VerstaerkungY = (float)this.Height / this.Hoehe;
            this.CentrePoint.X = (float)this.Breite * this.VerstaerkungX / 2.0F;
            this.CentrePoint.Y = (float)this.Hoehe * this.VerstaerkungY / 2.0F;


            PointF koor = new PointF();
            koor.X = X * VerstaerkungX;
            koor.Y = this.Height - Y * VerstaerkungX;
            this.UrsprungsKoordinaden.Add(koor);
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            bool drawObenUnten=false;
            float drehung = 0.0F;

            Color colorOben;
            Color colorUnten;
            Color colorPfeil;
            Color colorBackground;


            colorBackground = Color.Transparent;
            colorOben = Color.Yellow;
            colorUnten = Color.Lime;
            colorPfeil = Color.Gray;

            if (this.m_State == LifterState.Lifting)
            {
                drawObenUnten = false;
                drehung = 0.0F;
                colorBackground = Color.Transparent;
                colorOben = Color.DarkGreen;
                colorUnten = Color.Yellow;
                colorPfeil = Color.Gray;
            }
            if (this.m_State == LifterState.Up)
            {
                drawObenUnten = false;
                drehung = 0.0F;
                colorBackground = Color.Transparent;
                colorOben = Color.Yellow;
                colorUnten = Color.Yellow;
                colorPfeil = Color.Blue;
            }

            if (this.m_State == LifterState.Sinking)
            {
                drawObenUnten = false;
                drehung = 180.0F;
                colorBackground = Color.Transparent;
                colorOben = Color.DarkGreen;
                colorUnten = Color.Yellow;
                colorPfeil = Color.Gray;
            }
            if (this.m_State == LifterState.Down)
            {
                drawObenUnten = false;
                drehung = 180.0F;
                colorBackground = Color.Transparent;
                colorOben = Color.DarkGreen;
                colorUnten = Color.DarkGreen;
                colorPfeil = Color.Blue;
            }
            if (this.m_State == LifterState.Error)
            {
                drawObenUnten = true;
                drehung = 0.0F;
                colorBackground = Color.Red;
                colorOben = Color.Gray;
                colorUnten = Color.Gray;
                colorPfeil = Color.Gray;
            }
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            this.BackColor = colorBackground;
            this.UrsprungsKoordinaden.Clear();
            this.AddUrsprungsKoordinaden(0.0F, 1.0F);//00
            this.AddUrsprungsKoordinaden(4.0F, 1.0F);//01
            this.AddUrsprungsKoordinaden(4.0F, 0.0F);//02
            this.AddUrsprungsKoordinaden(0.0F, 0.0F);//03

            penOutline.Color = colorUnten;
            brushTriangle.Color = colorUnten;
            g.DrawPolygon(penOutline, this.UrsprungsKoordinaden.ToArray());
            g.FillPolygon(brushTriangle, this.UrsprungsKoordinaden.ToArray());

            this.UrsprungsKoordinaden.Clear();
            this.AddUrsprungsKoordinaden(0.0F,12.0F);//00
            this.AddUrsprungsKoordinaden(4.0F,12.0F);//01
            this.AddUrsprungsKoordinaden(4.0F,11.0F);//02
            this.AddUrsprungsKoordinaden(0.0F,11.0F);//03


            penOutline.Color = colorOben;
            brushTriangle.Color = colorOben;
            g.DrawPolygon(penOutline, this.UrsprungsKoordinaden.ToArray());
            g.FillPolygon(brushTriangle, this.UrsprungsKoordinaden.ToArray());


            if (!drawObenUnten)
            {
                this.UrsprungsKoordinaden.Clear();
                this.AddUrsprungsKoordinaden(2.0F, 9.0F);//00
                this.AddUrsprungsKoordinaden(4.0F, 8.0F);//01
                this.AddUrsprungsKoordinaden(4.0F, 7.0F);//02
                this.AddUrsprungsKoordinaden(3.0F, 7.0F);//03
                this.AddUrsprungsKoordinaden(3.0F, 3.0F);//04
                this.AddUrsprungsKoordinaden(1.0F, 3.0F);//05
                this.AddUrsprungsKoordinaden(1.0F, 7.0F);//06
                this.AddUrsprungsKoordinaden(0.0F, 7.0F);//07
                this.AddUrsprungsKoordinaden(0.0F, 8.0F);//08
            }
            else
            {
                this.UrsprungsKoordinaden.Clear();
                this.AddUrsprungsKoordinaden(2.0F, 9.0F);//00
                this.AddUrsprungsKoordinaden(4.0F, 8.0F);//01
                this.AddUrsprungsKoordinaden(4.0F, 7.0F);//02
                this.AddUrsprungsKoordinaden(3.0F, 7.0F);//03
                this.AddUrsprungsKoordinaden(3.0F, 5.0F);//04
                this.AddUrsprungsKoordinaden(4.0F, 5.0F);//05
                this.AddUrsprungsKoordinaden(4.0F, 4.0F);//06
                this.AddUrsprungsKoordinaden(2.0F, 3.0F);//07
                this.AddUrsprungsKoordinaden(0.0F, 4.0F);//08
                this.AddUrsprungsKoordinaden(0.0F, 5.0F);//09
                this.AddUrsprungsKoordinaden(1.0F, 5.0F);//10
                this.AddUrsprungsKoordinaden(1.0F, 7.0F);//11
                this.AddUrsprungsKoordinaden(0.0F, 7.0F);//12
                this.AddUrsprungsKoordinaden(0.0F, 8.0F);//13
            }

            FuncGeneral.RotatePoints(ref this.UrsprungsKoordinaden, this.CentrePoint, drehung);

            penOutline.Color = colorPfeil;
            brushTriangle.Color = colorPfeil;
            g.DrawPolygon(penOutline, this.UrsprungsKoordinaden.ToArray());
            g.FillPolygon(brushTriangle, this.UrsprungsKoordinaden.ToArray());
        }
    }

}