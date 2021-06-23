using System.Collections.Generic;

namespace Entities
{
    public class User : CommonEntity
    {
        public string Name { get; }

        public string DateOfBirth { get; }

        public int Age { get; private set; }

        public List<Award> UserAwards { get; }


        protected User() { }

        public User(string name, string dateOfBirth, int age) : base()
        {
            Name = name;

            DateOfBirth = dateOfBirth;

            Age = age;

            UserAwards = new List<Award>();
        }

        public User(List<string> userData) : base()
        {
            Name = userData[0];

            DateOfBirth = userData[1];

            Age = int.Parse(userData[2]);

            UserAwards = new List<Award>();
        }

        public void AddAward(Award award) => UserAwards.Add(award);
    }
}
