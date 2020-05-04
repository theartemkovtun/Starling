using Starling.Domain.Enums;

namespace Starling.Application.DTOs
{
    public class ShareUserInfoDto
    {
        public string Username { get; set; }
        public UserShareStatus UserShareStatus { get; set; }
        public ShareUserOwnershipStatus UserOwnershipStatus { get; set; }
    }
}