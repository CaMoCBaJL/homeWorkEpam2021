using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace JsonDAL
{
    static class DataInegrity
    {
        static void CheckUnitForExistence(string path, FileSystemObjectType type)
        {
            switch (type)
            {
                case FileSystemObjectType.File:
                    if (!File.Exists(path))
                        new FileStream(path, FileMode.CreateNew).Dispose();
                    break;
                case FileSystemObjectType.Folder:
                    if (!Directory.Exists(path))
                        new DirectoryInfo(PathConstants.dataFolderLocation).Create();
                    break;
                case FileSystemObjectType.None:
                default:
                    break;
            }
        }

        public static void CheckDataExistence()
        {
            CheckUnitForExistence(PathConstants.dataFolderLocation, FileSystemObjectType.Folder);

            CheckUnitForExistence(PathConstants.usersDataLocation, FileSystemObjectType.File);

            CheckUnitForExistence(PathConstants.awardsDataLocation, FileSystemObjectType.File);

            CheckUnitForExistence(PathConstants.identitiesDataLocation, FileSystemObjectType.File);
        }
    }
}
