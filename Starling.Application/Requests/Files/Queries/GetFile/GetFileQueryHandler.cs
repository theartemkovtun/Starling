using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Starling.Application.DTOs;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Files.Queries.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileDto>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetFileQueryHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        
        public async Task<FileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var document = await _fileRepository.FirstOrDefaultAsync(request.Id, cancellationToken);

            return _mapper.Map<FileDto>(document);
        }
    }
}