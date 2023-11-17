
using AddressesAPI.Boundary.Requests;
using AddressesAPI.v1.Validators;
using FluentValidation.TestHelper;

namespace AddressesAPI.Tests.v1.Validators
{
    public class GetAddressByPostcodeValidatorTests
    {
        private GetAddressByPostcodeValidator _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new GetAddressByPostcodeValidator();
        }

        [TestCase(TestName = "get addresses by postcode validator fails when postcode is missing")]
        public async Task WhenPostCodeIsMissingGetAddressesByPostCodeValidatorFailsValidationTest()
        {
            // arrange
            var request = new GetPostcodeAddressesDomain() { Postcode = null };

            // act
            var validationResult = _classUnderTest.TestValidate(request);

            // assert
            validationResult
                .ShouldHaveValidationErrorFor(r => r.Postcode)
                .WithErrorMessage("Postcode must be provided.");
        }

        [TestCase(TestName = "get addresses by postcode validator fails when postcode is empty")]
        public async Task WhenPostCodeIsEmptyGetAddressesByPostCodeValidatorFailsValidationTest()
        {
            // arrange
            var request = new GetPostcodeAddressesDomain() { Postcode = "     " };

            // act
            var validationResult = _classUnderTest.TestValidate(request);

            // assert
            validationResult
                .ShouldHaveValidationErrorFor(r => r.Postcode)
                .WithErrorMessage("Postcode cannot be empty.");
        }

        [TestCase("GIR 0AA", TestName = "get addresses by postcode validator fails when postcode is a non local pattern")]
        [TestCase("  E81DY", TestName = "get addresses by postcode validator fails when postcode is padded left with whitespace")]
        [TestCase("E81DY   ", TestName = "get addresses by postcode validator fails when postcode is padded left right with whitespace")]
        [TestCase("E8 1DY", TestName = "get addresses by postcode validator fails when postcode has a whitespace in between outcode and incode")]
        public async Task GetAddressesByPostCodeValidatorFailsWhenPostcodeDoesNotMatchAllowedPattern(string postcode)
        {
            // arrange
            var request = new GetPostcodeAddressesDomain() { Postcode = postcode };

            // act
            var validationResult = _classUnderTest.TestValidate(request);

            // assert
            validationResult
                .ShouldHaveValidationErrorFor(r => r.Postcode)
                .WithErrorMessage("Postcode is not provided in valid format.");
        }

        [TestCase(TestName = "get addresses by postcode validator fails when allowed postcode length is exceeded")]
        public async Task GetAddressesByPostCodeValidatorFailsWhenProvidedPostcodeExceeds8Chars()
        {
            // arrange
            var request = new GetPostcodeAddressesDomain() { Postcode = "123456789" };

            // act
            var validationResult = _classUnderTest.TestValidate(request);

            // assert
            validationResult
                .ShouldHaveValidationErrorFor(r => r.Postcode)
                .WithErrorMessage("Provided postcode may not exceed 8 symbols length.");
        }

        [TestCase(TestName = "get addresses by postcode validator passes when valid postcode is provided")]
        public async Task GetAddressesByPostCodeValidatorPassesWhenThePostcodeIsValid()
        {
            // arrange
            var request = new GetPostcodeAddressesDomain() { Postcode = "E81LL" };

            // act
            var validationResult = _classUnderTest.TestValidate(request);

            // assert
            validationResult.ShouldNotHaveValidationErrorFor(r => r.Postcode);
        }
    }
}
