using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Finance;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Finance
{
    public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
    {
        public void Configure(EntityTypeBuilder<JournalEntry> builder)
        {
            builder.ToTable("JournalEntries", "Finance");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.EntryNumber).IsRequired().HasMaxLength(150);
            builder.Property(t => t.Description).HasMaxLength(500);
            builder.Property(t => t.ReferenceDocumentType).HasMaxLength(150);
            builder.Property(t => t.TotalDebit).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalCredit).HasColumnType("decimal(18,2)");
        }
    }
}
