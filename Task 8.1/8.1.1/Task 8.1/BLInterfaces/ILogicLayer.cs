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
        bool ValidateEntityData(List<string> entityData);

        bool RemoveEntity(int entityId);

        bool UpdateEntity(List<string> dataToUpdate, List<int> newConnectedEntitiesIds);

        bool AddEntity(List<string> dataToAdd, List<int> connectedEntitiesIds, string password);

        List<string> GetConnectedEntities(List<int> addedEntities);

        List<string> GetConnectedEntitiesNames();
    }
}
