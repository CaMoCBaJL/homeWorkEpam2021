using System.Collections.Generic;
using Entities;
using CommonInterfaces;

namespace DALInterfaces
{
    public interface IDataLayer
    {
        int EntityCount { get; }

        bool AddEntity(CommonEntity entity, string passwordHashSum);

        bool UpdateEntity(CommonEntity entity);

        bool RemoveEntity(int entityId);

        IEnumerable<CommonEntity> GetEntities();

        IEnumerable<CommonEntity> GetConnectedEntities();

        int GetEntityId(string entityName);

        IAuthentificator CreateAuthentificator();
    }
}
