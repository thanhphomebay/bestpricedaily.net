using Newtonsoft.Json;
using System.Net;
namespace Core.ApiErrors
{
    public class ApiError
    {
        public int statusCode { get; private set; }
        public string statusDescription { get; private set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string message { get; private set; }
        public ApiError(int statusCode, string statusDescription)
        {
            this.statusCode = statusCode;
            this.statusDescription = statusDescription;
        }
        public ApiError(int statusCode, string statusDescription, string Message) : this(statusCode, statusDescription)
        {
            this.message = Message;
        }
    }
    public class InternalServerError : ApiError
    {
        public InternalServerError()
            : base(500, HttpStatusCode.InternalServerError.ToString())
        {
        }


        public InternalServerError(string message)
            : base(500, HttpStatusCode.InternalServerError.ToString(), message)
        {
        }
    }
}