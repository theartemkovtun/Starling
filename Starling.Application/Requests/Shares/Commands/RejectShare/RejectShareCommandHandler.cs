using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Shares.Commands.RejectShare
{
    public class RejectShareCommandHandler : IRequestHandler<RejectShareCommand>
    {
        private readonly IShareRepository _shareRepository;

        public RejectShareCommandHandler(IShareRepository shareRepository)
        {
            _shareRepository = shareRepository;
        }
        
        public async Task<Unit> Handle(RejectShareCommand request, CancellationToken cancellationToken)
        {
            await _shareRepository.RejectAsync(request.ShareId, request.Username);
            
            return Unit.Value;
        }
    }
}