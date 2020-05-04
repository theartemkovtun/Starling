using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Users.Commands.LogoutUser
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public LogoutUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteRefreshToken(request.Username, request.RefreshToken);
            
            return Unit.Value;
        }
    }
}