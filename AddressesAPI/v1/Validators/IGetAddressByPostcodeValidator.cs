using AddressesAPI.Boundary.Requests;
using FluentValidation.Results;

namespace AddressesAPI.v1.Validators
{
    public interface IGetAddressByPostcodeValidator
    {
        ValidationResult Validate(GetPostcodeAddressesDomain request);
    }
}
