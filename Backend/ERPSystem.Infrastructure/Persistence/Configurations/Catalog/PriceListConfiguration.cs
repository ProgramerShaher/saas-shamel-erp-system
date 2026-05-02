using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Catalog;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Catalog
{
    public class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
    {
        public void Configure(EntityTypeBuilder<PriceList> builder)
        {
            builder.ToTable("PriceLists", "Catalog");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Name).IsRequired().HasMaxLength(150);
        }
    }
}
