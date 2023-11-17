using System.Text.RegularExpressions;
using AddressesAPI.Boundary.Requests;
using FluentValidation;

namespace AddressesAPI.v1.Validators
{
    public class GetAddressByPostcodeValidator : AbstractValidator<GetPostcodeAddressesDomain>, IGetAddressByPostcodeValidator
    {
        private const string _allowedPostcodePattern = "^[A-Za-z][1-9][1-9][A-Za-z]{2}$";

        public GetAddressByPostcodeValidator()
        {
            RuleFor(request => request.Postcode)
                .NotNull().WithMessage("Postcode must be provided.")
                .NotEmpty().WithMessage("Postcode cannot be empty.")
                .MaximumLength(8).WithMessage("Provided postcode may not exceed 8 symbols length.")
                .Matches(new Regex(_allowedPostcodePattern)).WithMessage("Postcode is not provided in valid format.");
        }
    }
}
