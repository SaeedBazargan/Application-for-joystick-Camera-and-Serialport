using Com.Okmer.GameController;
using AppForJoystickCameraAndSerial.Controllers;
using SharpDX.DirectInput;
using System.Text;
using System.Numerics;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class JoysticksController : ControllerBase
    {
        private readonly TextBox _infoTxtBox;
        private readonly XBoxController xboxController;
        private readonly Label _JoystickLabel;
        private readonly PictureBox _xboxJoystickStatus;
        private readonly PictureBox _usbJoystickStatus;
        private readonly PictureBox _mainCameraPicture;
        private readonly RadioButton _searchRadioButton;
        private readonly SerialController _serialController;
        private readonly CancellationToken _cancellationToken;
        private readonly Task[] USB_JoystickTasks;
        private readonly bool[] isRunning;
        private readonly Action<string> _exceptionCallback;
        Vector2 _positionUSB;
        private readonly Point[] _positionBuffer;

        private static int _currentThrottle;
        private static int _currentYawTrim;
        private const double Ratio = 2000.0 / ushort.MaxValue;
        private const short Speed = 20;
        private int bufferPointer;

        public JoysticksController(CancellationToken cancellationToken, TextBox infoTxtBox, Label label, PictureBox XboxJoystickStatus, PictureBox USBJoystickStatus, PictureBox mainCameraPicture, RadioButton searchRadio, SerialController serialController, Action<string> exceptionCallback)
        {
            _infoTxtBox = infoTxtBox;
            _cancellationToken = cancellationToken;
            xboxController = new XBoxController();
            _JoystickLabel = label;
            _xboxJoystickStatus = XboxJoystickStatus;
            _usbJoystickStatus = USBJoystickStatus;
            _mainCameraPicture = mainCameraPicture;
            _searchRadioButton = searchRadio;
            _serialController = serialController;
            isRunning = new bool[2];
            USB_JoystickTasks = new Task[2];
            _exceptionCallback = exceptionCallback;
            Pointer.JoyPointer.SetContainerSize(_mainCameraPicture.Size);
            _positionUSB = new Vector2(320, 240);
            _positionBuffer = new Point[50000];
            bufferPointer = 0;
        }

        public void Start(byte joystickIndex)
        {
            if (joystickIndex == 0)
            {
                ChangePictureBox(_xboxJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
                xboxInit();
            }
            else if (1 <= joystickIndex || joystickIndex <= 2)
            {
                ChangePictureBox(_usbJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
                isRunning[joystickIndex] = true;
                USB_JoystickTasks[joystickIndex] = Task.Factory.StartNew(() => StartJoystick(joystickIndex), _cancellationToken).ContinueWith((t) => JoystickTaskDone(t, joystickIndex));
            }
            else
                throw new ArgumentOutOfRangeException();
        }

        public void Stop(int joystickIndex)
        {
            isRunning[joystickIndex] = false;
        }

        private void StartJoystick(int index)
        {
            var directInput = new DirectInput();
            var joystickGuid = Guid.Empty;
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;
            if (joystickGuid == Guid.Empty)
                Console.WriteLine("No joystick/Gamepad found.");

            var _joystick = new Joystick(directInput, joystickGuid);
            Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);
            _joystick.Acquire();
            //_currentThrottle = 0;
            //_currentYawTrim = 0;
            while (isRunning[index] && !_cancellationToken.IsCancellationRequested)
            {
                if (index == 1)
                {
                    var state = _joystick.GetCurrentState();
                    //Console.WriteLine("{0}", GetButtonsPressed(state.Buttons));
                    _currentThrottle = (1000 - GetAnalogStickValue(state.Sliders[0])) / 2;
                    var yawtrimChange = GetYawTrimChange(state.Buttons);
                    _currentYawTrim += yawtrimChange;
                    SetConsoleDisplay(state);
                    //Thread.Sleep(Speed);
                }
                if (index == 2)
                { }
            }
        }

        private void SetConsoleDisplay(JoystickState state)
        {
            //Console.WriteLine("Helicopter Controls");
            //Console.WriteLine("--------------------");
            //Console.WriteLine("Throttle:\t{0}   ", _currentThrottle);
            ////////Console.WriteLine("Yaw:\t\t{0}    ", -1 * (GetAnalogStickValue(state.X) + _currentYawTrim)); //For n64 controller
            //Console.WriteLine("Yaw:\t\t{0}    ", GetAnalogStickValue(state.RotationZ) + _currentYawTrim);
            //Console.WriteLine("Pitch:\t\t{0}   ", GetAnalogStickValue(state.Y));
            //Console.WriteLine();
            //Console.WriteLine("YawTrim:\t{0}   ", _currentYawTrim);
            //Console.WriteLine();
            //Console.WriteLine("POV:{0} deg      ", state.PointOfViewControllers[0] / 100);
            //Console.WriteLine();
            //Console.WriteLine("Buttons");
            //Console.WriteLine("--------------------");
            //Console.WriteLine("{0}", BoolArrayToString(state.Buttons));
            //Console.WriteLine("{0}", GetButtonsPressed(state.Buttons));
            //Console.WriteLine("X:\t\t{0}    ", GetAnalogStickValue(state.X));
            //Console.WriteLine("Y:\t\t{0}    ", GetAnalogStickValue(state.Y));
            //Console.WriteLine("W:\t\t{0}    ", GetAnalogStickValue(state.RotationZ));
            //Console.WriteLine("Slider:\t\t{0}    ", (1000 - GetAnalogStickValue(state.Sliders[0])) / 2);
            //Console.SetCursorPosition(0, 0);

            /*
             999 * x = 320 => x = 0.3203
             999 * x = 240 => x = 0.2402
             */

            //Console.WriteLine("ZZZZZ = " + state.Z);

            _positionBuffer[bufferPointer++] = new Point((int)(((state.Z) * Ratio) * 0.3203), (int)(((state.RotationZ) * Ratio) * 0.2402));
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
                //Console.WriteLine("XXXXX = " + _positionUSB.X);
                //Console.WriteLine("YYYYY = " + _positionUSB.Y);
                ////ChangeTextBox(_infoTxtBox, "[" + _positionUSB.X + ", " + _positionUSB.Y + "]");
                ////Console.WriteLine("XXXX = " + _positionUSB.X);
                ////Console.WriteLine("YYYY = " + _positionUSB.Y);
                Pointer.JoyPointer.MoveUSBJoystick(_positionUSB);
                bufferPointer = 0;
            }
        }

        private static int GetAnalogStickValue(int state)
        {
            return (int)(state * Ratio - 1000);
        }

        private static string GetButtonsPressed(bool[] array)
        {
            var s = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                if (array[i])
                {
                    if (s.Length > 0) s.Append("+");
                    s.Append(GetButtonName(i));
                }
            }
            return s.ToString().PadRight(80, ' ');
        }

        private static string BoolArrayToString(ICollection<bool> array)
        {
            var s = new StringBuilder(array.Count);
            foreach (var b in array.Take(10))
            {
                s.Append(b ? '1' : '0');
            }
            return s.ToString();
        }

        private static int GetThrottleChange(IList<bool> buttons)
        {
            if (buttons[0]) return _currentThrottle >= 1000 ? 0 : 1 * Speed;
            if (buttons[2]) return _currentThrottle <= 0 ? 0 : -1 * Speed;
            return 0;
        }

        private static int GetYawTrimChange(bool[] buttons)
        {
            if (buttons[4]) return _currentYawTrim >= 200 ? 0 : (int)(.1 * Speed);
            if (buttons[5]) return _currentYawTrim <= -200 ? 0 : (int)(-.1 * Speed);
            return 0;
        }

        private static string GetButtonName(int index)
        {
            switch (index)
            {
                case 0:
                    return "Up";
                case 1:
                    return "Right";
                case 2:
                    return "Down";
                case 3:
                    return "Left";
                case 4:
                    return "L";
                case 5:
                    return "R";
                case 6:
                    return "A";
                case 7:
                    return "Z";
                case 8:
                    return "B";
                case 9:
                    return "Start";
            }
            return "Unknown";
        }
        private void JoystickTaskDone(Task task, byte joyIndex)
        {
            if (task.IsCompletedSuccessfully)
            {
                if (joyIndex == 0)
                    ChangePictureBox(_xboxJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                if (joyIndex == 1)
                    ChangePictureBox(_usbJoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
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
            if (_searchRadioButton.Checked)
            {
                ChangeTextBox(_infoTxtBox, $"Left Thumbstick : {e.Value.LengthSquared()}");
                Pointer.JoyPointer.MoveJoystick(e.Value);
                ChangeTextBox(_infoTxtBox, "[" + "{0}" + ", " + "{1}" + "]" + e.Value.X + e.Value.Y);
                //Console.WriteLine("XXX = " + Pointer.Cursor[0]);
                //Console.WriteLine("YYY = " + Pointer.Cursor[1]);

                _serialController.Write(5, 9, Pointer.JoyPointer.Cursor, 2);
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
