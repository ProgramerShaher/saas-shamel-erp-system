using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERPsystem.Domain.Entities.Sales;

namespace ERPsystem.Infrastructure.Persistence.Configurations.Sales
{
    public class PosShiftConfiguration : IEntityTypeConfiguration<PosShift>
    {
        public void Configure(EntityTypeBuilder<PosShift> builder)
        {
            builder.ToTable("PosShifts", "Sales");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.ShiftNumber).IsRequired().HasMaxLength(100);
            builder.Property(t => t.OpeningCashBalance).HasColumnType("decimal(18,2)"); // تصحيح من OpeningAmount
            builder.Property(t => t.ExpectedCashBalance).HasColumnType("decimal(18,2)"); // تصحيح من ClosingAmount المفقود
            builder.Property(t => t.ActualCashBalance).HasColumnType("decimal(18,2)");
            builder.Ignore(t => t.DifferenceValue); // حقل محسوب (تصحيح من VarianceAmount)

            // حذف حقل CashSafe لأنه تم تسميته PosTerminal وموجود كعلاقة
            builder.HasOne(t => t.PosTerminal)
                .WithMany()
                .HasForeignKey(t => t.PosTerminalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
