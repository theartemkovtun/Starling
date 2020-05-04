using System;

namespace Starling.Domain.Models
{
    public class FileWithTotal : File
    {
        public Guid ShareId { get; set; }
        public int TotalQuantity { get; set; }
    }
}