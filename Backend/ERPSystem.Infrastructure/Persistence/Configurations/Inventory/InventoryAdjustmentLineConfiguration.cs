using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Inventory;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Inventory
{
    public class InventoryAdjustmentLineConfiguration : IEntityTypeConfiguration<InventoryAdjustmentLine>
    {
        public void Configure(EntityTypeBuilder<InventoryAdjustmentLine> builder)
        {
            builder.ToTable("InventoryAdjustmentLines", "Inventory");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.SystemQty).HasColumnType("decimal(18,4)");
            builder.Property(t => t.PhysicalQty).HasColumnType("decimal(18,4)");
            builder.Property(t => t.UnitCost).HasColumnType("decimal(18,2)");
            builder.Ignore(t => t.DifferenceQty); // حقل محسوب
            builder.Ignore(t => t.TotalAdjustmentValue); // حقل محسوب

            builder.HasOne(t => t.Adjustment)
                .WithMany(t => t.Lines)
                .HasForeignKey(t => t.AdjustmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
