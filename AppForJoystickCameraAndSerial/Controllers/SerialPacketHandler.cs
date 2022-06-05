namespace AppForJoystickCameraAndSerial.Controllers
{
    public class SerialPacketHandler : Form
    {
        protected static void Master_CheckPacket(byte[] Rx_Data, TextBox test)
        {
            if (Rx_Data[0] == 85)
            {
                test.AppendText("HELLO");
                test.AppendText(Environment.NewLine);
            }
        }
    }
}
