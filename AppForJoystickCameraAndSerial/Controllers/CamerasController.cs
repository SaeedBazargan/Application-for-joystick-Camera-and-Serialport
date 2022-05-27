using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class CamerasController : ControllerBase
    {
        private readonly PictureBox _mainPictureBox, _minorPictureBox;
        private readonly Label _Camera1Label, _Camera2Label;
        private readonly CancellationToken _cancellationToken;
        private readonly List<Task> cameraCaptureTasks;
        private bool isRunning;
        public CamerasController(CancellationToken cancellationToken, PictureBox main, PictureBox minor, Label Cam1, Label Cam2)
        {
            _cancellationToken = cancellationToken;
            _mainPictureBox = main;
            _minorPictureBox = minor;
            _Camera1Label = Cam1;
            _Camera2Label = Cam2;
            isRunning = false;
            cameraCaptureTasks = new List<Task>(2);
        }

        public void Start()
        {
            isRunning = true;
            cameraCaptureTasks.Add(Task.Factory.StartNew(() => StartCamera(0, true), _cancellationToken).ContinueWith((t) => CameraTaskDone(t, true)));
            cameraCaptureTasks.Add(Task.Factory.StartNew(() => StartCamera(1, false), _cancellationToken).ContinueWith((t) => CameraTaskDone(t, false)));
        }

        public void Stop()
        {
            isRunning = false;
            Task.WaitAll(cameraCaptureTasks.ToArray());
            cameraCaptureTasks.Clear();
            ChangePictureBox(_mainPictureBox, AppForJoystickCameraAndSerial.Properties.Resources.wesley_tingey_mvLyHPRGLCs_unsplash);
            ChangePictureBox(_minorPictureBox, AppForJoystickCameraAndSerial.Properties.Resources.wesley_tingey_mvLyHPRGLCs_unsplash);
            ChangeLabel(_Camera1Label, Color.Red);
            ChangeLabel(_Camera2Label, Color.Red);
            ChangePictureBox(_mainPictureBox, Properties.Resources.wesley_tingey_mvLyHPRGLCs_unsplash);
            ChangePictureBox(_minorPictureBox, Properties.Resources.wesley_tingey_mvLyHPRGLCs_unsplash);
        }

        private void StartCamera(int index, bool isMain)
        {
            using VideoCapture capture = new VideoCapture(index);
            var frame = new Mat();
            Bitmap image;

            capture.Open(index);
            if (!capture.IsOpened())
                throw new Exception($"Cannot open camera {index}");
            while (isRunning)
            {
                capture.Read(frame);
                image = BitmapConverter.ToBitmap(frame);
                ChangePictureBox(isMain ? _mainPictureBox : _minorPictureBox, image);
                ChangeLabel(isMain ? _Camera1Label : _Camera2Label, Color.Green);
            }
        }

        private void CameraTaskDone(Task task, bool isMain)
        {
            if (!task.IsCompletedSuccessfully)
            {
                if (isMain)
                    ChangeLabel(_Camera1Label, Color.Red);
                else
                    ChangeLabel(_Camera2Label, Color.Red);
            }
        }
    }
}
