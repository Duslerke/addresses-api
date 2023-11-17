using AddressesAPI.Boundary.Requests;

namespace AddressesAPI.v1.Factories
{
    public static class RequestFactory
    {
        public static GetPostcodeAddressesDomain ToDomain(this GetPostcodeAddressesRequest presentationRequest)
        {
            if (presentationRequest is null)
                return null;

            return new GetPostcodeAddressesDomain
            {
                Postcode = presentationRequest.Postcode,
            };
        }
    }
}
