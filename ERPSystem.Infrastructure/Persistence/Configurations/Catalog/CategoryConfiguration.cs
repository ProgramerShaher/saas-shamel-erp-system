using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Catalog;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Catalog
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "Catalog");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Name).IsRequired().HasMaxLength(200);

            builder.HasOne(t => t.ParentCategory)
                .WithMany(t => t.SubCategories)
                .HasForeignKey(t => t.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
