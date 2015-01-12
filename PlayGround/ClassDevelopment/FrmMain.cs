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
using System.Data.SqlClient;
using System.Data.SQLite;


namespace ClassDevelopment
{
    public partial class FrmMain : Helper.FrmVorlageMenu
    {
        private SQLiteCommand m_sqliteCommand;
        private SQLiteConnection m_sqliteConnection;
        private SQLiteDataReader m_sqliteDataReader;

        private int m_errorTickerCounter;
        private int m_errorNoRecords;
        private List<string>m_errorList;

        private ClsSingeltonVariablesCollecter m_varCollect;
        private ClsSingeltonUserManagement m_userManagement;
        private ClsSingeltonDataBinding m_dataBinding;
        private ClsSingeltonLanguage m_language;
        private ClsSingeltonPlc m_plc;
        private ClsSingeltonPlcParameter m_plcPara;
        private ClsSingeltonFormularManager m_formularManager;
        private ClsSingeltonParameter m_parameter;
        private FrmHeater FrmHeater;
        private FrmPara FrmParameter;
        private FrmInfo FrmInformation;
        private FrmServ FrmService;
        private FrmError FrmError;
        private FrmRelease FrmRelease;
        private FrmErrorReportingSystem FrmErrorReportingSystem;

        private FrmO2Curve FrmO2Curve;
        public FrmMain()
        {

            this.m_errorTickerCounter = 0;
            this.m_errorNoRecords = 0;
            this.m_errorList = new List<string>();
            this.m_errorList.Clear();
            this.m_parameter = ClsSingeltonParameter.CreateInstance();
            this.m_sqliteConnection = new SQLiteConnection();
            this.m_sqliteConnection.ConnectionString = this.m_parameter.ConnectionString;
            this.m_sqliteConnection.Open();

            this.m_sqliteCommand = new SQLiteCommand(this.m_sqliteConnection);

            FuncGeneral.Start();

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

            this.FrmHeater = new FrmHeater();
            this.FrmParameter = new FrmPara();
            this.FrmInformation = new FrmInfo();
            this.FrmService = new FrmServ();
            this.FrmError = new FrmError();
            this.FrmRelease = new FrmRelease();
            this.FrmErrorReportingSystem = new FrmErrorReportingSystem();
            this.FrmO2Curve = new FrmO2Curve();

            this.TmrErrorTicker.Enabled = true;
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

            this.m_formularManager.AddBitButtonCloseAll();
            this.m_formularManager.AddButton(CompBitButtonStyle.btg_Para, "FrmPara");
            this.m_formularManager.AddButton(CompBitButtonStyle.btg_Info, "FrmInfo");
            this.m_formularManager.AddButton(CompBitButtonStyle.btg_Service, "FrmServ");
            this.m_formularManager.AddButton(CompBitButtonStyle.btg_Error, "FrmError");
            this.m_formularManager.AddButton(CompBitButtonStyle.btg_Freigabe, "FrmRelease");
            this.m_formularManager.AddButton(CompBitButtonStyle.btg_Meldesystem, "FrmErrorReportingSystem");


            FuncGeneral.CentreObject(this.LblPlcNoConnect,this.GbxOutput);

            this.Text = "Demo";


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

        private void BtnMenuMain_Click(object sender, EventArgs e)
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

        private void FrmMain_Activated(object sender, EventArgs e)
        {
            this.m_parameter.ActualForm = this;
        }

        private void label36_Click(object sender, EventArgs e)
        {
            this.m_formularManager.FormularShow("FrmErrorReportingSystem");
        }

        private void ReadError()
        {
            this.m_sqliteCommand.CommandText = this.m_plc.GetSqlAkuell();
            this.m_errorList.Clear();
            if (this.m_sqliteDataReader != null)
            {
                this.m_sqliteDataReader.Close();
                this.m_sqliteDataReader = null;
            }
            this.m_sqliteDataReader = this.m_sqliteCommand.ExecuteReader();
            while (this.m_sqliteDataReader.Read())
            {
                this.m_errorList.Add(this.m_sqliteDataReader.GetValue(0).ToString() + " | " + 
                                     this.m_sqliteDataReader.GetValue(1).ToString() + " | " +
                                     this.m_sqliteDataReader.GetValue(2).ToString()
                                     );
            }
            this.m_sqliteDataReader.Close();

        }

        private void TmrErrorTicker_Tick(object sender, EventArgs e)
        {
            this.m_sqliteCommand.CommandText = "select count (id) as NumberOfRecords from errors";

            if (this.m_sqliteDataReader != null)
            {
                this.m_sqliteDataReader.Close();
                this.m_sqliteDataReader = null;
            }
            this.m_sqliteDataReader = this.m_sqliteCommand.ExecuteReader();
            this.m_sqliteDataReader.Read();
            int no_of_records = this.m_sqliteDataReader.GetInt32(0);
            this.m_sqliteDataReader.Close();
            this.LblErrorBar.Visible=Convert.ToBoolean(no_of_records);
            if (m_errorNoRecords != no_of_records)
            {
                this.m_errorTickerCounter = 0;
                m_errorNoRecords = no_of_records;
                this.ReadError();
            }
            if (this.m_errorTickerCounter<this.m_errorList.Count())
            {
                this.LblErrorBar.Text = (this.m_errorTickerCounter + 1).ToString() + "..." + this.m_errorList.Count().ToString() + " | " + this.m_errorList[this.m_errorTickerCounter];
                this.m_errorTickerCounter++;
            }
            else
            {
                this.m_errorTickerCounter = 0;
                this.ReadError();
            }
        }

        private void compBitButton1_Click(object sender, EventArgs e)
        {
            this.m_formularManager.FormularShow("FrmO2Curve");
        }

        private void BtnStoppPlc_Click_1(object sender, EventArgs e)
        {
            this.m_plc.StoppRead();

        }

        private void BtnStartPlc_Click_1(object sender, EventArgs e)
        {
            this.m_plc.StartRead();
        }

        private void compBitButton2_Click(object sender, EventArgs e)
        {
            this.ShowMenu();
        }
    }
}
