using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Identity;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "Identity");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.RoleName).IsRequired().HasMaxLength(100);

            // تم تصحيح العلاقة بالأسماء الحقيقية
            builder.HasMany(t => t.RolePermissions)
                .WithOne(t => t.Role)
                .HasForeignKey(t => t.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
