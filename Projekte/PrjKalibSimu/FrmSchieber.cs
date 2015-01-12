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

namespace PrjKalibSimu
{
    public partial class FrmSchieber : FrmVorlage
    {
        private ClsFormularManager frm_Mng;
        private ClsPLC glb_plc;
        private ClsDataBinding glb_DataBinding;

        public FrmSchieber()
        {
            InitializeComponent();
            frm_Mng = ClsFormularManager.CreateInstance();
            glb_plc=ClsPLC.CreateInstance();
            glb_DataBinding = ClsDataBinding.CreateInstance();
        }

        private void FrmSchieber_Load(object sender, EventArgs e)
        {
            double opacity = this.Opacity * 100.0;
            this.hScrollBar1.Value = (int)opacity;
        }

        public void BindDatas(string ValveID)
        {
            glb_DataBinding.AddList(this.Name, "LEDOpen", "State", "DB50.V" + ValveID + "_CMDManuOpen");
            glb_DataBinding.AddList(this.Name, "LEDClose", "State", "DB50.V" + ValveID + "_CMDManuClose");


            glb_DataBinding.AddList(this.Name, "LEDMode", "State", "DB50.V" + ValveID + "_ModeManuAuto");

            glb_DataBinding.AddList(this.Name, "TXTState", "Text", "DB50.V" + ValveID + "_State");
            glb_DataBinding.AddList(this.Name, "ValveState", "State", "DB50.V" + ValveID + "_State");
            glb_DataBinding.AddList(this.Name, "ValveState", "Mode", "DB50.V" + ValveID + "_ModeManuAuto");


            this.BTNOpen.Symbol = "DB50.V" + ValveID + "_CMDManuOpen";
            this.BTN_Close.Symbol = "DB50.V" + ValveID + "_CMDManuClose";
            this.BTN_Mode.Symbol = "DB50.V" + ValveID + "_ModeManuAuto";
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

        private void BtnDown(object sender, MouseEventArgs e)
        {
            glb_plc.Write(FuncPLC.SymbolName(sender), "1");
        }

        private void BtnUp(object sender, MouseEventArgs e)
        {
            glb_plc.Write(FuncPLC.SymbolName(sender), "0");
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
