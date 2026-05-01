using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Identity;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "Identity");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Username).IsRequired().HasMaxLength(150);
            builder.Property(t => t.Email).HasMaxLength(200);
            builder.Property(t => t.PasswordHash).IsRequired();
            builder.Property(t => t.FullName).IsRequired().HasMaxLength(250);

            // تم تصحيح التنقل: المستخدم له UserRoles وليس Role مباشرة
            builder.HasMany(t => t.UserRoles)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
