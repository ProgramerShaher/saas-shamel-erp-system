using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Catalog;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Catalog
{
    public class ItemUnitConfiguration : IEntityTypeConfiguration<ItemUnit>
    {
        public void Configure(EntityTypeBuilder<ItemUnit> builder)
        {
            builder.ToTable("ItemUnits", "Catalog");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.ConversionFactor).HasColumnType("decimal(18,4)");

            builder.HasOne(t => t.Item)
                .WithMany(t => t.Units)
                .HasForeignKey(t => t.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
