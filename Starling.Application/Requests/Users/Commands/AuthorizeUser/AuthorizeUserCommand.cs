using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Users.Commands.AuthorizeUser
{
    public class AuthorizeUserCommand : IRequest<TokenDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}