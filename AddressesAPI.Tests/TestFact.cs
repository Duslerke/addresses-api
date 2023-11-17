using AddressesAPI.v1.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AddressesAPI.Tests
{
    public class TestFact : WebApplicationFactory<Program>
    {
        // protected AddressesContext AddressesContext { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var testDbConnStr = $"Host=localhost;Port=5430;Database=testdb;Username=postgresuser;Password=postgresPass";

            var dbBuilder = new DbContextOptionsBuilder();
            dbBuilder.UseNpgsql(testDbConnStr);
            var addressesContext = new AddressesContext(dbBuilder.Options);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(addressesContext);
                var serviceProvider = services.BuildServiceProvider();
                var dbContext = serviceProvider.GetRequiredService<AddressesContext>();
                Clear(dbContext);
                var seedAddressData = Randomizer.Create<AddressEntity>();
                seedAddressData.PostcodeNospace = "E81LL";
                dbContext.Addresses.Add(seedAddressData);
                dbContext.SaveChanges();               
            });
        }

        private void Clear(AddressesContext context)
        {
            context.Addresses.RemoveRange(context.Addresses);
        }
    }
}
