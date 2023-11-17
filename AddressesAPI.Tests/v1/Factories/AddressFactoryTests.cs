using AddressesAPI.v1.Infrastructure;
using NUnit.Framework.Internal;
using AddressesAPI.v1.Factories;
using FluentAssertions;
using AddressesAPI.v1.Domain;

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

        [TestCase(TestName = "Mapping between AddressEntity and AddressDomain is correct for collections")]
        public void ListOfAddressDataGetsCorrectlyMappedFromDatabaseModelToDomainModel()
        {
            // arrange
            var dbAddress = Randomizer.CreateMany<AddressEntity>();

            // act
            var domainAddress = dbAddress.EntityToDomain();

            // assert
            domainAddress.Should().BeEquivalentTo(dbAddress);
        }

        [TestCase(TestName = "Address Entity to Domain mapper returns null when is provided with null.")]
        public void AddressEntityToDomainListMapperReturnsNullWhenProvidedWithNull()
        {
            // arrange
            var dbAddress = null as IList<AddressEntity>;

            // act
            var domainAddress = dbAddress.EntityToDomain();

            // assert
            domainAddress.Should().BeNull();
        }

        [TestCase(TestName = "Mapping between AddressDomain and AddressPresentation is correct")]
        public void AddressDataGetsCorrectlyMappedFromDomainModelToPresentationModel()
        {
            // arrange
            var domainAddress = Randomizer.Create<AddressDomain>();

            // act
            var presentationAddress = domainAddress.DomainToPresentation();

            // assert
            presentationAddress.Should().BeEquivalentTo(domainAddress);
        }

        [TestCase(TestName = "Address Domain to Presentation mapper returns null when is provided with null.")]
        public void AddressDomainToPresentationMapperReturnsNullWhenProvidedWithNull()
        {
            // arrange
            var domainAddress = null as AddressDomain;

            // act
            var presentationAddress = domainAddress.DomainToPresentation();

            // assert
            presentationAddress.Should().BeNull();
        }

    [TestCase(TestName = "Mapping between AddressDomain and AddressPresentation is correct for collections")]
        public void ListOfAddressDataGetsCorrectlyMappedFromDomainModelToPresentationModel()
        {
            // arrange
            var domainAddress = Randomizer.CreateMany<AddressDomain>();

            // act
            var presentationAddress = domainAddress.DomainToPresentation();

            // assert
            presentationAddress.Should().BeEquivalentTo(domainAddress);
        }

        [TestCase(TestName = "Address Domain to Presentation mapper returns null when is provided with null.")]
        public void AddressDomainToPresentationListMapperReturnsNullWhenProvidedWithNull()
        {
            // arrange
            var domainAddress = null as IList<AddressDomain>;

            // act
            var presentationAddress = domainAddress.DomainToPresentation();

            // assert
            presentationAddress.Should().BeNull();
        }
    }
}
