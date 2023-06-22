using AppForJoystickCameraAndSerial.Controllers;

namespace AppForJoystickCameraAndSerial
{
    public partial class Form1 : Form
    {
        enum Initial : byte
        {
            Motors = 0,
            Camera = 1,
            RangeFinder = 2,
            Serial = 3,
            ProcessingPlatform = 4,
            NdYag = 5,
            CO2 = 6,
            Fire = 7,
            Joystick = 8,
            SerialClick = 9,
            RunAutomaticlly = 10
        }

        enum Joystick : byte
        {
            Xbox = 0,
            USB = 1,
            Attack = 2
        }

        public enum WriteAddresses : byte
        {
            Relay = 1,
            LensDriver = 5,
            NdYag = 7,
            CAN = 8,
            TableControl = 9,
            ScanNdYag = 10,
            ProcessingPlatform = 11,
            Co2 = 12
        }

        public enum WriteMotorCodes : byte
        {
            Motor_1 = 1,
            Motor_2 = 2,
            Motor_3 = 3,
            All_Motors = 4,

            Enable_Motors = 6,
            Disable_Motors = 7,
            Reset_Alarm = 8
        }

        public enum WriteTableCodes : byte
        {
            Home = 4,
            Search = 5,
            Track = 6,
            Cancle = 7,
            Position = 8,
            Axis_3D = 9
        }

        public enum WriteLensDriverCodes : byte
        {
            Tele = 1,
            Wide = 2,
            Near = 3,
            Far = 4,
            Stop = 5,
            ZoomSpeed = 6,
            FocusSpeed = 7,
            RelayTvCamera = 8
        }

        public enum WriteNdYagCodes : byte
        {
            Ready = 1,
            Fire = 2,
            StopFire = 3,
            UnReady = 4,
            Frequency = 5,
            EN_Scan = 1,
            RelayScan = 2,
            HomingScan = 3
        }

        public enum WriteCo2Codes : byte
        {
            Co2_On = 1,
            Co2_Off = 2,
            Mode = 3,
            SingleShoot_Co2 = 4,
            Frequency = 5,
            //Fire = 2,
            //StopFire = 3,
            //Energy = 6
        }

        public enum WriteProPlatformCodes : byte
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

        int[] Joystick_zeroPos = new int[2] { 320, 240 };

        bool EnableMotors_Flag = false;

        int[] GateSize_Decrease = new int[1] { 0 };
        int[] GateSize_Increase = new int[1] { 2 };
        int[] GateSize_Stop = new int[1] { 1 };
        private bool PP_GateSize_NegButton_WasClicked = false;
        private bool PP_GateSize_PosButton_WasClicked = false;

        private bool CC_Zoom_WideButton_WasClicked = false;
        private bool CC_Zoom_TeleButton_WasClicked = false;
        private bool CC_Focus_NearButton_WasClicked = false;
        private bool CC_Focus_FarButton_WasClicked = false;

        private bool FireNdYagButton_WasClicked = false;
        private bool FireCo2Button_WasClicked = false;

        int[] LensDriverSpeed = new int[1] { 80 };

        int[] NdYag_Frequency = new int[1] { 1 };
        int[] CO2_Frequency = new int[1] { 1 };

        private byte NdYagReady_Button_WasClicked = 0;
        private byte TurnCo2_Button_WasClicked = 0;

        bool ReconnectSerialFlag = false;
        bool RunCameraFlag = true;

        byte secondCounter = 0;
        byte Timer_5min_Counter = 0;

        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly JoysticksController joysticksController;
        private readonly CamerasController camerasController;
        private readonly SerialController serialportController;
        private readonly MouseController mouseController;

