using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGit
{
    class MySVCDir
    {
        private List<MySVCFile> files;

        private List<MySVCDir> dirs;

    
        public MySVCDir()
        {
            files = new List<MySVCFile>();

            dirs = new List<MySVCDir>();
        }

        public string DirName { get; init; }

        public List<MySVCFile> GetFiles() => files;

        public List<MySVCDir> GetDirs() => dirs;

        public void AddFile(FileInfo file) => this.AddFile(new MySVCFile() { FileName = file.FullName, Text = File.ReadAllText(file.FullName) });
        

        public void AddFile(MySVCFile file)
        {
            for (int i = 0; i < files.Count; i++)
            {
                if (file.FileName == files[i].FileName)
                {
                    files[i] = file;

                    return;
                }
            }

            files.Add(file);
        }

        public void AddDir(MySVCDir dir)
        {
            for (int i = 0; i < dirs.Count; i++)
            {
                if (dir.DirName == dirs[i].DirName)
                {
                    dirs[i] = dir;

                    return;
                }
            }

            dirs.Add(dir);
        }
    }

}
