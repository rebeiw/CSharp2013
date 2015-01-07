using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;


namespace ClassDevelopment
{
    public partial class FrmMain : Helper.FrmVorlageMenu
    {

        private ClsSingeltonVariablesCollecter m_varCollect;
        private ClsSingeltonUserManagement m_userManagement;
        private ClsSingeltonDataBinding m_dataBinding;
        private ClsSingeltonLanguage m_language;
        private ClsSingeltonPlc m_plc;
        private ClsSingeltonPlcParameter m_plcPara;
        private ClsSingeltonFormularManager m_formularManager;
        private ClsSingeltonParameter m_parameter;
        private FrmHeater FrmHeater;
        private FrmParameter FrmParameter;
        public FrmMain()
        {


            this.m_parameter = ClsSingeltonParameter.CreateInstance();

            FuncGeneral.Start();
            FrmHeater = new FrmHeater();
            FrmParameter = new FrmParameter();

            this.InitializeComponent();
            this.Width = 1024;
            this.Height = 768;

            this.m_formularManager = ClsSingeltonFormularManager.CreateInstance();
            this.m_userManagement = ClsSingeltonUserManagement.CreateInstance(this);
            this.m_varCollect = ClsSingeltonVariablesCollecter.CreateInstance();
            this.m_dataBinding = ClsSingeltonDataBinding.CreateInstance();
            this.m_plcPara.ProgBar = this.BarPlcProgress;
            this.m_plcPara.LblStatus = this.lblState;
            this.m_plcPara.BtnStart = this.BtnStartPlc;
            this.m_plcPara.BtnStopp = this.BtnStoppPlc;
            this.m_plcPara.IP = this.m_parameter.PlcIp;
            this.m_plcPara.Rack = this.m_parameter.PlcRack;
            this.m_plcPara.Slot = this.m_parameter.PlcSlot;
            this.m_plcPara.LblPlc = this.LblPlcNoConnect;

            this.m_plc = ClsSingeltonPlc.CreateInstance(this.m_plcPara);
            this.m_language = ClsSingeltonLanguage.CreateInstance(this);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowClose = true;
            
            m_dataBinding.AddList(this, "Txt01", "Text", "DB50.P1_Qmin_1");
            m_dataBinding.AddList(this, "Txt02", "Text", "DB50.P1_Qmin_2");
            m_dataBinding.AddList(this, "Txt03", "Text", "DB50.P1_Qmin_3");
            m_dataBinding.AddList(this, "Txt04", "Text", "DB50.P1_Qmin_4");
            m_dataBinding.AddList(this, "Txt05", "Text", "DB50.P1_Qmin_5");
            m_dataBinding.AddList(this, "Txt06", "Text", "DB50.P1_Qmin_1");
            m_dataBinding.AddList(this, "Txt07", "Text", "DB50.P1_Qmin_2");
            m_dataBinding.AddList(this, "Txt08", "Text", "DB50.P1_Qmin_3");
            m_dataBinding.AddList(this, "Txt09", "Text", "DB50.P1_Qmin_4");
            m_dataBinding.AddList(this, "Txt10", "Text", "DB50.P1_Qmin_5");
            m_dataBinding.AddList(this, "Txt11", "Text", "DB50.P1_Qmin_1");
            m_dataBinding.AddList(this, "Txt12", "Text", "DB50.P1_Qmin_2");
            m_dataBinding.AddList(this, "Txt13", "Text", "DB50.P1_Qmin_3");
            m_dataBinding.AddList(this, "Txt14", "Text", "DB50.P1_Qmin_4");
            m_dataBinding.AddList(this, "Txt15", "Text", "DB50.P1_Qmin_5");
            m_dataBinding.AddList(this, "Txt16", "Text", "DB51.P1_Qmin_1");
            m_dataBinding.AddList(this, "Txt17", "Text", "DB51.P1_Qmin_2");
            m_dataBinding.AddList(this, "Txt18", "Text", "DB51.P1_Qmin_3");
            m_dataBinding.AddList(this, "Txt19", "Text", "DB51.P1_Qmin_4");
            m_dataBinding.AddList(this, "Txt20", "Text", "DB51.P1_Qmin_5");
            m_dataBinding.AddList(this, "Txt21", "Text", "DB50.P1_Qmin_1");

            m_dataBinding.AddList(this, "Vnt01", "State", "DB52.Valve01");
            m_dataBinding.AddList(this, "Vnt02", "State", "DB52.Valve02");
            m_dataBinding.AddList(this, "Vnt03", "State", "DB52.Valve03");
            m_dataBinding.AddList(this, "Vnt04", "State", "DB52.Valve04");
            m_dataBinding.AddList(this, "Vnt05", "State", "DB52.Valve05");
            m_dataBinding.AddList(this, "Vnt06", "State", "DB52.Valve01");
            m_dataBinding.AddList(this, "Vnt07", "State", "DB52.Valve02");
            m_dataBinding.AddList(this, "Vnt08", "State", "DB52.Valve03");
            m_dataBinding.AddList(this, "Vnt09", "State", "DB52.Valve04");
            m_dataBinding.AddList(this, "Vnt10", "State", "DB52.Valve05");
            m_dataBinding.AddList(this, "Vnt11", "State", "DB52.Valve01");

            m_dataBinding.AddList(this, "Pip01", "Flow", "DB53.Bit01");
            m_dataBinding.AddList(this, "Pip02", "Flow", "DB53.Bit02");
            m_dataBinding.AddList(this, "Pip03", "Flow", "DB53.Bit03");
            m_dataBinding.AddList(this, "Pip04", "Flow", "DB53.Bit04");
            m_dataBinding.AddList(this, "Pip05", "Flow", "DB53.Bit05");
            m_dataBinding.AddList(this, "Pip06", "Flow", "DB53.Bit06");
            m_dataBinding.AddList(this, "Pip07", "Flow", "DB53.Bit01");
            m_dataBinding.AddList(this, "Pip08", "Flow", "DB53.Bit08");
            m_dataBinding.AddList(this, "Pip09", "Flow", "DB53.Bit09");
            m_dataBinding.AddList(this, "Pip10", "Flow", "DB53.Bit10");
            m_dataBinding.AddList(this, "Pip11", "Flow", "DB53.Bit11");
            m_dataBinding.AddList(this, "Pip12", "Flow", "DB53.Bit12");
            m_dataBinding.AddList(this, "Pip13", "Flow", "DB53.Bit13");
            m_dataBinding.AddList(this, "Pip14", "Flow", "DB53.Bit14");
            m_dataBinding.AddList(this, "Pip15", "Flow", "DB53.Bit15");

            m_dataBinding.AddList(this, "Led01", "State", "DB54.Bit20");
            m_dataBinding.AddList(this, "Led02", "State", "DB54.Bit23");
            m_dataBinding.AddList(this, "Led03", "State", "DB54.Bit24");
            m_dataBinding.AddList(this, "Led04", "State", "DB54.Bit25");
            m_dataBinding.AddList(this, "Led05", "State", "DB54.Bit27");
            m_dataBinding.AddList(this, "Led06", "State", "DB54.Bit29");



            this.m_language.SetLanguage();
            this.m_formularManager.SetLanguage();
            this.m_userManagement.SetUserRight();
            this.m_formularManager.SetUserRight();


            FuncGeneral.CentreObject(this.LblPlcNoConnect,this.GbxOutput);


        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.m_plc.StoppRead();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Dispose();
        }

        private void bitButton2_Click(object sender, EventArgs e)
        {
            this.ShowMenu();
        }

        private void BtnStartPlc_Click(object sender, EventArgs e)
        {
            this.m_plc.StartRead();
        }

        private void BtnStoppPlc_Click(object sender, EventArgs e)
        {
            this.m_plc.StoppRead();
        }

        private void compBitButton1_Click(object sender, EventArgs e)
        {
            this.m_formularManager.FormularShow("FrmHeater");
        }

        private void compBitButton2_Click(object sender, EventArgs e)
        {
            this.BtnClose_Click(sender, e);
        }

        private void FrmMain_Activated(object sender, EventArgs e)
        {
            this.m_parameter.ActualForm = this;
        }

        private void BtnPara_Click(object sender, EventArgs e)
        {
            this.m_formularManager.FormularShow("FrmParameter");

        }
    }
}
