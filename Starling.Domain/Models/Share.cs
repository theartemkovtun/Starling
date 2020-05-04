using System;
using Starling.Domain.Enums;

namespace Starling.Domain.Models
{
    public class Share
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ShareStatus Status { get; set; }
        public UserShareStatus UserShareStatus { get; set; }
        public ShareUserOwnershipStatus UserOwnershipStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}