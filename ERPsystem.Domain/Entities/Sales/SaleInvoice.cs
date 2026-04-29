using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل فاتورة البيع الصادرة للعميل (نقطة البيع أو فاتورة آجلة).
    /// </summary>
    public class SaleInvoice : TenantBaseEntity
    {
        /// <summary>رقم الفاتورة المتسلسل</summary>
        public string InvoiceNumber { get; set; } = string.Empty;
        
        /// <summary>تاريخ ووقت إصدار الفاتورة</summary>
        public DateTime InvoiceDate { get; set; }
        
        /// <summary>معرف العميل (اختياري في حال كان البيع نقدي مباشر للعامة)</summary>
        public Guid? CustomerId { get; set; }
        
        /// <summary>معرف المستخدم (الكاشير) الذي قام بإصدار الفاتورة</summary>
        public Guid CashierId { get; set; }
        
        /// <summary>المجموع الفرعي للفاتورة قبل الخصومات والضرائب</summary>
        public decimal SubTotal { get; set; }
        
        /// <summary>النسبة المئوية للخصم الإجمالي على الفاتورة</summary>
        public decimal DiscountPercent { get; set; }
        
        /// <summary>قيمة الخصم الإجمالي على الفاتورة</summary>
        public decimal DiscountAmount { get; set; }
        
        /// <summary>نسبة ضريبة القيمة المضافة أو غيرها</summary>
        public decimal TaxPercent { get; set; }
        
        /// <summary>إجمالي القيمة المؤخوذة كضريبة</summary>
        public decimal TaxAmount { get; set; }
        
        /// <summary>مبلغ الفاتورة النهائي المستحق للدفع</summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>المبلغ الذي سدده العميل بالفعل</summary>
        public decimal PaidAmount { get; set; }
        
        /// <summary>باقي التغيير المرجع للعميل المتبقي من الدفع النقدي</summary>
        public decimal ChangeAmount { get; set; }
        
        /// <summary>طريقة الدفع (كاش، بطاقة، آجل)</summary>
        public string PaymentMethod { get; set; } = "Cash";
        
        /// <summary>حالة الفاتورة (مدفوعة، ملغاة، جزئي)</summary>
        public string Status { get; set; } = "Paid";
        
        /// <summary>ملاحظات إضافية حول الفاتورة</summary>
        public string? Notes { get; set; }

        // Navigation
        public virtual Customer? Customer { get; set; }
        public virtual User Cashier { get; set; } = null!;
        public virtual ICollection<SaleInvoiceItem> Items { get; set; } = new List<SaleInvoiceItem>();
        public virtual ICollection<SaleReturn> Returns { get; set; } = new List<SaleReturn>();
        public virtual ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }
}
