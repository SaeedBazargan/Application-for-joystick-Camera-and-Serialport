using AppForJoystickCameraAndSerial.Controllers;
using System.IO.Ports;

namespace AppForJoystickCameraAndSerial
{
    public partial class Form1 : Form
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly JoysticksController joysticksController;
        private readonly CamerasController camerasController;
        public SerialPort _SerialPort { get; private set; }
        public bool Open { get; private set; } = false;
        public bool Disposed { get; private set; } = false;

        const Parity ParityBit = Parity.None;
        const StopBits StopBit = StopBits.One;
        int Baudrate, DataBit;
        string PortNumber;

        public Form1()
        {
            InitializeComponent();

            Joystick_Label.ForeColor = Color.Red;
            Camera1_Label.ForeColor = Color.Red;
            Camera2_Label.ForeColor = Color.Red;
            cancellationTokenSource = new CancellationTokenSource();
            joysticksController = new JoysticksController(JoystickInfoTxtBox, Joystick_Label);
            camerasController = new CamerasController(cancellationTokenSource.Token, MainCameraPictureBox, MinorPictureBox, Camera1_Label, Camera2_Label);

            Serial1_Lable.ForeColor = Color.Red;
            Serial2_Lable.ForeColor = Color.Red;
        }

        private void Exit_Btn_Click(object sender, EventArgs e)
        {
            Open = false;
            Disposed = true;
            _SerialPort.Close();
            _SerialPort.Dispose();
            cancellationTokenSource.Cancel();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void SearchCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                camerasController.Start();
            else
                camerasController.Stop();
        }
        private void SetSetting_Button_Click(object sender, EventArgs e)
        {
            if (Com_ComboBox.SelectedItem != null)
                PortNumber = Com_ComboBox.SelectedItem.ToString();
            else
                MessageBox.Show("Wrong Port", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (Baud_ComboBox.SelectedItem != null)
                Baudrate = int.Parse(Baud_ComboBox.SelectedItem.ToString());
            else
                MessageBox.Show("Wrong Baudrate", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (DataBits_ComboBox.SelectedItem != null)
                DataBit = int.Parse(DataBits_ComboBox.SelectedItem.ToString());
            else
                MessageBox.Show("Wrong DataBits", "Faild to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _SerialPort = new SerialPort(PortNumber, Baudrate, ParityBit, DataBit, StopBit);
        }
        private void OpenPort_Button_Click(object sender, EventArgs e)
        {
            _SerialPort.DataReceived += new SerialDataReceivedEventHandler(_SerialPort_DataReceived);
            Open = true;
            _SerialPort.Open();
            if (_SerialPort.IsOpen)
                Serial1_Lable.ForeColor = Color.Green;
            else
                Serial1_Lable.ForeColor = Color.Red;
        }
        private void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialMonitoring_TextBox.Text = Readbyte().ToString();
        }
        public int Read(byte[] buffer, int offset, int count)
  	    {
      	    return _SerialPort.Read(buffer, offset, count);
  	    }
        byte Readbyte()
        {
            return (byte)_SerialPort.ReadByte();
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            Form configForm = new ConfigForm();
            configForm.Show(this);
        }
    }
}