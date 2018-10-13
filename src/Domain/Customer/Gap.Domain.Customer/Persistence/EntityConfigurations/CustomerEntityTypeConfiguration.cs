using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gap.Domain.Customer.Persistence.EntityConfigurations
{
    internal class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Model.Customer>
    {
        public void Configure(EntityTypeBuilder<Model.Customer> builder)
        {
            builder.ToTable("Customer", CustomerContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Id)
                .ForSqlServerUseSequenceHiLo("customer_seq", CustomerContext.DEFAULT_SCHEMA);

            builder.Property(b => b.Name)
                .IsRequired();

            builder.Property(b => b.PhoneNumber)
                .IsRequired(false);

            builder.Property(b => b.Email)
                .IsRequired();
        }
    }
}
