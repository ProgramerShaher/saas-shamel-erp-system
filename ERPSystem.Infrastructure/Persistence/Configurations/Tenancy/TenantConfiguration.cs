using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Tenancy
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants", "Tenancy");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Slug).IsRequired().HasMaxLength(100);
            builder.Property(t => t.DatabaseSchema).IsRequired().HasMaxLength(50);

            // تم تصحيح علاقة SubscriptionHistory بدلاً من SubscriptionHistories المفقودة
            builder.HasMany(t => t.SubscriptionHistory)
                .WithOne(t => t.Tenant)
                .HasForeignKey(t => t.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
