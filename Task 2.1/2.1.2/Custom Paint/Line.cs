using System;
using System.Collections.Generic;
using System.Text;

namespace Custom_Paint
{
    class Line
    {
        Point A;
        Point B;
        public Line(Point A, Point B)
        {
            this.A = A;
            this.B = B;
        }
        public Line()
        {
            A = new Point();
            B = new Point();
        }
        public Point Point1
        {
            get
            {
                return A;
            }
            set
            {
                A = value;
            }
        }
        public Point Point2 
        {
            get
            {
                return B;
            }
            set
            {
                B = value;
            }
        }


        private class Vector
        {
            public double x;
            public double y;

            public Vector(Point A, Point B)
            {
                x = B.X - A.X;
                y = B.Y - A.Y;
            }

            public Vector(Line l)
            {
                x = l.Point2.X - l.Point1.X;
                y = x = l.Point2.Y - l.Point1.Y;
            }

            public static double ScalarMultiplication(Point A, Point B, Point A1, Point B1)
            {
                Vector v = new Vector(A, B);
                Vector v1 = new Vector(A1, B1);
                return v.x * v1.x + v.y * v1.y;
            }

            public static double ScalarMultiplication(Line l1, Line l2)
            {
                Vector v = new Vector(l1);
                Vector v1 = new Vector(l2);
                return v.x * v1.x + v.y * v1.y;
            }
        }


        public static bool IsPerpendicular(Point A, Point B, Point A1, Point B1)
        {
            return Vector.ScalarMultiplication(A, B, A1, B1) == 0;
        }

        public static bool IsPerpendicular(Line l1, Line l2)
        {
            return Vector.ScalarMultiplication(l1, l2) == 0;
        }

        public double Length()
        {
            return Math.Sqrt(Math.Pow((B.X) - (A.X), 2) + Math.Pow((B.Y) - (A.Y), 2));
        }

        public static double Length(Point A, Point B)
        {
            return Math.Sqrt(Math.Pow((B.X) - (A.X), 2) + Math.Pow((B.Y) - (A.Y), 2)); 
        }

        public static bool IsParallel(Line l1, Line l2)
        {
           return l1.B.Y - l1.A.Y == l2.B.Y - l2.B.Y;
        }

        public void ShowLine()
        {
            Console.WriteLine("Линия :");
            Console.WriteLine($" A({A.X},{A.Y}) B({B.X},{B.Y})");
            Console.WriteLine("Длина - " + Length());
        }
    }
}
