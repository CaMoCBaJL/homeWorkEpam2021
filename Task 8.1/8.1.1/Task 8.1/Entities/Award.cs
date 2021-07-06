using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Entities
{
    public class Award : CommonEntity
    {
        [JsonProperty]
        public string Title { get; private set; }


        protected Award() {}

        public Award(int id, string title, List<int> connectedEntities) : base(id)
        {
            Title = title;

            ConnectedEntities = connectedEntities;
        }

        public Award(List<string> entityData, List<int> connectedIds) : base(int.Parse(entityData[0]))
        {
            Title = entityData[1];

            ConnectedEntities = connectedIds;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("Название награды: " + Title + Environment.NewLine);

            result.Append(base.ToString());

            return result.ToString();
        }

        public void AddAwardedUser(User user) => ConnectedEntities.Add(user.Id);

        public void AddAwardedUser(int id) => ConnectedEntities.Add(id);
    }
}
