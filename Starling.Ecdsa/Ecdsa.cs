using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Starling.Ecdsa.Models;

namespace Starling.Ecdsa
{
    public class Ecdsa : IEcdsa
    {
        private string BaseUrl { get; set; } = "http://localhost:5000/api";

        public async Task<EcdsaKeys> GetKeysAsync()
        {
            using var http = new HttpClient();
            var response = await http.GetStringAsync($"{BaseUrl}/keys");
            return JsonConvert.DeserializeObject<EcdsaKeys>(response);
        }

        public async Task<SignResponse> SignFileAsync(string filename, byte[] content, string privateKey)
        {
            using var http = new HttpClient();
            var response = await http.PostAsync($"{BaseUrl}/sign/file", new MultipartFormDataContent
            {
                {new StreamContent(new MemoryStream(content)), "file", filename}, 
                {new StringContent(privateKey), "private"}
            });
            return JsonConvert.DeserializeObject<SignResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<VerificationResponse> VerifySignature(string filename, byte[] content, string signature, string publicKey)
        {
            using var http = new HttpClient();
            var response = await http.PostAsync($"{BaseUrl}/verify/file", new MultipartFormDataContent
            {
                {new StreamContent(new MemoryStream(content)), "file", filename}, 
                {new StringContent(publicKey), "public"},
                {new StringContent(signature), "signature"}
            });
            return JsonConvert.DeserializeObject<VerificationResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}