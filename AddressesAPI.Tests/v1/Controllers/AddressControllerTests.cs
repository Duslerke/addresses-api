using AddressesAPI.Boundary.Requests;
using AddressesAPI.Boundary.Responses;
using AddressesAPI.Controllers;
using AddressesAPI.v1.Domain;
using AddressesAPI.v1.Usecases;
using FluentAssertions;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AddressesAPI.Tests.v1.Controllers
{
    public class AddressControllerTests
    {
        private Mock<ILogger<AddressController>> _loggerMock;
        private Mock<IGetAddressesByPostcodeUseCase> _getAddressByPostcodeUseCaseMock;
        private AddressController _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<AddressController>>();
            _getAddressByPostcodeUseCaseMock = new Mock<IGetAddressesByPostcodeUseCase>();
            _classUnderTest = new AddressController(_loggerMock.Object, _getAddressByPostcodeUseCaseMock.Object);
        }

        [TestCase(TestName = "get by postcode endpoint calls approapriate UC with correct parameters")]
        public async Task WhenGetAddressesByPostcodeEndpointIsCalledItCallsAddressUCWithCorrectParams()
        {
            // arrange
            var useCaseResponse = Randomizer.CreateMany<AddressDomain>().ToList();

            _getAddressByPostcodeUseCaseMock
                .Setup(v => v.Execute(It.IsAny<GetPostcodeAddressesDomain>()))
                .ReturnsAsync(useCaseResponse);

            var request = new GetPostcodeAddressesRequest() { Postcode = "E81DY" };

            // act
            await _classUnderTest.GetAddressesByPostcode(request).ConfigureAwait(false);

            // assert
           _getAddressByPostcodeUseCaseMock.Verify(
                v => v.Execute(It.Is<GetPostcodeAddressesDomain>(r => r.Postcode == request.Postcode)),
                Times.Once
            );
        }

        [TestCase(TestName = "get by postcode endpoint returns correct data and status code")]
        public async Task WhenGetAddressesByPostcodeEndpointIsCalledItReturnsExpectedDataAndStatusCode()
        {
            // arrange
            var useCaseResponse = Randomizer.CreateMany<AddressDomain>().ToList();

            _getAddressByPostcodeUseCaseMock
                .Setup(v => v.Execute(It.IsAny<GetPostcodeAddressesDomain>()))
                .ReturnsAsync(useCaseResponse);

            var request = new GetPostcodeAddressesRequest() { Postcode = "E81DY" };

            // act
            var actionResult = await _classUnderTest.GetAddressesByPostcode(request).ConfigureAwait(false);

            // assert
           var getResult = actionResult as OkObjectResult;
            getResult.Should().NotBeNull();
            getResult?.StatusCode.Should().Be(200);

            var responseObject = getResult?.Value as List<AddressPresentation>;
            responseObject.Should().HaveSameCount(useCaseResponse);
            responseObject.Should().BeEquivalentTo(useCaseResponse);
        }

        [TestCase(TestName = "get by postcode endpoint throws whenever usecase throws")]
        public async Task GetAddressesByPostcodeEndpointDoesNotObscureItsDependencyErrors()
        {
            // arrange
            var ucErrorMsg = "GW connection failed";

            _getAddressByPostcodeUseCaseMock
                .Setup(v => v.Execute(It.IsAny<GetPostcodeAddressesDomain>()))
                .ThrowsAsync(new ConnectionAbortedException(ucErrorMsg));

            var request = new GetPostcodeAddressesRequest() { Postcode = "E81DY" };

            Func<Task> endpointCall = async () => await _classUnderTest.GetAddressesByPostcode(request).ConfigureAwait(false);

            // act, assert
           await endpointCall.Should()
                .ThrowAsync<ConnectionAbortedException>()
                .WithMessage($"*{ucErrorMsg}*")
                .ConfigureAwait(false);
        }
    }
}
