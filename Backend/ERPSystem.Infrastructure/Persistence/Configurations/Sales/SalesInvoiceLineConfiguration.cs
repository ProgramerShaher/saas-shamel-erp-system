using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Sales;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Sales
{
    public class SalesInvoiceLineConfiguration : IEntityTypeConfiguration<SalesInvoiceLine>
    {
        public void Configure(EntityTypeBuilder<SalesInvoiceLine> builder)
        {
            builder.ToTable("SalesInvoiceLines", "Sales");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Quantity).HasColumnType("decimal(18,4)");
            builder.Property(t => t.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.DiscountAmount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TaxAmount).HasColumnType("decimal(18,2)");
            
            builder.Property(t => t.BatchNumber).HasMaxLength(100);
            builder.Property(t => t.SerialNumber).HasMaxLength(150);

            builder.HasOne(t => t.SalesInvoice)
                .WithMany(t => t.Lines)
                .HasForeignKey(t => t.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
