using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Finance;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Finance
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Vouchers", "Finance");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.VoucherNumber).IsRequired().HasMaxLength(150);
            builder.Property(t => t.PartyName).IsRequired().HasMaxLength(250);
            builder.Property(t => t.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.Description).HasMaxLength(500);
        }
    }
}
