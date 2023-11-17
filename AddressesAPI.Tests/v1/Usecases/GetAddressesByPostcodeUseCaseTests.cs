using AddressesAPI.Boundary.Requests;
using AddressesAPI.v1.Domain;
using AddressesAPI.v1.Exceptions;
using AddressesAPI.v1.Gateways;
using AddressesAPI.v1.Usecases;
using AddressesAPI.v1.Validators;
using FluentAssertions;
using Microsoft.AspNetCore.Connections;
using Moq;

namespace AddressesAPI.Tests.v1.Usecases
{
    public class GetAddressesByPostcodeUseCaseTests
    {
        private Mock<IAddressGateway> _addressGatewayMock;
        private Mock<IGetAddressByPostcodeValidator> _requestValidatorMock;
        private IGetAddressesByPostcodeUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _requestValidatorMock = new Mock<IGetAddressByPostcodeValidator>();
            _addressGatewayMock = new Mock<IAddressGateway>();
            _classUnderTest = new GetAddressesByPostcodeUseCase(_addressGatewayMock.Object, _requestValidatorMock.Object);
        }

        [TestCase(TestName = "get addresses by postcode usecase calls the validator with exactly its input")]
        public async Task GetAddressesByPostcodeUCValidatesTheRequestObjectItHasReceived()
        {
            // arrange
            _requestValidatorMock
                .Setup(v => v.Validate(It.IsAny<GetPostcodeAddressesDomain>()))
                .Returns(Randomizer.PassedValidation());

            var request = new GetPostcodeAddressesDomain() { Postcode = "E81DY" };

            // act
            await _classUnderTest.Execute(request).ConfigureAwait(false);

            // assert
           _requestValidatorMock.Verify(
                v => v.Validate(It.Is<GetPostcodeAddressesDomain>(r => r == request)),
                Times.Once
            );
        }

        [TestCase(TestName = "get addresses by postcode usecase calls addresses gateway with the request if validation was successful")]
        public async Task GetAddressesByPostcodeUCCallsAddressesGWIfValidationWasSuccessful()
        {
            // arrange
            _requestValidatorMock
                .Setup(v => v.Validate(It.IsAny<GetPostcodeAddressesDomain>()))
                .Returns(Randomizer.PassedValidation());

            var request = new GetPostcodeAddressesDomain();

            // act
            await _classUnderTest.Execute(request).ConfigureAwait(false);

            // assert
           _addressGatewayMock.Verify(
                v => v.GetPostcodeAddresses(It.Is<GetPostcodeAddressesDomain>(r => r == request)),
                Times.Once
            );
        }

        [TestCase(TestName = "get addresses by postcode usecase terminates execution when request is invalid")]
        public async Task GetAddressesByPostcodeUCDoesNotProceedWithAddressGatewayCallWhenRequestIsInvalid()
        {
            // arrange
            var failedValidationResult = Randomizer.FailedValidation();

            _requestValidatorMock
                .Setup(v => v.Validate(It.IsAny<GetPostcodeAddressesDomain>()))
                .Returns(failedValidationResult);

            var request = new GetPostcodeAddressesDomain();

            // act
            Func<Task> ucCall = async () => await _classUnderTest.Execute(request).ConfigureAwait(false);

            // assert
            await ucCall.Should().ThrowAsync<FailedValidationException>().ConfigureAwait(false);

           _addressGatewayMock.Verify(
                v => v.GetPostcodeAddresses(It.IsAny<GetPostcodeAddressesDomain>()),
                Times.Never
            );
        }

        [TestCase(TestName = "get addresses by postcode usecase encapsulates validation error data within failed validation exception")]
        public async Task GetAddressesByPostcodeUCThrowsExceptionWithValidationErrorDataUponRequestBeingInvalid()
        {
            // arrange
            var failedValidationResult = Randomizer.FailedValidation();

            _requestValidatorMock
                .Setup(v => v.Validate(It.IsAny<GetPostcodeAddressesDomain>()))
                .Returns(failedValidationResult);

            var request = new GetPostcodeAddressesDomain();

            try{
                // act
                await _classUnderTest.Execute(request).ConfigureAwait(false);
            }
            catch (FailedValidationException ex)
            {
                // assert
                var validationFailures = failedValidationResult.Errors;
                var validationFailureMessages = validationFailures
                    .Select(e => e.ErrorMessage)
                    .ToList();

                ex.Errors.Should().HaveCount(failedValidationResult.Errors.Count);
                ex.Errors.Should().AllSatisfy(valErrMsg => validationFailureMessages.Contains(valErrMsg));
            }
        }

        [TestCase(TestName = "get addresses by postcode usecase throws when its dependency throws")]
        public async Task GetAddressesByPostcodeUCThrowsWhenItsDependencyThrows()
        {
            // arrange shared
            var request = new GetPostcodeAddressesDomain();

            // arrange
            var validatorExceptionMsg = "Request should not be null";

            _requestValidatorMock
                .Setup(v => v.Validate(It.IsAny<GetPostcodeAddressesDomain>()))
                .Throws(new ArgumentNullException(validatorExceptionMsg));

            Func<Task> ucCall = async () => await _classUnderTest.Execute(request).ConfigureAwait(false);

            // act, assert for validator
            await ucCall.Should()
                .ThrowAsync<ArgumentNullException>()
                .WithMessage($"*{validatorExceptionMsg}*")
                .ConfigureAwait(false);

            // arrange
            var passedValidationResult = Randomizer.PassedValidation();
            var addressGWExceptionMsg = "Connection got closed on the database side.";

            _requestValidatorMock.Reset();
            _requestValidatorMock
                .Setup(v => v.Validate(It.IsAny<GetPostcodeAddressesDomain>()))
                .Returns(passedValidationResult);

            _addressGatewayMock
                .Setup(g => g.GetPostcodeAddresses(It.IsAny<GetPostcodeAddressesDomain>()))
                .ThrowsAsync(new ConnectionAbortedException(addressGWExceptionMsg));

            await ucCall.Should()
                .ThrowAsync<ConnectionAbortedException>()
                .WithMessage(addressGWExceptionMsg)
                .ConfigureAwait(false);
        }

        [TestCase(TestName = "get addresses by postcode usecase returns the address gateway data when all goes successfully")]
        public async Task GetAddressesByPostcodeUCReturnsAddressGatewayReturnOnSuccess()
        {
            // arrange
            _requestValidatorMock
                .Setup(v => v.Validate(It.IsAny<GetPostcodeAddressesDomain>()))
                .Returns(Randomizer.PassedValidation());

            var gwReturnData = Randomizer.CreateMany<AddressDomain>().ToList();

            _addressGatewayMock
                .Setup(g => g.GetPostcodeAddresses(It.IsAny<GetPostcodeAddressesDomain>()))
                .ReturnsAsync(gwReturnData);

            var request = new GetPostcodeAddressesDomain();

            // act
            var ucResponse = await _classUnderTest.Execute(request).ConfigureAwait(false);

            // assert
            ucResponse.Should().HaveSameCount(gwReturnData);
            ucResponse.Should().BeEquivalentTo(gwReturnData);
        }
    }
}
