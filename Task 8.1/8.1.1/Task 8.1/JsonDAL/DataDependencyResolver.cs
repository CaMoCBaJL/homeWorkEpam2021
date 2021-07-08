using DALInterfaces;

namespace JsonDAL 
{
    class DataDependencyResolver : IDALController
    {
        public IDataLayer UserDAL => new UserDAL();

        public IDataLayer AwardDAL => new AwardDAL();
    }
}
