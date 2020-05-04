using System.Collections.Generic;
using MediatR;
using Starling.Application.DTOs;
using Starling.Domain.Enums;

namespace Starling.Application.Requests.Files.Queries.GetFiles
{
    public class GetFilesQuery : IRequest<FilesSearchResult>
    {
        public GetFilesQuery(string username)
        {
            Username = username;
        }
        
        public string Username { get; set; }
        public UserFileStatus? OwnershipStatus { get; set; }
        public int? Page { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }
    }
}