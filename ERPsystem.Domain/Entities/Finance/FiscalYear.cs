using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Finance;

namespace ERPsystem.Domain.Entities.Finance
{
    /// <summary>
    /// السنة المالية الخاصة بالمنشأة (التي تتضمن جميع الفترات).
    /// </summary>
    public class FiscalYear : TenantBaseEntity
    {
        /// <summary>الاسم الدال على السنة (مثال: FY-2024)</summary>
        public string YearName { get; set; } = null!;

        /// <summary>تاريخ بداية السنة المالية</summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>تاريخ انقضاء السنة واقفالها</summary>
        public DateTime EndDate { get; set; }

        /// <summary>الحالة المحاسبية (مفتوحة للإدخال، أو مغلقة للأبد بعد التقفيل)</summary>
        public FiscalYearStatus Status { get; set; } = FiscalYearStatus.Open;

        /// <summary>الفترات/الأشهر التابعة لهذه السنة</summary>
        public virtual ICollection<AccountingPeriod> Periods { get; set; } = new List<AccountingPeriod>();
    }
}
