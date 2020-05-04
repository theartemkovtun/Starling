using System.Collections.Generic;
using System.Linq;
using Starling.Domain.Enums;

namespace Starling.Application.DTOs
{
    public class ShareForUserDto : ShareDto
    {
        public new ShareStatus Status
        {
            get
            {
                if (Users.Any(u => u.UserShareStatus == UserShareStatus.Rejected)) return ShareStatus.Rejected;
                return Users.All(u => u.UserShareStatus == UserShareStatus.Accepted) ? ShareStatus.Accepted : ShareStatus.Active;
            }
        }
        
        public bool CanApprove => FilesSigning?.All(file => file.Signature != null && !file.ResignNeeded) ?? false;

        public IList<ShareFileSigningDto> FilesSigning { get; set; }
        public IList<ShareUserInfoDto> Users { get; set; }
    }
}