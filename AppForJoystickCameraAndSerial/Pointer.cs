using System.Windows.Forms;

namespace AppForJoystickCameraAndSerial
{
    public class Pointer
    {
        public static Pointer JoyPointer = new Pointer();

        public PointF Center { get; set; }
        public PointF CenterNorm { get; set; }
        public Size ContainerSize { get; set; }
        public int Radius { get; set; }
        public int[] Cursor => new int[] { (int)(CenterNorm.X * 640 + 640 / 2), (int)(CenterNorm.Y * 480 + 480 / 2) };
        public Color Color { get; set; } = Color.Blue;
        public PointF[] LinePoints => new PointF[]
        {
            new PointF(Center.X, Center.Y - Radius),
            new PointF(Center.X, Center.Y + Radius),

            new PointF(Center.X + Radius, Center.Y),
            new PointF(Center.X - Radius, Center.Y)
        };

        public Pointer(PointF center = default, int radius = 20)
        {
            Center = center;
            Radius = radius;
        }

        public void SetContainerSize(Size size)
        {
            ContainerSize = size;
            Center = new PointF(size.Width / 2, size.Height / 2);
        }

        public void MoveJoystick(System.Numerics.Vector2 v)
        {
            Center = new PointF(((ContainerSize.Width / 2) * v.X) + (ContainerSize.Width / 2), ((ContainerSize.Height / 2) * -v.Y) + (ContainerSize.Height / 2));

            if (v.LengthSquared() < 0.05)
                Center = new PointF(ContainerSize.Width / 2, ContainerSize.Height / 2);
        }
        public void MoveMouse(System.Numerics.Vector2 v)
        {
            Center = new PointF(v.X, v.Y);
        }
        public void MoveUSBJoystick(System.Numerics.Vector2 v)
        {
            const int joystick_zero_value_x = 32758;
            const int joystick_zero_value_y = 32758;
            float normX = (v.X - joystick_zero_value_x) / 65535;
            float normY = (v.Y - joystick_zero_value_y) / 65535;
            CenterNorm = new PointF(normX, normY);

            //Console.WriteLine("({0} , {1})", v.X, v.Y);
            Center = new PointF(normX * ContainerSize.Width + ContainerSize.Width / 2, normY * ContainerSize.Height + ContainerSize.Height / 2);
        }
    }
}
