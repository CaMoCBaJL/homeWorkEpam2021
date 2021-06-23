using System;
using System.IO;
using System.Text;
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


        public static List<string> GetListOfEntities(EntityType entityType)
        {
            var dal = new DAL();

            IEnumerable<CommonEntity> data;

            switch (entityType)
            {
                case EntityType.User:
                    data = new List<User>();
                    break;
                case EntityType.Award:
                    data = new List<Award>();
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

        public static string AddEntity(EntityType entityType, string entityData)
        {
            var dal = new DAL();

            CommonEntity entityToAdd;

            switch (entityType)
            {
                case EntityType.User:
                    entityToAdd = new User(new List<string>(entityData.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)));
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

