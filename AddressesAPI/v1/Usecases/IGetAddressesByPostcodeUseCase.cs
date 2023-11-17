using AddressesAPI.Boundary.Requests;
using AddressesAPI.v1.Domain;

namespace AddressesAPI.v1.Usecases
{
    public interface IGetAddressesByPostcodeUseCase
    {
        Task<List<AddressDomain>> Execute(GetPostcodeAddressesDomain request);
    }
}
