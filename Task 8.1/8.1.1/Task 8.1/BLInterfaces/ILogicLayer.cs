using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonConstants;
using DALInterfaces;

namespace BLInterfaces
{
    public interface ILogicLayer
    {
        IDataLayer _DAO { get; }

        string RemoveEntity(int entityId);

        string UpdateEntity(List<string> dataToUpdate, List<int> newConnectedEntitiesIds);

        string AddEntity(List<string> dataToAdd, List<int> connectedEntitiesIds, string password = Constants.emptyString);

        List<string> GetListOfEntities(List<int> addedEntities);

        List<string> GetListOfEntities(bool onlyNamesNeeded);
    }
}
