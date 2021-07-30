using DALInterfaces;
using BLInterfaces;

namespace BL
{
    public class LogicDependencyResolver : IBLController
    {
        IDALController _DAO;

        public LogicDependencyResolver(IDALController dataLayer) => _DAO = dataLayer;

        public ILogicLayer UserLogic => new UserLogic(_DAO.UserDAL);

        public ILogicLayer AwardLogic => new AwardLogic(_DAO.AwardDAL);

    }
}
