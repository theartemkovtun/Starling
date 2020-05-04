using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Starling.Application.DTOs;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Files.Queries.GetFileContent
{
    public class GetFileContentQueryHandler : IRequestHandler<GetFileContentQuery, FileDto>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetFileContentQueryHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        
        public async Task<FileDto> Handle(GetFileContentQuery request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileContentAsync(request.FileId, request.Version, cancellationToken);

            return _mapper.Map<FileDto>(file);
        }
    }
}