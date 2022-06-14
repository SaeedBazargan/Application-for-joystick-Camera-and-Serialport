using System.IO;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class SerialPacketHandler
    {
        byte[] LookUpTable = new byte[55];
        byte CurrentState = 0;

        public void Master_CheckPacket(byte[] Rx_Data)
        {
            if (Rx_Data[0] == 85 && CurrentState == 0)
                CurrentState = 1;

            if (Rx_Data[1] == 170 && CurrentState == 1)
                CurrentState = 2;

            if (CurrentState == 2)
            {
                for (byte i = 2; i < 52; i++)
                    LookUpTable[i - 2] = Rx_Data[i];
                CurrentState = 3;
            }
            if (CurrentState == 3 && CheckCRC(LookUpTable, Rx_Data))
            {
                //SplitLookupTable(LookUpTable);
                CurrentState = 0;
            }
            else
            {
                CurrentState = 0;
                Array.Clear(Rx_Data, 0, Rx_Data.Length);
            }
        }
        public void SplitLookupTable(byte[] LUT)
        {
            //byte[] SpliCounter = new byte[4] { LUT[0], LUT[1], LUT[2], LUT[3] };
            //byte[] Data_1 = new byte[4] { LUT[6], LUT[7], LUT[8], LUT[9] };
            //byte[] Data_2 = new byte[4] { LUT[10], LUT[11], LUT[12], LUT[13] };
            //byte[] Data_3 = new byte[4] { LUT[14], LUT[15], LUT[16], LUT[17] };
            //byte[] Data_4 = new byte[4] { LUT[18], LUT[19], LUT[20], LUT[21] };
            //byte[] Data_5 = new byte[4] { LUT[22], LUT[23], LUT[24], LUT[25] };
            //byte[] Data_6 = new byte[4] { LUT[26], LUT[27], LUT[28], LUT[29] };
            //byte[] Data_7 = new byte[4] { LUT[30], LUT[31], LUT[32], LUT[33] };
            //byte[] Data_8 = new byte[4] { LUT[34], LUT[35], LUT[36], LUT[37] };
            //byte[] Data_9 = new byte[4] { LUT[38], LUT[39], LUT[40], LUT[41] };
            //byte[] Data_10 = new byte[4] { LUT[42], LUT[43], LUT[44], LUT[45] };
            //byte[] Data_11 = new byte[4] { LUT[46], LUT[47], LUT[48], LUT[49] };

            //Console.WriteLine("111:::" + BitConverter.ToInt32(SpliCounter, 0));
            //Console.WriteLine("333:::" + BitConverter.ToInt32(Data_1, 0));
            //Console.WriteLine("444:::" + BitConverter.ToInt32(Data_2, 0));
            //Console.WriteLine("555:::" + BitConverter.ToInt32(Data_3, 0));
            //Console.WriteLine("666:::" + BitConverter.ToInt32(Data_4, 0));
            //Console.WriteLine("777:::" + BitConverter.ToInt32(Data_5, 0));
            //Console.WriteLine("888:::" + BitConverter.ToInt32(Data_6, 0));
            //Console.WriteLine("999:::" + BitConverter.ToInt32(Data_7, 0));
            //Console.WriteLine("1111:::" + BitConverter.ToInt32(Data_8, 0));
            //Console.WriteLine("1010:::" + BitConverter.ToInt32(Data_9, 0));
            //Console.WriteLine("1212:::" + BitConverter.ToInt32(Data_10, 0));
            //Console.WriteLine("1313:::" + BitConverter.ToInt32(Data_11, 0));
        }
        public bool CheckCRC(byte[] LutData, byte[] rxData)
        {
            int Len = LutData.Length;
            if (xorOfArray(LutData, Len) == rxData[52])
                return true;
            else
                return false;
        }
        static int xorOfArray(byte[] arr, int n)
        {
            int xor_arr = 0;

            for (int i = 0; i < n; i++)
                xor_arr = xor_arr ^ arr[i];
            return xor_arr;
        }
    }
}
