using System.Collections.Generic;
using Entities;
using CommonInterfaces;
using CommonConstants;

namespace DALInterfaces
{
    public interface IDataLayer
    {
        int UsersCount { get; }

        int AwardsCount { get; }

        bool AddEntity(CommonEntity entity, string passwordHashSum = Constants.emptyString);

        bool UpdateEntity(CommonEntity entity, string passwordHashSum = Constants.emptyString);

        bool DeleteEntity(CommonEntity entity);

        IEnumerable<CommonEntity> GetEntities();

        int GetEntityId(string entityName);

        IAuthentificator CreateAuthentificator();
    }
}
