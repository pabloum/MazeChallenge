using System;
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


    // Move this class to a Domain Project 
    public class ValidationException : Exception
    {
        public class ValidationError
        {
            public string Message { get; set; }

            public ValidationError(string message)
            {
                Message = message;
            }
        }

        public IEnumerable<ValidationError> ValidationErrors { get; }

        public ValidationException()
        {
            ValidationErrors = new List<ValidationError>();
        }

        public ValidationException(string message) : base(message)
        {
            ValidationErrors = new List<ValidationError>();
        }

        public ValidationException(string message, Exception ex) : base(message, ex)
        {
            ValidationErrors = new List<ValidationError>();
        }

        public ValidationException(string message, IEnumerable<string> errors) : base(message)
        {
            ValidationErrors = errors.Select(e => new ValidationError(e));
        }
    }
}

