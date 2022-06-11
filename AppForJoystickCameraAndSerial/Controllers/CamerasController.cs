using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class CamerasController : ControllerBase
    {
        private readonly PictureBox _mainPictureBox, _minorPictureBox;
        private readonly Label _Camera1Label, _Camera2Label;
        private readonly CancellationToken _cancellationToken;
        private readonly Task[] cameraCaptureTasks;
        private readonly bool[] isRunning;
        private readonly Action<string> _exceptionCallback;
        public CamerasController(CancellationToken cancellationToken, PictureBox main, PictureBox minor, Label Cam1, Label Cam2, Action<string> exceptionCallback)
        {
            _cancellationToken = cancellationToken;
            _mainPictureBox = main;
            _minorPictureBox = minor;
            _Camera1Label = Cam1;
            _Camera2Label = Cam2;
            isRunning = new bool[2];
            cameraCaptureTasks = new Task[2];
            _exceptionCallback = exceptionCallback;
        }

        public void Start(int cameraIndex)
        {
            if (0 <= cameraIndex || cameraIndex <= 2)
            {
                isRunning[cameraIndex] = true;
                cameraCaptureTasks[cameraIndex] = Task.Factory.StartNew(() => StartCamera(cameraIndex), _cancellationToken).ContinueWith((t) => CameraTaskDone(t, cameraIndex == 0));
            }
            else
                throw new ArgumentOutOfRangeException();
            //cameraCaptureTasks.Add(Task.Factory.StartNew(() => StartCamera(1, false), _cancellationToken).ContinueWith((t) => CameraTaskDone(t, false)));
        }

        public void Stop(int cameraIndex)
        {
            isRunning[cameraIndex] = false;
        }

        private void StartCamera(int index)
        {
            using VideoCapture capture = new(index);
            var frame = new Mat();
            Bitmap image;

            capture.Open(index);
            if (!capture.IsOpened())
                throw new Exception($"Cannot open camera {index}");
            while (isRunning[index])
            {
                capture.Read(frame);
                image = BitmapConverter.ToBitmap(frame);
                ChangePictureBox(index == 0 ? _mainPictureBox : _minorPictureBox, image);
                ChangeLabel(index == 0 ? _Camera1Label : _Camera2Label, Color.Green);
            }
        }

        private void CameraTaskDone(Task task, bool isMain)
        {
            if (task.IsCompletedSuccessfully)
            {
                if (isMain)
                {
                    ChangeLabel(_Camera1Label, Color.Red);
                    ChangePictureBox(_mainPictureBox, AppForJoystickCameraAndSerial.Properties.Resources.wesley_tingey_mvLyHPRGLCs_unsplash);
                }
                else
                {
                    ChangeLabel(_Camera2Label, Color.Red);
                    ChangePictureBox(_minorPictureBox, AppForJoystickCameraAndSerial.Properties.Resources.wesley_tingey_mvLyHPRGLCs_unsplash);

                }
            }
            else
            {
                isRunning[Convert.ToInt32(isMain)] = false;
                _exceptionCallback.Invoke(task.Exception.Message);
            }
        }
    }
}
