using System;
using System.IO;
using MediatR;

namespace Starling.Application.Requests.Files.Commands.AddFile
{
    public class AddFileCommand : IRequest
    {
        public AddFileCommand(string filename, MemoryStream content, string username)
        {
            Filename = filename;
            Content = content;
            Username = username;
        }
        
        public Guid FileId { get; set; }
        public string Filename { get; set; }
        public MemoryStream Content { get; set; }
        public string Username { get; set; }
    }
}