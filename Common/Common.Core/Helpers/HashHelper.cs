using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Core.Helpers
{
    public static class HashHelper
    {
        public static string ConvertFromBase64String(string base64String)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
        }
        public static byte[] ConvertPasswordToHash(string password)
        {
            return GenerateMd5($"scrat-{password}");
        }
        private static byte[] GenerateMd5(string value)
        {
            using var hash = MD5.Create();
            var enc = Encoding.UTF8;
            return hash.ComputeHash(enc.GetBytes(value));
        }
    }
}
