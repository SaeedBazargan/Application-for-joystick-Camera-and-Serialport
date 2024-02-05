namespace AppForJoystickCameraAndSerial.Controllers
{
    public abstract class ControllerBase
    {
        private static PaintEventArgs e;

        protected static void ChangeTextBox(TextBox textBox, string txt)
        {
            textBox.Invoke((MethodInvoker)delegate ()
            {
                textBox.Text = txt;
            });
        }

        public void ChangePictureBox(PictureBox pictureBox, Bitmap image)
        {
            pictureBox.Invoke((MethodInvoker)delegate ()
            {
                if (pictureBox.Image != null)
                    pictureBox.Image.Dispose();
                pictureBox.Image = image;
            });
        }

        protected static void ChangeLabel(Label label, Color color)
        {
            label.Invoke((MethodInvoker)delegate ()
            {
                label.ForeColor = color;
            });
        }

        protected static void HidePictureBox(PictureBox box)
        {
            box.Invoke((MethodInvoker)delegate ()
            {
                box.Hide();
            });
        }
    }
}
