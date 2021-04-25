using System;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace MyGit
{
    static class MySVC
    {
        /// <summary>
        /// Time to cut off changes made beyond prog's work. 
        /// </summary>
        private static DateTime _startTime = DateTime.Now;

        /// <summary>
        /// Working directory.
        /// </summary>
        private static string _workDirPath = "./CurDir";

        /// <summary>
        /// Location of commits.
        /// </summary>
        private static string _commitDirPath = Path.Combine(_workDirPath + Path.DirectorySeparatorChar, "Commits");

        private static string _summaryFile = Path.Combine(_commitDirPath, "Summary.txt");

        /// <summary>
        /// Word to stop work in the Viewer mode.
        /// </summary>
        public static string _stopWord = "/stop/";


        public static void InitializeMySVC(int mode)
        {
            WorkDirExists();

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

            CommitChanges();
        }

        private static void WorkDirExists()
        {
            FileStream fS = null;

            try
            {
                if (!Directory.Exists(_workDirPath))
                {
                    Directory.CreateDirectory(_workDirPath);

                    var commitsDir = new DirectoryInfo(_commitDirPath);

                    commitsDir.Create();

                    commitsDir.Attributes = FileAttributes.Hidden | FileAttributes.Directory;

                    fS = new FileStream(Path.Combine(commitsDir.FullName, "initialCommit.txt"), FileMode.Create);

                    fS.Close();

                    fS = new FileStream(_summaryFile, FileMode.Create);

                    fS.Close();                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to create working directory. See:" + Environment.NewLine + ex.Message);
            }
            finally
            {
                if (fS != null)
                    fS.Close();

                GC.Collect();
            }
        }

        private static void Rollback(int commitNum, List<string> commits)
        {
            List<TheChange> userCommits = new List<TheChange>();

            for (int i = 0; i < commitNum; i++)
            {
                var str = File.ReadAllText(commits[i]);

                var commit = JsonConvert.DeserializeObject<List<TheChange>>(str);

                if (commit != null)
                    MakeChanges(commit);
                else
                {
                    foreach (var item in Directory.GetFiles(_workDirPath))
                    {
                        File.Delete(item);
                    }

                    foreach (var item in Directory.GetDirectories(_workDirPath))
                    {
                        if (!item.Contains("Commits"))
                            Directory.Delete(item, true);
                    }
                }
                
            }

            File.WriteAllText(_summaryFile,
                JsonConvert.SerializeObject(GetAllFilesFromDirectories(_workDirPath)));

        }

        private static void MakeChanges(List<TheChange> commit)
        {
            try
            {
                foreach (var item in commit)
                {
                    switch (item.ChangeType)
                    {
                        case DifType.Added_Text:
                            File.WriteAllText(item.FileName, 
                                File.ReadAllText(item.FileName) + item.NewContent);
                            break;
                        case DifType.Changed_Text:
                            File.WriteAllText(item.FileName,
                                File.ReadAllText(item.FileName)
                                .Replace(item.NewContent.Split("->")[0], item.NewContent.Split("->")[1]));
                            break;
                        case DifType.Removed_Text:
                            File.WriteAllText(item.FileName,
                                File.ReadAllText(item.FileName)
                                .Replace(item.NewContent, string.Empty));
                            break;
                        case DifType.Added_File:

                            StringBuilder newFolder = new StringBuilder();

                            foreach (var pathPart in item.FileName.Split("\\"))
                            {
                                if (pathPart.Contains(".txt"))
                                    File.WriteAllText(item.FileName, item.NewContent);
                                else
                                {
                                    newFolder.Append(pathPart + Path.DirectorySeparatorChar);

                                    if (!Directory.Exists(newFolder.ToString()))
                                        Directory.CreateDirectory(newFolder.ToString());
                                }
                            }
                            break;
                        case DifType.Removed_File:
                            File.Delete(item.FileName);
                            break;
                        case DifType.None:
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private static void CommitChanges()
        {
            var str1 = File.ReadAllText(_summaryFile);

            List<MySVCFile> lastCommitCond = JsonConvert.DeserializeObject<List<MySVCFile>>(File.ReadAllText(_summaryFile));

            List<MySVCFile> currentCond = GetAllFilesFromDirectories(_workDirPath);

            List<TheChange> changes = new List<TheChange>();

            CompareFiles(currentCond, lastCommitCond, ref changes);

            File.WriteAllText(Path.Combine(_commitDirPath, DateTime.Now.ToString().Replace(':', '_') + ".txt"), 
                JsonConvert.SerializeObject(changes));

            File.WriteAllText(_summaryFile, JsonConvert.SerializeObject(currentCond));

            Console.WriteLine();

            ShowChanges(changes);
        }

        private static void ShowChanges(List<TheChange> changes) => changes.ForEach((item) => { Console.WriteLine(item.ToString()); });

        private static List<MySVCFile> GetAllFilesFromDirectories(string pathToDir)
        {
            var result = new List<MySVCFile>();

            foreach (var item in Directory.GetDirectories(pathToDir))
            {
                if(!item.Contains("Commits"))
                    result.AddRange(GetAllFilesFromDirectories(item));
            }

            foreach (var pathToFile in Directory.GetFiles(pathToDir))
            {
                result.Add(new MySVCFile()
                {
                    FileName = pathToFile,

                    Text = File.ReadAllText(pathToFile)
                });

            }

            return result;
        }

        private static void CompareFiles(List<MySVCFile> curDirCond, List<MySVCFile> prevDirCond, ref List<TheChange> changes)
        {
            foreach (var item in curDirCond)
            {
                if (File.GetLastAccessTime(item.FileName) > _startTime) //Only files, modified after the program started will be remembered.
                {
                    int index;

                    if (prevDirCond != null)
                    {
                        index = prevDirCond.FindIndex(file => file.FileName == item.FileName);
                    }
                    else
                        index = -1;

                    if (index > -1)
                    {
                        List<TheChange> localChanges = localChanges = CompareTwoFiles(
                        prevDirCond[index].Text, item.Text, item.FileName);

                        if (localChanges.Count > 0)
                            changes.AddRange(localChanges);
                    }
                    else
                    {
                        changes.Add(new TheChange(item.FileName, DifType.Added_File, item.Text));
                    }
                }
            }
            if (prevDirCond != null)
            {
                foreach (var file in prevDirCond)
                {
                    if (curDirCond.FindIndex(oldFile => oldFile.FileName == file.FileName) < 0)
                        changes.Add(new TheChange(file.FileName, DifType.Removed_File, file.Text));
                }
            }

        }

        public static List<TheChange> CompareTwoFiles(string str1, string str2, string fileName)
        {
            List<TheChange> result = new List<TheChange>();

            CompareCommonPartOfFiles(str1, str2, fileName, ref result);

            CompareDifferPartOfFiles(str1, str2, fileName, ref result);

            return result;
        }

        private static void CompareCommonPartOfFiles(string str1, string str2, string fileName, ref List<TheChange> result)
        {

            StringBuilder changedStr = new();

            int indxOfLastChangedSym = -1;

            for (int i = 0; i < GetTheSmallestLengthOfStrings(str1, str2); i++)
            {
                if (str1[i] != str2[i])
                {
                    if (indxOfLastChangedSym > 0)
                    {
                        if (indxOfLastChangedSym + 1 == i)
                        {
                            changedStr.Append(str1[i]);

                            indxOfLastChangedSym++;
                        }
                        else
                        {
                                result.Add(new TheChange(fileName, DifType.Changed_Text,
                                    changedStr.Append($" -> {str2.Substring(str1.IndexOf(changedStr.ToString()), changedStr.Length)}").ToString()));

                            changedStr = new StringBuilder();
                        }
                    }
                    else
                    {
                        changedStr.Append(str1[i]);

                        indxOfLastChangedSym++;
                    }
                }
            }

            if (changedStr.Length > 0)
                result.Add(new TheChange(fileName, DifType.Changed_Text, 
                    changedStr.Append($" -> {str2.Substring(str1.IndexOf(changedStr.ToString()), changedStr.Length)}").ToString()));
        }

        public static int GetTheSmallestLengthOfStrings(string str1, string str2)
        {
            if (str1.Length > str2.Length)
                return str2.Length;
            if (str2.Length > str1.Length)
                return str1.Length;
            else
                return str1.Length;
        }

        private static void CompareDifferPartOfFiles(string str1, string str2, string fileName, ref List<TheChange> result)
        {
            if (str2.Length > str1.Length)
                result.Add(new TheChange(fileName, DifType.Added_Text, str2.Substring(
                        str1.Length, str2.Length - str1.Length)));
            else if (str1.Length > str2.Length)
                result.Add(new TheChange(fileName, DifType.Removed_Text, str1.Substring(
                        str2.Length, str1.Length - str2.Length)));
        }

        private static void ShowAllCommits(List<string> commits)
        {
            if (Directory.Exists(_commitDirPath))
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
            List<string> commits = Directory.GetFiles(_commitDirPath).ToList();

            commits.Insert(0, commits[commits.Count - 2]);

            commits.RemoveAt(commits.Count - 2);

            commits.Remove(commits.Last());

            do
            {
                ShowAllCommits(commits);

                if (int.TryParse(Console.ReadLine(), out int item))
                {
                    if (item < Directory.GetFiles(_commitDirPath).Length && item > 0)
                    {
                        Rollback(item, commits);
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