        public Form1()
        {
            InitializeComponent();
            cancellationTokenSource = new CancellationTokenSource();
            camerasController = new CamerasController(cancellationTokenSource.Token, MainCameraPictureBox, MinorPictureBox, Camera1Status_pictureBox, Camera2Status_pictureBox, RotateImage_CheckBox, TwoImage_CheckBox,
                TvCameraCheckBox, IrCameraCheckBox, SecCameraCheckBox, CameraExceptionCallBack);
            serialportController = new SerialController(cancellationTokenSource.Token, JoystickInfoTxtBox, Serial1Status_pictureBox, Serial2Status_pictureBox, OpenPort_Button, NdYagReady_Button, SelectSerial1_CheckBox,
                SelectSerial2_CheckBox, RecordSerial_1CheckBox, RecordSerial_2CheckBox, TvCameraCheckBox, Fov_TextBox, AzError_TextBox, EiError_TextBox, Ax_TextBox, Ay_TextBox, Az_TextBox);
            joysticksController = new JoysticksController(cancellationTokenSource.Token, JoystickInfoTxtBox, ATK3_Joystick_CheckBox, UsbJoystick_CheckBox, Joystick_CheckBox, Joystick_Label, XboxJoystickStatus_pictureBox,
                USBJoystickStatus_pictureBox, ATK3JoystickStatus_pictureBox, MainCameraPictureBox, TrackRadio, SearchRadio, PositionRadio, CancleRadio, serialportController, CameraExceptionCallBack);
            mouseController = new MouseController(cancellationTokenSource, JoystickInfoTxtBox, Mouse_CheckBox, MainCameraPictureBox, SearchRadio, serialportController);

            CustomInit((byte)Initial.Motors);
            CustomInit((byte)Initial.Camera);
            CustomInit((byte)Initial.RangeFinder);
            CustomInit((byte)Initial.Serial);
            CustomInit((byte)Initial.ProcessingPlatform);
            CustomInit((byte)Initial.NdYag);
            CustomInit((byte)Initial.CO2);
            CustomInit((byte)Initial.Fire);
            CustomInit((byte)Initial.Joystick);
            CustomInit((byte)Initial.RunAutomaticlly);

            Timer_15ms.Enabled = true;
            Timer_150ms.Enabled = true;
            Timer_500ms_Reconect.Enabled = true;
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
                serialportController.Start(0);
            else if (SelectSerial2_CheckBox.Checked)
                serialportController.Start(1);
            else
            {
                JoystickInfoTxtBox.Text = "No port selected!";
                Timer_500ms_Reconect.Enabled = true;
            }
        }
        private void ClosePort_Button_Click(object sender, EventArgs e)
        {
            if (SelectSerial1_CheckBox.Checked)
                serialportController.Stop(0);
            if (SelectSerial2_CheckBox.Checked)
                serialportController.Stop(1);
            Timer_500ms_Reconect.Enabled = false;
            //RunCameraFlag = true;
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
        /// <summary>
        /// Camera Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void TurnTvCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteLensDriverCodes.RelayTvCamera, (byte)WriteAddresses.LensDriver, ON, 1);
                TurnTvCamera_CheckBox.Text = "OFF";
            }
            else
            {
                serialportController.Write((byte)WriteLensDriverCodes.RelayTvCamera, (byte)WriteAddresses.LensDriver, OFF, 1);
                TurnTvCamera_CheckBox.Text = "ON";
            }
        }

