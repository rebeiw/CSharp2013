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
    public partial class FRM_Menu : FrmVorlage
    {
        ClsFormularManager frm_mng;
        List<RotaBitButton> BtnList;
        public FRM_Menu()
        {
            InitializeComponent();
            frm_mng = ClsFormularManager.CreateInstance();
            frm_mng.FormularAdd(this, this.Name.ToString());
            this.BtnList = new List<RotaBitButton>();
            this.ClrButtons();
        }
        private void BTN_Ende_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form2_Activated(object sender, EventArgs e)
        {
            this.BTN_User.PictureNumber = Convert.ToInt32(GlobalVar.Glb_Passwort_ok);
        }
        private void FRM_Menu_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BTN_User_Click(object sender, EventArgs e)
        {
            frm_mng.FormularShow("Frm_Passwort");
        }
        public void ClrButtons()
        {
            this.BtnList.Clear();
            this.BtnList.Add(this.BTN_Ende);
            this.BtnList.Add(this.BTN_User);
            this.BtnList.Add(this.BTN_Sprache);
            this.BtnList.Add(this.BTN_PrintScreen);
            this.BtnList.Add(this.BTN_KeyBoard);
            this.BtnList.Add(this.BTN_Quit);
            this.SetButtons();
        }
        private void SetButtons()
        {
            double anzSpalten = 0.0;
            anzSpalten = (double)((double)this.BtnList.Count-1.0) / 6.0;

            int dRound = (int)anzSpalten+1;
            int breite = (int)dRound * 89 + 10;
            this.GB_Menu.Width = breite;
            this.GB_Menu.Left=5;
            this.Width = breite + 26;
            int btnId=0;
            foreach (RotaBitButton bitBtn in BtnList)
            {
                double sp = anzSpalten - (int)((double)btnId / 6.0);
                int ze = btnId - (int)((double)btnId / 6.0) * 6;
                int possp = (int)sp * 89 + 10;
                int posze = ze * 54 + 15;
                bitBtn.Left = possp;
                bitBtn.Top = posze;
                btnId++;
            }

        }

        private void FRM_Menu_Load(object sender, EventArgs e)
        {

        }

        public override void SetLanguage()
        {
            base.SetLanguage();
            if (GlobalVar.Glb_Language == GlobalVar.Language.De)
                this.BTN_Sprache.Picture_0 = BtnStyle.btg_De;
            if (GlobalVar.Glb_Language == GlobalVar.Language.En)
                this.BTN_Sprache.Picture_0 = BtnStyle.btg_En;
            if (GlobalVar.Glb_Language == GlobalVar.Language.Fr)
                this.BTN_Sprache.Picture_0 = BtnStyle.btg_Fr;
            if (GlobalVar.Glb_Language == GlobalVar.Language.Sp)
                this.BTN_Sprache.Picture_0 = BtnStyle.btg_Sp;
            if (GlobalVar.Glb_Language == GlobalVar.Language.Ru)
                this.BTN_Sprache.Picture_0 = BtnStyle.btg_Ru;
        }


        private void bitButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BTN_Formular_Click(object sender, EventArgs e)
        {
            RotaBitButton btn = (RotaBitButton)sender;
            frm_mng.FormularShow(btn.Formular);
        }

        private void GB_Menu_Enter(object sender, EventArgs e)
        {

        }

        private void BTN_CloseAll_Click(object sender, EventArgs e)
        {
            frm_mng.FormularCloseAll();
        }

        public void AddBitButtonCloseAll()
        {
            RotaBitButton btn = new RotaBitButton();
            btn.Picture_0 = BtnStyle.btg_DeleteForm;
            btn.Click += new System.EventHandler(this.BTN_CloseAll_Click);

            this.BtnList.Add(btn);
            this.GB_Menu.Controls.Add(btn);
            this.SetButtons();

        }

        public void AddBitButton(BtnStyle ButtonStyle, string Formularname)
        {
            RotaBitButton btn = new RotaBitButton();
            btn.Picture_0 = ButtonStyle;
            btn.Formular = Formularname;
            btn.Click += new System.EventHandler(this.BTN_Formular_Click);

            this.BtnList.Add(btn);
            this.GB_Menu.Controls.Add(btn);
            this.SetButtons();
        }

        private void BTN_PrintScreen_Click(object sender, EventArgs e)
        {
            this.Close();
            Graphics graph = null;
            Bitmap bmp = new Bitmap(frm_mng.PrintScreen.Width, frm_mng.PrintScreen.Height);
            graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(frm_mng.PrintScreen.Left, frm_mng.PrintScreen.Top, 0, 0, bmp.Size);
            string Pfad = string.Empty;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = frm_mng.PrintScreen.Name + ".bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Pfad = saveFileDialog.FileName;
                bmp.Save(Pfad);
            }
            saveFileDialog.Dispose();
            bmp.Dispose();
            graph.Dispose();
        }

        private void BTN_Sprache_Click(object sender, EventArgs e)
        {
            this.Close();
            frm_mng.FormularShow("FrmLanguage");
        }

        private void BTN_KeyBoard_Click(object sender, EventArgs e)
        {
            this.Close();
            frm_mng.FormularShow("FrmKeyBoard");
        }
    }
}
