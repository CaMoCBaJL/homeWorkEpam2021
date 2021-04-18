using System;
using System.Collections.Generic;
using System.Text;

namespace MyGit
{
    static class Menu
    {
        public static void ChooseMode()
        {
            Console.WriteLine($"Welcome back, {Environment.UserName}!");

            int mode = -1;

            while(true)
            {
                Console.WriteLine("Choose mode:");

                Console.WriteLine("1. Viewer mode.");

                Console.WriteLine("2. Rollback mode.");


                if (int.TryParse(Console.ReadLine(), out mode))
                {
                    switch (mode)
                    {
                        case 1:
                        case 2:
                            MySVC.InitializeMySVC(mode);
                            return;

                        default:
                            Console.WriteLine("Incorrect input!");
                            break;
                    }
                }
                else
                    Console.WriteLine("Incorrect input!");

                Console.WriteLine();
            }
        }
    }
}
