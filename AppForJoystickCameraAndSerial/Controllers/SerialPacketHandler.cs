using System.Collections;
namespace AppForJoystickCameraAndSerial.Controllers
{
    public class SerialPacketHandler
    {
        StreamWriter writer = null;

        byte[] LookUpTable = new byte[55];
        byte Counter = 0;
        byte[] Data_CRC = new byte[52];

        bool TestLog = false;
        float Ax;
        float Ay;
        float Az;
        float FOV;
        float Az_Error;
        float Ei_Error;
        float Error_X;
        float Error_Y;
        float Error_Z;
        byte NdYag_State = 0;

        public void Master_CheckPacket(byte[] Rx_Data, string RecordDir, bool Record, int index, TextBox fov_TextBox, TextBox azError_TextBox, TextBox eiError_TextBox, TextBox ax_TextBox, TextBox ay_TextBox, TextBox az_TextBox)
        {
            if (Record)
            {
                if (writer == null)
                {
                    string recordingDir = RecordDir + index.ToString() + '/';
                    if (!Directory.Exists(recordingDir))
                        Directory.CreateDirectory(recordingDir);
                    string recordingPath = recordingDir + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".txt";
                    writer = new StreamWriter(recordingPath);
                }
                if (TestLog == false)
                    writer.Write('\n' + "Ax = " + Ax + "     " + "Ay = " + Ay + "     " + "Az = " + Az + "     " + "FOV = " + FOV + "     " +
                                        "Az_Error = " + Az_Error + "     " + "Ei_Error = " + Ei_Error + "     " + "ErrorX = " + Error_X + "     "
                                      + "ErrorY  = " + Error_Y + "     " + "ErrorZ  = " + Error_Z + "     ");
                else if (TestLog == true)
                {
                    writer.Write("\n");
                    for (int i = 0; i < 55; i++)
                        writer.Write("" + Rx_Data[i] + ",");
                }
            }
            else if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }

            for (byte k = 2; k < 52; k++)
                LookUpTable[k - 2] = Rx_Data[k];
            if (CheckCRC(LookUpTable, Rx_Data))
                SplitLookupTable(LookUpTable, fov_TextBox, azError_TextBox, eiError_TextBox, ax_TextBox, ay_TextBox, az_TextBox);
        }

        public void SplitLookupTable(byte[] LUT, TextBox fov_TextBox, TextBox azError_TextBox, TextBox eiError_TextBox, TextBox ax_TextBox, TextBox ay_TextBox, TextBox az_TextBox)
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

            Counter = (LUT[3] << 24) + (LUT[2] << 16) + (LUT[1] << 8) + (LUT[0]);
            Address = LUT[4];
            Code = LUT[5];
            Data_1 = (LUT[9] << 24) + (LUT[8] << 16) + (LUT[7] << 8) + (LUT[6]);
            Data_2 = (LUT[13] << 24) + (LUT[12] << 16) + (LUT[11] << 8) + (LUT[10]);
            Data_3 = (LUT[17] << 24) + (LUT[16] << 16) + (LUT[15] << 8) + (LUT[14]);
            Data_4 = (LUT[21] << 24) + (LUT[20] << 16) + (LUT[19] << 8) + (LUT[18]);
            Data_5 = (LUT[25] << 24) + (LUT[24] << 16) + (LUT[23] << 8) + (LUT[22]);
            Data_6 = (LUT[29] << 24) + (LUT[28] << 16) + (LUT[27] << 8) + (LUT[26]);
            Data_7 = (LUT[33] << 24) + (LUT[32] << 16) + (LUT[31] << 8) + (LUT[30]);
            Data_8 = (LUT[37] << 24) + (LUT[36] << 16) + (LUT[35] << 8) + (LUT[34]);
            Data_9 = (LUT[41] << 24) + (LUT[40] << 16) + (LUT[39] << 8) + (LUT[38]);
            Data_10 = LUT[42];
            //Data_10 = (LUT[45] << 24) + (LUT[44] << 16) + (LUT[43] << 8) + (LUT[42]);
            Data_11 = (LUT[49] << 24) + (LUT[48] << 16) + (LUT[47] << 8) + (LUT[46]);

            Ax = MathF.Round(((float)Data_1 / 1000), 3);
            Ay = MathF.Round(((float)Data_2 / 1000), 3);
            Az = MathF.Round(((float)Data_3 / 1000), 3);
            FOV = MathF.Round(((float)Data_4 / 1000), 3);
            Az_Error = MathF.Round(((float)Data_5 / 1000), 3);
            Ei_Error = MathF.Round(((float)Data_6 / 1000), 3);
            Error_X = MathF.Round(((float)Data_7 / 1000), 3);
            Error_Y = MathF.Round(((float)Data_8 / 1000), 3);
            Error_Z = MathF.Round(((float)Data_9 / 1000), 3);
            NdYag_State = (byte)Data_10;

            ChangeTextBox(ax_TextBox, Ax.ToString());
            ChangeTextBox(ay_TextBox, Ay.ToString());
            ChangeTextBox(az_TextBox, Az.ToString());
            ChangeTextBox(fov_TextBox, FOV.ToString());
            ChangeTextBox(azError_TextBox, Az_Error.ToString());
            ChangeTextBox(eiError_TextBox, Ei_Error.ToString());
        }

        void ChangeTextBox(TextBox textBox, string txt)
        {
            textBox.BeginInvoke((MethodInvoker)delegate ()
            {
                textBox.Text = txt;
            });
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
            }
            if (Length == 2)
            {
                Template[8] = (byte)((Tx_Data[0]) & 0xFF);
                Template[9] = (byte)((Tx_Data[0] >> 8) & 0xFF);
                Template[10] = (byte)((Tx_Data[0] >> 16) & 0xFF);
                Template[11] = (byte)((Tx_Data[0] >> 24) & 0xFF);

                Template[12] = (byte)((Tx_Data[1]) & 0xFF);
                Template[13] = (byte)((Tx_Data[1] >> 8) & 0xFF);
                Template[14] = (byte)((Tx_Data[1] >> 16) & 0xFF);
                Template[15] = (byte)((Tx_Data[1] >> 24) & 0xFF);
            }

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
