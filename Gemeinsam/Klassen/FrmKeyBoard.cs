using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;




namespace Helper
{
    public partial class FrmKeyBoard : Helper.FrmVorlageMenu
    {
        private ClsSingeltonParameter m_parameter;
        private ClsSingeltonFormularManager m_formularManager;//!<Objekt auf die Klasse FormularManager
        private ClsSingeltonLanguage m_language;

        /** 
         * \brief Konstruktor
         * 
         * Funktionsbeschreibung: Komponenten erzeugen und an den Formularmanager anmelden  
         */
        public FrmKeyBoard()
        {

            InitializeComponent();
            this.BtnBS.Click += new System.EventHandler(this.BtnBS_Click);
            this.BtnNumber0.Click += new System.EventHandler(this.KeyBoard_Click);
            this.BtnNumber1.Click += new System.EventHandler(this.KeyBoard_Click);
            this.BtnNumber2.Click += new System.EventHandler(this.KeyBoard_Click);
            this.BtnNumber3.Click += new System.EventHandler(this.KeyBoard_Click);
            this.BtnNumber4.Click += new System.EventHandler(this.KeyBoard_Click);
            this.BtnNumber5.Click += new System.EventHandler(this.KeyBoard_Click);
            this.BtnNumber6.Click += new System.EventHandler(this.KeyBoard_Click);
            this.BtnNumber7.Click += new System.EventHandler(this.KeyBoard_Click);
            this.BtnNumber8.Click += new System.EventHandler(this.KeyBoard_Click);
            this.BtnNumber9.Click += new System.EventHandler(this.KeyBoard_Click);
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
            this.m_language = ClsSingeltonLanguage.CreateInstance(this);
        }

        private void FrmKeyBoard_Load(object sender, System.EventArgs e)
        {

        }

        private void SendKey(object sender, string text)
        {
            Form frm = (Form)Application.OpenForms[this.m_parameter.ActualForm.Name];
            if (frm != null)
            {
                int iHandle = NativeWin32.FindWindow(null, this.m_parameter.ActualForm.Text);
                NativeWin32.SetForegroundWindow(iHandle);
                System.Windows.Forms.SendKeys.Send(text);
            }
        }

        private void KeyBoard_Click(object sender, EventArgs e)
        {
            CompBitButton btn = (CompBitButton)sender;
            this.SendKey(sender, btn.Caption);
        }

        private void BtnBS_Click(object sender, EventArgs e)
        {
            this.SendKey(sender, "{BS}");
        }
    }
}
