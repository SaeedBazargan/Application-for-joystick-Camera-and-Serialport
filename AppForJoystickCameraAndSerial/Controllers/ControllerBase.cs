namespace AppForJoystickCameraAndSerial.Controllers
{
    public abstract class ControllerBase
    {
        private static PaintEventArgs e;

        protected static void ChangeTextBox(TextBox textBox, string txt)
        {
            textBox.BeginInvoke((MethodInvoker)delegate ()
            {
                textBox.Text = txt;
            });
        }

        protected static void ChangePictureBox(PictureBox pictureBox, Bitmap image)
        {
            pictureBox.BeginInvoke((MethodInvoker)delegate ()
            {
                if (pictureBox.Image != null) 
                    pictureBox.Image.Dispose();
                pictureBox.Image = image;
            });
        }

        protected static void ChangeLabel(Label label, Color color)
        {
            label.BeginInvoke((MethodInvoker)delegate ()
            {
                label.ForeColor = color;
            });
        }
    }
}
