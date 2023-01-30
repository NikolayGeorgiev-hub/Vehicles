using System.Globalization;

namespace Business.Models.v1.Errors
{
    public class AppException : Exception
    {
        public AppException()
        {
        }

        public AppException(string? message) 
            : base(message)
        {
        }
    }
}
