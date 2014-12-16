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

namespace ClassDevelopment
{
    public struct PlcParameter
    {
        public string IP;
        public int Rack;
        public int Slot;
        public ProgressBar ProgBar;
    }

    public struct PLCDatas
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

    public class ClsPlc
    {
        private Hashtable m_DatabasesInfo;
        private Hashtable m_SymbolInfo;

        private List<int> m_DatabasesNr;

        private Hashtable m_DatabasesValues;

        public List<PLCDatas> m_Daten;

        private List<string> m_Value;

        private ClsVarCollect Glb_VarCollect;

        private ClsDataBinding Glb_DataBinding;

        private BackgroundWorker m_BackgroundWorkerPlcRead;

        private ProgressBar m_ProgressBar;

        private System.Windows.Forms.Timer m_TimerRead;

        private static ClsPlc instance;

        private libnodave.daveOSserialType m_Fds;
        private libnodave.daveInterface m_Di;
        private libnodave.daveConnection m_Dc;

        private int m_LocalMPI = 0;

        private string m_IP;
        private int m_Rack;
        private int m_Slot;

        private ClsPlc(PlcParameter PlcParameter)
        {

            this.m_DatabasesInfo = new Hashtable();
            this.m_DatabasesNr = new List<int>();
            this.m_SymbolInfo = new Hashtable();
            this.m_DatabasesValues = new Hashtable();
 

            this.m_Daten=new List<PLCDatas>();
            this.m_Value = new List<string>();
            this.m_TimerRead = new System.Windows.Forms.Timer();
            this.m_TimerRead.Enabled = false;
            this.m_TimerRead.Interval = 100;
            this.m_TimerRead.Tick+=m_TimerRead_Tick;

            this.m_IP = PlcParameter.IP;
            this.m_Rack = PlcParameter.Rack;
            this.m_Slot = PlcParameter.Slot;

            this.m_ProgressBar = PlcParameter.ProgBar;
            this.m_ProgressBar.Style = ProgressBarStyle.Blocks;
            this.m_ProgressBar.Value = 0;
            
            this.m_BackgroundWorkerPlcRead = new BackgroundWorker();
            this.m_BackgroundWorkerPlcRead.WorkerReportsProgress = true;
            this.m_BackgroundWorkerPlcRead.WorkerSupportsCancellation = true;
            this.m_BackgroundWorkerPlcRead.DoWork+=m_BackgroundWorkerPlcRead_DoWork;
            this.m_BackgroundWorkerPlcRead.ProgressChanged+=m_BackgroundWorkerPlcRead_ProgressChanged;
            this.m_BackgroundWorkerPlcRead.RunWorkerCompleted+=m_BackgroundWorkerPlcRead_RunWorkerCompleted;
            
            Glb_VarCollect = ClsVarCollect.CreateInstance();
            Glb_DataBinding = ClsDataBinding.CreateInstance();

        }

        public static ClsPlc CreateInstance(PlcParameter PlcParameter)
        {
            if (instance == null)
            {
                instance = new ClsPlc(PlcParameter);
            }
            return instance;
        }

        private void m_TimerRead_Tick(object sender, EventArgs e)
        {
            if (this.m_BackgroundWorkerPlcRead.IsBusy == false)
            {
                this.m_ProgressBar.Style = ProgressBarStyle.Marquee;
                this.m_BackgroundWorkerPlcRead.RunWorkerAsync();
            }
        }

        private void m_BackgroundWorkerPlcRead_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (int dbNr in this.m_DatabasesNr)
            {
                int anz = Convert.ToInt32(this.m_DatabasesInfo[dbNr]);
                byte[] daten = new byte[anz];
                this.m_Dc.readManyBytes(libnodave.daveDB, dbNr, 0, anz, daten);
                if ((sender as BackgroundWorker).CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                PLCDatas pclData;

                for (int i = 0; i < this.m_Daten.Count(); i++)
                {
                    pclData = this.m_Daten[i];
                    if (pclData.DatabaseNumber == dbNr)
                    {
                        string searchSymbol = "DB" + dbNr.ToString() + "." + pclData.Symbolname;
                        string value = "";
                        int byteNumber = pclData.ByteNumber;
                        double bitNumber = (double)pclData.BitNumber;
                        if (pclData.DataType == "BOOL")
                        {
                            int wert = (int)Math.Pow(2.0, bitNumber);
                            int byteWert = daten[byteNumber];
                            int valence = byteWert & wert;
                            if (valence > 0)
                            {
                                value = "1";
                            }
                            else
                            {
                                value = "0";
                            }
                        }
                        if (pclData.DataType == "INT")
                        {
                            int wert = daten[byteNumber + 0] * 256 + daten[byteNumber + 1];
                            value = wert.ToString();

                        }
                        if (pclData.DataType == "TIME")
                        {
                            int lowByteWert = daten[byteNumber + 1];
                            int highByteWert = daten[byteNumber + 0];
                            int wert = daten[byteNumber + 0] * 256 * 256 * 256 + daten[byteNumber + 1] * 256 * 256 + daten[byteNumber + 2] * 256 + daten[byteNumber + 3];
                            value = wert.ToString();
                        }

                        if (pclData.DataType == "REAL")
                        {
                            value = libnodave.getFloatfrom(daten, byteNumber).ToString();
                        }

                        if (m_DatabasesValues.ContainsKey(searchSymbol))
                        {
                            m_DatabasesValues[searchSymbol] = value;
                        }
                        else
                        {
                            m_DatabasesValues.Add(searchSymbol, value);
                        }
                    }
                }
            }

            this.m_Value.Clear();
            int fort = 0;
            int j = Glb_VarCollect.ReadValueInt("Variable1");
            j++;
            Glb_VarCollect.WriteValue("Variable1", j);
            Glb_VarCollect.WriteValue("Variable2", (double)j + 100);
            (sender as BackgroundWorker).ReportProgress(fort);
            this.m_Value.Add("Variable1");
            this.m_Value.Add("Variable2");
            e.Result = this.m_Value;

        }
        private void m_BackgroundWorkerPlcRead_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.m_ProgressBar.Value = e.ProgressPercentage;
        }

        private void m_BackgroundWorkerPlcRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                int asd = 0;
                asd++;
            }
            else
            {
                if (e.Error!=null)
                {
                    int asd = 0;
                    asd++;
                }
                else
                {
                    List<string> var_list = (List<string>)e.Result;
                    foreach (string var in var_list)
                    {
                        Glb_DataBinding.Dispatch(var);
                    }
                }
            }
            GC.Collect();
        }

        public void StartRead()
        {
            //this.m_Fds = new libnodave.daveOSserialType();
            this.m_Fds.rfd = libnodave.openSocket(102, this.m_IP);
            this.m_Fds.wfd = m_Fds.rfd;
            this.m_Di = new libnodave.daveInterface(this.m_Fds, "PLC", this.m_LocalMPI, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
            this.m_Di.setTimeout(5000);
            this.m_Dc = new libnodave.daveConnection(m_Di, this.m_LocalMPI, this.m_Rack, this.m_Slot);

            int retval = this.m_Dc.connectPLC();
            this.m_TimerRead.Enabled = true;
        }

        public void StoppRead()
        {
            this.m_BackgroundWorkerPlcRead.CancelAsync();
            this.m_TimerRead.Enabled = false;

            this.m_ProgressBar.Style = ProgressBarStyle.Blocks;
            if (this.m_Dc != null)
            {
                this.m_Dc.disconnectPLC();
                this.m_Dc = null;
                this.m_Di = null;
            }
        }

        public void AddList(string Adress, string Symbolname, string DataType, string Comment,bool DataLog=false)
        {
            int byte_laenge = 0;
            if (DataType == "BOOL")
            {
                byte_laenge = 2;
            }
            if (DataType == "INT")
            {
                byte_laenge = 2;
            }
            if (DataType == "TIME" || DataType == "REAL")
            {
                byte_laenge = 4;
            }
            int db_nr = 0;
            int byte_nr = 0;
            int bit_nr = 0;
            string[] adress = Adress.Split('.');
            if (adress.Count() >= 2)
            {
                db_nr=Convert.ToInt32(FuncString.GetOnlyNumeric(adress[0]));
                byte_nr = Convert.ToInt32(FuncString.GetOnlyNumeric(adress[1]));
                if (adress.Count() == 3)
                {
                    bit_nr = Convert.ToInt32(FuncString.GetOnlyNumeric(adress[2]));
                    int rest=byte_nr % 2;
                    byte_laenge = byte_laenge - rest;
                    
                }
                if (this.m_DatabasesInfo.ContainsKey(db_nr))
                {
                    int value = Convert.ToInt32(this.m_DatabasesInfo[db_nr]);
                    if ((byte_nr+byte_laenge)>value)
                    {
                        this.m_DatabasesInfo[db_nr] = byte_nr + byte_laenge;
                    }
                }
                else
                {
                    this.m_DatabasesInfo.Add(db_nr, byte_nr);
                    this.m_DatabasesNr.Add(db_nr);
                }
            }
            PLCDatas data;
            data.Adress = Adress;
            data.DatabaseNumber = db_nr;
            data.ByteNumber = byte_nr;
            data.BitNumber = bit_nr;
            data.Symbolname = Symbolname;
            data.DataType = DataType;
            data.Comment = Comment;
            data.Value = "0.0";
            data.DataLog = DataLog;
            this.m_Daten.Add(data);

            string keyInfo = "DB" + db_nr + "." + Symbolname;
            if (!this.m_SymbolInfo.ContainsKey(keyInfo))
            {
                this.m_SymbolInfo.Add(keyInfo, data);
            }

        }
    }
}
