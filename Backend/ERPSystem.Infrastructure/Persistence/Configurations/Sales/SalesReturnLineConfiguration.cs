using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Sales;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Sales
{
    public class SalesReturnLineConfiguration : IEntityTypeConfiguration<SalesReturnLine>
    {
        public void Configure(EntityTypeBuilder<SalesReturnLine> builder)
        {
            builder.ToTable("SalesReturnLines", "Sales");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.ReturnedQty).HasColumnType("decimal(18,4)");
            builder.Property(t => t.UnitRefundPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TaxAmount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.ReturnReason).HasMaxLength(250);

            builder.HasOne(t => t.SalesReturn)
                .WithMany(t => t.Lines)
                .HasForeignKey(t => t.SalesReturnId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
