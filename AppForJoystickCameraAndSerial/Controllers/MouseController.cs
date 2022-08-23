using System.Numerics;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class MouseController : ControllerBase
    {
        private readonly PictureBox _mainCameraPicture;
        private readonly RadioButton _searchRadioButton;
        private readonly SerialController _serialController;
        Vector2 _position;

        public MouseController(PictureBox mainCameraPicture, RadioButton searchRadio, SerialController serialController)
        {
            //xboxController = new XBoxController();
            _mainCameraPicture = mainCameraPicture;
            _searchRadioButton = searchRadio;
            _serialController = serialController;
            Pointer.JoyPointer.SetContainerSize(_mainCameraPicture.Size);
            _position = new Vector2(320, 240);
        }

        public void Start(bool Status)
        {
            if (Status)
            {
                _mainCameraPicture.MouseMove += (s, e) =>
                {
                    _position.X = e.X;
                    _position.Y = e.Y;
                    Pointer.JoyPointer.MoveMouse(_position);
                };
            }
        }

    }
}
