using System.IO.Ports;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class SerialController : SerialPacketHandler
    {
        public SerialPort _SerialPort { get; private set; }
        public bool Open { get; private set; } = false;
        public bool Disposed { get; private set; } = false;
        private readonly ComboBox _Com_ComboBox, _Baud_ComboBox, _DataBits_ComboBox;
        private readonly TextBox _SerialMonitoring_TextBox;
        private readonly Label _Serial1_Label, _Serial2_Label;
        const Parity ParityBit = Parity.None;
        const StopBits StopBit = StopBits.One;
        int Baudrate, DataBit;
        string PortNumber;

        byte[] DataBuffer_Rx = new byte[35];

        public SerialController(ComboBox _ComComboBox, ComboBox _BaudComboBox, ComboBox _DataBitsComboBox, TextBox _SerialMonitoringTextBox, Label _Serial1Label, Label _Serial2Label)
        {
            _Com_ComboBox = _ComComboBox;
            _Baud_ComboBox = _BaudComboBox;
            _DataBits_ComboBox = _DataBitsComboBox;
            _SerialMonitoring_TextBox = _SerialMonitoringTextBox;
            _Serial1_Label = _Serial1Label;
            _Serial2_Label = _Serial2Label;
        }
        public void OpenPort()
        {
            _SerialPort.DataReceived += new SerialDataReceivedEventHandler(_SerialPort_DataReceived);
            Open = true;
            _SerialPort.Open();
            if (_SerialPort.IsOpen)
                _Serial1_Label.ForeColor = Color.Green;
            else
                _Serial1_Label.ForeColor = Color.Red;
        }
        public void ClosePort()
        {
            if (Open)
            {
                Open = false;
                Disposed = true;
                _SerialPort.Close();
                _SerialPort.Dispose();
            }
        }
        public void SetSetting_Port()
        {
            if (_Com_ComboBox.SelectedItem != null)
                PortNumber = _Com_ComboBox.SelectedItem.ToString();
            else
                MessageBox.Show("Wrong Port", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (_Baud_ComboBox.SelectedItem != null)
                Baudrate = int.Parse(_Baud_ComboBox.SelectedItem.ToString());
            else
                MessageBox.Show("Wrong Baudrate", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (_DataBits_ComboBox.SelectedItem != null)
                DataBit = int.Parse(_DataBits_ComboBox.SelectedItem.ToString());
            else
                MessageBox.Show("Wrong DataBits", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _SerialPort = new SerialPort(PortNumber, Baudrate, ParityBit, DataBit, StopBit);
        }
        private void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            for (int i = 0; i < 35; i++)
            {
                DataBuffer_Rx[i] = Readbyte();
                _SerialMonitoring_TextBox.AppendText(DataBuffer_Rx[i].ToString());
                _SerialMonitoring_TextBox.AppendText(Environment.NewLine);
                //_SerialMonitoring_TextBox.AppendText(Readbyte().ToString());
            }
            Master_CheckPacket(DataBuffer_Rx, _SerialMonitoring_TextBox);
        }
        public int Read(byte[] buffer, int offset, int count)
        {
            return _SerialPort.Read(buffer, offset, count);
        }
        byte Readbyte()
        {
            return (byte)_SerialPort.ReadByte();
        }

    }
}
