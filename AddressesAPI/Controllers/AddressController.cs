using AddressesAPI.Boundary.Requests;
using AddressesAPI.Boundary.Responses;
using AddressesAPI.v1.Exceptions;
using AddressesAPI.v1.Factories;
using AddressesAPI.v1.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace AddressesAPI.Controllers
{
    [ApiController]
    [Route("api/v1/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IGetAddressesByPostcodeUseCase _getAddressByPostcodeUseCase;

        public AddressController(ILogger<AddressController> logger, IGetAddressesByPostcodeUseCase getAddressByPostcodeUseCase)
        {
            _logger = logger;
            _getAddressByPostcodeUseCase = getAddressByPostcodeUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddressesByPostcode([FromQuery] GetPostcodeAddressesRequest request)
        {
            try
            {
                var postCodeAddresses = await _getAddressByPostcodeUseCase.Execute(request.ToDomain());
                return Ok(postCodeAddresses.DomainToPresentation());
            }
            catch (FailedValidationException ex)
            {
                return BadRequest(new ValidationResponse(ex.Errors));
            }
        }
    }
}
