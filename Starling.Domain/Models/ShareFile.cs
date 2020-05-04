using System;

namespace Starling.Domain.Models
{
    public class ShareFile
    {
        public Guid Id { get; set; }
        public Guid ShareId { get; set; }
        public Guid FileId { get; set; }
    }
}