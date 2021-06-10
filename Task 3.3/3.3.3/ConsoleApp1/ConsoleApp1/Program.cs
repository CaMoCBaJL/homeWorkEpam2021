using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Pizzeria p = new Pizzeria();

            p.NotifyUser += (u, msg) => { Console.WriteLine(msg); };

            User u1 = new User("Олежка");

            User u2 = new User("Ivan");


            //Emulation of working Pizzeria 
            p.MakeOrder(u1);

            p.MakeOrder(u2);

            Random r = new Random();

            while (!SomeLogic(r))
            {
                Thread.Sleep(300);
            }

            p.TakeOrder(u1);

            while (!SomeLogic(r))
            {
                Thread.Sleep(300);
            }

            p.TakeOrder(u2);


        }

        static bool SomeLogic(Random rand) => DateTime.Now.Millisecond % rand.Next(0, 100) == 0;
    }
}
