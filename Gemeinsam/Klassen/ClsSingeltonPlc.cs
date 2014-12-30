using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;

namespace Helper
{
    public struct ClsSingeltonPlcParameter
    {
        public string IP;
        public int Rack;
        public int Slot;                        
        public object ProgBar;
        public object LblStatus;
        public object BtnStart;
        public object BtnStopp;
    }

    public struct ClsSingeltonPlcDatas
    {
        public string Adress;
        public int DatabaseNumber;
        public int ByteNumber;
        public int BitNumber;
        public string Symbolname;
        public string DataType;
        public string Value;
        public string Comment;
        public bool DataLog;
    }

    public class ClsSingeltonPlc
    {
        public List<ClsSingeltonPlcDatas> m_Daten;

        private byte[] m_DatenBytes;

        private bool m_FlagGoOn;
        private Hashtable m_DatabasesInfo;
        private Hashtable m_SymbolInfo;

        private List<int> m_DatabasesNr;

        private Hashtable m_DatabasesValues;

        private Hashtable m_InfoThread;


        private List<string> m_Value;

        private ClsSingeltonVariablesCollecter m_VarCollect;

        private ClsSingeltonDataBinding m_DataBinding;

        private BackgroundWorker m_BackgroundWorkerPlcRead;

        private object m_ProgressBar;
        private object m_LblStatus;
        private object m_BtnStart;
        private object m_BtnStopp;

        private System.Windows.Forms.Timer m_TimerRead;

        private static ClsSingeltonPlc m_instance;

        private libnodave.daveOSserialType m_Fds;
        private libnodave.daveInterface m_Di;
        private libnodave.daveConnection m_Dc;

        private int m_LocalMPI = 0;

        private string m_IP;
        private int m_Rack;
        private int m_Slot;

        private ClsSingeltonPlc(ClsSingeltonPlcParameter PlcParameter)
        {

            this.m_DatenBytes = new byte[0];

            this.m_FlagGoOn = false;

            this.m_DatabasesInfo = new Hashtable();
            this.m_DatabasesNr = new List<int>();
            this.m_SymbolInfo = new Hashtable();
            this.m_DatabasesValues = new Hashtable();

            this.m_InfoThread = new Hashtable();
 

            this.m_Daten=new List<ClsSingeltonPlcDatas>();
            this.m_Value = new List<string>();
            this.m_TimerRead = new System.Windows.Forms.Timer();
            this.m_TimerRead.Enabled = false;
            this.m_TimerRead.Interval = 100;
            this.m_TimerRead.Tick+=TimerRead_Tick;

            this.m_IP = PlcParameter.IP;
            this.m_Rack = PlcParameter.Rack;
            this.m_Slot = PlcParameter.Slot;

            this.m_ProgressBar = PlcParameter.ProgBar;

            this.m_LblStatus = PlcParameter.LblStatus;
            this.m_BtnStart = PlcParameter.BtnStart;
            this.m_BtnStopp = PlcParameter.BtnStopp;
            
            this.m_BackgroundWorkerPlcRead = new BackgroundWorker();
            this.m_BackgroundWorkerPlcRead.WorkerReportsProgress = true;
            this.m_BackgroundWorkerPlcRead.WorkerSupportsCancellation = true;
            this.m_BackgroundWorkerPlcRead.DoWork+=BackgroundWorkerPlcRead_DoWork;
            this.m_BackgroundWorkerPlcRead.ProgressChanged+=BackgroundWorkerPlcRead_ProgressChanged;
            this.m_BackgroundWorkerPlcRead.RunWorkerCompleted+=BackgroundWorkerPlcRead_RunWorkerCompleted;
            
            m_VarCollect = ClsSingeltonVariablesCollecter.CreateInstance();
            m_DataBinding = ClsSingeltonDataBinding.CreateInstance();

            this.ButtonVisibleOnOff(this.m_BtnStart, true);
            this.ButtonVisibleOnOff(this.m_BtnStopp, false);
            this.SetInfo(this.m_LblStatus, "Wait..");

        }

        public static ClsSingeltonPlc CreateInstance(ClsSingeltonPlcParameter PlcParameter)
        {
            if (m_instance == null)
            {
                m_instance = new ClsSingeltonPlc(PlcParameter);
            }
            return m_instance;
        }

        private void TimerRead_Tick(object sender, EventArgs e)
        {
            this.m_TimerRead.Enabled = false;
            this.m_BackgroundWorkerPlcRead.RunWorkerAsync();
        }


