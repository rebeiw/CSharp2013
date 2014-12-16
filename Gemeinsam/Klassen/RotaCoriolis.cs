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


    public class RotaCoriolis : PictureBox
    {
        PointF m_Point;
        SizeF m_Size;
        Pen m_Pen;
        SolidBrush m_Brush;
        RectangleF m_Rec1;


        public RotaCoriolis()
        {
            this.Height =100;
            this.Width = 100;
            m_Point = new PointF();
            m_Size = new SizeF();
            m_Pen = new Pen(Color.White,3);
            m_Rec1 = new RectangleF();
            m_Brush = new SolidBrush(Color.DarkBlue);

        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            //g.SmoothingMode = SmoothingMode.AntiAlias;

            m_Point.X = 1.0F;
            m_Point.Y = 1.0F;
            m_Size.Height = this.Height-3.0F;
            m_Size.Width = this.Width - 3.0F;
            m_Rec1.Size=m_Size;
            m_Rec1.Location=m_Point;

            g.DrawEllipse(m_Pen, m_Rec1);

            m_Point.X = 3.0F;
            m_Point.Y = 3.0F;
            m_Size.Height = this.Height - 7.0F;
            m_Size.Width = this.Width - 7.0F;
            m_Rec1.Size = m_Size;
            m_Rec1.Location = m_Point;

            g.FillEllipse(m_Brush, m_Rec1);

            m_Point.X = this.Height / 6.0F;
            m_Point.Y = this.Width / 6.0F;

            m_Size.Height = this.Height - 2 * m_Point.X;
            m_Size.Width = this.Width - 2 * m_Point.Y;

            m_Rec1.Size = m_Size;
            m_Rec1.Location = m_Point;

            g.DrawEllipse(m_Pen, m_Rec1);




        }
    }

}