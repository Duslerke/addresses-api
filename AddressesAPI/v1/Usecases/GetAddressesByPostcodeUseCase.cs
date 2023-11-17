using AddressesAPI.Boundary.Requests;
using AddressesAPI.v1.Domain;
using AddressesAPI.v1.Exceptions;
using AddressesAPI.v1.Gateways;
using AddressesAPI.v1.Validators;

namespace AddressesAPI.v1.Usecases
{
    public class GetAddressesByPostcodeUseCase : IGetAddressesByPostcodeUseCase
    {
        private readonly IAddressGateway _addressesGateway;
        private readonly IGetAddressByPostcodeValidator _requestValidator;

        public GetAddressesByPostcodeUseCase(IAddressGateway addressesGateway, IGetAddressByPostcodeValidator requestValidator)
        {
            _addressesGateway = addressesGateway;
            _requestValidator = requestValidator;
        }

        // Clean architecture requires for the validation to belong within the usecase. This comes from the
        // fact that the modules connecting to the usecase (controller, gateway) are intended to be swappable.
        // Validation is part of usecase (business core) because we don't our application to potentially lose
        // its business specific validation rules when we swap the controller module to a different one that
        // may not have validation. Also, upon validation failure we could return 'null', however, that way
        // we won't have access to validation error messages within our presentation layer (controller).
        public async Task<List<AddressDomain>> Execute(GetPostcodeAddressesDomain request)
        {
            var validationResult = _requestValidator.Validate(request);

            if (!validationResult.IsValid)
                throw new FailedValidationException(validationResult.Errors);

            var addresses = await _addressesGateway.GetPostcodeAddresses(request).ConfigureAwait(false);

            return addresses;
        }
    }
}
