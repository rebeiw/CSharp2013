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

    public enum ValveState
    {
        Undefine = 0,
        Open,
        Close,
        Error
    }

    public enum ValveDirection
    {
        Horizontal = 0,
        Vertical
    };

    public enum ValveMode
    {
        Manuell = 0,
        Automatic
    }

    public enum ValveType
    {
        Normal = 0,
        Small,
        Big
    }

    public enum SteeringMode
    {
        Digital = 0,
        Analog,
    }

    public class Valve : PictureBox
    {
        public Valve()
        {
            this.Height = 40;
            this.Width = 40;
            this.BackColor = Color.Transparent;
            this.SteeringMode = SteeringMode.Digital;
            this.Rotation = 0.0;

        }
        [Category("Default"), Description("")]
        private ValveType m_Type;
        public ValveType Type
        {
            set { if (m_Type != value) { m_Type = value; this.Invalidate(); } }
            get { return this.m_Type; }
        }
        [Category("Default"), Description("")]
        private ValveState m_State;
        public ValveState State
        {
            set { if (m_State != value) { m_State = value; this.Invalidate(); } }
            get { return this.m_State; }
        }

        [Category("Default"), Description("")]
        private double m_Rotation;
        public double Rotation
        {
            set { if (m_Rotation != value) { m_Rotation = value; this.Invalidate(); } }
            get { return this.m_Rotation; }
        }


        [Category("Default"), Description("")]
        private SteeringMode m_SteeringMode;
        public SteeringMode SteeringMode
        {
            set { if (m_SteeringMode != value) { m_SteeringMode = value; this.Invalidate(); } }
            get { return this.m_SteeringMode; }
        }
        [Category("Default"), Description("")]
        private ValveDirection m_FlowDirection;
        public ValveDirection FlowDirection
        {
            set { if (m_FlowDirection != value) { m_FlowDirection = value; this.Invalidate(); } }
            get { return this.m_FlowDirection; }
        }
        [Category("Default"), Description("")]
        private string m_Caption;
        public string Caption
        {
            set { if (m_Caption != value) { m_Caption = value; this.Invalidate(); } }
            get { return this.m_Caption; }
        }
        [Category("Default"), Description("")]
        private ValveMode m_Mode;
        public ValveMode Mode
        {
            set { if (m_Mode != value) { m_Mode = value; this.Invalidate(); } }
            get { return this.m_Mode; }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (m_Type == ValveType.Normal)
            {
                this.Width = 40;
                this.Height = 40;
            }
            if (m_Type == ValveType.Small)
            {
                this.Width = 30;
                this.Height = 30;
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {

            Color color = Color.Yellow;
            double rot = 0.0;
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            double valveRotation = 0.0;
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


            if (m_SteeringMode == Helper.SteeringMode.Analog)
            {
                color = Color.Black;
                if (m_Rotation < 0.0)
                {
                    this.Rotation = 0.0;
                }
                if (m_Rotation > 100.0)
                {
                    this.Rotation = 100.0;
                }
                valveRotation = 90.0 / 100.0 * m_Rotation;
            }
            else
            {
                if (m_State == ValveState.Open)
                {
                    color = Color.Black;
                    valveRotation = 0.0;
                }
                if (m_State == ValveState.Close)
                {
                    color = Color.Black;
                    valveRotation = 90.0;
                }
                if (m_State == ValveState.Undefine)
                {
                    color = Color.Black;
                    valveRotation = 45.0 + 90.0;
                }
            }
            if (m_State == ValveState.Error)
            {
                color = Color.Red;
                valveRotation = 45.0;
            }

            if (m_FlowDirection == ValveDirection.Horizontal)
            {
                rot = 0.0;
            }

            if (m_FlowDirection == ValveDirection.Vertical)
            {
                rot = 90.0;
            }
            double heightBar = 10.0;

            if (m_Type == ValveType.Normal)
            {
                heightBar = 10.0;
                this.Width = 40;
                this.Height = 40;
            }
            if (m_Type == ValveType.Small)
            {
                heightBar = 6.0;
                this.Width = 30;
                this.Height = 30;
            }

            if (m_Type == ValveType.Big)
            {
                heightBar = 12.0;
                this.Width = 50;
                this.Height = 50;
            }

            rot = rot + valveRotation;
            int height = this.Height;
            int width = this.Width;

            Point pointCenter = new Point();
            pointCenter.X = height / 2;
            pointCenter.Y = width / 2;

            int center = height / 2;




            double widthImage = this.Width;
            double heightImage = this.Height;



            double radius = heightImage / 2.0;

            double x_1 = 0.0;
            double x_2 = 0.0;
            double x_3 = 0.0;
            double x_4 = 0.0;

            double y_1 = 0.0;
            double y_2 = 0.0;
            double y_3 = 0.0;
            double y_4 = 0.0;

            double widthBar = (Math.Sqrt((radius * radius) - (heightBar / 2.0 * heightBar / 2.0)));

            x_1 = radius - widthBar;
            y_1 = heightImage / 2.0 - heightBar / 2.0;

            x_2 = x_1;
            y_2 = y_1 + heightBar;

            x_3 = x_2 + widthBar * 2.0;
            y_3 = y_2;

            x_4 = x_3;
            y_4 = y_1;

            int x1 = Convert.ToInt32(x_1);
            int x2 = Convert.ToInt32(x_2);
            int x3 = Convert.ToInt32(x_3);
            int x4 = Convert.ToInt32(x_4);

            int y1 = Convert.ToInt32(y_1);
            int y2 = Convert.ToInt32(y_2 - 1);
            int y3 = Convert.ToInt32(y_3 - 1);
            int y4 = Convert.ToInt32(y_4);

            Point[] point = { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3), new Point(x4, y4) };

            //rot = 10.0;
            point[0] = FuncGeneral.RotatePoint(point[0], pointCenter, rot);
            point[1] = FuncGeneral.RotatePoint(point[1], pointCenter, rot);
            point[2] = FuncGeneral.RotatePoint(point[2], pointCenter, rot);
            point[3] = FuncGeneral.RotatePoint(point[3], pointCenter, rot);

            Color colorValve = Color.White;
            if (this.m_Mode == ValveMode.Automatic)
            {
                colorValve = Color.White;
            }
            if (this.m_Mode == ValveMode.Manuell)
            {
                colorValve = Color.Gainsboro;
            }

            SolidBrush brush = new SolidBrush(colorValve);
            int penWidth = 3;
            Pen pen = new Pen(Color.Black, penWidth);
            int left = penWidth / 2;
            int top = left;
            Rectangle rec = new Rectangle(left, top, width - penWidth, height - penWidth);
            pe.Graphics.DrawEllipse(pen, rec);
            pe.Graphics.FillEllipse(brush, rec);

            Pen penTriangle = new Pen(color);
            SolidBrush brushTriangle = new SolidBrush(color);

            pe.Graphics.DrawPolygon(penTriangle, point);
            pe.Graphics.FillPolygon(brushTriangle, point);

            Font f = new Font("Arial", 14f, FontStyle.Bold);
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;


            g.DrawString(this.Caption, f, Brushes.Black, rec, strFormat);

        }
    }

}