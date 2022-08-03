using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class MouseController : ControllerBase
    {
        public static readonly CameraPointer Pointer = new CameraPointer();
        //private readonly XBoxController xboxController;
        private readonly PictureBox _mouseStatus;
        private readonly PictureBox _mainCameraPicture;
        private readonly RadioButton _searchRadioButton;
        private readonly SerialController _serialController;

        public MouseController(PictureBox mouseStatus, PictureBox mainCameraPicture, RadioButton searchRadio, SerialController serialController)
        {
            //xboxController = new XBoxController();
            _mouseStatus = mouseStatus;
            _mainCameraPicture = mainCameraPicture;
            _searchRadioButton = searchRadio;
            _serialController = serialController;
            Pointer.SetContainerSize(_mainCameraPicture.Size);
        }
    }
}
