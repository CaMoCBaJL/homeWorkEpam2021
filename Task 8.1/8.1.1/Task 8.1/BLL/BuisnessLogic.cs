using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using JsonDAL;
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

        public static List<string> GetListOfEntities(EntityType entityType, List<int> addedEntities)
        {
            List<string> result = new List<string>();

            foreach (var entity in GetListOfEntities(entityType))
            {
                if (!DoesStringContainsCommonParts(entity))
                {
                    if (!addedEntities.Contains(int.Parse(entity.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)[1].Split()[1])))
                        result.Add(entity);
                }
                else
                    result.Add(entity);
            }

            return result;
        }

        public static bool DoesStringContainsCommonParts(string entity) => entity.EndsWith(emptyStringValue) || entity.StartsWith("Список ");

        public static List<string> GetListOfEntities(EntityType entityType)
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
                result.Add(entity.ToString());
            }

            if (result.Count == 0)
                result.Add(entityType.ToString() + emptyStringValue);
            else
                result.Insert(0, $"Список {entityType.ToString()}: " + Environment.NewLine);

            return result;
        }

        public static string RemoveEntity(EntityType entityType, int entityId)
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

        public static string CorrectInputTheParameter(string entityField, string regularExpression)
        {
            string parameter;

            do
            {
                Console.WriteLine($"Введите параметр " + entityField);

                parameter = Console.ReadLine();

                if (ValidateParameter(parameter, regularExpression))
                    return parameter;
                else
                    Console.WriteLine("Ввод неверен");


            } while (true);
        }

        public static int GetEntityId(string entityInfo) => int.Parse(entityInfo.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[1].Split()[1]);

        static bool ValidateParameter(string parameter, string regexExpression) => new Regex(regexExpression).IsMatch(parameter);

        public static string AddEntity(EntityType entityType, string entityData, List<int> additionalEntitiesIds)
        {
            var dal = new DAL();

            CommonEntity entityToAdd;

            switch (entityType)
            {
                case EntityType.User:
                    entityToAdd = new User(new List<string>(entityData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)), additionalEntitiesIds, dal.Users.Count + 1);

                    AddAwardedUser(additionalEntitiesIds);
                    break;
                case EntityType.Award:
                    entityToAdd = new Award(entityData, new List<int>(), dal.Awards.Count + 1);
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

        static void AddAwardedUser(List<int> awardIds)
        {
            var dal = new DAL();

            int awardedUserId = dal.GetUsers().Count + 1;

            foreach (var award in dal.GetAwards())
            {
                if (awardIds.Contains(award.Id))
                    award.AddAwardedUser(awardedUserId);
            }

            dal.UpdateData();
        }

    }
}

