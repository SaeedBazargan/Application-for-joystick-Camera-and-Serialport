using DirectShowLib;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using SharpDX.DirectInput;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class CamerasController : ControllerBase
    {
        private string cameraPartNumber0;
        private string cameraPartNumber1;

        private readonly PictureBox _mainPictureBox, _minorPictureBox;
        private readonly PictureBox _Camera1Status, _Camera2Status;
        private readonly CancellationToken _cancellationToken;
        private readonly Task[] cameraCaptureTasks;
        private readonly bool[] isRunning;
        private readonly bool[] recording;
        private readonly CheckBox _rotateImages, _twoImages, _selectTvCamera, _selectIrCamera, _selectSecCamera;
        private readonly Action<string> _exceptionCallback;

        public string RecordingDirectory { get; set; }
        string recordingDir;

        public CamerasController(CancellationToken cancellationToken, PictureBox main, PictureBox minor, PictureBox camera1Status, PictureBox camera2Status, CheckBox rotate, CheckBox twoOrone,
                                 CheckBox tvCameraCheckBox, CheckBox irCameraCheckBox, CheckBox secCameraCheckBox, Action<string> exceptionCallback)
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
            _twoImages = twoOrone;
            _selectTvCamera = tvCameraCheckBox;
            _selectIrCamera = irCameraCheckBox;
            _selectSecCamera = secCameraCheckBox;
            _exceptionCallback = exceptionCallback;

        }

        private void ReadCameraPartNumbers()
        {
            DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            if (devices.Length >= 1)
            {
                cameraPartNumber0 = devices[0].DevicePath; // Store the part number of camera 0
                cameraPartNumber1 = devices[1].DevicePath; // Store the part number of camera 1
            }
            else
            {
                throw new Exception("Insufficient cameras detected.");
            }
        }

        public void Start(int cameraIndex)
        {
            if (0 <= cameraIndex || cameraIndex <= 2)
            {
                isRunning[cameraIndex] = true;
                cameraCaptureTasks[cameraIndex] = Task.Factory.StartNew(() => StartCamera(cameraIndex, cameraPartNumber0), _cancellationToken).ContinueWith((t) => CameraTaskDone(t, cameraIndex == 0));
            }
            else
                throw new ArgumentOutOfRangeException();

            //ReadCameraPartNumbers();

            //if (cameraIndex == 0)
            //{
            //    if (!string.IsNullOrEmpty(cameraPartNumber0))
            //    {
            //        isRunning[cameraIndex] = true;
            //        cameraCaptureTasks[cameraIndex] = Task.Factory.StartNew(() => StartCamera(cameraIndex, cameraPartNumber0), _cancellationToken).ContinueWith((t) => CameraTaskDone(t, true));
            //    }
            //}
            //else if (cameraIndex == 1)
            //{
            //    if (!string.IsNullOrEmpty(cameraPartNumber1))
            //    {
            //        isRunning[cameraIndex] = true;
            //        cameraCaptureTasks[cameraIndex] = Task.Factory.StartNew(() => StartCamera(cameraIndex, cameraPartNumber1), _cancellationToken).ContinueWith((t) => CameraTaskDone(t, false));
            //    }
            //}
            //else
            //    throw new ArgumentOutOfRangeException();
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

        private void StartCamera(int index, string partNumber)
        {
            using VideoCapture capture = new(index);
            VideoWriter writer = null;
            var frame = new Mat();
            Bitmap image;
            capture.Open(index);


            if (!capture.IsOpened())
            {
                ChangePictureBox(index == 0 ? _Camera1Status : _Camera2Status, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
                if (index == 1)
                    _selectSecCamera.BeginInvoke((MethodInvoker)delegate ()
                    {
                        _selectSecCamera.Checked = false;
                    });
            }
            else
            {
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

                    if (recording[index])
                    {
                        if (writer == null)
                        {
                            if (RecordingDirectory == null)
                                RecordingDirectory = @"..\..\..\..\..\..\Record\Camera\";
                            recordingDir = RecordingDirectory + index.ToString() + '/';
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

                    if (_twoImages.Checked)
                        _minorPictureBox.BeginInvoke((MethodInvoker)delegate () { _minorPictureBox.Hide(); });
                    else
                        _minorPictureBox.BeginInvoke((MethodInvoker)delegate () { _minorPictureBox.Show(); });
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
