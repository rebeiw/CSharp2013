using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
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

    };


    public class ClsPLC
    {
        private libnodave.daveOSserialType m_Fds;
        private libnodave.daveInterface m_Di;
        private libnodave.daveConnection m_Dc;

        private bool ReadWorking = false;
        private int localMPI = 0;

        public List<PLCDatas> Daten;

        public Hashtable DatabasesValues;

        private Hashtable DatabasesInfo;
        private Hashtable SymbolInfo;

        private List<int> DatabasesNr;


        public string IP;
        private string m_IP
        {
            set { this.m_IP = value; }
            get { return this.m_IP; }
        }

        public bool IsOpen;
        private bool m_IsOpen
        {
            get { return this.m_IsOpen; }
        }

        public int Rack;
        private int m_Rack
        {
            set { this.m_Rack = value; }
            get { return this.m_Rack; }
        }

        public int Slot;
        private int m_Slot
        {
            set { this.m_Slot = value; }
            get { return this.m_Slot; }
        }

        private static ClsPLC instance;
        private ClsPLC()
        {
            DatabasesInfo = new Hashtable();
            DatabasesValues = new Hashtable();
            DatabasesNr = new List<int>();
            Daten = new List<PLCDatas>();
            SymbolInfo=new Hashtable();
        }

        public static ClsPLC CreateInstance()
        {
            if (instance == null)
            {
                instance = new ClsPLC();
            }
            return instance;
        }

        public PLCDatas PlcDatas(string SymbolName)
        {
            PLCDatas retval;
            retval = (PLCDatas)SymbolInfo[SymbolName];
            return retval;
        }
        public string GetKommentar(string SymbolName)
        {
            string retval = "";
            PLCDatas plcData;
            plcData = (PLCDatas)SymbolInfo[SymbolName];
            string[] dataComment = plcData.Comment.Split(':');
            if (dataComment.Count() > 1)
            {
                retval = dataComment[1];
            }
            else
            {
                retval = plcData.Comment;
            }
            return retval;
        }


        public void Open()
        {
            if (!this.IsOpen)
            {
                this.m_Fds.rfd = libnodave.openSocket(102, this.IP);
                this.m_Fds.wfd = m_Fds.rfd;
                this.m_Di = new libnodave.daveInterface(this.m_Fds, "PLC", localMPI, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
                this.m_Di.setTimeout(5000);
                this.m_Dc = new libnodave.daveConnection(m_Di, localMPI, this.Rack, this.Slot);
                int retval = this.m_Dc.connectPLC();
                if (retval == 0)
                {
                    this.IsOpen = true;
                }
                else
                {
                    this.IsOpen = false;
                }
            }
        }

        public void Close()
        {
            this.m_Dc.disconnectPLC();
            this.IsOpen = false;
            this.m_Dc = null;
            this.m_Di = null;
        }

        public void Write(string Symbolname,string Value)
        {
            PLCDatas data;

            data = (PLCDatas)SymbolInfo[Symbolname];

            if (data.DataType == "BOOL")
            {
                byte[] low = new byte[] { 0 };
                byte[] high = new byte[] { 255 };
                int bitAdresse = data.ByteNumber * 8 + data.BitNumber;
                if (Value == "1")
                {
                    m_Dc.writeBits(libnodave.daveDB, data.DatabaseNumber, bitAdresse, 1, high);
                }
                if (Value == "0")
                {
                    m_Dc.writeBits(libnodave.daveDB, data.DatabaseNumber, bitAdresse, 1, low);
                }
            }
            if (data.DataType == "REAL")
            {
                float fltValue = (float)Convert.ToDouble(Value);
                var i = libnodave.daveToPLCfloat(fltValue);
                byte[] intBytes = BitConverter.GetBytes(i);
                m_Dc.writeBytes(libnodave.daveDB, data.DatabaseNumber, data.ByteNumber, 4, intBytes);
            }
            if (data.DataType == "INT")
            {
                Int16 intValue = Convert.ToInt16(Value);
                byte[] intBytes = BitConverter.GetBytes(intValue);
                Array.Reverse(intBytes);
                m_Dc.writeBytes(libnodave.daveDB, data.DatabaseNumber, data.ByteNumber, 2, intBytes);
            }
        }

        public bool ReadBool(string Symbolname)
        {
            bool retval=false;
            PLCDatas data;
            data = (PLCDatas)SymbolInfo[Symbolname];
            int byteNumber=data.ByteNumber;
            double bitNumber=(double) data.BitNumber;
            if (data.DataType == "BOOL")
            {
                byte[] inBytes = new byte[] { 0 };
                int adress = data.ByteNumber * 8 + data.BitNumber;
                m_Dc.readBits(libnodave.daveDB, data.DatabaseNumber, adress, 1, inBytes);
                retval = Convert.ToBoolean(inBytes[0]);
            }
            return retval;
        }

        public int ReadInt(string Symbolname)
        {
            int retval = 0;

            PLCDatas data;
            data = (PLCDatas)SymbolInfo[Symbolname];
            int byteNumber = data.ByteNumber;
            if (data.DataType == "INT")
            {
                byte[] inBytes = new byte[2];
                int adress = data.ByteNumber * 8 + data.BitNumber;
                m_Dc.readBytes(libnodave.daveDB, data.DatabaseNumber, adress, 2, inBytes);
                retval = inBytes[0] * 256 + inBytes[1];
            }
            return retval;
        }

        public void Read()
        {
            if (!this.ReadWorking)
            {
                this.ReadWorking = true;

                foreach (int dbNr in DatabasesNr)
                {
                    int anz=Convert.ToInt32(this.DatabasesInfo[dbNr]);
                    byte[]daten=new byte[anz];
                    this.m_Dc.readManyBytes(libnodave.daveDB, dbNr, 0, anz, daten);

                    PLCDatas pclData;

                    for(int i=0;i<this.Daten.Count();i++)
                    {
                        pclData = this.Daten[i];
                        if (pclData.DatabaseNumber == dbNr)
                        {
                            string searchSymbol = "DB" + dbNr.ToString() + "." + pclData.Symbolname;
                            string value = "";
                            int byteNumber=pclData.ByteNumber;
                            double bitNumber=(double) pclData.BitNumber;
                            if (pclData.DataType == "BOOL")
                            {
                                int wert = (int) Math.Pow(2.0, bitNumber);
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
                                value=libnodave.getFloatfrom(daten, byteNumber).ToString();
                            }

                            if (DatabasesValues.ContainsKey(searchSymbol))
                            {
                                DatabasesValues[searchSymbol] = value;
                            }
                            else
                            {
                                DatabasesValues.Add(searchSymbol, value);
                            }
                        }
                    }
                }
                this.ReadWorking = false;
            }
        }

        public void AddList(string Adress, string Symbolname, string DataType, string Comment)
        {
            this.AddList(Adress, Symbolname, DataType, Comment, false);
        }

        public void AddList(string Adress, string Symbolname, string DataType, string Comment,bool DataLog)
        {

            int byteLaenge = 0;
            if (DataType == "BOOL")
            {
                byteLaenge = 2;
            }
            if (DataType == "INT")
            {
                byteLaenge = 2;
            }
            if (
                DataType == "TIME" ||
                DataType == "REAL"
               )
            {
                byteLaenge = 4;
            }
            int dbNr = 0;
            int byteNr = 0;
            int bitNr = 0;
            string[] adress = Adress.Split('.');
            if (adress.Count() >= 2)
            {
                dbNr=Convert.ToInt32(FuncString.GetOnlyNumeric(adress[0]));
                byteNr = Convert.ToInt32(FuncString.GetOnlyNumeric(adress[1]));
                if (adress.Count() == 3)
                {
                    bitNr = Convert.ToInt32(FuncString.GetOnlyNumeric(adress[2]));
                    int rest=byteNr % 2;
                    byteLaenge = byteLaenge - rest;
                    
                }
                if (DatabasesInfo.ContainsKey(dbNr))
                {
                    int value = Convert.ToInt32(DatabasesInfo[dbNr]);
                    if ((byteNr+byteLaenge)>value)
                    {
                        DatabasesInfo[dbNr] = byteNr + byteLaenge;
                    }
                }
                else
                {
                    DatabasesInfo.Add(dbNr, byteNr);
                    DatabasesNr.Add(dbNr);
                }
            }
            PLCDatas data;
            data.Adress = Adress;
            data.DatabaseNumber = dbNr;
            data.ByteNumber = byteNr;
            data.BitNumber = bitNr;
            data.Symbolname = Symbolname;
            data.DataType = DataType;
            data.Comment = Comment;
            data.Value = "0.0";
            data.DataLog = DataLog;
            this.Daten.Add(data);

            string keyInfo = "DB" + dbNr + "." + Symbolname;
            if (!SymbolInfo.ContainsKey(keyInfo))
            {
                SymbolInfo.Add(keyInfo, data);
            }

        }
    }
}
