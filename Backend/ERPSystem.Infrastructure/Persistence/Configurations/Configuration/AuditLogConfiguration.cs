using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Configuration;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Configuration
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs", "Configuration");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.EntityName).IsRequired().HasMaxLength(150);
            builder.Property(t => t.OldValuesJson).HasColumnType("nvarchar(max)");
            builder.Property(t => t.NewValuesJson).HasColumnType("nvarchar(max)");
            builder.Property(t => t.IpAddress).HasMaxLength(50);
        }
    }
}
