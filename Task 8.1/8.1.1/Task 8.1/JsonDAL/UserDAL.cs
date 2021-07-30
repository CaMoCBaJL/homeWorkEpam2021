using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using DALInterfaces;
using Entities;
using Newtonsoft.Json;
using CommonLogic;

namespace JsonDAL
{
    public class UserDAL : IDataLayer
    {
        bool AdminExists => Users.FindIndex(user => user.Id == 0) != -1;

        public int EntityCount => Users.Count;

        List<User> Users { get; set; }


        public UserDAL()
        {
            Users = GetEntitiesData();

            DataInegrity.CheckDataExistence();

            UpdateData();

            if (!AdminExists)
                AddAdmin();
        }

        List<User> GetEntitiesData()
        {
            var data = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(PathConstants.usersDataLocation));

            if (data == null)
                return new List<User>();
            else
                return data;
        }

        void UpdateData()
        {
            UpdateConnectedEntities();

            File.WriteAllText(PathConstants.usersDataLocation, JsonConvert.SerializeObject(Users));

        }

        void UpdateConnectedEntities()
        {
            var awards = JsonConvert.DeserializeObject<List<Award>>(File.ReadAllText(PathConstants.awardsDataLocation));

            foreach (var item in Users)
            {
                item.ConnectedEntities.RemoveAll(entityId => !awards.Contains(awards.Find(award => award.Id == entityId)));
            }
        }

        void AddAdmin()
        {
            Users.Add(new User("admin", "0.0.0", 0, new List<int>(), 0));

            new IdentityDataLogic().AddIdentity(0,
                new PasswordHasher().HashThePassword("admin"));
        }

        public bool AddEntity(CommonEntity entity, string passwordHashSum)
        {
            int indx = Users.FindIndex(u => u.Id == entity.Id);

            if (indx > -1)
                return false;
            else
            {
                Users.Add(entity as User);

                new IdentityDataLogic().AddIdentity(entity.Id, passwordHashSum);

                UpdateData();

                return true;
            }
        }

        public IAuthentificator CreateAuthentificator() => new JSONAuthentificator(this);

        public bool RemoveEntity(int entityId)
        {
            var userToRemove = Users.Find(user => user.Id == entityId);

            if (userToRemove == null)
                return false;
            else
            {
                Users.Remove(userToRemove);

                new IdentityDataLogic().DeleteIdentity(entityId);

                UpdateData();

                return true;
            }
        }

        public IEnumerable<CommonEntity> GetEntities() => Users;

        public int GetEntityId(string entityName) => Users.FindIndex(user => user.Name == entityName);

        public bool UpdateEntity(CommonEntity entity)
        {
            int indx = Users.FindIndex(u => u.Id == entity.Id);

            if (indx == -1)
                return false;
            else
            {
                Users[indx] = entity as User;

                UpdateData();

                return true;
            }
        }

        public IEnumerable<CommonEntity> GetConnectedEntities()
        {
            var result = JsonConvert.DeserializeObject<List<Award>>(File.ReadAllText(PathConstants.awardsDataLocation));

            if (result == null)
                return new List<Award>();

            return result;
        }
    }
}
