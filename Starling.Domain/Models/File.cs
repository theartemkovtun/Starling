using System;

namespace Starling.Domain.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}