        private void BackgroundWorkerPlcRead_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter_progress = 0;
            int progress_value = 0;
            this.m_Value.Clear();
            foreach (int dbNr in this.m_DatabasesNr)
            {
                this.m_InfoThread[1] = "DB " + dbNr + " ..";
                this.m_BackgroundWorkerPlcRead.ReportProgress(1);
                int anz = Convert.ToInt32(this.m_DatabasesInfo[dbNr]);
                Array.Resize(ref this.m_DatenBytes, anz);
                int retval = this.m_Dc.readManyBytes(libnodave.daveDB, dbNr, 0, anz, this.m_DatenBytes);
                if(retval<0)
                {
                    Thread.Sleep(500);
                    this.InitPLC();
                }
                else
                {
                    ClsSingeltonPlcDatas pclData;
                    for (int i = 0; i < this.m_Daten.Count(); i++)
                    {
                        pclData = this.m_Daten[i];
                        if (pclData.DatabaseNumber == dbNr)
                        {
                            counter_progress++;
                            progress_value = 100 * counter_progress / this.m_Daten.Count();
                            this.m_InfoThread[2] = progress_value;
                            this.m_BackgroundWorkerPlcRead.ReportProgress(2);
                            string searchSymbol = "DB" + dbNr.ToString() + "." + pclData.Symbolname;
                            string value = "";
                            int byteNumber = pclData.ByteNumber;
                            double bitNumber = (double)pclData.BitNumber;
                            if (pclData.DataType == "BOOL")
                            {
                                int wert = (int)Math.Pow(2.0, bitNumber);
                                int byteWert = this.m_DatenBytes[byteNumber];
                                int valence = byteWert & wert;
                                if (valence > 0)
                                {
                                    value = "1";
                                }
                                else
                                {
                                    value = "0";
                                }
                                int convert_value = 0;
                                convert_value = Convert.ToInt32(value);
                                this.m_VarCollect.WriteValue(searchSymbol, convert_value);
                            }
                            if (pclData.DataType == "INT")
                            {
                                int wert = this.m_DatenBytes[byteNumber + 0] * 256 + this.m_DatenBytes[byteNumber + 1];
                                this.m_VarCollect.WriteValue(searchSymbol, wert);
                                value = wert.ToString();
                            }
                            if (pclData.DataType == "TIME")
                            {
                                int wert = this.m_DatenBytes[byteNumber + 0] * 256 * 256 * 256 + 
                                           this.m_DatenBytes[byteNumber + 1] * 256 * 256 + 
                                           this.m_DatenBytes[byteNumber + 2] * 256 + 
                                           this.m_DatenBytes[byteNumber + 3];
                                value = wert.ToString();
                            }

                            if (pclData.DataType == "REAL")
                            {
                                double convert_value=0.0;
                                value = libnodave.getFloatfrom(this.m_DatenBytes, byteNumber).ToString();
                                double.TryParse(value, out convert_value);
                                this.m_VarCollect.WriteValue(searchSymbol, convert_value);
                            }

                            if (m_DatabasesValues.ContainsKey(searchSymbol))
                            {
                                if (m_DatabasesValues[searchSymbol] != value)
                                {
                                    this.m_Value.Add(searchSymbol);
                                }
                                m_DatabasesValues[searchSymbol] = value;
                            }
                            else
                            {
                                m_DatabasesValues.Add(searchSymbol, value);
                                this.m_Value.Add(searchSymbol);
                            }
                        }
                    }
                }
            }
            this.m_BackgroundWorkerPlcRead.ReportProgress(0);
            e.Result = this.m_Value;
        }

