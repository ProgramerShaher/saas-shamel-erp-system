using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Inventory;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Inventory
{
    public class BatchLotConfiguration : IEntityTypeConfiguration<BatchLot>
    {
        public void Configure(EntityTypeBuilder<BatchLot> builder)
        {
            builder.ToTable("BatchLots", "Inventory");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.BatchNumber).IsRequired().HasMaxLength(100);
            builder.Property(t => t.QuantityReceived).HasColumnType("decimal(18,4)");
            builder.Property(t => t.QuantityRemaining).HasColumnType("decimal(18,4)");
            builder.Property(t => t.CostPerUnit).HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.Item)
                .WithMany()
                .HasForeignKey(t => t.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Warehouse)
                .WithMany()
                .HasForeignKey(t => t.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
