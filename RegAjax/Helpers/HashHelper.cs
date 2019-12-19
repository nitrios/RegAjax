using System;
using System.Security.Cryptography;
using System.Text;

namespace RegAjax.Helpers
{
    public static class HashHelper
    {
        public static string GenerateHash(string plainText)
        {
            return GenerateHash(Encoding.Default.GetBytes(plainText));
        }
        
        public static string GenerateHash(byte[] plainText)
        {
            byte[] result;

            using (HashAlgorithm algorithm = new SHA256Managed()) 
                result = algorithm.ComputeHash(plainText);

            return BitConverter.ToString(result).Replace("-", "");
        }
    }
}