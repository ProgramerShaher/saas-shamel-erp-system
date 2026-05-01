using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Identity;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Identity
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions", "Identity");
            builder.HasKey(t => t.Id);
            
            // تم حذف الحقول الغير موجودة في كلاس الربط (موجودة في كلاس Permission الأساسي)
            builder.HasOne(t => t.Role)
                .WithMany(t => t.RolePermissions)
                .HasForeignKey(t => t.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Permission)
                .WithMany()
                .HasForeignKey(t => t.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
