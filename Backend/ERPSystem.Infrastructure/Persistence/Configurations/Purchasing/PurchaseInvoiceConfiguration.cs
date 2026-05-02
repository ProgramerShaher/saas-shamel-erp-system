using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Purchasing;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Purchasing
{
    public class PurchaseInvoiceConfiguration : IEntityTypeConfiguration<PurchaseInvoice>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoice> builder)
        {
            builder.ToTable("PurchaseInvoices", "Purchasing");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.InvoiceNumber).IsRequired().HasMaxLength(100);
            builder.Property(t => t.SupplierInvoiceNumber).HasMaxLength(100); // تصحيح من VendorInvoiceNumber
            builder.Property(t => t.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalDiscount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalTax).HasColumnType("decimal(18,2)");
            builder.Property(t => t.GrandTotal).HasColumnType("decimal(18,2)");
            builder.Property(t => t.AmountPaid).HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.Supplier)
                .WithMany() // تم تصحيح التنقل المفقود في المورد
                .HasForeignKey(t => t.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
