using System;
using System.Collections.Generic;
using System.IO;
using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Shares.Commands.AddShare
{
    public class AddShareCommand : IRequest
    {
        public AddShareCommand(string sender)
        {
            Sender = sender;
        }
        
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sender { get; set; }
        public string PrivateKeyPassword { get; set; }
        public IList<string> Receivers { get; set; }
        public IList<FileDataDto> Files { get; set; } = new List<FileDataDto>();
    }
}