using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Entities
{
    public class Award : CommonEntity
    {
        static int awardCount = 0;

        [JsonProperty]
        public string Title { get; private set; }

        [JsonProperty]
        public List<int> AwardedUsers { get; protected set; }


        protected Award() : base(awardCount++) {}

        public Award(string title, List<int> awardedUsers) : base(awardCount++)
        {
            Title = title;

            AwardedUsers = awardedUsers;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("Название награды: " + Title + Environment.NewLine);

            result.Append(base.ToString());

            if (AwardedUsers.Count > 0)
            {
                result.Append(Environment.NewLine + "Список награжденных пользователей:" +Environment.NewLine);

                foreach (var user in AwardedUsers)
                {
                    result.Append(user.ToString() + Environment.NewLine);
                }
            }
            else
                result.Append(Environment.NewLine + "Пока ни 1 пользователь не обладает данной наградой.");

            return result.ToString();
        }

        public void AddAwardedUser(User user) => AwardedUsers.Add(user.Id);

        public void AddAwardedUser(int id) => AwardedUsers.Add(id);
    }
}
