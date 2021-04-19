using System;
using System.Linq;
using System.Text;
using System.IO;

namespace MyGit
{
    static class MySVC
    {
        private static readonly string _fileIdentifier = "<<File>>";

        private static readonly string _dirIdentifier = "<<Directory>>";

        private static readonly string _summaryFile = Path.Combine(_commitDirPath, "Summary.txt");

        /// <summary>
        /// Working directory.
        /// </summary>
        private static readonly string _workDirPath = "./CurDir";

        /// <summary>
        /// Location of commits.
        /// </summary>
        private static readonly string _commitDirPath = Path.Combine(_workDirPath + Path.DirectorySeparatorChar, "Commits");

        /// <summary>
        /// Word to stop work in the Viewer mode.
        /// </summary>
        private static readonly string _stopWord = "/stop/";


        public static void InitializeMySVC(int mode)
        {

            switch (mode)
            {
                case 1:
                    Console.WriteLine("Viewer mode enabled.");

                    Console.WriteLine($"To commit changes, write {_stopWord}.");

                    do
                    {

                    } while (Console.ReadLine() != _stopWord);
                    break;

                case 2:
                    Console.WriteLine("Rollback mode enabled." + Environment.NewLine);
                    ChooseTimestampToRollback();
                    break;
            }

            try
            {
                if (!Directory.Exists(_workDirPath))
                {
                    Directory.CreateDirectory(_workDirPath);

                    var commitsDir = new DirectoryInfo(_commitDirPath);

                    commitsDir.Create();

                    commitsDir.Attributes = FileAttributes.Hidden | FileAttributes.Directory;

                    File.Create(Path.Combine(commitsDir.FullName, "initialCommit.txt"));

                    File.Create(_summaryFile);
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

            CommitChanges();
        }

        private static void CommitChanges()
        {
            MySVCDir lastCommitCond = GetMyDirInfo(new DirectoryInfo(_workDirPath));

            MySVCDir currentCond = GetMyDirInfo(new DirectoryInfo(_workDirPath));

            //TODO: directories comparison
        }

        private static void Rollback(int commitNum)
        {
            
        }

        private static MySVCDir GetMyDirInfo(DirectoryInfo dir)
        {
            MySVCDir result = new MySVCDir() { DirName = dir.Name };

            foreach (var item in dir.GetDirectories())
            {
                result.AddDir(GetMyDirInfo(item));
            }

            foreach (var item in dir.GetFiles())
            {
                result.AddFile(item);
            }

            return result;
        }

        private static StringBuilder PrintDirInfo(DirectoryInfo dir)
        {
            StringBuilder result = new StringBuilder();

            foreach (var item in dir.GetDirectories())
            {
                result.Append($"{_dirIdentifier} {item.Name + Environment.NewLine + PrintDirInfo(item) + Environment.NewLine}");
            }

            foreach (var item in dir.GetFiles())
            {
                result.Append($"{_fileIdentifier} {item.Name + Environment.NewLine + File.ReadAllText(item.FullName) + Environment.NewLine}");
            }

            return result.Append(Environment.NewLine);

        }
        
        private static void ShowAllCommits()
        {
            var commits = new DirectoryInfo(_commitDirPath);

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
        
        public static void ChooseTimestampToRollback()
        {
            int item = -1;

            do
            {
                ShowAllCommits();

                if (item < Directory.GetFiles(_commitDirPath).Length && item > 0)
                    Rollback(item - 1);
                else
                    Console.WriteLine("Incorrect input!");
            } while (!int.TryParse(Console.ReadLine(), out item));
        }


    }
}
