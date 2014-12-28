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
        public string IP;                       //!< PLC IP Adresse 
        public int Rack;                        //!< Rack Adresse 300 = 2; 
        public int Slot;                        //!< Slot immer 0
        public ProgressBar ProgBar;             //!< Progressbarbar zur Anzeige des Fortschrittes im Thread
        public Label LblStatus;                 //!< Ausgabe nach
        public Button BtnStart;                 //!< Button zum Starten des Threads
        public Button BtnStopp;                 //!< Button zum Stoppen des Threads
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
        public List<PLCDatas> m_Daten;

        private byte[] m_DatenBytes;

        private bool m_FlagGoOn;
        private Hashtable m_DatabasesInfo;
        private Hashtable m_SymbolInfo;

        private List<int> m_DatabasesNr;

        private Hashtable m_DatabasesValues;

        private Hashtable m_InfoThread;


        private List<string> m_Value;

        private ClsVarCollect Glb_VarCollect;

        private ClsDataBinding Glb_DataBinding;

        private BackgroundWorker m_BackgroundWorkerPlcRead;

        private ProgressBar m_ProgressBar;
        private Label m_LblStatus;
        private Button m_BtnStart;
        private Button m_BtnStopp;

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

            this.m_DatenBytes = new byte[0];

            this.m_FlagGoOn = false;

            this.m_DatabasesInfo = new Hashtable();
            this.m_DatabasesNr = new List<int>();
            this.m_SymbolInfo = new Hashtable();
            this.m_DatabasesValues = new Hashtable();

            this.m_InfoThread = new Hashtable();
 

            this.m_Daten=new List<PLCDatas>();
            this.m_Value = new List<string>();
            this.m_TimerRead = new System.Windows.Forms.Timer();
            this.m_TimerRead.Enabled = false;
            this.m_TimerRead.Interval = 100;
            this.m_TimerRead.Tick+=TimerRead_Tick;

            this.m_IP = PlcParameter.IP;
            this.m_Rack = PlcParameter.Rack;
            this.m_Slot = PlcParameter.Slot;

            this.m_ProgressBar = PlcParameter.ProgBar;
            this.m_ProgressBar.Style = ProgressBarStyle.Blocks;
            this.m_ProgressBar.Value = 0;

            this.m_LblStatus = PlcParameter.LblStatus;
            this.m_BtnStart = PlcParameter.BtnStart;
            this.m_BtnStopp = PlcParameter.BtnStopp;
            
            this.m_BackgroundWorkerPlcRead = new BackgroundWorker();
            this.m_BackgroundWorkerPlcRead.WorkerReportsProgress = true;
            this.m_BackgroundWorkerPlcRead.WorkerSupportsCancellation = true;
            this.m_BackgroundWorkerPlcRead.DoWork+=BackgroundWorkerPlcRead_DoWork;
            this.m_BackgroundWorkerPlcRead.ProgressChanged+=BackgroundWorkerPlcRead_ProgressChanged;
            this.m_BackgroundWorkerPlcRead.RunWorkerCompleted+=BackgroundWorkerPlcRead_RunWorkerCompleted;
            
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

        private void TimerRead_Tick(object sender, EventArgs e)
        {
            this.m_TimerRead.Enabled = false;
            this.m_BackgroundWorkerPlcRead.RunWorkerAsync();
        }

        private void BackgroundWorkerPlcRead_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (int dbNr in this.m_DatabasesNr)
            {
                this.m_InfoThread[1] = "Datenbaustein " + dbNr + " wird eingelesen.";
                this.m_BackgroundWorkerPlcRead.ReportProgress(1);
                int anz = Convert.ToInt32(this.m_DatabasesInfo[dbNr]);
                Array.Resize(ref this.m_DatenBytes, anz);
                //Thread.Sleep(10);
                int retval = this.m_Dc.readManyBytes(libnodave.daveDB, dbNr, 0, anz, this.m_DatenBytes);
                if(retval<0)
                {
                    Thread.Sleep(500);
                    this.InitPLC();
                }
                else
                {
                    PLCDatas pclData;
                    for (int i = 0; i < this.m_Daten.Count(); i++)
                    {
                        this.m_InfoThread[2] = i;
                        this.m_BackgroundWorkerPlcRead.ReportProgress(2);
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
                            }
                            if (pclData.DataType == "INT")
                            {
                                int wert = this.m_DatenBytes[byteNumber + 0] * 256 + this.m_DatenBytes[byteNumber + 1];
                                value = wert.ToString();
                            }
                            if (pclData.DataType == "TIME")
                            {
                                int lowByteWert = this.m_DatenBytes[byteNumber + 1];
                                int highByteWert = this.m_DatenBytes[byteNumber + 0];
                                int wert = this.m_DatenBytes[byteNumber + 0] * 256 * 256 * 256 + this.m_DatenBytes[byteNumber + 1] * 256 * 256 + this.m_DatenBytes[byteNumber + 2] * 256 + this.m_DatenBytes[byteNumber + 3];
                                value = wert.ToString();
                            }

                            if (pclData.DataType == "REAL")
                            {
                                value = libnodave.getFloatfrom(this.m_DatenBytes, byteNumber).ToString();
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
            }

            

            this.m_Value.Clear();
            int j = Glb_VarCollect.ReadValueInt("Variable1");
            j++;
            Glb_VarCollect.WriteValue("Variable1", j);
            object www = m_DatabasesValues["DB59.P1_Qmax_3"];
            double eee = Convert.ToDouble(www);
            Glb_VarCollect.WriteValue("Variable2", eee);
            this.m_BackgroundWorkerPlcRead.ReportProgress(0);
            this.m_Value.Add("Variable1");
            this.m_Value.Add("Variable2");
            e.Result = this.m_Value;

        }
        private void BackgroundWorkerPlcRead_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (e.ProgressPercentage == 1)
            {
                this.m_LblStatus.Text = (string)this.m_InfoThread[e.ProgressPercentage];
            }
            if (e.ProgressPercentage == 2)
            {
                this.m_ProgressBar.Value = (int)this.m_InfoThread[e.ProgressPercentage];
            }
        }

        private void BackgroundWorkerPlcRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!this.m_FlagGoOn)
            {
                this.m_TimerRead.Enabled = false;
                this.DeletePLC();
                this.m_LblStatus.Text = "task cancled";
                this.m_BtnStart.Enabled = true;
                this.m_BtnStart.Focus();
            }
            else
            {
                //this.m_LblStatus.Text = "task complete";
            }
            List<string> var_list = (List<string>)e.Result;
            foreach (string var in var_list)
            {
                Glb_DataBinding.Dispatch(var);
            }
            this.m_TimerRead.Enabled = this.m_FlagGoOn;
        } 

        public void StartRead()
        {
            int retval = 0;
            this.m_FlagGoOn = true;
            this.m_BtnStart.Enabled = false;
            this.m_BtnStopp.Enabled = true;
            this.m_BtnStopp.Focus();
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
            this.m_BtnStart.Enabled = false;
            this.m_BtnStopp.Enabled = false;
            this.m_ProgressBar.Style = ProgressBarStyle.Blocks;
        }

        public void AddList(string adresse, string symbolName, string dataType, string comment, bool dataLog=false)
        {
            int byte_laenge = 0;
            if (dataType == "BOOL")
            {
                byte_laenge = 2;
            }
            if (dataType == "INT")
            {
                byte_laenge = 2;
            }
            if (dataType == "TIME" || dataType == "REAL")
            {
                byte_laenge = 4;
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
            if (!this.m_SymbolInfo.ContainsKey(keyInfo))
            {
                this.m_SymbolInfo.Add(keyInfo, data);
            }
        }
    }
}
