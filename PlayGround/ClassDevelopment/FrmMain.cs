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
        private ClsSingeltonVariablesCollecter Glb_VarCollect;
        private ClsSingeltonVariablesCollecter Glb_VarCollect1;

        private ClsSingeltonDataBinding Glb_DataBinding;

        private ClsSingeltonPlc Glb_Plc;
        private ClsSingeltonPlcParameter m_PlcPara; 


        public FrmMain()
        {
            FuncGeneral.Start();

            this.InitializeComponent();
            this.Width = 1024;
            this.Height = 768;
            this.Vnt11.Click += new System.EventHandler(this.Ventil_Click);
            this.Vnt02.Click += new System.EventHandler(this.Ventil_Click);
            this.Vnt03.Click += new System.EventHandler(this.Ventil_Click);
            this.Vnt01.Click += new System.EventHandler(this.Ventil_Click);

            Glb_VarCollect = ClsSingeltonVariablesCollecter.CreateInstance();
            Glb_VarCollect1 = ClsSingeltonVariablesCollecter.CreateInstance();
            Glb_DataBinding = ClsSingeltonDataBinding.CreateInstance();
            this.m_PlcPara.ProgBar = this.progressBar1;
            this.m_PlcPara.LblStatus = this.lblStatus;
            this.m_PlcPara.BtnStart = this.BtnStartPlc;
            this.m_PlcPara.BtnStopp = this.BtnStoppPlc;
            this.m_PlcPara.IP = "192.168.2.118";
            this.m_PlcPara.Rack = 0;
            this.m_PlcPara.Slot = 2;

            Glb_Plc = ClsSingeltonPlc.CreateInstance(this.m_PlcPara);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowClose = true;
            Glb_Plc.AddList("DB50.DBD0", "P1_Qmin_1", "REAL", "Pumpen:Pumpendruck 1[EHbar]");
            Glb_Plc.AddList("DB50.DBD4", "P1_Qmin_2", "REAL", "Pumpen:Pumpendruck 2[EHbar]");
            Glb_Plc.AddList("DB50.DBD8", "P1_Qmin_3", "REAL", "Pumpen:Pumpendruck 3[EHbar]");
            Glb_Plc.AddList("DB50.DBD12", "P1_Qmin_4", "REAL", "Pumpen:Pumpendruck 4[EHbar]");
            Glb_Plc.AddList("DB50.DBD16", "P1_Qmin_5", "REAL", "Pumpen:Pumpendruck 5[EHbar]");

            Glb_Plc.AddList("DB51.DBD0", "P1_Qmin_1", "REAL", "Pumpen:Pumpendruck 1[EHbar]");
            Glb_Plc.AddList("DB51.DBD4", "P1_Qmin_2", "REAL", "Pumpen:Pumpendruck 2[EHbar]");
            Glb_Plc.AddList("DB51.DBD8", "P1_Qmin_3", "REAL", "Pumpen:Pumpendruck 3[EHbar]");
            Glb_Plc.AddList("DB51.DBD12", "P1_Qmin_4", "REAL", "Pumpen:Pumpendruck 4[EHbar]");
            Glb_Plc.AddList("DB51.DBD16", "P1_Qmin_5", "REAL", "Pumpen:Pumpendruck 5[EHbar]");

            Glb_Plc.AddList("DB52.DBW0", "Valve01", "INT", "Schieber:Status 1");
            Glb_Plc.AddList("DB52.DBW2", "Valve02", "INT", "Schieber:Status 2");
            Glb_Plc.AddList("DB52.DBW4", "Valve03", "INT", "Schieber:Status 3");
            Glb_Plc.AddList("DB52.DBW6", "Valve04", "INT", "Schieber:Status 4");
            Glb_Plc.AddList("DB52.DBW8", "Valve05", "INT", "Schieber:Status 5");

            Glb_Plc.AddList("DB53.DBX0.0", "Bit01", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX0.1", "Bit01", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX0.2", "Bit02", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX0.3", "Bit03", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX0.4", "Bit04", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX0.5", "Bit05", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX0.6", "Bit06", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX0.7", "Bit07", "BOOL", "");

            Glb_Plc.AddList("DB53.DBX1.0", "Bit08", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX1.1", "Bit09", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX1.2", "Bit10", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX1.3", "Bit11", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX1.4", "Bit12", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX1.5", "Bit13", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX1.6", "Bit14", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX1.7", "Bit15", "BOOL", "");

            Glb_Plc.AddList("DB53.DBX2.0", "Bit16", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX2.1", "Bit17", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX2.2", "Bit18", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX2.3", "Bit29", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX2.4", "Bit20", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX2.5", "Bit21", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX2.6", "Bit22", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX2.7", "Bit23", "BOOL", "");

            Glb_Plc.AddList("DB53.DBX3.0", "Bit24", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX3.1", "Bit25", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX3.2", "Bit26", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX3.3", "Bit27", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX3.4", "Bit28", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX3.5", "Bit29", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX3.6", "Bit30", "BOOL", "");
            Glb_Plc.AddList("DB53.DBX3.7", "Bit31", "BOOL", "");

            Glb_Plc.AddList("DB54.DBX0.0", "Bit01", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX0.1", "Bit01", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX0.2", "Bit02", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX0.3", "Bit03", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX0.4", "Bit04", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX0.5", "Bit05", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX0.6", "Bit06", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX0.7", "Bit07", "BOOL", "");

            Glb_Plc.AddList("DB54.DBX1.0", "Bit08", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX1.1", "Bit09", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX1.2", "Bit10", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX1.3", "Bit11", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX1.4", "Bit12", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX1.5", "Bit13", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX1.6", "Bit14", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX1.7", "Bit15", "BOOL", "");

            Glb_Plc.AddList("DB54.DBX2.0", "Bit16", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX2.1", "Bit17", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX2.2", "Bit18", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX2.3", "Bit29", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX2.4", "Bit20", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX2.5", "Bit21", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX2.6", "Bit22", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX2.7", "Bit23", "BOOL", "");

            Glb_Plc.AddList("DB54.DBX3.0", "Bit24", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX3.1", "Bit25", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX3.2", "Bit26", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX3.3", "Bit27", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX3.4", "Bit28", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX3.5", "Bit29", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX3.6", "Bit30", "BOOL", "");
            Glb_Plc.AddList("DB54.DBX3.7", "Bit31", "BOOL", "");
            
            Glb_DataBinding.AddList(this, "Txt01", "Text", "DB50.P1_Qmin_1");
            Glb_DataBinding.AddList(this, "Txt02", "Text", "DB50.P1_Qmin_2");
            Glb_DataBinding.AddList(this, "Txt03", "Text", "DB50.P1_Qmin_3");
            Glb_DataBinding.AddList(this, "Txt04", "Text", "DB50.P1_Qmin_4");
            Glb_DataBinding.AddList(this, "Txt05", "Text", "DB50.P1_Qmin_5");
            Glb_DataBinding.AddList(this, "Txt06", "Text", "DB50.P1_Qmin_1");
            Glb_DataBinding.AddList(this, "Txt07", "Text", "DB50.P1_Qmin_2");
            Glb_DataBinding.AddList(this, "Txt08", "Text", "DB50.P1_Qmin_3");
            Glb_DataBinding.AddList(this, "Txt09", "Text", "DB50.P1_Qmin_4");
            Glb_DataBinding.AddList(this, "Txt10", "Text", "DB50.P1_Qmin_5");
            Glb_DataBinding.AddList(this, "Txt11", "Text", "DB50.P1_Qmin_1");
            Glb_DataBinding.AddList(this, "Txt12", "Text", "DB50.P1_Qmin_2");
            Glb_DataBinding.AddList(this, "Txt13", "Text", "DB50.P1_Qmin_3");
            Glb_DataBinding.AddList(this, "Txt14", "Text", "DB50.P1_Qmin_4");
            Glb_DataBinding.AddList(this, "Txt15", "Text", "DB50.P1_Qmin_5");
            Glb_DataBinding.AddList(this, "Txt16", "Text", "DB51.P1_Qmin_1");
            Glb_DataBinding.AddList(this, "Txt17", "Text", "DB51.P1_Qmin_2");
            Glb_DataBinding.AddList(this, "Txt18", "Text", "DB51.P1_Qmin_3");
            Glb_DataBinding.AddList(this, "Txt19", "Text", "DB51.P1_Qmin_4");
            Glb_DataBinding.AddList(this, "Txt20", "Text", "DB51.P1_Qmin_5");
            Glb_DataBinding.AddList(this, "Txt21", "Text", "DB50.P1_Qmin_1");

            Glb_DataBinding.AddList(this, "Vnt01", "State", "DB52.Valve01");
            Glb_DataBinding.AddList(this, "Vnt02", "State", "DB52.Valve02");
            Glb_DataBinding.AddList(this, "Vnt03", "State", "DB52.Valve03");
            Glb_DataBinding.AddList(this, "Vnt04", "State", "DB52.Valve04");
            Glb_DataBinding.AddList(this, "Vnt05", "State", "DB52.Valve05");
            Glb_DataBinding.AddList(this, "Vnt06", "State", "DB52.Valve01");
            Glb_DataBinding.AddList(this, "Vnt07", "State", "DB52.Valve02");
            Glb_DataBinding.AddList(this, "Vnt08", "State", "DB52.Valve03");
            Glb_DataBinding.AddList(this, "Vnt09", "State", "DB52.Valve04");
            Glb_DataBinding.AddList(this, "Vnt10", "State", "DB52.Valve05");
            Glb_DataBinding.AddList(this, "Vnt11", "State", "DB52.Valve01");

            Glb_DataBinding.AddList(this, "Pip01", "Flow", "DB53.Bit01");
            Glb_DataBinding.AddList(this, "Pip02", "Flow", "DB53.Bit02");
            Glb_DataBinding.AddList(this, "Pip03", "Flow", "DB53.Bit03");
            Glb_DataBinding.AddList(this, "Pip04", "Flow", "DB53.Bit04");
            Glb_DataBinding.AddList(this, "Pip05", "Flow", "DB53.Bit05");
            Glb_DataBinding.AddList(this, "Pip06", "Flow", "DB53.Bit06");
            Glb_DataBinding.AddList(this, "Pip07", "Flow", "DB53.Bit01");
            Glb_DataBinding.AddList(this, "Pip08", "Flow", "DB53.Bit08");
            Glb_DataBinding.AddList(this, "Pip09", "Flow", "DB53.Bit09");
            Glb_DataBinding.AddList(this, "Pip10", "Flow", "DB53.Bit10");
            Glb_DataBinding.AddList(this, "Pip11", "Flow", "DB53.Bit11");
            Glb_DataBinding.AddList(this, "Pip12", "Flow", "DB53.Bit12");
            Glb_DataBinding.AddList(this, "Pip13", "Flow", "DB53.Bit13");
            Glb_DataBinding.AddList(this, "Pip14", "Flow", "DB53.Bit14");
            Glb_DataBinding.AddList(this, "Pip15", "Flow", "DB53.Bit15");

            Glb_DataBinding.AddList(this, "Led01", "State", "DB54.Bit20");
            Glb_DataBinding.AddList(this, "Led02", "State", "DB54.Bit23");
            Glb_DataBinding.AddList(this, "Led03", "State", "DB54.Bit24");
            Glb_DataBinding.AddList(this, "Led04", "State", "DB54.Bit25");
            Glb_DataBinding.AddList(this, "Led05", "State", "DB54.Bit27");
            Glb_DataBinding.AddList(this, "Led06", "State", "DB54.Bit29");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Glb_Plc.StoppRead();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Dispose();
        }

        private void GB_Datenausgabe_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void ledRectangle6_Click(object sender, EventArgs e)
        {

        }

        private void valve1_Click(object sender, EventArgs e)
        {

        }

        private void valve2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void compPipe3_Click(object sender, EventArgs e)
        {

        }

        private void compPipe5_Click(object sender, EventArgs e)
        {

        }

        private void compPipe4_Click(object sender, EventArgs e)
        {

        }

        private void txtBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void Ventil_Click(object sender, EventArgs e)
        {
            CompVentil obj = (CompVentil)sender;
            if(obj.State== CompVentil.CompVentilState.Open)
            {
                obj.State = CompVentil.CompVentilState.Close;
            }
            else
            {
                obj.State = CompVentil.CompVentilState.Open;
            }
        }

        private void bitButton2_Click(object sender, EventArgs e)
        {
            this.ShowMenu();
        }

        private void BtnStartPlc_Click(object sender, EventArgs e)
        {
            Glb_Plc.StartRead();
        }

        private void BtnStoppPlc_Click(object sender, EventArgs e)
        {
            Glb_Plc.StoppRead();
        }

        private void compArrow1_Click(object sender, EventArgs e)
        {

        }

        private void Pip02_Click(object sender, EventArgs e)
        {

        }

        private void Pip10_Click(object sender, EventArgs e)
        {

        }

        private void Pip13_Click(object sender, EventArgs e)
        {

        }

        private void compPipe1_Click(object sender, EventArgs e)
        {

        }

        private void compArrow3_Click(object sender, EventArgs e)
        {

        }

        private void Txt16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

    }
}
