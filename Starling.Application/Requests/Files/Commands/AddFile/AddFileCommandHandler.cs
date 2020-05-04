using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Starling.Domain.Enums;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Files.Commands.AddFile
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUserRepository _userRepository;

        public AddFileCommandHandler(IFileRepository fileRepository, IUserRepository userRepository)
        {
            _fileRepository = fileRepository;
            _userRepository = userRepository;
        }
        
        public async Task<Unit> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            var fileId = await _fileRepository.AddAsync(request.Filename, request.Content.ToArray(), cancellationToken);
            request.FileId = fileId;

            await _userRepository.AssignFileAsync(request.Username, request.FileId, UserFileStatus.Owner, cancellationToken);
            
            return Unit.Value;
        }
    }
}