using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Finance;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Finance
{
    public class JournalEntryLineConfiguration : IEntityTypeConfiguration<JournalEntryLine>
    {
        public void Configure(EntityTypeBuilder<JournalEntryLine> builder)
        {
            builder.ToTable("JournalEntryLines", "Finance");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Debit).HasColumnType("decimal(18,2)");
            builder.Property(t => t.Credit).HasColumnType("decimal(18,2)");
            builder.Property(t => t.Description).HasMaxLength(500);

            builder.HasOne(t => t.JournalEntry)
                .WithMany(t => t.Lines)
                .HasForeignKey(t => t.JournalEntryId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(t => t.Account)
                .WithMany(t => t.JournalLines)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
