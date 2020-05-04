using System;
using MediatR;

namespace Starling.Application.Requests.Shares.Commands.AcceptShare
{
    public class AcceptShareCommand : IRequest
    {
        public AcceptShareCommand(Guid shareId, string username)
        {
            ShareId = shareId;
            Username = username;
        }
        
        public Guid ShareId { get; set; }
        public string Username { get; set; }
    }
}