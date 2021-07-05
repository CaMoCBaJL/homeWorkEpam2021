using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using CommonConstants;
using DALInterfaces;
using Entities;
using Newtonsoft.Json;
using System.IO;

namespace JsonDAL
{
    public class AwardDAL : IDataLayer
    {
        public int EntityCount => Awards.Count;

        List<Award> Awards
        {
            get
            {
                if (Awards != null)
                    return Awards;
                else
                {
                    var data = JsonConvert.DeserializeObject<List<Award>>(File.ReadAllText(PathConstants.awardsDataLocation));

                    if (data == null)
                        return new List<Award>();
                    else
                        return data;
                }
            }
            set { }
        }


        public AwardDAL()
        {
            DataInegrity.CheckDataExistence();

            UpdateData();
        }

        void UpdateData()
        {
            UpdateConnectedEntities();

            File.WriteAllText(PathConstants.awardsDataLocation, JsonConvert.SerializeObject(Awards));

        }

        void UpdateConnectedEntities()
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(PathConstants.usersDataLocation));

            foreach (var item in Awards)
            {
                item.ConnectedEntities.RemoveAll(entityId => !users.Contains(users.Find(user => user.Id == entityId)));
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

        public IAuthentificator CreateAuthentificator() => new JSONAuthentificator();

        public bool DeleteEntity(CommonEntity entity)
        {
            if (!Awards.Contains(entity as Award))
                return false;
            else
            {
                Awards.Remove(entity as Award);

                new UserIdentities().DeleteIdentity(entity.Id);

                UpdateData();

                return true;
            }
        }

        public IEnumerable<CommonEntity> GetEntities() => Awards;

        public int GetEntityId(string entityName) => Awards.FindIndex(award => award.Title == entityName);

        public bool UpdateEntity(CommonEntity entity)
        {
            int indx = Awards.FindIndex(u => u.Id == entity.Id);

            if (indx == -1)
                return false;
            else
            {
                Awards[indx] = entity as Award;

                UpdateData();

                return true;
            }
        }
    }
}
