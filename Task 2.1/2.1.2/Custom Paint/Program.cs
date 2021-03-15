using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom_Paint
{
    class Program
    {

        static List<Figure> DoesUserExists(string name)
        {
            List<Figure> curUserFigures;
            if (new FileInfo("users_info.txt").Exists)
                using (StreamReader users = new StreamReader("users_info.txt"))
                {
                    string info = users.ReadToEnd();
                    if (info.Contains(name))
                    {
                        curUserFigures = new List<Figure>();
                        List<string> inf = info.Split(Environment.NewLine).Cast<string>().ToList();
                        inf.RemoveAll(s => s == string.Empty);
                        int i = inf.FindIndex(s => s.Contains(name)) + 1;
                        while (true)
                        {
                            if (i == inf.Count)
                                break;
                            if (inf[i].Contains("user"))
                                break;
                            else
                            {
                                string[] s = inf[i].TrimEnd().Split();
                                List<Point> points = new List<Point>();
                                for (int j = 0; j < s.Length; j++)
                                {
                                    Point p;
                                    if (!Point.TryParse(s[j], out p))
                                    {
                                        if (s.Length == 2)
                                        {
                                            curUserFigures.Add(new Circle_and_Ring(double.Parse(s[0]), Point.Parse(s[1])));
                                            break;
                                        }
                                        if (s.Length == 4)
                                        {
                                            curUserFigures.Add(new Circle_and_Ring(double.Parse(s[0]), Point.Parse(s[1]), double.Parse(s[2]), Point.Parse(s[3])));
                                            break;
                                        }

                                    }
                                    points.Add(p);
                                }
                                i++;
                                if (points.Count > 0)
                                    curUserFigures.Add(new Polygon(points.ToArray()));
                            }
                        }
                        return curUserFigures;
                    }
                    else
                      return new List<Figure>();
                }
            else
               return new List<Figure>();

        }

        static void SaveChanges(List<Figure> figures, string userName)
        {
            List<string> inf;
            using (StreamReader file = new StreamReader("users_info.txt"))
            {
                string fileInfo = file.ReadToEnd();
                inf = fileInfo.Split(Environment.NewLine).Cast<string>().ToList();
                inf.RemoveAll(s => s == string.Empty);
                if (fileInfo.Contains(userName))
                {
                    int i = inf.FindIndex(s => s.Contains(userName));
                    inf.RemoveAt(i);
                    while(true)
                    {
                        if (i == inf.Count)
                            break;
                        if (inf[i].Contains("user"))
                            break;
                        else
                            inf.RemoveAt(i);
                    }
                }

            }
            inf.Add($"user {userName}:");
            foreach (var item in figures)
            {
                StringBuilder res = new StringBuilder();
                if (typeof(Polygon) == item.GetType())
                {
                    foreach (var p in (item as Polygon).GetPoints())
                    {
                        res.Append($"{p.X};{p.Y} ");
                    }
                    inf.Add(res.ToString());
                }
                else
                {
                    Circle_and_Ring elem = item as Circle_and_Ring;
                    if (!Circle_and_Ring.IsRing(elem))
                    {
                        (double r, Point center) circle = elem.GetCircle();
                        inf.Add($"{circle.r} {circle.center.X};{circle.center.Y}");
                    }
                    else
                    {
                        (double r, Point center, double r1, Point center1) ring = elem.GetRing();
                        inf.Add($"{ring.r} {ring.center.X};{ring.center.Y} {ring.r1} {ring.center1.X};{ring.center1.Y}");
                    }
                }
            }
            using (StreamWriter newfile = new StreamWriter("users_info.txt"))
            {
                foreach (var item in inf)
                {
                    newfile.WriteLine(item);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя пользователя:");
            string name = Console.ReadLine();
            List<Figure> curUserFigures;
            while (true)
            {
                curUserFigures = DoesUserExists(name);
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1.Добавить фигуру");
                Console.WriteLine("2.Вывести все фигуры");
                Console.WriteLine("3.Очистить холст");
                Console.WriteLine("4.Смена пользователя");
                Console.WriteLine("5.Выход");
                try
                {
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            Console.WriteLine("Выберите фигуру для добавления:");
                            Console.WriteLine("1. Треугольник");
                            Console.WriteLine("2. Четырехугольник"); ;
                            Console.WriteLine("3. N-угольник");
                            Console.WriteLine("4. Окружность");
                            Console.WriteLine("5. Кольцо");
                            try
                            {
                                switch (int.Parse(Console.ReadLine()))
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                        List<Point> points = new List<Point>();
                                        Console.WriteLine("Введите координаты точки (через ;)");
                                        Console.WriteLine("Слово \"стоп\" - окончание ввода.");
                                        string s = Console.ReadLine();

                                        while (s != "стоп")
                                        {
                                            Point p;
                                            if (Point.TryParse(s, out p))
                                                points.Add(p);
                                            else
                                                Console.WriteLine("Ввод неверен!");

                                            Console.WriteLine("Введите координаты точки (через ;)");
                                            Console.WriteLine("Слово \"стоп\" - окончание ввода.");
                                            s = Console.ReadLine();
                                        }

                                        if (points.Count < 3)
                                            Console.WriteLine("Добавление фигуры не произошло! Для добавления угольника нужно минимум 3 точки!");
                                        else
                                            curUserFigures.Add(new Polygon(points.ToArray()));
                                        break;
                                    case 4:
                                        Console.WriteLine("Введите радиус окружности:");
                                        double r;
                                        Point center;
                                        while(!double.TryParse(Console.ReadLine(), out r))
                                        {
                                            Console.WriteLine("Радуис - неотрицательное число!");
                                            Console.WriteLine("Повторите ввод.");
                                        }
                                        if (r < 0)
                                        {
                                            while (!double.TryParse(Console.ReadLine(), out r))
                                            {
                                                Console.WriteLine("Радуис - неотрицательное число!");
                                                Console.WriteLine("Повторите ввод.");
                                            }
                                        }
                                        Console.WriteLine("Введите центр окружности:");
                                        while (!Point.TryParse(Console.ReadLine(), out center))
                                        {
                                            Console.WriteLine("Введите координаты точки (через ;)");
                                            Console.WriteLine("Повторите ввод.");
                                        }
                                        curUserFigures.Add(new Circle_and_Ring(r, center));
                                        break;
                                    case 5:
                                        double[] radiuses = new double[2];
                                        Point[] centers = new Point[2];
                                        for (int i = 0; i < 2; i++)
                                        {
                                            Console.WriteLine("Введите радиус окружности:");
                                            while (!double.TryParse(Console.ReadLine(), out radiuses[i]))
                                            {
                                                Console.WriteLine("Радуис - неотрицательное число!");
                                                Console.WriteLine("Повторите ввод.");
                                            }
                                            if (radiuses[i] < 0)
                                            {
                                                while (!double.TryParse(Console.ReadLine(), out radiuses[i]))
                                                {
                                                    Console.WriteLine("Радуис - неотрицательное число!");
                                                    Console.WriteLine("Повторите ввод.");
                                                }
                                            }
                                            Console.WriteLine("Введите центр окружности:");
                                            while (!Point.TryParse(Console.ReadLine(), out centers[i]))
                                            {
                                                Console.WriteLine("Введите координаты точки (через ;)");
                                                Console.WriteLine("Повторите ввод.");
                                            }
                                        }
                                        curUserFigures.Add(new Circle_and_Ring(radiuses[0], centers[0], radiuses[1], centers[1]));
                                        break;
                                    default:
                                        Console.WriteLine("Пункт с таким номером отсутствует в меню.");
                                        break;
                                }
                                SaveChanges(curUserFigures, name);

                            }
                            catch
                            {
                                Console.WriteLine("Ввод неверен!");
                            }
                            break;
                        case 2:
                            if (curUserFigures.Count > 0)
                            foreach (var item in curUserFigures)
                            {
                                item.Show();
                                Console.WriteLine();
                            }
                            else
                                Console.WriteLine("Холст пуст.");
                            break;
                        case 3:
                            curUserFigures = new List<Figure>(); 
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Введите имя пользователя");
                            name = Console.ReadLine();
                            break;
                        case 5:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Пункт с таким номером отсутствует в меню.");
                            break;
                    }
                        }
                catch
                {
                    Console.WriteLine("Ввод неверен!");
                }
            }
        }
    }
}
