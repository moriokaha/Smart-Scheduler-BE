using Newtonsoft.Json;

namespace SmartScheduler.Data.DataTransferObjects
{
    public class ErrorDetails
    {
        public string Message {  get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
