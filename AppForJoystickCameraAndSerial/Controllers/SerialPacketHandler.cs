namespace AppForJoystickCameraAndSerial.Controllers
{
    public class SerialPacketHandler
    {
        byte[] LookUpTable = new byte[55];
        byte Counter = 0;
        byte[] Data_CRC = new byte[52];
        byte CurrentState = 0;

        public void Master_CheckPacket(byte[] Rx_Data, string RecordDir, bool Record, int index)
        {
            //for (byte i = 0; i < 55; i++)
            //{
            //    Console.Write(i + ":      ");
            //    Console.WriteLine(Rx_Data[i]);
            //}

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
                if (Record)
                {
                    string recordingDir = RecordDir + index.ToString() + '/';
                    if (!Directory.Exists(recordingDir))
                        Directory.CreateDirectory(recordingDir);
                    string recordingPath = recordingDir + "Log" + ".txt";
                    File.AppendAllText(recordingPath, '\n' + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + "  : ");
                    for (int j = 0; j < 55; j++)
                        File.AppendAllText(recordingPath, "" + Rx_Data[j] + ',');
                }
                SplitLookupTable(LookUpTable);
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
            Int32 Counter;
            byte Address;
            byte Code;
            Int32 Data_1;
            Int32 Data_2;
            Int32 Data_3;
            Int32 Data_4;
            Int32 Data_5;
            Int32 Data_6;
            Int32 Data_7;
            Int32 Data_8;
            Int32 Data_9;
            Int32 Data_10;
            Int32 Data_11;

            Counter = (LUT[0] << 24) + (LUT[1] << 16) + (LUT[2] << 8) + (LUT[3]);
            Address = LUT[4];
            Code = LUT[5];
            Data_1 = (LUT[6] << 24) + (LUT[7] << 16) + (LUT[8] << 8) + (LUT[9]);
            Data_2 = (LUT[10] << 24) + (LUT[11] << 16) + (LUT[12] << 8) + (LUT[13]);
            Data_3 = (LUT[14] << 24) + (LUT[15] << 16) + (LUT[16] << 8) + (LUT[17]);
            Data_4 = (LUT[18] << 24) + (LUT[19] << 16) + (LUT[20] << 8) + (LUT[21]);
            Data_5 = (LUT[22] << 24) + (LUT[23] << 16) + (LUT[24] << 8) + (LUT[25]);
            Data_6 = (LUT[26] << 24) + (LUT[27] << 16) + (LUT[28] << 8) + (LUT[29]);
            Data_7 = (LUT[30] << 24) + (LUT[31] << 16) + (LUT[32] << 8) + (LUT[33]);
            Data_8 = (LUT[34] << 24) + (LUT[35] << 16) + (LUT[36] << 8) + (LUT[37]);
            Data_9 = (LUT[38] << 24) + (LUT[39] << 16) + (LUT[40] << 8) + (LUT[41]);
            Data_10 = (LUT[42] << 24) + (LUT[43] << 16) + (LUT[44] << 8) + (LUT[45]);
            Data_11 = (LUT[46] << 24) + (LUT[47] << 16) + (LUT[48] << 8) + (LUT[49]);

            Console.WriteLine("111:::" + Counter);
            Console.WriteLine("222:::" + Address);
            Console.WriteLine("333:::" + Code);
            Console.WriteLine("444:::" + Data_1);
            Console.WriteLine("555:::" + Data_2);
            Console.WriteLine("666:::" + Data_3);
            Console.WriteLine("777:::" + Data_4);
            Console.WriteLine("888:::" + Data_5);
            Console.WriteLine("999:::" + Data_6);
            Console.WriteLine("1010:::" + Data_7);
            Console.WriteLine("1111:::" + Data_8);
            Console.WriteLine("1212:::" + Data_9);
            Console.WriteLine("1313:::" + Data_10);
            Console.WriteLine("1414:::" + Data_11);
            Console.WriteLine("HHHHEEEEELLLLLLOOOOOOOOO");
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
        public void WriteMessage_Generator(byte Code, byte Address, Int32[] Tx_Data, byte Length, byte[] Template)
        {
            Counter++;
            Template[0] = 0x55;                                                                     //Header 1
            Template[1] = 0xAA;                                                                     //Header 2
            Template[2] = Counter; Template[3] = 0x00; Template[4] = 0x00; Template[5] = 0x00;      //Counter
            Template[6] = Address;                                                                  //Address
            Template[7] = Code;                                                                     //Code

            if (Length == 1)
            {
                Template[8]  = (byte)((Tx_Data[0]) & 0xFF);
                Template[9]  = (byte)((Tx_Data[0] >> 8) & 0xFF); 
                Template[10] = (byte)((Tx_Data[0] >> 16) & 0xFF);
                Template[11] = (byte)((Tx_Data[0] >> 24) & 0xFF);

                //Console.WriteLine("FFFFF = " + ((Template[8] << 24) + (Template[9] << 16) + (Template[10] << 8) + Template[11]));
            }
            if (Length == 2)
            {
                Template[8] = (byte)((Tx_Data[0]) & 0xFF);
                Template[9] = (byte)((Tx_Data[0] >> 8) & 0xFF);
                Template[10] = (byte)((Tx_Data[0] >> 16) & 0xFF);
                Template[11] = (byte)((Tx_Data[0] >> 24) & 0xFF);
                //Console.WriteLine("AAAAA = " + ((Template[11] << 24) + (Template[10] << 16) + (Template[9] << 8) + Template[8]));

                Template[12] = (byte)((Tx_Data[1]) & 0xFF);
                Template[13] = (byte)((Tx_Data[1] >> 8) & 0xFF);
                Template[14] = (byte)((Tx_Data[1] >> 16) & 0xFF);
                Template[15] = (byte)((Tx_Data[1] >> 24) & 0xFF);

                //Console.WriteLine("BBBBB = " + ((Template[15] << 24) + (Template[14] << 16) + (Template[13] << 8) + Template[12]));
                //Console.WriteLine("CCCCC = " + ((buffer[0] << 24) + (buffer[1] << 16) + (buffer[2] << 8) + buffer[3]));
            }
            //else
            //{
            //    Template[8] = 0x00; Template[9] = 0x00; Template[10] = 0x00; Template[11] = 0x00;   //Data 1
            //    Template[12] = 0x00; Template[13] = 0x00; Template[14] = 0x00; Template[15] = 0x00; //Data 2
            //}
            Template[16] = 0x00; Template[17] = 0x00; Template[18] = 0x00; Template[19] = 0x00;     //Data 3
            Template[20] = 0x00; Template[21] = 0x00; Template[22] = 0x00; Template[23] = 0x00;     //Data 4
            Template[24] = 0x00; Template[25] = 0x00; Template[26] = 0x00; Template[27] = 0x00;     //Data 5
            Template[28] = 0x00; Template[29] = 0x00; Template[30] = 0x00; Template[31] = 0x00;     //Data 6
            Template[32] = 0x00; Template[33] = 0x00; Template[34] = 0x00; Template[35] = 0x00;     //Data 7
            Template[36] = 0x00; Template[37] = 0x00; Template[38] = 0x00; Template[39] = 0x00;     //Data 8
            Template[40] = 0x00; Template[41] = 0x00; Template[42] = 0x00; Template[43] = 0x00;     //Data 9
            Template[44] = 0x00; Template[45] = 0x00; Template[46] = 0x00; Template[47] = 0x00;     //Data 10
            Template[48] = 0x00; Template[49] = 0x00; Template[50] = 0x00; Template[51] = 0x00;     //Data 11


            Template[52] = 0x00;                                                                    //CRC
            Template[53] = 0x40;                                                                    //Footer 1
            Template[54] = 0x24;                                                                    //Footer 2
            for (byte i = 0; i < 52; i++)
            {
                Data_CRC[i] = Template[i];
            }
            int SizeData_CRC = Data_CRC.Length;
            Template[52] = (byte)xorOfArray(Data_CRC, SizeData_CRC);
        }
    }
}
