using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Validator()
        {
            Console.WriteLine("Введите строку:");
            List<string> str = new List<string>(Console.ReadLine().Split());
            str[0] = str[0].Replace(str[0][0], char.ToUpper(str[0][0]));
            for (int i = 0; i < str.Count; i++)
            {
                if (str[i].Contains('.') || str[i].Contains('?') || str[i].Contains('!'))
                    if (i + 1 < str.Count)
                        str[i + 1] = str[i + 1].Replace(str[i + 1][0], char.ToUpper(str[i + 1][0]));
            }
            foreach (string item in str)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
        static void LowerCase1(bool hardTask)
        {
            Console.WriteLine("Введите строку:");
            int counter = 0;
            List<string> strings;
            if (hardTask)
            {
                strings = new List<string>(Console.ReadLine().Split(';', ':', ',', ' '));
                strings.RemoveAll(s => s == string.Empty);
            }
            else
                strings = new List<string>(Console.ReadLine().Split());
            foreach (string s in strings)
            {
                if (char.IsLower(s[0]))
                    counter++;
            }
            Console.WriteLine(counter);
        }
        static void StringNotSting() //Результат округляю.
        {
            Console.WriteLine("Введите строку:");
            int res = 0;
            int c = 0;
            new List<string>(Console.ReadLine().Split()).ForEach((str) => 
            {
                res += str.Length; c++; 
            });
            Console.WriteLine($"Средняя длина слова - {res / c}.");
        }
        static void Doubler()
        {
            Console.WriteLine("Введите первую строку:");
            StringBuilder s1 = new StringBuilder(Console.ReadLine());
            Console.WriteLine("Введите вторую строку:");
            string s2 = Console.ReadLine();
            for (int i = 0; i < s1.Length; i++)
            {
                if (s2.Contains(s1[i]))
                {
                    s1.Insert(i, s1[i]);
                    i++;
                }
            }
            Console.WriteLine("Результат удвоения символов: "+Environment.NewLine+s1);
        }
        static void Main(string[] args)
        {
            int res = 0;
            while (res != 6)
            {
                Console.WriteLine("Выберите метод для демонстрации: ");
                Console.WriteLine("1 - String Not Sting.");
                Console.WriteLine("2 - Doubler.");
                Console.WriteLine("3 - Lower Case.");
                Console.WriteLine("4 - Lower Case*.");
                Console.WriteLine("5 - Validator.");
                Console.WriteLine("6 - Выход.");
                if(int.TryParse(Console.ReadLine(), out res))
                    switch (res)
                    {
                        case 1:
                            StringNotSting();
                            break;
                        case 2:
                            Doubler();
                            break;
                        case 3:
                            LowerCase1(false); //входной параметр определяет, вызвается ли задача со *.
                            break;
                        case 4:
                            LowerCase1(true);
                            break;
                        case 5:
                            Validator();
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("Введенный вариант отсутсвует в меню." + Environment.NewLine);
                            break;
                    }
                else
                    Console.WriteLine("Ввод неверен!" + Environment.NewLine);
            }
        }
    }
}
