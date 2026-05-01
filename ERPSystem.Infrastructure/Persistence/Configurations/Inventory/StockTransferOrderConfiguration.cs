using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Inventory;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Inventory
{
    /// <summary>
    /// تكوين جدول أوامر التحويل المخزني (StockTransferOrders).
    /// </summary>
    public class StockTransferOrderConfiguration : IEntityTypeConfiguration<StockTransferOrder>
    {
        public void Configure(EntityTypeBuilder<StockTransferOrder> builder)
        {
            builder.ToTable("StockTransferOrders", "Inventory");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TransferNumber).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Notes).HasMaxLength(500);

            builder.HasOne(t => t.FromWarehouse)
                .WithMany()
                .HasForeignKey(t => t.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.ToWarehouse)
                .WithMany()
                .HasForeignKey(t => t.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
