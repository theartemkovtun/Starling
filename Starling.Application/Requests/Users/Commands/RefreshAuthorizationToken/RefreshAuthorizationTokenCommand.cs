using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Users.Commands.RefreshAuthorizationToken
{
    public class RefreshAuthorizationTokenCommand : IRequest<TokenDto>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}