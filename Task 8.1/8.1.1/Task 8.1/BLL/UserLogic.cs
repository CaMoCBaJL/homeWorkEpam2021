using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLInterfaces;
using Entities;
using DALInterfaces;

namespace BLL
{
    public class UserLogic : ILogicLayer
    {
        public IDataLayer _DAO { get; }


        public UserLogic(IDataLayer dataLayer) => _DAO = dataLayer;

        public string AddEntity(List<string> dataToAdd, List<int> connectedEntitiesIds, string password = "")
        {
            throw new NotImplementedException();
        }

        public List<string> GetListOfEntities(List<int> addedEntities)
        {
            throw new NotImplementedException();
        }

        public List<string> GetListOfEntities(bool onlyNamesNeeded)
        {
            throw new NotImplementedException();
        }

        public string RemoveEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public string UpdateEntity(List<string> dataToUpdate, List<int> newConnectedEntitiesIds)
        {
            throw new NotImplementedException();
        }
    }
}
