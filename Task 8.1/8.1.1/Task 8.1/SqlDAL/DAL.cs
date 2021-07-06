using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using DALInterfaces;
using CommonConstants;

namespace SqlDAL
{
    public class DAL : IDALController
    {
        public IDataLayer UserDAL => new UserDAL();

        public IDataLayer AwardDAL => new AwardDAL();
    }
}
