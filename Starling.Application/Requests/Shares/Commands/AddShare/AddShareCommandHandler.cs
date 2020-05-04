using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Starling.Aes;
using Starling.Aes.Models;
using Starling.Domain.Enums;
using Starling.Domain.Repositories.Contracts;
using Starling.Ecdsa;
using Starling.Shared.Extensions;

namespace Starling.Application.Requests.Shares.Commands.AddShare
{
    public class AddShareCommandHandler : IRequestHandler<AddShareCommand>
    {
        private readonly IShareRepository _shareRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAesEncryptor _encryptor;
        private readonly IEcdsa _ecdsa;

        public AddShareCommandHandler(IShareRepository shareRepository, IFileRepository fileRepository, IUserRepository userRepository, IAesEncryptor encryptor, IEcdsa ecdsa)
        {
            _shareRepository = shareRepository;
            _fileRepository = fileRepository;
            _userRepository = userRepository;
            _encryptor = encryptor;
            _ecdsa = ecdsa;
        }
        
        public async Task<Unit> Handle(AddShareCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(request.Sender, cancellationToken);

            if (user.PrivateKeyPassword != request.PrivateKeyPassword.Sha512())
            {
                throw new Exception("Invalid private key password");
            }
            
            var decryptionResponse = _encryptor.Decrypt(user.PrivateKey, request.PrivateKeyPassword.Sha256AsBytes());
            if (decryptionResponse.Status == Status.Failed)
            {
                throw new Exception();
            }
            
            request.Id = await _shareRepository.AddShareAsync(request.Title, request.Description, cancellationToken);
            
            await _shareRepository.AddShareUserAsync(request.Id, request.Sender, ShareUserOwnershipStatus.Sender, cancellationToken);
            foreach (var receiver in request.Receivers)
            {
                await _shareRepository.AddShareUserAsync(request.Id, receiver, ShareUserOwnershipStatus.Receiver, cancellationToken);
            }
            
            foreach (var file in request.Files)
            {
                var fileId = await _fileRepository.AddAsync(file.Name, file.Content, cancellationToken);
                await _userRepository.AssignFileAsync(request.Sender, fileId, UserFileStatus.Owner, cancellationToken);
                await _shareRepository.AddShareFileAsync(request.Id, fileId, cancellationToken);
                
                var signResponse = await _ecdsa.SignFileAsync(file.Name, file.Content, decryptionResponse.DecryptedText);
                await _shareRepository.AddShareFileUserAsync(request.Id, fileId, request.Sender,
                    signResponse.Signature, cancellationToken);
            }
            
            await _shareRepository.AcceptAsync(request.Id, request.Sender);

            return Unit.Value;
        }
    }
}