using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gap.Domain.Insurance.Persistence.EntityConfigurations
{
    public class CoverageTypeConfiguration : IEntityTypeConfiguration<Model.CoverageType>
    {
        public void Configure(EntityTypeBuilder<Model.CoverageType> builder)
        {
            builder.ToTable("CoverageTypes", InsuranceContext.DEFAULT_SCHEMA);

            builder.Ignore(b => b.DomainEvents);

            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(ct => ct.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ct => ct.Description)
                .HasMaxLength(500)
                .IsRequired(false);
        }
    }
}
