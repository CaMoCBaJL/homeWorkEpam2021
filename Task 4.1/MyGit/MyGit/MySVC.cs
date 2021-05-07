using System;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace MyGit
{
    class MySVC
    {
        public event Action<string> mySVCIndicator;

        static MySVC()
        {
            _workDirPath = "./CurDir";

            _commitDirPath = Path.Combine(_workDirPath + Path.DirectorySeparatorChar, "Commits");

            _summaryFile = Path.Combine(_commitDirPath, "Summary.txt");
        }

        public MySVC()
        {
            mySVCIndicator += IndicateStart;

            try
            {
                mySVCIndicator?.Invoke("MySVC successfully start!");
            }
            catch (Exception)
            {
                mySVCIndicator?.Invoke("Input is incorrect. Shutting down...");

                Environment.Exit(123123);
            }
        }

        void IndicateStart(string str) => Console.WriteLine(str);

        /// <summary>
        /// Time to cut off changes made beyond prog's work. 
        /// </summary>
        public static readonly DateTime _startTime = DateTime.Now;

        /// <summary>
        /// Working directory.
        /// </summary>
        public static readonly string _workDirPath;

        /// <summary>
        /// Location of commits.
        /// </summary>
        public static readonly string _commitDirPath;

        /// <summary>
        /// Location of summary file.
        /// </summary>
        public static readonly string _summaryFile;

        /// <summary>
        /// Word to stop work in the Viewer mode.
        /// </summary>
        public static readonly string _stopWord = "/stop/";

        public void InitializeMySVC(WorkType mode)
        {
            IsWorkDirExists();

            IsCommitDirExists();

            switch (mode)
            {
                case WorkType.None:
                    Environment.Exit(123123);
                    break;

                case WorkType.Viewer:
                    do
                    {

                    } while (Console.ReadLine() != _stopWord);
                    break;

                case WorkType.Rollback:
                    ListOfCommits.ChooseTimestampToRollback();
                    break;
            }

            CommitChanges();
        }

        public void CommitChanges()
        {
            var str1 = File.ReadAllText(MySVC._summaryFile);

            List<MySVCFile> lastCommitCond = JsonConvert.DeserializeObject<List<MySVCFile>>(File.ReadAllText(MySVC._summaryFile));

            List<MySVCFile> currentCond = MySVC.GetAllFilesFromDirectories(MySVC._workDirPath);

            List<TheChange> changes = new List<TheChange>();

            CompareFiles(currentCond, lastCommitCond, ref changes);

            File.WriteAllText(Path.Combine(MySVC._commitDirPath, DateTime.Now.ToString().Replace(':', '_') + ".txt"),
                JsonConvert.SerializeObject(changes));

            File.WriteAllText(MySVC._summaryFile, JsonConvert.SerializeObject(currentCond));

            Console.WriteLine();

            ShowChanges(changes);
        }

        private void ShowChanges(List<TheChange> changes) => changes.ForEach((item) => { Console.WriteLine(item.ToString()); });

        private void CompareFiles(List<MySVCFile> curDirCond, List<MySVCFile> prevDirCond, ref List<TheChange> changes)
        {
            foreach (var item in curDirCond)
            {
                if (File.GetLastAccessTime(item.FileName) > MySVC._startTime) //Only files, modified after the program started will be remembered.
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
                        changes.Add(new TheChange(item.FileName, ChangeType.AddedFile, item.Text));
                    }
                }
            }
            if (prevDirCond != null)
            {
                foreach (var file in prevDirCond)
                {
                    if (curDirCond.FindIndex(oldFile => oldFile.FileName == file.FileName) < 0)
                        changes.Add(new TheChange(file.FileName, ChangeType.RemovedFile, file.Text));
                }
            }

        }

        public List<TheChange> CompareTwoFiles(string textOfFile1, string textOfFile2, string fileName)
        {
            List<TheChange> result = new List<TheChange>();

            CompareCommonPartOfFiles(textOfFile1, textOfFile2, fileName, ref result);

            CompareDifferPartOfFiles(textOfFile1, textOfFile2, fileName, ref result);

            return result;
        }

        private void CompareCommonPartOfFiles(string str1, string str2, string fileName, ref List<TheChange> result)
        {

            StringBuilder changedStr = new();

            int indxOfLastChangedSym = -1;

            int rigthBorder = Math.Min(str1.Length, str2.Length);

            for (int i = 0; i < rigthBorder; i++)
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
                            result.Add(new TheChange(fileName, ChangeType.ChangedText,
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
                result.Add(new TheChange(fileName, ChangeType.ChangedText,
                    changedStr.Append($" -> {str2.Substring(str1.IndexOf(changedStr.ToString()), changedStr.Length)}").ToString()));
        }

        private void CompareDifferPartOfFiles(string str1, string str2, string fileName, ref List<TheChange> result)
        {
            if (str2.Length > str1.Length)
                result.Add(new TheChange(fileName, ChangeType.AddedText, str2.Substring(
                        str1.Length, str2.Length - str1.Length)));
            else if (str1.Length > str2.Length)
                result.Add(new TheChange(fileName, ChangeType.RemovedText, str1.Substring(
                        str2.Length, str1.Length - str2.Length)));
        }

        private void IsWorkDirExists()
        {
            try
            {
                if (!Directory.Exists(_workDirPath))
                {
                    Directory.CreateDirectory(_workDirPath);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to create working directory. See:" + Environment.NewLine + ex.Message);
            }
        }

        private void IsCommitDirExists()
        {
            try
            {
                if (!Directory.Exists(_commitDirPath))
                {
                    var commitsDir = new DirectoryInfo(_commitDirPath);

                    commitsDir.Create();

                    commitsDir.Attributes = FileAttributes.Hidden | FileAttributes.Directory;

                    new FileStream(Path.Combine(commitsDir.FullName, "initialCommit.txt"), FileMode.Create).Dispose();

                    new FileStream(_summaryFile, FileMode.Create).Dispose();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to create commit directory. See:" + Environment.NewLine + ex.Message);
            }
        }

        public static List<MySVCFile> GetAllFilesFromDirectories(string pathToDir)
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


                
    }
}
