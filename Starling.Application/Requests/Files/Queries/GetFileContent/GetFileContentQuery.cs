using System;
using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Files.Queries.GetFileContent
{
    public class GetFileContentQuery : IRequest<FileDto>
    {
        public GetFileContentQuery(Guid fileId)
        {
            FileId = fileId;
        }
        
        public GetFileContentQuery(Guid fileId, int version)
        {
            FileId = fileId;
            Version = version;
        }
        
        public Guid FileId { get; set; }
        public int? Version { get; set; }
    }
}