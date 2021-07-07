using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using DALInterfaces;
using CommonLogic;

namespace JsonDAL
{
    public class JSONAuthentificator : IAuthentificator
    {
        IDataLayer _DAO;


        public JSONAuthentificator(IDataLayer dataLayer)
        {
            if (dataLayer.GetType() != typeof(UserDAL))
                throw new Exception("Инициализация аутентификатора из неверного класса DAL!");

            _DAO = dataLayer;
        }
        public bool CheckUserIdentity(string userName, string password)
        {
            List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(
                File.ReadAllText(PathConstants.identitiesDataLocation));

            if (identities == null)
                identities = new List<Identity>();

            Identity currentUserIdentity = new Identity(_DAO.GetEntityId(userName),
                new PasswordHasher().HashThePassword(password));

            int index = identities.FindIndex(
                id => id.PasswordHashSumm == currentUserIdentity.PasswordHashSumm);

            return index > -1;
        }
    }
}
