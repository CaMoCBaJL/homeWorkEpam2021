using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class User : CommonEntity
    {
        static int userCount = 0;


        public string Name { get;}

        public string DateOfBirth { get;}

        public int Age { get;}

        public List<int> UserAwards { get;}


        static User() => userCount++;


        public User() : base(userCount) { }

        public User(string name, string dateOfBirth, int age, List<int> userAwards, int id) : base(id)
        {
            Name = name;

            DateOfBirth = dateOfBirth;

            Age = age;

            UserAwards = userAwards;
        }

        public User(List<string> userData) : base(userCount)
        {
            Name = userData[0];

            DateOfBirth = userData[1];

            Age = int.Parse(userData[2]);

            UserAwards = new List<int>();
        }

        public void AddAward(Award award) => UserAwards.Add(award.Id);

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            res.Append("Имя пользователя: " + Name + System.Environment.NewLine);

            res.Append("Возраст пользователя: " + Age + System.Environment.NewLine);

            res.Append("Дата рождения пользователя: " + DateOfBirth + System.Environment.NewLine);

            res.Append("Cписок наград:"+System.Environment.NewLine);

            foreach (var award in UserAwards)
            {
                res.Append(award.ToString() + System.Environment.NewLine);
            }

            return res.ToString();
        }

        public int Last { get => userCount; }
    }
}
