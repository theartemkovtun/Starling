using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Shares.Commands.DeleteShare
{
    public class DeleteShareCommandHandler : IRequestHandler<DeleteShareCommand>
    {
        private readonly IShareRepository _shareRepository;

        public DeleteShareCommandHandler(IShareRepository shareRepository)
        {
            _shareRepository = shareRepository;
        }
        
        public async Task<Unit> Handle(DeleteShareCommand request, CancellationToken cancellationToken)
        {
            await _shareRepository.DeleteAsync(request.Id);
            
            return Unit.Value;
        }
    }
}