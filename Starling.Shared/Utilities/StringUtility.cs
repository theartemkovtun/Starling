using System;
using System.Security.Cryptography;

namespace Starling.Shared.Utilities
{
    public static class StringUtility
    {
        public static string GetRandomStringKey()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}