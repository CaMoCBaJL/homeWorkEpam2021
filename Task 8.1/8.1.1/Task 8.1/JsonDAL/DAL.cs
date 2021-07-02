using Entities;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Linq;
using DALInterfaces;

namespace JsonDAL
{

    public class DAL : IDataLayer
    {
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
                        new DirectoryInfo(PathConstants.dataFolderLocation).Create();
                    break;
                case FileSystemObjectType.None:
                default:
                    break;
            }
        }

        public static void CheckDataLocationForExistence()
        {
            CheckUnitForExistence(PathConstants.dataFolderLocation, FileSystemObjectType.Folder);

            CheckUnitForExistence(PathConstants.usersDataLocation, FileSystemObjectType.File);

            CheckUnitForExistence(PathConstants.awardsDataLocation, FileSystemObjectType.File);

            CheckUnitForExistence(PathConstants.identitiesDataLocation, FileSystemObjectType.File);
        }

        public DAL()
        {
            CheckDataLocationForExistence();

            Users = GetCurrentEntitiesInfo<User>(PathConstants.usersDataLocation);

            Awards = GetCurrentEntitiesInfo<Award>(PathConstants.awardsDataLocation);

            if (!AdminExists())
                AddAdmin();
        }

        bool AdminExists() => Users.FindIndex(user => user.Id == 0) != -1;

        void AddAdmin()
        {
            Users.Add(new User("admin", "0.0.0", 0, new List<int>(), 0));

            new UserIdentities().AddIdentity(0, Identity.HashThePassword("admin"));
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
            UpdateConnectedIds(EntityType.Award);

            UpdateConnectedIds(EntityType.User);

            File.WriteAllText(PathConstants.usersDataLocation, JsonConvert.SerializeObject(Users));

            File.WriteAllText(PathConstants.awardsDataLocation, JsonConvert.SerializeObject(Awards));
        }

        public bool AddEntity(CommonEntity entity, int passwordHashSum = -1)
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

                        var identities = new UserIdentities();

                        identities.AddIdentity(user.Id, passwordHashSum);

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

                        new UserIdentities().DeleteIdentity(user.Id);

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

        public void UpdateConnectedIds(EntityType entityType)
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
                if (item.Id > 0)
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
                    {
                        int index = Users.FindIndex(user => user.Name == entityName);

                        if (index > -1)
                            return Users[index].Id;
                        else return -1;
                    }
                    else
                        return 1;
                case EntityType.Award:
                    if (Awards.Count > 0)
                    {
                        int index = Awards.FindIndex(award => award.Title == entityName);

                        if (index > -1)
                            return Awards[index].Id;
                        else
                            return -1;
                    }
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

        public bool CheckUserIdentity(string userName, string password)
        {
            List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(File.ReadAllText(PathConstants.identitiesDataLocation));

            if (identities == null)
                identities = new List<Identity>();

            Identity currentUserIdentity = new Identity(new DAL().GetEntityId(EntityType.User, userName), Identity.HashThePassword(password));

            int index = identities.FindIndex(id => id.PasswordHashSumm == currentUserIdentity.PasswordHashSumm);

            return index > -1;
        }

        public bool UpdateEntity(CommonEntity entity, int passwordHashSum)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CommonEntity> GetEntities(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.User:
                    if (Users != null)
                        return new List<User>(Users);

                    return new List<User>();
                case EntityType.Award:
                    if (Awards != null)
                        return new List<Award>(Awards);

                    return new List<Award>();
                case EntityType.None:
                default:
                    return new List<CommonEntity>();
            }
        }
    }
}
