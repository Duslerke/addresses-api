using AutoFixture;
using B = Bogus;
using FluentValidation.Results;

namespace AddressesAPI.Tests
{
    public static class Randomizer
    {
        private static Fixture _fixture = new Fixture();
        private static B.Faker _faker = new B.Faker();

        // wrapper of convenience so fixtures' setup could be done once
        public static T Create<T>() => _fixture.Create<T>();
        public static IList<T> CreateMany<T>() => _fixture.CreateMany<T>().ToList();

        // custom helpers
        public static IEnumerable<TItem> CreateMany<TItem>(Func<TItem> creatorDelegate, int itemCount)
            => Enumerable.Range(1, itemCount).Select(_ => creatorDelegate()).ToList();

        private static ValidationFailure ValidationFailureItem()
            => new ValidationFailure(_faker.Random.Word(), _faker.Random.Hash());

        private static IList<ValidationFailure> ValidationFailuresList(int count)
            => CreateMany(ValidationFailureItem, count).ToList();

        public static ValidationResult FailedValidation(int errorCount = 3) =>
            new ValidationResult(ValidationFailuresList(errorCount));

        // tests constants:
        public static ValidationResult PassedValidation() => new ValidationResult();
    }
}
