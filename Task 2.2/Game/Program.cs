using System;
using System.Collections.Generic;
using System.IO;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> enemy1 = new List<string>();
            using(StreamReader fileIn = new StreamReader("enemy_1.txt"))
            {
                while(!fileIn.EndOfStream)
                enemy1.Add(fileIn.ReadLine());
            }
            foreach (var item in enemy1)
            {
                Console.WriteLine(item);
            }

            

            while (true)
            {
                Console.ForegroundColor = (ConsoleColor)new Random().Next(0,10);
                Console.WriteLine(Console.ReadKey().Key);
            }
        }
    }
}
