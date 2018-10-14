using System;
using System.Threading;
using System.Threading.Tasks;
using Gap.Domain.Customer.Persistence.EntityConfigurations;
using Gap.Infrastructure.Extensions;
using Gap.Infrastructure.Repository.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gap.Domain.Customer.Persistence
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public const string DEFAULT_SCHEMA = "Customer";

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public CustomerContext(DbContextOptions<CustomerContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerInsuranceEntityTypeConfiguration());
        }

        public DbSet<Model.Customer> Customers { get; set; }

        public DbSet<Model.CustomerInsurance> CustomerInsurances { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            await base.SaveChangesAsync(cancellationToken);

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            return true;
        }
    }

    // just to create the migrations.
    public class DriverContextDesignFactory : IDesignTimeDbContextFactory<CustomerContext>
    {
        public CustomerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>()
                .UseSqlServer("Server=.;Initial Catalog=Gap.Insurance;Integrated Security=true");

            return new CustomerContext(optionsBuilder.Options, new NoMediator());
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
