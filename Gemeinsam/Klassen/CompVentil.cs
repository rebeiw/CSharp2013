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

    public class CompVentil : PictureBox
    {

        public enum CompVentilState { Open = 0, Close };
        public enum CompVentilDirection { Horizontal = 0, Vertical };

        [Category("Default"), Description("")]
        private CompVentilDirection m_Direction;
        public CompVentilDirection Direction
        {
            set { if (this.m_Direction != value) { this.m_Direction = value; this.Invalidate(); } }
            get { return this.m_Direction; }
        }

        [Category("Default"), Description("")]
        private Color m_ColorOpen;
        public Color ColorOpen
        {
            set { if (this.m_ColorOpen != value) { this.m_ColorOpen = value; this.Invalidate(); } }
            get { return this.m_ColorOpen; }
        }

        [Category("Default"), Description("")]
        private Color m_ColorClose;
        public Color ColorClose
        {
            set { if (this.m_ColorClose != value) { this.m_ColorClose = value; this.Invalidate(); } }
            get { return this.m_ColorClose; }
        }

        [Category("Default"), Description("")]
        private CompVentilState m_State;
        public CompVentilState State
        {
            set { if (this.m_State != value) { this.m_State = value; this.Invalidate(); } }
            get { return this.m_State; }
        }

        private int m_NormalX;
        private int m_NormalY;
        private double m_RatioX;
        private double m_RatioY;
        private double m_Rotation;
        private Point m_Point;
        private PointF m_PointF;
        private PointF m_PointFCentrePoint;

        private Rectangle m_Rectangle;
        private List<Point> m_PointList;
        private List<Point> m_PointListCalc;
        private List<Point> m_PointListPoligon;

        private SolidBrush m_Brush;
        private Pen m_Pen;

        public CompVentil()
        {
            this.Width = 40;
            this.Height = 40;
            this.ColorOpen = Color.Blue;
            this.ColorClose = Color.Aqua;
            this.m_Brush = new SolidBrush(Color.Blue);
            this.m_Pen = new Pen(Color.Blue, 1);
            this.m_PointList = new List<Point>();
            this.m_PointListCalc = new List<Point>();
            this.m_PointListPoligon = new List<Point>();
            this.m_Point = new Point();
            this.m_PointFCentrePoint = new Point();
            this.m_Rectangle = new Rectangle();
            this.m_NormalX = 19;
            this.m_NormalY = 19;
            this.Direction = CompVentilDirection.Horizontal;
        }

        ~CompVentil()
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {

            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (this.State == CompVentilState.Open)
            {
                this.m_Brush.Color = this.ColorOpen;
                this.m_Pen.Color = this.ColorOpen;
            }
            if (this.State == CompVentilState.Close)
            {
                this.m_Brush.Color = this.ColorClose;
                this.m_Pen.Color = this.ColorClose;
            }
            if (this.Direction == CompVentilDirection.Vertical)
            {
                this.m_Rotation = -90.0;
            }
            if (this.Direction == CompVentilDirection.Horizontal)
            {
                this.m_Rotation = 0.0;
            }




            this.AddPoint(0, 11, true);         //Point  0
            this.AddPoint(0, 19);               //Point  1
            this.AddPoint(19, 19);              //Point  2
            this.AddPoint(19, 11);              //Point  3
            this.AddPoint(10, 15);              //Point  4
            this.AddPoint(9, 15);               //Point  5
            this.AddPoint(9, 5);                //Point  6
            this.AddPoint(10, 5);               //Point  7
            this.AddPoint(5, 5);                //Point  8
            this.AddPoint(14, 5);               //Point  9
            this.AddPoint(5, 0);                //Point 10
            this.AddPoint(14, 10);              //Point 11

            this.CalcPoints();


            this.AddPointPoligon(0, true);
            this.AddPointPoligon(1);
            this.AddPointPoligon(5);
            this.DrawPoligon(g);

            this.AddPointPoligon(2, true);
            this.AddPointPoligon(3);
            this.AddPointPoligon(4);
            this.DrawPoligon(g);

            this.AddPointPoligon(6, true);
            this.AddPointPoligon(5);
            this.AddPointPoligon(4);
            this.AddPointPoligon(7);
            this.DrawPoligon(g);

            this.AddPointPoligon(8, true);
            this.AddPointPoligon(9);
            this.AddPointPoligon(9);
            this.AddPointPoligon(8);
            this.DrawPoligon(g);

            this.AddPointPoligon(10, true);
            this.AddPointPoligon(11);
            this.DrawArc(g,180.0F,180.0F);
        }

        private void DrawArc(Graphics Graphic,float StartAngle, float Angle)
        {
            int left = 0;
            int top = 0;
            int width = 0;
            int height = 0;
            if (this.m_Rotation != 0.0)
            {
                left = this.m_PointListPoligon[0].X;
                top = this.m_PointListPoligon[1].Y;
                width = this.m_PointListPoligon[1].X - this.m_PointListPoligon[0].X + 1;
                height = this.m_PointListPoligon[0].Y - this.m_PointListPoligon[1].Y + 1;

            }
            else
            {
                left = this.m_PointListPoligon[0].X;
                top = this.m_PointListPoligon[0].Y;
                width = this.m_PointListPoligon[1].X - this.m_PointListPoligon[0].X + 1;
                height = this.m_PointListPoligon[1].Y - this.m_PointListPoligon[0].Y + 1;

            }
            this.m_Rectangle.X = left;
            this.m_Rectangle.Y = top;
            this.m_Rectangle.Width = Math.Abs(width);
            this.m_Rectangle.Height = Math.Abs(height);
            float start_angle = StartAngle + (float)this.m_Rotation;
            Graphic.DrawArc(this.m_Pen, this.m_Rectangle, start_angle, Angle);
            Graphic.FillPie(this.m_Brush, this.m_Rectangle, start_angle, Angle);
        }

        private void DrawPoligon(Graphics Graphic)
        {
            Graphic.DrawPolygon(this.m_Pen, this.m_PointListPoligon.ToArray());
            Graphic.FillPolygon(this.m_Brush, this.m_PointListPoligon.ToArray());
        }

        private PointF RotatePoint(PointF PointToRotate, PointF CenterPoint, double AngleInDegrees)
        {
            double angleInRadians = AngleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);

            return new Point
            {
                X = (int)(cosTheta * (PointToRotate.X - CenterPoint.X) - sinTheta * (PointToRotate.Y - CenterPoint.Y) + CenterPoint.X),
                Y = (int)(sinTheta * (PointToRotate.X - CenterPoint.X) + cosTheta * (PointToRotate.Y - CenterPoint.Y) + CenterPoint.Y)
            };
        }


        private void AddPointPoligon(int Index, bool ClearList=false)
        {
            if(ClearList==true)
            {
                this.m_PointListPoligon.Clear();
            }
            this.m_PointListPoligon.Add(this.m_PointListCalc[Index]);
        }

        private void SetRatio()
        {
            this.m_RatioX = Convert.ToDouble((this.Width - 1)) / Convert.ToDouble(this.m_NormalX);
            this.m_RatioY = Convert.ToDouble((this.Height - 1)) / Convert.ToDouble(this.m_NormalY);

            this.m_PointFCentrePoint.X = (float)(Convert.ToDouble(this.m_NormalX) / 2.0 * this.m_RatioX);
            this.m_PointFCentrePoint.Y = (float)(Convert.ToDouble(this.m_NormalY) / 2.0 * this.m_RatioY);

        }

        private void CalcPoints()
        {
            this.SetRatio();
            this.m_PointListCalc.Clear();
            foreach(Point point in this.m_PointList)
            {
                double x = (double)point.X * this.m_RatioX;
                double y = (double)point.Y * this.m_RatioY;

                this.m_PointF.X = (float)x;
                this.m_PointF.Y = (float)y;


                this.m_PointF=this.RotatePoint(this.m_PointF, this.m_PointFCentrePoint, this.m_Rotation);


                this.m_Point.X = (int)this.m_PointF.X;
                this.m_Point.Y = (int)this.m_PointF.Y;
                this.m_PointListCalc.Add(this.m_Point);
            }
        }

        private void AddPoint(int X, int Y,bool ClearList=false)
        {
            if(ClearList==true)
            {
                this.m_PointList.Clear();
            }
            this.m_Point.X = X;
            this.m_Point.Y = Y;
            this.m_PointList.Add(this.m_Point);
        }
    }
}