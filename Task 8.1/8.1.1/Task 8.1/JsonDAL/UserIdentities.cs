using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using Entities;
using System.Security.Cryptography;
using System.IO;

namespace JsonDAL
{
    public class UserIdentities
    {
        internal void DeleteIdentity(int userId) => UpdateIdentities(new Identity(userId, default), IdentityUpdateType.Delete);

        internal void AddIdentity(int userId, string password) => UpdateIdentities(new Identity(userId, Identity.HashThePassword(password)), IdentityUpdateType.Add);

        public static bool CheckUserIdentity(string userName, string password)
        {
            List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(File.ReadAllText(PathConstants.identitiesDataLocation));

            if (identities == null)
                identities = new List<Identity>();

            Identity currentUserIdentity = new Identity(new DAL().GetEntityId(EntityType.User, userName), Identity.HashThePassword(password));

            int index = identities.FindIndex(id => id.PasswordHashSumm == currentUserIdentity.PasswordHashSumm);

            return index > -1;
        }

        void UpdateIdentities(Identity identity, IdentityUpdateType updateType)
        {
            List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(File.ReadAllText(PathConstants.identitiesDataLocation));

            if (identities == null)
                identities = new List<Identity>();

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

            File.WriteAllText(PathConstants.identitiesDataLocation, JsonConvert.SerializeObject(identities));
        }

        internal static void AddAdmin()
        {
            var dal = new DAL();

            dal.AddNewUser(new User("admin", "0.0.0", 0, new List<int>(), 0), "admin");
        }
    }
}
