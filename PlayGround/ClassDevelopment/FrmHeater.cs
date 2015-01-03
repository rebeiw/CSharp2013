﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
namespace ClassDevelopment
{
    public partial class FrmHeater : Helper.FrmVorlageMenu
    {
        private ClsSingeltonFormularManager m_formularManager;//!<Objekt auf die erzeugte Instanz


        private ClsSingeltonDataBinding m_dataBinding;
        private ClsSingeltonLanguage m_language;

        public FrmHeater()
        {
            InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
            this.m_language = ClsSingeltonLanguage.CreateInstance();
            this.m_language.AddAllComponents(this);
            this.m_dataBinding = ClsSingeltonDataBinding.CreateInstance();
            m_dataBinding.AddList(this, "Txt01", "Text", "DB50.P1_Qmin_1");

        }

        private void FrmHaeter_Load(object sender, EventArgs e)
        {

        }
    }
}