using System.Collections.Generic;
using Entities;

namespace DALInterfaces
{
    public interface IDataLayer
    {
        int UsersCount { get; }

        int AwardsCount { get; }

        bool AddEntity(CommonEntity entity, int passwordHashSum = -1);

        bool UpdateEntity(CommonEntity entity, int passwordHashSum = -1);

        bool DeleteEntity(CommonEntity entity);

        IEnumerable<CommonEntity> GetEntities(EntityType entityType);

        int GetEntityId(EntityType entityType, string entityName);

    }
}
