using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALInterfaces;
using BLInterfaces;

namespace BL
{
    public class BuisnessLogic : IBLController
    {
        IDALController _DAO;

        public BuisnessLogic(IDALController dataLayer) => _DAO = dataLayer;

        public ILogicLayer UserLogic => new UserLogic(_DAO.UserDAL);

        public ILogicLayer AwardLogic => new AwardLogic(_DAO.AwardDAL);

    }
}
