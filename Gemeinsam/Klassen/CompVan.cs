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
namespace Helper
{
    public class CompVan : CompTemplate
    {
        public enum CompVanDirection { Right = 0, Left, Top, Down };

        [Category("Default"), Description("")]
        private Color m_ColorVan;
        public Color ColorVan
        {
            set { if (this.m_ColorVan != value) { this.m_ColorVan = value; this.Invalidate(); } }
            get { return this.m_ColorVan; }
        }

        [Category("Default"), Description("")]
        private CompVanDirection m_Direction;
        public CompVanDirection Direction
        {
            set { if (this.m_Direction != value) { this.m_Direction = value; this.Invalidate(); } }
            get { return this.m_Direction; }
        }

        public CompVan()
        {
            this.Height =40;
            this.Width = 40;
            this.m_NormalX = 19;
            this.m_NormalY = 19;
            this.ColorVan = Color.White;
            this.Direction = CompVanDirection.Right;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;


            if (this.Direction == CompVanDirection.Right)
            {
                this.m_Rotation = 0.0;
            }
            if (this.Direction == CompVanDirection.Down)
            {
                this.m_Rotation = 90.0;
            }

            if (this.Direction == CompVanDirection.Left)
            {
                this.m_Rotation = 180.0;
            }
            if (this.Direction == CompVanDirection.Top)
            {
                this.m_Rotation = 270.0;
            }


            this.AddPoint(1, 9, true);         //Point  0
            this.AddPoint(1, 10);              //Point  1

            this.AddPoint(5, 16);              //Point  2
            this.AddPoint(5, 17);              //Point  3

            this.AddPoint(15, 14);             //Point  4
            this.AddPoint(15, 15);             //Point  5

            this.AddPoint(15, 9);              //Point  6
            this.AddPoint(15, 10);             //Point  7

            this.AddPoint(15, 4);              //Point  8
            this.AddPoint(15, 5);              //Point  9

            this.AddPoint(5, 2);               //Point 10
            this.AddPoint(5, 3);               //Point 11

            this.CalcPoints();

            this.m_Rectangle.X = 1;
            this.m_Rectangle.Y = 1;
            this.m_Rectangle.Width = this.Width-2;
            this.m_Rectangle.Height = this.Height-2;



            this.m_Brush.Color = this.ColorVan;
            this.m_Pen.Color = Color.Black;

            this.DrawEllipse(g);

            this.m_Pen.Color = Color.Black;
            this.m_Brush.Color = Color.Black;

            //this.m_Rotation = 0.0;
            this.AddPointPoligon(1, true);
            this.AddPointPoligon(7);
            this.AddPointPoligon(6);
            this.AddPointPoligon(0);
            this.DrawPoligon(g);

            this.AddPointPoligon(3, true);
            this.AddPointPoligon(5);
            this.AddPointPoligon(4);
            this.AddPointPoligon(2);
            this.DrawPoligon(g);

            this.AddPointPoligon(11, true);
            this.AddPointPoligon(9);
            this.AddPointPoligon(8);
            this.AddPointPoligon(10);
            this.DrawPoligon(g);

        }
    }

}