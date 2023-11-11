namespace MazeChallenge.Domain.Exceptions
{
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

