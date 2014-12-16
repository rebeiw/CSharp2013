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

    public enum BtnStyle
     {
        btg_Blanko = 0,
        btg_Benutzer,
        btg_Clipboard,
        btg_Diskette,
        btg_Esc,
        btg_Excel,
        btg_Exit,
        btg_Gruen,
        btg_Menue,
        btg_Ok,
        btg_Requery,
        btg_Run,
        btg_Stop,
        btg_UserNok,
        btg_UserOk,
        btg_Switchoff,
        btg_DeleteForm,
        btg_Error,
        btg_Fehler,
        btg_Freigabe,
        btg_Gelb,
        btg_Rot,
        btg_Weiss,
        btg_Cn,
        btg_De,
        btg_En,
        btg_It,
        btg_Nl,
        btg_Pl,
        btg_Pt,
        btg_Ru,
        btg_Se,
        btg_Sp,
        btg_Tu,
        btg_Fr,
        btg_ScreenShot,
        btg_Keyboard,
        btg_Para,
        btg_Service,
        btg_Blau,
        btg_Orange,
        btg_DunkelGruen,
        btg_New,
        btg_Folder,
        btg_PfeilOben,
        btg_PfeilUnten,
        btg_PfeilObenAktiv,
        btg_PfeilUntenAktiv,
        btg_Pfeillinks,
        btg_PfeilRechts,
        btg_Graph


}

    public class RotaBitButton : PictureBox
    {
        private List<Bitmap> Bitmaps;
        private Rectangle Rec,Rec1;
        private Pen Pen1;
        private bool FlagDrawRec;
        public RotaBitButton()
        {
            Bitmaps = new List<Bitmap>();
            Font fnt = new Font("Arial", 18f, System.Drawing.FontStyle.Bold);
            this.Width = 79;
            this.Height = 48;
            this.Font = fnt;
            FlagDrawRec = false;
            Rec1 = new Rectangle(0, 0, 79, 48);
            Rec = new Rectangle(0, 0, 79-1, 48-1);
            Pen1 = new Pen(Color.DarkBlue);
            this.FillButtonBitmaps();
            this.Picture_0 = BtnStyle.btg_Blanko;
            this.Picture_1 = BtnStyle.btg_Blanko;
            this.Picture_2 = BtnStyle.btg_Blanko;
            this.Picture_3 = BtnStyle.btg_Blanko;
            this.Picture_4 = BtnStyle.btg_Blanko;
            this.PictureNumber = 0;
            this.Caption = "";
            this.EnableMouseDown = false;

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
        private bool m_EnableMouseDown;
        public bool EnableMouseDown
        {
            set { m_EnableMouseDown = value;  }
            get { return this.m_EnableMouseDown; }
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
        private BtnStyle m_Picture_0;
        public BtnStyle Picture_0
        {
            set { if (m_Picture_0 != value) { m_Picture_0 = value; this.Invalidate(); } }
            get { return this.m_Picture_0; }
        }


        [Category("Default"), Description("")]
        private BtnStyle m_Picture_1;
        public BtnStyle Picture_1
        {
            set { if (m_Picture_1 != value) { m_Picture_1 = value; this.Invalidate(); } }
            get { return this.m_Picture_1; }
        }

        [Category("Default"), Description("")]
        private BtnStyle m_Picture_2;
        public BtnStyle Picture_2
        {
            set { if (m_Picture_2 != value) { m_Picture_2 = value; this.Invalidate(); } }
            get { return this.m_Picture_2; }
        }


        [Category("Default"), Description("")]
        private BtnStyle m_Picture_3;
        public BtnStyle Picture_3
        {
            set { if (m_Picture_3 != value) { m_Picture_3 = value; this.Invalidate(); } }
            get { return this.m_Picture_3; }
        }

        [Category("Default"), Description("")]
        private BtnStyle m_Picture_4;
        public BtnStyle Picture_4
        {
            set { if (m_Picture_4 != value) { m_Picture_4 = value; this.Invalidate(); } }
            get { return this.m_Picture_4; }
        }

        
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 79;
            this.Height = 48;
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

            if (this.EnableMouseDown && this.FlagDrawRec)
            {
                nr = (int)m_Picture_1;
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
           
            this.Bitmaps.Add(Bmp.btg_Blanko);
            this.Bitmaps.Add(Bmp.btg_benutzer);
            this.Bitmaps.Add(Bmp.btg_Clipboard);
            this.Bitmaps.Add(Bmp.btg_diskette);
            this.Bitmaps.Add(Bmp.btg_esc);
            this.Bitmaps.Add(Bmp.btg_Excel);
            this.Bitmaps.Add(Bmp.btg_exit);
            this.Bitmaps.Add(Bmp.btg_Gruen);
            this.Bitmaps.Add(Bmp.btg_menue);
            this.Bitmaps.Add(Bmp.btg_ok);
            this.Bitmaps.Add(Bmp.btg_Requery);
            this.Bitmaps.Add(Bmp.btg_Run);
            this.Bitmaps.Add(Bmp.btg_Stop);
            this.Bitmaps.Add(Bmp.btg_usernok);
            this.Bitmaps.Add(Bmp.btg_userok);
            this.Bitmaps.Add(Bmp.btg_switchoff);
            this.Bitmaps.Add(Bmp.btg_DeleteForm);
            this.Bitmaps.Add(Bmp.btg_Error);
            this.Bitmaps.Add(Bmp.btg_fehler);
            this.Bitmaps.Add(Bmp.btg_freigabe);
            this.Bitmaps.Add(Bmp.btg_Gelb);
            this.Bitmaps.Add(Bmp.btg_rot);
            this.Bitmaps.Add(Bmp.btg_Weis);
            this.Bitmaps.Add(Bmp.btg_cn);
            this.Bitmaps.Add(Bmp.btg_de);
            this.Bitmaps.Add(Bmp.btg_en);
            this.Bitmaps.Add(Bmp.btg_it);
            this.Bitmaps.Add(Bmp.btg_NL);
            this.Bitmaps.Add(Bmp.btg_pl);
            this.Bitmaps.Add(Bmp.btg_pt);
            this.Bitmaps.Add(Bmp.btg_ru);
            this.Bitmaps.Add(Bmp.btg_SE);
            this.Bitmaps.Add(Bmp.btg_sp);
            this.Bitmaps.Add(Bmp.btg_tu);
            this.Bitmaps.Add(Bmp.btg_fr);
            this.Bitmaps.Add(Bmp.btg_screenshot);
            this.Bitmaps.Add(Bmp.btg_keyboard);
            this.Bitmaps.Add(Bmp.btg_para);
            this.Bitmaps.Add(Bmp.btg_service);
            this.Bitmaps.Add(Bmp.btg_Blau);
            this.Bitmaps.Add(Bmp.btg_Orange);
            this.Bitmaps.Add(Bmp.btg_DarkGruen);
            this.Bitmaps.Add(Bmp.btg_new);
            this.Bitmaps.Add(Bmp.btg_folder);
            this.Bitmaps.Add(Bmp.btg_Pfeilob);
            this.Bitmaps.Add(Bmp.btg_Pfeilunt);
            this.Bitmaps.Add(Bmp.btg_Gruen_Pfeilob);
            this.Bitmaps.Add(Bmp.btg_Gruen_Pfeilunt);
            this.Bitmaps.Add(Bmp.btg_pfeilli);
            this.Bitmaps.Add(Bmp.btg_pfeilre);
            this.Bitmaps.Add(Bmp.btg_graph);

        }
    }
}