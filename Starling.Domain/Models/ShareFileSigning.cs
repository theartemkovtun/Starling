using System;

namespace Starling.Domain.Models
{
    public class ShareFileSigning
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Signature { get; set; }
        public bool ResignNeeded { get; set; }
    }
}