using FluentValidation.Results;

namespace Ahlatci.Shop.Application.Exceptions
{
    public class ValidateException : Exception
    {
        public List<string> ErrorMessages { get; set; }

        public ValidateException(ValidationResult result):base()
        {
            ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}
