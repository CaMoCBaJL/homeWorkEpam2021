using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace MyGit
{
    static class ListOfCommits
    {
        private static void ShowAllCommits(List<string> commits)
        {
            if (Directory.Exists(MySVC._commitDirPath))
            {
                int counter = 1;

                foreach (var item in commits)
                {

                    Console.WriteLine($"{counter}.{item.Split('\\').Last().Remove(item.Split('\\').Last().Length - 5, 4)}");

                    counter++;

                }
            }
            else
                Console.WriteLine("Ther are no commits at all.");
        }

        public static void ChooseTimestampToRollback()
        {
            List<string> commits = Directory.GetFiles(MySVC._commitDirPath).ToList();

            commits.Insert(0, commits[commits.Count - 2]);

            commits.RemoveAt(commits.Count - 2);

            commits.Remove(commits.Last());

            do
            {
                ShowAllCommits(commits);

                if (int.TryParse(Console.ReadLine(), out int item))
                {
                    if (item < Directory.GetFiles(MySVC._commitDirPath).Length && item > 0)
                    {


                        new Rollback(item, commits);
                        return;
                    }
                    else
                        Console.WriteLine("Incorrect input!");
                }
                else
                    Console.WriteLine("Incorrect input!");

            } while (true);
        }
    }
}
