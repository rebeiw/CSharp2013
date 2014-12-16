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

    public enum CrossState
    {
        StandStill=0,
        Moving
    }

    public class Cross : PictureBox
    {
        private float VerstaerkungX;
        private float VerstaerkungY;
        private float Breite = 8.0F;
        private float Hoehe =  8.0F;

        private List<PointF> UrsprungsKoordinaden;
        private PointF CentrePoint=new PointF();
        private SolidBrush brushTriangle = new SolidBrush(Color.Blue);
        private Pen penOutline = new Pen(Color.Blue, 1);
        //private RectangleF Circle=new RectangleF();  Noch implementieren
        

        [Category("Default"), Description("")]
        private CrossState m_State;
        public CrossState State
        {
            set { m_State = value; Invalidate(); }
            get { return this.m_State; }
        }

        [Category("Default"), Description("")]
        private Color m_ColorCross;
        public Color ColorCross
        {
            set { m_ColorCross = value; Invalidate(); }
            get { return this.m_ColorCross; }
        }

        [Category("Default"), Description("")]
        private double m_Direction;
        public double Direction
        {
            set { m_Direction = value; Invalidate(); }
            get { return this.m_Direction; }
        }



        public Cross()
        {
            this.Height = 70;
            this.Width = 70;
            this.BackColor = Color.Transparent;
            this.ColorCross = Color.Blue;
            this.Direction = 0.0;

            this.UrsprungsKoordinaden = new List<PointF>();
        }
        ~Cross()
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
            float drehung = (float)this.m_Direction;
    
            Color colorOutline;
            Color colorInLine;
            Color colorBackground;

            colorBackground = Color.Transparent;
            colorOutline = Color.White;
            colorInLine = this.m_ColorCross;

            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            this.BackColor = colorBackground;
            this.UrsprungsKoordinaden.Clear();

            if (this.m_State == CrossState.StandStill)
            {
                this.AddUrsprungsKoordinaden(4.0F, 8.0F);// 0
                this.AddUrsprungsKoordinaden(6.0F, 7.0F);// 1
                this.AddUrsprungsKoordinaden(5.0F, 7.0F);// 2
                this.AddUrsprungsKoordinaden(5.0F, 5.0F);// 3
                this.AddUrsprungsKoordinaden(7.0F, 5.0F);// 4
                this.AddUrsprungsKoordinaden(7.0F, 6.0F);// 5
                this.AddUrsprungsKoordinaden(8.0F, 4.0F);// 6
                this.AddUrsprungsKoordinaden(7.0F, 2.0F);// 7
                this.AddUrsprungsKoordinaden(7.0F, 3.0F);// 8
                this.AddUrsprungsKoordinaden(5.0F, 3.0F);// 9
                this.AddUrsprungsKoordinaden(5.0F, 1.0F);//10
                this.AddUrsprungsKoordinaden(6.0F, 1.0F);//11
                this.AddUrsprungsKoordinaden(4.0F, 0.0F);//12
                this.AddUrsprungsKoordinaden(2.0F, 1.0F);//13
                this.AddUrsprungsKoordinaden(3.0F, 1.0F);//14
                this.AddUrsprungsKoordinaden(3.0F, 3.0F);//15
                this.AddUrsprungsKoordinaden(1.0F, 3.0F);//16
                this.AddUrsprungsKoordinaden(1.0F, 2.0F);//17
                this.AddUrsprungsKoordinaden(0.0F, 4.0F);//18
                this.AddUrsprungsKoordinaden(1.0F, 6.0F);//19
                this.AddUrsprungsKoordinaden(1.0F, 5.0F);//20
                this.AddUrsprungsKoordinaden(3.0F, 5.0F);//21
                this.AddUrsprungsKoordinaden(3.0F, 7.0F);//22
                this.AddUrsprungsKoordinaden(2.0F, 7.0F);//23
            }

            if (this.m_State == CrossState.Moving)
            {
                this.AddUrsprungsKoordinaden(4.0F, 7.0F);// 0
                this.AddUrsprungsKoordinaden(7.0F, 5.0F);// 1
                this.AddUrsprungsKoordinaden(5.0F, 5.0F);// 2
                this.AddUrsprungsKoordinaden(5.0F, 1.0F);// 3
                this.AddUrsprungsKoordinaden(3.0F, 1.0F);// 4
                this.AddUrsprungsKoordinaden(3.0F, 5.0F);// 5
                this.AddUrsprungsKoordinaden(1.0F, 5.0F);// 6
                FuncGeneral.RotatePoints(ref this.UrsprungsKoordinaden, this.CentrePoint, drehung);
            }

            penOutline.Width = 3;

            penOutline.Color = colorOutline;
            brushTriangle.Color = colorInLine;
            g.DrawPolygon(penOutline, this.UrsprungsKoordinaden.ToArray());
            g.FillPolygon(brushTriangle, this.UrsprungsKoordinaden.ToArray());

            this.UrsprungsKoordinaden.Clear();
            this.AddUrsprungsKoordinaden(0.5F, 0.5F);// 0
            this.AddUrsprungsKoordinaden(7.0F, 7.0F);// 0

            float x=this.UrsprungsKoordinaden[0].X;
            float y=this.UrsprungsKoordinaden[0].Y;
            float widh=this.UrsprungsKoordinaden[1].X;

        }
    }

}