using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Inventory;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Inventory
{
    public class WarehouseStockConfiguration : IEntityTypeConfiguration<WarehouseStock>
    {
        public void Configure(EntityTypeBuilder<WarehouseStock> builder)
        {
            builder.ToTable("WarehouseStocks", "Inventory");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.QuantityOnHand).HasColumnType("decimal(18,4)");
            builder.Property(t => t.QuantityReserved).HasColumnType("decimal(18,4)");

            // قيمة محسوبة — لا تُخزَّن في DB
            builder.Ignore(t => t.QuantityAvailable);

            // فهرس مركب لسرعة الاستعلام عن صنف في مستودع محدد
            builder.HasIndex(t => new { t.ItemId, t.WarehouseId, t.VariantId }).IsUnique();

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
