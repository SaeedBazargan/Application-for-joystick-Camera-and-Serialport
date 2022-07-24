using System.IO.Ports;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class SerialController : ControllerBase
    {
        SerialPacketHandler Handler = new SerialPacketHandler();

        public SerialPort[] _SerialPort { get; private set; }
        public bool Open { get; private set; } = false;
        public bool Disposed { get; private set; } = false;
        const Parity ParityBit = Parity.None;
        const StopBits StopBit = StopBits.One;
        int Baudrate, DataBit;
        string PortNumber;

        private readonly TextBox _SerialMonitoring_TextBox;
        private readonly PictureBox _Serial1Status, _Serial2Status;
        private readonly Button _openPortBtn;

        private readonly CancellationToken _cancellationToken;
        private readonly Task[] serialPortTasks;
        private readonly bool[] isRunning;
        private readonly bool[] recording;

        const byte MaxDataBuffer_Rx_Size = 55;
        byte[] DataBuffer_Rx = new byte[MaxDataBuffer_Rx_Size];
        public string RecordingDirectory { get; set; }

        public SerialController(CancellationToken cancellationToken, TextBox _SerialMonitoringTextBox, PictureBox serial1Status, PictureBox serial2Status, Button openPortBtn)
        {
            _SerialPort = new SerialPort[2];

            _SerialMonitoring_TextBox = _SerialMonitoringTextBox;
            _Serial1Status = serial1Status; _Serial2Status = serial2Status;
            _openPortBtn = openPortBtn;

            _cancellationToken = cancellationToken;
            serialPortTasks = new Task[2];
            isRunning = new bool[2];
            recording = new bool[2];
        }
        public SerialController()
        { }
        public void Start(int SerialIndex)
        {
            if (0 <= SerialIndex || SerialIndex <= 2)
            {
                isRunning[SerialIndex] = true;
                serialPortTasks[SerialIndex] = Task.Factory.StartNew(() => StartSerial(SerialIndex), _cancellationToken).ContinueWith((t) => SerialTaskDone(t, SerialIndex == 0));
            }
            else
                throw new ArgumentOutOfRangeException();
        }
        public void Stop(int SerialIndex)
        {
            isRunning[SerialIndex] = false;
            _openPortBtn.BeginInvoke((MethodInvoker)delegate ()
            {
                _openPortBtn.Enabled = true;
            });
        }

        public void Record(int SerialIndex)
        {
            recording[SerialIndex] = true;
        }
        public void StopRecord(int SerialIndex)
        {
            recording[SerialIndex] = false;
        }
        public void RecordDirectory(string Directory)
        {
            RecordingDirectory = Directory;
        }
        public void SetConfiguration_Port(string Com, int Baud, int data)
        {
            PortNumber = Com;
            Baudrate = Baud;
            DataBit = data;
            Console.WriteLine("AAAA " + PortNumber);
            Console.WriteLine("BBBB " + Baudrate);
            Console.WriteLine("CCCC " + DataBit);
        }
        public void SetSetting_Port(int SerialIndex)
        {
            if (SerialIndex == 0)
            {
                Console.WriteLine("AAAA " + PortNumber);
                Console.WriteLine("BBBB " + Baudrate);
                Console.WriteLine("CCCC " + DataBit);

                //_SerialPort[0] = new SerialPort(PortNumber, Baudrate, ParityBit, DataBit, StopBit);
            }
            if (SerialIndex == 1)
                _SerialPort[1] = new SerialPort(PortNumber, Baudrate, ParityBit, DataBit, StopBit);
        }
        private void StartSerial(int index)
        {
            Open = true;
            _SerialPort[index].Open();
            _openPortBtn.BeginInvoke((MethodInvoker)delegate ()
            {
                _openPortBtn.Enabled = false;
            });
            if (!_SerialPort[index].IsOpen)
                throw new Exception($"Cannot open camera {index}");
            ChangePictureBox(index == 0 ? _Serial1Status : _Serial2Status, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
            while(isRunning[index])
            {
                for (int i = 0; i < 55; i++)
                {
                    DataBuffer_Rx[i] = (byte)_SerialPort[index].ReadByte();
                    //ChangeTextBox(_SerialMonitoring_TextBox, _SerialMonitoring_TextBox.Text + DataBuffer_Rx[i].ToString());
                }
                Handler.Master_CheckPacket(DataBuffer_Rx , RecordingDirectory, recording[index], index);
            }
        }
        private void SerialTaskDone(Task task, bool isMain)
        {
            if (task.IsCompletedSuccessfully)
                ChangePictureBox(isMain == true ? _Serial1Status : _Serial2Status, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
            else
                isRunning[Convert.ToInt32(isMain)] = false;
        }
        public void Write(byte Code, byte Address, Int32[] Value, byte Length)
        {
            byte[] Data = new byte[55];
            Handler.WriteMessage_Generator(Code, Address, Value, Length, Data);
            //for (byte i = 0; i < 55; i++)
            //{
            //    Console.Write(i + ":      ");
            //    Console.WriteLine(Data[i]);
            //}

            if (Open)
                _SerialPort[0].Write(Data, 0, 55);
            else
                MessageBox.Show("SerialPort is not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

