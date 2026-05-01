using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Catalog;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Catalog
{
    /// <summary>
    /// تكوين جدول الخصومات (Discounts).
    /// يحل محل الخاطئ DiscountPolicyConfiguration ويطابق الكيان الحقيقي.
    /// </summary>
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts", "Catalog");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Value).HasColumnType("decimal(18,2)");
            builder.Property(t => t.MinPurchaseAmount).HasColumnType("decimal(18,2)");

            // علاقة اختيارية بالفرع (Null = يطبق على كل الفروع)
            builder.HasOne(t => t.Branch)
                .WithMany()
                .HasForeignKey(t => t.BranchId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
