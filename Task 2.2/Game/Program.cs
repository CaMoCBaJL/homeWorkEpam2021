using System;
using System.Collections.Generic;
using System.IO;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine g = new GameEngine();
            Swamp s = new Swamp((14, 14), 5, 5);
            g.BuildField(20, 30, s, Wall.CreateVerticalWall(5, (5,0)), Wall.CreateHorizontalWall(5, (6, 5)));
            g.ShowField();

            while (true)
            {
                Console.ForegroundColor = (ConsoleColor)new Random().Next(0,10);
                string key =  Console.ReadKey().Key.ToString();
                if (key.Contains("Arrow"))
                    Console.WriteLine(key);
            }
        }
    }
}
