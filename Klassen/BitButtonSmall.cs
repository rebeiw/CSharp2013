using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Helper
{

    public enum BtnStyleSmall
     {
        btk_Blanko = 0,
        btk_Copy,
        btk_Delete,
        btk_Fileshare,
        btk_Gelb,
        btk_Gruen,
        btk_New,
        btk_Ok,
        btk_OpenFileSmall,
        btk_PfeilLinks,
        btk_PfeilRechts,
        btk_Refresh,
        btk_Ring,
        btk_Tk

}

    public class BitButtonSmall : PictureBox
    {
        private List<Bitmap> Bitmaps;
        private Rectangle Rec,Rec1;
        private Pen Pen1;
        private bool FlagDrawRec;
        public BitButtonSmall()
        {
            Bitmaps = new List<Bitmap>();
            Font fnt = new Font("Arial", 18f, System.Drawing.FontStyle.Bold);
            this.Width = 40;
            this.Height = 24;
            this.Font = fnt;
            FlagDrawRec = false;
            Rec1 = new Rectangle(0, 0, 40, 24);
            Rec = new Rectangle(0, 0, 40-1, 24-1);
            Pen1 = new Pen(Color.DarkBlue);
            this.FillButtonBitmaps();
            this.Picture_0 = BtnStyleSmall.btk_Blanko;
            this.Picture_1 = BtnStyleSmall.btk_Blanko;
            this.Picture_2 = BtnStyleSmall.btk_Blanko;
            this.Picture_3 = BtnStyleSmall.btk_Blanko;
            this.Picture_4 = BtnStyleSmall.btk_Blanko;
            this.PictureNumber = 0;
            this.Caption = "";

        }

        [Category("Default"), Description("")]
        private string m_Formular;
        public string Formular
        {
            set { if (m_Formular != value) { m_Formular = value; this.Invalidate(); } }
            get { return this.m_Formular; }
        }

        [Category("Default"), Description("")]
        private string m_Caption;
        public string Caption
        {
            set { if (m_Caption != value) { m_Caption = value; this.Invalidate(); } }
            get { return this.m_Caption; }
        }

        [Category("Default"), Description("")]
        private string m_Symbol;
        public string Symbol
        {
            set { m_Symbol = value; }
            get { return this.m_Symbol; }
        }

        [Category("Default"), Description("")]
        private int m_PictureNumber;
        public int PictureNumber
        {
            set { if (m_PictureNumber != value) { m_PictureNumber = value; this.Invalidate(); } }
            get { return this.m_PictureNumber; }
        }

        [Category("Default"), Description("")]
        private BtnStyleSmall m_Picture_0;
        public BtnStyleSmall Picture_0
        {
            set { if (m_Picture_0 != value) { m_Picture_0 = value; this.Invalidate(); } }
            get { return this.m_Picture_0; }
        }


        [Category("Default"), Description("")]
        private BtnStyleSmall m_Picture_1;
        public BtnStyleSmall Picture_1
        {
            set { if (m_Picture_1 != value) { m_Picture_1 = value; this.Invalidate(); } }
            get { return this.m_Picture_1; }
        }

        [Category("Default"), Description("")]
        private BtnStyleSmall m_Picture_2;
        public BtnStyleSmall Picture_2
        {
            set { if (m_Picture_2 != value) { m_Picture_2 = value; this.Invalidate(); } }
            get { return this.m_Picture_2; }
        }


        [Category("Default"), Description("")]
        private BtnStyleSmall m_Picture_3;
        public BtnStyleSmall Picture_3
        {
            set { if (m_Picture_3 != value) { m_Picture_3 = value; this.Invalidate(); } }
            get { return this.m_Picture_3; }
        }

        [Category("Default"), Description("")]
        private BtnStyleSmall m_Picture_4;
        public BtnStyleSmall Picture_4
        {
            set { if (m_Picture_4 != value) { m_Picture_4 = value; this.Invalidate(); } }
            get { return this.m_Picture_4; }
        }

        
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 40;
            this.Height = 24;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            int nr = 0;

            if (this.PictureNumber < 0)
            {
                this.PictureNumber = 0;
                this.m_PictureNumber = 0;
            }
            if (this.PictureNumber > 4)
            {
                this.PictureNumber = 4;
                this.m_PictureNumber = 4;
            }

            if (this.m_PictureNumber == 0)
            {
                nr = (int)m_Picture_0;
            }
            if (this.m_PictureNumber == 1)
            {
                nr = (int)m_Picture_1;
            }
            if (this.m_PictureNumber == 2)
            {
                nr = (int)m_Picture_2;
            }
            if (this.m_PictureNumber == 3)
            {
                nr = (int)m_Picture_3;
            }
            if (this.m_PictureNumber == 4)
            {
                nr = (int)m_Picture_4;
            }
            g.DrawImage(this.Bitmaps[nr], Rec1);


            Font f = new Font("Arial", 14f, FontStyle.Bold);
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;


            g.DrawString(this.Caption, f, Brushes.Black, Rec1, strFormat);

            if (this.FlagDrawRec)
            {
                g.DrawRectangle(Pen1, Rec);
            }



        }
        protected override void OnMouseDown(MouseEventArgs pe)
        {
            base.OnMouseDown(pe);
            this.FlagDrawRec = true;
            this.Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs pe)
        {
            base.OnMouseUp(pe);
            this.FlagDrawRec = false;
            this.Invalidate();
        }

        private void FillButtonBitmaps()
        {

            this.Bitmaps.Add(Bmp.Btk_Blanko);
            this.Bitmaps.Add(Bmp.Btk_Copy);
            this.Bitmaps.Add(Bmp.Btk_Delete);
            this.Bitmaps.Add(Bmp.Btk_Fileshare);
            this.Bitmaps.Add(Bmp.btk_Gelb);
            this.Bitmaps.Add(Bmp.btk_Gruen);
            this.Bitmaps.Add(Bmp.Btk_New);
            this.Bitmaps.Add(Bmp.btk_ok);
            this.Bitmaps.Add(Bmp.Btk_OpenFileSmall);
            this.Bitmaps.Add(Bmp.Btk_PfeilLinks);
            this.Bitmaps.Add(Bmp.Btk_PfeilRechts);
            this.Bitmaps.Add(Bmp.Btk_Refresh);
            this.Bitmaps.Add(Bmp.btk_ring);
            this.Bitmaps.Add(Bmp.btk_tl);

        }
    }
}