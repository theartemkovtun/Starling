using System.Text;
using Microsoft.IdentityModel.Tokens;
using Starling.Application.Contracts;

namespace Starling.WebApi.ViewModels
{
    public class AuthorizationOptions : IAuthorizationOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Lifetime { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}