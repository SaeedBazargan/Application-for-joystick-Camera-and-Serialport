using Microsoft.VisualBasic.Devices;
using System.Numerics;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class MouseController : ControllerBase
    {
        private readonly PictureBox _mainCameraPicture;
        private readonly RadioButton _searchRadioButton;
        private readonly SerialController _serialController;
        private readonly CancellationTokenSource _cancellationToken;
        Vector2 _position;

        public MouseController(CancellationTokenSource cancellationToken, PictureBox mainCameraPicture, RadioButton searchRadio, SerialController serialController)
        {
            _mainCameraPicture = mainCameraPicture;
            _searchRadioButton = searchRadio;
            _serialController = serialController;
            _cancellationToken = cancellationToken;
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
                    _serialController.Write((byte)Form1.WriteTableCodes.Position, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
                };

                _mainCameraPicture.MouseClick += (s, e) =>
                {
                    _position.X = e.X;
                    _position.Y = e.Y;
                    Pointer.JoyPointer.MoveMouse(_position);
                    _serialController.Write((byte)Form1.WriteTableCodes.Track, (byte)Form1.WriteAddresses.TableControl, Pointer.JoyPointer.Cursor, 2);
                };
            }
            else if (!Status)
            {
                _cancellationToken.Cancel();
                Status = false;
            }
        }
    }
}
