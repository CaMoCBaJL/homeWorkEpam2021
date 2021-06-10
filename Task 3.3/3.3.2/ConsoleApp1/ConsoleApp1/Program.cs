using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку:");

            string str = Console.ReadLine();

            foreach (var s in str.Split())
                Console.WriteLine($"{s} - строка типа {s.CheckLanguage() + Environment.NewLine}");

        }
    }
}
