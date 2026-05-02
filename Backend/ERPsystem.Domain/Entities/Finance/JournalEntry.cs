using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Finance;

namespace ERPsystem.Domain.Entities.Finance
{
    /// <summary>
    /// القيد اليومي المزدوج (روتين المحاسبة).
    /// يسجل تلقائياً مع حركات المبيعات والشراء والمخزون. ويمكن إدخاله يدويا.
    /// </summary>
    public class JournalEntry : TenantBaseEntity
    {
        /// <summary>رقم القيد المتسلسل (مثال: JE-10029)</summary>
        public string EntryNumber { get; set; } = null!;

        /// <summary>سند الإدخال المحاسبي الفعلي</summary>
        public DateTime EntryDate { get; set; } = DateTime.UtcNow;

        /// <summary>وصف موجز للمبرر حول هذا القيد المالي لمنشأه</summary>
        public string Description { get; set; } = null!;

        /// <summary>هل القيد أوتوماتيكي ناتج عن فاتورة، أم مسجل يدويا من مسؤولي المالية؟</summary>
        public JournalEntryType EntryType { get; set; }

        /// <summary>نوع المستند المرجعي الأصلي إن كان اوتوماتيكي (SalesInvoice الخ)</summary>
        public string? ReferenceDocumentType { get; set; }
        
        /// <summary>الأي دي الخاص بالعملية التي أنتجت القيد من المبيعات والمخزن</summary>
        public Guid? ReferenceDocumentId { get; set; }

        /// <summary>مجموع شق المدين (يجب أن يساوي شق الدائن للترحيل)</summary>
        public decimal TotalDebit { get; set; }
        
        /// <summary>مجموع شق الدائن (Credit)</summary>
        public decimal TotalCredit { get; set; }

        /// <summary>حالة هذا القيد المالي</summary>
        public JournalEntryStatus Status { get; set; } = JournalEntryStatus.Draft;

        /// <summary>المستخدم المحاسب الذي قام بترحيل وإقفال هذا القيد واعتماده</summary>
        public Guid? PostedByUserId { get; set; }
        
        /// <summary>تاريخ الترحيل والنشر</summary>
        public DateTime? PostedAt { get; set; }

        /// <summary>الأسطر المكونة للقيد (المدين والدائن لكل حساب)</summary>
        public virtual ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
    }
}
