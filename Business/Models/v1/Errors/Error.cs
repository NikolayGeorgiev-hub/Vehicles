using System.Net;

namespace Business.Models.v1.Errors
{
    public class Error
    {
        public HttpStatusCode StatusCode { get; init; }

        public string Description { get; init; }
    }
}
