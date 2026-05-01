using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Organization;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Organization
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches", "Organization");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Code).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(150);
            
            // حذف الحقول غير الموجودة بالكيان (مثل TaxRegistrationNumber و ParentBranch)
            // حيث أن العول هنا عبر HeadquartersId

            builder.HasOne(t => t.Headquarters)
                .WithMany(t => t.Branches)
                .HasForeignKey(t => t.HeadquartersId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
