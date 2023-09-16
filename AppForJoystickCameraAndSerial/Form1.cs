using AppForJoystickCameraAndSerial.Controllers;
using System.Diagnostics;
using System.Threading;

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
            SerialClick = 9
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
            Co2 = 12,
            LRF = 13
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
            Position = 8
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
        }

        public enum WriteLRFCodes : byte
        {
            LRF_Relay = 1,
            LRF_Setting = 2,
            LRF_Active = 3,
            LRF_Deactive = 4,
            LRF_SingleShoot = 5,
            LRF_Burst = 6,
            LRF_Stop = 7,
            LRF_DownRange = 8,
            LRF_UpRange = 9,
            LRF_Frequency = 10,
            LRF_Time = 11
        }


        public enum WriteProPlatformCodes : byte
        {
            Polarity = 4,
            GateSize = 5,
            SelectCamera = 6,
            RelayOnBoard = 7,
            AutoWide = 8,
            Track_3D = 9,
            Axis_3D = 10
        }

        int[] ON = new int[1] { 1 };
        int[] OFF = new int[1] { 0 };

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

        private byte NdYagReady_Button_WasClicked = 0;
        private byte TurnCo2_Button_WasClicked = 0;

        bool ReconnectSerialFlag = true;
        byte selectReconnectSerial = 0;

        byte Timer_5min_Counter = 0;

        Int16 RoutineTimer_1ms = 0;

        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly JoysticksController joysticksController;
        private readonly CamerasController camerasController;
        private readonly SerialController serialportController;
        private readonly MouseController mouseController;
        private ItemController itemController;

        public Form1()
        {
            InitializeComponent();
            cancellationTokenSource = new CancellationTokenSource();
            camerasController = new CamerasController(cancellationTokenSource.Token, MainCameraPictureBox, MinorPictureBox, Camera1Status_pictureBox, Camera2Status_pictureBox, RotateImage_CheckBox, TwoImage_CheckBox,
                TvCameraCheckBox, IrCameraCheckBox, SecCameraCheckBox, CameraExceptionCallBack);
            serialportController = new SerialController(cancellationTokenSource.Token, JoystickInfoTxtBox, Serial1Status_pictureBox, Serial2Status_pictureBox, OpenPort_Button, NdYagReady_Button, SelectSerial1_CheckBox,
                SelectSerial2_CheckBox, RecordSerial_1CheckBox, RecordSerial_2CheckBox, Fov_TextBox, AzError_TextBox, EiError_TextBox, Ax_TextBox, Ay_TextBox, Az_TextBox, LrfRange_TextBox);
            joysticksController = new JoysticksController(cancellationTokenSource.Token, JoystickInfoTxtBox, ATK3_Joystick_CheckBox, UsbJoystick_CheckBox, Joystick_CheckBox, Joystick_Label, XboxJoystickStatus_pictureBox,
                USBJoystickStatus_pictureBox, ATK3JoystickStatus_pictureBox, MainCameraPictureBox, TrackRadio, SearchRadio, PositionRadio, CancleRadio, serialportController, CameraExceptionCallBack);
            mouseController = new MouseController(cancellationTokenSource, JoystickInfoTxtBox, Mouse_CheckBox, MainCameraPictureBox, SearchRadio, serialportController);
            itemController = new ItemController();

            CustomInit((byte)Initial.Motors);
            CustomInit((byte)Initial.Camera);
            CustomInit((byte)Initial.RangeFinder);
            CustomInit((byte)Initial.Serial);
            CustomInit((byte)Initial.ProcessingPlatform);
            CustomInit((byte)Initial.NdYag);
            CustomInit((byte)Initial.CO2);
            CustomInit((byte)Initial.Fire);
            CustomInit((byte)Initial.Joystick);

            Timer_1ms_Routine.Enabled = true;
        }

        private void Exit_Btn_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            joysticksController.drawIntoImage();
            ItemController itemController = new ItemController();
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
                JoystickInfoTxtBox.Text = "No port selected!";
        }
        private void ClosePort_Button_Click(object sender, EventArgs e)
        {
            if (SelectSerial1_CheckBox.Checked)
                serialportController.Stop(0);
            if (SelectSerial2_CheckBox.Checked)
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
                itemController.CheckBox_Checked(true, PP_RelayOnBoard_CheckBox, RecordTvCamera_CheckBox);
                itemController.CheckBox_Checked(false, IrCameraCheckBox);
                itemController.CheckBox_Enable(true, RecordTvCamera_CheckBox);
                itemController.Button_Enable(true, WideCameraButton, TeleButton, NearButton, FarButton);

                serialportController.Write((byte)WriteProPlatformCodes.SelectCamera, (byte)WriteAddresses.ProcessingPlatform, OFF, 1);
                camerasController.Start(0);
            }
            else
            {
                itemController.CheckBox_Checked(false, RecordTvCamera_CheckBox);
                itemController.CheckBox_Enable(false, RecordTvCamera_CheckBox);
                itemController.Button_Enable(false, WideCameraButton, TeleButton, NearButton, FarButton);

                camerasController.Stop(0);
            }
        }

        private void IrCameraCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked && serialportController.Open)
            {
                itemController.CheckBox_Checked(true, PP_RelayOnBoard_CheckBox, RecordIrCamera_CheckBox);
                itemController.CheckBox_Checked(false, TvCameraCheckBox);
                itemController.CheckBox_Enable(true, RecordIrCamera_CheckBox);
                itemController.Button_Enable(true, WideCameraButton, TeleButton, NearButton, FarButton);

                serialportController.Write((byte)WriteProPlatformCodes.SelectCamera, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
                camerasController.Start(0);
            }
            else
            {
                itemController.CheckBox_Enable(false, RecordIrCamera_CheckBox);
                itemController.CheckBox_Checked(false, RecordIrCamera_CheckBox);
                itemController.Button_Enable(true, WideCameraButton, TeleButton, NearButton, FarButton);

                camerasController.Stop(0);
            }
        }

        private void SecCameraCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                itemController.CheckBox_Checked(true, PP_RelayOnBoard_CheckBox, RecordSecCamera_CheckBox);
                itemController.CheckBox_Enable(true, RecordSecCamera_CheckBox);
                itemController.Button_Enable(true, WideCameraButton, TeleButton, NearButton, FarButton);

                //TODO
                camerasController.Start(1);
            }
            else
            {
                itemController.CheckBox_Enable(false, RecordSecCamera_CheckBox);
                itemController.CheckBox_Checked(false, RecordSecCamera_CheckBox);
                itemController.Button_Enable(false, WideCameraButton, TeleButton, NearButton, FarButton);

                camerasController.Stop(1);
            }
        }

        private void CameraExceptionCallBack(string message)
        { }

        private void RecordTvCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                itemController.CheckBox_Checked(false, RecordIrCamera_CheckBox);
                camerasController.Record(0);
            }
            else
                camerasController.StopRecord(0);
        }

        private void RecordIrCamera_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                itemController.CheckBox_Checked(false, RecordTvCamera_CheckBox);
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
                itemController.CheckBox_Enable(true, PP_AutoWide_CheckBox, PP_3dTrack_CheckBox, PP_3dTrack_CheckBox);
                itemController.Button_Enable(true, PP_GateSize_NegButton, PP_GateSize_PosButton);
                itemController.Button_Enable(true, PP_GateSize_NegButton, PP_GateSize_PosButton);
                itemController.RadioButton_Checked(true, NegPolarity_RadioButton, PosPolarity_RadioButton);
                itemController.RadioButton_Enable(true, NegPolarity_RadioButton, PosPolarity_RadioButton);

                serialportController.Write((byte)WriteProPlatformCodes.RelayOnBoard, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
                itemController.RadioButton_Checked(true, NegPolarity_RadioButton);
            }
            else
            {
                itemController.CheckBox_Enable(false, PP_AutoWide_CheckBox, PP_3dTrack_CheckBox);
                itemController.Button_Enable(false, PP_GateSize_NegButton, PP_GateSize_PosButton);
                itemController.RadioButton_Enable(false, NegPolarity_RadioButton, PosPolarity_RadioButton);

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
                itemController.CheckBox_Checked(false, UsbJoystick_CheckBox, ATK3_Joystick_CheckBox);
            }
            else
                joysticksController.Stop((byte)Joystick.Xbox);
        }

        private void UsbJoystick_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                joysticksController.Start((byte)Joystick.USB);
                itemController.CheckBox_Checked(false, ATK3_Joystick_CheckBox, Joystick_CheckBox);
            }
            else
                joysticksController.Stop((byte)Joystick.USB);
        }

        private void ATK3_Joystick_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                joysticksController.Start((byte)Joystick.Attack);
                itemController.CheckBox_Checked(false, UsbJoystick_CheckBox, Joystick_CheckBox);
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
            itemController.CheckBox_Checked(false, AllMotorsCheckBox, Motor1_CheckBox, Motor2_CheckBox, Motor3_CheckBox, Axis3D_CheckBox, EnableMotors_CheckBox);
            itemController.CheckBox_Checked(true, PP_RelayOnBoard_CheckBox);
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
                itemController.CheckBox_Checked(false, Motor1_CheckBox, Motor2_CheckBox, Motor3_CheckBox, EnableMotors_CheckBox);
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
        private void Axis3D_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                serialportController.Write((byte)WriteProPlatformCodes.Axis_3D, (byte)WriteAddresses.ProcessingPlatform, ON, 1);
            }
            else
                serialportController.Write((byte)WriteProPlatformCodes.Axis_3D, (byte)WriteAddresses.ProcessingPlatform, OFF, 1);
        }

        private void EnableMotors_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                if (EnableMotors_Flag)
                {
                    itemController.CheckBox_Checked(false, EnableMotors_CheckBox);
                    MessageBox.Show("You have to wait 3seconds please", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Thread.Sleep(30);
                    serialportController.Write((byte)WriteMotorCodes.Enable_Motors, (byte)WriteAddresses.CAN, OFF, 1);
                    EnableMotors_CheckBox.Text = "DS_Motors";
                    EnableMotors_Flag = false;
                    itemController.CheckBox_Checked(true, EnableMotors_CheckBox);
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
            itemController.CheckBox_Checked(false, ResetAlarm_CheckBox);
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
            int[] Joystick_zeroPos = new int[2] { 320, 240 };
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
                //Console.WriteLine(PositionX_TextBox.Text);
                //Console.WriteLine(PositionY_TextBox.Text);
                //Console.WriteLine(PositionZ_TextBox.Text);
            }
        }

        private void PositionY_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Console.WriteLine(PositionX_TextBox.Text);
                //Console.WriteLine(PositionY_TextBox.Text);
                //Console.WriteLine(PositionZ_TextBox.Text);
            }
        }

        private void PositionZ_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Console.WriteLine(PositionX_TextBox.Text);
                //Console.WriteLine(PositionY_TextBox.Text);
                //Console.WriteLine(PositionZ_TextBox.Text);
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
                itemController.Numeric_Enable(true, NdYagFreq_Numeric);
                itemController.Button_Enable(true, FireNdYag_Button);
            }
            else if (NdYagReady_Button_WasClicked == 2)
            {
                serialportController.Write((byte)WriteNdYagCodes.UnReady, (byte)WriteAddresses.NdYag, OFF, 1);
                NdYagReady_Button.Text = "Ready";
                itemController.Numeric_Enable(false, NdYagFreq_Numeric);
                itemController.Button_Enable(false, FireNdYag_Button);
            }
            else if (NdYagReady_Button_WasClicked == 3)
            {
                NdYagReady_Button_WasClicked = 1;
                serialportController.Write((byte)WriteNdYagCodes.Ready, (byte)WriteAddresses.NdYag, ON, 1);
                NdYagReady_Button.Text = "UnReady";
                itemController.Numeric_Enable(true, NdYagFreq_Numeric);
                itemController.Button_Enable(true, FireNdYag_Button);
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
            int[] NdYag_Frequency = new int[1] { (int)NdYagFreq_Numeric.Value };
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
                itemController.CheckBox_Enable(true, SettingLrf_CheckBox);
                itemController.Button_Enable(true, ActiveLrf_Button, DeactiveLrf_Button, SingleShootRangeFinder_Button, BurstRangeFinder_Button, StopRangeFinder_Button);
                itemController.Numeric_Enable(true, DownRangeLrf_Numeric, UpRangeLrf_Numeric, FreqLrf_Numeric, TimeLrf_Numeric);

                serialportController.Write((byte)WriteLRFCodes.LRF_Relay, (byte)WriteAddresses.LRF, ON, 1);
            }
            else
            {
                itemController.CheckBox_Checked(false, SettingLrf_CheckBox);
                itemController.CheckBox_Enable(false, SettingLrf_CheckBox);
                itemController.Button_Enable(false, ActiveLrf_Button, DeactiveLrf_Button, SingleShootRangeFinder_Button, BurstRangeFinder_Button, StopRangeFinder_Button);
                itemController.Numeric_Enable(false, DownRangeLrf_Numeric, UpRangeLrf_Numeric, FreqLrf_Numeric, TimeLrf_Numeric);

                serialportController.Write((byte)WriteLRFCodes.LRF_Relay, (byte)WriteAddresses.LRF, OFF, 1);
            }
        }

        private void SettingLrf_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            int[] LRF_Down = new int[1] { (int)DownRangeLrf_Numeric.Value };
            int[] LRF_Up = new int[1] { (int)UpRangeLrf_Numeric.Value };
            int[] LRF_Freq = new int[1] { (int)FreqLrf_Numeric.Value };
            int[] LRF_Time = new int[1] { (int)TimeLrf_Numeric.Value };

            serialportController.Write((byte)WriteLRFCodes.LRF_Setting, (byte)WriteAddresses.LRF, ON, 1);
            serialportController.Write((byte)WriteLRFCodes.LRF_DownRange, (byte)WriteAddresses.LRF, LRF_Down, 1);
            serialportController.Write((byte)WriteLRFCodes.LRF_UpRange, (byte)WriteAddresses.LRF, LRF_Up, 1);
            serialportController.Write((byte)WriteLRFCodes.LRF_Frequency, (byte)WriteAddresses.LRF, LRF_Freq, 1);
            serialportController.Write((byte)WriteLRFCodes.LRF_Time, (byte)WriteAddresses.LRF, LRF_Time, 1);
        }

        private void ActiveLrf_Button_Click(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteLRFCodes.LRF_Active, (byte)WriteAddresses.LRF, ON, 1);
            itemController.Button_Enable(false, ActiveLrf_Button);
        }

        private void DeactiveLrf_Button_Click(object sender, EventArgs e)
        {
            serialportController.Write((byte)WriteLRFCodes.LRF_Deactive, (byte)WriteAddresses.LRF, ON, 1);
            itemController.Button_Enable(true, ActiveLrf_Button);
            itemController.Button_Enable(false, DeactiveLrf_Button);
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
                itemController.RadioButton_Enable(true, AutoMode_Co2_RadioButton, SingleMode_Co2_RadioButton);
                itemController.Numeric_Enable(true, Co2Freq_Numeric);
                itemController.Button_Enable(true, SingleShootCo2_Button);
            }
            if (TurnCo2_Button_WasClicked == 2)
            {
                serialportController.Write((byte)WriteCo2Codes.Co2_Off, (byte)WriteAddresses.Co2, OFF, 1);
                TurnCo2_Button.Text = "ON";
                itemController.RadioButton_Enable(false, AutoMode_Co2_RadioButton, SingleMode_Co2_RadioButton);
                itemController.Numeric_Enable(false, Co2Freq_Numeric);
                itemController.Button_Enable(false, SingleShootCo2_Button);
            }
            if (TurnCo2_Button_WasClicked == 3)
            {
                TurnCo2_Button_WasClicked = 1;
                serialportController.Write((byte)WriteCo2Codes.Co2_On, (byte)WriteAddresses.Co2, ON, 1);
                TurnCo2_Button.Text = "OFF";
                itemController.RadioButton_Enable(true, AutoMode_Co2_RadioButton, SingleMode_Co2_RadioButton);
                itemController.Numeric_Enable(true, Co2Freq_Numeric);
                itemController.Button_Enable(true, SingleShootCo2_Button);
            }
        }

        private void AutoMode_Co2_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoMode_Co2_RadioButton.Checked)
            {
                serialportController.Write((byte)WriteCo2Codes.Mode, (byte)WriteAddresses.Co2, ON, 1);
                itemController.Button_Enable(false, SingleShootCo2_Button);
            }
        }

        private void SingleMode_Co2_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SingleMode_Co2_RadioButton.Checked)
            {
                serialportController.Write((byte)WriteCo2Codes.Mode, (byte)WriteAddresses.Co2, OFF, 1);
                itemController.Button_Enable(true, SingleShootCo2_Button);
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
            int[] CO2_Frequency = new int[1] { (int)Co2Freq_Numeric.Value };
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
                    itemController.CheckBox_Enable(false, AllMotorsCheckBox, Motor1_CheckBox, Motor2_CheckBox, Motor3_CheckBox, EnableMotors_CheckBox, ResetAlarm_CheckBox, Axis3D_CheckBox);
                    break;
                case 1:     //Camera
                    itemController.CheckBox_Enable(false, TurnTvCamera_CheckBox, TurnIrCamera_CheckBox, TurnSecCamera_CheckBox, TvCameraCheckBox,
                        IrCameraCheckBox, SecCameraCheckBox, RecordTvCamera_CheckBox, RecordIrCamera_CheckBox, RecordSecCamera_CheckBox);
                    itemController.CheckBox_Checked(false, TvCameraCheckBox);
                    itemController.Button_Enable(false, WideCameraButton, TeleButton, NearButton, FarButton);
                    break;

                case 2:     //RangeFinder
                    itemController.CheckBox_Enable(false, RelayOnLrf_CheckBox, SettingLrf_CheckBox);
                    itemController.Button_Enable(false, ActiveLrf_Button, DeactiveLrf_Button);
                    itemController.Numeric_Enable(false, DownRangeLrf_Numeric, UpRangeLrf_Numeric, FreqLrf_Numeric, TimeLrf_Numeric);
                    break;

                case 3:     //Serial
                    itemController.CheckBox_Enable(false, RecordSerial_1CheckBox, RecordSerial_2CheckBox);
                    itemController.CheckBox_Checked(false, RecordSerial_1CheckBox, RecordSerial_2CheckBox);
                    break;

                case 4:     //ProcessingPlatform
                    itemController.CheckBox_Enable(false, PP_RelayOnBoard_CheckBox, PP_AutoWide_CheckBox, PP_3dTrack_CheckBox, RotateImage_CheckBox, TwoImage_CheckBox);
                    itemController.Button_Enable(false, PP_GateSize_NegButton, PP_GateSize_PosButton);
                    itemController.RadioButton_Enable(false, NegPolarity_RadioButton, PosPolarity_RadioButton);
                    break;

                case 5:     //NdYag
                    itemController.CheckBox_Enable(false, RelayOnScan_NdYagCheckBox, EnableNdYagScaner_CheckBox, HomingScan_NYagCheckBox);
                    itemController.Button_Enable(false, NdYagReady_Button);
                    itemController.Numeric_Enable(false, NdYagFreq_Numeric);
                    break;

                case 6:     //CO2
                    itemController.Button_Enable(false, TurnCo2_Button, SingleShootCo2_Button);
                    itemController.Numeric_Enable(false, Co2Freq_Numeric);
                    itemController.RadioButton_Enable(false, AutoMode_Co2_RadioButton, SingleMode_Co2_RadioButton);
                    break;

                case 7:     //Fire
                    itemController.Button_Enable(false, FireNdYag_Button, EmergencyStop_Button, SingleShootRangeFinder_Button, BurstRangeFinder_Button, StopRangeFinder_Button);
                    itemController.CheckBox_Enable(false, FollowRadar_CheckBox);
                    break;

                case 8:     //Joystick
                    itemController.CheckBox_Enable(false, UsbJoystick_CheckBox, Joystick_CheckBox, ATK3_Joystick_CheckBox, Mouse_CheckBox);
                    itemController.CheckBox_Checked(false, ATK3_Joystick_CheckBox, Mouse_CheckBox, Mouse_CheckBox);
                    itemController.RadioButton_Enable(false, TrackRadio, SearchRadio, PositionRadio, HomingRadio, CancleRadio);
                    itemController.RadioButton_Checked(true, CancleRadio);

                    PositionX_TextBox.Enabled = false;
                    PositionY_TextBox.Enabled = false;
                    PositionZ_TextBox.Enabled = false;
                    break;

                case 9:     //SerialClick
                    itemController.CheckBox_Enable(true, AllMotorsCheckBox, Motor1_CheckBox, Motor2_CheckBox, Motor3_CheckBox, EnableMotors_CheckBox, Axis3D_CheckBox, ResetAlarm_CheckBox,
                        TurnIrCamera_CheckBox, TurnTvCamera_CheckBox, TurnSecCamera_CheckBox, TvCameraCheckBox, IrCameraCheckBox, SecCameraCheckBox, RecordSerial_1CheckBox,
                        RecordSerial_2CheckBox, PP_RelayOnBoard_CheckBox, RotateImage_CheckBox, TwoImage_CheckBox, RelayOnScan_NdYagCheckBox, EnableNdYagScaner_CheckBox, HomingScan_NYagCheckBox,
                        RelayOnLrf_CheckBox);
                    itemController.CheckBox_Enable(false, Mouse_CheckBox, ATK3_Joystick_CheckBox, UsbJoystick_CheckBox, Joystick_CheckBox);
                    itemController.CheckBox_Checked(true, Mouse_CheckBox, ATK3_Joystick_CheckBox);
                    itemController.Button_Enable(true, EmergencyStop_Button, NdYagReady_Button, TurnCo2_Button);
                    itemController.RadioButton_Enable(true, TrackRadio, SearchRadio, PositionRadio, HomingRadio, CancleRadio);

                    PositionX_TextBox.Enabled = true;
                    PositionY_TextBox.Enabled = true;
                    PositionZ_TextBox.Enabled = true;
                    break;
            }
        }

        private void Timer_1ms_Routine_Tick(object sender, EventArgs e)
        {
            RoutineTimer_1ms++;
            Console.WriteLine(RoutineTimer_1ms);

            while (ReconnectSerialFlag && RoutineTimer_1ms == 10)
            {
                itemController.CheckBox_Checked(true, SelectSerial1_CheckBox);
                itemController.CheckBox_Checked(false, SelectSerial2_CheckBox);
                serialportController.Start(0);

                //if (serialportController.CheckOpen(0))
                //{
                //    ReconnectSerialFlag = false;
                //    CustomInit((byte)Initial.SerialClick);
                //    joysticksController.second = 1;

                //    if (PP_GateSize_NegButton_WasClicked)
                //        serialportController.Write((byte)WriteProPlatformCodes.GateSize, (byte)WriteAddresses.ProcessingPlatform, GateSize_Decrease, 1);
                //    else if (PP_GateSize_PosButton_WasClicked)
                //        serialportController.Write((byte)WriteProPlatformCodes.GateSize, (byte)WriteAddresses.ProcessingPlatform, GateSize_Increase, 1);

                //    else if (CC_Zoom_TeleButton_WasClicked)
                //        serialportController.Write((byte)WriteLensDriverCodes.Tele, (byte)WriteAddresses.LensDriver, OFF, 1);
                //    else if (CC_Zoom_WideButton_WasClicked)
                //        serialportController.Write((byte)WriteLensDriverCodes.Wide, (byte)WriteAddresses.LensDriver, OFF, 1);
                //    else if (CC_Focus_NearButton_WasClicked)
                //        serialportController.Write((byte)WriteLensDriverCodes.Near, (byte)WriteAddresses.LensDriver, OFF, 1);
                //    else if (CC_Focus_FarButton_WasClicked)
                //        serialportController.Write((byte)WriteLensDriverCodes.Far, (byte)WriteAddresses.LensDriver, OFF, 1);

                //    else if (FireNdYagButton_WasClicked)
                //        serialportController.Write((byte)WriteNdYagCodes.Fire, (byte)WriteAddresses.NdYag, OFF, 1);
                //    // else if (FireCo2Button_WasClicked)
                //    //     serialportController.Write((byte)WriteCo2Codes.Fire, (byte)WriteAddresses.Co2, OFF, 1);
                //}
                //else
                //{
                //    CustomInit((byte)Initial.Motors);
                //    CustomInit((byte)Initial.Camera);
                //    CustomInit((byte)Initial.RangeFinder);
                //    CustomInit((byte)Initial.Serial);
                //    CustomInit((byte)Initial.ProcessingPlatform);
                //    CustomInit((byte)Initial.NdYag);
                //    CustomInit((byte)Initial.CO2);
                //    CustomInit((byte)Initial.Fire);
                //    CustomInit((byte)Initial.Joystick);

                //    ReconnectSerialFlag = true;

                //    switch (selectReconnectSerial)
                //    {
                //        case 0:
                //            itemController.CheckBox_Checked(true, SelectSerial1_CheckBox);
                //            itemController.CheckBox_Checked(false, SelectSerial2_CheckBox);
                //            serialportController.Start(0);
                //            if (!serialportController.CheckOpen(0))
                //                selectReconnectSerial = 1;
                //            break;

                //        case 1:
                //            itemController.CheckBox_Checked(false, SelectSerial1_CheckBox);
                //            itemController.CheckBox_Checked(true, SelectSerial2_CheckBox);
                //            serialportController.Start(1);
                //            if (!serialportController.CheckOpen(1))
                //                selectReconnectSerial = 0;
                //            break;
                //    }
                //}
                RoutineTimer_1ms = 0;
            }
            //if (RecordSerial_1CheckBox.Checked || RecordSerial_2CheckBox.Checked || RecordIrCamera_CheckBox.Checked || RecordTvCamera_CheckBox.Checked || RecordSecCamera_CheckBox.Checked)
            //{
            //    Timer_5min_RecordData.Enabled = true;
            //}
            //}
            //else if (RoutineTimer_1ms == 101)
            //{
            //    RoutineTimer_1ms = 0;
            //}
        }

        //if (Timer_5min_Counter == 0 && (RecordIrCamera_CheckBox.Checked || RecordSecCamera_CheckBox.Checked || RecordSerial_1CheckBox.Checked || RecordSerial_2CheckBox.Checked || RecordTvCamera_CheckBox.Checked))
        //{
        //    Timer_5min_Counter = 1;
        //    DialogResult dialogResult = MessageBox.Show("Want to continue saving?", "Warning", MessageBoxButtons.YesNo);
        //    if (dialogResult == DialogResult.No)
        //    {
        //        //Timer_5min_RecordData.Enabled = false;
        //        itemController.CheckBox_Checked(false, RecordSerial_1CheckBox, RecordSerial_2CheckBox, RecordIrCamera_CheckBox, RecordTvCamera_CheckBox, RecordSecCamera_CheckBox);
        //        Timer_5min_Counter = 0;
        //    }
        //    else if (dialogResult == DialogResult.Yes)
        //        Timer_5min_Counter = 0;
        //}
    }
}