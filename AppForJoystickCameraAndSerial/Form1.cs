using AppForJoystickCameraAndSerial.Controllers;

namespace AppForJoystickCameraAndSerial
{
    public partial class Form1 : Form
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly JoysticksController joysticksController;
        private readonly CamerasController camerasController;

        public Form1()
        {
            InitializeComponent();
            Joystick_Lable.ForeColor = Color.Red;
            Camera1_Lable.ForeColor = Color.Red;
            Camera2_Lable.ForeColor = Color.Red;
            cancellationTokenSource = new CancellationTokenSource();
            joysticksController = new JoysticksController(JoystickInfoTxtBox, Joystick_Lable);
            camerasController = new CamerasController(cancellationTokenSource.Token, MainCameraPictureBox, MinorPictureBox, Camera1_Lable, Camera2_Lable);
        }

        private void Exit_Btn_Click(object sender, EventArgs e)
        {
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

        private void SerialPortSetting_Btn_Click(object sender, EventArgs e)
        {
            Form SerialPort_Setting = new SerialPort_Configuration();
            SerialPort_Setting.Show(this);
        }
    }
}