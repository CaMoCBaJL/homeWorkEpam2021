using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;

namespace IdentityChecker
{
    public class Authentificator
    {
        IAuthentificator authentificator;

        public Authentificator(IAuthentificator authRealization) => authentificator = authRealization;

        public bool CheckUserIdentity(string userName, string password) => authentificator.CheckUserIdentity(userName, password);
    }
}
