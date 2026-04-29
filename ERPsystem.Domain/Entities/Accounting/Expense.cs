using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل المصروف الفعلي المدفوع لأي بند داخل الشركة.
    /// </summary>
    public class Expense : TenantBaseEntity
    {
        /// <summary>الرقم التسلسلي لسند الصرف</summary>
        public string ExpenseNumber { get; set; } = string.Empty;
        
        /// <summary>تاريخ دفع المصروف</summary>
        public DateTime ExpenseDate { get; set; }
        
        /// <summary>معرف فئة هذا المصروف</summary>
        public Guid ExpenseCategoryId { get; set; }
        
        /// <summary>قيمة المصروف المدفوع</summary>
        public decimal Amount { get; set; }
        
        /// <summary>طريقة الدفع (كاش، بنكي)</summary>
        public string PaymentMethod { get; set; } = "Cash";
        
        /// <summary>البيان أو الوصف الذي توضح سبب الصرف</summary>
        public string? Description { get; set; }
        
        /// <summary>مسار مرفق الفاتورة أو وصل الصرف</summary>
        public string? AttachmentUrl { get; set; }

        // Navigation
        public virtual ExpenseCategory ExpenseCategory { get; set; } = null!;
        public virtual ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }
}
