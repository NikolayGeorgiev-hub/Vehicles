using Business.Models.v1.Errors;

namespace Business.Models.v1
{
    public class BaseResponse
    {
        public bool IsSucceeded { get; init; }

        public Error Error { get; init; }
    }
}
