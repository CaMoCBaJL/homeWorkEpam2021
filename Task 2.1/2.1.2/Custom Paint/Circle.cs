using System;
using System.Collections.Generic;
using System.Text;

namespace Custom_Paint
{
    internal class Circle : Figure
    {
        public double R {get; set;}

        public Point Center { get; set; }

        public Circle(double radius, Point center)
        {
            R = radius;
            Center = center;
        }

        public double LengthOfCircle
        { get => Math.PI * 2 * R; }

        public double AreaOfCircle
        { get => Math.PI * Math.Pow(R, 2); }

        public override void Show()
        {
            Console.WriteLine("Окружность:");
            Console.WriteLine($"Радиус - {R}");
            Console.WriteLine("Координаты центра - ");
            Center.Show();
            Console.WriteLine($"Длина окружности - {LengthOfCircle}");
            Console.WriteLine($"Площадь круга - {AreaOfCircle}");

        }
    }
}
