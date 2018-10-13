using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gap.Domain.Insurance.Persistence.EntityConfigurations
{
    public class InsuranceEntityTypeConfiguration : IEntityTypeConfiguration<Model.Insurance>
    {
        public void Configure(EntityTypeBuilder<Model.Insurance> builder)
        {
            builder.ToTable("Insurances", InsuranceContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Id)
                .ForSqlServerUseSequenceHiLo("insurance_seq", InsuranceContext.DEFAULT_SCHEMA);

            builder.Property(b => b.Name)
                .IsRequired();

            builder.Property(b => b.Risk)
                .IsRequired();

            builder.Property(b => b.Cost)
                .IsRequired();

            builder.Property(b => b.CoveragePeriod)
                .IsRequired();

            builder.HasIndex(x => x.CreationDate)
                .IsUnique();

            builder.HasIndex(x => x.CustomerId)
                .IsUnique();

            builder.HasIndex(x => x.Description)
                .IsUnique(false);

            builder.HasIndex(x => x.StartDate)
                .IsUnique();
        }
    }
}
