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
        const int DefualtBaudRate = 6800;
        const int DefaultSize = 8;
        //int Baudrate, DataBit;
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
            //PortNumber = Com_ComboBox.Text;
            //Baudrate = int.Parse(Baud_ComboBox.Text);
            //DataBit = int.Parse(DataBits_ComboBox.Text);
            //_SerialPort = new SerialPort(PortNumber, Baudrate, ParityBit, DataBit, StopBit);
            _SerialPort = new SerialPort("COM3", DefualtBaudRate, ParityBit, DefaultSize, StopBit);
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

        private void OpenPort_Button_Click(object sender, EventArgs e)
        {
            Open = true;
            _SerialPort.Open();
            if (_SerialPort.IsOpen)
                Serial1_Lable.ForeColor = Color.Green;
            else
                Serial1_Lable.ForeColor = Color.Red;
            SerialMonitoring_TextBox.Text = Readbyte().ToString();
        }
        byte Readbyte()
        {
            return (byte)_SerialPort.ReadByte();
        }

        char ReadChar()
        {
            return (char)_SerialPort.ReadByte();
        }
        string ReadExisting()
        {
            return _SerialPort.ReadExisting();
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            Form configForm = new ConfigForm();
            configForm.Show(this);
        }
    }
}