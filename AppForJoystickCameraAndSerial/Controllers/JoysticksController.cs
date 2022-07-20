using Com.Okmer.GameController;
using AppForJoystickCameraAndSerial.Controllers;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class CameraPointer
    {
        public PointF Center { get; set; }
        public Size ContainerSize { get; set; }
        public int Radius { get; set; }
        public int[] Cursor => new int[] { (int)(Center.X * 1000), (int)(Center.Y * 1000) };
        public Color Color { get; set; } = Color.Blue;
        public PointF[] LinePoints => new PointF[]
        {
            new PointF(Center.X - Radius, Center.Y - Radius),
            new PointF(Center.X + Radius, Center.Y + Radius),

            new PointF(Center.X + Radius, Center.Y - Radius),
            new PointF(Center.X - Radius, Center.Y + Radius)
        };

        public CameraPointer(PointF center = default, int radius = 10)
        {
            Center = center;
            Radius = radius;
        }

        public void SetContainerSize(Size size)
        {
            ContainerSize = size;
            Center = new PointF(size.Width / 2, size.Height / 2);
        }

        public void Move(System.Numerics.Vector2 v)
        {
            var center = new PointF(Center.X + v.X, Center.Y - v.Y);
            if (center.X > 0 && center.X < ContainerSize.Width && center.Y > 0 && center.Y < ContainerSize.Height)
                Center = new PointF(Center.X + v.X, Center.Y - v.Y);
        }
    }

    public class JoysticksController : ControllerBase
    {
        public static readonly CameraPointer Pointer = new CameraPointer();
        private readonly TextBox _infoTxtBox;
        private readonly XBoxController xboxController;
        private readonly Label _JoystickLabel;
        private readonly PictureBox _JoystickStatus;
        private readonly PictureBox _mainCameraPicture;
        private readonly RadioButton _searchRadioButton;
        private Task _movePointerTask;
        private bool _stopMoving = true;
        private readonly SerialController _serialController;

        public JoysticksController(TextBox infoTxtBox, Label label, PictureBox JoystickStatus, PictureBox mainCameraPicture, RadioButton searchRadio, SerialController serialController)
        {
            xboxController = new XBoxController();
            _infoTxtBox = infoTxtBox;
            _JoystickLabel = label;
            _JoystickStatus = JoystickStatus;
            _mainCameraPicture = mainCameraPicture;
            _searchRadioButton = searchRadio;
            Pointer.SetContainerSize(_mainCameraPicture.Size);
            _serialController = serialController;
        }

        public void Start()
        {
            Init();
        }
        private void Init()
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
                ChangePictureBox(_JoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
            }
            else
            {
                ChangeTextBox(_infoTxtBox, "Not Connected");
                ChangePictureBox(_JoystickStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
            }
        }

        private void XboxRightThumbstick_ValueChanged(object sender, ValueChangeArgs<System.Numerics.Vector2> e)
        {
            if (_searchRadioButton.Checked)
            {
                ChangeTextBox(_infoTxtBox, $"Right Thumbstick : {e.Value.LengthSquared()}");
                if (e.Value.LengthSquared() < 1e-1)
                {
                    _stopMoving = true;
                    return;
                }
                _stopMoving = false;
                if (_movePointerTask == null || _movePointerTask.IsCompleted)
                    _movePointerTask = CreatePointerMovingTask(e.Value);
                else
                {
                    _stopMoving = true;
                    _movePointerTask.Wait();
                    _movePointerTask = CreatePointerMovingTask(e.Value);
                }
            }
        }

        private void XboxLeftThumbstick_ValueChanged(object sender, ValueChangeArgs<System.Numerics.Vector2> e)
        {
            ChangeTextBox(_infoTxtBox, $"Left Thumbstick : {e.Value}");
        }

        private Task CreatePointerMovingTask(System.Numerics.Vector2 v)
        {
            return Task.Factory.StartNew(async () =>
            {
                while (!_stopMoving)
                {
                    Pointer.Move(v);
                    _serialController.Write(100, 100, Pointer.Cursor, 2);
                    await Task.Delay(2);
                }
            });
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
