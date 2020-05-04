using System;
using Starling.Domain.Enums;

namespace Starling.Application.DTOs
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Content { get; set; }
        public UserFileStatus OwnershipStatus { get; set; }
        public Guid ShareId { get; set; }
    }
}