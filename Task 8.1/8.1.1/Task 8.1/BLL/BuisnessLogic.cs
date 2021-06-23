using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using JsonDAL;
using Entities;

namespace BLL
{
    public class BuisnessLogic
    {
        const string emptyStringValue = "Пользователи отсутствуют.";

        const string successfullOperationResult = "Операция успешно завершена.";

        const string unsuccessfullOperationResult = "Не удалось завешрить операцию.";

        public const string birthDateRegexPattern = "\\d{2}(\\.\\d{2}){2}";

        public const string ageRegexPattern = "\\d{1,3}";


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
                    return new List<string>(new string[] { emptyStringValue });
            }

            List<string> result = new List<string>();

            foreach (var user in data)
            {
                result.Add(user.ToString());
            }

            if (result.Count == 0)
                result.Add(emptyStringValue);

            return result;
        }

        public static string RemoveEntity(EntityType entityType, int entityId)
        {
            var dal = new DAL();

            CommonEntity entityToRemove;

            switch (entityType)
            {
                case EntityType.User:
                    entityToRemove = dal.GetUsers()[entityId];
                    break;

                case EntityType.Award:
                    entityToRemove = dal.GetAwards()[entityId];
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

                if (BuisnessLogic.ValidateParameter(parameter, regularExpression))
                    return parameter;
                else
                    Console.WriteLine("Ввод неверен");


            } while (true);
        }

        static bool ValidateParameter(string parameter, string regexExpression) => new Regex(regexExpression).IsMatch(parameter);
       
        public static string AddEntity(EntityType entityType, string entityData)
        {
            var dal = new DAL();

            CommonEntity entityToAdd;

            switch (entityType)
            {
                case EntityType.User:
                    entityToAdd = new User(new List<string>(entityData.Split(new char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)));
                    break;
                case EntityType.Award:
                    entityToAdd = new Award(entityData);
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

    }
}

