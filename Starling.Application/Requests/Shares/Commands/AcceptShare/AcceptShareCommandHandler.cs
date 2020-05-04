using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Shares.Commands.AcceptShare
{
    public class AcceptShareCommandHandler : IRequestHandler<AcceptShareCommand>
    {
        private readonly IShareRepository _shareRepository;

        public AcceptShareCommandHandler(IShareRepository shareRepository)
        {
            _shareRepository = shareRepository;
        }
        
        public async Task<Unit> Handle(AcceptShareCommand request, CancellationToken cancellationToken)
        {
            await _shareRepository.AcceptAsync(request.ShareId, request.Username);
            
            return Unit.Value;
        }
    }
}