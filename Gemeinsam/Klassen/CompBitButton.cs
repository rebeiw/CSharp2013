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

    public enum CompBitButtonStyle
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

    public class CompBitButton : PictureBox
    {
        private List<Bitmap> m_Bitmaps;
        private Rectangle Rec,Rec1;
        private Pen m_Pen;
        private bool FlagDrawRec;

        /**
         * @brief      Konstruktor.
         * @details    Verbose description of method
         *             (or function) details.
         * @todo       Make it do something.
         * @bug        To be Microsoft Certified,
         */

        public CompBitButton()
        {
            this.m_Bitmaps = new List<Bitmap>();
            Font fnt = new Font("Arial", 18f, System.Drawing.FontStyle.Bold);
            this.Width = 79;
            this.Height = 48;
            this.Font = fnt;
            FlagDrawRec = false;
            Rec1 = new Rectangle(0, 0, 79, 48);
            Rec = new Rectangle(0, 0, 79-1, 48-1);
            this.m_Pen = new Pen(Color.DarkBlue);
            this.FillButtonBitmaps();
            this.Picture_0 = CompBitButtonStyle.btg_Blanko;
            this.Picture_1 = CompBitButtonStyle.btg_Blanko;
            this.Picture_2 = CompBitButtonStyle.btg_Blanko;
            this.Picture_3 = CompBitButtonStyle.btg_Blanko;
            this.Picture_4 = CompBitButtonStyle.btg_Blanko;
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
        private CompBitButtonStyle m_Picture_0;
        public CompBitButtonStyle Picture_0
        {
            set { if (m_Picture_0 != value) { m_Picture_0 = value; this.Invalidate(); } }
            get { return this.m_Picture_0; }
        }


        [Category("Default"), Description("")]
        private CompBitButtonStyle m_Picture_1;
        public CompBitButtonStyle Picture_1
        {
            set { if (m_Picture_1 != value) { m_Picture_1 = value; this.Invalidate(); } }
            get { return this.m_Picture_1; }
        }

        [Category("Default"), Description("")]
        private CompBitButtonStyle m_Picture_2;
        public CompBitButtonStyle Picture_2
        {
            set { if (m_Picture_2 != value) { m_Picture_2 = value; this.Invalidate(); } }
            get { return this.m_Picture_2; }
        }


        [Category("Default"), Description("")]
        private CompBitButtonStyle m_Picture_3;
        public CompBitButtonStyle Picture_3
        {
            set { if (m_Picture_3 != value) { m_Picture_3 = value; this.Invalidate(); } }
            get { return this.m_Picture_3; }
        }

        [Category("Default"), Description("")]
        private CompBitButtonStyle m_Picture_4;
        public CompBitButtonStyle Picture_4
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

            g.DrawImage(this.m_Bitmaps[nr], Rec1);


            Font f = new Font("Arial", 14f, FontStyle.Bold);
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;


            g.DrawString(this.Caption, f, Brushes.Black, Rec1, strFormat);

            if (this.FlagDrawRec)
            {
                g.DrawRectangle(this.m_Pen, Rec);
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
           
            this.m_Bitmaps.Add(Bmp.btg_Blanko);
            this.m_Bitmaps.Add(Bmp.btg_benutzer);
            this.m_Bitmaps.Add(Bmp.btg_Clipboard);
            this.m_Bitmaps.Add(Bmp.btg_diskette);
            this.m_Bitmaps.Add(Bmp.btg_esc);
            this.m_Bitmaps.Add(Bmp.btg_Excel);
            this.m_Bitmaps.Add(Bmp.btg_exit);
            this.m_Bitmaps.Add(Bmp.btg_Gruen);
            this.m_Bitmaps.Add(Bmp.btg_menue);
            this.m_Bitmaps.Add(Bmp.btg_ok);
            this.m_Bitmaps.Add(Bmp.btg_Requery);
            this.m_Bitmaps.Add(Bmp.btg_Run);
            this.m_Bitmaps.Add(Bmp.btg_Stop);
            this.m_Bitmaps.Add(Bmp.btg_usernok);
            this.m_Bitmaps.Add(Bmp.btg_userok);
            this.m_Bitmaps.Add(Bmp.btg_switchoff);
            this.m_Bitmaps.Add(Bmp.btg_DeleteForm);
            this.m_Bitmaps.Add(Bmp.btg_Error);
            this.m_Bitmaps.Add(Bmp.btg_fehler);
            this.m_Bitmaps.Add(Bmp.btg_freigabe);
            this.m_Bitmaps.Add(Bmp.btg_Gelb);
            this.m_Bitmaps.Add(Bmp.btg_rot);
            this.m_Bitmaps.Add(Bmp.btg_Weis);
            this.m_Bitmaps.Add(Bmp.btg_cn);
            this.m_Bitmaps.Add(Bmp.btg_de);
            this.m_Bitmaps.Add(Bmp.btg_en);
            this.m_Bitmaps.Add(Bmp.btg_it);
            this.m_Bitmaps.Add(Bmp.btg_NL);
            this.m_Bitmaps.Add(Bmp.btg_pl);
            this.m_Bitmaps.Add(Bmp.btg_pt);
            this.m_Bitmaps.Add(Bmp.btg_ru);
            this.m_Bitmaps.Add(Bmp.btg_SE);
            this.m_Bitmaps.Add(Bmp.btg_sp);
            this.m_Bitmaps.Add(Bmp.btg_tu);
            this.m_Bitmaps.Add(Bmp.btg_fr);
            this.m_Bitmaps.Add(Bmp.btg_screenshot);
            this.m_Bitmaps.Add(Bmp.btg_keyboard);
            this.m_Bitmaps.Add(Bmp.btg_para);
            this.m_Bitmaps.Add(Bmp.btg_service);
            this.m_Bitmaps.Add(Bmp.btg_Blau);
            this.m_Bitmaps.Add(Bmp.btg_Orange);
            this.m_Bitmaps.Add(Bmp.btg_DarkGruen);
            this.m_Bitmaps.Add(Bmp.btg_new);
            this.m_Bitmaps.Add(Bmp.btg_folder);
            this.m_Bitmaps.Add(Bmp.btg_Pfeilob);
            this.m_Bitmaps.Add(Bmp.btg_Pfeilunt);
            this.m_Bitmaps.Add(Bmp.btg_Gruen_Pfeilob);
            this.m_Bitmaps.Add(Bmp.btg_Gruen_Pfeilunt);
            this.m_Bitmaps.Add(Bmp.btg_pfeilli);
            this.m_Bitmaps.Add(Bmp.btg_pfeilre);
            this.m_Bitmaps.Add(Bmp.btg_graph);

        }
    }
}