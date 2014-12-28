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
    public partial class FrmMenu : FrmVorlage
    {
        ClsSingeltonFormularManager m_formularManager;
        List<CompBitButton> m_ButtonList;
        public FrmMenu()
        {
            InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance();
            this.m_formularManager.FormularAdd(this, this.Name.ToString());
            this.m_ButtonList = new List<CompBitButton>();
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
            m_formularManager.FormularShow("FrmPasswort");
        }
        public void ClrButtons()
        {
            this.m_ButtonList.Clear();
            this.m_ButtonList.Add(this.BTN_Ende);
            this.m_ButtonList.Add(this.BTN_User);
            this.m_ButtonList.Add(this.BTN_Sprache);
            this.m_ButtonList.Add(this.BTN_PrintScreen);
            this.m_ButtonList.Add(this.BTN_KeyBoard);
            this.m_ButtonList.Add(this.BTN_Quit);
            this.SetButtons();
        }
        private void SetButtons()
        {
            double anzSpalten = 0.0;
            anzSpalten = (double)((double)this.m_ButtonList.Count-1.0) / 6.0;

            int dRound = (int)anzSpalten+1;
            int breite = (int)dRound * 89 + 10;
            this.GB_Menu.Width = breite;
            this.GB_Menu.Left=5;
            this.Width = breite + 26;
            int btnId=0;
            foreach (CompBitButton bitBtn in m_ButtonList)
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
                this.BTN_Sprache.Picture_0 = CompBitButtonStyle.btg_De;
            if (GlobalVar.Glb_Language == GlobalVar.Language.En)
                this.BTN_Sprache.Picture_0 = CompBitButtonStyle.btg_En;
            if (GlobalVar.Glb_Language == GlobalVar.Language.Fr)
                this.BTN_Sprache.Picture_0 = CompBitButtonStyle.btg_Fr;
            if (GlobalVar.Glb_Language == GlobalVar.Language.Sp)
                this.BTN_Sprache.Picture_0 = CompBitButtonStyle.btg_Sp;
            if (GlobalVar.Glb_Language == GlobalVar.Language.Ru)
                this.BTN_Sprache.Picture_0 = CompBitButtonStyle.btg_Ru;
        }


        private void bitButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BTN_Formular_Click(object sender, EventArgs e)
        {
            CompBitButton btn = (CompBitButton)sender;
            this.m_formularManager.FormularShow(btn.Formular);
        }

        private void GB_Menu_Enter(object sender, EventArgs e)
        {

        }

        private void BTN_CloseAll_Click(object sender, EventArgs e)
        {
            this.m_formularManager.FormularCloseAll();
        }

        public void AddBitButtonCloseAll()
        {
            CompBitButton btn = new CompBitButton();
            btn.Picture_0 = CompBitButtonStyle.btg_DeleteForm;
            btn.Click += new System.EventHandler(this.BTN_CloseAll_Click);

            this.m_ButtonList.Add(btn);
            this.GB_Menu.Controls.Add(btn);
            this.SetButtons();
        }

        public void AddBitButton(CompBitButtonStyle ButtonStyle, string Formularname)
        {
            CompBitButton btn = new CompBitButton();
            btn.Picture_0 = ButtonStyle;
            btn.Formular = Formularname;
            btn.Click += new System.EventHandler(this.BTN_Formular_Click);

            this.m_ButtonList.Add(btn);
            this.GB_Menu.Controls.Add(btn);
            this.SetButtons();
        }

        private void BTN_PrintScreen_Click(object sender, EventArgs e)
        {
            this.Close();
            Graphics graph = null;
            Bitmap bmp = new Bitmap(this.m_formularManager.m_printScreen.Width, this.m_formularManager.m_printScreen.Height);
            graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(this.m_formularManager.m_printScreen.Left, this.m_formularManager.m_printScreen.Top, 0, 0, bmp.Size);
            string path = string.Empty;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = m_formularManager.m_printScreen.Name + ".bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                bmp.Save(path);
            }
            saveFileDialog.Dispose();
            bmp.Dispose();
            graph.Dispose();
        }

        private void BTN_Sprache_Click(object sender, EventArgs e)
        {
            this.Close();
            m_formularManager.FormularShow("FrmLanguage");
        }

        private void BTN_KeyBoard_Click(object sender, EventArgs e)
        {
            this.Close();
            m_formularManager.FormularShow("FrmKeyBoard");
        }
    }
}
