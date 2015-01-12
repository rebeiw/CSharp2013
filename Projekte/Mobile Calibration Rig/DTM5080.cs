using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Calibration_Rig
{
    class DTM5080
    {
        /// <summary>
        /// get temperature value
        /// </summary>
        /// <param name="Port">serial port</param>
        /// <returns>temperature</returns>
        public double Get_Temp(System.IO.Ports.SerialPort Port)
        {
            double doubTemp=0;            

            Port.DiscardInBuffer();
            Port.DiscardOutBuffer();
            Port.Write("D");
            System.Threading.Thread.Sleep(50);                      //25 is possible
            string Buffer_Temp = Port.ReadExisting();
            string strTemp = (Buffer_Temp.Substring(2, 2) + "," + Buffer_Temp.Substring(4, 2));             //String wird in Kommazahl convertiert
            doubTemp = Convert.ToDouble(strTemp);

            return doubTemp;
        }

        /// <summary>
        /// get device serial no
        /// </summary>
        /// <param name="Port">serial port</param>
        /// <returns>serial no</returns>
        public string Get_Temp_Serialno(System.IO.Ports.SerialPort Port)
        {
            Port.Write("L");
            System.Threading.Thread.Sleep(100);
            string Buffer_Serial = Port.ReadExisting();
            string strTemp_Serialno = Buffer_Serial.Substring(0, 5);
            return strTemp_Serialno;
            }

        /// <summary>
        /// get temp sensor type
        /// </summary>
        /// <param name="Port">serial port</param>
        /// <returns></returns>
        public string Get_Temp_Typ(System.IO.Ports.SerialPort Port)
        {
            Port.Write("T");
            System.Threading.Thread.Sleep(100);
            string Buffer_Typ = Port.ReadExisting();
            string strTemp_Typ = Buffer_Typ.Substring(0, 4);
            return strTemp_Typ;

        }

        /// <summary>
        /// get temperature resolution
        /// </summary>
        /// <param name="Port">serial port</param>
        /// <returns>temperature resolution</returns>
        public string Get_Temp_resolution(System.IO.Ports.SerialPort Port)
        {
            Port.Write("A");
            System.Threading.Thread.Sleep(100);
            string Buffer_Resolution = Port.ReadExisting();
            string strTemp_Typ = Buffer_Resolution.Substring(0, 3);
            return strTemp_Typ;
        }

        /// <summary>
        /// set temperature resistor
        /// </summary>
        public void Set_Temp_Resistance(System.IO.Ports.SerialPort Port)
        {
            Port.Write("B1");
            System.Threading.Thread.Sleep(100);
            string Buffer_Pt100 = Port.ReadExisting();
            if (Buffer_Pt100 != ":")
                System.Windows.Forms.MessageBox.Show("Temperature resistor setting error", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

    }
}
