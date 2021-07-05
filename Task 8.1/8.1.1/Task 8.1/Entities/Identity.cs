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
        public string PasswordHashSumm { get; private set; }


        public Identity() { }

        public Identity(int userId, string hashSumm)
        {
            UserId = userId;

            PasswordHashSumm = hashSumm;
        }

        public static string HashThePassword(string password)
        {
            HashAlgorithm sha = SHA256.Create();

            StringBuilder result = new StringBuilder();

            foreach (var hashValue in sha.ComputeHash(Encoding.UTF8.GetBytes(password)))
            {
                result.Append(hashValue);
            }

            return result.ToString();
        }
    }
}
