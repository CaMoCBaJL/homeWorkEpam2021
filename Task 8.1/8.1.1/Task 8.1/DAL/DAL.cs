using Entities;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

namespace JsonDAL
{

    public class DAL
    {
        const string dataFolderLocation = @"./data";

        const string usersDataLocation = @"./data/usersData.json";

        const string awardsDataLocation = @"./data/awardsData.json";


        public List<User> Users { get; }

        public List<Award> Awards { get; }


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
                        new DirectoryInfo(dataFolderLocation).Create();
                    break;
                case FileSystemObjectType.None:
                default:
                    break;
            }
        }

        public static void CheckDataLocationForExistence()
        {
            CheckUnitForExistence(dataFolderLocation, FileSystemObjectType.Folder);

            CheckUnitForExistence(usersDataLocation, FileSystemObjectType.File);

            CheckUnitForExistence(awardsDataLocation, FileSystemObjectType.File);
        }

        public DAL()
        {
            Users = GetCurrentEntitiesInfo<User>(usersDataLocation);

            Awards = GetCurrentEntitiesInfo<Award>(awardsDataLocation);
        }

        List<T> GetCurrentEntitiesInfo<T>(string pathToData)
        {
            var result = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(pathToData));

            if (result == null)
                return new List<T>();

            return result;
        }

        public void UpdateData()
        {
            File.WriteAllText(usersDataLocation, JsonConvert.SerializeObject(Users));

            File.WriteAllText(awardsDataLocation, JsonConvert.SerializeObject(Awards));
        }

        public bool AddEntity(CommonEntity entity)
        {
            switch (entity)
            {
                case User user:
                    if (Users.Contains(user))
                        return false;
                    else
                    {
                        Users.Add(user);

                        UpdateData();

                        return true;
                    }

                case Award award:
                    if (Awards.Contains(award))
                        return false;
                    else
                    {
                        Awards.Add(award);

                        UpdateData();

                        return true;
                    }
                default:
                    return false;
            }
        }

        public bool DeleteEntity(CommonEntity entity)
        {
            switch (entity)
            {
                case User user:

                    if (!Users.Contains(user))
                        return false;
                    else
                    {
                        Users.Remove(user);

                        UpdateData();

                        return true;
                    }
                case Award award:

                    if (!Awards.Contains(award))
                        return false;
                    else
                    {
                        Awards.Remove(award);

                        UpdateData();

                        return true;
                    }
                default:
                    return false;
            }
        }    

        public List<User> GetUsers()
        {
            if (Users != null)
                return new List<User>(Users);

            return new List<User>();
        }

        public List<Award> GetAwards()
        {
            if (Awards != null)
                return new List<Award>(Awards);

            return new List<Award>();
        }

    }
}
