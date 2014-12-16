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
using System.Windows.Input;

namespace Helper
{

    public class Scale : PictureBox
    {
        public Scale()
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            float width = this.Width;
            float height = this.Height;
            float x_middle = width / 2.0f;
            float y_middle = height / 2.0f;

            float y_70 = 0.7F * this.Height;
            float y_90 = 0.9F * this.Height;
            float widthPipe = 0.1F * this.Width;
            PointF[] points = new PointF[]
            {
                new PointF(2.0F,2.0F),                              //P0
                new PointF(2.0F,y_70),                              //P1
                new PointF(x_middle-widthPipe,y_90),                //P2
                new PointF(x_middle-widthPipe,height-2.0F),         //P3
                new PointF(x_middle+widthPipe,height-2.0F),         //P4
                new PointF(x_middle+widthPipe,y_90),                //P5
                new PointF(width-2.0F,y_70),                        //P6
                new PointF(width-2.0F,2.0F)                         //P7
            };
           
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle recLedGradient = new Rectangle(0, 0, this.Width, this.Height);
            Brush brushGradient = new LinearGradientBrush(recLedGradient, Color.DarkGray, Color.White, 45);

            GraphicsPath gp = new GraphicsPath();

            gp.AddPolygon(points);

            int middleWidth=this.Width / 2;
            int middleHeight=this.Height / 2;

            PathGradientBrush pgb = new PathGradientBrush(gp);
            pgb.CenterColor = Color.White;
            Point centerPoint = new Point(middleWidth, middleHeight);
            pgb.CenterPoint = centerPoint;
            Color[] colors = new Color[] {Color.LightGray };
            pgb.SurroundColors = colors;
            pe.Graphics.FillRectangle(pgb, recLedGradient);

            pe.Graphics.DrawPolygon(new Pen(Color.Black,2), points);
        }
    }
}
