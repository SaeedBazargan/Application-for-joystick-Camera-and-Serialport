using OpenCvSharp;
using OpenCvSharp.Extensions;
using SharpDX;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace AppForJoystickCameraAndSerial.Controllers
{
    public class CamerasController : ControllerBase
    {
        private readonly PictureBox _mainPictureBox, _minorPictureBox;
        private readonly PictureBox _Camera1Status, _Camera2Status;
        private readonly Task[] cameraCaptureTasks;
        private readonly bool[] isRunning;
        private readonly bool[] recording;
        private readonly CheckBox _rotateImages, _twoImages;
        private readonly CheckBox _tvCameraCheckBox, _irCameraCheckBox, _secCameraCheckBox;
        private readonly Action<string> _exceptionCallback;

        VideoCapture[] capture;

        public string RecordingDirectory { get; set; }

        public CamerasController(CancellationToken cancellationToken, PictureBox main, PictureBox minor, PictureBox camera1Status, PictureBox camera2Status, CheckBox rotate, CheckBox twoOrone, Action<string> exceptionCallback, CheckBox TvCameraCheckBox, CheckBox IrCameraCheckBox, CheckBox SecCameraCheckBox)
        {
            _mainPictureBox = main;
            _minorPictureBox = minor;
            _Camera1Status = camera1Status;
            _Camera2Status = camera2Status;
            cameraCaptureTasks = new Task[2];
            isRunning = new bool[2];
            recording = new bool[2];
            _rotateImages = rotate;
            _twoImages = twoOrone;
            _exceptionCallback = exceptionCallback;
            _tvCameraCheckBox = TvCameraCheckBox;
            _irCameraCheckBox = IrCameraCheckBox;
            _secCameraCheckBox = SecCameraCheckBox;

            capture = new VideoCapture[2];

            for (int i = 0; i < 2; i++)
            {
                capture[i] = new VideoCapture();
            }
        }

        public void Start(int cameraIndex)
        {
            if (0 <= cameraIndex || cameraIndex <= 2)
            {
                var cancellationTokenSource = new CancellationTokenSource();
                var token = cancellationTokenSource.Token;

                capture[cameraIndex].Open(cameraIndex, VideoCaptureAPIs.DSHOW);
                if (!capture[cameraIndex].IsOpened())
                    Console.WriteLine($"Cannot open camera {cameraIndex}");

                cameraCaptureTasks[cameraIndex] = Task.Factory.StartNew(() => StartCamera(cameraIndex), token);
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
            ChangePictureBox(cameraIndex == 0 ? _Camera1Status : _Camera2Status, AppForJoystickCameraAndSerial.Properties.Resources.Red_Circle);
            if (cameraIndex == 0)
            {
                _tvCameraCheckBox.BeginInvoke((MethodInvoker)delegate () { _tvCameraCheckBox.Checked = false; });
                _irCameraCheckBox.BeginInvoke((MethodInvoker)delegate () { _irCameraCheckBox.Checked = false; });
            }
            else if (cameraIndex == 1)
            {
                _secCameraCheckBox.BeginInvoke((MethodInvoker)delegate () { _secCameraCheckBox.Checked = false; });
            }
            capture[cameraIndex].Release();
        }

        private void StartCamera(int index)
        {
            VideoWriter writer = null;
            var frame = new Mat();
            Bitmap image;

            capture[index].Open(index, VideoCaptureAPIs.DSHOW);

            if (!capture[index].IsOpened())
                Console.WriteLine($"Cannot open camera {index}");
                //throw new Exception($"Cannot open camera {index}");
            isRunning[index] = true;
            ChangePictureBox(index == 0 ? _Camera1Status : _Camera2Status, AppForJoystickCameraAndSerial.Properties.Resources.Green_Circle);

            while (isRunning[index])
            {
                if (index == 0)
                {
                    try
                    {
                        capture[index].Read(frame);
                        image = BitmapConverter.ToBitmap(frame);
                        DrawJoyStickPointer(image);
                        _mainPictureBox.BeginInvoke((MethodInvoker)delegate ()
                        {
                            _mainPictureBox.Image = image;
                        });

                        //if (_rotateImages.Checked)
                        //    ChangePictureBox(index == 0 ? _minorPictureBox : _mainPictureBox, image);
                        //else
                        //    ChangePictureBox(index == 0 ? _mainPictureBox : _minorPictureBox, image);

                        if (recording[index])
                        {
                            if (writer == null)
                            {
                                string recordingDir = RecordingDirectory + index.ToString() + '/';
                                if (!Directory.Exists(recordingDir))
                                    Directory.CreateDirectory(recordingDir);
                                string recordingPath = recordingDir + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".mp4";
                                writer = new VideoWriter(recordingPath, FourCC.MJPG, capture[index].Fps, new OpenCvSharp.Size(capture[index].Get(VideoCaptureProperties.FrameWidth), capture[index].Get(VideoCaptureProperties.FrameHeight)));
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
                    catch (Exception e) 
                    {
                        Stop(index);
                    }
                }
                else
                {
                    try
                    {
                        capture[index].Read(frame);
                        image = BitmapConverter.ToBitmap(frame);
                        DrawJoyStickPointer(image);
                        _minorPictureBox.BeginInvoke((MethodInvoker)delegate ()
                        {
                            _minorPictureBox.Image = image;
                        });

                        if (recording[index])
                        {
                            if (writer == null)
                            {
                                string recordingDir = RecordingDirectory + index.ToString() + '/';
                                if (!Directory.Exists(recordingDir))
                                    Directory.CreateDirectory(recordingDir);
                                string recordingPath = recordingDir + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".mp4";
                                writer = new VideoWriter(recordingPath, FourCC.MJPG, capture[index].Fps, new OpenCvSharp.Size(capture[index].Get(VideoCaptureProperties.FrameWidth), capture[index].Get(VideoCaptureProperties.FrameHeight)));
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
                    catch (Exception e)
                    {
                        Stop(index);
                    }
                }
                if (_twoImages.Checked)
                    _minorPictureBox.BeginInvoke((MethodInvoker)(() => _minorPictureBox.Hide()));
                else
                    _minorPictureBox.BeginInvoke((MethodInvoker)(() => _minorPictureBox.Show()));
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