using System;
using MediatR;

namespace Starling.Application.Requests.Shares.Commands.RejectShare
{
    public class RejectShareCommand : IRequest
    {
        public RejectShareCommand(Guid shareId, string username)
        {
            ShareId = shareId;
            Username = username;
        }
        
        public Guid ShareId { get; set; }
        public string Username { get; set; }
    }
}