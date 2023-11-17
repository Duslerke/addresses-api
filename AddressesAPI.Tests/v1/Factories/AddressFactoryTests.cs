using AddressesAPI.v1.Infrastructure;
using NUnit.Framework.Internal;
using AddressesAPI.v1.Factories;
using FluentAssertions;

namespace AddressesAPI.Tests.v1.Factories
{
    [TestFixture]
    public class AddressFactoryTests
    {
        [TestCase(TestName = "Mapping between AddressEntity and AddressDomain is correct")]
        public void AddressDataGetsCorrectlyMappedFromDatabaseModelToDomainModel()
        {
            // arrange
            var dbAddress = Randomizer.Create<AddressEntity>();

            // act
            var domainAddress = dbAddress.EntityToDomain();

            // assert
            domainAddress.Should().BeEquivalentTo(dbAddress);
        }

        [TestCase(TestName = "Address Entity to Domain mapper returns null when is provided with null.")]
        public void AddressEntityToDomainMapperReturnsNullWhenProvidedWithNull()
        {
            // arrange
            var dbAddress = null as AddressEntity;

            // act
            var domainAddress = dbAddress.EntityToDomain();

            // assert
            domainAddress.Should().BeNull();
        }
    }
}
