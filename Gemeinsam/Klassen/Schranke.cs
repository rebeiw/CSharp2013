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

namespace Helper
{
    public class Schranke : PictureBox
    {
        [Category("Default"), Description("")]
        private double m_rotation;
        public double Rotation
        {
            set { if (m_rotation != value) { m_rotation = value; this.Invalidate(); } }
            get { return this.m_rotation; }
        }

        Point m_PointRotate;
        List <Point> m_Points;
        Pen m_Pen;
        SolidBrush m_Brush;
        Rectangle m_Rec;

        public Schranke()
        {
            this.m_Rec = new Rectangle();
            m_PointRotate = new Point();
            m_Points = new List<Point>();
 
            m_Pen = new Pen(Color.White, 2);
            m_Brush = new SolidBrush(Color.White);
            this.Rotation = 0.0;

            this.Height =350;
            this.Width = 320;
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


            int nr = 0;
            int startPosX = 40;
            int startPosY = 20;
            int hoeheBalken = 20;
            int breiteBalken = 40;

            int radiusKreis = 15;



            this.SetXY(startPosX,startPosY, true);
            
            m_PointRotate.Y = m_Points[0].Y-10;
            m_PointRotate.X = m_Points[0].X;

            int LinksKreis = m_PointRotate.X - radiusKreis;
            int ObenKreis = m_PointRotate.Y - radiusKreis;

            this.m_Rec.X = LinksKreis-1;
            this.m_Rec.Y = ObenKreis-1;
            this.m_Rec.Width = radiusKreis * 2;
            this.m_Rec.Height = radiusKreis * 2;

            
            m_Pen.Color = Color.White;

            for (int i = 0; i < 7; i++)
            {
                this.SetXY(startPosX + nr * breiteBalken, startPosY, true);
                this.SetXY(startPosX + nr * breiteBalken, startPosY + hoeheBalken);
                this.SetXY(startPosX + nr * breiteBalken + breiteBalken, startPosY + hoeheBalken);
                this.SetXY(startPosX + nr * breiteBalken + breiteBalken, startPosY);



                m_Points[0] = FuncGeneral.RotatePoint(m_Points[0], m_PointRotate, m_rotation);
                m_Points[1] = FuncGeneral.RotatePoint(m_Points[1], m_PointRotate, m_rotation);
                m_Points[2] = FuncGeneral.RotatePoint(m_Points[2], m_PointRotate, m_rotation);
                m_Points[3] = FuncGeneral.RotatePoint(m_Points[3], m_PointRotate, m_rotation);

                double asd = i % 2;
                if (i % 2 == 0)
                {
                    m_Pen.Color = Color.Red;
                }
                else
                {
                    m_Pen.Color = Color.White;
                }

                m_Brush.Color = m_Pen.Color;
                g.DrawPolygon(m_Pen, m_Points.ToArray());
                g.FillPolygon(m_Brush, m_Points.ToArray());

                nr++;
                this.m_Pen.Color = Color.Black;
                m_Brush.Color = m_Pen.Color;

                g.DrawEllipse(m_Pen, m_Rec);
                g.FillEllipse(m_Brush, m_Rec);

            }


        }


        private void SetXY(int X, int Y,bool ClearList=false)
        {
            if (ClearList==true)
            {
                this.m_Points.Clear();
            }
            Point pointXY = new Point(X,Y);
            this.m_Points.Add(SetXY(pointXY));
        }

        private Point SetXY(Point PointXY)
        {
            Point retval=new Point();
            retval.X = PointXY.X;
            retval.Y = this.Height - PointXY.Y - 1;
            return retval;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 350;
            this.Height =320;
        }

    }

}