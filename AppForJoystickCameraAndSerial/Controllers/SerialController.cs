using AppForJoystickCameraAndSerial.Models;
using System.IO.Ports;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class SerialController : ControllerBase
    {
        SerialPacketHandler Handler = new SerialPacketHandler();

        public SerialPort[] _SerialPort { get; private set; }
        public SerialPortSetting[] Settings { get; set; }
        public bool Open { get; private set; } = false;
        public bool Disposed { get; private set; } = false;
        const Parity ParityBit = Parity.None;
        const StopBits StopBit = StopBits.One;
        int Baudrate, DataBit;
        string PortNumber;

        private readonly ComboBox _Com_ComboBox, _Baud_ComboBox, _DataBits_ComboBox;
        private readonly ComboBox _Com_ComboBox2, _Baud_ComboBox2, _DataBits_ComboBox2;
        private readonly TextBox _Fov_TextBox, _AzError_TextBox, _EiError_TextBox, _Ax_TextBox, _Ay_TextBox, _Az_TextBox, _infoTxtBox;
        private readonly Button _openPortBtn;
        private readonly PictureBox _Serial1Status, _Serial2Status;
        private readonly CheckBox _selectSerial1, _selectSerial2, _recordSerial1, _recordSerial2;
        private readonly CheckBox _tvCameraCheckBox;
        
        private readonly CancellationToken _cancellationToken;
        private readonly Task[] serialPortTasks;
        private readonly bool[] isRunning;
        private readonly bool[] recording;
        public string RecordingDirectory { get; set; }

        int DataInBuffer_Size = 200;
        byte[] Data_Rx;
        int Data_Counter = 0;
        int serialportIndex = 0;

        public SerialController(CancellationToken cancellationToken, TextBox infoTxtBox, PictureBox serial1Status, PictureBox serial2Status, Button openPortBtn, Button readyNdYagBtn, CheckBox SelectSerial1, CheckBox SelectSerial2,
            CheckBox recordSerial1, CheckBox recordSerial2, CheckBox tvCamera, TextBox fov_TextBox, TextBox azError_TextBox, TextBox eiError_TextBox, TextBox ax_TextBox, TextBox ay_TextBox, TextBox az_TextBox)
        {
            _SerialPort = new SerialPort[2];
            Settings = new SerialPortSetting[2]
            {
                new SerialPortSetting{PortNumber = "COM7",Baudrate = 115200, DataBit = 8},
                new SerialPortSetting{PortNumber = "COM5",Baudrate = 115200, DataBit = 8}
            };

            _Serial1Status = serial1Status; _Serial2Status = serial2Status;
            _openPortBtn = openPortBtn;
            _selectSerial1 = SelectSerial1; _recordSerial1 = recordSerial1;
            _selectSerial2 = SelectSerial2; _recordSerial2 = recordSerial2;
            _tvCameraCheckBox = tvCamera;

            _cancellationToken = cancellationToken;
            _infoTxtBox = infoTxtBox;
            serialPortTasks = new Task[2];
            isRunning = new bool[2];
            recording = new bool[2];
            _Fov_TextBox = fov_TextBox;
            _AzError_TextBox = azError_TextBox;
            _EiError_TextBox = eiError_TextBox;
            _Ax_TextBox = ax_TextBox;
            _Ay_TextBox = ay_TextBox;
            _Az_TextBox = az_TextBox;

            Data_Rx = new byte[DataInBuffer_Size];
        }
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
            Open = false;
            _openPortBtn.BeginInvoke((MethodInvoker)delegate ()
            {
                _openPortBtn.Enabled = true;
            });
            ChangePictureBox(SerialIndex == 0 ? _Serial1Status : _Serial2Status, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
            if (SerialIndex == 0)
            {
                _selectSerial1.BeginInvoke((MethodInvoker)delegate () { _selectSerial1.Checked = false; });
                _recordSerial1.BeginInvoke((MethodInvoker)delegate () { _recordSerial1.Checked = false; });
            }
            else if (SerialIndex == 1)
            {
                _selectSerial2.BeginInvoke((MethodInvoker)delegate () { _selectSerial2.Checked = false; });
                _recordSerial2.BeginInvoke((MethodInvoker)delegate () { _recordSerial2.Checked = false; });
            }
            _SerialPort[SerialIndex].Close();
            //_tvCameraCheckBox.BeginInvoke((MethodInvoker)delegate () { _tvCameraCheckBox.Checked = false; });
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

        public void SetSetting_Port(int SerialIndex)
        {
            byte setOk = 1;
            if (SerialIndex == 0)
            {
                if (_Com_ComboBox.SelectedItem != null)
                    PortNumber = _Com_ComboBox.SelectedItem.ToString();
                else if (_Baud_ComboBox.SelectedItem != null)
                    Baudrate = int.Parse(_Baud_ComboBox.SelectedItem.ToString());
                else if (_DataBits_ComboBox.SelectedItem != null)
                    DataBit = int.Parse(_DataBits_ComboBox.SelectedItem.ToString());
                else
                {
                    setOk = 0;
                    MessageBox.Show("Please fill in the fields.", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _SerialPort[0] = new SerialPort(PortNumber, Baudrate, ParityBit, DataBit, StopBit);
            }
            if (SerialIndex == 1)
            {
                if (_Com_ComboBox2.SelectedItem != null)
                    PortNumber = _Com_ComboBox2.SelectedItem.ToString();
                else if (_Baud_ComboBox2.SelectedItem != null)
                    Baudrate = int.Parse(_Baud_ComboBox2.SelectedItem.ToString());
                else if (_DataBits_ComboBox2.SelectedItem != null)
                    DataBit = int.Parse(_DataBits_ComboBox2.SelectedItem.ToString());
                else
                {
                    setOk = 0;
                    MessageBox.Show("Please fill in the fields.", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _SerialPort[1] = new SerialPort(PortNumber, Baudrate, ParityBit, DataBit, StopBit);
            }
            if (setOk == 1)
                MessageBox.Show("Serial settings saved!");
        }
        private void StartSerial(int index)
        {
            try
            {
                Open = true;
                serialportIndex = index;
                var setting = Settings[index];
                _SerialPort[index] = new SerialPort(setting.PortNumber, setting.Baudrate, ParityBit, setting.DataBit, StopBit);
                _SerialPort[index].Open();
                _openPortBtn.BeginInvoke((MethodInvoker)delegate ()
                {
                    _openPortBtn.Enabled = false;
                });
                if (index == 0)
                {
                    _recordSerial1.BeginInvoke((MethodInvoker)delegate () { _recordSerial1.Checked = true; });
                    ChangePictureBox(_Serial1Status, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
                }
                else if (index == 1)
                {
                    _recordSerial2.BeginInvoke((MethodInvoker)delegate () { _recordSerial2.Checked = true; });
                    ChangePictureBox(_Serial2Status, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
                }

                ChangeTextBox(_infoTxtBox, $"Port {index + 1} is Connected!");
                //_tvCameraCheckBox.Checked = true;
            }
            catch(Exception e)
            {
                ChangeTextBox(_infoTxtBox, $"Port {index + 1} is not found!");
                Stop(index);
            }

            while (isRunning[index])
            {
                try
                {
                    Data_Rx[Data_Counter] = (byte)_SerialPort[index].ReadByte();
                    Data_Counter = (Data_Counter + 1) % DataInBuffer_Size;
                    if (Data_Rx[0] == 85 && Data_Counter == 55)
                    {
                        Handler.Master_CheckPacket(Data_Rx, RecordingDirectory, recording[index], index, _Fov_TextBox, _AzError_TextBox, _EiError_TextBox, _Ax_TextBox, _Ay_TextBox, _Az_TextBox);
                        Data_Counter = 0;
                    }
                    else if (Data_Rx[0] != 85)
                    {
                        Array.Clear(Data_Rx, 0, 55);
                        Data_Counter = 0;
                    }
                }
                catch(Exception e)
                {
                    Stop(index);
                    ChangeTextBox(_infoTxtBox, $"Port {index + 1} is disconnected!");
                }
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
            for (byte i = 0; i < 55; i++)
            {
                Console.Write(i + ":      ");
                Console.WriteLine(Data[i]);
            }

            if (Open)
            {
                if (serialportIndex == 0)
                    _SerialPort[0].Write(Data, 0, 55);
                else
                    _SerialPort[1].Write(Data, 0, 55);
            }
            else
                ChangeTextBox(_infoTxtBox, "SerialPort is disconnected!");
        }
    }
}