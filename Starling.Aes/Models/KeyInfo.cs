using System;
using aes = System.Security.Cryptography.Aes;

namespace Starling.Aes.Models
{
    public class KeyInfo
    {
        public byte[] Key { get; }
        public byte[] Iv { get; }

        public string KeyString => Convert.ToBase64String(Key);
        public string IvString => Convert.ToBase64String(Iv);

        public KeyInfo()
        {
            using var myAes = aes.Create();
            Key = myAes?.Key;
            Iv = myAes?.IV;
        }
        public KeyInfo(byte[] key, byte[] iv)
        {
            Key = key;
            Iv = iv;
        }
        public KeyInfo(string key, string iv)
        {
            Key = Convert.FromBase64String(key);
            Iv = Convert.FromBase64String(iv);
        }
    }
}