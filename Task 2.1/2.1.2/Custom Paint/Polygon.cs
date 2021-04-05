using System;
using System.Collections.Generic;
using System.Linq;

namespace Custom_Paint
{
    class Polygon : Figure
    {
        double perimetr;

        List<Line> lines;

        List<Point> points;


        public Polygon(params Point[] points)
        {
            if (points.Length < 3)
                throw new Exception("Для создания многоугольника нужно как минимум 3 точки!");
            else
            {
                this.points = new List<Point>();
                this.points.AddRange(points);
                Type = MatchFigureType();
            }
        }

        public override void Show()
        {
            Console.WriteLine(string.Format("Тип фигуры: " + Type));
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
            Point[] figurePoints = new Point[points.Count];
            Array.Copy(points.ToArray(), figurePoints, points.Count);
            points.CopyTo(figurePoints, 0);
            return figurePoints.Cast<Point>().ToList();
        }

        public FigureType Type { get; set; }

        public double Square()
        {
            switch (Type)
            {
                case FigureType.Triangle:
                    return Math.Sqrt((perimetr / 2)*((perimetr / 2) - lines[0].Length) * ((perimetr / 2) - lines[1].Length) * ((perimetr / 2) - lines[2].Length));

                case FigureType.Square:
                    return Math.Pow(lines[0].Length, 2);

                case FigureType.Rectangle:
                    return lines[0].Length * lines[1].Length;

                case FigureType.Romb:
                    return Line.DistanceBetweenPoints(points[0], points[2]) * Line.DistanceBetweenPoints(points[1], points[3]) * 0.5;

                default: return double.NaN;
            }

        }

        FigureType MatchFigureType()
        {
            lines = new List<Line>();
            for (int i = 1; i <= points.Count; i++)
            {
                if (i == points.Count)
                    lines.Add(new Line(points[i - 1], points[0]));
                else
                    lines.Add(new Line(points[i], points[i - 1]));
                perimetr += lines[i - 1].Length;
            }

            switch (points.Count)
            {
                case 3:
                    return FigureType.Triangle;
                case 4:
                    if (Line.IsPerpendicular(lines[0], lines[1]))
                    {
                        if (Line.IsParallel(lines[0], lines[2]) && Line.IsParallel(lines[1], lines[3]))
                        {
                            if (lines[0].Length == lines[2].Length && 
                                lines[1].Length == lines[3].Length && 
                                lines[1].Length == lines[3].Length)
                                return FigureType.Square;
                            else
                                return FigureType.Rectangle;
                        }
                        else return FigureType.Polygon;
                    }
                    else
                    {
                        if (Line.IsParallel(lines[0], lines[2]) && 
                            Line.IsParallel(lines[1], lines[3]) &&
                            lines[0].Length == lines[2].Length &&
                            lines[1].Length == lines[3].Length && 
                            lines[1].Length == lines[3].Length)
                            return FigureType.Romb;
                        else
                            return FigureType.Polygon;
                    }
                default:
                    return FigureType.N_angled_figure;
            }
        }

    }
}
