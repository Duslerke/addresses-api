using FluentValidation.Results;

namespace AddressesAPI.v1.Exceptions
{
    public class FailedValidationException : Exception
    {
        public IList<string> Errors { get; set; }

        public FailedValidationException(IList<ValidationFailure> errors)
            => Errors = errors.Select(e => e.ErrorMessage).ToList();
    }
}
