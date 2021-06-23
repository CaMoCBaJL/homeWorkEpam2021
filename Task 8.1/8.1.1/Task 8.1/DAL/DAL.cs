using Entities;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JsonDAL
{

    public class DAL
    {
        const string dataFolderLocation = @"./data";

        const string usersDataLocation = @"./data/usersData.json";

        const string awardsDataLocation = @"./data/awardsData.json";


        List<User> Users { get; }

        List<Award> Awards { get; }


        static void CheckUnitForExistence(string path, FileSystemObjectType type)
        {
            switch (type)
            {
                case FileSystemObjectType.File:
                    if (!File.Exists(path))
                        File.Create(path);
                    break;
                case FileSystemObjectType.Folder:
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    break;
                case FileSystemObjectType.None:
                default:
                    break;
            }
        }

        static DAL()
        {
            CheckUnitForExistence(dataFolderLocation, FileSystemObjectType.Folder);

            CheckUnitForExistence(usersDataLocation, FileSystemObjectType.File);

            CheckUnitForExistence(awardsDataLocation, FileSystemObjectType.File);
        }

        public DAL()
        {
            Users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(usersDataLocation));

            Awards = JsonConvert.DeserializeObject<List<Award>>(File.ReadAllText(awardsDataLocation));
        }

        void UpdateData()
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
