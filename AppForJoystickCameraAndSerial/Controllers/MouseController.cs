using Microsoft.VisualBasic.Devices;
using System.Numerics;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class MouseController : ControllerBase
    {
        private readonly TextBox _infoTxtBox;
        private readonly CheckBox _selectMouse;
        private readonly PictureBox _mainCameraPicture;
        private readonly RadioButton _searchRadioButton;
        private readonly SerialController _serialController;
        private bool isReady;
        Vector2 _position;

        public MouseController(CancellationTokenSource cancellationToken, TextBox infoTxtBox, CheckBox SelectMouse, PictureBox mainCameraPicture, RadioButton searchRadio, SerialController serialController)
        {
            _infoTxtBox = infoTxtBox;
            _selectMouse = SelectMouse;
            _mainCameraPicture = mainCameraPicture;
            _searchRadioButton = searchRadio;
            _serialController = serialController;
            Pointer.JoyPointer.SetContainerSize(_mainCameraPicture.Size);
            _position = new Vector2(320, 240);
            isReady = true;
        }

        public void Start()
        {
            isReady = true;
            _mainCameraPicture.MouseMove += (s, e) =>
            {
                _position.X = e.X;
                _position.Y = e.Y;
                if (isReady)
                {
                    _serialController.Write((byte)Form1.WriteTableCodes.Position, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
                    ChangeTextBox(_infoTxtBox, $"X: {e.X}" + $", Y: {e.Y}");
                }
            };

            _mainCameraPicture.MouseClick += (s, e) =>
            {
                _position.X = e.X;
                _position.Y = e.Y;
                Pointer.JoyPointer.MoveMouse(_position);
                if (isReady)
                    _serialController.Write((byte)Form1.WriteTableCodes.Track, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
            };
        }
        public void Stop()
        {
            isReady = false;
        }
    }
}
