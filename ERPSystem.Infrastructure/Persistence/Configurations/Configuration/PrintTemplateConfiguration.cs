using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Configuration;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Configuration
{
    public class PrintTemplateConfiguration : IEntityTypeConfiguration<PrintTemplate>
    {
        public void Configure(EntityTypeBuilder<PrintTemplate> builder)
        {
            builder.ToTable("PrintTemplates", "Configuration");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.TemplateName).IsRequired().HasMaxLength(150);
            builder.Property(t => t.TargetDocument).IsRequired().HasMaxLength(100);
            builder.Property(t => t.TemplateDesignJsonHtml).IsRequired().HasColumnType("nvarchar(max)");
        }
    }
}
