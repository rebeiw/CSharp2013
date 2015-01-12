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
    public partial class FrmBehaelter : Helper.FrmVorlage
    {
        private ClsFormularManager frm_Mng;
        private ClsPLC glb_plc;
        private ClsDataBinding glb_DataBinding;

        public FrmBehaelter()
        {
            InitializeComponent();
            frm_Mng = ClsFormularManager.CreateInstance();
            glb_plc = ClsPLC.CreateInstance();
            glb_DataBinding = ClsDataBinding.CreateInstance();
        }
        private void FrmBehaelter_Load(object sender, EventArgs e)
        {
            double opacity = this.Opacity * 100.0;
            this.hScrollBar1.Value = (int)opacity;
        }
        public void BindDatas(string BehaelterID)
        {
            glb_DataBinding.AddList(this.Name, "LEDOben", "State", "DB50.T" + BehaelterID + "_CMDManuObenActivate");
            glb_DataBinding.AddList(this.Name, "LEDUnten", "State", "DB50.T" + BehaelterID + "_CMDManuUntenActivate");


            glb_DataBinding.AddList(this.Name, "LEDMode", "State", "DB50.T" + BehaelterID + "_ModeManuAuto");

            glb_DataBinding.AddList(this.Name, "TXTState", "Text", "DB50.T" + BehaelterID + "_State");
            glb_DataBinding.AddList(this.Name, "LifterState", "State", "DB50.T" + BehaelterID + "_State");
            glb_DataBinding.AddList(this.Name, "ValveState", "Mode", "DB50.T" + BehaelterID + "_ModeManuAuto");


            this.BTN_Oben.Symbol = "DB50.T" + BehaelterID + "_CMDManuObenActivate";
            this.BTN_Unten.Symbol = "DB50.T" + BehaelterID + "_CMDManuUntenActivate";
            this.BTN_Mode.Symbol = "DB50.T" + BehaelterID + "_ModeManuAuto";
        }

        private void Toggle(object sender, EventArgs e)
        {
            bool wert = false;
            string symbolName = FuncPLC.SymbolName(sender);
            wert = glb_plc.ReadBool(symbolName);
            if (wert == false)
            {
                glb_plc.Write(symbolName, "1");
            }
            else
            {
                glb_plc.Write(symbolName, "0");
            }
        }
        private void BTN_FrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            double slider = (double)this.hScrollBar1.Value;
            this.Opacity = slider / 100.0;
        }

    }
}
