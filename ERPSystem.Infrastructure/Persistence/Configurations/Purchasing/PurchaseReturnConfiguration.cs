using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Purchasing;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Purchasing
{
    public class PurchaseReturnConfiguration : IEntityTypeConfiguration<PurchaseReturn>
    {
        public void Configure(EntityTypeBuilder<PurchaseReturn> builder)
        {
            builder.ToTable("PurchaseReturns", "Purchasing");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.ReturnNumber).IsRequired().HasMaxLength(150);
            builder.Property(t => t.GrandTotal).HasColumnType("decimal(18,2)");
        }
    }
}
