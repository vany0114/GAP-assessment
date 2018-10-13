using Gap.Domain.Insurance.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gap.Domain.Insurance.Persistence.EntityConfigurations
{
    public class InsuranceCoverageEntityTypeConfiguration : IEntityTypeConfiguration<Model.InsuranceCoverage>
    {
        public void Configure(EntityTypeBuilder<InsuranceCoverage> builder)
        {
            builder.ToTable("InsuranceCoverage", InsuranceContext.DEFAULT_SCHEMA);

            builder.HasKey(o => new { o.InsuranceId, o.CoverageId });

            builder.Property(b => b.CoverageId)
                .IsRequired();

            builder.Property(b => b.InsuranceId)
                .IsRequired();

            builder.Property(b => b.Percentage)
                .IsRequired();

            builder.HasOne(pt => pt.Insurance)
                .WithMany(p => p.Coverages)
                .HasForeignKey(pt => pt.InsuranceId);

            builder.HasOne(pt => pt.Coverage)
                .WithMany(t => t.InsuranceCoverages)
                .HasForeignKey(pt => pt.CoverageId);
        }
    }
}
