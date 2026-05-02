using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Inventory;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Inventory
{
    public class StockLedgerConfiguration : IEntityTypeConfiguration<StockLedger>
    {
        public void Configure(EntityTypeBuilder<StockLedger> builder)
        {
            builder.ToTable("StockLedger", "Inventory");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Quantity).HasColumnType("decimal(18,4)");
            builder.Property(t => t.UnitCost).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalCost).HasColumnType("decimal(18,2)");
            builder.Property(t => t.SourceDocumentType).IsRequired().HasMaxLength(100);

            builder.HasOne(t => t.Item)
                .WithMany()
                .HasForeignKey(t => t.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
