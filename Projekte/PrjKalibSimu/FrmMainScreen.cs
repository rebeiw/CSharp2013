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
using System.Threading;
using PrjMakroControl;

namespace PrjKalibSimu
{
    public partial class frmMainScreen : Form
    {
        private ClsPLC glb_plc;
        private ClsDataBinding glb_DataBinding;
        private ClsFormularManager frmMng;
        private FrmSchieber[] frmSchieber;
        private FrmBehaelter[] frmBehaelter;

        private HelperMakroControl helperMakroControl;

        private HelperLibNoDave helperLibNoDave;
        public frmMainScreen()
        {
            InitializeComponent();
            FuncGeneral.Start();

            glb_DataBinding = ClsDataBinding.CreateInstance();
            frmMng = ClsFormularManager.CreateInstance();
            frmMng.AddButton(BtnStyle.btg_Para, "FrmPara");
            frmMng.AddButton(BtnStyle.btg_Service, "FrmService");
            frmMng.AddButton(BtnStyle.btg_Freigabe, "FrmRelease");
            frmMng.AddButton(BtnStyle.btg_Error, "FrmError");
            frmMng.AddBitButtonCloseAll();


            glb_plc = ClsPLC.CreateInstance();

            helperMakroControl = new HelperMakroControl();
            helperLibNoDave = new HelperLibNoDave(this);
        }

        private void BtnDown(object sender, MouseEventArgs e)
        {
            glb_plc.Write(FuncPLC.SymbolName(sender), "1");
        }

        private void BtnUp(object sender, MouseEventArgs e)
        {
            glb_plc.Write(FuncPLC.SymbolName(sender), "0");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.helperLibNoDave.LoadPlcData();
            this.LoadComponentBindings();
            glb_plc.IP = "10.100.72.125";
            glb_plc.Rack = 0;
            glb_plc.Slot = 2;
            glb_plc.Open();
            glb_plc.Read();


            FrmRelease frmRelease = new FrmRelease();
            FrmError frmError = new FrmError();
            FrmPara frmPara = new FrmPara();
            FrmService frmService = new FrmService();


            List<Helper.Valve> Schieber = new List<Helper.Valve>();
            Schieber.Add(this.ValveV100);
            Schieber.Add(this.ValveV110);
            Schieber.Add(this.ValveV120);
            Schieber.Add(this.ValveV130);
            Schieber.Add(this.ValveV140);

            Schieber.Add(this.ValveV200);
            Schieber.Add(this.ValveV210);
            Schieber.Add(this.ValveV220);

            Schieber.Add(this.ValveV300);
            Schieber.Add(this.ValveV310);
            Schieber.Add(this.ValveV320);

            Schieber.Add(this.ValveV400);
            Schieber.Add(this.ValveV410);
            Schieber.Add(this.ValveV420);

            Schieber.Add(this.ValveV500);
            Schieber.Add(this.ValveV510);

            Schieber.Add(this.ValveV600);
            Schieber.Add(this.ValveV610);
            Schieber.Add(this.ValveV620);
            Schieber.Add(this.ValveV630);
            Schieber.Add(this.ValveV640);
            Schieber.Add(this.ValveV650);
            Schieber.Add(this.ValveV660);
            Schieber.Add(this.ValveV670);
            Schieber.Add(this.ValveV680);
            Schieber.Add(this.ValveV690);
            Schieber.Add(this.ValveV700);
            Schieber.Add(this.ValveV710);


            frmSchieber = new FrmSchieber[Schieber.Count];

            int counter = 0;
            foreach (Helper.Valve schieber in Schieber)
            {
                schieber.Click += new System.EventHandler(this.valve);
                frmSchieber[counter] = new FrmSchieber();
                frmSchieber[counter].TopMost = true;
                string frmName = "FrmSchieber" + schieber.Name;
                frmSchieber[counter].Name = frmName;
                string titelFormular = "Schieber " + schieber.Name.Substring(5, 2) + "." + schieber.Name.Substring(7);
                frmSchieber[counter].Text = titelFormular;
                frmMng.FormularAdd(frmSchieber[counter], frmName);
                frmSchieber[counter].BindDatas(FuncString.GetOnlyNumeric(schieber.Name));
                Point xyValve = FuncGeneral.GetMiddle(schieber);

                Point xyFrm = FuncGeneral.GetMiddle(frmSchieber[counter]);

                Point valvePos = new Point();
                valvePos.X = schieber.Left + xyValve.X;
                valvePos.Y = schieber.Top - xyValve.Y;

                frmSchieber[counter].Left = valvePos.X - xyFrm.X ;
                frmSchieber[counter].Top = valvePos.Y - xyFrm.Y * 2 - xyValve.Y + 35;

                if (frmSchieber[counter].Left < 0)
                {
                    frmSchieber[counter].Left = 0;
                }
                if (frmSchieber[counter].Top < 0)
                {
                    frmSchieber[counter].Top = valvePos.Y +  xyValve.Y + 70;
                }
            }

            List<Helper.Scale> Behaelter = new List<Helper.Scale>();
            Behaelter.Add(this.ScaleT1);

            frmBehaelter = new FrmBehaelter[Behaelter.Count];

            counter = 0;
            foreach (Helper.Scale behaelter in Behaelter)
            {
                behaelter.Click += new System.EventHandler(this.Behaelter);
                frmBehaelter[counter] = new FrmBehaelter();
                frmBehaelter[counter].TopMost = true;
                string frmName = "FrmBehaelter" + behaelter.Name;
                frmBehaelter[counter].Name = frmName;
                string titelFormular = "Behaelter " + behaelter.Name.Substring(5, 2) + "." + behaelter.Name.Substring(7);
                frmBehaelter[counter].Text = titelFormular;
                frmMng.FormularAdd(frmBehaelter[counter], frmName);
                frmBehaelter[counter].BindDatas(FuncString.GetOnlyNumeric(behaelter.Name));
                Point xyValve = FuncGeneral.GetMiddle(behaelter);

                Point xyFrm = FuncGeneral.GetMiddle(frmBehaelter[counter]);

                Point valvePos = new Point();
                valvePos.X = behaelter.Left + xyValve.X;
                valvePos.Y = behaelter.Top - xyValve.Y;

                frmBehaelter[counter].Left = valvePos.X - xyFrm.X;
                frmBehaelter[counter].Top = valvePos.Y - xyFrm.Y * 2 - xyValve.Y + 35;

                if (frmBehaelter[counter].Left < 0)
                {
                    frmBehaelter[counter].Left = 0;
                }
                if (frmBehaelter[counter].Top < 0)
                {
                    frmBehaelter[counter].Top = valvePos.Y + xyValve.Y + 70;
                }


                ToolTip toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 0;
                toolTip1.ShowAlways = true;
                toolTip1.UseAnimation = false;
                toolTip1.SetToolTip(this.BTN_Menu, "Menue");
                toolTip1.SetToolTip(this.BTN_Error, "Fehlermeldungen");


            }

            this.Left = 0;
            this.Top = 0;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.timer1.Interval = 100;
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            glb_plc.Read();
            glb_DataBinding.Dispatch();
        }

