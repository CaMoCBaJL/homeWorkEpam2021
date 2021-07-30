using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using JsonDAL;
using System.Linq;
using Entities;

namespace BLL
{
    public class BuisnessLogic
    {
        public const string emptyStringValue = " отсутствуют.";

        const string successfullOperationResult = "Операция успешно завершена.";

        const string unsuccessfullOperationResult = "Не удалось завешрить операцию.";

        public const string birthDateRegexPattern = "\\d{1,2}(\\.\\d{1,2}){2}";

        public const string ageRegexPattern = "\\d{1,2}";


        public static void CheckDataLocation() => DAL.CheckDataLocationForExistence();

        public List<string> GetListOfEntities(EntityType entityType, List<int> addedEntities)
        {
            List<string> result = new List<string>();

            var data = GetListOfEntities(entityType, true);

            addedEntities = addedEntities.OrderByDescending(id => id).ToList();

            addedEntities.ForEach(entityId => data.RemoveAt(entityId));

            data.ForEach(entity => result.Add(entity));
            
            return result;
        }

        public static bool DoesStringContainsCommonParts(string entity) => entity.EndsWith(emptyStringValue) || entity.StartsWith("Список ");

        public List<string> GetListOfEntities(EntityType entityType, bool onlyNamesNeeded)
        {
            var dal = new DAL();

            IEnumerable<CommonEntity> data;

            switch (entityType)
            {
                case EntityType.User:
                    data = dal.GetUsers();
                    break;
                case EntityType.Award:
                    data = dal.GetAwards();
                    break;
                case EntityType.None:
                default:
                    return new List<string>(new string[] { entityType.ToString() + emptyStringValue });
            }

            List<string> result = new List<string>();

            foreach (var entity in data)
            {
                if (onlyNamesNeeded)
                {
                    switch (entity)
                    {
                        case User user:
                            result.Add(user.Name);
                            break;
                        case Award award:
                            result.Add(award.Title);
                            break;
                        default:
                            break;
                    }
                }
                else
                    result.Add(entity.ToString());
            }

            if (result.Count == 0)
                result.Add(entityType.ToString() + emptyStringValue);
            else
                result.Insert(0, $"Список {entityType.ToString()}: " + Environment.NewLine);

            return result;
        }

        public string RemoveEntity(EntityType entityType, int entityId)
        {
            var dal = new DAL();

            CommonEntity entityToRemove;

            switch (entityType)
            {
                case EntityType.User:
                    entityToRemove = dal.GetUsers()[entityId - 1];
                    break;

                case EntityType.Award:
                    entityToRemove = dal.GetAwards()[entityId - 1];
                    break;

                case EntityType.None:
                default:
                    return unsuccessfullOperationResult;
            }

            if (dal.DeleteEntity(entityToRemove))
                return successfullOperationResult;
            else
                return unsuccessfullOperationResult;

        }

        public string UpdateEntity(EntityType entityType, string entityData, List<int> connectedEntitiesIds)
        {
            var dal = new DAL();

            CommonEntity entityToUpdate;

            List<string> entityParameters = entityData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            switch (entityType)
            {
                case EntityType.User:
                    entityToUpdate = new User(entityParameters[0], entityParameters[1], int.Parse(entityParameters[2]),
                        connectedEntitiesIds, int.Parse(entityParameters[3]));
                    break;
                case EntityType.Award:
                    entityToUpdate = new Award(entityParameters[0], connectedEntitiesIds,
                        int.Parse(entityParameters[1]));
                    break;
                case EntityType.None:
                default:
                    return unsuccessfullOperationResult;
            }

            if (dal.UpdateEntity(entityToUpdate))
                return successfullOperationResult;
            else
                return unsuccessfullOperationResult;
        }

        public static int GetEntityId(EntityType entityType, string entityName) => new DAL().GetEntityId(entityType, entityName);

        public static bool ValidateParameter(string parameter, string regexExpression) => new Regex(regexExpression).IsMatch(parameter);

        public  string AddEntity(EntityType entityType, string entityData, List<int> connectedEntitiesIds)
        {
            var dal = new DAL();

            CommonEntity entityToAdd;

            switch (entityType)
            {
                case EntityType.User:
                    entityToAdd = new User(new List<string>(entityData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)), connectedEntitiesIds, dal.UsersCount + 1);

                    UpdateConnnectedEntities(connectedEntitiesIds, dal.GetAwards(), entityToAdd.Id);
                    break;
                case EntityType.Award:
                    entityToAdd = new Award(entityData, connectedEntitiesIds, dal.AwardsCount + 1);

                    UpdateConnnectedEntities(connectedEntitiesIds, dal.GetUsers(), entityToAdd.Id);
                    break;
                case EntityType.None:
                default:
                    return unsuccessfullOperationResult;
            }

            if (dal.AddEntity(entityToAdd))
                return successfullOperationResult;
            else
                return unsuccessfullOperationResult;
        }

         void UpdateConnnectedEntities(List<int> connectedIds, IEnumerable<CommonEntity> otherEntitiesToConnect, int newEntityId)
        {

            foreach (var entity in otherEntitiesToConnect)
            {
                if (connectedIds.Contains(entity.Id))
                    entity.AddConnectedEntity(newEntityId);
            }

            new DAL().UpdateData();
        }

    }
}

