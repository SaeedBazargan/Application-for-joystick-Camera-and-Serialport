using AppForJoystickCameraAndSerial.Controllers;

namespace AppForJoystickCameraAndSerial
{
    public partial class Form1 : Form
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly JoysticksController joysticksController;
        private readonly CamerasController camerasController;
        private readonly SerialController serialportController;
        public Form1()
        {
            InitializeComponent();

            Joystick_Label.ForeColor = Color.Red;
            Camera1_Label.ForeColor = Color.Red;
            Camera2_Label.ForeColor = Color.Red;
            Serial1_Lable.ForeColor = Color.Red;
            Serial2_Lable.ForeColor = Color.Red;
            cancellationTokenSource = new CancellationTokenSource();
            joysticksController = new JoysticksController(JoystickInfoTxtBox, Joystick_Label);
            camerasController = new CamerasController(cancellationTokenSource.Token, MainCameraPictureBox, MinorPictureBox, Camera1_Label, Camera2_Label);
            serialportController = new SerialController(Com_ComboBox, Baud_ComboBox, DataBits_ComboBox, SerialMonitoring_TextBox, Serial1_Lable, Serial2_Lable);
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

        private void SearchCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                camerasController.Start();
            else
                camerasController.Stop();
        }
        private void SetSetting_Button_Click(object sender, EventArgs e)
        {
            serialportController.SetSetting_Port();
        }
        private void OpenPort_Button_Click(object sender, EventArgs e)
        {
            serialportController.OpenPort();
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            Form configForm = new ConfigForm();
            configForm.Show(this);
        }
    }
}