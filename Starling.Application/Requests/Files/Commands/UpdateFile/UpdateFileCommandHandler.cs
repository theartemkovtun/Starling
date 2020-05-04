using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Starling.Aes;
using Starling.Aes.Models;
using Starling.Domain.Repositories.Contracts;
using Starling.Ecdsa;
using Starling.Shared.Extensions;

namespace Starling.Application.Requests.Files.Commands.UpdateFile
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand>
    {
        private readonly IShareRepository _shareRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAesEncryptor _encryptor;
        private readonly IEcdsa _ecdsa;

        public UpdateFileCommandHandler(IShareRepository shareRepository, IFileRepository fileRepository, IUserRepository userRepository, IAesEncryptor encryptor, IEcdsa ecdsa)
        {
            _shareRepository = shareRepository;
            _fileRepository = fileRepository;
            _userRepository = userRepository;
            _encryptor = encryptor;
            _ecdsa = ecdsa;
        }
        
        public async Task<Unit> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(request.Username, cancellationToken);

            if (user.PrivateKeyPassword != request.PrivateKeyPassword.Sha512())
            {
                throw new Exception("Invalid private key password");
            }
            
            var decryptionResponse = _encryptor.Decrypt(user.PrivateKey, request.PrivateKeyPassword.Sha256AsBytes());
            if (decryptionResponse.Status == Status.Failed)
            {
                throw new Exception("Error while trying to decrypt private key");
            }
            
            await _fileRepository.UpdateAsync(request.FileId, request.Content.ToArray(), cancellationToken);

            var file = await _fileRepository.FirstOrDefaultAsync(request.FileId, cancellationToken);

            var signResponse = await _ecdsa.SignFileAsync(file.Name, request.Content.ToArray(), decryptionResponse.DecryptedText);
            await _shareRepository.AddShareFileUserAsync(request.ShareId, file.Id, request.Username,
                signResponse.Signature, cancellationToken);
            
            return Unit.Value;
        }
    }
}