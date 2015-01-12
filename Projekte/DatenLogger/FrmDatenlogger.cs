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

namespace DatenLogger
{
    public partial class FrmDatenlogger : FrmVorlageMenu
    {

        private string m_ConnectionString =
            "Data Source=H4T9FV1;" +
            "Initial Catalog=Kalibwin;" +
            "Integrated Security=True";


        private ClsPLC glb_plc;
        private ClsDataBinding glb_DataBinding;
        private ClsFormularManager frmMng;
        private SQLHelper sqlHelper;

        public FrmDatenlogger()
        {
            InitializeComponent();
            glb_DataBinding = ClsDataBinding.CreateInstance();
            frmMng = ClsFormularManager.CreateInstance();
            glb_plc = ClsPLC.CreateInstance();

            sqlHelper = new SQLHelper(m_ConnectionString);
            FuncGeneral.Start();
            this.ShowClose = true;
            this.LoadPlc();
            this.BindConrols();

            glb_plc.IP = "10.100.72.125";
            glb_plc.Rack = 0;
            glb_plc.Slot = 2;

            glb_plc.Open();
            glb_plc.Read();

            this.CreateListView();

        }

        private void LoadPlc()
        {

            glb_plc.AddList("DB51.DBX0.0", "W1401_Trigger", "BOOL", "vorläufige Platzhaltervariable");
            glb_plc.AddList("DB51.DBW2", "W1401_Funktion", "INT", "", true);
            glb_plc.AddList("DB51.DBD4", "W1401_Parameter_1", "REAL", "", true);
            glb_plc.AddList("DB51.DBD8", "W1401_Parameter_2", "REAL", "", true);
            glb_plc.AddList("DB51.DBD12", "W1401_Parameter_3", "REAL", "", true);
            glb_plc.AddList("DB51.DBD16", "W1401_Parameter_4", "REAL", "", true);

            glb_plc.AddList("DB52.DBW0", "PLC_WatchDog", "INT", "", true);
            glb_plc.AddList("DB52.DBW2", "Visu_WatchDog", "INT", "", true);
            glb_plc.AddList("DB52.DBX4.0", "Reset_Fehler", "BOOL", "Fehermeldungen zuruecksetzen");
            glb_plc.AddList("DB52.DBX4.1", "Sammelfehler", "BOOL", "Meldung Sammelfehler");
            glb_plc.AddList("DB52.DBX4.2", "SK_Abbruch", "BOOL", "Schrittkette abbrechen");
            glb_plc.AddList("DB52.DBX4.3", "SK_Weiter", "BOOL", "Schrittkette bei Fehler wieder fortfahren");
            glb_plc.AddList("DB52.DBX4.4", "SK_SafetyPos", "BOOL", "Schrittkette Anlage in sichere Position fahren");
            glb_plc.AddList("DB52.DBX4.5", "SK_Einzelschritt", "BOOL", "Schrittkette Modus Einzelschritt");
            glb_plc.AddList("DB52.DBX4.6", "SK_Naechster", "BOOL", "Schrittkette Modus Einzelschritt Naechster Schritt");
            glb_plc.AddList("DB52.DBX4.7", "SK_Halt", "BOOL", "Schrittkette anhalten");
            glb_plc.AddList("DB52.DBX5.0", "SK_Start", "BOOL", "Schrittkette starten");
            glb_plc.AddList("DB52.DBX5.1", "SK_Aktiv", "BOOL", "Schrittkette ist aktiviert");
            glb_plc.AddList("DB52.DBX5.2", "SK_Fehler", "BOOL", "Schrittkette meldet Fehler");
            glb_plc.AddList("DB52.DBW6", "SK_SchrittNummer", "INT", "Aktuelle Schrittnummer");
            glb_plc.AddList("DB52.DBW8", "SK_FunkionsNummer", "INT", "Aktuelle Funktion");
            glb_plc.AddList("DB52.DBD10", "SK_Restzeit", "TIME", "Aktuelle Restzeit",true);
            glb_plc.AddList("DB52.DBX14.0", "SK_WatchdogOff", "BOOL", "Schrittkette starten");

            glb_plc.AddList("DB50.DBX0.0", "V100_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX0.1", "V100_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX0.2", "V100_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX0.3", "V100_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX0.4", "V100_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX0.5", "V100_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX0.6", "V100_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX0.7", "V100_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW2", "V100_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW4", "V100_State", "INT", "",true);
            glb_plc.AddList("DB50.DBX6.0", "V100_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX6.1", "V100_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX6.2", "V100_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX6.3", "V110_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX6.4", "V110_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX6.5", "V110_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX6.6", "V110_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX6.7", "V110_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX7.0", "V110_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX7.1", "V110_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX7.2", "V110_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW8", "V110_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW10", "V110_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX12.0", "V110_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX12.1", "V110_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX12.2", "V110_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX12.3", "V120_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX12.4", "V120_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX12.5", "V120_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX12.6", "V120_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX12.7", "V120_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX13.0", "V120_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX13.1", "V120_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX13.2", "V120_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW14", "V120_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW16", "V120_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX18.0", "V120_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX18.1", "V120_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX18.2", "V120_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX18.3", "V130_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX18.4", "V130_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX18.5", "V130_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX18.6", "V130_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX18.7", "V130_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX19.0", "V130_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX19.1", "V130_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX19.2", "V130_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW20", "V130_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW22", "V130_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX24.0", "V130_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX24.1", "V130_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX24.2", "V130_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX24.3", "V140_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX24.4", "V140_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX24.5", "V140_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX24.6", "V140_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX24.7", "V140_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX25.0", "V140_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX25.1", "V140_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX25.2", "V140_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW26", "V140_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW28", "V140_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX30.0", "V140_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX30.1", "V140_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX30.2", "V140_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX30.3", "F101_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX30.4", "F102_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX30.5", "F103_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX30.6", "F104_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX30.7", "F105_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX31.0", "F106_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX31.1", "F107_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX31.2", "F108_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX31.3", "F109_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX31.4", "F110_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX31.5", "F111_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX31.6", "F112_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX31.7", "F113_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX32.0", "F114_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX32.1", "F115_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX32.2", "F116_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX32.3", "V200_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX32.4", "V200_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX32.5", "V200_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX32.6", "V200_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX32.7", "V200_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX33.0", "V200_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX33.1", "V200_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX33.2", "V200_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW34", "V200_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW36", "V200_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX38.0", "V200_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX38.1", "V200_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX38.2", "V200_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX38.3", "V210_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX38.4", "V210_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX38.5", "V210_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX38.6", "V210_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX38.7", "V210_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX39.0", "V210_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX39.1", "V210_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX39.2", "V210_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW40", "V210_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW42", "V210_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX44.0", "V210_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX44.1", "V210_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX44.2", "V210_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX44.3", "V220_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX44.4", "V220_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX44.5", "V220_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX44.6", "V220_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX44.7", "V220_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX45.0", "V220_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX45.1", "V220_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX45.2", "V220_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW46", "V220_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW48", "V220_State", "INT", "");
            glb_plc.AddList("DB50.DBX50.0", "V220_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX50.1", "V220_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX50.2", "V220_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX50.3", "V230_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX50.4", "V230_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX50.5", "V230_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX50.6", "V230_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX50.7", "V230_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX51.0", "V230_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX51.1", "V230_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX51.2", "V230_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW52", "V230_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW54", "V230_State", "INT", "");
            glb_plc.AddList("DB50.DBX56.0", "V230_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX56.1", "V230_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX56.2", "V230_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX56.3", "F201_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX56.4", "F202_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX56.5", "F203_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX56.6", "F204_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX56.7", "V300_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX57.0", "V300_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX57.1", "V300_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX57.2", "V300_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX57.3", "V300_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX57.4", "V300_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX57.5", "V300_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX57.6", "V300_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW58", "V300_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW60", "V300_State", "INT", "");
            glb_plc.AddList("DB50.DBX62.0", "V300_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX62.1", "V300_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX62.2", "V300_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX62.3", "V310_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX62.4", "V310_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX62.5", "V310_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX62.6", "V310_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX62.7", "V310_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX63.0", "V310_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX63.1", "V310_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX63.2", "V310_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW64", "V310_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW66", "V310_State", "INT", "");
            glb_plc.AddList("DB50.DBX68.0", "V310_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX68.1", "V310_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX68.2", "V310_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX68.3", "V320_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX68.4", "V320_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX68.5", "V320_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX68.6", "V320_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX68.7", "V320_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX69.0", "V320_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX69.1", "V320_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX69.2", "V320_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW70", "V320_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW72", "V320_State", "INT", "");
            glb_plc.AddList("DB50.DBX74.0", "V320_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX74.1", "V320_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX74.2", "V320_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX74.3", "F301_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX74.4", "F302_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX74.5", "F303_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX74.6", "F304_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX74.7", "F205_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX75.0", "V400_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX75.1", "V400_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX75.2", "V400_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX75.3", "V400_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX75.4", "V400_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX75.5", "V400_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX75.6", "V400_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX75.7", "V400_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW76", "V400_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW78", "V400_State", "INT", "");
            glb_plc.AddList("DB50.DBX80.0", "V400_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX80.1", "V400_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX80.2", "V400_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX80.3", "V410_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX80.4", "V410_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX80.5", "V410_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX80.6", "V410_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX80.7", "V410_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX81.0", "V410_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX81.1", "V410_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX81.2", "V410_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW82", "V410_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW84", "V410_State", "INT", "");
            glb_plc.AddList("DB50.DBX86.0", "V410_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX86.1", "V410_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX86.2", "V410_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX86.3", "V420_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX86.4", "V420_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX86.5", "V420_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX86.6", "V420_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX86.7", "V420_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX87.0", "V420_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX87.1", "V420_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX87.2", "V420_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW88", "V420_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW90", "V420_State", "INT", "");
            glb_plc.AddList("DB50.DBX92.0", "V420_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX92.1", "V420_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX92.2", "V420_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX92.3", "F401_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX92.4", "F402_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX92.5", "F403_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX92.6", "F404_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX92.7", "F405_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX93.0", "F406_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX93.1", "F407_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX93.2", "V500_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX93.3", "V500_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX93.4", "V500_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX93.5", "V500_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX93.6", "V500_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX93.7", "V500_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX94.0", "V500_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX94.1", "V500_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW96", "V500_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW98", "V500_State", "INT", "");
            glb_plc.AddList("DB50.DBX100.0", "V500_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX100.1", "V500_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX100.2", "V500_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX100.3", "V510_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX100.4", "V510_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX100.5", "V510_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX100.6", "V510_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX100.7", "V510_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX101.0", "V510_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX101.1", "V510_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX101.2", "V510_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW102", "V510_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW104", "V510_State", "INT", "");
            glb_plc.AddList("DB50.DBX106.0", "V510_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX106.1", "V510_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX106.2", "V510_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX106.3", "F500_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX106.4", "V600_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX106.5", "V600_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX106.6", "V600_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX106.7", "V600_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX107.0", "V600_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX107.1", "V600_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX107.2", "V600_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX107.3", "V600_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW108", "V600_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW110", "V600_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX112.0", "V600_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX112.1", "V600_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX112.2", "V600_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX112.3", "V610_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX112.4", "V610_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX112.5", "V610_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX112.6", "V610_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX112.7", "V610_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX113.0", "V610_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX113.1", "V610_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX113.2", "V610_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW114", "V610_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW116", "V610_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX118.0", "V610_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX118.1", "V610_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX118.2", "V610_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX118.3", "V620_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX118.4", "V620_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX118.5", "V620_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX118.6", "V620_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX118.7", "V620_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX119.0", "V620_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX119.1", "V620_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX119.2", "V620_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW120", "V620_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW122", "V620_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX124.0", "V620_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX124.1", "V620_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX124.2", "V620_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX124.3", "V630_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX124.4", "V630_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX124.5", "V630_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX124.6", "V630_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX124.7", "V630_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX125.0", "V630_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX125.1", "V630_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX125.2", "V630_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW126", "V630_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW128", "V630_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX130.0", "V630_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX130.1", "V630_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX130.2", "V630_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX130.3", "V640_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX130.4", "V640_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX130.5", "V640_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX130.6", "V640_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX130.7", "V640_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX131.0", "V640_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX131.1", "V640_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX131.2", "V640_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW132", "V640_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW134", "V640_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX136.0", "V640_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX136.1", "V640_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX136.2", "V640_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX136.3", "V650_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX136.4", "V650_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX136.5", "V650_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX136.6", "V650_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX136.7", "V650_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX137.0", "V650_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX137.1", "V650_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX137.2", "V650_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW138", "V650_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW140", "V650_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX142.0", "V650_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX142.1", "V650_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX142.2", "V650_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX142.3", "V660_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX142.4", "V660_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX142.5", "V660_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX142.6", "V660_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX142.7", "V660_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX143.0", "V660_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX143.1", "V660_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX143.2", "V660_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW144", "V660_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW146", "V660_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX148.0", "V660_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX148.1", "V660_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX148.2", "V660_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX148.3", "V670_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX148.4", "V670_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX148.5", "V670_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX148.6", "V670_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX148.7", "V670_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX149.0", "V670_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX149.1", "V670_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX149.2", "V670_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW150", "V670_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW152", "V670_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX154.0", "V670_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX154.1", "V670_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX154.2", "V670_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX154.3", "V680_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX154.4", "V680_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX154.5", "V680_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX154.6", "V680_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX154.7", "V680_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX155.0", "V680_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX155.1", "V680_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX155.2", "V680_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW156", "V680_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW158", "V680_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX160.0", "V680_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX160.1", "V680_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX160.2", "V680_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX160.3", "V690_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX160.4", "V690_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX160.5", "V690_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX160.6", "V690_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX160.7", "V690_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX161.0", "V690_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX161.1", "V690_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX161.2", "V690_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW162", "V690_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW164", "V690_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX166.0", "V690_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX166.1", "V690_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX166.2", "V690_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX166.3", "V700_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX166.4", "V700_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX166.5", "V700_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX166.6", "V700_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX166.7", "V700_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX167.0", "V700_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX167.1", "V700_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX167.2", "V700_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW168", "V700_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW170", "V700_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX172.0", "V700_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX172.1", "V700_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX172.2", "V700_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX172.3", "V710_ModeManuAuto", "BOOL", "");
            glb_plc.AddList("DB50.DBX172.4", "V710_Opened", "BOOL", "");
            glb_plc.AddList("DB50.DBX172.5", "V710_Closed", "BOOL", "");
            glb_plc.AddList("DB50.DBX172.6", "V710_CMDAutoOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX172.7", "V710_CMDAutoClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX173.0", "V710_CMDManuOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX173.1", "V710_CMDManuClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX173.2", "V710_ResetFehler", "BOOL", "");
            glb_plc.AddList("DB50.DBW174", "V710_Laufzeit", "INT", "");
            glb_plc.AddList("DB50.DBW176", "V710_State", "INT", "", true);
            glb_plc.AddList("DB50.DBX178.0", "V710_Fehler", "BOOL", "");
            glb_plc.AddList("DB50.DBX178.1", "V710_CMDOpen", "BOOL", "");
            glb_plc.AddList("DB50.DBX178.2", "V710_CMDClose", "BOOL", "");
            glb_plc.AddList("DB50.DBX178.3", "F600_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX178.4", "F601_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX178.5", "F602_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX178.6", "F603_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX178.7", "F604_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX179.0", "F605_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX179.1", "F606_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX179.2", "F607_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX179.3", "F608_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX179.4", "F609_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX179.5", "F610_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX179.6", "F611_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX179.7", "F612_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX180.0", "F613_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX180.1", "F614_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX180.2", "F615_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX180.3", "F616_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX180.4", "F617_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX180.5", "F618_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX180.6", "F619_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX180.7", "F620_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX181.0", "F621_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX181.1", "F622_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX181.2", "F623_FlowOffOn", "BOOL", "");
            glb_plc.AddList("DB50.DBX181.3", "V_All_Auto", "BOOL", "Alle Schieber auf Automatik");
            glb_plc.AddList("DB50.DBX181.4", "V_All_Manu", "BOOL", "Alle Schieber auf Manuell");
            glb_plc.AddList("DB50.DBX181.5", "V_All_Off", "BOOL", "Alle Schieber Automatik Kommandos reseten");
            glb_plc.AddList("DB50.DBX181.6", "T1_CMDAutoObenActivate", "BOOL", "Behaelter: Zylinder Oben Luftkanal oeffnen im Mode Automatik");
            glb_plc.AddList("DB50.DBX181.7", "T1_CMDManuObenActivate", "BOOL", "Behaelter: Zylinder Oben Luftkanal oeffnen im Mode Manuell");
            glb_plc.AddList("DB50.DBX182.0", "T1_CMDAutoUntenActivate", "BOOL", "Behaelter: Zylinder Unten Luftkanal oeffnen im Mode Automatik");
            glb_plc.AddList("DB50.DBX182.1", "T1_CMDManuUntenActivate", "BOOL", "Behaelter: Zylinder Unten Luftkanal oeffnen im Mode Manuell");
            glb_plc.AddList("DB50.DBX182.2", "T1_Oben", "BOOL", "Behaelter: Reedkontakt oben");
            glb_plc.AddList("DB50.DBX182.3", "T1_Unten", "BOOL", "Behaelter: Reedkontakt unten");
            glb_plc.AddList("DB50.DBX182.4", "T1_ModeManuAuto", "BOOL", "Behaelter: Mode Automatik Manuell");
            glb_plc.AddList("DB50.DBW184", "T1_State", "INT", "Behaelter: Status", true);
            glb_plc.AddList("DB50.DBX186.0", "T1_CMDObenActivate", "BOOL", "Behaelter: Zylinder Oben Luftkanal oeffnen");
            glb_plc.AddList("DB50.DBX186.1", "T1_CMDUntenActivate", "BOOL", "Behaelter: Zylinder Unten Luftkanal oeffnen");



        }

