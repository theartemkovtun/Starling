using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Starling.Application.DTOs;
using Starling.Domain.Repositories.Contracts;
using Starling.Ecdsa;
using Starling.Ecdsa.Enums;

namespace Starling.Application.Requests.Shares.Commands.VerifySignatures
{
    public class VerifySignaturesCommandHandler : IRequestHandler<VerifySignaturesCommand,SignaturesVerificationResponseDto>
    {
        private readonly IShareRepository _shareRepository;
        private readonly IEcdsa _ecdsa;

        public VerifySignaturesCommandHandler(IShareRepository shareRepository, IEcdsa ecdsa)
        {
            _shareRepository = shareRepository;
            _ecdsa = ecdsa;
        }
        
        public async Task<SignaturesVerificationResponseDto> Handle(VerifySignaturesCommand request, CancellationToken cancellationToken)
        {
            var response = new SignaturesVerificationResponseDto();
            var signatureVerificationData = await _shareRepository.GetSignatureVerificationData(request.ShareId, request.Username);

            foreach (var signatureVerification in signatureVerificationData)
            {
                var verificationResponse = await _ecdsa.VerifySignature(signatureVerification.Filename,
                    signatureVerification.Content, signatureVerification.Signature, signatureVerification.PublicKey);

                if (verificationResponse.Status == Status.Success) continue;
                
                response.Status = (Aes.Models.Status) Status.Failed;
                response.ErroneousFiles.Add(new FileDto
                {
                    Id = signatureVerification.FileId,
                    Name = signatureVerification.Filename
                });
            }
            
            return response;
        }
    }
}