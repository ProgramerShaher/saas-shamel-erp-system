using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Inventory;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Inventory
{
    /// <summary>
    /// تكوين جدول أسطر أوامر التحويل المخزني (StockTransferLines).
    /// </summary>
    public class StockTransferLineConfiguration : IEntityTypeConfiguration<StockTransferLine>
    {
        public void Configure(EntityTypeBuilder<StockTransferLine> builder)
        {
            builder.ToTable("StockTransferLines", "Inventory");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.RequestedQty).HasColumnType("decimal(18,4)");
            builder.Property(t => t.SentQty).HasColumnType("decimal(18,4)");
            builder.Property(t => t.ReceivedQty).HasColumnType("decimal(18,4)");
            builder.Property(t => t.UnitCost).HasColumnType("decimal(18,2)");
            builder.Property(t => t.BatchNumber).HasMaxLength(100);

            // العلاقة مع أمر التحويل
            builder.HasOne(t => t.TransferOrder)
                .WithMany(t => t.Lines)
                .HasForeignKey(t => t.TransferOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
