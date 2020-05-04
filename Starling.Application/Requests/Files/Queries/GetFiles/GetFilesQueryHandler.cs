using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Starling.Application.DTOs;
using Starling.Domain.Models.Filters;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Files.Queries.GetFiles
{
    public class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, FilesSearchResult>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetFilesQueryHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        
        public async Task<FilesSearchResult> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            var filters = _mapper.Map<GetFilesFilter>(request);
            var files = await _fileRepository.GetFiles(filters);

            return new FilesSearchResult
            {
                Files = _mapper.Map<IList<FileDto>>(files),
                Total = files.FirstOrDefault()?.TotalQuantity ?? 0
            };
        }
    }
}