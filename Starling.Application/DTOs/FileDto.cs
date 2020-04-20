using System;
using System.Collections.Generic;

namespace Starling.Application.DTOs
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Content { get; set; }
    }
}