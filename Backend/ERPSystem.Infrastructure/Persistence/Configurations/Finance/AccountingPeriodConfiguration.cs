using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Finance;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Finance
{
    public class AccountingPeriodConfiguration : IEntityTypeConfiguration<AccountingPeriod>
    {
        public void Configure(EntityTypeBuilder<AccountingPeriod> builder)
        {
            builder.ToTable("AccountingPeriods", "Finance");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.PeriodName).IsRequired().HasMaxLength(100);

            builder.HasOne(t => t.FiscalYear)
                .WithMany(t => t.Periods)
                .HasForeignKey(t => t.FiscalYearId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
