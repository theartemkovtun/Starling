using System.IO;
using MediatR;

namespace Starling.Application.Requests.Documents.Commands.AddDocument
{
    public class AddDocumentCommand : IRequest
    {
        public AddDocumentCommand(string filename, MemoryStream content)
        {
            Filename = filename;
            Content = content;
        }
        
        public string Filename { get; set; }
        public MemoryStream Content { get; set; }
    }
}