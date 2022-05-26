namespace AppForJoystickCameraAndSerial.Controllers
{
    public abstract class ControllerBase
    {
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

        protected static void ChangeLable(Label lable, Color color)
        {
            lable.BeginInvoke((MethodInvoker)delegate ()
            {
                lable.ForeColor = color;
            });
        }
    }
}
