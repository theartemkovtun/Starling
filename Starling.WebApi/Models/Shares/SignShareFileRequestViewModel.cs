using System;

namespace Starling.WebApi.Models.Shares
{
    public class SignShareFileRequestViewModel
    {
        public Guid ShareId { get; set; }
        public Guid FileId { get; set; }
        public string PrivateKeyPassword { get; set; }
    }
}