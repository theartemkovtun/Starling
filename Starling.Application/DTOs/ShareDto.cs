using System;
using Starling.Domain.Enums;

namespace Starling.Application.DTOs
{
    public class ShareDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ShareStatus Status { get; set; }
        public UserShareStatus UserShareStatus { get; set; }
        public ShareUserOwnershipStatus UserOwnershipStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UsersQuantity { get; set; }
        public int FilesQuantity { get; set; }
    }
}