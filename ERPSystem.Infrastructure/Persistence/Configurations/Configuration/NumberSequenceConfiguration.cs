using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Configuration;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Configuration
{
    public class NumberSequenceConfiguration : IEntityTypeConfiguration<NumberSequence>
    {
        public void Configure(EntityTypeBuilder<NumberSequence> builder)
        {
            builder.ToTable("NumberSequences", "Configuration");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.EntityType).IsRequired().HasMaxLength(150);
            builder.Property(t => t.Prefix).HasMaxLength(20);
            builder.Property(t => t.Suffix).HasMaxLength(20);
        }
    }
}
