using AppForJoystickCameraAndSerial.Controllers;

namespace AppForJoystickCameraAndSerial
{
    public partial class Form1 : Form
    {
        enum Joystick : byte
        {
            Xbox = 0,
            USB = 1,
            Attack = 2
        }

        enum WriteAddresses : byte
        {
            Relay = 1,
            CAN = 8,
            TableControl = 9,
            ProcessingPlatform = 11
        }

        enum WriteMotorCodes : byte
        {
            Motor_1 = 1,
            Motor_2 = 2,
            Motor_3 = 3,
            All_Motors = 4,

            Enable_Motors = 6,
            Disable_Motors = 7,
            Reset_Alarm = 8
        }

        enum WriteTableCodes : byte
        {
            Home = 4,
            Search = 5,
            Track = 6,
            Cancle = 7,
            Position = 8,
            Axis_3D = 9
        }

        enum WriteProPlatformCodes : byte
        {
            Polarity = 4,
            GateSize = 5,
            SelectCamera = 6,
            RelayOnBoard = 7,
            AutoWide = 8,
            Track_3D = 9
        }

        int[] ON = new int[1] { 1 };
        int[] OFF = new int[1] { 0 };
        int[] Search_zeroPos = new int[2] { 320, 240 };
        private bool PP_GateSize_NegButton_WasClicked = false;
        private bool PP_GateSize_PosButton_WasClicked = false;
        bool Enable_Flag = false;

        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly JoysticksController joysticksController;
        private readonly CamerasController camerasController;
        private readonly SerialController serialportController;
        private readonly MouseController mouseController;

        public Form1()
        {
            InitializeComponent();

            cancellationTokenSource = new CancellationTokenSource();
            camerasController = new CamerasController(cancellationTokenSource.Token, MainCameraPictureBox, MinorPictureBox, Camera1Status_pictureBox, Camera2Status_pictureBox, RotateImage_CheckBox, CameraExceptionCallBack);
            serialportController = new SerialController(cancellationTokenSource.Token, Serial1Status_pictureBox, Serial1Status_pictureBox, OpenPort_Button);
            joysticksController = new JoysticksController(cancellationTokenSource.Token, JoystickInfoTxtBox, Joystick_Label, XboxJoystickStatus_pictureBox, USBJoystickStatus_pictureBox, MainCameraPictureBox, SearchRadio, serialportController, CameraExceptionCallBack);
            mouseController = new MouseController(MainCameraPictureBox, SearchRadio, serialportController);

            TrackRadio.Enabled = false;
            SearchRadio.Enabled = false;
            PositionRadio.Enabled = false;
            HomingRadio.Enabled = false;
            CancleRadio.Enabled = false;

            TvCameraCheckBox.Enabled = false;
            IrCameraCheckBox.Enabled = false;
            RecordTvCamera_CheckBox.Enabled = false;
            RecordIrCamera_CheckBox.Enabled = false;
            WideCameraButton.Enabled = false;
            TeleButton.Enabled = false;
            NearButton.Enabled = false;
            FarButton.Enabled = false;
            RotateImage_CheckBox.Enabled = false;

            PP_AutoWide_CheckBox.Enabled = false;
            PP_3dTrack_CheckBox.Enabled = false;
            PP_GateSize_NegButton.Enabled = false;
            PP_GateSize_PosButton.Enabled = false;
            PP_RelayOnBoard_CheckBox.Enabled = false;
            NegPolarity_RadioButton.Enabled = false;
            PosPolarity_RadioButton.Enabled = false;

            AllMotorsCheckBox.Enabled = false;
            Motor1_CheckBox.Enabled = false;
            Motor2_CheckBox.Enabled = false;
            Motor3_CheckBox.Enabled = false;
            EnableMotors_CheckBox.Enabled = false;
            ResetAlarm_CheckBox.Enabled = false;

            RelayOnScan_NdYagCheckBox.Enabled = false;
            HomingScan_NYagCheckBox.Enabled = false;
            NdYagFreq_TextBox.Enabled = false;
            NdYagReady_Button.Enabled = false;

            RecordSerial_1CheckBox.Enabled = false;
            RecordSerial_2CheckBox.Enabled = false;
        }

