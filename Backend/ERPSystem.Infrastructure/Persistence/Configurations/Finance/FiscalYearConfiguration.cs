using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Finance;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Finance
{
    public class FiscalYearConfiguration : IEntityTypeConfiguration<FiscalYear>
    {
        public void Configure(EntityTypeBuilder<FiscalYear> builder)
        {
            builder.ToTable("FiscalYears", "Finance");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.YearName).IsRequired().HasMaxLength(50);
        }
    }
}
