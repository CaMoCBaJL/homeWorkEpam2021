

namespace Custom_Paint
{
    class Vector
    {
        public double X { get; set; }

        public double Y { get; set; }

        public Vector(Point A, Point B)
        {
            X = B.X - A.X;
            Y = B.Y - A.Y;
        }

        public Vector(Line l)
        {
            X = l.B.X - l.A.X;
            Y = l.B.Y - l.A.Y;
        }

        public static double ScalarMultiplication(Point A, Point B, Point A1, Point B1)
        {
            Vector v = new Vector(A, B);
            Vector v1 = new Vector(A1, B1);
            return v.X * v1.X + v.Y * v1.Y;
        }

        public static double ScalarMultiplication(Line l1, Line l2)
        {
            Vector v = new Vector(l1);
            Vector v1 = new Vector(l2);
            return v.X * v1.X + v.Y * v1.Y;
        }

    }
}
