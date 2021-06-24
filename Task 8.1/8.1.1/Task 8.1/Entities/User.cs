using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Entities
{
    public class User : CommonEntity
    {
        static int userCount = 0;

        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public string DateOfBirth { get; private set; }

        [JsonProperty]
        public int Age { get; private set; }

        [JsonProperty]
        public List<int> UserAwards { get; private set; }


        public User(){ }

        public User(string name, string dateOfBirth, int age, List<int> userAwards, int id) : base(id)
        {
            Name = name;

            DateOfBirth = dateOfBirth;

            Age = age;

            UserAwards = userAwards;

            userCount++;
        }

        public User(List<string> userData, List<int> awards) : base(userCount++)
        {
            Name = userData[0];

            DateOfBirth = userData[1];

            Age = int.Parse(userData[2]);

            UserAwards = awards;
        }

        public void AddAward(Award award) => UserAwards.Add(award.Id);

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            res.Append("Имя пользователя: " + Name + System.Environment.NewLine);

            res.Append(base.ToString());

            res.Append("Возраст пользователя: " + Age + System.Environment.NewLine);

            res.Append("Дата рождения пользователя: " + DateOfBirth + System.Environment.NewLine);

            res.Append(System.Environment.NewLine + "Cписок наград:" + System.Environment.NewLine);

            if (UserAwards.Count > 0)
            {
                foreach (var award in UserAwards)
                {
                    res.Append(award.ToString() + System.Environment.NewLine);
                }
            }
            else
                res.Append("Наград пока нет.");
            return res.ToString();
        }

    }    
}