        private void BindConrols()
        {
            glb_DataBinding.AddList(this.Name, this.TXT_Watchdog.Name.ToString(), "Text", "DB52.PLC_WatchDog");

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Dispose();
        }

        private void FrmDatenlogger_Load(object sender, EventArgs e)
        {
            List<SQLHelper.SQLSpalten> felder = new List<SQLHelper.SQLSpalten>();
            SQLHelper.SQLSpalten feld;

            feld.Feldname = "Time";
            feld.Feldtype = "Time(7)";
            felder.Add(feld);
            feld.Feldname = "Data";
            feld.Feldtype = "date";
            felder.Add(feld);

            foreach (PLCDatas plcData in glb_plc.Daten)
            {
                if (plcData.DataLog == true)
                {
                    feld.Feldname = plcData.Symbolname;
                    feld.Feldtype = "float";
                    felder.Add(feld);
                }
            }
            sqlHelper.CheckTableSpalten("MaschDaten", felder);

            felder.Clear();
            felder = null;
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            glb_plc.Read();
            glb_DataBinding.Dispatch();
            this.UpdateListView();
        }

        private void BTN_Menu_Click(object sender, EventArgs e)
        {
            this.ShowMenu();
        }

        private void UpdateListView()
        {
            List<SQLHelper.SQLWerte>daten=new List<SQLHelper.SQLWerte>();
            SQLHelper.SQLWerte data;
            listView1.BeginUpdate();
            int id = 0;

            DateTime timestamp = DateTime.Now;


            string uhrzeit = "'" + timestamp.ToLongTimeString()+"'";

            data.Feldname = "Time";
            data.Feldtype = "";
            data.Wert = uhrzeit;
            daten.Add(data);


            string datum = "'" + timestamp.Year + "-" + timestamp.Month + "-" + timestamp.Day +"'";

            data.Feldname = "Date";
            data.Feldtype = "";
            data.Wert = datum;
            daten.Add(data);


            foreach(PLCDatas val in glb_plc.Daten)
            {
                if (val.DataLog == true)
                {

                    string db = val.DatabaseNumber.ToString();
                    string sm = val.Symbolname;

                    string symbolName = "DB" + db + "." + sm;
                    string wert = glb_plc.DatabasesValues[symbolName].ToString();

                    listView1.Items[id].SubItems[3].Text = wert;


                    data.Feldname = val.Symbolname;
                    data.Feldtype = "float";
                    data.Wert = wert;
                    daten.Add(data);
                    id++;
                }
            }
            listView1.EndUpdate();
            sqlHelper.InsertData("MaschDaten", daten);
        }

        private void CreateListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.CheckBoxes = false;

            listView1.Columns.Add("Symbol", 200);
            listView1.Columns.Add("Kommentar", 300);
            listView1.Columns.Add("Type", 50);
            listView1.Columns.Add("Value", 100);

            string[] arr = new string[4];
            ListViewItem itm;

            foreach (PLCDatas val in glb_plc.Daten)
            {
                if (val.DataLog == true)
                {
                    arr[0] = val.Symbolname;
                    arr[1] = val.Comment;
                    arr[2] = val.DataType;
                    arr[3] = "0.0";
                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                    itm = null;
                }
            }
        }

        private void GB_Datenausgabe_Enter(object sender, EventArgs e)
        {

        }
    }
}
