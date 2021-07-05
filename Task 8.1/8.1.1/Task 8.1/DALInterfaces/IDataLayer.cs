using System.Collections.Generic;
using Entities;
using CommonInterfaces;
using CommonConstants;

namespace DALInterfaces
{
    public interface IDataLayer
    {
        int EntityCount { get; }

        bool AddEntity(CommonEntity entity, string passwordHashSum);

        bool UpdateEntity(CommonEntity entity);

        bool DeleteEntity(CommonEntity entity);

        IEnumerable<CommonEntity> GetEntities();

        int GetEntityId(string entityName);

        IAuthentificator CreateAuthentificator();
    }
}
