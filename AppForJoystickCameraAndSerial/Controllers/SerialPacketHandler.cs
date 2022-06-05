namespace AppForJoystickCameraAndSerial.Controllers
{
    public class SerialPacketHandler
    {
        protected static void Master_CheckPacket(byte[] Rx_Data)
        {
            if (Rx_Data[0] == 85)
                Console.WriteLine("dfghjl");
        }
    }
}
