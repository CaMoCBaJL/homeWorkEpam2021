using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using DALInterfaces;

namespace SqlDAL
{
    public class DataDependencyResolver : IDALController
    {
        public IDataLayer UserDAL => new UserDAL();

        public IDataLayer AwardDAL => new AwardDAL();
    }
}
