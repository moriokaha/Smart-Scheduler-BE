using System.Net;

namespace SmartScheduler.Exceptions
{
    public class ClientException(string? message, HttpStatusCode statusCode) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; set; } = statusCode;
    }
}
