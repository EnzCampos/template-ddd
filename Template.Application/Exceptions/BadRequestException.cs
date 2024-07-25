using FluentValidation.Results;

namespace Template.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {
        }
        
        public BadRequestException(string message, IDictionary<string, string[]> validationErrors) : base(message)
        {
            ValidationErrors = validationErrors;
        }

        public BadRequestException(string message, ValidationResult validationResult, IDictionary<string, string[]> validationErrors) : base(message)
        {
            ValidationErrors = validationResult.ToDictionary();
        }

        public IDictionary<string , string[]>? ValidationErrors { get; set; }
    }
}
