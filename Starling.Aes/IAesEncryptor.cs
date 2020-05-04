using Starling.Aes.Models;

namespace Starling.Aes
{
    public interface IAesEncryptor
    {
        EncryptionResponse Encrypt(string source, byte[] key = null);
        DecryptionResponse Decrypt(string decryptedSource, byte[] key = null);
    }
}