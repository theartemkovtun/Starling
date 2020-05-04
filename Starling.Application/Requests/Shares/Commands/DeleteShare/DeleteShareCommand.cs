using System;
using MediatR;

namespace Starling.Application.Requests.Shares.Commands.DeleteShare
{
    public class DeleteShareCommand : IRequest
    {
        public DeleteShareCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}