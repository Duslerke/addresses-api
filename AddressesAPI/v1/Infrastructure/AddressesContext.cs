using Microsoft.EntityFrameworkCore;

namespace AddressesAPI.v1.Infrastructure
{
    public class AddressesContext : DbContext
    {
        public AddressesContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<AddressEntity> Addresses { get; set; }
    }
}
