using System;
using System.Collections.Generic;
using System.Text;

namespace Custom_Paint
{
    class Point
    {

        public Point()
        {
            X = 0;
            Y = 0;
        }
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public static Point Parse(string s)
        {
            if (s.Contains(';'))
            {
                return new Point(double.Parse(s.Split(';')[0]), double.Parse(s.Split(';')[1]));
            }
            throw new FormatException();
        }

        public static bool TryParse(string s, out Point p)
        {
            if (s.Contains(';'))
            {
                if (double.TryParse(s.Split(';')[0], out double x) && double.TryParse(s.Split(';')[1], out double y))
                {
                    p = new Point(x, y);
                    return true;
                }
            }

            p = null;
            return false;
        }

        public void Show()
        {
            Console.WriteLine( $"({X}, {Y})");
        }
    }
}
