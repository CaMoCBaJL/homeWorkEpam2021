using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using DALInterfaces;
using BLInterfaces;
using CommonInterfaces;
using IdentityChecker;

namespace Dependencies
{
    public class DependencyResolver
    {

        #region Singleton
        private static DependencyResolver _instance;

        public static DependencyResolver Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DependencyResolver();

                return _instance;
            }
        }
        #endregion

        public IDALController ProjectDAO => new SqlDAL.DataDependencyResolver();

        public IBLController ProjectBLL => new LogicDependencyResolver(ProjectDAO);

        public IAuthentificator Authentificator => ProjectDAO.UserDAL.CreateAuthentificator(); 

    }
}
