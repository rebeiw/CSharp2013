using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace Helper
{
    public enum WatertankType
    {
        Watertank=0,
        Leckage
    }
    public class WaterTank : PictureBox
    {

        Font m_Font;
        StringFormat m_Format;
        List<Point> m_Points;
        Point m_Point;
        Pen m_Pen;
        SolidBrush m_Brush;



        public WaterTank()
        {
            this.m_Font = new Font("Arial", 14f, FontStyle.Bold);
            this.m_Format = new StringFormat();
            this.m_Format.Alignment = StringAlignment.Center;
            this.m_Format.LineAlignment = StringAlignment.Center;
            this.m_Points = new List<Point>();
            this.m_Point = new Point();
            this.m_Pen = new Pen(Color.Aqua);
            this.m_Brush = new SolidBrush(Color.Aqua);
            this.Thickness = 10;
            this.Type = WatertankType.Watertank;
        }
        ~WaterTank()
        {
            this.m_Font.Dispose();
            this.m_Format.Dispose();
        }

        [Category("Default"), Description("")]
        private WatertankType m_Type;
        public WatertankType Type
        {
            set { if (m_Type != value) { m_Type = value; this.Invalidate(); } }
            get { return this.m_Type; }
        }

        [Category("Default"), Description("")]
        private bool m_Error;
        public bool Error
        {
            set { if (m_Error != value) { m_Error = value; this.Invalidate(); } }
            get { return this.m_Error; }
        }


        [Category("Default"), Description("")]
        private string m_Caption;
        public string Caption
        {
            set { if (m_Caption != value) { m_Caption = value; this.Invalidate(); } }
            get { return this.m_Caption; }
        }

        [Category("Default"), Description("")]
        private double m_Value;
        public double Value
        {
            set { if (m_Value != value) { m_Value = value; this.Invalidate(); } }
            get { return this.m_Value; }
        }

        [Category("Default"), Description("")]
        private int m_Thickness;
        public int Thickness
        {
            set { if (m_Thickness != value) { m_Thickness = value; this.Invalidate(); } }
            get { return this.m_Thickness; }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            int left = this.Left;
            int top = this.Top;
            int width = this.Width-1;
            int height = this.Height-1;


            
            Rectangle rec = new Rectangle(0, 0, width, height);

            this.AddPoints(0, 0,true);
            this.AddPoints(0, height);
            this.AddPoints(width, height);
            this.AddPoints(width, 0);

            this.AddPoints(width - m_Thickness, 0);
            this.AddPoints(width - m_Thickness, height - m_Thickness);
            this.AddPoints(0 + m_Thickness, height - m_Thickness);
            this.AddPoints(0 + m_Thickness, 0);


            this.m_Pen.Color = Color.Gray;
            this.m_Brush.Color = this.m_Pen.Color;
            g.DrawPolygon(this.m_Pen, this.m_Points.ToArray());
            g.FillPolygon(this.m_Brush, this.m_Points.ToArray());


            int heightWaterMax = height - this.m_Thickness - 1;
            int waterHeight = Convert.ToInt32(Convert.ToDouble(heightWaterMax) / 100.0 * this.m_Value);


            int x1 = Convert.ToInt32(m_Points[6].X) + 1;
            int x2 = Convert.ToInt32(m_Points[5].X) - 1;
            int y1 = Convert.ToInt32(m_Points[6].Y) - 1;
            int y2 = y1 - waterHeight;


            this.AddPoints(x1, y2,true);
            this.AddPoints(x2, y2);
            this.AddPoints(x2, y1);
            this.AddPoints(x1, y1);

            if (this.m_Type == WatertankType.Watertank)
            {
                this.m_Pen.Color = Color.Aqua;
            }
            if (this.m_Type == WatertankType.Leckage)
            {
                if (this.m_Error == true)
                {
                    this.m_Pen.Color = Color.Red;
                }
                else
                {
                    this.m_Pen.Color = Color.Lime;
                }
            }
            this.m_Brush.Color = this.m_Pen.Color;
            if (waterHeight > 0)
            {
                g.DrawPolygon(this.m_Pen, this.m_Points.ToArray());
                g.FillPolygon(this.m_Brush, this.m_Points.ToArray());
            }

            g.DrawString(this.Caption, this.m_Font , Brushes.Black, this.ClientRectangle, this.m_Format);


        }
        private void AddPoints(int X, int Y,bool Clear=false)
        {
            if (Clear==true)
            {
                this.m_Points.Clear();
            }
            this.m_Point.X = X;
            this.m_Point.Y = Y;

            this.m_Points.Add(this.m_Point);
        }
    }
}
