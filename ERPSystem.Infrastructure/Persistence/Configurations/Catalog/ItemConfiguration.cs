using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Catalog;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Catalog
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items", "Catalog");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Code).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(250);
            builder.Property(t => t.NameAr).HasMaxLength(250);
            
            builder.HasOne(t => t.Category)
                .WithMany(t => t.Items)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
