using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Calibration_Rig
{
    class Modbus
    {
        #region Enumerations
        /// <summary>
        /// enum of counter types
        /// </summary>
       public enum CounterTypesReset
	        {
	            Not_execute, Flex_totals, Mass_totals, Volume_totals, Net_totals, All_totals     
	        }
        #endregion

       #region Functions
       /// <summary>
        /// read input register with Modbus
        /// </summary>
        /// <param name="DevAdr">adress of the device</param>
        /// <param name="StartAdr">adress of the parameter to read</param>
        /// <param name="Quantity">quantity of bytes to read</param>
        /// <param name="InputReg">reply of the register (2*Quantity + 5)</param>
        /// <param name="ComPort">serial port</param>
        public void ReadInputRegisters(byte DevAdr, UInt16 StartAdr, UInt16 Quantity, ref byte[] InputReg, System.IO.Ports.SerialPort ComPort)
        {
            byte[] Reply = new byte[Quantity * 2 + 5];
            byte[] CommandChk = new byte[6];
            byte[] Command = new byte[8];
            byte[] IntValues = new byte[2];
            int size= 0;

            IntValues = BitConverter.GetBytes(StartAdr);

            CommandChk[0] = DevAdr;
            CommandChk[1] = 4;
            CommandChk[2] = IntValues[1];
            CommandChk[3] = IntValues[0];
            IntValues = BitConverter.GetBytes(Quantity);
            CommandChk[4] = IntValues[1];
            CommandChk[5] = IntValues[0];
            IntValues = CalculateChecksum(CommandChk);
            Array.Copy(CommandChk, Command, 6);
            Command[6] = IntValues[0];
            Command[7] = IntValues[1];

            ComPort.DiscardInBuffer();
            ComPort.DiscardOutBuffer();
            ComPort.Write(Command, 0, 8);
            System.Threading.Thread.Sleep(35); 

            while (size < Reply.Length)
            {
                size = ComPort.Read(Reply, 0, Reply.Length);
            }

            //ComPort.Read(Reply, 0, Reply.Length);
            CheckReply(Command, Reply);
            Array.Copy(Reply, 3, InputReg, 0, Quantity * 2);
        }

        /// <summary>
        /// read holding register with Modbus
        /// </summary>
        /// <param name="DevAdr">adress of the device</param>
        /// <param name="StartAdr">adress of the parameter to read</param>
        /// <param name="Quantity">quantity of bytes to read</param>
        /// <param name="InputReg">reply of the register (2*Quantity + 5)</param>
        /// <param name="ComPort">serial port</param>
        public void ReadHoldingRegisters(byte DevAdr, UInt16 StartAdr, UInt16 Quantity, ref byte[] HoldingReg, System.IO.Ports.SerialPort ComPort)
        {
            byte[] Reply = new byte[Quantity * 2 + 5];
            byte[] CommandChk = new byte[6];
            byte[] Command = new byte[8];
            byte[] IntValues = new byte[2];
            int size = 0;

            IntValues = BitConverter.GetBytes(StartAdr);

            CommandChk[0] = DevAdr;
            CommandChk[1] = 3;
            CommandChk[2] = IntValues[1];
            CommandChk[3] = IntValues[0];
            IntValues = BitConverter.GetBytes(Quantity);
            CommandChk[4] = IntValues[1];
            CommandChk[5] = IntValues[0];
            IntValues = CalculateChecksum(CommandChk);
            Array.Copy(CommandChk, Command, 6);
            Command[6] = IntValues[0];      
            Command[7] = IntValues[1];      

            ComPort.DiscardInBuffer();
            ComPort.DiscardOutBuffer();
            ComPort.Write(Command, 0, 8);
            System.Threading.Thread.Sleep(1000);//35
            
            while (size < Reply.Length)
            {
                try
                {
                    size = ComPort.Read(Reply, 0, Reply.Length);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Communication error", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }

            //System.Threading.Thread.Sleep(300);
            ////size = ComPort.Read(Reply, 0, Reply.Length);
            //string mist = ComPort.ReadExisting();

            //ComPort.Read(Reply, 0, Reply.Length);
            CheckReply(Command, Reply);
            Array.Copy(Reply, 3, HoldingReg, 0, Quantity * 2);
        }

        /// <summary>
        /// write holding register with Modbus
        /// </summary>
        /// <param name="DevAdr">adress of the device</param>
        /// <param name="RegAdr">adress of the parameter to write</param>
        /// <param name="InputValue">values to write</param>
        /// <param name="ComPort">serial port</param>
        public void WriteHoldingRegisters(byte DevAdr, UInt16 RegAdr, UInt16 InputValue, System.IO.Ports.SerialPort ComPort)
        {
            byte[] Reply = new byte[8];
            byte[] CommandChk = new byte[6];
            byte[] Command = new byte[8];
            byte[] IntValues = new byte[2];
            byte[] InputValues = new byte[2];

            CommandChk[0] = DevAdr;
            CommandChk[1] = 6;     
            IntValues = BitConverter.GetBytes(RegAdr);
            CommandChk[2] = IntValues[1];
            CommandChk[3] = IntValues[0];
            InputValues = BitConverter.GetBytes(InputValue);
            CommandChk[4] = InputValues[1];
            CommandChk[5] = InputValues[0];
            IntValues = CalculateChecksum(CommandChk);
            Array.Copy(CommandChk, Command, 6);
            Command[6] = IntValues[0];
            Command[7] = IntValues[1];

            ComPort.DiscardInBuffer();
            ComPort.DiscardOutBuffer();
            ComPort.Write(Command, 0, 8);
            System.Threading.Thread.Sleep(750);

            ComPort.Read(Reply, 0, Reply.Length);
            CheckEchoReply(Command, Reply);
        }

        /// <summary>
        /// write multiple register with Modbus
        /// </summary>
        /// <param name="DevAdr">adress of the device</param>
        /// <param name="RegAdr">adress of the parameter to write</param>
        /// <param name="InputValues">values to write</param>
        /// <param name="ComPort">serial port</param>
        public void WriteMultipleRegisters(byte DevAdr, UInt16 RegAdr, string InputValues, System.IO.Ports.SerialPort ComPort)
        {
            byte[] Reply = new byte[8];
            byte[] CommandChk = new byte[7+InputValues.Length];
            byte[] Command = new byte[9+InputValues.Length];
            byte[] IntValues = new byte[2];
            byte[] arrInputValue = new byte[InputValues.Length];

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            arrInputValue = enc.GetBytes(InputValues);

            CommandChk[0] = DevAdr;
            CommandChk[1] = 0x10;
            IntValues = BitConverter.GetBytes(RegAdr);
            CommandChk[2] = IntValues[1];
            CommandChk[3] = IntValues[0];
            IntValues = BitConverter.GetBytes(InputValues.Length/2);
            CommandChk[4] = IntValues[1];
            CommandChk[5] = IntValues[0];
            CommandChk[6] = (byte)(InputValues.Length);
            for (int i = 0; i < InputValues.Length; i=i+2)
            {
                CommandChk[7 + i] = arrInputValue[i];
                CommandChk[8 + i] = arrInputValue[i + 1];
            }
            IntValues = CalculateChecksum(CommandChk);
            Array.Copy(CommandChk, Command, CommandChk.Length);
            Command[7+InputValues.Length] = IntValues[0];
            Command[8+InputValues.Length] = IntValues[1];

            ComPort.DiscardInBuffer();
            ComPort.DiscardOutBuffer();
            ComPort.Write(Command, 0, Command.Length);
            System.Threading.Thread.Sleep(500);
            ComPort.Read(Reply, 0, Reply.Length);
            CheckReply(Command, Reply);
        }

        public void WriteMultipleRegisters(byte DevAdr, UInt16 RegAdr, byte[] InputValues, System.IO.Ports.SerialPort ComPort)
        {
            byte[] Reply = new byte[8];
            byte[] CommandChk = new byte[7 + InputValues.Length];
            byte[] Command = new byte[9 + InputValues.Length];
            byte[] IntValues = new byte[2];

            CommandChk[0] = DevAdr;
            CommandChk[1] = 0x10;
            IntValues = BitConverter.GetBytes(RegAdr);
            CommandChk[2] = IntValues[1];
            CommandChk[3] = IntValues[0];
            IntValues = BitConverter.GetBytes(InputValues.Length / 2);
            CommandChk[4] = IntValues[1];
            CommandChk[5] = IntValues[0];
            CommandChk[6] = (byte)(InputValues.Length);
            for (int i = 0; i < InputValues.Length; i = i + 2)
            {
                CommandChk[7 + i] = InputValues[i];
                CommandChk[8 + i] = InputValues[i+1];
            }
            IntValues = CalculateChecksum(CommandChk);
            Array.Copy(CommandChk, Command, CommandChk.Length);
            Command[7 + InputValues.Length] = IntValues[0];
            Command[8 + InputValues.Length] = IntValues[1];

            ComPort.DiscardInBuffer();
            ComPort.DiscardOutBuffer();
            ComPort.Write(Command, 0, Command.Length);
            System.Threading.Thread.Sleep(1000);//750
            ComPort.Read(Reply, 0, Reply.Length);
            CheckReply(Command, Reply);

        }

        /// <summary>
        /// berechnet aus 4 Bytes Modbus Gleitkommazahl
        /// </summary>
        /// <param name="bytFirstByte">1.Byte</param>
        /// <param name="bytSecondByte">2.Byte</param>
        /// <param name="bytThirdByte">3.Byte</param>
        /// <param name="bytFourthByte">4.Byte</param>
        /// <returns>Floatwert</returns>
        private static float CalculateModBusFloatFrom4Bytes(byte bytFirstByte, byte bytSecondByte, byte bytThirdByte, byte bytFourthByte)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            string strFirstByte = null;
            string strSecondByte = null;
            string strThirdByte = null;
            string strFourthByte = null;
            string strAll = null;
            string strSign = null;
            byte bytExpo = 0;
            double dblMant = 0;
            int intMult = 0, tempVal = 0;
            float fltZahl = 0;

            strThirdByte = Convert.ToString(bytFirstByte, 2).PadLeft(8, '0');
            strFourthByte = Convert.ToString(bytSecondByte, 2).PadLeft(8, '0');
            strFirstByte = Convert.ToString(bytThirdByte, 2).PadLeft(8, '0');
            strSecondByte = Convert.ToString(bytFourthByte, 2).PadLeft(8, '0');
            strAll = strFirstByte + strSecondByte + strThirdByte + strFourthByte;
            strSign = strAll.Substring(0, 1);
            if (strSign == "1")
                intMult = -1;
            else
                intMult = 1;
            bytExpo = Convert.ToByte(strAll.Substring(1, 8), 2);
            tempVal = Convert.ToInt32(strAll.Substring(9), 2);
            dblMant = (double)(tempVal / Math.Pow(2, 23)) + 1;
            fltZahl = Convert.ToSingle(dblMant * Math.Pow(2, bytExpo - 127));
            return fltZahl * intMult;
        }

        /// <summary>
        /// berechnet aus Float-Wert 4 Bytes der internen Zahlendarstellung nach IEEE
        /// </summary>
        /// <param name="fltValue">Zahlenwert</param>
        /// <returns>Bytearray mit 4 Bytes</returns>
        private static byte[] CalculateModBusFloat(float fltValue)
        {
            string strBinVal = null;
            int intBiased_Exponent_Float = 0;
            double dblMantissa_Float = 0;
            double dblTemp_mantissa = 0;
            byte bytVZ = 0;
            string strBiased_Exponent_Float = null;
            string strMantissa_Float = null;
            byte[] arrFloatValue = new byte[4];
            byte[] arrFloatValueTemp = new byte[4];

            if (fltValue == 0)   //Definition für float 0: 0,0,0,0
            {
                arrFloatValue[0] = 0;
                arrFloatValue[1] = 0;
                arrFloatValue[2] = 0;
                arrFloatValue[3] = 0;
            }
            else
            {
                //erst VZ ermitteln
                if (fltValue < 0)
                {
                    bytVZ = 1;
                    fltValue = Math.Abs(fltValue);  //bei neg. VZ Absolutwert weiterverwenden
                }
                else
                    bytVZ = 0;

                //zunächst nötigen Exponent ermitteln, damit Mantisse < 2
                intBiased_Exponent_Float = -127;

                do
                {
                    dblTemp_mantissa = fltValue / Math.Pow(2, intBiased_Exponent_Float);
                    intBiased_Exponent_Float = intBiased_Exponent_Float + 1;
                }
                while (dblTemp_mantissa >= 2 || dblTemp_mantissa < 1);

                dblMantissa_Float = dblTemp_mantissa;
                intBiased_Exponent_Float = intBiased_Exponent_Float - 1;

                //dann eigentliche Werte für Bytes bestimmen
                intBiased_Exponent_Float = intBiased_Exponent_Float + 127;
                dblMantissa_Float = (dblMantissa_Float - 1) * Math.Pow(2, 23);

                //umwandeln in Bin
                strBiased_Exponent_Float = Convert.ToString(intBiased_Exponent_Float, 2).PadLeft(8, '0');
                strMantissa_Float = Convert.ToString(Convert.ToInt64(dblMantissa_Float), 2).PadLeft(23, '0');

                //String zusammenstellen
                strBinVal = bytVZ + strBiased_Exponent_Float + strMantissa_Float;

                //String in 4 Bytes aufteilen
                arrFloatValueTemp[0] = Convert.ToByte(strBinVal.Substring(0, 8), 2);
                arrFloatValueTemp[1] = Convert.ToByte(strBinVal.Substring(8, 8), 2);
                arrFloatValueTemp[2] = Convert.ToByte(strBinVal.Substring(16, 8), 2);
                arrFloatValueTemp[3] = Convert.ToByte(strBinVal.Substring(24, 8), 2);

                //Changing for Rotamass 3 Series
                arrFloatValue[2] = arrFloatValueTemp[0];
                arrFloatValue[3] = arrFloatValueTemp[1];
                arrFloatValue[0] = arrFloatValueTemp[2];
                arrFloatValue[1] = arrFloatValueTemp[3];

            }
            return arrFloatValue;
        }

        private byte[] CalculateChecksum(byte[] Data)
        {
            byte[] reply = new byte[2];
            UInt16 Crc = 0xFFFF;

            for (int i = 0; i < Data.Length; i++)
            {
                Crc = CRC16(Crc, Data[i]);
            }
            reply[0] = Convert.ToByte(Crc & 0x00FF);
            reply[1] = Convert.ToByte((Crc & 0xFF00) / 256); //TODO: ggf. umdrehen
            return reply;

        }

        private UInt16 CRC16(UInt16 crc, UInt16 data)
        {
            const UInt16 Poly16 = 0xA001;
            Boolean LSB = false;

            crc = Convert.ToUInt16(((crc ^ data) | 0xFF00) & (crc | 0x00FF));
            for (int i = 0; i < 8; i++)
            {
                LSB = Convert.ToBoolean(crc & 0x0001);
                crc = Convert.ToUInt16(crc / 2);
                if (LSB)
                    crc = Convert.ToUInt16(crc ^ Poly16);
            }
            return crc;
        }

        /// <summary>
        /// check reply of read commands
        /// </summary>
        /// <param name="Command">command bytes</param>
        /// <param name="Reply">reply bytes</param>
        private void CheckReply(byte[] Command, byte[] Reply)
        {
            byte[] RepCheck = new byte[Reply.Length - 2];
            byte[] ChkSum = new byte[2];

            if (Reply[1] == (Command[1] + 0x80))
            {
                switch (Reply[2])
                {
                    case 1:
                        System.Windows.Forms.MessageBox.Show("Illegal function", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    case 2:
                        System.Windows.Forms.MessageBox.Show("Illegal data address", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    case 3:
                        System.Windows.Forms.MessageBox.Show("Illegal data value", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    case 4:
                        System.Windows.Forms.MessageBox.Show("Server device failure", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    default:
                        System.Windows.Forms.MessageBox.Show("Modbus communication error", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                Array.Copy(Reply, 0, RepCheck, 0, RepCheck.Length);
                ChkSum = CalculateChecksum(RepCheck);
                if(ChkSum[0] != Reply[Reply.Length-2] || ChkSum[1] != Reply[Reply.Length-1])
                    System.Windows.Forms.MessageBox.Show("Modbus checksum error", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// check echo reply of write commands
        /// </summary>
        /// <param name="Command">command bytes</param>
        /// <param name="Reply">reply bytes</param>
        private void CheckEchoReply(byte[] Command, byte[] Reply)
        {
            if (Reply[1] == (Command[1] + 0x80))
            {
                switch (Reply[2])
                {
                    case 1:
                        System.Windows.Forms.MessageBox.Show("Illegal function", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    case 2:
                        System.Windows.Forms.MessageBox.Show("Illegal data address", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    case 3:
                        System.Windows.Forms.MessageBox.Show("Illegal data value", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    case 4:
                        System.Windows.Forms.MessageBox.Show("Server device failure", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    default:
                        System.Windows.Forms.MessageBox.Show("Modbus communication error", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                if (!Command.SequenceEqual(Reply))
                    System.Windows.Forms.MessageBox.Show("Modbus communication error", "Modbus error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Calculate int32 value from 4 bytes
        /// </summary>
        /// <param name="bytFirstByte">1. byte</param>
        /// <param name="bytSecondByte">2. byte</param>
        /// <param name="bytThirdByte">3. byte</param>
        /// <param name="bytFourthByte">4. byte</param>
        /// <returns>int value</returns>
        private Int32 CalculateInt32From4Bytes(byte bytFirstByte, byte bytSecondByte, byte bytThirdByte, byte bytFourthByte)
        {
            string HexValue = bytFirstByte.ToString().PadLeft(2, '0') + bytSecondByte.ToString().PadLeft(2, '0') + bytThirdByte.ToString().PadLeft(2, '0') + bytFourthByte.ToString().PadLeft(2, '0');
            return Convert.ToInt32(HexValue, 16);
        }
       #endregion

        #region Commands
        /// <summary>
        /// read Device identification with Modbus
        /// </summary>
        /// <param name="DevAdr">adress of the device</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>Device identification</returns>
        public string ReadDeviceID(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] Reply = new byte[57];
            byte[] CommandChk = new byte[5];
            byte[] Command = new byte[7];
            byte[] IntValues = new byte[2];
            byte[] arrReply = new byte[45];

            CommandChk[0] = DevAdr;
            CommandChk[1] = 43;
            CommandChk[2] = 14;
            CommandChk[3] = 1;
            CommandChk[4] = 0;
            IntValues = CalculateChecksum(CommandChk);
            Array.Copy(CommandChk, Command, 5);
            Command[5] = IntValues[0];
            Command[6] = IntValues[1];

            ComPort.DiscardInBuffer();
            ComPort.DiscardOutBuffer();
            ComPort.Write(Command, 0, Command.Length);
            System.Threading.Thread.Sleep(2000);

            ComPort.Read(Reply, 0, Reply.Length);
            CheckReply(Command, Reply);

            Array.Copy(Reply, 9, arrReply, 0, 45);
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arrReply);
        }

        /// <summary>
        /// read temerature
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>temperature</returns>
        public float ReadTemperature(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[9];

            ReadInputRegisters(DevAdr, 6, 2, ref TempValues, ComPort);
            return CalculateModBusFloatFrom4Bytes(TempValues[0], TempValues[1], TempValues[2], TempValues[3]);
        }

        /// <summary>
        /// read mass flow
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>flow</returns>
        public float ReadMassFlow(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[9];

            ReadInputRegisters(DevAdr, 0, 2, ref TempValues, ComPort);
            return CalculateModBusFloatFrom4Bytes(TempValues[0], TempValues[1], TempValues[2], TempValues[3]);
        }

        /// <summary>
        /// read mass flow unit
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>flow unit</returns>
        public string ReadMassFlowUnit(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[2];
            string unit = string.Empty;

            ReadInputRegisters(DevAdr,45, 1, ref TempValues, ComPort);
            switch (TempValues[1])
            {
                case 70:
                    unit = "g/s";
                    break;
                case 71:
                    unit = "g/min";
                    break;
                case 72:
                    unit = "g/h";
                    break;
                case 73:
                    unit = "kg/s";
                    break;
                case 74:
                    unit = "kg/min";
                    break;
                case 75:
                    unit = "kg/h";
                    break;
                case 76:
                    unit = "kg/d";
                    break;
                case 77:
                    unit = "t/min";
                    break;
                case 78:
                    unit = "t/h";
                    break;
                case 79:
                    unit = "t/d";
                    break;
                case 80:
                    unit = "lb/s";
                    break;
                case 81:
                    unit = "lb/min";
                    break;
                case 82:
                    unit = "lb/h";
                    break;
                case 83:
                    unit = "lb/d";
                    break;
                default:
                    unit = string.Empty;
                    break;
            }
            return unit;
        }

        /// <summary>
        /// read volume flow unit
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>volume flow unit</returns>
        public string ReadVolumeFlowUnit(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[2];
            string unit = string.Empty;

            ReadInputRegisters(DevAdr, 46, 1, ref TempValues, ComPort);
            switch (TempValues[1])
            {
                case 240:
                    unit = "cm³/s";
                    break;
                case 241:
                    unit = "cm³/min";
                    break;
                case 242:
                    unit = "cm³/h";
                    break;
                case 24:
                    unit = "l/s";
                    break;
                case 17:
                    unit = "l/min";
                    break;
                case 138:
                    unit = "l/h";
                    break;
                case 221:
                    unit = "l/d";
                    break;
                case 28:
                    unit = "m³/s";
                    break;
                case 131:
                    unit = "m³/min";
                    break;
                case 19:
                    unit = "m³/h";
                    break;
                case 29:
                    unit = "m³/d";
                    break;
                case 22:
                    unit = "gal/s";
                    break;
                case 16:
                    unit = "gal/min";
                    break;
                case 136:
                    unit = "gal/h";
                    break;
                case 235:
                    unit = "gal/d";
                    break;
                case 26:
                    unit = "Cuft/s";
                    break;
                case 15:
                    unit = "Cuft/min";
                    break;
                case 130:
                    unit = "Cuft/h";
                    break;
                case 27:
                    unit = "Cuft/d";
                    break;
                case 132:
                    unit = "bbl/s";
                    break;
                case 133:
                    unit = "bbl/min";
                    break;
                case 134:
                    unit = "bbl/h";
                    break;
                case 135:
                    unit = "bbl/d";
                    break;
                case 137:
                    unit = "Impgal/s";
                    break;
                case 18:
                    unit = "Impgal/min";
                    break;
                case 30:
                    unit = "Impgal/h";
                    break;
                case 31:
                    unit = "Impgal/d";
                    break;
                case 176:
                    unit = "I(N)/s";
                    break;
                case 175:
                    unit = "I(N)/min";
                    break;
                case 122:
                    unit = "I(N)/h";
                    break;
                case 222:
                    unit = "I(N)/d";
                    break;
                case 183:
                    unit = "m³(N)/s";
                    break;
                case 182:
                    unit = "m³(N)/min";
                    break;
                case 121:
                    unit = "m³(N)/h";
                    break;
                case 181:
                    unit = "m³(N)/d";
                    break;
                case 180:
                    unit = "Sl/s";
                    break;
                case 179:
                    unit = "Sl/min";
                    break;
                case 178:
                    unit = "Sl/h";
                    break;
                case 177:
                    unit = "Sl/d";
                    break;
                case 186:
                    unit = "Scuft/s";
                    break;
                case 123:
                    unit = "Scuft/min";
                    break;
                case 248:
                    unit = "Scuft/h";
                    break;
                case 184:
                    unit = "Scuft/d";
                    break;
                case 190:
                    unit = "Sm³/s";
                    break;
                case 189:
                    unit = "Sm³/min";
                    break;
                case 188:
                    unit = "Sm³/h";
                    break;
                case 187:
                    unit = "Sm³/d";
                    break;
                default:
                    unit = string.Empty;
                    break;
            }
            return unit;
        }

        /// <summary>
        /// read density unit
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>density unit</returns>
        public string ReadDensityUnit(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[2];
            string unit = string.Empty;

            ReadInputRegisters(DevAdr, 47, 1, ref TempValues, ComPort);
            switch (TempValues[1])
            {
                case 95:
                    unit = "g/ml";
                    break;
                case 96:
                    unit = "kg/l";
                    break;
                case 92:
                    unit = "kg/m³";
                    break;
                case 93:
                    unit = "lb/gal";
                    break;
                case 94:
                    unit = "lb/Cuft";
                    break;
                case 91:
                    unit = "g/cm³";
                    break;
                case 97:
                    unit = "g/l";
                    break;
                case 102:
                    unit = "°Bé hv";
                    break;
                case 103:
                    unit = "°Bé lt";
                    break;
                case 104:
                    unit = "°API";
                    break;
                default:
                    unit = string.Empty;
                    break;
            }
            return unit;
        }

        /// <summary>
        /// read mass flow total unit
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>total mass flow unit</returns>
        public string ReadTotalMassUnit(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[2];
            string unit = string.Empty;

            ReadInputRegisters(DevAdr, 51, 1, ref TempValues, ComPort);
            switch (TempValues[1])
            {
                case 60:
                    unit = "g";
                    break;
                case 61:
                    unit = "kg";
                    break;
                case 62:
                    unit = "t";
                    break;
                case 63:
                    unit = "lb";
                    break;
                default:
                    unit = string.Empty;
                    break;
            }
            return unit;
        }

        /// <summary>
        /// read volume total unit
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>total volume unit</returns>
        public string ReadTotalVolumeUnit(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[2];
            string unit = string.Empty;

            ReadInputRegisters(DevAdr, 52, 1, ref TempValues, ComPort);
            switch (TempValues[1])
            {
                case 245:
                    unit = "cm³";
                    break;
                case 41:
                    unit = "l";
                    break;
                case 43:
                    unit = "m³";
                    break;
                case 40:
                    unit = "gal";
                    break;
                case 243:
                    unit = "kgal";
                    break;
                case 112:
                    unit = "Cuft";
                    break;
                case 46:
                    unit = "bbl";
                    break;
                case 42:
                    unit = "Impgal";
                    break;
                case 244:
                    unit = "klmpgal";
                    break;
                case 167:
                    unit = "I(N)";
                    break;
                case 166:
                    unit = "m³(N)";
                    break;
                case 171:
                    unit = "SI";
                    break;
                case 168:
                    unit = "Scuft";
                    break;
                case 249:
                    unit = "MMscuft";
                    break;
                case 172:
                    unit = "Sm³";
                    break;
                default:
                    unit = string.Empty;
                    break;
            }
            return unit;
        }

        /// <summary>
        /// read velocity unit
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>velocity unit</returns>
        public string ReadVelocityUnit(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[2];
            string unit = string.Empty;

            ReadInputRegisters(DevAdr,49, 1, ref TempValues, ComPort);
            switch (TempValues[1])
            {
                case 21:
                    unit = "m/s";
                    break;
                case 20:
                    unit = "ft/s";
                    break;
                default:
                    unit = string.Empty;
                    break;
            }
            return unit;
        }

        /// <summary>
        /// read temperature unit
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>temperature unit</returns>
        public string ReadTemperatureUnit(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[2];
            string unit = string.Empty;

            ReadInputRegisters(DevAdr, 48, 1, ref TempValues, ComPort);
            switch (TempValues[1])
            {
                case 32:
                    unit = "°C";
                    break;
                case 33:
                    unit = "°F";
                    break;
                case 35:
                    unit = "K";
                    break;
                default:
                    unit = string.Empty;
                    break;
            }
            return unit;
        }

        /// <summary>
        /// enable counter reset
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        public void EnableCounterReset(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            WriteHoldingRegisters(DevAdr, 848, 1, ComPort);
        }

        /// <summary>
        /// reset counter
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="type">type of counter</param>
        /// <param name="ComPort">serial port</param>
        public void ResetCounter(byte DevAdr, CounterTypesReset type , System.IO.Ports.SerialPort ComPort)
        {
            WriteHoldingRegisters(DevAdr, 864, (byte)type, ComPort);
        }

        /// <summary>
        /// read F-Total mass
        /// </summary>
        /// <param name="ComPort">serial port</param>
        /// <returns>MassFTotal</returns>
        public float ReadMassFTotal(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[9];

            ReadInputRegisters(DevAdr, 14, 2, ref TempValues, ComPort);
            return CalculateModBusFloatFrom4Bytes(TempValues[0], TempValues[1], TempValues[2], TempValues[3]);
        }

        /// <summary>
        /// read F-Total vol
        /// </summary>
        /// <param name="ComPort">serial port</param>
        /// <returns>VolFTotal</returns>
        public float ReadVolFTotal(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[9];

            ReadInputRegisters(DevAdr, 20, 2, ref TempValues, ComPort);
            return CalculateModBusFloatFrom4Bytes(TempValues[0], TempValues[1], TempValues[2], TempValues[3]);
        }

        /// <summary>
        /// read Volume flow
        /// </summary>
        /// <param name="ComPort">serial port</param>
        /// <returns>Volume flow</returns>
        public float ReadVolumeFlow(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[9];

            ReadInputRegisters(DevAdr, 2, 2, ref TempValues, ComPort);
            return CalculateModBusFloatFrom4Bytes(TempValues[0], TempValues[1], TempValues[2], TempValues[3]);
        }

        /// <summary>
        /// read density
        /// </summary>
        /// <param name="ComPort">serial port</param>
        /// <returns>density</returns>
        public float ReadDensity(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[9];

            ReadInputRegisters(DevAdr, 4, 2, ref TempValues, ComPort);
            return CalculateModBusFloatFrom4Bytes(TempValues[0], TempValues[1], TempValues[2], TempValues[3]);
        }

        /// <summary>
        /// read velocity
        /// </summary>
        /// <param name="ComPort">serial port</param>
        /// <returns>Velocity</returns>
        public float ReadVelocity(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[9];

            ReadInputRegisters(DevAdr, 38, 2, ref TempValues, ComPort);
            return CalculateModBusFloatFrom4Bytes(TempValues[0], TempValues[1], TempValues[2], TempValues[3]);
        }

        /// <summary>
        /// read modbus option 
        /// </summary>
        /// <param name="ComPort">serial port</param>
        /// <returns>2=/MB2, 3=/MB3</returns>
        public byte GetInterfaceOption(System.IO.Ports.SerialPort ComPort)
        {
            byte[] TempValues = new byte[2];

            ReadHoldingRegisters(1, 2000, 1, ref TempValues, ComPort);
            return TempValues[1]; ;
        }

        /// <summary>
        /// enter service menu for 10 minutes
        /// </summary>
        /// <param name="ComPort">serial port</param>
        public void EnterServicePassword(System.IO.Ports.SerialPort ComPort)
        {
                WriteMultipleRegisters(0, 6880, "ROTA2003", ComPort);
        }

        /// <summary>
        /// write modbus key
        /// </summary>
        /// <param name="Modbuskey">/MB2 or /MB3</param>
        /// <param name="ComPort">serialport</param>
        public void WriteModbusKey(string Modbuskey, System.IO.Ports.SerialPort ComPort)
        {
            if(Modbuskey == "/MB3")
                WriteMultipleRegisters(0, 6800, "L64CH935", ComPort);
            else if(Modbuskey == "/MB2")
                WriteMultipleRegisters(0, 6800, "ABCDEFGH", ComPort);              
        }

        /// <summary>
        /// execute autozero
        /// </summary>
        /// <param name="DevAdr">device adress</param>
        /// <param name="ComPort">serial port</param>
        public void ExecuteAutozero(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            WriteHoldingRegisters(DevAdr, 992, 1, ComPort);
        }

        /// <summary>
        /// get suction pressure
        /// </summary>
        /// <param name="PumpMeterAdress">adress of pump meter</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>pressure value in bar</returns>
        public float GetSuctionPressure(byte PumpMeterAdress, System.IO.Ports.SerialPort ComPort)
        {
            byte[] arrReply = new byte[4];
            Int32 value = 0;

            ReadHoldingRegisters(PumpMeterAdress, 0x4502, 2, ref arrReply, ComPort);
            value=CalculateInt32From4Bytes(arrReply[0], arrReply[1], arrReply[2], arrReply[3]);
            return Convert.ToSingle(value / 100000.0);
        }

        /// <summary>
        /// get end pressure
        /// </summary>
        /// <param name="PumpMeterAdress">adress of pump meter</param>
        /// <param name="ComPort">serial port</param>
        /// <returns>pressure value in bar</returns>
        public float GetEndPressure(byte PumpMeterAdress, System.IO.Ports.SerialPort ComPort)
        {
            byte[] arrReply = new byte[4];
            Int32 value = 0;

            ReadHoldingRegisters(PumpMeterAdress, 0x4504, 2, ref arrReply, ComPort);
            value = CalculateInt32From4Bytes(arrReply[0], arrReply[1], arrReply[2], arrReply[3]);
            return Convert.ToSingle(value / 100000.0);
        }

        public float ReadMassFlURV(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] arrReply = new byte[4];

            ReadHoldingRegisters(DevAdr, 1104, 2, ref arrReply, ComPort);
            return CalculateModBusFloatFrom4Bytes(arrReply[0], arrReply[1], arrReply[2], arrReply[3]);
        }

        public float ReadMassFlLRV(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] arrReply = new byte[4];

            ReadHoldingRegisters(DevAdr, 1088, 2, ref arrReply, ComPort);
            if(arrReply[0]==0 && arrReply[1]==0 && arrReply[2]==0 &&arrReply[3]==0)
                return 0;
            return CalculateModBusFloatFrom4Bytes(arrReply[0], arrReply[1], arrReply[2], arrReply[3]);
        }

        public float ReadVolFlURV(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] arrReply = new byte[4];

            ReadHoldingRegisters(DevAdr, 1280, 2, ref arrReply, ComPort);
            return CalculateModBusFloatFrom4Bytes(arrReply[0], arrReply[1], arrReply[2], arrReply[3]);
        }

        public float ReadVolFlLRV(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] arrReply = new byte[4];

            ReadHoldingRegisters(DevAdr, 1264, 2, ref arrReply, ComPort);
            if (arrReply[0] == 0 && arrReply[1] == 0 && arrReply[2] == 0 && arrReply[3] == 0)
                return 0;
            return CalculateModBusFloatFrom4Bytes(arrReply[0], arrReply[1], arrReply[2], arrReply[3]);
        }
        public float ReadPulse1Rate(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] arrReply = new byte[4];

            ReadHoldingRegisters(DevAdr, 4416, 2, ref arrReply, ComPort);
            if (arrReply[0] == 0 && arrReply[1] == 0 && arrReply[2] == 0 && arrReply[3] == 0)
                return 0;
            return CalculateModBusFloatFrom4Bytes(arrReply[0], arrReply[1], arrReply[2], arrReply[3]);
        }
        /// <summary>
        /// Write MassFlow LRV
        /// </summary>
        /// <param name="DevAdr">Address</param>
        /// <param name="InputFloat">Float Number</param>
        /// <param name="ComPort">Port</param>
        public void WriteMassFlLRV(byte DevAdr, float InputFloat, System.IO.Ports.SerialPort ComPort)
        {
            byte[] InputValue = new byte[4];
            if (InputFloat != 0)
                InputValue = CalculateModBusFloat(InputFloat);
            WriteMultipleRegisters(DevAdr, 1088, InputValue, ComPort);
        }
        /// <summary>
        /// Write MassFlow URV
        /// </summary>
        /// <param name="DevAdr">Address</param>
        /// <param name="InputFloat">Float Number</param>
        /// <param name="ComPort">Port</param>
        public void WriteMassFlURV(byte DevAdr, float InputFloat, System.IO.Ports.SerialPort ComPort)
        {
            byte[] InputValue = new byte[4];
            InputValue=CalculateModBusFloat(InputFloat);
            WriteMultipleRegisters(DevAdr, 1104, InputValue, ComPort);
        }
        /// <summary>
        /// Write VolumeFlow LRV
        /// </summary>
        /// <param name="DevAdr">Address</param>
        /// <param name="InputFloat">Float Number</param>
        /// <param name="ComPort">Port</param>
        public void WritVolFlLRV(byte DevAdr, float InputFloat, System.IO.Ports.SerialPort ComPort)
        {
            byte[] InputValue = new byte[4];
            if (InputFloat != 0)
                InputValue = CalculateModBusFloat(InputFloat);
            WriteMultipleRegisters(DevAdr, 1264, InputValue, ComPort);
        }
        /// <summary>
        /// Write VolumeFlow URV
        /// </summary>
        /// <param name="DevAdr">Address</param>
        /// <param name="InputFloat">Float Number</param>
        /// <param name="ComPort">Port</param>
        public void WritVolFlURV(byte DevAdr, float InputFloat, System.IO.Ports.SerialPort ComPort)
        {
            byte[] InputValue = new byte[4];
            InputValue = CalculateModBusFloat(InputFloat);
            WriteMultipleRegisters(DevAdr, 1280, InputValue, ComPort);
        }

        public void WritePulse1Rate(byte DevAdr, float InputFloat, System.IO.Ports.SerialPort ComPort)
        {
            byte[] InputValue = new byte[4];
            InputValue = CalculateModBusFloat(InputFloat);
            WriteMultipleRegisters(DevAdr, 4416, InputValue, ComPort);
        }
        /// <summary>
        /// read pressure value of the device
        /// </summary>
        /// <param name="DevAdr">device address</param>
        /// <param name="ComPort">Modbus Comport</param>
        /// <returns>pressure value</returns>
        public float ReadPressure(byte DevAdr, System.IO.Ports.SerialPort ComPort)
        {
            byte[] arrReply = new byte[4];

            ReadHoldingRegisters(DevAdr, 1584, 2, ref arrReply, ComPort);
            return CalculateModBusFloatFrom4Bytes(arrReply[0], arrReply[1], arrReply[2], arrReply[3]);
        }
        /// <summary>
        /// Write pressure value to the device
        /// </summary>
        /// <param name="DevAdr">Device address</param>
        /// <param name="InputFloat">Pressure value</param>
        /// <param name="ComPort">Modbus Comport</param>
        public void WritPressure(byte DevAdr, float InputFloat, System.IO.Ports.SerialPort ComPort)
        {
            byte[] InputValue = new byte[4];
            if (InputFloat != 0)
                InputValue = CalculateModBusFloat(InputFloat);
            WriteMultipleRegisters(DevAdr, 1584, InputValue, ComPort);
        }
        /// <summary>
        /// Write PumpDrive Command
        /// </summary>
        /// <param name="DevAdr">Device address</param>
        /// <param name="Command">Command</param>
        /// <param name="ComPort">SerialPort</param>
        public void WritePumpDriveCommand(byte DevAdr, byte[] Command, System.IO.Ports.SerialPort ComPort)
        {
            WriteMultipleRegisters(DevAdr, 0x481C, Command, ComPort);
        }
        /// <summary>
        /// Write PumpDrive Mode 
        /// </summary>
        /// <param name="DevAdr">Device address</param>
        /// <param name="Command">Command</param>
        /// <param name="ComPort">SerialPort</param>
        public void WritePumpDriveMode(byte DevAdr, byte[] Command, System.IO.Ports.SerialPort ComPort)
        {
            WriteMultipleRegisters(DevAdr, 0x4110, Command, ComPort);
        }
        /// <summary>
        /// write Value to PumpDrive
        /// </summary>
        /// <param name="DevAdr">Device address</param>
        /// <param name="SollValue">Value between 50% and 100%</param>
        /// <param name="ComPort">SerialPort</param>
        public void WritePumpDriveSollwertBus(byte DevAdr, byte[] SollValue, System.IO.Ports.SerialPort ComPort)
        {
            WriteMultipleRegisters(DevAdr, 0x4814, SollValue, ComPort);
        }

        public void ReadPumpDriveSollwertBus(byte DevAdr, byte[] arrReply,System.IO.Ports.SerialPort ComPort)
        {
            ReadHoldingRegisters(DevAdr, 0x4814, 2, ref arrReply, ComPort);
        }
        /// <summary>
        /// read PumpDrive status
        /// </summary>
        /// <param name="DevAdr">Device address</param>
        /// <param name="ComPort">SerialPort</param>
        public void ReadPumpDriveStatus(byte DevAdr,byte[] arrReply, System.IO.Ports.SerialPort ComPort)
        {
            ReadHoldingRegisters(DevAdr, 0x4440, 2, ref arrReply, ComPort);
        }

        #endregion
    }
}
