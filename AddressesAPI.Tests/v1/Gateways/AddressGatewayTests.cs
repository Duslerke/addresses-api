using AddressesAPI.Boundary.Requests;
using AddressesAPI.v1.Gateways;
using AddressesAPI.v1.Infrastructure;
using FluentAssertions;

namespace AddressesAPI.Tests.v1.Gateways
{
    public class AddressGatewayTests : DbTests
    {
        private IAddressGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new AddressGateway(AddressesContext);
        }

        [TestCase(TestName = "Getting addresses while there are none, gives an empty list back")]
        public async Task AddressGatewayReturnsAnEmptyListWhenDatabaseContainsNoAddresses()
        {
            // arrange
            var request = new GetPostcodeAddressesDomain() { Postcode = "E81DY" };

            // act
            var gwResponse = await _classUnderTest.GetPostcodeAddresses(request).ConfigureAwait(false);

            // assert
            gwResponse.Should().HaveCount(0);
        }

        [TestCase(TestName = "GW ignores null postcodes")]
        public async Task GetAddressByPostcodeGatewayFiltersOutNullAndEmptyPostcodeAddresses()
        {
            // arrange
            var randomAddress =Randomizer.Create<AddressEntity>();
            randomAddress.PostcodeNospace = null;

            AddressesContext.Addresses.Add(randomAddress);
            AddressesContext.SaveChanges();

            var request = new GetPostcodeAddressesDomain() { Postcode = null };

            // act
            var gwResponse = await _classUnderTest.GetPostcodeAddresses(request).ConfigureAwait(false);

            // assert
            gwResponse.Should().HaveCount(0);
        }

        [TestCase(TestName = "GW ignores empty postcodes")]
        public async Task GetAddressByPostcodeGatewayFiltersOutEmptyAndEmptyPostcodeAddresses()
        {
            // arrange
            var randomAddress =Randomizer.Create<AddressEntity>();
            randomAddress.PostcodeNospace = "  ";

            AddressesContext.Addresses.Add(randomAddress);
            AddressesContext.SaveChanges();

            var request = new GetPostcodeAddressesDomain() { Postcode = "" };

            // act
            var gwResponse = await _classUnderTest.GetPostcodeAddresses(request).ConfigureAwait(false);

            // assert
            gwResponse.Should().HaveCount(0);
        }

        [TestCase(TestName = "GW results don't depend on database postcodes being trimmed")]
        public async Task GetAddressByPostcodeGatewayIgnoresWhitespaceAroundDatabasePostcodes()
        {
            // arrange
            var requestedPostCode = "E81LL";
            var randomAddress1 =Randomizer.Create<AddressEntity>();
            var randomAddress2 =Randomizer.Create<AddressEntity>();
            var randomAddress3 =Randomizer.Create<AddressEntity>();

            randomAddress1.PostcodeNospace = $" {requestedPostCode} ";
            randomAddress2.PostcodeNospace = $" {requestedPostCode}";
            randomAddress3.PostcodeNospace = $"{requestedPostCode}   ";

            AddressesContext.Addresses.Add(randomAddress1);
            AddressesContext.Addresses.Add(randomAddress2);
            AddressesContext.Addresses.Add(randomAddress3);
            AddressesContext.SaveChanges();

            var request = new GetPostcodeAddressesDomain() { Postcode = requestedPostCode };

            // act
            var gwResponse = await _classUnderTest.GetPostcodeAddresses(request).ConfigureAwait(false);

            // assert
            gwResponse.Should().HaveCount(3);
        }

        [TestCase(TestName = "GW results don't depend on database postcodes being cased")]
        public async Task GetAddressByPostcodeGatewayWorksWithAllPostcodeCasings()
        {
            // arrange
            var requestedPostCode = "e81Ll";
            var randomAddress1 =Randomizer.Create<AddressEntity>();
            var randomAddress2 =Randomizer.Create<AddressEntity>();
            var randomAddress3 =Randomizer.Create<AddressEntity>();

            randomAddress1.PostcodeNospace = requestedPostCode.ToUpper();
            randomAddress2.PostcodeNospace = requestedPostCode.ToLower();
            randomAddress3.PostcodeNospace = "E81ll"; // mixed case

            AddressesContext.Addresses.Add(randomAddress1);
            AddressesContext.Addresses.Add(randomAddress2);
            AddressesContext.Addresses.Add(randomAddress3);
            AddressesContext.SaveChanges();

            var request = new GetPostcodeAddressesDomain() { Postcode = requestedPostCode };

            // act
            var gwResponse = await _classUnderTest.GetPostcodeAddresses(request).ConfigureAwait(false);

            // assert
            gwResponse.Should().HaveCount(3);
        }

        [TestCase(TestName = "GW returns an empty list on no matches")]
        public async Task GetAddressByPostcodeGatewayReturnsEmptyListWhenNoMatchesAreFound()
        {
            // arrange
            var requestedPostCode = "E81DY";
            var nonMatchingPostCode = "E92DU";

            var randomAddress1 =Randomizer.Create<AddressEntity>();
            var randomAddress2 =Randomizer.Create<AddressEntity>();

            randomAddress1.PostcodeNospace = nonMatchingPostCode;
            randomAddress2.PostcodeNospace = nonMatchingPostCode;

            AddressesContext.Addresses.Add(randomAddress1);
            AddressesContext.Addresses.Add(randomAddress2);
            AddressesContext.SaveChanges();

            var request = new GetPostcodeAddressesDomain() { Postcode = requestedPostCode };

            // act
            var gwResponse = await _classUnderTest.GetPostcodeAddresses(request).ConfigureAwait(false);

            // assert
            gwResponse.Should().HaveCount(0);
        }

        [TestCase(TestName = "GW returns only the matching records")]
        public async Task GetAddressByPostcodeGatewayReturnsOnlyTheMatchingRecordsIgnoresTheRest()
        {
            // arrange
            var requestedPostCode = "E81DY";
            var nonMatchingPostCode = "E72SU";

            var randomAddress1 =Randomizer.Create<AddressEntity>();
            var randomAddress2 =Randomizer.Create<AddressEntity>();
            var randomAddress3 =Randomizer.Create<AddressEntity>();

            randomAddress1.PostcodeNospace = requestedPostCode;
            randomAddress2.PostcodeNospace = requestedPostCode;
            randomAddress3.PostcodeNospace = nonMatchingPostCode;

            AddressesContext.Addresses.Add(randomAddress1);
            AddressesContext.Addresses.Add(randomAddress2);
            AddressesContext.Addresses.Add(randomAddress3);
            AddressesContext.SaveChanges();

            var request = new GetPostcodeAddressesDomain() { Postcode = requestedPostCode };

            // act
            var gwResponse = await _classUnderTest.GetPostcodeAddresses(request).ConfigureAwait(false);

            // assert
            gwResponse.Should().HaveCount(2);
            gwResponse.Should().AllSatisfy(a => a.PostcodeNospace?.Equals(requestedPostCode));
        }
    }
}
