using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Helper
{
    public class ClsDataBinding
    {

        public struct DataBindingList
        {
            public string FormName;
            public string CompName;
            public string Propertie;
            public string SymbolName;
        };
        List<DataBindingList> CompInfomation;

        private static ClsDataBinding instance;
        private ClsPLC Glb_Plc=ClsPLC.CreateInstance();

        private ClsDataBinding()
        {
            CompInfomation = new List<DataBindingList>();
        }
        public static ClsDataBinding CreateInstance()
        {
            if (instance == null)
            {
                instance = new ClsDataBinding();
            }
            return instance;
        }

        public void RemoveList(string FormName, string CompName)
        {

            int index = 0;
            foreach (DataBindingList compInformation in this.CompInfomation)
            {
                if ((compInformation.FormName == FormName) && (compInformation.CompName == CompName))
                {
                    CompInfomation.RemoveAt(index);
                    break;
                }
                index++;
            }
        }

        public void AddList(string FormName, string CompName, string Propertie, string SymbolName)
        {
            DataBindingList data;
            data.FormName = FormName;
            data.CompName = CompName;
            data.Propertie = Propertie;
            data.SymbolName = SymbolName;
            this.CompInfomation.Add(data);
        }
        public void Dispatch()
        {
            foreach (DataBindingList bindingList in this.CompInfomation)
            {
                Form frm = Application.OpenForms[bindingList.FormName];
                if (frm != null)
                {
                    object obj=FuncGeneral.GetControlByName(frm, bindingList.CompName);
                    if (obj != null)
                    {
                        string obj_type = obj.GetType().ToString();
                        if (obj_type == "Helper.Valve")
                        {
                            Valve obj_valve;
                            obj_valve = (Valve)obj;
                            if (bindingList.Propertie == "State")
                            {
                                obj_valve.State = FuncGeneral.GetState(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString());
                            }
                            if (bindingList.Propertie == "Mode")
                            {
                                obj_valve.Mode = FuncGeneral.GetMode(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString());
                            }
                            if (bindingList.Propertie == "Rotation")
                            {
                                double wert = 0.0;
                                double.TryParse(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString(), out wert);
                                obj_valve.Rotation = wert;
                            }

                        }

                        if (obj_type == "Helper.LedRectangle")
                        {
                            CompLedRectangle obj_led_rectangle;
                            obj_led_rectangle = (CompLedRectangle)obj;
                            if (bindingList.Propertie == "State")
                            {
                                obj_led_rectangle.State = FuncGeneral.GetSateLED(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString());
                            }
                        }

                        if (obj_type == "Helper.Pump")
                        {
                            Pump obj_pump;
                            obj_pump = (Pump)obj;
                            if (bindingList.Propertie == "State")
                            {
                                obj_pump.State = FuncGeneral.GetStatePump(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString());
                            }
                        }


                        if (obj_type == "Helper.Lifter")
                        {
                            Lifter obj_lifter;
                            obj_lifter = (Lifter)obj;
                            if (bindingList.Propertie == "State")
                            {
                                obj_lifter.State = FuncGeneral.GetStateLift(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString());
                            }
                        }
                        
                        if (obj_type == "Helper.WaterTank")
                        {
                            WaterTank obj_water_tank;
                            obj_water_tank = (WaterTank)obj;
                            if (bindingList.Propertie == "Value")
                            {
                                double wert = 0.0;
                                double.TryParse(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString(),out wert);
                                obj_water_tank.Value=wert;
                            }
                            if (bindingList.Propertie == "Error")
                            {
                                bool wert = false;
                                if (Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString() != "0")
                                {
                                    wert = true;
                                }
                                obj_water_tank.Error = wert;
                            }
                        }

                        if (obj_type == "Helper.LedRound")
                        {
                            LedRound obj_led_round;
                            obj_led_round = (LedRound)obj;
                            if (bindingList.Propertie == "State")
                            {
                                obj_led_round.State = FuncGeneral.GetSateLED(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString());
                            }
                        }

                        if (obj_type == "Helper.Pipe")
                        {
                            CompPipe obj_pipe;
                            obj_pipe = (CompPipe)obj;
                            if (bindingList.Propertie == "Flow")
                            {
                                obj_pipe.Flow = FuncGeneral.GetSatePipe(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString());
                            }
                        }

                        if (obj_type == "Helper.ToggleSwitch")
                        {
                            ToggleSwitch obj_toogle_switch;
                            obj_toogle_switch = (ToggleSwitch)obj;
                            if (bindingList.Propertie == "State")
                            {
                                obj_toogle_switch.State = FuncGeneral.GetSateToggleSwitch(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString());
                            }
                        }

                        if (obj_type == "Helper.TxtBox")
                        {
                            CompTxtBox obj_txt_box;
                            obj_txt_box = (CompTxtBox)obj;
                            if (bindingList.Propertie == "Text")
                            {
                                obj_txt_box.Text = Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString();
                            }
                        }

                        if (obj_type == "Helper.BitButton")
                        {
                            CompBitButton obj_bit_button;
                            obj_bit_button = (CompBitButton)obj;
                            if (bindingList.Propertie == "PictureNumber")
                            {
                                obj_bit_button.PictureNumber=Convert.ToInt32(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString());
                            }
                        }

                        if (obj_type == "Helper.InputBox")
                        {
                            InputBox obj_input_box;
                            obj_input_box = (Helper.InputBox)obj;
                            if (bindingList.Propertie == "Text")
                            {
                                if (!obj_input_box.Focused)
                                {
                                    double wert = 0.0;
                                    string format = "{0:" + obj_input_box.Format + "}";
                                    bool testen = Double.TryParse(Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString(), out wert);
                                    if (testen)
                                    {
                                        obj_input_box.Text = String.Format(format, wert);
                                    }
                                    else
                                    {
                                        obj_input_box.Text = String.Format(format, wert);
                                    }
                                }
                            }
                        }

                        if (obj_type == "System.Windows.Forms.TextBox")
                        {
                            TextBox obj_text_box;
                            obj_text_box = (TextBox)obj;
                            if (bindingList.Propertie == "Text")
                            {
                                if (!obj_text_box.Focused)
                                {
                                    obj_text_box.Text = Glb_Plc.DatabasesValues[bindingList.SymbolName].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
