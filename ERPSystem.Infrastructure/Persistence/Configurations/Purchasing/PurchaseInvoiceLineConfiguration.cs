using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Purchasing;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Purchasing
{
    public class PurchaseInvoiceLineConfiguration : IEntityTypeConfiguration<PurchaseInvoiceLine>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoiceLine> builder)
        {
            builder.ToTable("PurchaseInvoiceLines", "Purchasing");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Quantity).HasColumnType("decimal(18,4)");
            builder.Property(t => t.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TaxAmount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.DiscountAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.PurchaseInvoice)
                .WithMany(t => t.Lines)
                .HasForeignKey(t => t.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
