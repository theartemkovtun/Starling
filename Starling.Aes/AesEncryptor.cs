using System;
using System.IO;
using System.Security.Cryptography;
using Starling.Aes.Models;
using aes = System.Security.Cryptography.Aes;

namespace Starling.Aes
{
    public class AesEncryptor : IAesEncryptor
    {
        private readonly KeyInfo _keyInfo;

        public AesEncryptor()
        {
            _keyInfo = new KeyInfo("45BLO2yoJkvBwz99kBEMlNkxvL40vUSGaqr/WBu3+Vg=", "Ou3fn+I9SVicGWMLkFEgZQ==");
        }
        
        public EncryptionResponse Encrypt(string source, byte[] key = null)
        {
            try
            {
                byte[] encryptedBytes;

                using (var aesAlg = aes.Create())
                {
                    aesAlg.Key = key ?? _keyInfo.Key;
                    aesAlg.IV = _keyInfo.Iv;

                    var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(source);
                            }

                            encryptedBytes = msEncrypt.ToArray();
                        }
                    }
                }

                return new EncryptionResponse
                {
                    Status = Status.Success,
                    EncryptedText = Convert.ToBase64String(encryptedBytes)
                };
            }
            catch (Exception)
            {
                return new EncryptionResponse
                {
                    Status = Status.Failed
                };
            }

        } 
        
        public DecryptionResponse Decrypt(string decryptedSource, byte[] key = null)
        {
            try
            {
                var cipherBytes = Convert.FromBase64String(decryptedSource);

                string plaintext;

                using (var aesAlg = aes.Create())
                {
                    aesAlg.Key = key ?? _keyInfo.Key;
                    aesAlg.IV = _keyInfo.Iv;

                    var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var msDecrypt = new MemoryStream(cipherBytes))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                }

                return new DecryptionResponse
                {
                    Status = Status.Success,
                    DecryptedText = plaintext
                };
            }
            catch (Exception)
            {
                return new DecryptionResponse
                {
                    Status = Status.Failed
                };
            }

        }
    }
}