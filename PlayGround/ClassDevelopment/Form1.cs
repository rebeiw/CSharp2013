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



namespace ClassDevelopment
{
    public partial class Form1 : Form
    {
        private ClsVarCollect Glb_VarCollect;
        private ClsVarCollect Glb_VarCollect1;

        private ClsDataBinding Glb_DataBinding;

        private ClsPlc Glb_Plc;
        private PlcParameter m_PlcPara; 


        public Form1()
        {
            InitializeComponent();
            Glb_VarCollect = ClsVarCollect.CreateInstance();
            Glb_VarCollect1 = ClsVarCollect.CreateInstance();
            Glb_DataBinding = ClsDataBinding.CreateInstance();
            this.m_PlcPara.ProgBar = this.progressBar1;
            this.m_PlcPara.LblStatus = this.lblStatus;
            this.m_PlcPara.BtnStart = this.btnstart;
            this.m_PlcPara.BtnStopp = this.btnstopp;
            this.m_PlcPara.IP = "192.168.2.118";
            this.m_PlcPara.Rack = 0;
            this.m_PlcPara.Slot = 2;

            Glb_Plc = ClsPlc.CreateInstance(this.m_PlcPara);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glb_Plc.AddList("DB59.DBD0","P1_Qmin_1","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE0]");
            Glb_Plc.AddList("DB59.DBD4","P1_Qmax_1","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE0]");
            Glb_Plc.AddList("DB59.DBD8","P1_Drehzahl_1","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE0]");
            Glb_Plc.AddList("DB59.DBD12","P1_Bypass_1","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE0]");
            Glb_Plc.AddList("DB59.DBD16","P1_Qmin_2","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE1]");
            Glb_Plc.AddList("DB59.DBD20","P1_Qmax_2","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE1]");
            Glb_Plc.AddList("DB59.DBD24","P1_Drehzahl_2","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE1]");
            Glb_Plc.AddList("DB59.DBD28","P1_Bypass_2","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE1]");
            Glb_Plc.AddList("DB59.DBD32","P1_Qmin_3","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE2]");
            Glb_Plc.AddList("DB59.DBD36","P1_Qmax_3","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE2]");
            Glb_Plc.AddList("DB59.DBD40","P1_Drehzahl_3","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE2]");
            Glb_Plc.AddList("DB59.DBD44","P1_Bypass_3","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE2]");
            Glb_Plc.AddList("DB59.DBD48","P1_Qmin_4","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE3]");
            Glb_Plc.AddList("DB59.DBD52","P1_Qmax_4","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE3]");
            Glb_Plc.AddList("DB59.DBD56","P1_Drehzahl_4","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE3]");
            Glb_Plc.AddList("DB59.DBD60","P1_Bypass_4","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE3]");
            Glb_Plc.AddList("DB59.DBD64","P1_Qmin_5","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP1][ZE4]");
            Glb_Plc.AddList("DB59.DBD68","P1_Qmax_5","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP2][ZE4]");
            Glb_Plc.AddList("DB59.DBD72","P1_Drehzahl_5","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP3][ZE4]");
            Glb_Plc.AddList("DB59.DBD76","P1_Bypass_5","REAL","Pumpe 1:Qmin[m3/h],Qmax[m3/h],Drehzahl[%],Bypass[%][SP4][ZE4]");
            Glb_Plc.AddList("DB59.DBD80","P1_Drehzahl_Man_1","REAL","Pumpe 1 Drehzahlen:Drehzahl 1[ZE0][EH%]");
            Glb_Plc.AddList("DB59.DBD84","P1_Drehzahl_Man_2","REAL","Pumpe 1 Drehzahlen:Drehzahl 2[ZE0][EH%]");
            Glb_Plc.AddList("DB59.DBD88","P1_Drehzahl_Man_3","REAL","Pumpe 1 Drehzahlen:Drehzahl 3[ZE0][EH%]");
            Glb_Plc.AddList("DB59.DBD92","P1_Drehzahl_Man_4","REAL","Pumpe 1 Drehzahlen:Drehzahl 4[ZE0][EH%]");
            Glb_Plc.AddList("DB59.DBD96","P1_Drehzahl_Man_5","REAL","Pumpe 1 Drehzahlen:Drehzahl 5[ZE0][EH%]");
            Glb_Plc.AddList("DB59.DBD100","P1_Drehzahl_Man_6","REAL","Pumpe 1 Drehzahlen:Drehzahl 6[ZE0][EH%]");
            Glb_Plc.AddList("DB59.DBD104","P1_Druck_Man_1","REAL","Pumpe 1 Druecke:Druck 1[ZE0][EHbar]");
            Glb_Plc.AddList("DB59.DBD108","P1_Druck_Man_2","REAL","Pumpe 1 Druecke:Druck 2[ZE0][EHbar]");
            Glb_Plc.AddList("DB59.DBD112","P1_Druck_Man_3","REAL","Pumpe 1 Druecke:Druck 3[ZE0][EHbar]");

            Glb_DataBinding.AddList(this, "textBox1", "Text", "Variable1");
            Glb_DataBinding.AddList(this, "textBox2", "Text", "Variable1");
            Glb_DataBinding.AddList(this, "textBox3", "Text", "Variable2");
            Glb_VarCollect.CreateVariable("Variable1", DataType.Int);
            Glb_VarCollect.CreateVariable("Variable2",DataType.Double);

            Glb_VarCollect1.CreateVariable("Variable3", DataType.Double);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Glb_Plc.StartRead();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Glb_Plc.StoppRead();
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

    }
}
