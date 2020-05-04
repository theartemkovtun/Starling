using System;

namespace Starling.Domain.Models
{
    public class SignatureVerification
    {
        public Guid FileId { get; set; }
        public string Filename { get; set; }
        public byte[] Content { get; set; }
        public string PublicKey { get; set; }
        public string Signature { get; set; }
    }
}