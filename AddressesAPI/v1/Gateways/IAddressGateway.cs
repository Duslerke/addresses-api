using AddressesAPI.Boundary.Requests;
using AddressesAPI.v1.Domain;

namespace AddressesAPI.v1.Gateways
{
    public interface IAddressGateway
    {
        Task<List<AddressDomain>> GetPostcodeAddresses(GetPostcodeAddressesDomain request);
    }
}
