using System.Collections.Generic;

namespace Starling.Application.DTOs
{
    public class ShareSearchResult
    {
        public IList<ShareDto> Shares { get; set; }
        public int Total { get; set; }
    }
}