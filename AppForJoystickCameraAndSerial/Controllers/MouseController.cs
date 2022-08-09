using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class MouseController : ControllerBase
    {
        public static readonly CameraPointer Pointer = new CameraPointer();
        private readonly PictureBox _mouseStatus;
        private readonly PictureBox _mainCameraPicture;
        private readonly RadioButton _searchRadioButton;
        private readonly SerialController _serialController;

        Vector2 _position;

        public MouseController(PictureBox mouseStatus, PictureBox mainCameraPicture, RadioButton searchRadio, SerialController serialController)
        {
            //xboxController = new XBoxController();
            _mouseStatus = mouseStatus;
            _mainCameraPicture = mainCameraPicture;
            _searchRadioButton = searchRadio;
            _serialController = serialController;
            Pointer.SetContainerSize(_mainCameraPicture.Size);
            _position = new Vector2(320, 240);
        }

        public void Start(bool Status)
        {
            if (Status)
            {
                ChangePictureBox(_mouseStatus, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);
                _mainCameraPicture.MouseMove += (s, e) =>
                {
                    _position.X = e.X;
                    _position.Y = e.Y;
                    Pointer.Move(_position);
                };
            }
            else
                ChangePictureBox(_mouseStatus, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
        }

    }
}

//private void MainCameraPictureBox_Paint(object sender, PaintEventArgs e)
//{
//    e.Graphics.FillRectangle(Brushes.DarkBlue, rec);
//}

//private void MainCameraPictureBox_MouseDown(object sender, MouseEventArgs e)
//{
//    rec = new Rectangle(e.X, e.Y, 0, 0);
//    Invalidate();
//}

//private void MainCameraPictureBox_MouseMove(object sender, MouseEventArgs e)
//{
//    rec.Width = e.X - rec.X;
//    rec.Height = e.Y - rec.Y;
//    Invalidate();
//}

