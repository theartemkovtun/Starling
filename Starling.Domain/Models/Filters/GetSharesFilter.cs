using Starling.Domain.Enums;

namespace Starling.Domain.Models.Filters
{
    public class GetSharesFilter
    {
        public string Username { get; set; }
        public int? Page { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }
        public ShareUserOwnershipStatus? UserOwnershipStatus { get; set; }
        public SharesFilterStatus? SharesFilterStatus { get; set; }
    }
}