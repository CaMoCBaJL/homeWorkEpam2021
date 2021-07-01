using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using JsonDAL;

namespace BL
{
    public class UserAuthentication
    {
        public static bool AuthenticateTheUser(string userName, string password) => UserIdentities.CheckUserIdentity(userName, password);
        
    }
}
