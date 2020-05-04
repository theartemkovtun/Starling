using System.Collections.Generic;
using Starling.Aes.Models;
using Starling.Domain.Models;

namespace Starling.Application.DTOs
{
    public class SignaturesVerificationResponseDto
    {
        public Status Status { get; set; } = Status.Success;
        public IList<FileDto> ErroneousFiles { get; set; } = new List<FileDto>();
    }
}