using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Finance;

namespace ERPsystem.Domain.Entities.Finance
{
    /// <summary>
    /// الفترة المحاسبية الدورية (غالباً الأجزاء الشهرية داخل السنة المالية).
    /// </summary>
    public class AccountingPeriod : TenantBaseEntity
    {
        /// <summary>السنة المالية الرئيسية</summary>
        public Guid FiscalYearId { get; set; }
        
        /// <summary>السنة</summary>
        public virtual FiscalYear FiscalYear { get; set; } = null!;

        /// <summary>اسم الفترة (مثال: الربع الأول، شهر يناير، فترة 1)</summary>
        public string PeriodName { get; set; } = null!;
        
        /// <summary>بداية الفترة</summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>نهاية الفترة</summary>
        public DateTime EndDate { get; set; }

        /// <summary>حالة الفترة الحالية</summary>
        public PeriodStatus Status { get; set; } = PeriodStatus.Open;
    }
}
