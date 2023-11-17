
using AddressesAPI.v1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AddressesAPI.Tests
{
    [TestFixture]
    public class DbTests
    {
        private IDbContextTransaction _transaction;
        protected AddressesContext AddressesContext { get; private set; }

        [SetUp]
        public void RunBeforeAnyTests()
        {
            var testDbConnStr = $"Host=localhost;Port=5430;Database=testdb;Username=postgresuser;Password=postgresPass";

            var builder = new DbContextOptionsBuilder();
            builder.UseNpgsql(testDbConnStr);
            AddressesContext = new AddressesContext(builder.Options);
            AddressesContext.Database.EnsureCreated();
            _transaction = AddressesContext.Database.BeginTransaction();
        }

        [TearDown]
        public void RunAfterAnyTests()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            ClearDb();
        }

        private void ClearDb()
        {
            AddressesContext.Database.ExecuteSqlRaw("TRUNCATE TABLE hackney_address CASCADE");
        }
    }
}
