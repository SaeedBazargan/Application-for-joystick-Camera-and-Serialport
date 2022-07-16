using Com.Okmer.GameController;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class CameraPointer
    {
        public PointF Center { get; set; }
        public Size ContainerSize { get; set; }
        public int Radius { get; set; }
        public Color Color { get; set; } = Color.Red;
        public PointF[] LinePoints => new PointF[]
        {
            new PointF(Center.X - Radius, Center.Y - Radius),
            new PointF(Center.X + Radius, Center.Y + Radius),

            new PointF(Center.X + Radius, Center.Y - Radius),
            new PointF(Center.X - Radius, Center.Y + Radius)
        };

        public CameraPointer(PointF center = default, int radius = 20)
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
        private Task _movePointerTask;
        private bool _stopMoving = true;

        public JoysticksController(TextBox infoTxtBox, Label label, PictureBox JoystickStatus, PictureBox mainCameraPicture)
        {
            xboxController = new XBoxController();
            _infoTxtBox = infoTxtBox;
            _JoystickLabel = label;
            _JoystickStatus = JoystickStatus;
            _mainCameraPicture = mainCameraPicture;
            Pointer.SetContainerSize(_mainCameraPicture.Size);
        }
        Rectangle rec = new Rectangle(125, 125, 50, 50);
        bool isMouseDown = false;

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
            ChangeTextBox(_infoTxtBox, $"Right Thumbstick : {e.Value}");
        }

        private void XboxLeftThumbstick_ValueChanged(object sender, ValueChangeArgs<System.Numerics.Vector2> e)
        {
            ChangeTextBox(_infoTxtBox, $"Left Thumbstick : {e.Value.LengthSquared()}");
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

        private Task CreatePointerMovingTask(System.Numerics.Vector2 v)
        {
            return Task.Factory.StartNew(async () =>
            {
                while (!_stopMoving)
                {
                    Pointer.Move(v);
                    await Task.Delay(2);
                }
            });
        }

        public void drawIntoImage()
        {
            using (Graphics G = Graphics.FromImage(_mainCameraPicture.Image))
            {
                G.DrawEllipse(Pens.Orange, new Rectangle(13, 14, 44, 44));
            }
            _mainCameraPicture.Refresh();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Red), rec);
        }
        private void PicMouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
        }
        private void PicMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                rec.Location = e.Location;
                if (rec.Right > _mainCameraPicture.Width)
                    rec.X = _mainCameraPicture.Width - rec.Width;
                if (rec.Top < 0)
                    rec.Y = 0;
                if (rec.Left < 0)
                    rec.X = 0;
                if (rec.Bottom > _mainCameraPicture.Height)
                    rec.Y = _mainCameraPicture.Height - rec.Height;
                //Refresh();
            }
        }
        private void PicMouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
    }
}
