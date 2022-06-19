using AppForJoystickCameraAndSerial.Controllers;

namespace AppForJoystickCameraAndSerial
{
    public partial class Form1 : Form
    {
        enum WriteCodes : byte
        {
            Motor_1     = 1,
            Motor_2     = 2,
            Motor_3     = 3,
            All_Motors  = 4
        }
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly JoysticksController joysticksController;
        private readonly CamerasController camerasController;
        private readonly SerialController serialportController;

        public Form1()
        {
            InitializeComponent();

            cancellationTokenSource = new CancellationTokenSource();
            joysticksController = new JoysticksController(JoystickInfoTxtBox, Joystick_Label, JoystickStatus_pictureBox);
            camerasController = new CamerasController(cancellationTokenSource.Token, MainCameraPictureBox, MinorPictureBox, Camera1Status_pictureBox, Camera2Status_pictureBox, CameraExceptionCallBack);
            serialportController = new SerialController(Com_ComboBox, Baud_ComboBox, DataBits_ComboBox, SerialMonitoring_TextBox, Serial1Status_pictureBox, Serial1Status_pictureBox);
        }

        private void Exit_Btn_Click(object sender, EventArgs e)
        {
            serialportController.ClosePort();
            cancellationTokenSource.Cancel();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void SetSetting_Button_Click(object sender, EventArgs e)
        {
            serialportController.SetSetting_Port();
        }
        private void OpenPort_Button_Click(object sender, EventArgs e)
        {
            serialportController.OpenPort();
        }
        private void ClosePort_Button_Click(object sender, EventArgs e)
        {
            serialportController.ClosePort();
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            Form LoginconfigForm = new LoginConfig_Form();
            LoginconfigForm.Show(this);
        }

        private void Camera1CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                camerasController.Start(0);
            else
                camerasController.Stop(0);
        }

        private void Camera2CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                camerasController.Start(1);
            else
                camerasController.Stop(1);
        }

        private void CameraExceptionCallBack(string message)
        {
            BeginInvoke(() => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        private void RecordCamera1_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                camerasController.Record(0);
            else
                camerasController.StopRecord(0);
        }

        private void RecordCamera2_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                camerasController.Record(1);
            else
                camerasController.StopRecord(1);
        }

        private void AllMotorsCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Write((byte)WriteCodes.All_Motors, 1);
            else
                serialportController.Write((byte)WriteCodes.All_Motors, 0);
        }
    }
}