using System;
using MediatR;

namespace Starling.Application.Requests.Shares.Commands.SignShareFile
{
    public class SignShareFileCommand : IRequest
    {
        public SignShareFileCommand(string username)
        {
            Username = username;
        }
        
        public Guid ShareId { get; set; }
        public Guid FileId { get; set; }
        public string Username { get; set; }
        public string PrivateKeyPassword { get; set; }
    }
}