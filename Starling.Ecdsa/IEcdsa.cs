using System.IO;
using System.Threading.Tasks;
using Starling.Ecdsa.Models;

namespace Starling.Ecdsa
{
    public interface IEcdsa
    {
        Task<EcdsaKeys> GetKeysAsync();
        Task<SignResponse> SignFileAsync(string filename, byte[] content, string privateKey);
        Task<VerificationResponse> VerifySignature(string filename, byte[] content, string signature, string publicKey);
    }
}