using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLInterfaces;
using DALInterfaces;

namespace BLL
{
    public class AwardLogic : ILogicLayer
    {
        public IDataLayer _DAO { get; }


        public AwardLogic(IDataLayer dataLayer) => _DAO = dataLayer;

        string ILogicLayer.AddEntity(List<string> dataToAdd, List<int> connectedEntitiesIds, string password)
        {
            throw new NotImplementedException();
        }

        List<string> ILogicLayer.GetListOfEntities(List<int> addedEntities)
        {
            throw new NotImplementedException();
        }

        List<string> ILogicLayer.GetListOfEntities(bool onlyNamesNeeded)
        {
            throw new NotImplementedException();
        }

        string ILogicLayer.RemoveEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        string ILogicLayer.UpdateEntity(List<string> dataToUpdate, List<int> newConnectedEntitiesIds)
        {
            throw new NotImplementedException();
        }
    }
}
