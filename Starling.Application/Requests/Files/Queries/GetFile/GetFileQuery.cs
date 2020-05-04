using System;
using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Files.Queries.GetFile
{
    public class GetFileQuery : IRequest<FileDto>
    {
        public GetFileQuery(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}