using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل فاتورة مشتريات من المورد لتوريد البضاعة للمخزن.
    /// </summary>
    public class PurchaseInvoice : TenantBaseEntity
    {
        /// <summary>رقم الفاتورة الداخلي لدينا</summary>
        public string InvoiceNumber { get; set; } = string.Empty;
        
        /// <summary>تاريخ شراء البضاعة</summary>
        public DateTime InvoiceDate { get; set; }
        
        /// <summary>معرف المورد</summary>
        public Guid SupplierId { get; set; }
        
        /// <summary>الرقم المرجعي للفاتورة كما هي صادرة في نظام المورد (للمطابقة)</summary>
        public string? SupplierInvoiceRef { get; set; }
        
        /// <summary>المجموع الفرعي قبل الضريبة</summary>
        public decimal SubTotal { get; set; }
        
        /// <summary>قيمة الخصم إن وجد</summary>
        public decimal DiscountAmount { get; set; }
        
        /// <summary>إجمالي الضريبة عليها</summary>
        public decimal TaxAmount { get; set; }
        
        /// <summary>المبلغ الإجمالي للفاتورة</summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>المبلغ المسدد للمورد</summary>
        public decimal PaidAmount { get; set; }
        
        /// <summary>طريقة السداد للمورد</summary>
        public string PaymentMethod { get; set; } = "Cash";
        
        /// <summary>الحالة (مسودة، معتمدة، ملغاة)</summary>
        public string Status { get; set; } = "Draft";
        
        /// <summary>ملاحظات عن المشتريات</summary>
        public string? Notes { get; set; }

        // Navigation
        public virtual Supplier Supplier { get; set; } = null!;
        public virtual ICollection<PurchaseInvoiceItem> Items { get; set; } = new List<PurchaseInvoiceItem>();
        public virtual ICollection<PurchaseReturn> Returns { get; set; } = new List<PurchaseReturn>();
        public virtual ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }
}
