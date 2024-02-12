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

        VideoCapture[] capture;

        public string RecordingDirectory { get; set; }

        public CamerasController(PictureBox main, PictureBox minor, PictureBox camera1Status, PictureBox camera2Status, CheckBox rotate, CheckBox twoOrone, CheckBox TvCameraCheckBox, CheckBox IrCameraCheckBox, CheckBox SecCameraCheckBox)
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
                _tvCameraCheckBox.Invoke((MethodInvoker)delegate () { _tvCameraCheckBox.Checked = false; });
                _irCameraCheckBox.Invoke((MethodInvoker)delegate () { _irCameraCheckBox.Checked = false; });
            }
            else if (cameraIndex == 1)
            {
                _secCameraCheckBox.Invoke((MethodInvoker)delegate () { _secCameraCheckBox.Checked = false; });
            }

            capture[cameraIndex].Release();
        }

        private async Task StartCamera(int index)
        {
            VideoWriter writer = null;
            var frame = new Mat();
            Bitmap image;

            capture[index].Open(index, VideoCaptureAPIs.DSHOW);

            if (!capture[index].IsOpened())
                Stop(index);
            else
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

                        //if (recording[index])
                        //{
                        //    if (writer == null)
                        //    {
                        //        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        //        string recordingDir = Path.Combine(desktopPath, "Recordings");

                        //        if (!Directory.Exists(recordingDir))
                        //            Directory.CreateDirectory(recordingDir);

                        //        string recordingPath = Path.Combine(recordingDir, $"{index}_{DateTime.Now:MM-dd-yyyy-HH-mm-ss}.mp4");

                        //        writer = new VideoWriter(recordingPath, FourCC.MJPG, capture[index].Fps, new OpenCvSharp.Size(capture[index].Get(VideoCaptureProperties.FrameWidth), capture[index].Get(VideoCaptureProperties.FrameHeight)));
                        //    }
                        //    writer.Write(frame);
                        //}
                        //else if (writer != null && !writer.IsDisposed)
                        //{
                        //    writer.Release();
                        //    //writer = null;
                        //}
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
                        //DrawJoyStickPointer(image);
                        _minorPictureBox.BeginInvoke((MethodInvoker)delegate ()
                        {
                            _minorPictureBox.Image = image;
                        });

                        //if (recording[index])
                        //{
                        //    if (writer == null)
                        //    {
                        //        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        //        string recordingDir = Path.Combine(desktopPath, "Recordings");

                        //        if (!Directory.Exists(recordingDir))
                        //            Directory.CreateDirectory(recordingDir);

                        //        string recordingPath = Path.Combine(recordingDir, $"{index}_{DateTime.Now:MM-dd-yyyy-HH-mm-ss}.mp4");

                        //        writer = new VideoWriter(recordingPath, FourCC.MJPG, capture[index].Fps, new OpenCvSharp.Size(capture[index].Get(VideoCaptureProperties.FrameWidth), capture[index].Get(VideoCaptureProperties.FrameHeight)));
                        //    }
                        //    writer.Write(frame);

                        //    //if (writer == null)
                        //    //{
                        //    //    string recordingDir = RecordingDirectory + index.ToString() + '/';
                        //    //    if (!Directory.Exists(recordingDir))
                        //    //        Directory.CreateDirectory(recordingDir);
                        //    //    string recordingPath = recordingDir + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".mp4";
                        //    //    writer = new VideoWriter(recordingPath, FourCC.MJPG, capture[index].Fps, new OpenCvSharp.Size(capture[index].Get(VideoCaptureProperties.FrameWidth), capture[index].Get(VideoCaptureProperties.FrameHeight)));
                        //    //}
                        //    //writer.Write(frame);
                        //}
                        //else if (writer != null && !writer.IsDisposed)
                        //{
                        //    writer.Release();
                        //    //writer.Dispose();
                        //}
                    }
                    catch (Exception e)
                    {
                        Stop(index);
                    }
                }
                //if (_twoImages.Checked)
                //    _minorPictureBox.Invoke((MethodInvoker)(() => _minorPictureBox.Hide()));
                //else
                //    _minorPictureBox.Invoke((MethodInvoker)(() => _minorPictureBox.Show()));
            }
        }

        private void DrawJoyStickPointer(Bitmap image)
        {
            Graphics g = _mainPictureBox.CreateGraphics();
            _mainPictureBox.Image = image;
            var points = Pointer.JoyPointer.LinePoints;
            g.DrawLine(new Pen(Pointer.JoyPointer.Color, 5f), points[0], points[1]);
            g.DrawLine(new Pen(Pointer.JoyPointer.Color, 5f), points[2], points[3]);
        }
    }
}