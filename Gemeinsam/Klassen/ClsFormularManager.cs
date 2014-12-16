using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Helper;


namespace Helper
{
    public class ClsFormularManager
    {


        private Hashtable m_Formulare;
        private static ClsFormularManager instance;
        public Form PrintScreen;

        public delegate void BTN_Click(object sender, EventArgs e);

        private ClsFormularManager()
        {
            m_Formulare = new Hashtable();
        }
        public static ClsFormularManager CreateInstance()
        {
            if (instance == null)
            {
                instance = new ClsFormularManager();
            }
            return instance;
        }
        public void SetFormPrintScreen(Form FormPrintScreen)
        {
            this.PrintScreen = FormPrintScreen;
        }

        public void FormularAdd(Control Form,string Formularname)
        {
            if (m_Formulare[Formularname] == null)
            {
                if (!m_Formulare.Contains(Formularname))
                {
                    m_Formulare.Add(Formularname, Form);
                }
            }
        }
        public void FormularShow(string Formularname)
        {
            if (m_Formulare[Formularname] != null)
            {
                Form frm = (Form)m_Formulare[Formularname];
                if (Formularname == "FRM_Menu")
                {
                    frm.Left = Cursor.Position.X - frm.Width;
                    frm.Top = Cursor.Position.Y;
                }
                if (Formularname == "FrmLanguage")
                {
                    Form frmMenu = (Form)m_Formulare["FRM_Menu"];
                    frm.Left = Cursor.Position.X - frm.Width;
                    frm.Top = frmMenu.Top;
                }
                
                frm.Show();
                frm.BringToFront();

            }
        }
        public void FormularCloseAll()
        {
            foreach (DictionaryEntry pair in m_Formulare)
            {
               Form frm = (Form)pair.Value;
               frm.Hide();
            }
        }
        public void SetUserRight()
        {
            foreach (DictionaryEntry pair in m_Formulare)
            {
                FrmVorlage frm = (FrmVorlage)pair.Value;
                frm.SetUserRight();
            }
        }
        public void SetLanguage()
        {
            foreach (DictionaryEntry pair in m_Formulare)
            {
                FrmVorlage frm = (FrmVorlage)pair.Value;
                frm.SetLanguage();
            }
        }
        public void AddBitButtonCloseAll()
        {
            FRM_Menu frm = (FRM_Menu)m_Formulare["FRM_Menu"];
            frm.AddBitButtonCloseAll();
        }


        public void AddButton(BtnStyle ButtonStyle, string Formularname)
        {
            FRM_Menu frm = (FRM_Menu)m_Formulare["FRM_Menu"];
            frm.AddBitButton(ButtonStyle, Formularname);
        }

        public void AddButton(BitButton BitButton, EventHandler ButtonEvent)
        {
//            BTN_Click btn_Click;
//            btn_Click = new BTN_Click(ButtonEvent);
//            BitButton += btn_Click;
        }
    }
}
