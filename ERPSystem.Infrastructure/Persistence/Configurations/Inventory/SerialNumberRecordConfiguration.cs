using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Inventory;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Inventory
{
    public class SerialNumberRecordConfiguration : IEntityTypeConfiguration<SerialNumberRecord>
    {
        public void Configure(EntityTypeBuilder<SerialNumberRecord> builder)
        {
            builder.ToTable("SerialNumberRecords", "Inventory");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.SerialNumber).IsRequired().HasMaxLength(150);
            builder.Property(t => t.Notes).HasMaxLength(500);

            // فهرس فريد على رقم السيريال لمنع التكرار
            builder.HasIndex(t => t.SerialNumber).IsUnique();

            builder.HasOne(t => t.Item)
                .WithMany()
                .HasForeignKey(t => t.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Warehouse)
                .WithMany()
                .HasForeignKey(t => t.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
