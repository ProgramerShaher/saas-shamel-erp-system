using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Tenancy
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.ToTable("SubscriptionPlans", "Tenancy");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.PlanName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.MonthlyPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.AnnualPrice).HasColumnType("decimal(18,2)"); // تصحيح من YearlyPrice
        }
    }
}
