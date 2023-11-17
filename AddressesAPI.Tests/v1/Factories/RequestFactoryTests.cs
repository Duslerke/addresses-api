using AddressesAPI.Boundary.Requests;
using AddressesAPI.v1.Factories;
using FluentAssertions;

namespace AddressesAPI.Tests.v1.Factories
{
    public class RequestFactoryTests
    {
        [TestCase(TestName = "Mapping between GetPostcodeAddressesRequest and GetPostcodeAddressesDomain is correct")]
        public void GetPostcodeAddressesWrapperGetsCorrectlyMappedFromPresentationModelToDomainModel()
        {
            // arrange
            var presentationRequest = Randomizer.Create<GetPostcodeAddressesRequest>();

            // act
            var domainRequest = presentationRequest.ToDomain();

            // assert
            domainRequest.Should().BeEquivalentTo(presentationRequest);
        }

        [TestCase(TestName = "GetPostcodeAddresses Request to Domain mapper returns null when is provided with null.")]
        public void GetPostcodeAddressesWrapperPresentationToDomainMapperReturnsNullWhenProvidedWithNull()
        {
            // arrange
            var presentationRequest = null as GetPostcodeAddressesRequest;

            // act
            var domainRequest = presentationRequest.ToDomain();

            // assert
            domainRequest.Should().BeNull();
        }
    }
}
