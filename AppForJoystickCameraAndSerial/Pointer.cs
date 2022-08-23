namespace AppForJoystickCameraAndSerial
{
    public class Pointer
    {
        public static Pointer JoyPointer = new Pointer();

        public PointF Center { get; set; }
        public Size ContainerSize { get; set; }
        public int Radius { get; set; }
        public int[] Cursor => new int[] { (int)Center.X, (int)Center.Y };
        public Color Color { get; set; } = Color.Blue;
        public PointF[] LinePoints => new PointF[]
        {
            new PointF(Center.X, Center.Y - Radius),
            new PointF(Center.X, Center.Y + Radius),

            new PointF(Center.X + Radius, Center.Y),
            new PointF(Center.X - Radius, Center.Y)
        };

        public Pointer(PointF center = default, int radius = 10)
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
            //Console.WriteLine("xxxx = " + v.X);
            //Console.WriteLine("yyyy = " + v.Y);

            var center = new PointF(Center.X + v.X, Center.Y - v.Y);
            Center = new PointF(((ContainerSize.Width / 2) * v.X) + (ContainerSize.Width / 2), ((ContainerSize.Height / 2) * -v.Y) + (ContainerSize.Height / 2));

            if (v.LengthSquared() < 0.05)
                Center = new PointF(ContainerSize.Width / 2, ContainerSize.Height / 2);
        }
        public void MoveMouse(System.Numerics.Vector2 v)
        {
            //Console.WriteLine("xxxx = " + v.X);
            //Console.WriteLine("yyyy = " + v.Y);

            var center = new PointF(v.X, v.Y);
            Center = new PointF(v.X, v.Y);
        }
        public void MoveUSBJoystick(System.Numerics.Vector2 v)
        {
            //Console.WriteLine("xxxx = " + v.X);
            //Console.WriteLine("yyyy = " + v.Y);

            var center = new PointF(v.X, v.Y);
            Center = new PointF(v.X, v.Y);
        }
    }
}
