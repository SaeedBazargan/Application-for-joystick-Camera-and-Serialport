using Com.Okmer.GameController;

namespace AppForJoystickCameraAndSerial
{
    public partial class Form1 : Form
    {
        XBoxController controller = new XBoxController();
        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;

            //Connection
            controller.Connection.ValueChanged += Connection_ValueChanged;

            //Buttons A, B, X, Y
            controller.A.ValueChanged += (s, e) => ChangeTextBox(textBox1, "A");
            controller.B.ValueChanged += (s, e) => ChangeTextBox(textBox1, "B");
            controller.X.ValueChanged += (s, e) => ChangeTextBox(textBox1, "X");
            controller.Y.ValueChanged += (s, e) => ChangeTextBox(textBox1, "Y");

            //Buttons Start, Back
            controller.Start.ValueChanged += (s, e) => ChangeTextBox(textBox1, "Start");
            controller.Back.ValueChanged += (s, e) => ChangeTextBox(textBox1, "Back");

            //Buttons D-Pad Up, Down, Left, Right
            controller.Up.ValueChanged += (s, e) => ChangeTextBox(textBox1, "D-Pad Up");
            controller.Down.ValueChanged += (s, e) => ChangeTextBox(textBox1, "D-Pad Down");
            controller.Left.ValueChanged += (s, e) => ChangeTextBox(textBox1, "D-Pad Left");
            controller.Right.ValueChanged += (s, e) => ChangeTextBox(textBox1, "D-Pad Right");

            //Buttons Shoulder Left, Right
            controller.LeftShoulder.ValueChanged += (s, e) => ChangeTextBox(textBox1, "Shoulder Left");
            controller.RightShoulder.ValueChanged += (s, e) => ChangeTextBox(textBox1, "Shoulder Right");

            //Buttons Thumb Left, Right
            controller.LeftThumbclick.ValueChanged += (s, e) => ChangeTextBox(textBox1, "Thumb Left");
            controller.RightThumbclick.ValueChanged += (s, e) => ChangeTextBox(textBox1, "Thumb Right");

            //Trigger Position Left, Right 
            controller.LeftTrigger.ValueChanged += (s, e) => ChangeTextBox(textBox1, "Position Left");
            controller.RightTrigger.ValueChanged += (s, e) => ChangeTextBox(textBox1, "Position Right");

            //Thumb Positions Left, Right
            controller.LeftThumbstick.ValueChanged += LeftThumbstick_ValueChanged;
            controller.RightThumbstick.ValueChanged += RightThumbstick_ValueChanged;
        }
        private void RightThumbstick_ValueChanged(object sender, ValueChangeArgs<System.Numerics.Vector2> e)
        {
            ChangeTextBox(textBox1, $"Right Thumbstick : {e.Value.Length()}");
        }
        private void LeftThumbstick_ValueChanged(object sender, ValueChangeArgs<System.Numerics.Vector2> e)
        {
            ChangeTextBox(textBox1, $"Left Thumbstick : {e.Value.Length()}");
        }
        private void Connection_ValueChanged(object sender, ValueChangeArgs<bool> e)
        {
            if (e.Value)
                ChangeTextBox(textBox1, "Connected");
            else
                ChangeTextBox(textBox1, "Not Connected");
        }
        void ChangeTextBox(TextBox textBox, string txt)
        {
            BeginInvoke((MethodInvoker)delegate ()
            {
                textBox.Text = txt;
            });
        }
    }
}