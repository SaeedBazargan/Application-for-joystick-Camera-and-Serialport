﻿using Com.Okmer.GameController;
using SharpDX;
using SharpDX.DirectInput;
using System.IO.Ports;
using System.Numerics;
using static AppForJoystickCameraAndSerial.Form1;
using static System.Net.Mime.MediaTypeNames;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class JoysticksController : ControllerBase
    {
        private Joystick _joystick;
        private readonly DirectInput directInput;
        private readonly TextBox _infoTxtBox;
        private readonly CheckBox _selectATK3, _selectUSBJoy, _selectXBox;
        private readonly XBoxController xboxController;
        private readonly PictureBox _xboxJoystickStatus;
        private readonly PictureBox _usbJoystickStatus;
        private readonly PictureBox _atk3JoystickStatus;
        private readonly PictureBox _mainCameraPicture;
        private readonly RadioButton _trackRadioButton;
        private readonly RadioButton _searchRadioButton;
        private readonly RadioButton _positionRadioButton;
        private readonly RadioButton _cancleRadioButton;
        private readonly SerialController _serialController;
        private Guid joystickGuid;
        private readonly Task[] USB_JoystickTasks;
        private readonly bool[] isRunning;
        private readonly Action<string> _exceptionCallback;
        private readonly Point[] _positionBuffer;
        Vector2 _positionUSB;

        private static int _currentThrottle;
        private static int _currentYawTrim;
        private const double Ratio = 1 / 65535;
        private const short Speed = 20;
        private int bufferPointer;
        int[] ON = new int[1] { 1 };
        int[] OFF = new int[1] { 0 };
        int[] GateSize_Decrease = new int[1] { 0 };
        int[] GateSize_Increase = new int[1] { 2 };
        int[] GateSize_Stop = new int[1] { 1 };
        bool CancleButton = false;
        public bool SearchButton = false;
        public byte second = 0;
        public byte btnPressed = 0;
        public bool searchBtnClicked_Flag = false;

        public JoysticksController(TextBox infoTxtBox, CheckBox SelectATK3, CheckBox SelectUSBJoy, CheckBox SelectXBox, PictureBox XboxJoystickStatus, PictureBox USBJoystickStatus, PictureBox ATK3JoystickStatus, PictureBox mainCameraPicture, RadioButton trackRadio, RadioButton searchRadio, RadioButton positionRadio, RadioButton cancleRadio, SerialController serialController, Action<string> exceptionCallback)
        {
            directInput = new DirectInput();
            _infoTxtBox = infoTxtBox;
            _selectATK3 = SelectATK3;
            _selectUSBJoy = SelectUSBJoy;
            _selectXBox = SelectXBox;
            xboxController = new XBoxController();
            _xboxJoystickStatus = XboxJoystickStatus;
            _usbJoystickStatus = USBJoystickStatus;
            _atk3JoystickStatus = ATK3JoystickStatus;
            _mainCameraPicture = mainCameraPicture;
            _trackRadioButton = trackRadio;
            _searchRadioButton = searchRadio;
            _positionRadioButton = positionRadio;
            _cancleRadioButton = cancleRadio;
            _serialController = serialController;
            joystickGuid = Guid.Empty;
            isRunning = new bool[3];
            USB_JoystickTasks = new Task[3];
            _exceptionCallback = exceptionCallback;
            Pointer.JoyPointer.SetContainerSize(_mainCameraPicture.Size);
            _positionUSB = new Vector2(320, 240);
            _positionBuffer = new Point[50];
            bufferPointer = 0;
        }

        public void SizeChanged()
        {
            Pointer.JoyPointer.SetContainerSize(_mainCameraPicture.Size);
        }

        public void Start(byte joystickIndex)
        {
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
                Stop(joystickIndex);
            else
            {
                Console.WriteLine("joystick/Gamepad found.");
                if (joystickIndex == 0)
                {
                    ChangePictureBox(_xboxJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
                    xboxInit();
                }
                else if (1 == joystickIndex || joystickIndex == 2)
                {
                    var cancellationTokenSource = new CancellationTokenSource();
                    var token = cancellationTokenSource.Token;

                    USB_JoystickTasks[joystickIndex] = Task.Factory.StartNew(() => StartJoystick(joystickIndex), token);
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Stop(int joystickIndex)
        {
            isRunning[joystickIndex] = false;
            if (joystickIndex == 0)
            {
                ChangePictureBox(_xboxJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                _selectXBox.Invoke((MethodInvoker)delegate () { _selectXBox.Checked = false; });
            }
            else if (joystickIndex == 1)
            {
                ChangePictureBox(_usbJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                _selectUSBJoy.Invoke((MethodInvoker)delegate () { _selectUSBJoy.Checked = false; });
            }
            else if (joystickIndex == 2)
            {
                ChangePictureBox(_atk3JoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                _selectATK3.Invoke((MethodInvoker)delegate () { _selectATK3.Checked = false; });
            }
        }

        private async Task StartJoystick(int index)
        {
            try
            {
                _joystick = new Joystick(directInput, joystickGuid);
                _joystick.Acquire();
                isRunning[index] = true;
                //_cancleRadioButton.Checked = true;

                if (index == 1)
                    ChangePictureBox(_usbJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
                else if (index == 2)
                    ChangePictureBox(_atk3JoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
            }
            catch (Exception e)
            {
                ChangeTextBox(_infoTxtBox, "No joystick/Gamepad found!");
                Stop(index);
            }

            while (isRunning[index])
            {
                try
                {
                    if (index == 1)
                    {
                        var state = _joystick.GetCurrentState();
                        GetButtonsPressed(state.Buttons);
                        _currentThrottle = (1000 - GetAnalogStickValue(state.Sliders[0])) / 2;
                        var yawtrimChange = GetYawTrimChange(state.Buttons);
                        _currentYawTrim += yawtrimChange;
                        USBJoystick_State(state);
                    }
                    if (index == 2)
                    {
                        var state = _joystick.GetCurrentState();
                        GetButtonsPressed(state.Buttons);
                        _currentThrottle = (1000 - GetAnalogStickValue(state.Sliders[0])) / 2;
                        var yawtrimChange = GetYawTrimChange(state.Buttons);
                        _currentYawTrim += yawtrimChange;
                        ATK3Joystick_State(state);
                    }
                }
                catch (Exception e)
                {
                    ChangeTextBox(_infoTxtBox, $"Joystick {index + 1} is disconnected!");
                    Stop(index);
                }
            }
        }

        private void USBJoystick_State(JoystickState state)
        {
            /*
             999 * x = 320 => x = 0.3203
             999 * x = 240 => x = 0.2402
             */
        }

        private void ATK3Joystick_State(JoystickState state)
        {
            /*
             999 * x = 320 => x = 0.3203
             999 * x = 240 => x = 0.2402
             998x648
             1000 * x = 320 => x = 0.32
             1000 * x = 240 => x = 0.24
             */

            const int joystick_zero_value_x = 32758;
            const int joystick_zero_value_y = 32758;


            _positionBuffer[bufferPointer++] = new Point(state.X, state.Y);


            //Console.WriteLine("XXXXXXX" + state.X);
            //Console.WriteLine("YYYYYYY" + state.Y);

            if (bufferPointer == _positionBuffer.Length)
            {
                int x = 0, y = 0;
                for (int i = 0; i < bufferPointer; i++)
                {
                    x += _positionBuffer[i].X;
                    y += _positionBuffer[i].Y;
                }

                _positionUSB.X = x / bufferPointer;
                _positionUSB.Y = y / bufferPointer;
                Pointer.JoyPointer.MoveUSBJoystick(_positionUSB);
                bufferPointer = 0;
                
                if (SearchButton || searchBtnClicked_Flag)
                {

                    _serialController.Write((byte)Form1.WriteTableCodes.Search, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
                    //Console.WriteLine(Pointer.JoyPointer.Cursor[0].ToString());
                    searchBtnClicked_Flag = false;
                }

                //if (CancleButton)
                //{
                //    _serialController.Write((byte)WriteTableCodes.Cancle, (byte)WriteAddresses.TableControl, ON, 1);
                //}
                //else if (SearchButton || searchBtnClicked_Flag)
                //{
                //    _serialController.Write((byte)Form1.WriteTableCodes.Search, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
                //    Console.WriteLine(Pointer.JoyPointer.Cursor[0].ToString());
                //    searchBtnClicked_Flag = false;
                //}
            }
        }

        private void GetButtonsPressed(bool[] buttonsIn)
        {
            if (second == 1)
            {
                if (buttonsIn[0])
                {
                    CancleButton = false;
                    SearchButton = false;
                    Pointer.JoyPointer.MoveUSBJoystick(_positionUSB);
                    _serialController.Write((byte)Form1.WriteTableCodes.Track, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
                    _trackRadioButton.BeginInvoke((MethodInvoker)delegate ()
                    {
                        _trackRadioButton.Checked = true;
                    });
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[1])
                {
                    CancleButton = false;
                    SearchButton = true;
                    _serialController.Write((byte)Form1.WriteTableCodes.Search, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
                    _searchRadioButton.BeginInvoke((MethodInvoker)delegate ()
                    {
                        _searchRadioButton.Checked = true;
                    });
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[2])
                {
                    //_serialController.Write((byte)Form1.WriteCo2Codes.Fire, (byte)Form1.WriteAddresses.Co2, ON, 1);
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[3])
                {
                    CancleButton = true;
                    SearchButton = false;
                    _cancleRadioButton.BeginInvoke((MethodInvoker)delegate ()
                    {
                        _cancleRadioButton.Checked = true;
                    });
                    _serialController.Write((byte)WriteTableCodes.Cancle, (byte)WriteAddresses.TableControl, ON, 1);
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[4])
                {
                    _serialController.Write((byte)Form1.WriteProPlatformCodes.AutoWide, (byte)Form1.WriteAddresses.ProcessingPlatform, ON, 1);
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[5])
                {
                    _serialController.Write((byte)Form1.WriteProPlatformCodes.GateSize, (byte)Form1.WriteAddresses.ProcessingPlatform, GateSize_Decrease, 1);
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[6])
                {
                    _serialController.Write((byte)Form1.WriteProPlatformCodes.GateSize, (byte)Form1.WriteAddresses.ProcessingPlatform, GateSize_Increase, 1);
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[7])
                {
                    _serialController.Write((byte)Form1.WriteLensDriverCodes.Tele, (byte)Form1.WriteAddresses.LensDriver, OFF, 1);
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[8])
                {
                    _serialController.Write((byte)Form1.WriteLensDriverCodes.Wide, (byte)Form1.WriteAddresses.LensDriver, OFF, 1);
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[9])
                {
                    _serialController.Write((byte)Form1.WriteNdYagCodes.Fire, (byte)Form1.WriteAddresses.NdYag, OFF, 1);
                    second = 0;
                    btnPressed = 1;
                }
                else if (buttonsIn[10])
                {
                    //_serialController.Write((byte)Form1.WriteCo2Codes.Fire, (byte)Form1.WriteAddresses.Co2, OFF, 1);
                    second = 0;
                    btnPressed = 1;
                }
                else if (btnPressed == 1)
                {
                    _serialController.Write((byte)Form1.WriteLensDriverCodes.Stop, (byte)Form1.WriteAddresses.LensDriver, OFF, 1);
                    _serialController.Write((byte)Form1.WriteProPlatformCodes.GateSize, (byte)Form1.WriteAddresses.ProcessingPlatform, GateSize_Stop, 1);
                    second = 0;
                }
            }
            else
                second = 1;
        }

        private static int GetAnalogStickValue(int state)
        {
            return (int)(state * Ratio - 1000);
        }

        private static int GetYawTrimChange(bool[] buttons)
        {
            if (buttons[4]) return _currentYawTrim >= 200 ? 0 : (int)(.1 * Speed);
            if (buttons[5]) return _currentYawTrim <= -200 ? 0 : (int)(-.1 * Speed);
            return 0;
        }

        private void JoystickTaskDone(Task task, byte joyIndex)
        {
            if (task.IsCompletedSuccessfully)
            {
                if (joyIndex == 0)
                {
                    _selectXBox.Checked = false;
                    ChangePictureBox(_xboxJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                }
                else if (joyIndex == 1)
                    ChangePictureBox(_usbJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                else
                    ChangePictureBox(_atk3JoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
            }
            else
            {
                isRunning[Convert.ToInt32(joyIndex)] = false;
                _exceptionCallback.Invoke(task.Exception.Message);
            }
        }

        private void xboxInit()
        {
            //Connection
            xboxController.Connection.ValueChanged += XboxConnection_ValueChanged;

            //Buttons A, B, X, Y
            xboxController.A.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "A");
            xboxController.B.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "B");
            xboxController.X.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "X");
            xboxController.Y.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "Y");

            //Buttons Start, Back
            xboxController.Start.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "Start");
            xboxController.Back.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "Back");

            //Buttons D-Pad Up, Down, Left, Right
            xboxController.Up.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "D-Pad Up");
            xboxController.Down.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "D-Pad Down");
            xboxController.Left.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "D-Pad Left");
            xboxController.Right.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "D-Pad Right");

            //Buttons Shoulder Left, Right
            xboxController.LeftShoulder.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "Shoulder Left");
            xboxController.RightShoulder.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "Shoulder Right");

            //Buttons Thumb Left, Right
            xboxController.LeftThumbclick.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "Thumb Left");
            xboxController.RightThumbclick.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "Thumb Right");

            //Trigger Position Left, Right 
            xboxController.LeftTrigger.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "Position Left");
            xboxController.RightTrigger.ValueChanged += (s, e) => ChangeTextBox(_infoTxtBox, "Position Right");

            //Thumb Positions Left, Right
            xboxController.LeftThumbstick.ValueChanged += XboxLeftThumbstick_ValueChanged;
            xboxController.RightThumbstick.ValueChanged += XboxRightThumbstick_ValueChanged;
        }

        private void XboxConnection_ValueChanged(object sender, ValueChangeArgs<bool> e)
        {
            if (e.Value)
            {
                ChangeTextBox(_infoTxtBox, "Connected");
                ChangePictureBox(_xboxJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
            }
            else
            {
                ChangeTextBox(_infoTxtBox, "Not Connected");
                ChangePictureBox(_xboxJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
            }
        }

        private void XboxRightThumbstick_ValueChanged(object sender, ValueChangeArgs<System.Numerics.Vector2> e)
        {
            ChangeTextBox(_infoTxtBox, $"Right Thumbstick : {e.Value.LengthSquared()}");
        }

        private void XboxLeftThumbstick_ValueChanged(object sender, ValueChangeArgs<System.Numerics.Vector2> e)
        {
            if (_trackRadioButton.Checked)
            {
                ChangeTextBox(_infoTxtBox, $"Left Thumbstick : {e.Value.LengthSquared()}");
                Pointer.JoyPointer.MoveJoystick(e.Value);

                _serialController.Write((byte)Form1.WriteTableCodes.Track, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
            }
            else if (_searchRadioButton.Checked)
            {
                ChangeTextBox(_infoTxtBox, $"Left Thumbstick : {e.Value.LengthSquared()}");
                Pointer.JoyPointer.MoveJoystick(e.Value);

                _serialController.Write((byte)Form1.WriteTableCodes.Search, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
            }
        }

        public void drawIntoImage()
        {
            using (Graphics G = Graphics.FromImage(_mainCameraPicture.Image))
            {
                G.DrawEllipse(Pens.Blue, new Rectangle(10, 10, 0, 0));
            }
            _mainCameraPicture.Refresh();
        }
    }
}
