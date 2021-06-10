using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom_Paint
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя пользователя:");
            string name = Console.ReadLine();
            List<Figure> curUserFigures;
            while (true)
            {
                curUserFigures = DoesUserExists(name);
                ShowStartMenu();
                try
                {
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            ChooseFigureToAdd();
                            try
                            {
                                switch (int.Parse(Console.ReadLine()))
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                        curUserFigures.Add(InputPolygon());
                                        break;
                                    case 4:
                                        curUserFigures.Add(InputCircle());
                                        break;
                                    case 5:
                                        curUserFigures.Add(InputRing());
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
                            ShowUserFigures(curUserFigures);
                            break;
                        case 3:
                            curUserFigures = new List<Figure>();
                            break;
                        case 4:
                            ChangeUser();
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

        static string ChangeUser()
        {
            Console.Clear();
            Console.WriteLine("Введите имя пользователя");
            return Console.ReadLine();
        }

        static void ShowUserFigures(List<Figure> curUserFigures)
        {
            if (curUserFigures.Count > 0)
                foreach (var item in curUserFigures)
                {
                    item.Show();
                    Console.WriteLine();
                }
            else
                Console.WriteLine("Холст пуст.");
        }

        static Ring InputRing()
        {
            return new Ring(InputCircle(), InputCircle());
        }

        static Circle InputCircle()
        {
            Console.WriteLine("Введите радиус окружности:");
            double r = 0;
            Point center;
            if (r <= 0)
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
            return new Circle(r, center);

        }

        static Polygon InputPolygon()
        {
            List<Point> points = new List<Point>();

            string s = default;

            while (s != "стоп")
            {

                Console.WriteLine("Введите координаты точки (через ;)");
                Console.WriteLine("Слово \"стоп\" - окончание ввода." + Environment.NewLine);

                s = Console.ReadLine();

                Point p;
                if (Point.TryParse(s, out p))
                {
                    points.Add(p);
                    Console.WriteLine("Ввод успешен)" + Environment.NewLine);
                }
                else
                    Console.WriteLine("Ввод неверен!" + Environment.NewLine);
            }

            if (points.Count < 3)
            {
                Console.WriteLine("Добавление фигуры не произошло! Для добавления угольника нужно минимум 3 точки!");
                return null;
            }
            else
                return new Polygon(points.ToArray());

        }

        static void ChooseFigureToAdd()
        {
            Console.WriteLine("Выберите фигуру для добавления:");
            Console.WriteLine("1. Треугольник");
            Console.WriteLine("2. Четырехугольник"); ;
            Console.WriteLine("3. N-угольник");
            Console.WriteLine("4. Окружность");
            Console.WriteLine("5. Кольцо");
        }

        static void ShowStartMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1.Добавить фигуру");
            Console.WriteLine("2.Вывести все фигуры");
            Console.WriteLine("3.Очистить холст");
            Console.WriteLine("4.Смена пользователя");
            Console.WriteLine("5.Выход");
        }

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
                                            curUserFigures.Add(new Ring(double.Parse(s[0]), Point.Parse(s[1])));
                                            break;
                                        }
                                        if (s.Length == 4)
                                        {
                                            curUserFigures.Add(new Ring(double.Parse(s[0]), Point.Parse(s[1]), double.Parse(s[2]), Point.Parse(s[3])));
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
                switch (item)
                    {
                    case Polygon poly:
                    foreach (var p in poly.GetPoints())
                    {
                        res.Append($"{p.X};{p.Y} ");
                    }
                    inf.Add(res.ToString());
                        break;
                    case Circle c:
                        inf.Add($"{c.R};{c.Center}");
                        break;
                    case Ring r:
                        inf.Add($"{r.InnerCircle.R} {r.InnerCircle.Center};{r.OuterCicle.R} {r.OuterCicle.Center}");
                        break;
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

    }
}
