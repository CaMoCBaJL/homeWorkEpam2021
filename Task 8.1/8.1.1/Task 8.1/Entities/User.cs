using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Entities
{
    public class User : CommonEntity
    {
        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public string DateOfBirth { get; private set; }

        [JsonProperty]
        public int Age { get; private set; }


        public User(){}

        public User(string name, string dateOfBirth, int age, List<int> userAwards, int id) : base(id)
        {
            Name = name;

            DateOfBirth = dateOfBirth;

            Age = age;

            ConnectedEntities = userAwards;
        }

        public User(List<string> userData, List<int> awards, int id) : base(id)
        {
            Name = userData[0];

            DateOfBirth = userData[1];

            Age = int.Parse(userData[2]);

            ConnectedEntities = awards;
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            res.Append("Имя пользователя: " + Name + System.Environment.NewLine);

            res.Append("Возраст пользователя: " + Age + System.Environment.NewLine);

            res.Append("Дата рождения пользователя: " + DateOfBirth + System.Environment.NewLine);

            res.Append(base.ToString());
            
            return res.ToString();
        }

    }    
}
