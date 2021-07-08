using System.Collections.Generic;
using System.Linq;
using Entities;
using DALInterfaces;
using System.Text.RegularExpressions;
using BLInterfaces;
using DataValidation;

namespace BL
{
    class UserLogic : ILogicLayer
    {
        IDataLayer _DAO { get; }


        public UserLogic(IDataLayer dataLayer) => _DAO = dataLayer;

        bool ILogicLayer.AddEntity(List<string> dataToAdd, List<int> connectedEntitiesIds, string password)
        {
            if (ValidateEntityData(dataToAdd).StartsWith("All"))
            {
                return _DAO.AddEntity(new User(dataToAdd, connectedEntitiesIds), password);
            }

            return false;
        }

        List<string> ILogicLayer.GetConnectedEntities(List<int> addedEntities)
        {
            List<string> result = new List<string>();

            foreach (var entity in _DAO.GetConnectedEntities())
            {
                result.Add(entity.ToString());
            }

            return result;
        }
        List<string> ILogicLayer.GetConnectedEntitiesNames()
        {
            List<string> result = new List<string>();

            foreach (var entity in _DAO.GetConnectedEntities())
            {
                result.Add((entity as Award).Title);
            }

            return result;
        }

        public string FindEntity(string entityName)
        {
            if (_DAO.GetEntityId(entityName) != -1)
                return _DAO.GetEntities().ElementAt(_DAO.GetEntityId(entityName)).ToString();

            return "Не было найдено пользователей с указанным названием.";
        }

        bool ILogicLayer.RemoveEntity(int entityId)
            => _DAO.RemoveEntity(entityId);

        bool ILogicLayer.UpdateEntity(List<string> dataToUpdate, List<int> newConnectedEntitiesIds)
        {
            if (ValidateEntityData(dataToUpdate).StartsWith("All"))
            {
                return _DAO.UpdateEntity(new Award(dataToUpdate, newConnectedEntitiesIds));
            }

            return false;
        }

        public string ValidateEntityData(List<string> entityData)
            => new Validator().ValidateUser(entityData);

        public List<string> GetEntities()
        {
            List<string> result = new List<string>();

            foreach (var item in _DAO.GetEntities())
            {
                result.Add(item.ToString());
            }

            return result;
        }


    }
}
