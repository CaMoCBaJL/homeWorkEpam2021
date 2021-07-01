using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Entities
{
    public class Identity
    {
        [JsonProperty]
        public int UserId { get; private set;}

        [JsonProperty]
        public int PasswordHashSumm { get; private set; }


        public Identity() { }

        public Identity(int userId, int hashSumm)
        {
            UserId = userId;

            PasswordHashSumm = hashSumm;
        }

        public static int HashThePassword(string password)
        {
            HashAlgorithm sha = SHA256.Create();

            int result = 0;

            foreach (var hashValue in sha.ComputeHash(Encoding.UTF8.GetBytes(password)))
            {
                result += hashValue;
            }

            return result;
        }
    }
}
