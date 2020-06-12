using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Starling.Aes;
using Starling.Aes.Models;
using Starling.Domain.Models;
using Starling.Domain.Repositories.Contracts;
using Starling.Ecdsa;
using Starling.Shared.Extensions;

namespace Starling.Application.Requests.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, File>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterUserCommand> _validator;
        private readonly IEcdsa _ecdsa;
        private readonly IAesEncryptor _encryptor;

        public RegisterUserCommandHandler(IUserRepository userRepository, IValidator<RegisterUserCommand> validator, IEcdsa ecdsa, IAesEncryptor encryptor)
        {
            _userRepository = userRepository;
            _validator = validator;
            _ecdsa = ecdsa;
            _encryptor = encryptor;
        }
        
        public async Task<File> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, null, cancellationToken);

            var keys = await _ecdsa.GetKeysAsync();
            
            var encryptedPrivateKey = string.Empty;
            var encryptionResponse = _encryptor.Encrypt(keys.PrivateKey, request.PrivateKeyPassword.Sha256AsBytes());
            if (encryptionResponse.Status == Status.Success)
            {
                encryptedPrivateKey = encryptionResponse.EncryptedText;
            }
            
            var user = new User
            {
                Username = request.Username,
                Password = request.Password.Sha512(),
                PublicKey = keys.PublicKey,
                PrivateKey = encryptedPrivateKey,
                PrivateKeyPassword = request.PrivateKeyPassword.Sha512()
            };
            
            await _userRepository.AddAsync(user, cancellationToken);

            return new File
            {
                Name = "private.sk"
            };
        }
    }
}