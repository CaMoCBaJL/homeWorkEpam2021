using System;
using System.Collections.Generic;
using System.Text;

namespace Custom_Paint
{
    class Polygon : Figure
    {
        double perimetr;
        List<Line> lines;
        List<Point> points;
        FigureType type;

        public Polygon(params Point[] points)
        {
            if (points.Length < 3)
                Console.WriteLine("Для создания многоугольника нужно как минимум 3 точки!");
            else
            {
                this.points = new List<Point>();
                this.points.AddRange(points);
                type = FigureType();
            }
        }

        public override void Show()
        {
            Console.WriteLine(string.Format("Тип фигуры: " + FigureType()));
            Console.WriteLine("Точки:");
            foreach (var item in points)
            {
                item.Show();
            }
            Console.WriteLine($"Площадь фигуры - {Square()}");
            Console.WriteLine($"Периметр - {perimetr}");
        }

        public List<Point> GetPoints()
        {
            return points;
        }

        public double Square()
        {
            switch (type)
            {
                case Custom_Paint.FigureType.Triangle:
                    return Math.Sqrt((perimetr / 2)*((perimetr / 2) - lines[0].Length()) * ((perimetr / 2) - lines[1].Length()) * ((perimetr / 2) - lines[2].Length()));
                case Custom_Paint.FigureType.Square:
                    return Math.Pow(lines[0].Length(), 2);
                case Custom_Paint.FigureType.Rectangle:
                    return lines[0].Length() * lines[1].Length();
                case Custom_Paint.FigureType.Romb:
                    return Line.Length(points[0], points[2]) * Line.Length(points[1], points[3]) * 0.5;
                default: return double.NaN;
            }

        }

        public override Type GetType()
        {
            return typeof(Polygon);
        }

        FigureType FigureType()
        {
            lines = new List<Line>();
            for (int i = 1; i <= points.Count; i++)
            {
                if (i == points.Count)
                    lines.Add(new Line(points[i - 1], points[0]));
                else
                    lines.Add(new Line(points[i], points[i - 1]));
                perimetr += lines[i - 1].Length();
            }

            switch (points.Count)
            {
                case 3:
                    return Custom_Paint.FigureType.Triangle;
                case 4:
                    if (Line.IsPerpendicular(lines[0], lines[1]))
                    {
                        if (Line.IsParallel(lines[0], lines[2]) && Line.IsParallel(lines[1], lines[3]))
                        {
                            if (lines[0].Length() == lines[2].Length() && lines[1].Length() == lines[3].Length() && lines[1].Length() == lines[3].Length())
                                return Custom_Paint.FigureType.Square;
                            else
                                return Custom_Paint.FigureType.Rectangle;
                        }
                        else return Custom_Paint.FigureType.Polygon;
                    }
                    else
                    {
                        if (Line.IsParallel(lines[0], lines[2]) && Line.IsParallel(lines[1], lines[3]) &&
                       lines[0].Length() == lines[2].Length() && lines[1].Length() == lines[3].Length() && lines[1].Length() == lines[3].Length())
                            return Custom_Paint.FigureType.Romb;
                        else
                            return Custom_Paint.FigureType.Polygon;
                    }
                default:
                    return Custom_Paint.FigureType.N_angled_figure;
            }
        }

    }
}
