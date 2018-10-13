using Gap.Domain.Customer.Persistence.EntityConfigurations;
using Gap.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gap.Domain.Customer.Persistence
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "Customer";

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
        }

        public DbSet<Model.Customer> Customers { get; set; }
    }

    // just to create the migrations.
    public class DriverContextDesignFactory : IDesignTimeDbContextFactory<CustomerContext>
    {
        public CustomerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>()
                .UseSqlServer("Server=.;Initial Catalog=Gap.Insurance;Integrated Security=true");

            return new CustomerContext(optionsBuilder.Options);
        }
    }
}
