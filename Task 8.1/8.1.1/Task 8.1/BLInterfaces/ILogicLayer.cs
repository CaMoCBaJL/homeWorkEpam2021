using System.Collections.Generic;


namespace BLInterfaces
{
    public interface ILogicLayer
    {
        string FindEntity(string entityName);

        string ValidateEntityData(List<string> entityData);

        bool RemoveEntity(int entityId);

        bool UpdateEntity(List<string> dataToUpdate, List<int> newConnectedEntitiesIds);

        bool AddEntity(List<string> dataToAdd, List<int> connectedEntitiesIds, string password);

        List<string> GetConnectedEntities(List<int> addedEntities);

        List<string> GetConnectedEntitiesNames();

        List<string> GetEntities();
    }
}
