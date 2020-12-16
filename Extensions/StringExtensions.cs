using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtensions
    {
        public static (string, string) GenerateSaltAndHashPassword(this string plainText)
        {
            byte[] password = Encoding.UTF8.GetBytes(plainText);

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[128];
            rng.GetBytes(salt);

            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = password[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            string saltedPassword = Convert.ToBase64String(algorithm.ComputeHash(plainTextWithSaltBytes));
            string saltBase64 = Convert.ToBase64String(salt);

            return (saltedPassword, saltBase64);
        }

        public static string SaltPassword(this string plainText, string saltText)
        {
            byte[] password = Encoding.UTF8.GetBytes(plainText);

            byte[] salt = Convert.FromBase64String(saltText);

            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = password[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return Convert.ToBase64String(algorithm.ComputeHash(plainTextWithSaltBytes));
        }
    }
}
