using Starling.Domain.Enums;

namespace Starling.WebApi.Models.Files
{
    public class GetFilesRequestViewModel
    {
        public UserFileStatus? OwnershipStatus { get; set; }
        public int? Page { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }     
    }
}