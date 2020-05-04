using Starling.Domain.Enums;

namespace Starling.WebApi.Models.Shares
{
    public class GetSharesRequestViewModel
    {
        public ShareUserOwnershipStatus? UserOwnershipStatus { get; set; }
        public SharesFilterStatus? SharesFilterStatus { get; set; }
        public int? Page { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }     
    }
}