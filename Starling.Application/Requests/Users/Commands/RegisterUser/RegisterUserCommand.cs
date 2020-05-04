using MediatR;
using Starling.Domain.Models;

namespace Starling.Application.Requests.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<File>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
        
        public string PrivateKeyPassword { get; set; }
        public string PrivateKeyPasswordRepeat { get; set; }
    }
}