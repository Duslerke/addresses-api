using Microsoft.AspNetCore.Mvc;

namespace AddressesAPI.Boundary.Requests
{
    public class GetPostcodeAddressesRequest
    {
        [FromQuery(Name = "postcode")]
        public string Postcode { get; set; }
    }
}
