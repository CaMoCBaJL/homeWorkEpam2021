using System;
using System.Collections.Generic;
using System.Text;

namespace Custom_Paint
{
    class Line
    {
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

        public Point A { get; set; }

        public Point B { get; set; }

        public static bool IsPerpendicular(Point A, Point B, Point A1, Point B1)
        {
            return Vector.ScalarMultiplication(A, B, A1, B1) == 0;
        }

        public static bool IsPerpendicular(Line l1, Line l2)
        {
            return Vector.ScalarMultiplication(l1, l2) == 0;
        }

        public double Length
        {
            get => Math.Sqrt(Math.Pow((B.X) - (A.X), 2) + Math.Pow((B.Y) - (A.Y), 2));
        }

        public static double DistanceBetweenPoints(Point A, Point B)
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
            Console.WriteLine("Длина - " + Length);
        }
    }
}
