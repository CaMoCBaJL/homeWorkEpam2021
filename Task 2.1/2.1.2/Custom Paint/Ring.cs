using System;
using System.Collections.Generic;
using System.Text;

namespace Custom_Paint
{
    class Ring : Figure
    {
        (Circle innerCircle, Circle outerCircle) ring;

        public Ring(Circle innerCircle, Circle outerCircle)
        {
            ring.innerCircle = innerCircle;
            ring.outerCircle = outerCircle;
        }

        public Ring(double radius, Point center)
        {
            ring.innerCircle = new Circle(radius, center);
            ring.outerCircle = null;
        }

        public Ring(double radius, Point center, double radius1, Point center1)
        {
            ring.innerCircle = new Circle(radius, center);
            ring.outerCircle = new Circle(radius1, center1);

        }

        public Circle InnerCircle
        {
            get => ring.innerCircle;
        }

        public Circle OuterCicle
        {
            get => ring.outerCircle;
        }

        public double LengthOfCircles
        {
            get => ring.innerCircle.LengthOfCircle + ring.outerCircle.LengthOfCircle;
        }

        public double AreaOfCircles
        {
            get => ring.innerCircle.AreaOfCircle - ring.outerCircle.AreaOfCircle;
        }

        public override void Show()
        {
            if (ring.outerCircle == null)
                ring.innerCircle.Show();
            else
            {
                Console.WriteLine("Кольцо.");
                Console.WriteLine("Внешняя окружность:");
                ring.innerCircle.Show();
                Console.WriteLine("Внутренняя окружность:");
                ring.outerCircle.Show();
            }
        }
    }
}