        private void Behaelter(object sender, EventArgs e)
        {
            Helper.Scale behaelter = (Helper.Scale)sender;
            string frmName = "FrmBehaelter" + behaelter.Name;
            frmMng.FormularShow(frmName);
        }


        private void valve(object sender, EventArgs e)
        {
            Valve schieber = (Valve)sender;
            string frmName = "FrmSchieber" + schieber.Name;
            frmMng.FormularShow(frmName);
        }

        private void toogleSwitch1_Click(object sender, EventArgs e)
        {
            ToggleSwitch obj = (ToggleSwitch)sender;
            string value = "0";
            string symbol = obj.Symbol;
            if (obj.State == SwitchState.On)
            {
                value = "0";
            }
            if (obj.State == SwitchState.Off)
            {
                value = "1";
            }
            glb_plc.Write(symbol, value);
        }


        private void BTN_Freigabe_Click(object sender, EventArgs e)
        {
            frmMng.FormularShow("FrmRelease");
        }

        private void BTN_Error_Click(object sender, EventArgs e)
        {
            frmMng.FormularShow("FrmError");
        }

        private void BTN_SK_Start_Click(object sender, EventArgs e)
        {

        }

        private void ScaleT1_Click(object sender, EventArgs e)
        {

        }

        private void BTN_T1_Click(object sender, EventArgs e)
        {
            frmMng.FormularShow("FrmBehaelterScaleT1");
        }

        private void BTN_Menu_Click(object sender, EventArgs e)
        {
            frmMng.SetFormPrintScreen(this);
            frmMng.FormularShow("FRM_Menu");
        }

        private void BTN_Reset_Error_Click(object sender, EventArgs e)
        {
            

        }

        private void TXT_SK_FunktionsNummer_TextChanged(object sender, EventArgs e)
        {

            SchrittInfo schrittInfo = helperMakroControl.GetInfo(this.TXT_SK_FunktionsNummer.Text, this.TXT_SK_Nummer.Text);
            this.GB_Schrittinfo.Text = schrittInfo.FunktionsText;
            this.LBL_Schrittinfo.Text = schrittInfo.SchrittText;
            Application.DoEvents();


        }