        private void Exit_Btn_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            joysticksController.drawIntoImage();
        }
        /// <summary>
        /// Configuration Form Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigButton_Click(object sender, EventArgs e)
        {
            LoginConfig_Form LoginconfigForm = new LoginConfig_Form();
            LoginconfigForm.ShowDialog(this);
            if (LoginconfigForm.LoggedIn)
            {
                ConfigForm ConfigForm = new ConfigForm(serialportController.Settings);
                ConfigForm.ShowDialog(this);
                serialportController.Settings = ConfigForm.Settings;
            }
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
        /// <summary>
        /// Serial Port Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenPort_Button_Click(object sender, EventArgs e)
        {
            if (SelectSerial1_CheckBox.Checked)
            {
                TrackRadio.Enabled = true;
                SearchRadio.Enabled = true;
                PositionRadio.Enabled = true;
                HomingRadio.Enabled = true;
                CancleRadio.Enabled = true;
                RotateImage_CheckBox.Enabled = true;
                TvCameraCheckBox.Enabled = true;
                IrCameraCheckBox.Enabled = true;
                PP_RelayOnBoard_CheckBox.Enabled = true;
                AllMotorsCheckBox.Enabled = true;
                Motor1_CheckBox.Enabled = true;
                Motor2_CheckBox.Enabled = true;
                Motor3_CheckBox.Enabled = true;
                EnableMotors_CheckBox.Enabled = true;
                ResetAlarm_CheckBox.Enabled = true;
                RelayOnScan_NdYagCheckBox.Enabled = true;
                RecordSerial_1CheckBox.Enabled = true;
                RecordSerial_2CheckBox.Enabled = true;

                serialportController.Start(0);
            }
            else if (SelectSerial2_CheckBox.Checked)
            {
                TrackRadio.Enabled = true;
                SearchRadio.Enabled = true;
                PositionRadio.Enabled = true;
                HomingRadio.Enabled = true;
                CancleRadio.Enabled = true;
                RotateImage_CheckBox.Enabled = true;
                TvCameraCheckBox.Enabled = true;
                IrCameraCheckBox.Enabled = true;
                PP_RelayOnBoard_CheckBox.Enabled = true;
                AllMotorsCheckBox.Enabled = true;
                Motor1_CheckBox.Enabled = true;
                Motor2_CheckBox.Enabled = true;
                Motor3_CheckBox.Enabled = true;
                EnableMotors_CheckBox.Enabled = true;
                ResetAlarm_CheckBox.Enabled = true;
                RelayOnScan_NdYagCheckBox.Enabled = true;
                RecordSerial_1CheckBox.Enabled = true;
                RecordSerial_2CheckBox.Enabled = true;

                serialportController.Start(1);
            }
            else
            {
                TrackRadio.Enabled = false;
                SearchRadio.Enabled = false;
                PositionRadio.Enabled = false;
                HomingRadio.Enabled = false;
                CancleRadio.Enabled = false;
                RotateImage_CheckBox.Enabled = false;
                TvCameraCheckBox.Enabled = false;
                IrCameraCheckBox.Enabled = false;
                PP_RelayOnBoard_CheckBox.Enabled = false;
                AllMotorsCheckBox.Enabled = false;
                Motor1_CheckBox.Enabled = false;
                Motor2_CheckBox.Enabled = false;
                Motor3_CheckBox.Enabled = false;
                EnableMotors_CheckBox.Enabled = false;
                ResetAlarm_CheckBox.Enabled = false;
                RelayOnScan_NdYagCheckBox.Enabled = false;
                RecordSerial_1CheckBox.Enabled = false;
                RecordSerial_2CheckBox.Enabled = false;

                MessageBox.Show("No port selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        /// <summary>
        /// Camera Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvCameraCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                RecordTvCamera_CheckBox.Enabled = true;
                RecordIrCamera_CheckBox.Enabled = true;
                WideCameraButton.Enabled = true;
                TeleButton.Enabled = true;
                NearButton.Enabled = true;
                FarButton.Enabled = true;

                serialportController.Write((byte)WriteProPlatformCodes.SelectCamera, (byte)WriteAddresses.ProcessingPlatform, OFF, 1);
                camerasController.Start(0);
            }
            else
            {
                RecordTvCamera_CheckBox.Enabled = false;
                RecordIrCamera_CheckBox.Enabled = false;
                WideCameraButton.Enabled = false;
                TeleButton.Enabled = false;
                NearButton.Enabled = false;
                FarButton.Enabled = false;

                camerasController.Stop(0);
            }
        }

        private void IrCameraCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                RecordTvCamera_CheckBox.Enabled = true;
                RecordIrCamera_CheckBox.Enabled = true;
                WideCameraButton.Enabled = true;
                TeleButton.Enabled = true;
                NearButton.Enabled = true;
                FarButton.Enabled = true;

                serialportController.Write((byte)WriteProPlatformCodes.SelectCamera, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
                camerasController.Start(1);
            }
            else
            {
                RecordTvCamera_CheckBox.Enabled = false;
                RecordIrCamera_CheckBox.Enabled = false;
                WideCameraButton.Enabled = false;
                TeleButton.Enabled = false;
                NearButton.Enabled = false;
                FarButton.Enabled = false;

                camerasController.Stop(1);
            }
        }

        private void CameraExceptionCallBack(string message)
        {
            BeginInvoke(() => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        private void RecordTvCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                camerasController.Record(0);
            else
                camerasController.StopRecord(0);
        }

        private void RecordIrCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                camerasController.Record(1);
            else
                camerasController.StopRecord(1);
        }
        private void CameraLogBrowse_Button_Click(object sender, EventArgs e)
        {
            string dir;
            if (TvCameraCheckBox.Checked || IrCameraCheckBox.Checked)
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.ShowNewFolderButton = true;
                DialogResult result = folderBrowser.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dir = folderBrowser.SelectedPath;
                    dir = dir.Replace(@"\", @"/");
                    CameraLogDirectory_TextBox.Text = dir + '/';
                    camerasController.RecordDirectory(dir + '/');
                    Environment.SpecialFolder root = folderBrowser.RootFolder;
                }
            }
            else
                MessageBox.Show("You can't record! First select one of the cameras please", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Processing Platform Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PosPolarity_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteProPlatformCodes.Polarity, (byte)WriteAddresses.ProcessingPlatform, OFF, 1);
        }

