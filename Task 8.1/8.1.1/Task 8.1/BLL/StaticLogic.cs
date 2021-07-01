using Entities;
using JsonDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class StaticLogic
    {
        public static int GetEntityId(EntityType entityType, string entityName) => new DAL().GetEntityId(entityType, entityName);

        public static bool ValidateParameter(string parameter, string regexExpression) => new Regex(regexExpression).IsMatch(parameter);

        public static bool DoesStringContainsCommonParts(string entity) => entity.EndsWith(StringConstants.emptyStringValue) || entity.StartsWith("Список ");

        public static void UpdateConnnectedEntities(List<int> connectedIds, IEnumerable<CommonEntity> otherEntitiesToConnect, int newEntityId)
        {

            foreach (var entity in otherEntitiesToConnect)
            {
                if (connectedIds.Contains(entity.Id))
                    entity.AddConnectedEntity(newEntityId);
            }

            new DAL().UpdateData();
        }

        public static void CheckDataLocation() => new DAL().CheckDataLocationForExistence();

    }
}
