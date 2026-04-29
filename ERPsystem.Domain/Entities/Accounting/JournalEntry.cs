using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل القيد المحاسبي اليومي سواء كان قيداً إلكترونياً تلقائياً أم قيداً يدوياً.
    /// </summary>
    public class JournalEntry : TenantBaseEntity
    {
        /// <summary>الرقم التسلسلي أو المسلسل للقيد المحاسبي</summary>
        public string EntryNumber { get; set; } = string.Empty;
        
        /// <summary>تاريخ القيد</summary>
        public DateTime EntryDate { get; set; }
        
        /// <summary>بيان وشرح القيد العام</summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>نوع العملية (يدوي أم نظامي آلي Auto/Manual)</summary>
        public string EntryType { get; set; } = string.Empty;  
        
        /// <summary>المصدر الأساسي للقيد (מبيعات، مشتريات، إرجاع، رواتب)</summary>
        public string SourceType { get; set; } = string.Empty; 
        
        /// <summary>المعرف الفريد للسجل الأصلي كالفاتورة وغيرها إن وجد للربط</summary>
        public Guid? SourceId { get; set; }                    
        
        /// <summary>مجموع المبالغ المدينة، ولابد أن يتساوى مع الدائن</summary>
        public decimal TotalDebit { get; set; }
        
        /// <summary>مجموع المبالغ الدائنة، ولابد أن يتساوى مع المدين</summary>
        public decimal TotalCredit { get; set; }
        
        /// <summary>حالة القيد هل تم ترحيله للحسابات الختامية أم لا</summary>
        public bool IsPosted { get; set; } = true;

        // مفاتيح أجنبية للربط المباشر بالمصادر لسهولة الوصول
        public Guid? SaleInvoiceId { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public Guid? ExpenseId { get; set; }

        // Navigation
        public virtual ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
    }
}
