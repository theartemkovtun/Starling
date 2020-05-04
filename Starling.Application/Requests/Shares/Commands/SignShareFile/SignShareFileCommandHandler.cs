using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Starling.Aes;
using Starling.Aes.Models;
using Starling.Domain.Repositories.Contracts;
using Starling.Ecdsa;
using Starling.Shared.Extensions;

namespace Starling.Application.Requests.Shares.Commands.SignShareFile
{
    public class SignShareFileCommandHandler : IRequestHandler<SignShareFileCommand>
    {
        private readonly IShareRepository _shareRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IAesEncryptor _encryptor;
        private readonly IEcdsa _ecdsa;

        public SignShareFileCommandHandler(IShareRepository shareRepository, IUserRepository userRepository, IFileRepository fileRepository, IAesEncryptor encryptor, IEcdsa ecdsa)
        {
            _shareRepository = shareRepository;
            _userRepository = userRepository;
            _fileRepository = fileRepository;
            _encryptor = encryptor;
            _ecdsa = ecdsa;
        }
        
        public async Task<Unit> Handle(SignShareFileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(request.Username, cancellationToken);

            if (user.PrivateKeyPassword != request.PrivateKeyPassword.Sha512())
            {
                throw new Exception();
            }

            var decryptionResponse = _encryptor.Decrypt(user.PrivateKey, request.PrivateKeyPassword.Sha256AsBytes());
            if (decryptionResponse.Status == Status.Failed)
            {
                throw new Exception();
            }
            
            var file = await _fileRepository.GetFileContentAsync(request.FileId, null, cancellationToken);
            var signResponse = await _ecdsa.SignFileAsync(file.Name, file.Content, decryptionResponse.DecryptedText);

            await _shareRepository.AddShareFileUserAsync(request.ShareId, request.FileId, request.Username,
                signResponse.Signature, cancellationToken);

            return Unit.Value;
        }
    }
}