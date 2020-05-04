using System;
using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Shares.Commands.VerifySignatures
{
    public class VerifySignaturesCommand : IRequest<SignaturesVerificationResponseDto>
    {
        public Guid ShareId { get; set; }
        public string Username { get; set; }
    }
}