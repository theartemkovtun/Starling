using System;

namespace Starling.Application.DTOs
{
    public class ShareFileSigningDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Signature { get; set; }
        public bool ResignNeeded { get; set; }
    }
}