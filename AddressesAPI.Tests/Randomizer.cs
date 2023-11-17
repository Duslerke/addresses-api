using AutoFixture;

namespace AddressesAPI.Tests
{
    public static class Randomizer
    {
        private static Fixture _fixture = new Fixture();

        public static T Create<T>() => _fixture.Create<T>();
        public static IList<T> CreateMany<T>() => _fixture.CreateMany<T>().ToList();
    }
}
