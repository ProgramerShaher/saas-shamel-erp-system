using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Sales;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Sales
{
    public class SalesReturnConfiguration : IEntityTypeConfiguration<SalesReturn>
    {
        public void Configure(EntityTypeBuilder<SalesReturn> builder)
        {
            builder.ToTable("SalesReturns", "Sales");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.ReturnNumber).IsRequired().HasMaxLength(150);
            builder.Property(t => t.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalTax).HasColumnType("decimal(18,2)");
            builder.Property(t => t.GrandTotal).HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.SalesInvoice)
                .WithMany()
                .HasForeignKey(t => t.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
