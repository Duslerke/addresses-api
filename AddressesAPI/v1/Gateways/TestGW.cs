using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressesAPI.v1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AddressesAPI.v1.Gateways
{
    public class AddressGateway : IAddressGateway
    {
        private readonly AddressesContext Context;

        public AddressGateway(AddressesContext addressesContext)
        {
            Context = addressesContext;
        }

        public async Task<int> CountAddresses() //List<AddressEntity>
        {
            var aa = await Context.Addresses.CountAsync().ConfigureAwait(false);
            return aa;
        }
    }
}
