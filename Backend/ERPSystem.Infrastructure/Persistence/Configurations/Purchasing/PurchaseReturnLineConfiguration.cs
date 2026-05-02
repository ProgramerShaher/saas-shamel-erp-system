using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Purchasing;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Purchasing
{
    public class PurchaseReturnLineConfiguration : IEntityTypeConfiguration<PurchaseReturnLine>
    {
        public void Configure(EntityTypeBuilder<PurchaseReturnLine> builder)
        {
            builder.ToTable("PurchaseReturnLines", "Purchasing");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.ReturnedQty).HasColumnType("decimal(18,4)");
            builder.Property(t => t.UnitRefundPrice).HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.PurchaseReturn)
                .WithMany(t => t.Lines)
                .HasForeignKey(t => t.PurchaseReturnId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
