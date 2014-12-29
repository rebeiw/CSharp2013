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
    public partial class Form1 : Helper.FrmVorlageMenu
    {
        private ClsSingeltonVariablesCollecter Glb_VarCollect;
        private ClsSingeltonVariablesCollecter Glb_VarCollect1;

        private ClsSingeltonDataBinding Glb_DataBinding;

        private ClsSingeltonPlc Glb_Plc;
        private ClsSingeltonPlcParameter m_PlcPara; 


        public Form1()
        {
            FuncGeneral.Start();

            this.InitializeComponent();
            this.compVentil1.Click += new System.EventHandler(this.Ventil_Click);
            this.compVentil2.Click += new System.EventHandler(this.Ventil_Click);
            this.compVentil3.Click += new System.EventHandler(this.Ventil_Click);
            this.compVentil4.Click += new System.EventHandler(this.Ventil_Click);

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


            Glb_DataBinding.AddList(this, "Txt01", "Text", "DB50.P1_Qmin_1");
            Glb_DataBinding.AddList(this, "Txt02", "Text", "DB50.P1_Qmin_2");
            Glb_DataBinding.AddList(this, "Txt03", "Text", "DB50.P1_Qmin_3");
            Glb_DataBinding.AddList(this, "Txt04", "Text", "DB50.P1_Qmin_4");
            Glb_DataBinding.AddList(this, "Txt05", "Text", "DB50.P1_Qmin_5");
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

    }
}
