using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public abstract class CommonEntity
    {
        [JsonProperty]
        public int Id { get; private set; }
        [JsonProperty]
        public List<int> ConnectedEntities { get; protected set; }


        public CommonEntity() { ConnectedEntities = new List<int>(); }

        public CommonEntity(int id)
        {
            Id = id;

            ConnectedEntities = new List<int>();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"{Environment.NewLine}ID: {Id} {Environment.NewLine}");

            if (ConnectedEntities.Count > 0)
            {
                result.Append($"{Environment.NewLine}Список связанных сущностей({CalculateAdditionalEntityName()}): {Environment.NewLine}");

                foreach (var item in ConnectedEntities)
                {
                    result.Append(item + Environment.NewLine);
                }
            }
            else
                result.Append($"{Environment.NewLine}Список связанных сущностей пока пуст. {Environment.NewLine}");

            return result.ToString();
        }

        string CalculateAdditionalEntityName()
        {
            if (GetType().Name == "User")
                return "Award";

            return "User";
        }

        public void AddConnectedEntity(int id) => ConnectedEntities.Add(id);

        public void ChangeId(int id) => Id = id;
    }
}
