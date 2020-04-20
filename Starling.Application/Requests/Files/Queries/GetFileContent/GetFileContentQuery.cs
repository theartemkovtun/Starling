using System;
using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Files.Queries.GetDocumentContent
{
    public class GetDocumentContentQuery : IRequest<FileDto>
    {
        public GetDocumentContentQuery(Guid fileId)
        {
            FileId = fileId;
        }
        
        public GetDocumentContentQuery(Guid fileId, int version)
        {
            FileId = fileId;
            Version = version;
        }
        
        public Guid FileId { get; set; }
        public int? Version { get; set; }
    }
}