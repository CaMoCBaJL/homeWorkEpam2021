using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using JsonDAL;
using System.Linq;
using Entities;
using System.Text;
using BLInterfaces;
using DALInterfaces;

namespace BL
{
    public class BuisnessLogic: ILogicLayer
    {
        private IDataLayer _DAO;


        public BuisnessLogic(IDataLayer currentDAO) => _DAO = currentDAO;

        public List<string> GetListOfEntities(EntityType entityType, List<int> addedEntities)
        {
            List<string> result = new List<string>();

            var data = GetListOfEntities(entityType, true);

            addedEntities = addedEntities.OrderByDescending(id => id).ToList();

            addedEntities.ForEach(entityId => data.RemoveAt(entityId));

            data.ForEach(entity => result.Add(entity));

            return result;
        }

        public List<string> GetListOfEntities(EntityType entityType, bool onlyNamesNeeded)
        {
            IEnumerable<CommonEntity> data;

            switch (entityType)
            {
                case EntityType.User:
                    data = _DAO.GetEntities(EntityType.User);
                    break;
                case EntityType.Award:
                    data = _DAO.GetEntities(EntityType.Award);
                    break;
                case EntityType.None:
                default:
                    return new List<string>(new string[] { entityType.ToString() + StringConstants.emptyStringValue });
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
                result.Add(entityType.ToString() + StringConstants.emptyStringValue);
            else
                result.Insert(0, $"Список {entityType.ToString()}: " + Environment.NewLine);

            return result;
        }

        public string RemoveEntity(EntityType entityType, int entityId)
        {
            CommonEntity entityToRemove;

            switch (entityType)
            {
                case EntityType.User:
                    entityToRemove = _DAO.GetEntities(EntityType.User).ElementAt(entityId - 1);
                    break;

                case EntityType.Award:
                    entityToRemove = _DAO.GetEntities(EntityType.Award).ElementAt(entityId - 1);
                    break;

                case EntityType.None:
                default:
                    return StringConstants.unsuccessfullOperationResult;
            }

            if (_DAO.DeleteEntity(entityToRemove))
                return StringConstants.successfullOperationResult;
            else
                return StringConstants.unsuccessfullOperationResult;
        }

        public string UpdateEntity(EntityType entityType, string entityData, List<int> connectedEntitiesIds)
        {
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
                    return StringConstants.unsuccessfullOperationResult;
            }

            if (_DAO.UpdateEntity(entityToUpdate))
                return StringConstants.successfullOperationResult;
            else
                return StringConstants.unsuccessfullOperationResult;
        }        

        public string AddEntity(EntityType entityType, string entityData, List<int> connectedEntitiesIds, string password)
        {
            CommonEntity entityToAdd;

            switch (entityType)
            {
                case EntityType.User:
                    entityToAdd = new User(new List<string>(entityData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)), connectedEntitiesIds, _DAO.UsersCount + 1);

                    StaticLogic.UpdateConnnectedEntities(connectedEntitiesIds, _DAO.GetEntities(EntityType.Award), entityToAdd.Id);
                    break;
                case EntityType.Award:
                    entityToAdd = new Award(entityData, connectedEntitiesIds, _DAO.AwardsCount + 1);

                    StaticLogic.UpdateConnnectedEntities(connectedEntitiesIds, _DAO.GetEntities(EntityType.User), entityToAdd.Id);
                    break;
                case EntityType.None:
                default:
                    return StringConstants.unsuccessfullOperationResult;
            }

            if (_DAO.AddEntity(entityToAdd, Identity.HashThePassword(password)))
                return StringConstants.successfullOperationResult;
            else
                return StringConstants.unsuccessfullOperationResult;
        }

    }
}

