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
        FrmHeater FrmHeater;

        public FrmMain()
        {


            this.m_parameter = ClsSingeltonParameter.CreateInstance();

            FuncGeneral.Start();
            FrmHeater = new FrmHeater();

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

            this.m_plc = ClsSingeltonPlc.CreateInstance(this.m_plcPara);
            this.m_language = ClsSingeltonLanguage.CreateInstance(this);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowClose = true;
            this.m_plc.AddList("DB50.DBD0", "P1_Qmin_1", "REAL", "Pumpen:Pumpendruck 1[EHbar]");
            this.m_plc.AddList("DB50.DBD4", "P1_Qmin_2", "REAL", "Pumpen:Pumpendruck 2[EHbar]");
            this.m_plc.AddList("DB50.DBD8", "P1_Qmin_3", "REAL", "Pumpen:Pumpendruck 3[EHbar]");
            this.m_plc.AddList("DB50.DBD12", "P1_Qmin_4", "REAL", "Pumpen:Pumpendruck 4[EHbar]");
            this.m_plc.AddList("DB50.DBD16", "P1_Qmin_5", "REAL", "Pumpen:Pumpendruck 5[EHbar]");

            this.m_plc.AddList("DB51.DBD0", "P1_Qmin_1", "REAL", "Pumpen:Pumpendruck 1[EHbar]");
            this.m_plc.AddList("DB51.DBD4", "P1_Qmin_2", "REAL", "Pumpen:Pumpendruck 2[EHbar]");
            this.m_plc.AddList("DB51.DBD8", "P1_Qmin_3", "REAL", "Pumpen:Pumpendruck 3[EHbar]");
            this.m_plc.AddList("DB51.DBD12", "P1_Qmin_4", "REAL", "Pumpen:Pumpendruck 4[EHbar]");
            this.m_plc.AddList("DB51.DBD16", "P1_Qmin_5", "REAL", "Pumpen:Pumpendruck 5[EHbar]");

            this.m_plc.AddList("DB52.DBW0", "Valve01", "INT", "Schieber:Status 1");
            this.m_plc.AddList("DB52.DBW2", "Valve02", "INT", "Schieber:Status 2");
            this.m_plc.AddList("DB52.DBW4", "Valve03", "INT", "Schieber:Status 3");
            this.m_plc.AddList("DB52.DBW6", "Valve04", "INT", "Schieber:Status 4");
            this.m_plc.AddList("DB52.DBW8", "Valve05", "INT", "Schieber:Status 5");

            this.m_plc.AddList("DB53.DBX0.0", "Bit01", "BOOL", "");
            this.m_plc.AddList("DB53.DBX0.1", "Bit01", "BOOL", "");
            this.m_plc.AddList("DB53.DBX0.2", "Bit02", "BOOL", "");
            this.m_plc.AddList("DB53.DBX0.3", "Bit03", "BOOL", "");
            this.m_plc.AddList("DB53.DBX0.4", "Bit04", "BOOL", "");
            this.m_plc.AddList("DB53.DBX0.5", "Bit05", "BOOL", "");
            this.m_plc.AddList("DB53.DBX0.6", "Bit06", "BOOL", "");
            this.m_plc.AddList("DB53.DBX0.7", "Bit07", "BOOL", "");

            this.m_plc.AddList("DB53.DBX1.0", "Bit08", "BOOL", "");
            this.m_plc.AddList("DB53.DBX1.1", "Bit09", "BOOL", "");
            this.m_plc.AddList("DB53.DBX1.2", "Bit10", "BOOL", "");
            this.m_plc.AddList("DB53.DBX1.3", "Bit11", "BOOL", "");
            this.m_plc.AddList("DB53.DBX1.4", "Bit12", "BOOL", "");
            this.m_plc.AddList("DB53.DBX1.5", "Bit13", "BOOL", "");
            this.m_plc.AddList("DB53.DBX1.6", "Bit14", "BOOL", "");
            this.m_plc.AddList("DB53.DBX1.7", "Bit15", "BOOL", "");

            this.m_plc.AddList("DB53.DBX2.0", "Bit16", "BOOL", "");
            this.m_plc.AddList("DB53.DBX2.1", "Bit17", "BOOL", "");
            this.m_plc.AddList("DB53.DBX2.2", "Bit18", "BOOL", "");
            this.m_plc.AddList("DB53.DBX2.3", "Bit29", "BOOL", "");
            this.m_plc.AddList("DB53.DBX2.4", "Bit20", "BOOL", "");
            this.m_plc.AddList("DB53.DBX2.5", "Bit21", "BOOL", "");
            this.m_plc.AddList("DB53.DBX2.6", "Bit22", "BOOL", "");
            this.m_plc.AddList("DB53.DBX2.7", "Bit23", "BOOL", "");

            this.m_plc.AddList("DB53.DBX3.0", "Bit24", "BOOL", "");
            this.m_plc.AddList("DB53.DBX3.1", "Bit25", "BOOL", "");
            this.m_plc.AddList("DB53.DBX3.2", "Bit26", "BOOL", "");
            this.m_plc.AddList("DB53.DBX3.3", "Bit27", "BOOL", "");
            this.m_plc.AddList("DB53.DBX3.4", "Bit28", "BOOL", "");
            this.m_plc.AddList("DB53.DBX3.5", "Bit29", "BOOL", "");
            this.m_plc.AddList("DB53.DBX3.6", "Bit30", "BOOL", "");
            this.m_plc.AddList("DB53.DBX3.7", "Bit31", "BOOL", "");

            this.m_plc.AddList("DB54.DBX0.0", "Bit01", "BOOL", "");
            this.m_plc.AddList("DB54.DBX0.1", "Bit01", "BOOL", "");
            this.m_plc.AddList("DB54.DBX0.2", "Bit02", "BOOL", "");
            this.m_plc.AddList("DB54.DBX0.3", "Bit03", "BOOL", "");
            this.m_plc.AddList("DB54.DBX0.4", "Bit04", "BOOL", "");
            this.m_plc.AddList("DB54.DBX0.5", "Bit05", "BOOL", "");
            this.m_plc.AddList("DB54.DBX0.6", "Bit06", "BOOL", "");
            this.m_plc.AddList("DB54.DBX0.7", "Bit07", "BOOL", "");

            this.m_plc.AddList("DB54.DBX1.0", "Bit08", "BOOL", "");
            this.m_plc.AddList("DB54.DBX1.1", "Bit09", "BOOL", "");
            this.m_plc.AddList("DB54.DBX1.2", "Bit10", "BOOL", "");
            this.m_plc.AddList("DB54.DBX1.3", "Bit11", "BOOL", "");
            this.m_plc.AddList("DB54.DBX1.4", "Bit12", "BOOL", "");
            this.m_plc.AddList("DB54.DBX1.5", "Bit13", "BOOL", "");
            this.m_plc.AddList("DB54.DBX1.6", "Bit14", "BOOL", "");
            this.m_plc.AddList("DB54.DBX1.7", "Bit15", "BOOL", "");

            this.m_plc.AddList("DB54.DBX2.0", "Bit16", "BOOL", "");
            this.m_plc.AddList("DB54.DBX2.1", "Bit17", "BOOL", "");
            this.m_plc.AddList("DB54.DBX2.2", "Bit18", "BOOL", "");
            this.m_plc.AddList("DB54.DBX2.3", "Bit29", "BOOL", "");
            this.m_plc.AddList("DB54.DBX2.4", "Bit20", "BOOL", "");
            this.m_plc.AddList("DB54.DBX2.5", "Bit21", "BOOL", "");
            this.m_plc.AddList("DB54.DBX2.6", "Bit22", "BOOL", "");
            this.m_plc.AddList("DB54.DBX2.7", "Bit23", "BOOL", "");

            this.m_plc.AddList("DB54.DBX3.0", "Bit24", "BOOL", "");
            this.m_plc.AddList("DB54.DBX3.1", "Bit25", "BOOL", "");
            this.m_plc.AddList("DB54.DBX3.2", "Bit26", "BOOL", "");
            this.m_plc.AddList("DB54.DBX3.3", "Bit27", "BOOL", "");
            this.m_plc.AddList("DB54.DBX3.4", "Bit28", "BOOL", "");
            this.m_plc.AddList("DB54.DBX3.5", "Bit29", "BOOL", "");
            this.m_plc.AddList("DB54.DBX3.6", "Bit30", "BOOL", "");
            this.m_plc.AddList("DB54.DBX3.7", "Bit31", "BOOL", "");
            
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
            this.m_userManagement.SetUserRight();


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
    }
}
