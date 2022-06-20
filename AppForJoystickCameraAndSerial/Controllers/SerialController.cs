using System.IO.Ports;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class SerialSetting
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public static Parity Parity => Parity.None;
        public static StopBits StopBits => StopBits.One;

        public SerialSetting(string portName, int baudRate, int dataBits)
        {
            PortName = portName;
            BaudRate = baudRate;
            DataBits = dataBits;
        }
    }

    class Serial
    {
        public SerialSetting Setting { get; set; }
        public SerialPort Port { get; set; }
        public bool IsRunnig { get; set; }
        public bool Recording { get; set; }
    }


    public class SerialController : ControllerBase
    {
        private const int buffer_rx_size = 55;

        private readonly SerialPort _serialPort;
        private readonly Task[] serialPortTasks;
        private readonly CancellationToken _cancellationToken;
        private readonly ComboBox _Com_ComboBox, _Baud_ComboBox, _DataBits_ComboBox;
        private readonly ComboBox _Com_ComboBox2, _Baud_ComboBox2, _DataBits_ComboBox2;
        private readonly TextBox _SerialMonitoring_TextBox;
        private readonly PictureBox _Serial1Status, _Serial2Status;
        private readonly bool[] recording;
        private readonly bool[] isRunning;
        private readonly byte[] dataBuffer_Rx;
        private readonly SerialSetting[] settings;

        public enum SerialName
        {
            Gun = 0,
            Radar
        }

        public SerialController(CancellationToken cancellationToken, ComboBox _ComComboBox, ComboBox _ComComboBox2, ComboBox _BaudComboBox, ComboBox _BaudComboBox2, ComboBox _DataBitsComboBox, ComboBox _DataBitsComboBox2, TextBox _SerialMonitoringTextBox, PictureBox serial1Status, PictureBox serial2Status)
        {
            serialPortTasks = new Task[2];
            isRunning = new bool[2];
            recording = new bool[2];
            _Com_ComboBox = _ComComboBox;
            _Baud_ComboBox = _BaudComboBox;
            _DataBits_ComboBox = _DataBitsComboBox;
            _Com_ComboBox2 = _ComComboBox2;
            _Baud_ComboBox2 = _BaudComboBox2;
            _DataBits_ComboBox2 = _DataBitsComboBox2;
            _SerialMonitoring_TextBox = _SerialMonitoringTextBox;
            _Serial1Status = serial1Status;
            _Serial2Status = serial2Status;
            dataBuffer_Rx = new byte[buffer_rx_size];
            settings = new SerialSetting[2];
        }

        public void OpenPort()
        {
            _SerialPort.DataReceived += new SerialDataReceivedEventHandler(_SerialPort_DataReceived);
            Open = true;
            _SerialPort.Open();
            if (_SerialPort.IsOpen)
                ChangePictureBox(_Serial1Status, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
            else
                ChangePictureBox(_Serial1Status, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
        }
        public void ClosePort()
        {
            if (Open)
            {
                ChangePictureBox(_Serial1Status, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                Open = false;
                Disposed = true;
                _SerialPort.Close();
                _SerialPort.Dispose();
            }
        }
        public void SetSetting(SerialName serial,SerialSetting setting)
        {
            settings[(int)serial] = setting;
            //if (0 <= SerialIndex || SerialIndex <= 2)
            //{
            //    isRunning[SerialIndex] = true;
            //    SerialPortTasks[SerialIndex] = Task.Factory.StartNew(() => StartSerial(SerialIndex), _cancellationToken).ContinueWith((t) => SerialTaskDone(t, SerialIndex == 0));
            //}
            //if (_Com_ComboBox.SelectedItem != null)
            //    PortNumber = _Com_ComboBox.SelectedItem.ToString();
            //else
            //    MessageBox.Show("Wrong Port", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //if (_Baud_ComboBox.SelectedItem != null)
            //    Baudrate = int.Parse(_Baud_ComboBox.SelectedItem.ToString());
            //else
            //    MessageBox.Show("Wrong Baudrate", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //if (_DataBits_ComboBox.SelectedItem != null)
            //    DataBit = int.Parse(_DataBits_ComboBox.SelectedItem.ToString());
            //else
            //    MessageBox.Show("Wrong DataBits", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //_SerialPort = new SerialPort(PortNumber, Baudrate, ParityBit, DataBit, StopBit);
            //MessageBox.Show("Serial settings saved!");
        }
        private void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            for (int i = 0; i < 55; i++)
            {
                DataBuffer_Rx[i] = Readbyte();
                ChangeTextBox(_SerialMonitoring_TextBox, _SerialMonitoring_TextBox.Text + DataBuffer_Rx[i].ToString());
            }
            SerialPacketHandler.Master_CheckPacket(DataBuffer_Rx);
        }
        public void Write(byte Code, byte Value)
        {
            byte[] Data = new byte[55];
            SerialPacketHandler.WriteMessage(Code, Value, Data);
            //for (byte i = 0; i < 55; i++)
            //{
            //    Console.Write(i + ":      ");
            //    Console.WriteLine(Data[i]);
            //}

            if (Open)
                _SerialPort.Write(Data, 0, 55);
            else
                MessageBox.Show("SerialPort is not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        byte Readbyte()
        {
            return (byte)_SerialPort.ReadByte();
        }

    }
}
