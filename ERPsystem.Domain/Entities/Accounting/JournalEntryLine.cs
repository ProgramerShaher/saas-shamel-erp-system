using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل سطر ضمن القيد المحاسبي المزدوج. قد يكون مديناً أو دائناً.
    /// </summary>
    public class JournalEntryLine : TenantBaseEntity
    {
        /// <summary>معرف القيد المحاسبي الأب الدال على هذه العملية الدفترية</summary>
        public Guid JournalEntryId { get; set; }
        
        /// <summary>معرف الحساب من شجرة الدليل المحاسبي</summary>
        public Guid AccountId { get; set; }
        
        /// <summary>المبلغ المدين، يجب أن يكون صفراً إذا كان السطر دائناً، ويشير للمبالغ الداخلة للصندوق والأصل</summary>
        public decimal DebitAmount { get; set; }
        
        /// <summary>المبلغ الدائن، يجب أن يكون صفراً إذا كان مديناً، ويشير للمبالغ الخارجة والالتزامات</summary>
        public decimal CreditAmount { get; set; }
        
        /// <summary>وصف أو تفصيل خاص بهذا السطر يختلف عن وصف القيد العام</summary>
        public string? Description { get; set; }
        
        /// <summary>رقم ترتيب السطر داخل القيد للفرز والتنظيم</summary>
        public int LineOrder { get; set; }

        // Navigation
        public virtual JournalEntry JournalEntry { get; set; } = null!;
        public virtual ChartOfAccount Account { get; set; } = null!;
    }
}
