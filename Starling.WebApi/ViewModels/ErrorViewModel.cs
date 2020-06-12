using Newtonsoft.Json;

namespace Starling.WebApi.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel() { }

        public ErrorViewModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}