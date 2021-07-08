using Newtonsoft.Json;

namespace Entities
{
    public class Identity
    {
        [JsonProperty]
        public int UserId { get; private set;}

        [JsonProperty]
        public string PasswordHashSumm { get; private set; }


        public Identity() { }

        public Identity(int userId, string hashSumm)
        {
            UserId = userId;

            PasswordHashSumm = hashSumm;
        }
    }
}
