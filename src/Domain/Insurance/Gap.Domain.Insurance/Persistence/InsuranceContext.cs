using System;
using System.Threading;
using System.Threading.Tasks;
using Gap.Domain.Insurance.Persistence.EntityConfigurations;
using Gap.Infrastructure.Extensions;
using Gap.Infrastructure.Repository.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gap.Domain.Insurance.Persistence
{
    public class InsuranceContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public const string DEFAULT_SCHEMA = "Insurance";

        public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options) { }

        public InsuranceContext(DbContextOptions<InsuranceContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new CoverageTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InsuranceCoverageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InsuranceEntityTypeConfiguration());
        }

        public DbSet<Model.Insurance> Insurances { get; set; }

        public DbSet<Model.CoverageType> CoverageTypes { get; set; }

        public DbSet<Model.InsuranceCoverage> InsuranceCoverages { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    // just to create the migrations.
    public class DriverContextDesignFactory : IDesignTimeDbContextFactory<InsuranceContext>
    {
        public InsuranceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InsuranceContext>()
                .UseSqlServer("Server=.;Initial Catalog=Gap.Insurance;Integrated Security=true");

            return new InsuranceContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }
        }
    }
}
