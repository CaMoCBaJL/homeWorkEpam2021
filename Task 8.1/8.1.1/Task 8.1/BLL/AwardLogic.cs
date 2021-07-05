using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLInterfaces;
using DALInterfaces;
using Entities;

namespace BL
{
    public class AwardLogic : ILogicLayer
    {
        IDataLayer _DAO { get; }


        public AwardLogic(IDataLayer dataLayer) => _DAO = dataLayer;

        bool ILogicLayer.AddEntity(List<string> dataToAdd, List<int> connectedEntitiesIds, string password)
        {
            if (ValidateEntityData(dataToAdd))
            {
                return _DAO.AddEntity(new Award(dataToAdd, connectedEntitiesIds), password);
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
                result.Add((entity as User).Name);
            }

            return result;
        }

        bool ILogicLayer.RemoveEntity(int entityId)
            => _DAO.RemoveEntity(entityId);

        bool ILogicLayer.UpdateEntity(List<string> dataToUpdate, List<int> newConnectedEntitiesIds)
        {
            if (ValidateEntityData(dataToUpdate))
            {
                return _DAO.UpdateEntity(new Award(dataToUpdate, newConnectedEntitiesIds));
            }

            return false;
        }

        public bool ValidateEntityData(List<string> entityData)
        => true;
    }
}
