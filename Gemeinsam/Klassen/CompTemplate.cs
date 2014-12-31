using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Helper
{

    public class CompTemplate : PictureBox
    {

        protected int m_NormalX;
        protected int m_NormalY;
        protected double m_RatioX;
        protected double m_RatioY;
        protected double m_Rotation;
        protected Point m_Point;
        protected PointF m_PointF;
        protected PointF m_PointFCentrePoint;

        protected Rectangle m_Rectangle;
        protected List<Point> m_PointList;
        protected List<Point> m_PointListCalc;
        protected List<Point> m_PointListPoligon;

        protected SolidBrush m_Brush;
        protected Pen m_Pen;

        public CompTemplate()
        {
            this.Width = 40;
            this.Height = 40;
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
            this.m_Rotation = 0.0;
        }

        ~CompTemplate()
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
        }

        protected void DrawArc(Graphics graphic,float startAngle, float angle)
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
            float start_angle = startAngle + (float)this.m_Rotation;
            graphic.DrawArc(this.m_Pen, this.m_Rectangle, start_angle, angle);
            graphic.FillPie(this.m_Brush, this.m_Rectangle, start_angle, angle);
        }

        protected void DrawEllipse(Graphics graphic)
        {
            graphic.DrawEllipse(this.m_Pen, this.m_Rectangle);
            graphic.FillEllipse(this.m_Brush, this.m_Rectangle);
        }
        protected void DrawPoligon(Graphics Graphic)
        {
            Graphic.DrawPolygon(this.m_Pen, this.m_PointListPoligon.ToArray());
            Graphic.FillPolygon(this.m_Brush, this.m_PointListPoligon.ToArray());
        }

        protected PointF RotatePoint(PointF PointToRotate, PointF CenterPoint, double AngleInDegrees)
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


        protected void AddPointPoligon(int Index, bool ClearList = false)
        {
            if(ClearList==true)
            {
                this.m_PointListPoligon.Clear();
            }
            this.m_PointListPoligon.Add(this.m_PointListCalc[Index]);
        }

        protected void SetRatio()
        {
            this.m_RatioX = Convert.ToDouble((this.Width - 1)) / Convert.ToDouble(this.m_NormalX);
            this.m_RatioY = Convert.ToDouble((this.Height - 1)) / Convert.ToDouble(this.m_NormalY);
            this.m_PointFCentrePoint.X = (float)(Convert.ToDouble(this.m_NormalX) / 2.0 * this.m_RatioX);
            this.m_PointFCentrePoint.Y = (float)(Convert.ToDouble(this.m_NormalY) / 2.0 * this.m_RatioY);
        }

        protected void CalcPoints()
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

        protected void AddPoint(int X, int Y, bool ClearList = false)
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