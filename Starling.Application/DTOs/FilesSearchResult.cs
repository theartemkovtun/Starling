using System.Collections.Generic;

namespace Starling.Application.DTOs
{
    public class FilesSearchResult
    {
        public IList<FileDto> Files { get; set; }
        
        public int Total { get; set; }
    }
}