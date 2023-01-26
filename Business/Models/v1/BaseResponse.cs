using Business.Models.v1.Errors;

namespace Business.Models.v1
{
    public class BaseResponse
    {
        public bool IsSucceeded { get; set; }

        public Error Error { get; init; }
    }
}
