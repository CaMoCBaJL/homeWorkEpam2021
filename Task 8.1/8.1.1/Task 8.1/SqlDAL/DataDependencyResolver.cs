using DALInterfaces;

namespace SqlDAL
{
    public class DataDependencyResolver : IDALController
    {
        public IDataLayer UserDAL => new UserDAL();

        public IDataLayer AwardDAL => new AwardDAL();
    }
}
