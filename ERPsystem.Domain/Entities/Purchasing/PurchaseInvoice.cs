using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Shared;

namespace ERPsystem.Domain.Entities.Purchasing
{
    /// <summary>
    /// فاتورة الشراء النهائية المستلمة من المورد (يزيد بها المخزون وتتولد عنها قيود محاسبية).
    /// </summary>
    public class PurchaseInvoice : TenantBaseEntity
    {
        /// <summary>رقم الفاتورة الداخلي</summary>
        public string InvoiceNumber { get; set; } = null!;
        
        /// <summary>رقم الفاتورة الورقية الخاصة بالمورد للمطابقة</summary>
        public string? SupplierInvoiceNumber { get; set; }

        /// <summary>المورد الذي صدرت منه الفاتورة</summary>
        public Guid SupplierId { get; set; }
        
        /// <summary>المورد</summary>
        public virtual Supplier Supplier { get; set; } = null!;

        /// <summary>الفرع المستلم للبضاعة والمعني بالتسجيل</summary>
        public Guid BranchId { get; set; }
        
        /// <summary>الفرع</summary>
        public virtual Organization.Branch Branch { get; set; } = null!;

        /// <summary>أمر الشراء الذي تولدت منه هذه الفاتورة (إن وجد)</summary>
        public Guid? PurchaseOrderId { get; set; }
        
        /// <summary>أمر التوريد الأصلي</summary>
        public virtual PurchaseOrder? PurchaseOrder { get; set; }

        /// <summary>تاريخ استلام وتشغيل الفاتورة</summary>
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        /// <summary>إجمالي المبلغ قبل الخصم والضريبة</summary>
        public decimal SubTotal { get; set; }
        
        /// <summary>إجمالي الخصومات المستحقة من المورد</summary>
        public decimal TotalDiscount { get; set; }
        
        /// <summary>القيمة المضافة المستحقة للاسترداد</summary>
        public decimal TotalTax { get; set; }
        
        /// <summary>صافي الفاتورة الإجمالي المطالب بسداده للمورد</summary>
        public decimal GrandTotal { get; set; }

        /// <summary>حالة دفع الفاتورة (مدفوع بالكامل، أو جزئي، أو غير مدفوع)</summary>
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        
        /// <summary>المبلغ الذي تم تسديده من قيمة الفاتورة حتى الآن</summary>
        public decimal AmountPaid { get; set; } = 0;

        /// <summary>المعرف المحاسبي للقيد المولد من الفاتورة</summary>
        public Guid? JournalEntryId { get; set; }

        /// <summary>مرفقات ووثائق مصورة للفاتورة</summary>
        public string? AttachmentUrl { get; set; }

        /// <summary>تسجيل المشتريات الملموسة في الفاتورة</summary>
        public virtual ICollection<PurchaseInvoiceLine> Lines { get; set; } = new List<PurchaseInvoiceLine>();
    }
}
