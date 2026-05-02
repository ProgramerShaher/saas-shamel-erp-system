using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Purchasing;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Purchasing
{
    public class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
        {
            builder.ToTable("PurchaseOrderLines", "Purchasing");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.OrderedQty).HasColumnType("decimal(18,4)"); // تصحيح من OrderedQuantity
            builder.Property(t => t.ReceivedQty).HasColumnType("decimal(18,4)"); // تصحيح من ReceivedQuantity
            builder.Property(t => t.UnitPrice).HasColumnType("decimal(18,2)"); // تصحيح من EstimatedUnitPrice
            builder.Ignore(t => t.TotalPrice); // حقل محسوب

            builder.HasOne(t => t.PurchaseOrder)
                .WithMany(t => t.Lines)
                .HasForeignKey(t => t.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
