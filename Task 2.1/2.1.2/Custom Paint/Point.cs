using System;
using System.Collections.Generic;
using System.Text;

namespace Custom_Paint
{
    class Point
    {
        double x;
        double y;
        public Point()
        {
            x = 0;
            y = 0;
        }
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

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
                p = new Point(double.Parse(s.Split(';')[0]), double.Parse(s.Split(';')[1]));
                return true;
            }
            else
            {
                p = null;
                return false;
            }
        }
        public void Show()
        {
            Console.WriteLine( $"({x}, {y})");
        }
    }
}
