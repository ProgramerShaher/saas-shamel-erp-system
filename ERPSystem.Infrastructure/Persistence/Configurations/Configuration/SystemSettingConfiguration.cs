using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Configuration;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Configuration
{
    public class SystemSettingConfiguration : IEntityTypeConfiguration<SystemSetting>
    {
        public void Configure(EntityTypeBuilder<SystemSetting> builder)
        {
            builder.ToTable("SystemSettings", "Configuration");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.SettingGroup).IsRequired().HasMaxLength(100);
            builder.Property(t => t.SettingKey).IsRequired().HasMaxLength(100);
            builder.Property(t => t.SettingValue).IsRequired().HasMaxLength(1000);
        }
    }
}
