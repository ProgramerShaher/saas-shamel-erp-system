using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Inventory;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Inventory
{
    public class InventoryAdjustmentConfiguration : IEntityTypeConfiguration<InventoryAdjustment>
    {
        public void Configure(EntityTypeBuilder<InventoryAdjustment> builder)
        {
            builder.ToTable("InventoryAdjustments", "Inventory");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.AdjustmentNumber).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Notes).HasMaxLength(500);

            builder.HasOne(t => t.Warehouse)
                .WithMany()
                .HasForeignKey(t => t.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
