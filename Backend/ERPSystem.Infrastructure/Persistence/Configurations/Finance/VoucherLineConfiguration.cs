using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Finance;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Finance
{
    public class VoucherLineConfiguration : IEntityTypeConfiguration<VoucherLine>
    {
        public void Configure(EntityTypeBuilder<VoucherLine> builder)
        {
            builder.ToTable("VoucherLines", "Finance");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Amount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.Description).HasMaxLength(500);

            builder.HasOne(t => t.Voucher)
                .WithMany(t => t.Lines)
                .HasForeignKey(t => t.VoucherId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