        private void TurnIrCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write(19, 6, ON, 1);
                TurnIrCamera_CheckBox.Text = "OFF";
            }
            else
            {
                serialportController.Write(19, 6, OFF, 1);
                TurnIrCamera_CheckBox.Text = "ON";
            }
        }

        private void TurnSecCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                // TODO
                TurnSecCamera_CheckBox.Text = "OFF";
            else
                // TODO
                TurnSecCamera_CheckBox.Text = "ON";
        }

        private void TvCameraCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                Thread.Sleep(2000);
                PP_RelayOnBoard_CheckBox.Checked = true;
                IrCameraCheckBox.Checked = false;
                RecordTvCamera_CheckBox.Enabled = true;
                RecordTvCamera_CheckBox.Checked = true;
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
                RecordTvCamera_CheckBox.Checked = false;
                WideCameraButton.Enabled = false;
                TeleButton.Enabled = false;
                NearButton.Enabled = false;
                FarButton.Enabled = false;

                camerasController.Stop(0);
            }
        }

        private void IrCameraCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked && serialportController.Open)
            {
                Thread.Sleep(2000);
                PP_RelayOnBoard_CheckBox.Checked = true;
                TvCameraCheckBox.Checked = false;
                RecordIrCamera_CheckBox.Enabled = true;
                RecordIrCamera_CheckBox.Checked = true;
                WideCameraButton.Enabled = true;
                TeleButton.Enabled = true;
                NearButton.Enabled = true;
                FarButton.Enabled = true;

                serialportController.Write((byte)WriteProPlatformCodes.SelectCamera, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
                camerasController.Start(0);
            }
            else
            {
                RecordIrCamera_CheckBox.Enabled = false;
                RecordIrCamera_CheckBox.Checked = false;
                WideCameraButton.Enabled = false;
                TeleButton.Enabled = false;
                NearButton.Enabled = false;
                FarButton.Enabled = false;

                camerasController.Stop(0);
            }
        }

        private void SecCameraCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                PP_RelayOnBoard_CheckBox.Checked = true;
                RecordSecCamera_CheckBox.Enabled = true;
                RecordSecCamera_CheckBox.Checked = true;
                WideCameraButton.Enabled = true;
                TeleButton.Enabled = true;
                NearButton.Enabled = true;
                FarButton.Enabled = true;

                //TODO
                camerasController.Start(1);
            }
            else
            {
                RecordSecCamera_CheckBox.Enabled = false;
                RecordSecCamera_CheckBox.Checked = false;
                WideCameraButton.Enabled = false;
                TeleButton.Enabled = false;
                NearButton.Enabled = false;
                FarButton.Enabled = false;

                camerasController.Stop(1);
            }
        }

        private void CameraExceptionCallBack(string message)
        {
            // BeginInvoke(() => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        private void RecordTvCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                RecordIrCamera_CheckBox.Checked = false;
                camerasController.Record(0);
            }
            else
                camerasController.StopRecord(0);
        }

        private void RecordIrCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                RecordTvCamera_CheckBox.Checked = false;
                camerasController.Record(0);
            }
            else
                camerasController.StopRecord(0);
        }

        private void RecordSecCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                camerasController.Record(1);
            else
                camerasController.StopRecord(1);
        }

        private void CameraLogBrowse_Button_Click(object sender, EventArgs e)
        {
            string dir;
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

        private void WideCameraButton_MouseDown(object sender, MouseEventArgs e)
        {
            CC_Zoom_WideButton_WasClicked = true;
            serialportController.Write((byte)WriteLensDriverCodes.Wide, (byte)WriteAddresses.LensDriver, OFF, 1);
        }

        private void WideCameraButton_MouseUp(object sender, MouseEventArgs e)
        {
            CC_Zoom_WideButton_WasClicked = false;
            serialportController.Write((byte)WriteLensDriverCodes.Stop, (byte)WriteAddresses.LensDriver, OFF, 1);
        }

        private void TeleButton_MouseDown(object sender, MouseEventArgs e)
        {
            CC_Zoom_TeleButton_WasClicked = true;
            serialportController.Write((byte)WriteLensDriverCodes.Tele, (byte)WriteAddresses.LensDriver, OFF, 1);
        }

        private void TeleButton_MouseUp(object sender, MouseEventArgs e)
        {
            CC_Zoom_TeleButton_WasClicked = false;
            serialportController.Write((byte)WriteLensDriverCodes.Stop, (byte)WriteAddresses.LensDriver, OFF, 1);
        }

        private void NearButton_MouseDown(object sender, MouseEventArgs e)
        {
            CC_Focus_NearButton_WasClicked = true;
            serialportController.Write((byte)WriteLensDriverCodes.Near, (byte)WriteAddresses.LensDriver, OFF, 1);
        }

        private void NearButton_MouseUp(object sender, MouseEventArgs e)
        {
            CC_Focus_NearButton_WasClicked = false;
            serialportController.Write((byte)WriteLensDriverCodes.Stop, (byte)WriteAddresses.LensDriver, OFF, 1);
        }

        private void FarButton_MouseDown(object sender, MouseEventArgs e)
        {
            CC_Focus_FarButton_WasClicked = true;
            serialportController.Write((byte)WriteLensDriverCodes.Far, (byte)WriteAddresses.LensDriver, OFF, 1);
        }

        private void FarButton_MouseUp(object sender, MouseEventArgs e)
        {
            CC_Focus_FarButton_WasClicked = false;
            serialportController.Write((byte)WriteLensDriverCodes.Stop, (byte)WriteAddresses.LensDriver, OFF, 1);
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
                NegPolarity_RadioButton.Checked = true;
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

        private void PP_GateSize_NegButton_MouseDown(object sender, MouseEventArgs e)
        {
            PP_GateSize_NegButton_WasClicked = true;
            serialportController.Write((byte)WriteProPlatformCodes.GateSize, (byte)WriteAddresses.ProcessingPlatform, GateSize_Decrease, 1);
        }

        private void PP_GateSize_NegButton_MouseUp(object sender, MouseEventArgs e)
        {
            PP_GateSize_NegButton_WasClicked = false;
            serialportController.Write((byte)WriteProPlatformCodes.GateSize, (byte)WriteAddresses.ProcessingPlatform, GateSize_Stop, 1);
        }

        private void PP_GateSize_PosButton_MouseDown(object sender, MouseEventArgs e)
        {
            PP_GateSize_PosButton_WasClicked = true;
            serialportController.Write((byte)WriteProPlatformCodes.GateSize, (byte)WriteAddresses.ProcessingPlatform, GateSize_Increase, 1);
        }

        private void PP_GateSize_PosButton_MouseUp(object sender, MouseEventArgs e)
        {
            PP_GateSize_PosButton_WasClicked = false;
            serialportController.Write((byte)WriteProPlatformCodes.GateSize, (byte)WriteAddresses.ProcessingPlatform, GateSize_Stop, 1);
        }

        /// <summary>
        /// Joysticks Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mouse_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                mouseController.Start();
            else
                mouseController.Stop();
        }

        private void Joystick_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                joysticksController.Start((byte)Joystick.Xbox);
                UsbJoystick_CheckBox.Checked = false;
                ATK3_Joystick_CheckBox.Checked = false;
            }
            else
                joysticksController.Stop((byte)Joystick.Xbox);
        }

        private void UsbJoystick_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                joysticksController.Start((byte)Joystick.USB);
                ATK3_Joystick_CheckBox.Checked = false;
                Joystick_CheckBox.Checked = false;
            }
            else
                joysticksController.Stop((byte)Joystick.USB);
        }

        private void ATK3_Joystick_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                joysticksController.Start((byte)Joystick.Attack);
                UsbJoystick_CheckBox.Checked = false;
                Joystick_CheckBox.Checked = false;
            }
            else
                joysticksController.Stop((byte)Joystick.Attack);
        }
        /// <summary>
        /// Motors Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void EmergencyStop_Button_Click(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteMotorCodes.All_Motors, (byte)WriteAddresses.Relay, OFF, 1);
            AllMotorsCheckBox.Checked = false;
            Motor1_CheckBox.Checked = false;
            Motor2_CheckBox.Checked = false;
            Motor3_CheckBox.Checked = false;
            EnableMotors_CheckBox.Checked = false;
            PP_RelayOnBoard_CheckBox.Checked = true;
        }

        private void AllMotorsCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteMotorCodes.All_Motors, (byte)WriteAddresses.Relay, ON, 1);
                EnableMotors_Flag = true;
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
                EnableMotors_Flag = true;
            }
            else
                serialportController.Write((byte)WriteMotorCodes.Motor_1, (byte)WriteAddresses.Relay, OFF, 1);
        }

        private void Motor2_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteMotorCodes.Motor_2, (byte)WriteAddresses.Relay, ON, 1);
                EnableMotors_Flag = true;
            }
            else
                serialportController.Write((byte)WriteMotorCodes.Motor_2, (byte)WriteAddresses.Relay, OFF, 1);
        }

        private void Motor3_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteMotorCodes.Motor_3, (byte)WriteAddresses.Relay, ON, 1);
                EnableMotors_Flag = true;
            }
            else
                serialportController.Write((byte)WriteMotorCodes.Motor_3, (byte)WriteAddresses.Relay, OFF, 1);
        }

        private void EnableMotors_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                if (EnableMotors_Flag)
                {
                    EnableMotors_CheckBox.Checked = false;
                    MessageBox.Show("You have to wait 3seconds please", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Thread.Sleep(3000);
                    serialportController.Write((byte)WriteMotorCodes.Enable_Motors, (byte)WriteAddresses.CAN, OFF, 1);
                    EnableMotors_CheckBox.Text = "DS_Motors";
                    EnableMotors_Flag = false;
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
        { }

        private void SearchRadio_CheckedChanged(object sender, EventArgs e)
        { }

        private void PositionRadio_CheckedChanged(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteTableCodes.Position, (byte)WriteAddresses.TableControl, Joystick_zeroPos, 2);
        }

        private void HomingRadio_CheckedChanged(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteTableCodes.Home, (byte)WriteAddresses.TableControl, ON, 1);
        }

        private void CancleRadio_CheckedChanged(object sender, EventArgs e)
        { }

        private void PositionX_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine(PositionX_TextBox.Text);
                Console.WriteLine(PositionY_TextBox.Text);
                Console.WriteLine(PositionZ_TextBox.Text);
            }
        }

        private void PositionY_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine(PositionX_TextBox.Text);
                Console.WriteLine(PositionY_TextBox.Text);
                Console.WriteLine(PositionZ_TextBox.Text);
            }
        }

        private void PositionZ_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine(PositionX_TextBox.Text);
                Console.WriteLine(PositionY_TextBox.Text);
                Console.WriteLine(PositionZ_TextBox.Text);
            }
        }
        /// <summary>
        /// NdYag Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelayOnScan_NdYagCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Write((byte)WriteNdYagCodes.RelayScan, (byte)WriteAddresses.ScanNdYag, ON, 1);
            else
                serialportController.Write((byte)WriteNdYagCodes.RelayScan, (byte)WriteAddresses.ScanNdYag, OFF, 1);
        }

        private void HomingScan_NYagCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Write((byte)WriteNdYagCodes.HomingScan, (byte)WriteAddresses.ScanNdYag, ON, 1);
            else
                serialportController.Write((byte)WriteNdYagCodes.HomingScan, (byte)WriteAddresses.ScanNdYag, OFF, 1);
        }
        private void EnableNdYagScaner_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                serialportController.Write((byte)WriteNdYagCodes.EN_Scan, (byte)WriteAddresses.ScanNdYag, ON, 1);
            else
                serialportController.Write((byte)WriteNdYagCodes.EN_Scan, (byte)WriteAddresses.ScanNdYag, OFF, 1);
        }

        private void NdYagReady_Button_Click(object sender, EventArgs e)
        {
            ++NdYagReady_Button_WasClicked;
            if (NdYagReady_Button_WasClicked == 1)
            {
                serialportController.Write((byte)WriteNdYagCodes.Ready, (byte)WriteAddresses.NdYag, OFF, 1);
                NdYagReady_Button.Text = "UnReady";
                NdYagFreq_Numeric.Enabled = true;
                FireNdYag_Button.Enabled = true;
            }
            else if (NdYagReady_Button_WasClicked == 2)
            {
                serialportController.Write((byte)WriteNdYagCodes.UnReady, (byte)WriteAddresses.NdYag, OFF, 1);
                NdYagReady_Button.Text = "Ready";
                NdYagFreq_Numeric.Enabled = false;
                FireNdYag_Button.Enabled = false;
            }
            else if (NdYagReady_Button_WasClicked == 3)
            {
                NdYagReady_Button_WasClicked = 1;
                serialportController.Write((byte)WriteNdYagCodes.Ready, (byte)WriteAddresses.NdYag, ON, 1);
                NdYagReady_Button.Text = "UnReady";
                NdYagFreq_Numeric.Enabled = true;
                FireNdYag_Button.Enabled = true;
            }
        }

        private void FireNdYag_Button_MouseDown(object sender, MouseEventArgs e)
        {
            FireNdYagButton_WasClicked = true;
            serialportController.Write((byte)WriteNdYagCodes.Fire, (byte)WriteAddresses.NdYag, OFF, 1);
        }

        private void FireNdYag_Button_MouseUp(object sender, MouseEventArgs e)
        {
            FireNdYagButton_WasClicked = false;
            serialportController.Write((byte)WriteNdYagCodes.StopFire, (byte)WriteAddresses.NdYag, ON, 1);
        }

        private void NdYagFreq_Numeric_ValueChanged(object sender, EventArgs e)
        {
            NdYag_Frequency[0] = (int)NdYagFreq_Numeric.Value;
            serialportController.Write((byte)WriteNdYagCodes.Frequency, (byte)WriteAddresses.NdYag, NdYag_Frequency, 1);
        }

        /// <summary>
        /// Range Finder Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelayOnLrf_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                SettingLrf_CheckBox.Enabled = true;
                ActiveLrf_Button.Enabled = true;
                DeactiveLrf_Button.Enabled = true;
                DownRangeLrf_Numeric.Enabled = true;
                UpRangeLrf_Numeric.Enabled = true;
                FreqLrf_Numeric.Enabled = true;
                TimeLrf_Numeric.Enabled = true;
                SingleShootRangeFinder_Button.Enabled = true;
                BurstRangeFinder_Button.Enabled = true;
                StopRangeFinder_Button.Enabled = true;
            }
            else
            {
                SettingLrf_CheckBox.Enabled = false;
                ActiveLrf_Button.Enabled = false;
                DeactiveLrf_Button.Enabled = false;
                DownRangeLrf_Numeric.Enabled = false;
                UpRangeLrf_Numeric.Enabled = false;
                FreqLrf_Numeric.Enabled = false;
                TimeLrf_Numeric.Enabled = false;
                SingleShootRangeFinder_Button.Enabled = false;
                BurstRangeFinder_Button.Enabled = false;
                StopRangeFinder_Button.Enabled = false;
            }
        }

        private void SettingLrf_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void ActiveLrf_Button_Click(object sender, EventArgs e)
        {

        }

        private void DeactiveLrf_Button_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Co2 Functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void TurnCo2_Button_Click(object sender, EventArgs e)
        {
            ++TurnCo2_Button_WasClicked;
            if (TurnCo2_Button_WasClicked == 1)
            {
                serialportController.Write((byte)WriteCo2Codes.Co2_On, (byte)WriteAddresses.Co2, ON, 1);
                TurnCo2_Button.Text = "OFF";
                AutoMode_Co2_RadioButton.Enabled = true;
                SingleMode_Co2_RadioButton.Enabled = true;
                Co2Freq_Numeric.Enabled = true;
                SingleShootCo2_Button.Enabled = true;
            }
            if (TurnCo2_Button_WasClicked == 2)
            {
                serialportController.Write((byte)WriteCo2Codes.Co2_Off, (byte)WriteAddresses.Co2, OFF, 1);
                TurnCo2_Button.Text = "ON";
                AutoMode_Co2_RadioButton.Enabled = false;
                SingleMode_Co2_RadioButton.Enabled = false;
                Co2Freq_Numeric.Enabled = false;
                SingleShootCo2_Button.Enabled = false;
            }
            if (TurnCo2_Button_WasClicked == 3)
            {
                TurnCo2_Button_WasClicked = 1;
                serialportController.Write((byte)WriteCo2Codes.Co2_On, (byte)WriteAddresses.Co2, ON, 1);
                TurnCo2_Button.Text = "OFF";
                AutoMode_Co2_RadioButton.Enabled = true;
                SingleMode_Co2_RadioButton.Enabled = true;
                Co2Freq_Numeric.Enabled = true;
                SingleShootCo2_Button.Enabled = true;
            }
        }

        private void AutoMode_Co2_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoMode_Co2_RadioButton.Checked)
            {
                serialportController.Write((byte)WriteCo2Codes.Mode, (byte)WriteAddresses.Co2, ON, 1);
                SingleShootCo2_Button.Enabled = false;
            }
        }

        private void SingleMode_Co2_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SingleMode_Co2_RadioButton.Checked)
            {
                serialportController.Write((byte)WriteCo2Codes.Mode, (byte)WriteAddresses.Co2, OFF, 1);
                SingleShootCo2_Button.Enabled = true;
            }
        }

        private void SingleShootCo2_Button_Click(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteCo2Codes.SingleShoot_Co2, (byte)WriteAddresses.Co2, OFF, 1);
        }

        private void FireCo2_Button_MouseDown(object sender, MouseEventArgs e)
        {
            FireCo2Button_WasClicked = true;
            //serialportController.Write((byte)WriteCo2Codes.Fire, (byte)WriteAddresses.Co2, ON, 1);
        }

        private void FireCo2_Button_MouseUp(object sender, MouseEventArgs e)
        {
            FireCo2Button_WasClicked = false;
            //serialportController.Write((byte)WriteCo2Codes.StopFire, (byte)WriteAddresses.Co2, ON, 1);
        }

        private void Co2Freq_Numeric_ValueChanged(object sender, EventArgs e)
        {
            CO2_Frequency[0] = (int)Co2Freq_Numeric.Value;
            serialportController.Write((byte)WriteCo2Codes.Frequency, (byte)WriteAddresses.Co2, CO2_Frequency, 1);
        }

        /// <summary>
        /// Custom Functions
        /// </summary>
        public void CustomInit(byte Initializer)
        {
            switch (Initializer)
            {
                case 0:     //Motors
                    AllMotorsCheckBox.Enabled = false;
                    Motor1_CheckBox.Enabled = false;
                    Motor2_CheckBox.Enabled = false;
                    Motor3_CheckBox.Enabled = false;
                    EnableMotors_CheckBox.Enabled = false;
                    ResetAlarm_CheckBox.Enabled = false;
                    break;
                case 1:     //Camera
                    TurnTvCamera_CheckBox.Enabled = false;
                    TurnIrCamera_CheckBox.Enabled = false;
                    TurnSecCamera_CheckBox.Enabled = false;
                    TvCameraCheckBox.Checked = false;
                    TvCameraCheckBox.Enabled = false;
                    IrCameraCheckBox.Enabled = false;
                    SecCameraCheckBox.Enabled = false;
                    RecordTvCamera_CheckBox.Enabled = false;
                    RecordIrCamera_CheckBox.Enabled = false;
                    RecordSecCamera_CheckBox.Enabled = false;
                    WideCameraButton.Enabled = false;
                    TeleButton.Enabled = false;
                    NearButton.Enabled = false;
                    FarButton.Enabled = false;
                    break;

                case 2:     //RangeFinder
                    RelayOnLrf_CheckBox.Enabled = false;
                    SettingLrf_CheckBox.Enabled = false;
                    ActiveLrf_Button.Enabled = false;
                    DeactiveLrf_Button.Enabled = false;
                    DownRangeLrf_Numeric.Enabled = false;
                    UpRangeLrf_Numeric.Enabled = false;
                    FreqLrf_Numeric.Enabled = false;
                    TimeLrf_Numeric.Enabled = false;
                    break;

                case 3:     //Serial
                    RecordSerial_1CheckBox.Enabled = false;
                    RecordSerial_2CheckBox.Enabled = false;
                    break;

                case 4:     //ProcessingPlatform
                    PP_RelayOnBoard_CheckBox.Enabled = false;
                    PP_AutoWide_CheckBox.Enabled = false;
                    PP_3dTrack_CheckBox.Enabled = false;
                    PP_GateSize_NegButton.Enabled = false;
                    PP_GateSize_PosButton.Enabled = false;
                    NegPolarity_RadioButton.Enabled = false;
                    PosPolarity_RadioButton.Enabled = false;
                    RotateImage_CheckBox.Enabled = false;
                    TwoImage_CheckBox.Enabled = false;
                    break;

                case 5:     //NdYag
                    RelayOnScan_NdYagCheckBox.Enabled = false;
                    EnableNdYagScaner_CheckBox.Enabled = false;
                    HomingScan_NYagCheckBox.Enabled = false;
                    NdYagReady_Button.Enabled = false;
                    NdYagFreq_Numeric.Enabled = false;
                    break;

                case 6:     //CO2
                    TurnCo2_Button.Enabled = false;
                    SingleShootCo2_Button.Enabled = false;
                    Co2Freq_Numeric.Enabled = false;
                    AutoMode_Co2_RadioButton.Enabled = false;
                    SingleMode_Co2_RadioButton.Enabled = false;
                    break;

                case 7:     //Fire
                    FireNdYag_Button.Enabled = false;
                    EmergencyStop_Button.Enabled = false;
                    SingleShootRangeFinder_Button.Enabled = false;
                    BurstRangeFinder_Button.Enabled = false;
                    StopRangeFinder_Button.Enabled = false;
                    FollowRadar_CheckBox.Enabled = false;
                    break;

                case 8:     //Joystick
                    UsbJoystick_CheckBox.Enabled = false;
                    Joystick_CheckBox.Enabled = false;
                    ATK3_Joystick_CheckBox.Enabled = false;
                    Mouse_CheckBox.Enabled = false;
                    ATK3_Joystick_CheckBox.Checked = false;
                    Mouse_CheckBox.Checked = false;
                    TrackRadio.Enabled = false;
                    SearchRadio.Enabled = false;
                    PositionRadio.Enabled = false;
                    HomingRadio.Enabled = false;
                    CancleRadio.Enabled = false;
                    CancleRadio.Checked = true;
                    PositionX_TextBox.Enabled = false;
                    PositionY_TextBox.Enabled = false;
                    PositionZ_TextBox.Enabled = false;
                    break;

                case 9:     //SerialClick
                    AllMotorsCheckBox.Enabled = true;
                    Motor1_CheckBox.Enabled = true;
                    Motor2_CheckBox.Enabled = true;
                    Motor3_CheckBox.Enabled = true;
                    EnableMotors_CheckBox.Enabled = true;
                    ResetAlarm_CheckBox.Enabled = true;
                    EmergencyStop_Button.Enabled = true;

                    TurnIrCamera_CheckBox.Enabled = true;
                    TurnTvCamera_CheckBox.Enabled = true;
                    TurnSecCamera_CheckBox.Enabled = true;
                    TvCameraCheckBox.Enabled = true;
                    IrCameraCheckBox.Enabled = true;
                    SecCameraCheckBox.Enabled = true;

                    RecordSerial_1CheckBox.Enabled = true;
                    RecordSerial_2CheckBox.Enabled = true;

                    PP_RelayOnBoard_CheckBox.Enabled = true;
                    RotateImage_CheckBox.Enabled = true;
                    TwoImage_CheckBox.Enabled = true;

                    RelayOnScan_NdYagCheckBox.Enabled = true;
                    EnableNdYagScaner_CheckBox.Enabled = true;
                    HomingScan_NYagCheckBox.Enabled = true;
                    NdYagReady_Button.Enabled = true;

                    TurnCo2_Button.Enabled = true;

                    TrackRadio.Enabled = true;
                    SearchRadio.Enabled = true;
                    PositionRadio.Enabled = true;
                    HomingRadio.Enabled = true;
                    CancleRadio.Enabled = true;
                    PositionX_TextBox.Enabled = true;
                    PositionY_TextBox.Enabled = true;
                    PositionZ_TextBox.Enabled = true;

                    RelayOnLrf_CheckBox.Enabled = false;

                    Mouse_CheckBox.Enabled = false;
                    Mouse_CheckBox.Checked = true;
                    ATK3_Joystick_CheckBox.Enabled = false;
                    ATK3_Joystick_CheckBox.Checked = true;
                    UsbJoystick_CheckBox.Enabled = false;
                    Joystick_CheckBox.Enabled = false;
                    break;

                case 10:    // RunAutomaticlly
                    //SelectSerial1_CheckBox.Checked = false;
                    //SelectSerial2_CheckBox.Checked = true;
                    //OpenPort_Button.PerformClick();
                    //if (!SerialisBusy)
                    //{
                    //    SelectSerial1_CheckBox.Checked = true;
                    //    SelectSerial2_CheckBox.Checked = false;
                    //    OpenPort_Button.PerformClick();
                    //}
                    //Thread.Sleep(1000);
                    //if (SerialisBusy)
                    //    TvCameraCheckBox.Checked = true;
                    break;
            }
        }

        private void Timer_15ms_Tick(object sender, EventArgs e)
        {
            if (PP_GateSize_NegButton_WasClicked)
                serialportController.Write((byte)WriteProPlatformCodes.GateSize, (byte)WriteAddresses.ProcessingPlatform, GateSize_Decrease, 1);
            else if (PP_GateSize_PosButton_WasClicked)
                serialportController.Write((byte)WriteProPlatformCodes.GateSize, (byte)WriteAddresses.ProcessingPlatform, GateSize_Increase, 1);

            else if (CC_Zoom_TeleButton_WasClicked)
                serialportController.Write((byte)WriteLensDriverCodes.Tele, (byte)WriteAddresses.LensDriver, OFF, 1);
            else if (CC_Zoom_WideButton_WasClicked)
                serialportController.Write((byte)WriteLensDriverCodes.Wide, (byte)WriteAddresses.LensDriver, OFF, 1);
            else if (CC_Focus_NearButton_WasClicked)
                serialportController.Write((byte)WriteLensDriverCodes.Near, (byte)WriteAddresses.LensDriver, OFF, 1);
            else if (CC_Focus_FarButton_WasClicked)
                serialportController.Write((byte)WriteLensDriverCodes.Far, (byte)WriteAddresses.LensDriver, OFF, 1);

            else if (FireNdYagButton_WasClicked)
                serialportController.Write((byte)WriteNdYagCodes.Fire, (byte)WriteAddresses.NdYag, OFF, 1);
            // else if (FireCo2Button_WasClicked)
            //     serialportController.Write((byte)WriteCo2Codes.Fire, (byte)WriteAddresses.Co2, OFF, 1);
        }

        private void Timer_150ms_Tick(object sender, EventArgs e)
        {
            if (serialportController.Open)
            {
                Timer_500ms_Reconect.Enabled = false;
                CustomInit((byte)Initial.SerialClick);

                secondCounter++;
                if (secondCounter == 1)
                {
                    joysticksController.second = 1;
                    secondCounter = 0;
                }

            }
            else if (!serialportController.Open)
            {
                CustomInit((byte)Initial.Motors);
                CustomInit((byte)Initial.Camera);
                CustomInit((byte)Initial.RangeFinder);
                CustomInit((byte)Initial.Serial);
                CustomInit((byte)Initial.ProcessingPlatform);
                CustomInit((byte)Initial.NdYag);
                CustomInit((byte)Initial.CO2);
                CustomInit((byte)Initial.Fire);
                CustomInit((byte)Initial.Joystick);

                Timer_500ms_Reconect.Enabled = true;
            }

            if (RecordSerial_1CheckBox.Checked == true || RecordSerial_2CheckBox.Checked == true || RecordIrCamera_CheckBox.Checked == true || RecordTvCamera_CheckBox.Checked == true || RecordSecCamera_CheckBox.Checked == true)
                Timer_5min_RecordData.Enabled = true;
        }

        private void Timer_500ms_Reconect_Tick(object sender, EventArgs e)
        {
            if (!serialportController.Open && !ReconnectSerialFlag)
            {
                SelectSerial1_CheckBox.Checked = true;
                SelectSerial2_CheckBox.Checked = false;
                OpenPort_Button_Click(sender, e);
                ReconnectSerialFlag = true;
                Thread.Sleep(1000);
            }
            else if (!serialportController.Open && ReconnectSerialFlag)
            {
                SelectSerial1_CheckBox.Checked = false;
                SelectSerial2_CheckBox.Checked = true;
                OpenPort_Button_Click(sender, e);
                ReconnectSerialFlag = false;
                Thread.Sleep(1000);
            }
        }

        private void Timer_5min_RecordData_Tick(object sender, EventArgs e)
        {
            if (Timer_5min_Counter == 0 && (RecordIrCamera_CheckBox.Checked || RecordSecCamera_CheckBox.Checked || RecordSerial_1CheckBox.Checked || RecordSerial_2CheckBox.Checked || RecordTvCamera_CheckBox.Checked))
            {
                Timer_5min_Counter = 1;
                DialogResult dialogResult = MessageBox.Show("Want to continue saving?", "Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    Timer_5min_RecordData.Enabled = false;
                    RecordSerial_1CheckBox.Checked = false;
                    RecordSerial_2CheckBox.Checked = false;
                    RecordIrCamera_CheckBox.Checked = false;
                    RecordTvCamera_CheckBox.Checked = false;
                    RecordSecCamera_CheckBox.Checked = false;
                    Timer_5min_Counter = 0;
                }
                else if (dialogResult == DialogResult.Yes)
                    Timer_5min_Counter = 0;
            }
        }
    }
}