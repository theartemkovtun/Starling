using System;
using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Shares.Queries.GetShare
{
    public class GetShareQuery : IRequest<ShareForUserDto>
    {
        public GetShareQuery(string username, Guid shareId)
        {
            Username = username;
            ShareId = shareId;
        }
        
        public string Username { get; set; }
        public Guid ShareId { get; set; }
    }
}