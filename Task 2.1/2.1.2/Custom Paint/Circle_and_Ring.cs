using System;
using System.Collections.Generic;
using System.Text;

namespace Custom_Paint
{
    class Circle_and_Ring : Figure
    {
        private class Circle
        {
            double radius;
            Point center;

            public double R
            {
                set
                {
                    radius = value;
                }
                get
                {
                    return radius;
                }
            }

            public Point Center
            {
                get
                {
                    return center;
                }
            }

            public Circle(double radius, Point center)
            {
                this.radius = radius;
                this.center = center;
            }

            public double LengthOfCircle()
            { return Math.PI * 2 * radius; }

            public double SquareOfCircle()
            { return Math.PI * Math.Pow(radius, 2); }

            public void Show()
            {
                Console.WriteLine("Окружность:");
                Console.WriteLine($"Радиус - {radius}");
                Console.WriteLine("Координаты центра - ");
                center.Show();
                Console.WriteLine($"Длина окружности - {LengthOfCircle()}");
                Console.WriteLine($"Площадь круга - {SquareOfCircle()}");

            }
        }
        (Circle c1, Circle c2) ring;

        public Circle_and_Ring(double radius, Point center)
        {
            ring.c1 = new Circle(radius, center);
            ring.c2 = null;
        }

        public Circle_and_Ring(double radius, Point center, double radius1, Point center1)
        {
            ring.c1 = new Circle(radius, center);
            ring.c2 = new Circle(radius1, center1);

        }

        public (double radius, Point Center) GetCircle()
        {
            return (ring.c1.R, ring.c1.Center);
        }

        public (double radius, Point center, double radius1, Point center1) GetRing()
        {
            if (ring.c2 != null)
                return (ring.c1.R, ring.c1.Center, ring.c2.R, ring.c2.Center);
            else throw new Exception();
        }

        public override Type GetType()
        {
            return typeof(Circle_and_Ring);
        }

        public static bool IsRing(Circle_and_Ring c)
        {
            return c.ring.c2 != null;
        }

        public double Length()
        {
            if (ring.c2 == null)
                return ring.c1.LengthOfCircle();
            return ring.c1.LengthOfCircle() + ring.c2.LengthOfCircle();
        }

        public double Square()
        {
            if (ring.c2 == null)
                return ring.c1.SquareOfCircle();
            return ring.c1.SquareOfCircle() - ring.c2.SquareOfCircle();
        }

        public override void Show()
        {
            if (ring.c2 == null)
                ring.c1.Show();
            else
            {
                Console.WriteLine("Кольцо.");
                Console.WriteLine("Внешняя окружность:");
                ring.c1.Show();
                Console.WriteLine("Внутренняя окружность:");
                ring.c2.Show();
            }
        }
    }
}
