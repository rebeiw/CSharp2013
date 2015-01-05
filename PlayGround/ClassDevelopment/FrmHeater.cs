using System;
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

        private ClsSingeltonParameter m_parameter;
        private ClsSingeltonDataBinding m_dataBinding;
        private ClsSingeltonLanguage m_language;
        private ClsSingeltonUserManagement m_userManagement;

        public FrmHeater()
        {
            InitializeComponent();
            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance(this, this.Name.ToString());
            this.m_language = ClsSingeltonLanguage.CreateInstance(this);
            this.m_dataBinding = ClsSingeltonDataBinding.CreateInstance();
            this.m_userManagement = ClsSingeltonUserManagement.CreateInstance(this);
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_dataBinding.AddList(this, "Txt01", "Text", "DB50.P1_Qmin_1");
            this.m_dataBinding.AddList(this, "Txt02", "Text", "DB50.P1_Qmin_3");
            this.m_dataBinding.AddList(this, "Txt03", "Text", "DB50.P1_Qmin_2");

            this.m_dataBinding.AddList(this, "Led01", "State", "DB54.Bit20");

            this.m_dataBinding.AddList(this, "Vnt01", "State", "DB52.Valve04");

            this.m_dataBinding.AddList(this, "Pip01", "Flow", "DB53.Bit04");


        }

        private void FrmHaeter_Load(object sender, EventArgs e)
        {
            this.m_userManagement.SetEnable(this.textBox1, ClsSingeltonUserManagement.UserRight.Logoff,true);

        }

        private void FrmHeater_Activated(object sender, EventArgs e)
        {
            this.m_parameter.ActualForm = this;
        }

        private void bitButtonSmall1_Click(object sender, EventArgs e)
        {
            this.m_userManagement.ResetEnable(this.textBox1, ClsSingeltonUserManagement.UserRight.Logoff, true);

        }

        private void bitButtonSmall2_Click(object sender, EventArgs e)
        {
            this.m_userManagement.SetEnable(this.textBox1, ClsSingeltonUserManagement.UserRight.Logoff, true);
        }
    }
}
