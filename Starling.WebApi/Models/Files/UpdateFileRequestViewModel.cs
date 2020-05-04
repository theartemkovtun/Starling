using System;
using System.IO;

namespace Starling.WebApi.Models.Files
{
    public class UpdateFileRequestViewModel
    {
        public Guid ShareId { get; set; }
        public Guid FileId { get; set; }
        public string PrivateKeyPassword { get; set; }
    }
}