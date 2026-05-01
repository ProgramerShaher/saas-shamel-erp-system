using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Finance
{
    /// <summary>
    /// الطرف الدائن أو المدين داخل القيد الواحد.
    /// </summary>
    public class JournalEntryLine : TenantBaseEntity
    {
        /// <summary>القيد الأم</summary>
        public Guid JournalEntryId { get; set; }
        
        /// <summary>الوثيقة الأصلية للقيد</summary>
        public virtual JournalEntry JournalEntry { get; set; } = null!;

        /// <summary>الحساب المحاسبي الذي تم رمي العملية عليه</summary>
        public Guid AccountId { get; set; }
        
        /// <summary>الدليل المحاسبي</summary>
        public virtual ChartOfAccount Account { get; set; } = null!;

        /// <summary>مركز التكلفة لتقييم أداء الفروع أو المشاريع بمعزل عن بقية الشركة (Cost Center)</summary>
        public Guid? CostCenterId { get; set; }

        /// <summary>القيمة المدينة (Debit)</summary>
        public decimal Debit { get; set; }
        
        /// <summary>القيمة الدائنة (Credit)</summary>
        public decimal Credit { get; set; }

        /// <summary>شرح وتفصيل مصغر للسطر (مثال: عمولة بنك الراجحي)</summary>
        public string? Description { get; set; }
    }
}
