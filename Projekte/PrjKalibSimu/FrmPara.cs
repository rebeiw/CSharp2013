using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Helper;

namespace PrjKalibSimu
{
    public partial class FrmPara : Helper.FrmVorlageMenu
    {
        private ClsFormularManager m_glb_FormularManager;
        private ClsPLC m_glb_PLC;
        private ClsDataBinding m_glb_DataBinding;

        public FrmPara()
        {
            InitializeComponent();
            m_glb_FormularManager = ClsFormularManager.CreateInstance();
            m_glb_FormularManager.FormularAdd(this, this.Name.ToString());
            m_glb_PLC = ClsPLC.CreateInstance();
            m_glb_DataBinding = ClsDataBinding.CreateInstance();

        }

        private void FrmPara_Load(object sender, EventArgs e)
        {

        }
    }
}