        private void LED_SK_Aktive_ValueChanged(object sender, EventArgs e)
        {
            if (LED_SK_Aktive.State == LEDState.LEDOn)
            {
                this.GB_Schrittinfo.Visible = true;
            }
            else
            {
                this.GB_Schrittinfo.Visible = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void LoadComponentBindings()
        {

            //Wasserklreislauf
            //Pipes
            string[] pipes = new string[] 
            { 
                "F101", "F102", "F103", "F104", "F105", "F106", "F107", "F108", "F109", "F110", 
                "F111", "F112", "F113", "F114", "F115", "F116", 

                "F201", "F202", "F203", "F204", "F205", 

                "F301", "F302", "F303", "F304", 

                "F401", "F402", "F403", "F404", "F405", "F406", "F407",

                "F500",

                "F600", "F601", "F602", "F603", "F604", "F605", "F606", "F607", "F608", "F609",
                "F610", "F611", "F612", "F613", "F614", "F615", "F616", "F617", "F618", "F619",
                "F620", "F621", "F622", "F623"
            };
            foreach (string pipe in pipes)
            {
                glb_DataBinding.AddList(this.Name, "Pipe" + pipe, "Flow", "DB50." + pipe + "_FlowOffOn");
            }

            //Wasserklreislauf
            //Klappen
            string[] valves = new string[] { "V100", "V110", "V120", "V130", "V140", "V200", "V210", "V220", "V300", "V310", "V320", "V400", "V410", "V420", "V500", "V510", "V600", "V610", "V620", "V630", "V640", "V650", "V660", "V670", "V680", "V690", "V700", "V710" };

            foreach (string valve in valves)
            {
                glb_DataBinding.AddList(this.Name, "Valve" + valve, "Mode", "DB50." + valve + "_ModeManuAuto");
                glb_DataBinding.AddList(this.Name, "Valve" + valve, "State", "DB50." + valve + "_State");
            }

            //Behalter
            glb_DataBinding.AddList(this.Name, this.LifterT1.Name.ToString(), "State", "DB50.T1_State");

            //Schrittketteninfo
            glb_DataBinding.AddList(this.Name, this.TXT_Watchdog.Name.ToString(), "Text", "DB52.SK_SchrittNummer");
            glb_DataBinding.AddList(this.Name, this.BTN_Error.Name.ToString(), "PictureNumber", "DB52.Sammelfehler");

            glb_DataBinding.AddList(this.Name, this.LED_SK_Fehler.Name.ToString(), "State", "DB52.SK_Fehler");
            glb_DataBinding.AddList(this.Name, this.LED_SK_Aktive.Name.ToString(), "State", "DB52.SK_Aktiv");


            glb_DataBinding.AddList(this.Name, this.TXT_Watchdog.Name.ToString(), "Text", "DB52.PLC_WatchDog");

            glb_DataBinding.AddList(this.Name, this.TXT_SK_Nummer.Name.ToString(), "Text", "DB52.SK_SchrittNummer");
            glb_DataBinding.AddList(this.Name, this.TXT_SK_FunktionsNummer.Name.ToString(), "Text", "DB52.SK_FunkionsNummer");
            glb_DataBinding.AddList(this.Name, this.TXT_SK_Restzeit.Name.ToString(), "Text", "DB52.SK_Restzeit");

            //Leckagetest
            glb_DataBinding.AddList(this.Name, this.WT_FS121.Name.ToString(), "Value", "DB50.FS121_Value");
            glb_DataBinding.AddList(this.Name, this.WT_FS121.Name.ToString(), "Error", "DB50.FS121_Error");
            glb_DataBinding.AddList(this.Name, this.WT_FS131.Name.ToString(), "Value", "DB50.FS131_Value");
            glb_DataBinding.AddList(this.Name, this.WT_FS131.Name.ToString(), "Error", "DB50.FS131_Error");
            glb_DataBinding.AddList(this.Name, this.WT_FS141.Name.ToString(), "Value", "DB50.FS141_Value");
            glb_DataBinding.AddList(this.Name, this.WT_FS141.Name.ToString(), "Error", "DB50.FS141_Error");


            //Pumpe
            glb_DataBinding.AddList(this.Name, this.pump1.Name.ToString(), "State", "DB50.P1_State");
            glb_DataBinding.AddList(this.Name, this.TXT_P1Speed.Name.ToString(), "Text", "DB50.P1_SetSpeed");

            //Klappe
            glb_DataBinding.AddList(this.Name, this.ValveV510.Name.ToString(), "Rotation", "DB50.P1_SetBypass");
            glb_DataBinding.AddList(this.Name, this.TXT_V510.Name.ToString(), "Text", "DB50.P1_SetBypass");


            //Druck und Temperaturen
            glb_DataBinding.AddList(this.Name, this.TXT_B13.Name.ToString(), "Text", "DB50.WKL_B13_Druck");
            glb_DataBinding.AddList(this.Name, this.TXT_B15.Name.ToString(), "Text", "DB50.WKL_B15_Druck");
            glb_DataBinding.AddList(this.Name, this.TXT_B16.Name.ToString(), "Text", "DB50.WKL_B16_Druck");
            glb_DataBinding.AddList(this.Name, this.TXT_B17.Name.ToString(), "Text", "DB50.WKL_B17_Druck");
            glb_DataBinding.AddList(this.Name, this.TXT_B18.Name.ToString(), "Text", "DB50.WKL_B18_Druck");

            glb_DataBinding.AddList(this.Name, this.TXT_B14.Name.ToString(), "Text", "DB50.WKL_B14_Temperatur");


        }


    }
}
