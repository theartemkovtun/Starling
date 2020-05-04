using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Starling.WebApi.Models.Shares
{
    public class AddShareRequestViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PrivateKeyPassword { get; set; }
        public IEnumerable<string> Receivers { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
    }
}