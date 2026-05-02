using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Tenancy
{
    public class TenantSubscriptionHistoryConfiguration : IEntityTypeConfiguration<TenantSubscriptionHistory>
    {
        public void Configure(EntityTypeBuilder<TenantSubscriptionHistory> builder)
        {
            builder.ToTable("TenantSubscriptionHistory", "Tenancy");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.InvoiceRef).HasMaxLength(100); // تصحيح من ReferenceInvoiceNumber

            // حذف حقل PaidAmount المفقود بالكيان الحقيقي

            builder.HasOne(t => t.Plan) // تصحيح من SubscriptionPlan
                .WithMany()
                .HasForeignKey(t => t.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
