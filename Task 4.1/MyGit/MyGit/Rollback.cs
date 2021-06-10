using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.IO;


namespace MyGit
{
    class Rollback
    {
        public Rollback (int commitNum, List<string> commits)
        {
            if (commitNum > 0 & commits?.Count > 0)
                MakeRollback(commitNum, commits);
        }

        private void MakeRollback(int commitNum, List<string> commits)
        {
            List<TheChange> userCommits = new List<TheChange>();

            for (int i = 0; i < commitNum; i++)
            {
                var commit = JsonConvert.DeserializeObject<List<TheChange>>(File.ReadAllText(commits[i]));

                if (commit != null)
                    MakeChanges(commit);
                else
                {
                    foreach (var item in Directory.GetFiles(MySVC._workDirPath))
                    {
                        File.Delete(item);
                    }

                    foreach (var item in Directory.GetDirectories(MySVC._workDirPath))
                    {
                        if (!item.Contains("Commits"))
                            Directory.Delete(item, true);
                    }
                }

            }

            for (int i = commitNum - 1; i < commits.Count; i++)
            {
                File.Delete(commits[i]);
            }

            File.WriteAllText(MySVC._summaryFile,
                JsonConvert.SerializeObject(MySVC.GetAllFilesFromDirectories(MySVC._workDirPath)));

        }

        private void MakeChanges(List<TheChange> commit)
        {
            try
            {
                foreach (var item in commit)
                {
                    switch (item.ChangeType)
                    {
                        case ChangeType.AddedText:
                            File.WriteAllText(item.FileName,
                                File.ReadAllText(item.FileName) + item.NewContent);
                            break;
                        case ChangeType.ChangedText:
                            File.WriteAllText(item.FileName,
                                File.ReadAllText(item.FileName)
                                .Replace(item.NewContent.Split("->")[0], item.NewContent.Split("->")[1]));
                            break;
                        case ChangeType.RemovedText:
                            File.WriteAllText(item.FileName,
                                File.ReadAllText(item.FileName)
                                .Replace(item.NewContent, string.Empty));
                            break;
                        case ChangeType.AddedFile:

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
                        case ChangeType.RemovedFile:
                            File.Delete(item.FileName);
                            break;
                        case ChangeType.None:
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
