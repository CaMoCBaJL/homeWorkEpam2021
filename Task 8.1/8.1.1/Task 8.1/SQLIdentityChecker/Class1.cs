using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLIdentityChecker
{
    public class IdentityChecker
    {

        public bool CheckUserIdentity(string userName, string password)
        {
            List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(File.ReadAllText(PathConstants.identitiesDataLocation));

            if (identities == null)
                identities = new List<Identity>();

            Identity currentUserIdentity = new Identity(new DAL().GetEntityId(EntityType.User, userName), Identity.HashThePassword(password));

            int index = identities.FindIndex(id => id.PasswordHashSumm == currentUserIdentity.PasswordHashSumm);

            return index > -1;
        }
    }
}
