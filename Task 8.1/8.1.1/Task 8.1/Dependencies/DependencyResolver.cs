using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALInterfaces;
using BLInterfaces;
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

        public IDataLayer ProjectDAO { get => new SqlDAL.DAL(); private set { } }

        public ILogicLayer ProjectBLL { get => new BL.BuisnessLogic(ProjectDAO); private set { } }

        public void UpdateLayers()
        {
            ProjectDAO = new SqlDAL.DAL();

            ProjectBLL = new BL.BuisnessLogic(ProjectDAO);
        }
    }
}
