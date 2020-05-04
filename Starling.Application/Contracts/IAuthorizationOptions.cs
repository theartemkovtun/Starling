using Microsoft.IdentityModel.Tokens;

namespace Starling.Application.Contracts
{
    public interface IAuthorizationOptions
    {
        string Issuer { get; set; }
        string Audience { get; set; }
        string Key { get; set; }
        int Lifetime { get; set; }
        SymmetricSecurityKey SymmetricSecurityKey { get; }
    }
}