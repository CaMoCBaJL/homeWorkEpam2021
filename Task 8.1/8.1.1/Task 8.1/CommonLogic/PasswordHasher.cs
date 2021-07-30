using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonLogic
{
    public class PasswordHasher
    {
        public string HashThePassword(string password)
        {
            HashAlgorithm sha = SHA256.Create();

            StringBuilder result = new StringBuilder();

            foreach (var hashValue in sha.ComputeHash(Encoding.UTF8.GetBytes(password)))
            {
                result.Append(hashValue + " ");
            }

            return result.ToString();
        }
    }
}
