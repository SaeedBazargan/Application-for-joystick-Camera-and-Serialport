using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class CamerasController : ControllerBase
    {
        private readonly PictureBox _mainPictureBox, _minorPictureBox;
        private readonly PictureBox _Camera1Status, _Camera2Status;
        private readonly CancellationToken _cancellationToken;
        private readonly Task[] cameraCaptureTasks;
        private readonly bool[] isRunning;
        private readonly bool[] recording;
        private readonly CheckBox _rotateImages;
        private readonly Action<string> _exceptionCallback;

        public string RecordingDirectory { get; set; }

        public CamerasController(CancellationToken cancellationToken, PictureBox main, PictureBox minor, PictureBox camera1Status, PictureBox camera2Status, CheckBox rotate, Action<string> exceptionCallback)
        {
            _mainPictureBox = main;
            _minorPictureBox = minor;
            _Camera1Status = camera1Status;
            _Camera2Status = camera2Status;
            _cancellationToken = cancellationToken;
            cameraCaptureTasks = new Task[2];
            isRunning = new bool[2];
            recording = new bool[2];
            _rotateImages = rotate;
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
        }

        public void Record(int cameraIndex)
        {
            recording[cameraIndex] = true;
        }

        public void StopRecord(int cameraIndex)
        {
            recording[cameraIndex] = false;
        }

        public void RecordDirectory(string Directory)
        {
            RecordingDirectory = Directory;
        }

        public void Stop(int cameraIndex)
        {
            isRunning[cameraIndex] = false;
        }

        private void StartCamera(int index)
        {
            using VideoCapture capture = new(index);
            VideoWriter writer = null;
            var frame = new Mat();
            Bitmap image;
            capture.Open(index);

            if (!capture.IsOpened())
                throw new Exception($"Cannot open camera {index}");
            ChangePictureBox(index == 0 ? _Camera1Status : _Camera2Status, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);

            while (isRunning[index])
            {
                capture.Read(frame);
                image = BitmapConverter.ToBitmap(frame);
                DrawJoyStickPointer(image);
                if (_rotateImages.Checked)
                    ChangePictureBox(index == 0 ? _minorPictureBox : _mainPictureBox, image);
                else
                    ChangePictureBox(index == 0 ? _mainPictureBox : _minorPictureBox, image);
                    //HidePictureBox(index == 0 ? _minorPictureBox : _mainPictureBox);
                if (recording[index])
                {
                    if (writer == null)
                    {
                        string recordingDir = RecordingDirectory + index.ToString() + '/';
                        if (!Directory.Exists(recordingDir))
                            Directory.CreateDirectory(recordingDir);
                        string recordingPath = recordingDir + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".mp4";
                        writer = new VideoWriter(recordingPath, FourCC.MJPG, capture.Fps, new OpenCvSharp.Size(capture.Get(VideoCaptureProperties.FrameWidth), capture.Get(VideoCaptureProperties.FrameHeight)));
                    }
                    writer.Write(frame);
                }
                else if (writer != null && !writer.IsDisposed)
                {
                    writer.Release();
                    writer.Dispose();
                    writer = null;
                }
            }
        }

        private void DrawJoyStickPointer(Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);
            var points = Pointer.JoyPointer.LinePoints;
            g.DrawLine(new Pen(Pointer.JoyPointer.Color, 3f), points[0], points[1]);
            g.DrawLine(new Pen(Pointer.JoyPointer.Color, 3f), points[2], points[3]);
        }

        private void CameraTaskDone(Task task, bool isMain)
        {
            if (task.IsCompletedSuccessfully)
            {
                if (isMain)
                {
                    ChangePictureBox(_Camera1Status, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                    ChangePictureBox(_mainPictureBox, AppForJoystickCameraAndSerial.Properties.Resources.Premium_Photo___Macro_falling_coffee_bean_on_gray_background);
                }
                else
                {
                    ChangePictureBox(_Camera2Status, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                    ChangePictureBox(_minorPictureBox, AppForJoystickCameraAndSerial.Properties.Resources.Premium_Photo___Macro_falling_coffee_bean_on_gray_background);
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
