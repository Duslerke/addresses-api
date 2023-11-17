using AddressesAPI.v1.Gateways;
using AddressesAPI.v1.Infrastructure;
using AutoFixture;
using FluentAssertions;

namespace AddressesAPI.Tests.v1.Gateways
{
    public class AddressGatewayTests : DbTests
    {
        private Fixture _fixture = new Fixture();
        private IAddressGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new AddressGateway(AddressesContext);
        }

        [TestCase(TestName = "Connection Test")]
        public async Task DbConnectionTest()
        {
            // arrange
            var randomAddress =_fixture.Create<AddressEntity>();

            AddressesContext.Addresses.Add(randomAddress);
            AddressesContext.SaveChanges();

            // act
            var gwResponse = await _classUnderTest.CountAddresses().ConfigureAwait(false);

            // assert
            gwResponse.Should().Be(1);
        }


        //My tests:
        /*
            - Connects to DB in general ... ignoring all the models [done]
            - Getting addresses while there are none, gives an empty list back
            - Getting addresses by postcode returns just the matching and ignores non matching
            - The data inserted into db matches the gateway return
        */
    }
}
