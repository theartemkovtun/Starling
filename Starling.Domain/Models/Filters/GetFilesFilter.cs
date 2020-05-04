using Starling.Domain.Enums;

namespace Starling.Domain.Models.Filters
{
    public class GetFilesFilter
    {
        public string Username { get; set; }
        public int? Page { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }
        public UserFileStatus? OwnershipStatus { get; set; }
    }
}