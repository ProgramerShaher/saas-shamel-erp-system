using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Catalog;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Catalog
{
    public class ItemBarcodeConfiguration : IEntityTypeConfiguration<ItemBarcode>
    {
        public void Configure(EntityTypeBuilder<ItemBarcode> builder)
        {
            builder.ToTable("ItemBarcodes", "Catalog");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Barcode).IsRequired().HasMaxLength(100);

            builder.HasOne(t => t.Item)
                .WithMany(t => t.Barcodes)
                .HasForeignKey(t => t.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
