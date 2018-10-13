using Gap.Domain.Insurance.Persistence.EntityConfigurations;
using Gap.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gap.Domain.Insurance.Persistence
{
    public class InsuranceContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "Insurance";

        public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new CoverageTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InsuranceCoverageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InsuranceEntityTypeConfiguration());
        }

        public DbSet<Model.Insurance> Insurances { get; set; }

        public DbSet<Model.CoverageType> CoverageTypes { get; set; }

        public DbSet<Model.InsuranceCoverage> InsuranceCoverages { get; set; }
    }

    // just to create the migrations.
    public class DriverContextDesignFactory : IDesignTimeDbContextFactory<InsuranceContext>
    {
        public InsuranceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InsuranceContext>()
                .UseSqlServer("Server=.;Initial Catalog=Gap.Insurance;Integrated Security=true");

            return new InsuranceContext(optionsBuilder.Options);
        }
    }
}
