﻿using System.Security.Cryptography;
using System.Text;

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
