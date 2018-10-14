using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gap.Domain.Customer.Persistence.EntityConfigurations
{
    public class CustomerInsuranceEntityTypeConfiguration : IEntityTypeConfiguration<Model.CustomerInsurance>
    {
        public void Configure(EntityTypeBuilder<Model.CustomerInsurance> builder)
        {
            builder.ToTable("CustomerInsurance", CustomerContext.DEFAULT_SCHEMA);

            builder.Property<int>("CustomerInsuranceID")
                .ForSqlServerUseSequenceHiLo("customerinsurance_seq", CustomerContext.DEFAULT_SCHEMA);

            builder.HasKey("CustomerInsuranceID");

            builder.Property(b => b.CustomerId)
                .IsRequired();

            builder.Property(b => b.InsuranceId)
                .IsRequired();

            builder.Property(b => b.Status)
                .IsRequired();

            builder.Property(b => b.AssigningDate)
                .IsRequired();

            builder.Property(b => b.CancellationDate)
                .IsRequired(false);

            builder.HasOne(pt => pt.Customer)
                .WithMany(p => p.Insurances)
                .HasForeignKey(pt => pt.CustomerId);
        }
    }
}