        private void BackgroundWorkerPlcRead_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                this.SetInfo(this.m_LblStatus, (string)this.m_InfoThread[e.ProgressPercentage]);
            }
            if (e.ProgressPercentage == 2)
            {
                this.SetProgressbar(this.m_ProgressBar, (int)this.m_InfoThread[e.ProgressPercentage]);
            }
        }

        private void BackgroundWorkerPlcRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!this.m_FlagGoOn)
            {
                this.m_TimerRead.Enabled = false;
                this.DeletePLC();
                this.SetInfo(this.m_LblStatus, "Canc..");
                this.ButtonVisibleOnOff(this.m_BtnStart, true);
                this.SetProgressbar(this.m_ProgressBar, 0);

            }
            foreach (string var in (List<string>)e.Result)
            {
                this.m_DataBinding.Dispatch(var);
            }
            this.m_TimerRead.Enabled = this.m_FlagGoOn;
        }

        private void SetInfo(object labelInfo, string message)
        {
            string obj_type = labelInfo.GetType().ToString();
            if (obj_type == "System.Windows.Forms.Label")
            {
                Label label = (Label)labelInfo;
                label.Text = message;
            }
        }
        private void SetProgressbar(object progressBar, int value)
        {
            string obj_type = progressBar.GetType().ToString();
            if (obj_type == "System.Windows.Forms.ProgressBar")
            {
                ProgressBar progress_bar = (ProgressBar)progressBar;
                progress_bar.Value = value;
            }
        }


        private void ButtonVisibleOnOff(object button, bool visible)
        {
            string obj_type = button.GetType().ToString();
            if (obj_type == "System.Windows.Forms.Button")
            {
                Button btn = (Button)button;
                btn.Visible = visible;
                if(visible)
                {
                    btn.Focus();
                }
            }
            if (obj_type == "Helper.CompBitButton")
            {
                CompBitButton btn = (CompBitButton)button;
                btn.Visible = visible;
                if (visible)
                {
                    btn.Focus();
                }
            }
        }

        public void StartRead()
        {
            int retval = 0;
            this.m_FlagGoOn = true;
            this.ButtonVisibleOnOff(this.m_BtnStart, false);
            this.ButtonVisibleOnOff(this.m_BtnStopp, true);
            do
            {
                retval=this.InitPLC();
            } while (retval < 0);
            this.m_TimerRead.Enabled = true;
        }

        private int InitPLC()
        {
            int retval = 0;
            this.DeletePLC();
            this.m_Fds.rfd = libnodave.openSocket(102, this.m_IP);
            this.m_Fds.wfd = this.m_Fds.rfd;
            this.m_Di =new libnodave.daveInterface(this.m_Fds, "PLC", this.m_LocalMPI, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
            this.m_Di.setTimeout(5000);
            this.m_Dc = new libnodave.daveConnection(this.m_Di, this.m_LocalMPI, this.m_Rack, this.m_Slot);

            retval = this.m_Dc.connectPLC();
            return retval;
        }

        private void DeletePLC()
        {
            if (this.m_Dc != null)
            {
                this.m_Di.disconnectAdapter();
                this.m_Dc.disconnectPLC();
                libnodave.closeSocket(this.m_Fds.rfd);
                this.m_Dc = null;
                this.m_Di = null;
            }
        }

        public void StoppRead()
        {
            this.m_FlagGoOn = false;
            this.ButtonVisibleOnOff(this.m_BtnStart, false);
            this.ButtonVisibleOnOff(this.m_BtnStopp, false);
            this.SetProgressbar(this.m_ProgressBar, 0);
        }
        public void AddList(string adresse, string symbolName, string dataType, string comment, bool dataLog=false)
        {
            int byte_length = 0;
            ClsSingeltonVariablesCollecterDataType data_type = ClsSingeltonVariablesCollecterDataType.Null;
            if (dataType == "BOOL")
            {
                byte_length = 2;
                data_type = ClsSingeltonVariablesCollecterDataType.Int;
            }
            if (dataType == "INT")
            {
                byte_length = 2;
                data_type = ClsSingeltonVariablesCollecterDataType.Int;
            }
            if (dataType == "TIME")
            {
                byte_length = 4;
                data_type = ClsSingeltonVariablesCollecterDataType.Int;
            }
            if (dataType == "REAL")
            {
                byte_length = 4;
                data_type = ClsSingeltonVariablesCollecterDataType.Double;
            }

            int db_nr = 0;
            int byte_nr = 0;
            int bit_nr = 0;
            string[] adressen = adresse.Split('.');
            if (adressen.Count() >= 2)
            {
                db_nr=Convert.ToInt32(FuncString.GetOnlyNumeric(adressen[0]));
                byte_nr = Convert.ToInt32(FuncString.GetOnlyNumeric(adressen[1]));
                if (adressen.Count() == 3)
                {
                    bit_nr = Convert.ToInt32(FuncString.GetOnlyNumeric(adressen[2]));
                    int rest=byte_nr % 2;
                    byte_length = byte_length - rest;
                }
                if (this.m_DatabasesInfo.ContainsKey(db_nr))
                {
                    int value = Convert.ToInt32(this.m_DatabasesInfo[db_nr]);
                    if ((byte_nr+byte_length)>value)
                    {
                        this.m_DatabasesInfo[db_nr] = byte_nr + byte_length;
                    }
                }
                else
                {
                    this.m_DatabasesInfo.Add(db_nr, byte_nr);
                    this.m_DatabasesNr.Add(db_nr);
                }
            }
            ClsSingeltonPlcDatas data;
            data.Adress = adresse;
            data.DatabaseNumber = db_nr;
            data.ByteNumber = byte_nr;
            data.BitNumber = bit_nr;
            data.Symbolname = symbolName;
            data.DataType = dataType;
            data.Comment = comment;
            data.Value = "0.0";
            data.DataLog = dataLog;
            this.m_Daten.Add(data);
            string keyInfo = "DB" + db_nr + "." + symbolName;
            this.m_VarCollect.CreateVariable(keyInfo, data_type);
            if (!this.m_SymbolInfo.ContainsKey(keyInfo))
            {
                this.m_SymbolInfo.Add(keyInfo, data);
            }
        }
    }
}
