using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Purchasing;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Purchasing
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("PurchaseOrders", "Purchasing");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.OrderNumber).IsRequired().HasMaxLength(100);
            builder.Property(t => t.TotalAmount).HasColumnType("decimal(18,2)");

            // تصحيح: المورد لا يملك قائمة PurchaseOrders في الكيان الحقيقي
            builder.HasOne(t => t.Supplier)
                .WithMany()
                .HasForeignKey(t => t.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
