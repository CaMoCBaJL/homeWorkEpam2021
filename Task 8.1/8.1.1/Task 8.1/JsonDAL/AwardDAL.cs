using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using DALInterfaces;
using Entities;
using Newtonsoft.Json;
using System.IO;

namespace JsonDAL
{
    public class AwardDAL : IDataLayer
    {
        public int EntityCount => Awards.Count;

        List<Award> Awards { get; set; }


        public AwardDAL()
        {
            Awards = GetEntitiesData();

            DataInegrity.CheckDataExistence();

            UpdateData();
        }

        List<Award> GetEntitiesData()
        {
            var data = JsonConvert.DeserializeObject<List<Award>>(File.ReadAllText(PathConstants.awardsDataLocation));

            if (data == null)
                return new List<Award>();
            else
                return data;
        }

        void UpdateData()
        {
            UpdateConnectedEntities();

            File.WriteAllText(PathConstants.awardsDataLocation, JsonConvert.SerializeObject(Awards));

        }

        void UpdateConnectedEntities()
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(PathConstants.usersDataLocation));

            if (users != null)
            {
                foreach (var item in Awards)
                {
                    item.ConnectedEntities.RemoveAll(entityId => !users.Contains(users.Find(user => user.Id == entityId)));
                }
            }
        }

        public bool AddEntity(CommonEntity entity, string passwordHashSum)
        {
            int indx = Awards.FindIndex(award => award.Id == entity.Id);

            if (indx > -1)
                return false;
            else
            {
                Awards.Add(entity as Award);

                UpdateData();

                return true;
            }
        }

        public bool RemoveEntity(int entityId)
        {
            var awardToRemove = Awards.Find(award => award.Id == entityId);

            if (awardToRemove == null)
                return false;
            else
            {
                Awards.Remove(awardToRemove);

                new IdentityDataLogic().DeleteIdentity(entityId);

                UpdateData();

                return true;
            }
        }

        public IEnumerable<CommonEntity> GetEntities() => Awards;

        public int GetEntityId(string entityName) => Awards.FindIndex(award => award.Title == entityName);

        public bool UpdateEntity(CommonEntity entity)
        {
            int index = Awards.FindIndex(u => u.Id == entity.Id);

            if (index == -1)
                return false;
            else
            {
                Awards[index] = entity as Award;

                UpdateData();

                return true;
            }
        }

        public IEnumerable<CommonEntity> GetConnectedEntities()
        {
            var result = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(PathConstants.usersDataLocation));

            if (result == null)
                return new List<User>();

            return result;
        }

        public IAuthentificator CreateAuthentificator() => new JSONAuthentificator(this);
    }
}
