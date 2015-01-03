using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Helper
{
    public class CompArrow : CompTemplate
    {
        public enum CompArrowDirection { Right = 0, Left, Top, Down };

        [Category("Default"), Description("")]
        private Color m_ColorArrow;
        public Color ColorArrow
        {
            set { if (this.m_ColorArrow != value) { this.m_ColorArrow = value; this.Invalidate(); } }
            get { return this.m_ColorArrow; }
        }

        [Category("Default"), Description("")]
        private CompArrowDirection m_Direction;
        public CompArrowDirection Direction
        {
            set { if (this.m_Direction != value) { this.m_Direction = value; this.Invalidate(); } }
            get { return this.m_Direction; }
        }

        public CompArrow()
        {
            this.Height =40;
            this.Width = 40;
            this.m_NormalX = 19;
            this.m_NormalY = 19;
            this.ColorArrow = Color.White;
            this.Direction = CompArrowDirection.Right;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;

            this.m_Brush.Color = this.ColorArrow;
            this.m_Pen.Color = this.ColorArrow;

            if (this.Direction == CompArrowDirection.Right)
            {
                this.m_Rotation = 0.0;
            }
            if (this.Direction == CompArrowDirection.Down)
            {
                this.m_Rotation = 90.0;
            }

            if (this.Direction == CompArrowDirection.Left)
            {
                this.m_Rotation = 180.0;
            }
            if (this.Direction == CompArrowDirection.Top)
            {
                this.m_Rotation = 270.0;
            }


            this.AddPoint(0, 8, true);         //Point  0
            this.AddPoint(0, 11);              //Point  1
            this.AddPoint(10, 8);              //Point  2
            this.AddPoint(10, 11);             //Point  3


            this.AddPoint(10, 9);              //Point  4
            this.AddPoint(10, 10);             //Point  5

            this.AddPoint(10, 14);             //Point  6
            this.AddPoint(19, 10);             //Point  7
            this.AddPoint(19, 9);              //Point  8
            this.AddPoint(10, 5);              //Point  9

            this.CalcPoints();

            this.m_Rotation = 0.0;
            this.AddPointPoligon(1, true);
            this.AddPointPoligon(3);
            this.AddPointPoligon(2);
            this.AddPointPoligon(0);
            this.DrawPoligon(g);

            this.AddPointPoligon(7, true);
            this.AddPointPoligon(8);
            this.AddPointPoligon(9);
            this.AddPointPoligon(9);
            this.AddPointPoligon(5);
            this.AddPointPoligon(6);
            this.DrawPoligon(g);


        }
    }

}