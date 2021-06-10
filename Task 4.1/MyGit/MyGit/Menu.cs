using System;

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

                Console.WriteLine("3. End of work.");


                if (int.TryParse(Console.ReadLine(), out mode))
                {
                    MySVC git = new MySVC();

                    switch (mode)
                    {

                        case 1:
                            Console.WriteLine("Viewer mode enabled.");

                            Console.WriteLine($"To commit changes, write {MySVC._stopWord}.");

                            git.InitializeMySVC(WorkType.Viewer);
                            break;

                        case 2:
                            Console.WriteLine("Rollback mode enabled." + Environment.NewLine);

                            git.InitializeMySVC(WorkType.Rollback);
                            break;

                        case 3:
                            git.InitializeMySVC(WorkType.None);
                            break;

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
