using System;
using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Documents.Queries.GetDocument
{
    public class GetDocumentQuery : IRequest<DocumentDto>
    {
        public GetDocumentQuery(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}