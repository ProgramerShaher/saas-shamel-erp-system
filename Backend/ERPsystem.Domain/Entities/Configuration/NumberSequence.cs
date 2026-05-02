using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Configuration
{
    /// <summary>
    /// المتتاليات الرقمية وتوليد البادئات للفواتير والقيود.
    /// (Numbering Sequences - Prefix generation).
    /// </summary>
    public class NumberSequence : TenantBaseEntity
    {
        /// <summary>نوع الكيان الذي ينتمي له هذا التسلسل (مثال: Invoice, JournalEntry)</summary>
        public string EntityType { get; set; } = null!;

        /// <summary>البادئة النصية الثابتة (مثال: INV-)</summary>
        public string Prefix { get; set; } = "";
        
        /// <summary>اللاحقة أو الذيل الشكلي (مثال: -2024)</summary>
        public string Suffix { get; set; } = "";

        /// <summary>طول وأصفار الحشو (مثال حشو 6 لحالة 000001)</summary>
        public int NumberLength { get; set; } = 4;

        /// <summary>آخر رقم تم الوصول إليه وحجزه من الكاونتر</summary>
        public long LastNumber { get; set; } = 0;
        
        /// <summary>هل هذا التسلسل مرتبط بفرع معين؟ لتمييز فواتير الفروع ببادئات مختلفة عن بعض</summary>
        public Guid? BranchId { get; set; }
    }
}
