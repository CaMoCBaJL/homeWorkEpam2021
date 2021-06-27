using Entities;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;


namespace JsonDAL
{

    public class DAL
    {
        const string dataFolderLocation = @"./data";

        const string usersDataLocation = @"./data/usersData.json";

        const string awardsDataLocation = @"./data/awardsData.json";

        const string identitiesDataLocation = @"./data/identities.json";


        List<User> Users { get; }

        List<Award> Awards { get; }

        public int UsersCount { get => Users.Count; }

        public int AwardsCount { get => Awards.Count; }


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

            CheckUnitForExistence(identitiesDataLocation, FileSystemObjectType.File);
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
            UpdateIds(EntityType.Award);

            UpdateIds(EntityType.User);

            File.WriteAllText(usersDataLocation, JsonConvert.SerializeObject(Users));

            File.WriteAllText(awardsDataLocation, JsonConvert.SerializeObject(Awards));
        }

        public bool AddEntity(CommonEntity entity)
        {
            switch (entity)
            {
                case User user:
                    int indx = Users.FindIndex(u => u.Name == user.Name);

                    if (indx > -1)
                        return false;
                    else
                    {
                        Users.Add(user);

                        UpdateData();

                        return true;
                    }

                case Award award:
                    if (Awards.FindIndex(a => a.Title == award.Title) > -1)
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

                        DeleteIdentity(user.Id);

                        UpdateEntitiesConnectedWithDeleted(entity);

                        UpdateData();

                        return true;
                    }
                case Award award:

                    if (!Awards.Contains(award))
                        return false;
                    else
                    {
                        Awards.Remove(award);

                        UpdateEntitiesConnectedWithDeleted(entity);

                        UpdateData();

                        return true;
                    }
                default:
                    return false;
            }
        }

        void DeleteIdentity(int userId) => UpdateIdentities(new Identity(userId, default), IdentityUpdateType.Delete);

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

        public void UpdateIds(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.User:
                    UpdateCommonEntityList(Users);
                    break;
                case EntityType.Award:
                    UpdateCommonEntityList(Awards);
                    break;
                case EntityType.None:
                default:
                    break;
            }
        }

        void UpdateCommonEntityList(IEnumerable<CommonEntity> entities)
        {
            int counter = 1;

            foreach (var item in entities)
            {
                item.ChangeId(counter);

                counter++;
            }
        }

        public int GetEntityId(EntityType entityType, string entityName)
        {
            switch (entityType)
            {
                case EntityType.User:
                    if (Users.Count > 0)
                        return Users[Users.FindIndex(user => user.Name == entityName)].Id;
                    else
                        return 1;
                case EntityType.Award:
                    if (Awards.Count > 0)
                        return Awards[Awards.FindIndex(award => award.Title == entityName)].Id;
                    else
                        return 1;
                case EntityType.None:
                default:
                    return -1;
            }
        }

        void UpdateEntitiesConnectedWithDeleted(CommonEntity deletedEntity)
        {
            IEnumerable<CommonEntity> connectedEntities;

            switch (deletedEntity)
            {
                case User user:
                    connectedEntities = Awards;
                    break;
                case Award award:
                    connectedEntities = Users;
                    break;
                default:
                    return;
            }

            foreach (var entity in connectedEntities)
            {
                if (entity.ConnectedEntities.Contains(deletedEntity.Id))
                    entity.ConnectedEntities.Remove(deletedEntity.Id);
            }
        }

        public bool UpdateEntity(CommonEntity entity)
        {
            switch (entity)
            {
                case User user:
                    Users[entity.Id - 1] = user;

                    UpdateData();

                    return true;

                case Award award:

                    Awards[entity.Id - 1] = award;

                    UpdateData();

                    return true;

                default:
                    return false;
            }
        }

        public void AddNewUser(CommonEntity newUser, string password)
        {
            AddEntity(newUser);

            AddIdentity(newUser.Id, password);
        }

        void AddIdentity(int userId, string password) => UpdateIdentities(new Identity(userId, HashThePassword(password)), IdentityUpdateType.Add);

        public bool CheckUserIdentity(string userName, string password)
        {
            List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(identitiesDataLocation);

            Identity currentUserIdentity = new Identity(GetEntityId(EntityType.User, userName), HashThePassword(password));

            return identities.FindIndex(id => id == currentUserIdentity) > -1;
        }

        int HashThePassword(string password)
        {
            HashAlgorithm sha = SHA256.Create();

            int result = 0;

            foreach(var hashValue in sha.ComputeHash(Encoding.UTF8.GetBytes(password)))
            {
                result += hashValue;
            }

            return result;
        }

        void UpdateIdentities(Identity identity, IdentityUpdateType updateType)
        {
            List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(identitiesDataLocation);

            switch (updateType)
            {
                case IdentityUpdateType.Add:
                    identities.Add(identity);
                    break;
                case IdentityUpdateType.Delete:
                    identities.Remove(identities.Find(id => id.UserId == identity.UserId));
                    break;
                case IdentityUpdateType.None:
                default:
                    return;
            }

            File.WriteAllText(identitiesDataLocation, JsonConvert.SerializeObject(identities));
        }
    }
}
