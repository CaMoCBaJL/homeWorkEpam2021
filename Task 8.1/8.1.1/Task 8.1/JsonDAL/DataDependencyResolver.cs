using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALInterfaces;

namespace JsonDAL 
{
    class DataDependencyResolver : IDALController
    {
        public IDataLayer UserDAL => new UserDAL();

        public IDataLayer AwardDAL => new AwardDAL();
    }
}
