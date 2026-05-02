using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Tenancy
{
    public class TenantFeatureFlagConfiguration : IEntityTypeConfiguration<TenantFeatureFlag>
    {
        public void Configure(EntityTypeBuilder<TenantFeatureFlag> builder)
        {
            builder.ToTable("TenantFeatureFlags", "Tenancy");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.FeatureKey).IsRequired().HasMaxLength(100); // تصحيح من FeatureName
            builder.Property(t => t.ConfigJson).HasMaxLength(2000);
        }
    }
}
