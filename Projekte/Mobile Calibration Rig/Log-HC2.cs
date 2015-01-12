using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Calibration_Rig
{
    class Log_HC2
    {
        //################################################################################################################
        private byte[] table = new byte[256];
        //############################################################################################################


        public void Initialize(System.IO.Ports.SerialPort Port)
        {
            byte[] arrCommand = new byte[8];

            arrCommand[0] = 0;
            arrCommand[1] = 0;
            arrCommand[2] = 0;
            arrCommand[3] = 0;
            arrCommand[4] = 0;
            arrCommand[5] = 0;
            arrCommand[6] = 0;
            arrCommand[7] = 0;

            //arrCommand[7] = CalcCRC8(arrCommand);

            Port.Write(arrCommand, 0, 8);
            System.Threading.Thread.Sleep(100);
            Port.Write(arrCommand, 0, 8);
            System.Threading.Thread.Sleep(100);
        }

        public void Echo(System.IO.Ports.SerialPort Port)
        {
            byte[] Reply = new byte[8];
            byte[] arrCommand = new byte[7];
            byte[] arrCommandCRC = new byte[8];

            arrCommand[0] = 128;
            arrCommand[1] = 0;
            arrCommand[2] = 0;
            arrCommand[3] = 0;
            arrCommand[4] = 0;
            arrCommand[5] = 0;
            arrCommand[6] = 0;
            
            Array.Copy(arrCommand, 0, arrCommandCRC, 0, 7);
            arrCommandCRC[7] = CalcCRC8(arrCommand);
            Port.Write(arrCommandCRC, 0, 8);
            System.Threading.Thread.Sleep(100);
            Port.Read(Reply, 0, Reply.Length);
        }

        public void WakeUp(System.IO.Ports.SerialPort Port)
        {
            byte[] Reply = new byte[8];
            byte[] arrCommand = new byte[7];
            byte[] arrCommandCRC = new byte[8];

            arrCommand[0] = 138;
            arrCommand[1] = 1;
            arrCommand[2] = 0;
            arrCommand[3] = 0;
            arrCommand[4] = 0;
            arrCommand[5] = 0;
            arrCommand[6] = 0;

            Array.Copy(arrCommand, 0, arrCommandCRC, 0, 7);
            arrCommandCRC[7] = CalcCRC8(arrCommand);
            Port.Write(arrCommandCRC, 0, 8);
            System.Threading.Thread.Sleep(100);
            Port.Read(Reply, 0, Reply.Length);
            if (Reply[0] != 10)
            {
                System.Windows.Forms.MessageBox.Show("communication error", "Datalogger error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                if(Reply[1]==0)
                    System.Windows.Forms.MessageBox.Show("Speicher ist belegt", "Datalogger error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        public void TaskStart(System.IO.Ports.SerialPort Port)
        {
            byte[] ReplyData = new byte[2];
            byte[] Reply = new byte[8];
            byte[] arrCommand = new byte[7];
            byte[] arrCommandCRC = new byte[8];

            arrCommand[0] = 134;
            arrCommand[1] = 2;
            arrCommand[2] = 0;
            arrCommand[3] = 255;
            arrCommand[4] = 67;
            arrCommand[5] = 0;
            arrCommand[6] = 0;
            Array.Copy(arrCommand, 0, arrCommandCRC, 0, 7);
            arrCommandCRC[7] = CalcCRC8(arrCommand);
            Port.Write(arrCommandCRC, 0, 8);
            System.Threading.Thread.Sleep(100);
            Port.Read(Reply, 0, Reply.Length);
        }

        public void TaskChannelInfo(System.IO.Ports.SerialPort Port,int channel)
        {
            byte[] Reply = new byte[8];
            byte[] arrCommand = new byte[7];
            byte[] arrCommandCRC = new byte[8];

            arrCommand[0] = 135;
            arrCommand[1] = 0;
            arrCommand[2] = 0;//(byte)channel;
            arrCommand[3] = 0;
            arrCommand[4] = 0;
            arrCommand[5] = 0;
            arrCommand[6] = 0;
            Array.Copy(arrCommand, 0, arrCommandCRC, 0, 7);
            arrCommandCRC[7] = CalcCRC8(arrCommand);
            Port.Write(arrCommandCRC, 0, 8);
            System.Threading.Thread.Sleep(100);
            Port.Read(Reply, 0, Reply.Length);
        }

        public void ReadMesureData(System.IO.Ports.SerialPort Port, ref float Val0, ref float Val1, ref float Val2, int ch0, int ch1, int ch2)
        {
            byte[] ReplyData = new byte[2];
            byte[] Reply = new byte[8];
            byte[] arrCommand = new byte[7];
            byte[] arrCommandCRC = new byte[8];

            arrCommand[0] = 130;
            arrCommand[1] = 2;
            arrCommand[2] = (byte)ch0;      //Channel 0     Presure                 //Channel 1     AccX        //Channel 14    AkkuCapacity
            arrCommand[3] = (byte)ch1;      //Channel 5     relative humidity       //Channel 2     AccY
            arrCommand[4] = (byte)ch2;      //Channel 6     temperature             //Channel 3     AccZ
            arrCommand[5] = 0;
            arrCommand[6] = 0;

            Array.Copy(arrCommand, 0, arrCommandCRC, 0, 7);
            arrCommandCRC[7] = CalcCRC8(arrCommand);
            Port.Write(arrCommandCRC, 0, 8);
            System.Threading.Thread.Sleep(100);
            Port.Read(Reply, 0, Reply.Length);
            if (Reply[0] == (arrCommand[1] + 0x20))
            {
                System.Windows.Forms.MessageBox.Show("communication error", "Datalogger error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                Array.Copy(Reply, 1, ReplyData, 0, 2);
                Val0 = BitConverter.ToInt16(ReplyData, 0) / (float)10;
                Array.Copy(Reply, 3, ReplyData, 0, 2);
                Val1 = BitConverter.ToInt16(ReplyData, 0) / (float)10;
                Array.Copy(Reply, 5, ReplyData, 0, 2);
                Val2 = BitConverter.ToInt16(ReplyData, 0) / (float)10;
            }
        }
        
         public void ReadDisplay(System.IO.Ports.SerialPort Port)
        {
            byte[] ReplyData = new byte[2];
            byte[] Reply = new byte[8];
            byte[] arrCommand = new byte[7];
            byte[] arrCommandCRC = new byte[8];

            arrCommand[0] = 130;
            arrCommand[1] = 1;
            arrCommand[2] = 0;      
            arrCommand[3] = 0;      
            arrCommand[4] = 0;      
            arrCommand[5] = 0;
            arrCommand[6] = 0;

            Array.Copy(arrCommand, 0, arrCommandCRC, 0, 7);
            arrCommandCRC[7] = CalcCRC8(arrCommand);
            Port.Write(arrCommandCRC, 0, 8);
            System.Threading.Thread.Sleep(100);
            Port.Read(Reply, 0, Reply.Length);
        }

        byte CalcCRC8(byte[] Command)
        {
            byte[] Table = new byte[]
            {
                0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126, 32, 163, 253, 31, 65,
                157, 195, 33, 127, 252, 162, 64, 30, 95, 1, 227, 189, 62, 96, 130, 220,
                35, 125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3, 128, 222, 60, 98,
                190, 224, 2, 92, 223, 129, 99, 61, 124, 34, 192, 158, 29, 67, 161, 255,
                70, 24, 250, 164, 39, 121, 155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
                219, 133, 103, 57, 186, 228, 6, 88, 25, 71, 165, 251, 120, 38, 196, 154,
                101, 59, 217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198, 152, 122, 36,
                248, 166, 68, 26, 153, 199, 37, 123, 58, 100, 134, 216, 91, 5, 231, 185,
                140, 210, 48, 110, 237, 179, 81, 15, 78, 16, 242, 172, 47, 113, 147, 205,
                17, 79, 173, 243, 112, 46, 204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
                175, 241, 19, 77, 206, 144, 114, 44, 109, 51, 209, 143, 12, 82, 176, 238,
                50, 108, 142, 208, 83, 13, 239, 177, 240, 174, 76, 18, 145, 207, 45, 115,
                202, 148, 118, 40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55, 213, 139,
                87, 9, 235, 181, 54, 104, 138, 212, 149, 203, 41, 119, 244, 170, 72, 22,
                233, 183, 85, 11, 136, 214, 52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
                116, 42, 200, 150, 21, 75, 169, 247, 182, 232, 10, 84, 215, 137, 107, 53
            };

            byte retval = 0;
            byte result = 0;  
            int temp_result = 0;
            byte item = 0;
            //for (int i = 0; i < datas.Count(); i++)
            for (int i = 0; i <= Command.Length-1; i++)
            {
                item = (byte)Command[i];
                temp_result = result ^ item;//Adresse aus data xor letztes Ergebnis
                result = (byte)temp_result;
                result = Table[result];
            }
            retval = result;
            return retval;
        }

        //byte CalcCRC8(byte[] datas)
        //{
        //    byte[] Table = new byte[]
        //    {
        //        0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126, 32, 163, 253, 31, 65,
        //        157, 195, 33, 127, 252, 162, 64, 30, 95, 1, 227, 189, 62, 96, 130, 220,
        //        35, 125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3, 128, 222, 60, 98,
        //        190, 224, 2, 92, 223, 129, 99, 61, 124, 34, 192, 158, 29, 67, 161, 255,
        //        70, 24, 250, 164, 39, 121, 155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
        //        219, 133, 103, 57, 186, 228, 6, 88, 25, 71, 165, 251, 120, 38, 196, 154,
        //        101, 59, 217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198, 152, 122, 36,
        //        248, 166, 68, 26, 153, 199, 37, 123, 58, 100, 134, 216, 91, 5, 231, 185,
        //        140, 210, 48, 110, 237, 179, 81, 15, 78, 16, 242, 172, 47, 113, 147, 205,
        //        17, 79, 173, 243, 112, 46, 204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
        //        175, 241, 19, 77, 206, 144, 114, 44, 109, 51, 209, 143, 12, 82, 176, 238,
        //        50, 108, 142, 208, 83, 13, 239, 177, 240, 174, 76, 18, 145, 207, 45, 115,
        //        202, 148, 118, 40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55, 213, 139,
        //        87, 9, 235, 181, 54, 104, 138, 212, 149, 203, 41, 119, 244, 170, 72, 22,
        //        233, 183, 85, 11, 136, 214, 52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
        //        116, 42, 200, 150, 21, 75, 169, 247, 182, 232, 10, 84, 215, 137, 107, 53
        //    };

        //    byte retval = 0;
        //    byte tmp1 = 0;//result  
        //    int tmp2=0;
        //    byte tmp3 = 0;
        //    //for (int i = 0; i < datas.Count(); i++)
        //    for (int i = 0; i < datas.Length; i++)
        //    {
        //        tmp3 = (byte)datas[i];
        //        tmp2 = tmp1^tmp3;       //Adresse aus data xor letztes Ergebnis
        //        tmp1 = (byte)tmp2;
        //        tmp1 = Table[tmp1];
        //    }
        //    retval = tmp1;
        //    return retval;
        //}
    }
}
