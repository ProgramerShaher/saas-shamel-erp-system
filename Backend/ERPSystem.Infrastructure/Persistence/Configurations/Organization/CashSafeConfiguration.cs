using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Organization;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Organization
{
    public class CashSafeConfiguration : IEntityTypeConfiguration<CashSafe>
    {
        public void Configure(EntityTypeBuilder<CashSafe> builder)
        {
            builder.ToTable("CashSafes", "Organization");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(150);
            builder.Property(t => t.CurrentBalance).HasColumnType("decimal(18,2)");
            builder.Property(t => t.CurrencyCode).HasMaxLength(10);
        }
    }
}
