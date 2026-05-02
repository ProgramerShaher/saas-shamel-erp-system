using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Sales;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Sales
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "Sales");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(250); // تصحيح من CustomerName
            builder.Property(t => t.PhoneNumber).HasMaxLength(50);
            builder.Property(t => t.CurrentBalance).HasColumnType("decimal(18,2)");
            builder.Property(t => t.CreditLimit).HasColumnType("decimal(18,2)");
            
            // حذف Navigation Property المفقودة (SalesInvoices)
        }
    }
}
