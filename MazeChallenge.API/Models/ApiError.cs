using MazeChallenge.Domain.Exceptions;

namespace MazeChallenge.API.Models
{
    public class ApiError
    {
        public string Type { get; protected set; }
        public string Error { get; protected set; }
        public IEnumerable<object> Details { get; protected set; }

        public ApiError(string message, IEnumerable<string> details)
        {
            Type = "Multiple errors";
            Error = message;
            Details = details;
        }

        public ApiError(string message)
        {
            Type = "Generic error";
            Error = message;
            Details = new List<object>();
        }

        public ApiError(ValidationException validatioError)
        {
            Type = "Validation error";
            Error = validatioError.Message;
            Details = validatioError.ValidationErrors;
        }

    }
}

