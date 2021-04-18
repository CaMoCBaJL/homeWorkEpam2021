using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyGit
{
    static class MySVC
    {
        /// <summary>
        /// Working directory.
        /// </summary>
        private static readonly string dirPath = "./CurDir";

        /// <summary>
        /// Location of commits.
        /// </summary>
        private static readonly string commitDir = Path.Combine(dirPath + Path.DirectorySeparatorChar, "Commits");

        /// <summary>
        /// Word to stop work in the Viewer mode.
        /// </summary>
        private static readonly string stopWord = "/stop/";


        public static void InitializeMySVC(int mode)
        {

            switch (mode)
            {
                case 1:
                    Console.WriteLine("Viewer mode enabled.");

                    Console.WriteLine($"To commit changes, write {stopWord}.");

                    do
                    {

                    } while (Console.ReadLine() != stopWord);
                    break;

                case 2:
                    Console.WriteLine("Rollback mode enabled." + Environment.NewLine);
                    ChooseTimestampToRollback();
                    break;
            }

            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);

                    var commitsDir = new DirectoryInfo(commitDir);

                    commitsDir.Create();

                    commitsDir.Attributes = FileAttributes.Hidden | FileAttributes.Directory;

                    File.Create(Path.Combine(commitsDir.FullName, "initialCommit.txt"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to create working directory. See:" + Environment.NewLine + ex.Message);
            }
            finally
            {
                GC.Collect();
            }

            CloseMySVC();
        }

        public static void ChooseTimestampToRollback()
        {
            int item = -1;

            do
            {
                ShowAllCommits();

                if (item < Directory.GetFiles(commitDir).Length && item > 0)
                    Rollback(item - 1);
                else
                    Console.WriteLine("Incorrect input!");
            } while (!int.TryParse(Console.ReadLine(), out item));
        }

        private static void ShowAllCommits()
        {
            var commits = new DirectoryInfo(commitDir);

            if (commits.Exists)
            {
                int counter = 1;

                foreach (var item in commits.GetFiles())
                {
                    Console.WriteLine($"{counter}.{item.Name}, {item.CreationTime}");

                    counter++;
                }
            }
        }

        private static void Rollback(int commitNum)
        {
            
        }

        private static void SeekForChanges()
        {
            ParseChanges();
        }

        private static void ParseChanges()
        {

        }

        public static void CloseMySVC()
        {
            SeekForChanges();
        }

    }
}
