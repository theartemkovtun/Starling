using System;
using System.IO;
using MediatR;
using Starling.Domain.Models;

namespace Starling.Application.Requests.Files.Commands.UpdateFile
{
    public class UpdateFileCommand : IRequest
    {
        public UpdateFileCommand(string username, MemoryStream content) 
        {
            Username = username;
            Content = content;
        }
        
        public Guid ShareId { get; set; }
        public string Username { get; set; }
        public Guid FileId { get; set; }
        public MemoryStream Content { get; set; }
        public string PrivateKeyPassword { get; set; }
    }
}