using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dependencies;

namespace IdentityChecker
{
    public class IdentityChecker
    {
        public static bool CheckIdentity(string userName, string password) => DependencyResolver.Instance.ProjectDAO.CheckUserIdentity(userName, password);
    }
}
