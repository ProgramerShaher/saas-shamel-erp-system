using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Purchasing;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Purchasing
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers", "Purchasing");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(250); // تصحيح من SupplierName
            builder.Property(t => t.PhoneNumber).HasMaxLength(50); // تصحيح من ContactPhone
            builder.Property(t => t.TaxNumber).HasMaxLength(50);
            builder.Property(t => t.CurrentBalance).HasColumnType("decimal(18,2)");
            builder.Property(t => t.CreditLimit).HasColumnType("decimal(18,2)");
        }
    }
}
