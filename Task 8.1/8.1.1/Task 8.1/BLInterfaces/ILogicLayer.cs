using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLInterfaces
{
    public interface ILogicLayer
    {
        List<string> GetListOfEntities(EntityType entityType, List<int> addedEntities);

        List<string> GetListOfEntities(EntityType entityType, bool onlyNamesNeeded);

        string RemoveEntity(EntityType entityType, int entityId);

        string UpdateEntity(EntityType entityType, string entityData, List<int> connectedEntitiesIds);

        string AddEntity(EntityType entityType, string entityData, List<int> connectedEntitiesIds, string password = "");
    }
}
