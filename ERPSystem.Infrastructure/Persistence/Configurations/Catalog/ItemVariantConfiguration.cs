using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Catalog;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Catalog
{
    public class ItemVariantConfiguration : IEntityTypeConfiguration<ItemVariant>
    {
        public void Configure(EntityTypeBuilder<ItemVariant> builder)
        {
            builder.ToTable("ItemVariants", "Catalog");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.SKU).IsRequired().HasMaxLength(150);
            builder.Property(t => t.SalePrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.CostPrice).HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.Item)
                .WithMany(t => t.Variants)
                .HasForeignKey(t => t.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
