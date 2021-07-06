using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALInterfaces
{
    public interface IDALController
    {
        IDataLayer UserDAL { get; }

        IDataLayer AwardDAL { get; }
    }
}
