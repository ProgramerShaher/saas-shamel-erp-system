using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Finance;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Finance
{
    public class ChartOfAccountConfiguration : IEntityTypeConfiguration<ChartOfAccount>
    {
        public void Configure(EntityTypeBuilder<ChartOfAccount> builder)
        {
            builder.ToTable("ChartOfAccounts", "Finance");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.AccountCode).IsRequired().HasMaxLength(50);
            builder.Property(t => t.AccountName).IsRequired().HasMaxLength(250);
            builder.Property(t => t.CurrentBalance).HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.ParentAccount)
                .WithMany(t => t.SubAccounts)
                .HasForeignKey(t => t.ParentAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
