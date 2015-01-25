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
    public class CompMultiBar : CompTemplate
    {
        public enum CompVanDirection { Right = 0, Left, Top, Down };

        [Category("Default"), Description("")]
        private Color m_ColorBar1;
        public Color ColorBar1
        {
            set { if (this.m_ColorBar1 != value) { this.m_ColorBar1 = value; this.Invalidate(); } }
            get { return this.m_ColorBar1; }
        }

        [Category("Default"), Description("")]
        private Color m_ColorBar2;
        public Color ColorBar2
        {
            set { if (this.m_ColorBar2 != value) { this.m_ColorBar2 = value; this.Invalidate(); } }
            get { return this.m_ColorBar2; }
        }

        [Category("Default"), Description("")]
        private Color m_ColorBar3;
        public Color ColorBar3
        {
            set { if (this.m_ColorBar3 != value) { this.m_ColorBar3 = value; this.Invalidate(); } }
            get { return this.m_ColorBar3; }
        }

        [Category("Default"), Description("")]
        private Color m_ColorBar4;
        public Color ColorBar4
        {
            set { if (this.m_ColorBar4 != value) { this.m_ColorBar4 = value; this.Invalidate(); } }
            get { return this.m_ColorBar4; }
        }

        [Category("Default"), Description("")]
        private Color m_ColorBar5;
        public Color ColorBar5
        {
            set { if (this.m_ColorBar5 != value) { this.m_ColorBar5 = value; this.Invalidate(); } }
            get { return this.m_ColorBar5; }
        }

        [Category("Default"), Description("")]
        private CompVanDirection m_Direction;
        public CompVanDirection Direction
        {
            set { if (this.m_Direction != value) { this.m_Direction = value; this.Invalidate(); } }
            get { return this.m_Direction; }
        }

        [Category("Default"), Description("")]
        private double m_MaxValue;
        public double MaxValue
        {
            set { if (this.m_MaxValue != value) { this.m_MaxValue = value; this.Invalidate(); } }
            get { return this.m_MaxValue; }
        }

        [Category("Default"), Description("")]
        private int m_NumberOfBars;
        public int NumberOfBars
        {
            set { if (this.m_NumberOfBars != value) { this.m_NumberOfBars = value; this.Invalidate(); } }
            get { return this.m_NumberOfBars; }
        }

        
        [Category("Default"), Description("")]
        private double m_Value1;
        public double Value1
        {
            set { if (this.m_Value1 != value) { this.m_Value1 = value; this.Invalidate(); } }
            get { return this.m_Value1; }
        }

        [Category("Default"), Description("")]
        private double m_Value2;
        public double Value2
        {
            set { if (this.m_Value2 != value) { this.m_Value2 = value; this.Invalidate(); } }
            get { return this.m_Value2; }
        }

        [Category("Default"), Description("")]
        private double m_Value3;
        public double Value3
        {
            set { if (this.m_Value3 != value) { this.m_Value3 = value; this.Invalidate(); } }
            get { return this.m_Value3; }
        }

        [Category("Default"), Description("")]
        private double m_Value4;
        public double Value4
        {
            set { if (this.m_Value4 != value) { this.m_Value4 = value; this.Invalidate(); } }
            get { return this.m_Value4; }
        }

        [Category("Default"), Description("")]
        private double m_Value5;
        public double Value5
        {
            set { if (this.m_Value5 != value) { this.m_Value5 = value; this.Invalidate(); } }
            get { return this.m_Value5; }
        }

        [Category("Default"), Description("")]
        private bool m_Choise;
        public bool Choise
        {
            set { if (this.m_Choise != value) { this.m_Choise = value; this.Invalidate(); } }
            get { return this.m_Choise; }
        }

        public CompMultiBar()
        {
            this.Height =50;
            this.Width = 250;
            this.m_NormalX = 9;
            this.m_NormalY = 99;
            this.Value1 = 0.00;
            this.Value2 = 0.00;
            this.Value3 = 0.00;
            this.Value4 = 0.00;
            this.Value5 = 0.00;
            this.NumberOfBars = 1;
            this.MaxValue = 10.00;
            this.ColorBar1 = Color.Yellow;
            this.ColorBar2 = Color.Orange;
            this.ColorBar3 = Color.RoyalBlue;
            this.ColorBar4 = Color.Blue;
            this.ColorBar5 = Color.Green;

            this.Direction = CompVanDirection.Right;
            this.Choise = false;
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

            if(this.m_NumberOfBars==1)
            {
                this.m_NormalX = 9;
                DrawOneBar(g, 0, this.Value1, this.ColorBar1);
            }

            if (this.m_NumberOfBars == 2)
            {
                this.m_NormalX = 9;
                DrawOneBar(g, 0, this.Value1, this.ColorBar1);
                DrawOneBar(g, 1, this.Value2, this.ColorBar2);
            }
            if (this.m_NumberOfBars == 3)
            {
                this.m_NormalX = 9;
                DrawOneBar(g, 0, this.Value1, this.ColorBar1);
                DrawOneBar(g, 1, this.Value2, this.ColorBar2);
                DrawOneBar(g, 2, this.Value3, this.ColorBar3);
            }

            if (this.m_NumberOfBars == 4)
            {
                this.m_NormalX = 12;
                DrawOneBar(g, 0, this.Value1, this.ColorBar1);
                DrawOneBar(g, 1, this.Value2, this.ColorBar2);
                DrawOneBar(g, 2, this.Value3, this.ColorBar3);
                DrawOneBar(g, 3, this.Value4, this.ColorBar4);
            }
            if (this.m_NumberOfBars == 5)
            {
                this.m_NormalX = 10;
                DrawOneBar(g, 0, this.Value1, this.ColorBar1);
                DrawOneBar(g, 1, this.Value2, this.ColorBar2);
                DrawOneBar(g, 2, this.Value3, this.ColorBar3);
                DrawOneBar(g, 3, this.Value4, this.ColorBar4);
                DrawOneBar(g, 4, this.Value5, this.ColorBar5);
            }


            if (this.Choise==true)
            {
                this.SetFocus(g);
            }
        }
        private void DrawOneBar(Graphics g, int numberBar, double value, Color color)
        {
            int[] x_width_bars = { 10, 5, 3, 3, 2 };

            int x_width = x_width_bars[this.m_NumberOfBars-1];
            int x_1 = numberBar * x_width;
            int x_2 = 0;
            if(numberBar==NumberOfBars)
            {
                x_2 = x_1 + x_width - 1;
            }
            else
            {
                x_2 = x_1 + x_width;
            }

            double normal_height = Convert.ToDouble(this.m_NormalY);
            double bar_height = value / this.m_MaxValue * normal_height;

            int point_y = Convert.ToInt32(normal_height - bar_height);

            this.AddPoint(x_1, 0, true);              //Point  0
            this.AddPoint(x_1, 99);                   //Point  1
            this.AddPoint(x_2, 99);                   //Point  2
            this.AddPoint(x_2, 0);                    //Point  3


            this.CalcPoints();

            this.m_Pen.Color = Color.Black;
            this.m_Brush.Color = Color.Silver;
            this.AddPointPoligon(0, true);
            this.AddPointPoligon(1);
            this.AddPointPoligon(2);
            this.AddPointPoligon(3);
            this.DrawPoligon(g);

            this.AddPoint(x_1, point_y, true);        //Point  0
            this.AddPoint(x_1, 99);                   //Point  1
            this.AddPoint(x_2, 99);                   //Point  2
            this.AddPoint(x_2, point_y);              //Point  3

            this.CalcPoints();

            this.m_Pen.Color = color;
            this.m_Brush.Color = color;
            this.AddPointPoligon(0, true);
            this.AddPointPoligon(1);
            this.AddPointPoligon(2);
            this.AddPointPoligon(3);
            this.DrawPoligon(g);
        }
    }
}