using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Shared;

namespace ERPsystem.Domain.Entities.Purchasing
{
    /// <summary>
    /// مرتجع المشتريات (يُرجع البضاعة من المستودع للمورد، ويثبت مديونية المورد لنا).
    /// </summary>
    public class PurchaseReturn : TenantBaseEntity
    {
        /// <summary>الرقم الداخلي للمرتجع</summary>
        public string ReturnNumber { get; set; } = null!;

        /// <summary>المورد المعاد إليه</summary>
        public Guid SupplierId { get; set; }
        
        /// <summary>المورد</summary>
        public virtual Supplier Supplier { get; set; } = null!;

        /// <summary>الفرع المُرجع</summary>
        public Guid BranchId { get; set; }
        
        /// <summary>الفرع المرتبط</summary>
        public virtual Organization.Branch Branch { get; set; } = null!;

        /// <summary>فاتورة الشراء الأم التي نرجع منها القطع</summary>
        public Guid PurchaseInvoiceId { get; set; }
        
        /// <summary>الفاتورة الأصلية</summary>
        public virtual PurchaseInvoice PurchaseInvoice { get; set; } = null!;

        /// <summary>تاريخ الإرجاع</summary>
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;

        /// <summary>أقسام إجمالي قيمة المستردات العكسية</summary>
        public decimal SubTotal { get; set; }
        
        /// <summary>الخصم المردود</summary>
        public decimal TotalDiscount { get; set; }
        
        /// <summary>الضريبة العكسية المعادة</summary>
        public decimal TotalTax { get; set; }
        
        /// <summary>صافي المرتجع الإجمالي</summary>
        public decimal GrandTotal { get; set; }

        /// <summary>حالة الدفع الخاصة بسداد المرجوعات</summary>
        public PaymentStatus RefundStatus { get; set; } = PaymentStatus.Unpaid;
        
        /// <summary>المبلغ الذي سدده المورد لنا كاش أو بنك</summary>
        public decimal RefundAmountReceived { get; set; } = 0;

        /// <summary>القيد العكسي المولد</summary>
        public Guid? JournalEntryId { get; set; }

        /// <summary>ملاحظات الاسترجاع كعيوب مصنعية</summary>
        public string? Notes { get; set; }

        /// <summary>قائمة العناصر المردودة</summary>
        public virtual ICollection<PurchaseReturnLine> Lines { get; set; } = new List<PurchaseReturnLine>();
    }
}
