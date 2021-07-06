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

        public IDALController ProjectDAO { get => new SqlDAL.DAL(); private set { } }

        public IBLController ProjectBLL { get => new BuisnessLogic(ProjectDAO); private set { } }

        public IAuthentificator Authentificator { get => ProjectDAO.UserDAL.CreateAuthentificator(); }

        public void UpdateLayers()
        {
            ProjectDAO = new SqlDAL.DAL();

            ProjectBLL = new BuisnessLogic(ProjectDAO);
        }
    }
}
