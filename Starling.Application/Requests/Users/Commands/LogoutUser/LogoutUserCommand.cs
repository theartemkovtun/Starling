using MediatR;

namespace Starling.Application.Requests.Users.Commands.LogoutUser
{
    public class LogoutUserCommand : IRequest
    {
        public LogoutUserCommand(string username)
        {
            Username = username;
        }
        
        public string Username { get; set; }
        public string RefreshToken { get; set; }
    }
}