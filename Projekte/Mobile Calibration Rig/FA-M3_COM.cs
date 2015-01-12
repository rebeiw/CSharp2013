using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mobile_Calibration_Rig
{
    class FA_M3_COM
    {

        //######################################################################################################################
        //#####################################################################################################################

        private bool IsCalibrationCanceld = false;

        public bool IsCalibCanceled
        {
            get { return IsCalibrationCanceld; }
            set { IsCalibrationCanceld = value; }
        }
        

        /// <summary>
        /// read state of channels
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="ChannelNo">channel number</param>
        /// <param name="Port">serial port</param>
        /// <param name="CommMessage">communication message</param>
        /// <returns>true if set, else false</returns>
        public bool ReadChannel(byte SlotNo, byte ChannelNo, System.IO.Ports.SerialPort Port)
        {
            string value = string.Empty;

            Port.WriteLine((char)2 + "01010BRDY"+ SlotNo.ToString().PadLeft(3, '0') + ChannelNo.ToString().PadLeft(2, '0') + ",001" + (char)3);
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            value = Port.ReadExisting();
            if (value.Substring(7, 1) == "1")
                return true;
            else if (value.Substring(7, 1) == "0")
                return false;
            else
            {
                MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// close relay at FA-M3
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="RelayNo">relay number</param>
        /// <param name="Port">serial port</param>
        /// <param name="CommMessage">communication message</param>
        public void CloseRelay(byte SlotNo, byte RelayNo, System.IO.Ports.SerialPort Port)
        {
            string value = string.Empty;

            Port.WriteLine((char)2 + "01010BWRY" + SlotNo.ToString().PadLeft(3, '0') + RelayNo.ToString().PadLeft(2, '0') + ",001,1" + (char)3);
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            value = Port.ReadExisting();
            if (value.Substring(1, 6) != "0101OK")
                MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// open relay at FA-M3
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="RelayNo">relay number</param>
        /// <param name="Port">serial port</param>
        public void OpenRelay(byte SlotNo, byte RelayNo, System.IO.Ports.SerialPort Port)
        {
            string value = string.Empty;

            Port.WriteLine((char)2 + "01010BWRY" + SlotNo.ToString().PadLeft(3, '0') + RelayNo.ToString().PadLeft(2, '0') + ",001,0" + (char)3);
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            value = Port.ReadExisting();
            if (value.Substring(1, 6) != "0101OK")
                MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// close internal relay at FA-M3
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="RelayNo">relay number</param>
        /// <param name="Port">serial port</param>
        /// <param name="CommMessage">communication message</param>
        public void CloseInternalRelay(byte SlotNo, byte RelayNo, System.IO.Ports.SerialPort Port)
        {
            if (Port.IsOpen)
            {
                string value = string.Empty;

                Port.WriteLine((char)2 + "01010BWRI" + SlotNo.ToString().PadLeft(3, '0') + RelayNo.ToString().PadLeft(2, '0') + ",001,1" + (char)3);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                value = Port.ReadExisting();
                if (value.Substring(1, 6) != "0101OK")
                    MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// open internal relay at FA-M3
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="RelayNo">relay number</param>
        /// <param name="Port">serial port</param>
        public void OpenInternalRelay(byte SlotNo, byte RelayNo, System.IO.Ports.SerialPort Port)
        {
            if(Port.IsOpen)
            {
                string value = string.Empty;

                Port.WriteLine((char)2 + "01010BWRI" + SlotNo.ToString().PadLeft(3, '0') + RelayNo.ToString().PadLeft(2, '0') + ",001,0" + (char)3);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                value = Port.ReadExisting();
                if (value.Substring(1, 6) != "0101OK")
                    MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// get data value at FA-M3
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="CounterNo">counter number</param>
        /// <param name="Port">serial port</param>
        public Int32 GetFAM3DataValue(byte SlotNo, byte DataNo, System.IO.Ports.SerialPort Port)
        {
            if (Port.IsOpen)
            {
                string value = string.Empty;

                Port.WriteLine((char)2 + "01010WRDD" + SlotNo.ToString().PadLeft(3, '0') + DataNo.ToString().PadLeft(2, '0') + ",01" + (char)3);

                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                value = Port.ReadExisting();
                if (value.Substring(1, 6) != "0101OK")
                {
                    MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(value.Substring(7, 4), 16);
                }
            }
            else return 0;
        }

        /// <summary>
        /// set data value at FA-M3
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="CounterNo">counter number</param>
        /// <param name="Value">Value to set</param>
        /// <param name="Port">serial port</param>
        public void SetFAM3DataValue(byte SlotNo, byte DataNo, int Value, System.IO.Ports.SerialPort Port)
        {
            string HexValue = string.Empty;

            HexValue = Value.ToString("X4");

            Port.WriteLine((char)2 + "01010WWRD" + SlotNo.ToString().PadLeft(3, '0') + DataNo.ToString().PadLeft(2, '0') + ",01," + HexValue + (char)3);

            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            string reply = Port.ReadExisting();
            if (reply.Substring(1, 6) != "0101OK")
            {
                MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }


        /// <summary>
        /// opens all relays at FA-M3
        /// <param name="SlotNo">slot number</param>
        /// <param name="Port">serial port</param>
        /// </summary>
        public void OpenAllRelays(byte SlotNo, System.IO.Ports.SerialPort Port)
        {
            if (Port.IsOpen)
            {
                string value = string.Empty;

                Port.WriteLine((char)2 + "01010WWRY" + SlotNo.ToString().PadLeft(3, '0') + "01,01,0000" + (char)3);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                value = Port.ReadExisting();
                if (value.Substring(1, 6) != "0101OK")
                    MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// read counter values
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="ChannelNo">channel number</param>
        /// <param name="Port">serial port</param>
        /// <returns>true if set, else false</returns>
        public int ReadCounterValue(byte SlotNo, byte ChannelNo, System.IO.Ports.SerialPort Port)
        {
            if (Port.IsOpen)
            {
                string value = string.Empty;

                Port.WriteLine((char)2 + "01010SWR" + SlotNo.ToString().PadLeft(3, '0') + "," + ChannelNo.ToString().PadLeft(4, '0') + ",1" + (char)3);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                value = Port.ReadExisting();

                if (value.Substring(1, 6) != "0101OK")
                {
                    MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(value.Substring(7, 4), 16);
                }
            }
            else return 0;
        }

        /// <summary>
        /// read 4 counter values
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="ChannelNo">channel number</param>
        /// <param name="Port">serial port</param>
        /// <returns>true if set, else false</returns>
        public void Read4CounterValues(byte SlotNo, System.IO.Ports.SerialPort Port, ref int Counter1, ref int Counter2, ref int Counter3, ref int Counter4)
        {
            if (Port.IsOpen)
            {
                string value = string.Empty;

                Port.WriteLine((char)2 + "01010SWR" + SlotNo.ToString().PadLeft(3, '0') + ",0001,4" + (char)3);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                value = Port.ReadExisting();
                if (value == string.Empty)
                {
                    Port.WriteLine((char)2 + "01010SWR" + SlotNo.ToString().PadLeft(3, '0') + ",0001,4" + (char)3);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(100);
                    value = Port.ReadExisting();
                }
                else if (value.Substring(1, 6) != "0101OK")
                {
                    MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Counter1 = Convert.ToInt32(value.Substring(7, 4), 16);
                    Counter2 = Convert.ToInt32(value.Substring(11, 4), 16);
                    Counter3 = Convert.ToInt32(value.Substring(15, 4), 16);
                    Counter4 = Convert.ToInt32(value.Substring(19, 4), 16);
                }
            }
        }

        public int ReadWord(byte SlotNo, string Adress, System.IO.Ports.SerialPort Port)
        {
            string value = string.Empty;
            int tempval = 0;

            Port.WriteLine((char)2 + "01010WRD" + Adress + ",01" + (char)3);
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            value = Port.ReadExisting();

            if (value.Substring(1, 6) != "0101OK")
            {
                MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            else
            {
                value = value.Substring(7, 4);
                tempval = Convert.ToInt32(value, 16);
                return tempval;
            }

        }

        /// <summary>
        /// read temperature values
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="ChannelNo">channel number</param>
        /// <param name="Port">serial port</param>
        /// <returns>temperature value</returns>
        public float ReadTempValue(byte SlotNo, byte ChannelNo, System.IO.Ports.SerialPort Port)
        {
            if (Port.IsOpen)
            {
                string value = string.Empty;
                int tempval = 0;

                Port.WriteLine((char)2 + "01010SWR" + SlotNo.ToString().PadLeft(3, '0') + "," + ChannelNo.ToString().PadLeft(4, '0') + ",1" + (char)3);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                value = Port.ReadExisting();

                if (value.Substring(1, 6) != "0101OK")
                {
                    MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                else
                {
                    value = value.Substring(7, 4);
                    tempval = Convert.ToInt32(value, 16);
                    return (float)tempval / 100;
                }
            }
            else return 0;
        }

        /// <summary>
        /// read four temperature channel values
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="Port">serial port</param>
        /// <param name="Temp1">temperature channel 1</param>
        /// <param name="Temp2">temperature channel 2</param>
        /// <param name="Temp3">temperature channel 3</param>
        /// <param name="Temp4">temperature channel 4</param>
        public void Read4TempValues(byte SlotNo, System.IO.Ports.SerialPort Port, ref float Temp1, ref float Temp2, ref float Temp3, ref float Temp4)
        {
            if (Port.IsOpen)
            {
                string value1 = string.Empty, value2 = string.Empty, value3 = string.Empty, value4 = string.Empty, value = string.Empty;

                Port.WriteLine((char)2 + "01010SWR" + SlotNo.ToString().PadLeft(3, '0') + ",0001,4" + (char)3);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                value = Port.ReadExisting();
                if (value != string.Empty)
                {
                    if (value.Substring(1, 6) != "0101OK")
                    {
                        MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        value1 = value.Substring(7, 4);
                        value2 = value.Substring(11, 4);
                        value3 = value.Substring(15, 4);
                        value4 = value.Substring(19, 4);
                        Temp1 = (float)Convert.ToInt32(value1, 16) / 100;
                        Temp2 = (float)Convert.ToInt32(value2, 16) / 100;
                        Temp3 = (float)Convert.ToInt32(value3, 16) / 100;
                        Temp4 = (float)Convert.ToInt32(value4, 16) / 100;
                    }
                }
            }
            
        }

        /// <summary>
        /// read analog input values
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="ChannelNo">channel number</param>
        /// <param name="Port">serial port</param>
        /// <returns>analog input value</returns>
        public float ReadAnalogInputValue(byte SlotNo, byte ChannelNo, System.IO.Ports.SerialPort Port)
        {
            if (Port.IsOpen)
            {
                string value = string.Empty;
                int tempval = 0;

                Port.WriteLine((char)2 + "01010SWR" + SlotNo.ToString().PadLeft(3, '0') + "," + ChannelNo.ToString().PadLeft(4, '0') + ",1" + (char)3);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                value = Port.ReadExisting();

                if (value.Substring(1, 6) != "0101OK")
                {
                    MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                else
                {
                    value = value.Substring(7, 4);
                    tempval = Convert.ToInt32(value, 16);
                    return (float)tempval / 100;
                }
            }
            else return 0;
        }

        /// <summary>
        /// read eight analog input channel values
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="Port">serial port</param>
        /// <param name="Temp1">value channel 1</param>
        /// <param name="Temp2">value channel 2</param>
        /// <param name="Temp3">value channel 3</param>
        /// <param name="Temp4">value channel 4</param>
        /// <param name="Temp5">value chennel 5</param>
        /// <param name="Temp6">value channel 6</param>
        /// <param name="Temp7">value channel 7</param>
        /// <param name="Temp8">value channel 8</param>
        public void Read8AnalogInputValues(byte SlotNo, System.IO.Ports.SerialPort Port, ref float Value1, ref float Value2, ref float Value3, ref float Value4, ref float Value5, ref float Value6, ref float Value7, ref float Value8)
        {
            if (Port.IsOpen)
            {
                string value1 = string.Empty, value2 = string.Empty, value3 = string.Empty, value4 = string.Empty, value5 = string.Empty, value6 = string.Empty, value7 = string.Empty, value8 = string.Empty, value = string.Empty;

                Port.WriteLine((char)2 + "01010SWR" + SlotNo.ToString().PadLeft(3, '0') + ",0001,8" + (char)3);
                Application.DoEvents();
                System.Threading.Thread.Sleep(150);  //100
                value = Port.ReadExisting();
                if (value != string.Empty)
                {
                    if (value.Substring(1, 6) != "0101OK")
                    {
                        MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        value1 = value.Substring(7, 4);
                        value2 = value.Substring(11, 4);
                        value3 = value.Substring(15, 4);
                        value4 = value.Substring(19, 4);
                        value5 = value.Substring(23, 4);
                        value6 = value.Substring(27, 4);
                        value7 = value.Substring(31, 4);
                        value8 = value.Substring(35, 4);
                        Value1 = (float)Convert.ToInt32(value1, 16);
                        Value2 = (float)Convert.ToInt32(value2, 16);
                        Value3 = (float)Convert.ToInt32(value3, 16);
                        Value4 = (float)Convert.ToInt32(value4, 16);
                        Value5 = (float)Convert.ToInt32(value1, 16);
                        Value6 = (float)Convert.ToInt32(value2, 16);
                        Value7 = (float)Convert.ToInt32(value3, 16);
                        Value8 = (float)Convert.ToInt32(value4, 16);
                    }
                }
            }
            
        }

        /// <summary>
        /// get status of analog input module
        /// </summary>
        /// <param name="SlotNo">slot number</param>
        /// <param name="Port">serial port</param>
        /// <returns>true if an error occured</returns>
        public bool GetAnalogInputStatus(byte SlotNo, System.IO.Ports.SerialPort Port)
        {
            string error = string.Empty;

            Port.WriteLine((char)2 + "01010SWR" + SlotNo.ToString().PadLeft(3, '0') + ",0201,1" + (char)3);
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            error = Port.ReadExisting();

            if (error.Substring(1, 10) != "0101OK0000")
            {
                MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                error = error.Substring(7, 4);
                if (error == "A000")
                    MessageBox.Show("Calibration value error", "FA-M3: Analog input module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (error.Substring(0, 2) == "C0")
                    MessageBox.Show("DC/DC error", "FA-M3: Analog input module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (error.Substring(0, 2) == "B0")
                    MessageBox.Show("ADC error", "FA-M3: Analog input module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }


        }

        public void ResetAnalogInputError(byte SlotNo, System.IO.Ports.SerialPort Port)
        {
            string value = string.Empty;

            Port.WriteLine((char)2 + "01010SWW" + SlotNo.ToString().PadLeft(3, '0') + ",0700,1, FFFF" + (char)3);
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            value = Port.ReadExisting();

            if (value.Substring(1, 6) != "0101OK")
                MessageBox.Show("FA-M3 error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;            
        }

        /// <summary>
        /// reset all counters
        /// </summary>
        /// <param name="Port">serial port</param>
        public void ResetCounter(System.IO.Ports.SerialPort Port)
        {
            CloseInternalRelay(0, 2, Port);
            System.Threading.Thread.Sleep(100);
            OpenInternalRelay(0, 2, Port);
        }

        /// <summary>
        /// calculate frequency of pulse output
        /// </summary>
        /// <param name="Freq1">pulse output 1</param>
        /// <param name="Freq2">pulse output 2</param>
        /// <param name="Freq3">pulse output 3</param>
        /// <param name="Freq4">pulse output 4</param>
        /// <param name="FreqCust">customer pulse output</param>
        /// <param name="TestTime">measuring time</param>
        /// <param name="Current">current value</param>
        /// <param name="Port">serial port</param>
        /// <param name="ProgBar">progress bar</param>
        public void GetFrequency(ref float Freq1, ref float Freq2, ref float Freq3, ref float Freq4, ref float FreqCust, int TestTime, ref float Current, System.IO.Ports.SerialPort Port, System.Windows.Forms.ToolStripProgressBar ProgBar)
        {

            int Count1=0,Count2=0,Count3=0,Count4=0,CountCust=0;
            int CountRing1 = 0, CountRing2 = 0, CountRing3 = 0, CountRing4 = 0, CountRingCust = 0;
            float[] arrCurrent = new float[(int)TestTime];

            OpenInternalRelay(0, 1, Port);
            OpenInternalRelay(0, 3, Port);
            OpenInternalRelay(0, 4, Port);
            ResetCounter(Port);
            if(TestTime == 60)
                CloseInternalRelay(0, 1, Port);
            else if(TestTime==120)
                CloseInternalRelay(0, 3, Port);
            else if(TestTime==300)
                CloseInternalRelay(0, 4, Port);

            for (int i = 0; i < TestTime; i++)
            {              
                if (IsCalibrationCanceld)
                {
                    OpenInternalRelay(0, 1, Port);
                    OpenInternalRelay(0, 3, Port);
                    OpenInternalRelay(0, 4, Port);
                    ResetCounter(Port);
                    ProgBar.Value = 0;
                    return;
                }
                System.Threading.Thread.Sleep(1000);
                ProgBar.Increment(1);
                Application.DoEvents();
                arrCurrent[(int)i] = ReadAnalogInputValue(4, 3, Port);
            }

            Current = CalculateMean(arrCurrent)/10;

            System.Threading.Thread.Sleep(1000);
            ProgBar.Value = 0;
            Read4CounterValues(5, Port, ref Count1, ref Count2, ref Count3, ref Count4);
            CountCust = ReadCounterValue(6, 1, Port);
            OpenInternalRelay(0, 1, Port);
            CountRing1 = GetFAM3DataValue(0, 3, Port);
            CountRing2 = GetFAM3DataValue(0, 4, Port);
            CountRing3 = GetFAM3DataValue(0, 5, Port);
            CountRing4 = GetFAM3DataValue(0, 6, Port);
            CountRingCust = GetFAM3DataValue(0, 2, Port);
            Freq1 = Convert.ToSingle((Count1 + (CountRing1 * 0xFFFF)) / (float)TestTime);
            Freq2 = Convert.ToSingle((Count2 + (CountRing2 * 0xFFFF)) / (float)TestTime);
            Freq3 = Convert.ToSingle((Count3 + (CountRing3 * 0xFFFF)) / (float)TestTime);
            Freq4 = Convert.ToSingle((Count4 + (CountRing4 * 0xFFFF)) / (float)TestTime);
            FreqCust = Convert.ToSingle((CountCust + (CountRingCust * 0xFFFF)) / (float)TestTime);
        }

        /// <summary>
        /// calculates mean of array values
        /// </summary>
        /// <param name="Values">values</param>
        /// <returns>mean</returns>
        private float CalculateMean(float[] Values)
        {
            float tempValue = 0;

            foreach (var item in Values)
            {
                tempValue += item;
            }
            return tempValue/Values.Length;
        }
    }
}
