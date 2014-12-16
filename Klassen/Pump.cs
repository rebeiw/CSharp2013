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

    public enum PumpState
    {
        NotUsed = 0,
        Ready,
        Running,
        Error
    }

    public enum PumpFlowDirection
    {
        Right = 0,
        Up,
        Left,
        Down
    };

    public class Pump : PictureBox
    {
        public Pump()
        {
            this.Height = 80;
            this.Width = 80;
            this.BackColor = Color.Transparent;
        }
        [Category("Default"), Description("")]
        private PumpState m_State;
        public PumpState State
        {
            set { if (m_State != value) { m_State = value; this.Invalidate(); } }
            get { return this.m_State; }
        }
        [Category("Default"), Description("")]
        private PumpFlowDirection m_FlowDirection;
        public PumpFlowDirection FlowDirection
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 40;
            this.Height = 40;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Color color = Color.Yellow;
            double rot = 0.0;
            base.OnPaint(pe);
            Graphics g = pe.Graphics;

            if (m_State == PumpState.NotUsed)
            {
                color = Color.Gray;
            }
            if (m_State == PumpState.Ready)
            {
                color = Color.Yellow;
            }
            if (m_State == PumpState.Running)
            {
                color = Color.Lime;
            }
            if (m_State == PumpState.Error)
            {
                color = Color.Red;
            }

            if (m_FlowDirection == PumpFlowDirection.Right)
            {
                rot = 0.0;
            }

            if (m_FlowDirection == PumpFlowDirection.Down)
            {
                rot = 90.0;
            }
            if (m_FlowDirection == PumpFlowDirection.Left)
            {
                rot = 180.0;
            }
            if (m_FlowDirection == PumpFlowDirection.Up)
            {
                rot = 270.0;
            }
            int height = this.Height;
            int width = this.Width;

            Point pointCenter = new Point();
            pointCenter.X = height / 2;
            pointCenter.Y = width / 2;

            int center = height / 2;

            double radius = height / 2.0;
            double a = Math.Sqrt(Math.Pow(radius, 2.0) / 2.0);
            double y = radius - a;
            int y1 = Convert.ToInt32(y);
            int x1 = y1;
            int y2 = Convert.ToInt32(2.0 * a + y);
            int x2 = y1;
            int y3 = Convert.ToInt32(height / 2.0);
            int x3 = width;



            Point[] point = { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) };


            point[0] = FuncGeneral.RotatePoint(point[0], pointCenter, rot);
            point[1] = FuncGeneral.RotatePoint(point[1], pointCenter, rot);
            point[2] = FuncGeneral.RotatePoint(point[2], pointCenter, rot);

            SolidBrush brush = new SolidBrush(Color.White);
            int penWidth = 3;
            Pen pen = new Pen(Color.White, penWidth);
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