using Starling.Domain.Enums;

namespace Starling.Domain.Models
{
    public class ShareUserInfo
    {
        public string Username { get; set; }
        public UserShareStatus UserShareStatus { get; set; }
        public ShareUserOwnershipStatus UserOwnershipStatus { get; set; }
    }
}