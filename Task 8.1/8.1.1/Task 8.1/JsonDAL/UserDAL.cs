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

namespace JsonDAL
{
    public class UserDAL : IDataLayer
    {
        bool AdminExists => Users.FindIndex(user => user.Id == 0) != -1;

        public int EntityCount => Users.Count;

        List<User> Users
        {
            get
            {
                if (Users != null)
                    return Users;
                else
                {
                    var data = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(PathConstants.usersDataLocation));

                    if (data == null)
                        return new List<User>();
                    else
                        return data;
                }
            }
            set { }
        }


        public UserDAL()
        {
            DataInegrity.CheckDataExistence();

            UpdateData();

            if (!AdminExists)
                AddAdmin();
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

            new UserIdentities().AddIdentity(0, Identity.HashThePassword("admin"));
        }

        public bool AddEntity(CommonEntity entity, string passwordHashSum)
        {
            int indx = Users.FindIndex(u => u.Id == entity.Id);

            if (indx > -1)
                return false;
            else
            {
                Users.Add(entity as User);

                new UserIdentities().AddIdentity(entity.Id, passwordHashSum);

                UpdateData();

                return true;
            }
        }

        public IAuthentificator CreateAuthentificator() => new JSONAuthentificator();

        public bool DeleteEntity(CommonEntity entity)
        {
            if (!Users.Contains(entity as User))
                return false;
            else
            {
                Users.Remove(entity as User);

                new UserIdentities().DeleteIdentity(entity.Id);

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
    }
}
