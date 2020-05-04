using System;
using Starling.Domain.Enums;

namespace Starling.Domain.Models
{
    public class File
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Content { get; set; }
        public UserFileStatus OwnershipStatus { get; set; }
    }
}