using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Sales;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Sales
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", "Sales");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.PaymentNumber).IsRequired().HasMaxLength(150);
            builder.Property(t => t.Amount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TransactionReference).HasMaxLength(150);
        }
    }
}
