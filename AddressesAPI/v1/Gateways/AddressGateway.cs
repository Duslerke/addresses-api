using AddressesAPI.Boundary.Requests;
using AddressesAPI.v1.Domain;
using AddressesAPI.v1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AddressesAPI.v1.Factories;

namespace AddressesAPI.v1.Gateways
{
    public class AddressGateway : IAddressGateway
    {
        private readonly AddressesContext Context;

        public AddressGateway(AddressesContext addressesContext)
        {
            Context = addressesContext;
        }

        public async Task<List<AddressDomain>> GetPostcodeAddresses(GetPostcodeAddressesDomain request)
        {
            var requestedPostcode = request?.Postcode?.ToLower();

            var addresses = await Context.Addresses
                .Where(a => a.PostcodeNospace != null)
                .Where(a => a.PostcodeNospace.Trim() != "")
                .Where(a =>
                    a.PostcodeNospace.Replace(" ", "").Trim().ToLower().Equals(requestedPostcode)
                )
                .ToListAsync()
                .ConfigureAwait(false);

            return addresses.EntityToDomain();
        }
    }
}
