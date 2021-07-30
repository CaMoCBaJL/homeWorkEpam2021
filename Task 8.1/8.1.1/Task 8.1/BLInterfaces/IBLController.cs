using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLInterfaces
{
    public interface IBLController
    {
        ILogicLayer AwardLogic { get; }

        ILogicLayer UserLogic { get; }
    }
}
