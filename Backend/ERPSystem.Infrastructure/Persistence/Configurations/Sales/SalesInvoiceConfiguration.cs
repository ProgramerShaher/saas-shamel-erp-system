using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Sales;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Sales
{
    public class SalesInvoiceConfiguration : IEntityTypeConfiguration<SalesInvoice>
    {
        public void Configure(EntityTypeBuilder<SalesInvoice> builder)
        {
            builder.ToTable("SalesInvoices", "Sales");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.InvoiceNumber).IsRequired().HasMaxLength(150);
            builder.Property(t => t.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalDiscount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalTax).HasColumnType("decimal(18,2)");
            builder.Property(t => t.GrandTotal).HasColumnType("decimal(18,2)");

            // تصحيح: العميل لا يملك قائمة SalesInvoices في الكيان الحقيقي
            builder.HasOne(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
