using AppForJoystickCameraAndSerial.Controllers;

namespace AppForJoystickCameraAndSerial
{
    public partial class Form1 : Form
    {
        enum WriteAddresses : byte
        {
            Relay = 1,
            CAN = 8
        }

        enum WriteCodes : byte
        {
            Motor_1         = 1,
            Motor_2         = 2,
            Motor_3         = 3,
            All_Motors      = 4,

            Enable_Motors   = 6,
            Disable_Motors  = 7,
            Reset_Alarm     = 8
        }
        byte ON     = 1;
        byte OFF    = 0;
        bool Enable_Flag = false;

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
            serialportController = new SerialController(cancellationTokenSource.Token, Com_ComboBox, Com_ComboBox2, Baud_ComboBox, Baud_ComboBox2, DataBits_ComboBox, DataBits_ComboBox2, SerialMonitoring_TextBox, Serial1Status_pictureBox, Serial1Status_pictureBox, OpenPort_Button);
        }

        private void Exit_Btn_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void SetSetting_Button_Click(object sender, EventArgs e)
        {
            if (SelectSerial1_CheckBox.Checked)
                serialportController.SetSetting_Port(0);
            if (SelectSerial2_CheckBox.Checked)
                serialportController.SetSetting_Port(1);
            else
                MessageBox.Show("No port selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void OpenPort_Button_Click(object sender, EventArgs e)
        {
            if (SelectSerial1_CheckBox.Checked)
                serialportController.Start(0);
            else if (SelectSerial2_CheckBox.Checked)
                serialportController.Start(1);
            else
                MessageBox.Show("No port selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ClosePort_Button_Click(object sender, EventArgs e)
        {
            serialportController.Stop(0);
            serialportController.Stop(1);
        }
        private void RecordSerial_1CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Record(0);
            else
                serialportController.StopRecord(0);
        }
        private void RecordSerial_2CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Record(1);
            else
                serialportController.StopRecord(1);
        }
        private void SerialLogBrowse_Button_Click(object sender, EventArgs e)
        {
            string dir;
            if (SelectSerial1_CheckBox.Checked || SelectSerial2_CheckBox.Checked)
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.ShowNewFolderButton = true;
                DialogResult result = folderBrowser.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dir = folderBrowser.SelectedPath;
                    dir = dir.Replace(@"\", @"/");
                    SerialLogDirectory_TextBox.Text = dir + '/';
                    serialportController.RecordDirectory(dir + '/');
                    Environment.SpecialFolder root = folderBrowser.RootFolder;
                }
            }
            else
                MessageBox.Show("You can't record! First select one of the serials please", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void CameraLogBrowse_Button_Click(object sender, EventArgs e)
        {
            string dir;
            if (Camera1CheckBox.Checked || Camera2CheckBox.Checked)
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.ShowNewFolderButton = true;
                DialogResult result = folderBrowser.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dir = folderBrowser.SelectedPath;
                    dir = dir.Replace(@"\" , @"/");
                    CameraLogDirectory_TextBox.Text = dir + '/';
                    camerasController.RecordDirectory(dir + '/');
                    Environment.SpecialFolder root = folderBrowser.RootFolder;
                }
            }
            else
                MessageBox.Show("You can't record! First select one of the cameras please", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void Joystick_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                joysticksController.Start();
            //else
            //    serialportController.Write((byte)WriteCodes.All_Motors, 0);
        }
        private void AllMotorsCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteCodes.All_Motors, (byte)WriteAddresses.Relay, ON);
                Enable_Flag = true;
            }
            else
            {
                serialportController.Write((byte)WriteCodes.All_Motors, (byte)WriteAddresses.Relay, OFF);
                Motor1_CheckBox.Checked         = false;
                Motor2_CheckBox.Checked         = false;
                Motor3_CheckBox.Checked         = false;
                EnableMotors_CheckBox.Checked   = false;
            }
        }

        private void Motor1_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteCodes.Motor_1, (byte)WriteAddresses.Relay, ON);
                Enable_Flag = true;
            }
            else
                serialportController.Write((byte)WriteCodes.Motor_1, (byte)WriteAddresses.Relay, OFF);
        }

        private void Motor2_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteCodes.Motor_2, (byte)WriteAddresses.Relay, ON);
                Enable_Flag = true;
            }
            else
                serialportController.Write((byte)WriteCodes.Motor_2, (byte)WriteAddresses.Relay, OFF);
        }

        private void Motor3_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteCodes.Motor_3, (byte)WriteAddresses.Relay, ON);
                Enable_Flag = true;
            }
            else
                serialportController.Write((byte)WriteCodes.Motor_3, (byte)WriteAddresses.Relay, OFF);
        }

        private void EnableMotors_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                if (Enable_Flag)
                {
                    EnableMotors_CheckBox.Checked = false;
                    MessageBox.Show($"You must be wait for 3seconds please", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Thread.Sleep(3000);
                    serialportController.Write((byte)WriteCodes.Enable_Motors, (byte)WriteAddresses.CAN, OFF);
                    EnableMotors_CheckBox.Text = "Disable_Motors";
                    Enable_Flag = false;
                    EnableMotors_CheckBox.Checked = true;
                }
                else
                {
                    serialportController.Write((byte)WriteCodes.Enable_Motors, (byte)WriteAddresses.CAN, OFF);
                    EnableMotors_CheckBox.Text = "Disable_Motors";
                }
            }
            else
            {
                serialportController.Write((byte)WriteCodes.Disable_Motors, (byte)WriteAddresses.CAN, OFF);
                EnableMotors_CheckBox.Text = "Enable_Motors";
            }
        }

        private void ResetAlarm_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Write((byte)WriteCodes.Reset_Alarm, (byte)WriteAddresses.CAN, OFF);
            Thread.Sleep(3000);
            ResetAlarm_CheckBox.Checked = false;
        }
    }
}