        private void NegPolarity_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteProPlatformCodes.Polarity, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
        }

        private void PP_RelayOnBoard_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                PP_AutoWide_CheckBox.Enabled = true;
                PP_3dTrack_CheckBox.Enabled = true;
                PP_GateSize_NegButton.Enabled = true;
                PP_GateSize_PosButton.Enabled = true;
                NegPolarity_RadioButton.Enabled = true;
                PosPolarity_RadioButton.Enabled = true;

                serialportController.Write((byte)WriteProPlatformCodes.RelayOnBoard, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
            }
            else
            {
                PP_AutoWide_CheckBox.Enabled = false;
                PP_3dTrack_CheckBox.Enabled = false;
                PP_GateSize_NegButton.Enabled = false;
                PP_GateSize_PosButton.Enabled = false;
                NegPolarity_RadioButton.Enabled = false;
                PosPolarity_RadioButton.Enabled = false;

                serialportController.Write((byte)WriteProPlatformCodes.RelayOnBoard, (byte)WriteAddresses.ProcessingPlatform, OFF, 1);
            }
        }

        private void PP_AutoWide_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Write((byte)WriteProPlatformCodes.AutoWide, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
            else
                serialportController.Write((byte)WriteProPlatformCodes.AutoWide, (byte)WriteAddresses.ProcessingPlatform, OFF, 1);
        }

        private void PP_3dTrack_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Write((byte)WriteProPlatformCodes.Track_3D, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
            else
                serialportController.Write((byte)WriteProPlatformCodes.Track_3D, (byte)WriteAddresses.ProcessingPlatform, OFF, 1);
        }

        private void PP_GateSize_NegButton_Click(object sender, EventArgs e)
        {
            PP_GateSize_NegButton_WasClicked = true;
            //if(((Button)sender).Click)
            //serialportController.Write((byte)WriteProPlatformCodes.Track_3D, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
        }

        private void PP_GateSize_PosButton_Click(object sender, EventArgs e)
        {
            PP_GateSize_PosButton_WasClicked = true;
        }
        /// <summary>
        /// Joysticks Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mouse_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                mouseController.Start(true);
            else
                mouseController.Start(false);
        }

        private void Joystick_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                joysticksController.Start((byte)Joystick.Xbox);
            else
                joysticksController.Stop((byte)Joystick.Xbox);
        }

        private void UsbJoystick_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                joysticksController.Start((byte)Joystick.USB);
            else
                joysticksController.Stop((byte)Joystick.USB);
        }

        private void ATK3_Joystick_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                joysticksController.Start((byte)Joystick.Attack);
            else
                joysticksController.Stop((byte)Joystick.Attack);
        }
        /// <summary>
        /// Motors Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllMotorsCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteMotorCodes.All_Motors, (byte)WriteAddresses.Relay, ON, 1);
                Enable_Flag = true;
            }
            else
            {
                serialportController.Write((byte)WriteMotorCodes.All_Motors, (byte)WriteAddresses.Relay, OFF, 1);
                Motor1_CheckBox.Checked = false;
                Motor2_CheckBox.Checked = false;
                Motor3_CheckBox.Checked = false;
                EnableMotors_CheckBox.Checked = false;
            }
        }

        private void Motor1_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteMotorCodes.Motor_1, (byte)WriteAddresses.Relay, ON, 1);
                Enable_Flag = true;
            }
            else
                serialportController.Write((byte)WriteMotorCodes.Motor_1, (byte)WriteAddresses.Relay, OFF, 1);
        }

        private void Motor2_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteMotorCodes.Motor_2, (byte)WriteAddresses.Relay, ON, 1);
                Enable_Flag = true;
            }
            else
                serialportController.Write((byte)WriteMotorCodes.Motor_2, (byte)WriteAddresses.Relay, OFF, 1);
        }

        private void Motor3_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteMotorCodes.Motor_3, (byte)WriteAddresses.Relay, ON, 1);
                Enable_Flag = true;
            }
            else
                serialportController.Write((byte)WriteMotorCodes.Motor_3, (byte)WriteAddresses.Relay, OFF, 1);
        }

        private void EnableMotors_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                if (Enable_Flag)
                {
                    EnableMotors_CheckBox.Checked = false;
                    MessageBox.Show("You have to wait 3seconds please", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Thread.Sleep(3000);
                    serialportController.Write((byte)WriteMotorCodes.Enable_Motors, (byte)WriteAddresses.CAN, OFF, 1);
                    EnableMotors_CheckBox.Text = "DS_Motors";
                    Enable_Flag = false;
                    EnableMotors_CheckBox.Checked = true;
                }
                else
                {
                    serialportController.Write((byte)WriteMotorCodes.Enable_Motors, (byte)WriteAddresses.CAN, OFF, 1);
                    EnableMotors_CheckBox.Text = "DS_Motors";
                }
            }
            else
            {
                serialportController.Write((byte)WriteMotorCodes.Disable_Motors, (byte)WriteAddresses.CAN, OFF, 1);
                EnableMotors_CheckBox.Text = "EN_Motors";
            }
        }

        private void ResetAlarm_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Write((byte)WriteMotorCodes.Reset_Alarm, (byte)WriteAddresses.CAN, OFF, 1);
            Thread.Sleep(3000);
            ResetAlarm_CheckBox.Checked = false;
        }
        /// <summary>
        /// Control Table Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackRadio_CheckedChanged(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteTableCodes.Track, (byte)WriteAddresses.TableControl, ON, 1);
        }

        private void SearchRadio_CheckedChanged(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteTableCodes.Search, (byte)WriteAddresses.TableControl, Search_zeroPos, 2);
        }

        private void PositionRadio_CheckedChanged(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteTableCodes.Position, (byte)WriteAddresses.TableControl, ON, 1);
        }

        private void HomingRadio_CheckedChanged(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteTableCodes.Home, (byte)WriteAddresses.TableControl, ON, 1);
        }

        private void CancleRadio_CheckedChanged(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteTableCodes.Cancle, (byte)WriteAddresses.TableControl, ON, 1);
        }
        /// <summary>
        /// NdYag Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelayOnScan_NdYagCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                HomingScan_NYagCheckBox.Enabled = true;
                NdYagFreq_TextBox.Enabled = true;
                NdYagReady_Button.Enabled = true;

                //serialportController.Write((byte)WriteProPlatformCodes.RelayOnBoard, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
            }
            else
            {
                HomingScan_NYagCheckBox.Enabled = false;
                NdYagFreq_TextBox.Enabled = false;
                NdYagReady_Button.Enabled = false;

                //serialportController.Write((byte)WriteProPlatformCodes.RelayOnBoard, (byte)WriteAddresses.ProcessingPlatform, OFF, 1);
            }
        }

        private void HomingScan_NYagCheckBox_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void NdYagReady_Button_Click(object sender, EventArgs e)
        {

        }
    }
}