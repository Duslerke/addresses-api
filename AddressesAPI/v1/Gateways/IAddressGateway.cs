using System;

namespace AddressesAPI.v1.Gateways
{
    public interface IAddressGateway
    {
        Task<int> CountAddresses();
    }
}
