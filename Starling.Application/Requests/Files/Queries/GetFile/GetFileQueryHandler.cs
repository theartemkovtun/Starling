using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Starling.Application.Contracts.Repositories;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Documents.Queries.GetDocument
{
    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, DocumentDto>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetDocumentQueryHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        
        public async Task<DocumentDto> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            var document = await _fileRepository.FirstOrDefaultAsync(request.Id, cancellationToken);

            return _mapper.Map<DocumentDto>(document);
        }
    }
}