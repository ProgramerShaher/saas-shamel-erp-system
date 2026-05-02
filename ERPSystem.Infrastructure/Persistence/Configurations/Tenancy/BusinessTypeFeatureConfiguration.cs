using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Tenancy
{
    /// <summary>
    /// إعدادات جدول قوالب الميزات لكل نوع نشاط.
    /// </summary>
    public class BusinessTypeFeatureConfiguration : IEntityTypeConfiguration<BusinessTypeFeature>
    {
        public void Configure(EntityTypeBuilder<BusinessTypeFeature> builder)
        {
            builder.ToTable("BusinessTypeFeatures", "Tenancy");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.BusinessType).IsRequired();
            builder.Property(t => t.FeatureKey).IsRequired();

            // منع تكرار نفس الميزة لنفس النشاط التجاري
            builder.HasIndex(t => new { t.BusinessType, t.FeatureKey }).IsUnique();
        }
    }
}
