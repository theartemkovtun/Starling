using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Starling.Application.Contracts.Repositories;
using Starling.Application.Requests.Files.Commands.AddFile;

namespace Starling.Application.Requests.Documents.Commands.AddDocument
{
    public class AddDocumentCommandHandler : IRequestHandler<AddFileCommand>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        
        public AddDocumentCommandHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            await _fileRepository.AddAsync(request.Filename, request.Content.ToArray(), cancellationToken);
            
            return Unit.Value;
        }
    }
}