using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Sales;

namespace ERPsystem.Domain.Entities.Sales
{
    /// <summary>
    /// ورقة وسند الدفع — قد يدفع العميل جزء كاش وجزء بالفيزا في نفس الفاتورة.
    /// هذا الجدول يوزع الدفعات.
    /// </summary>
    public class Payment : TenantBaseEntity
    {
        /// <summary>رقم مرجع السند للعمليات المستقلة</summary>
        public string PaymentNumber { get; set; } = null!;

        /// <summary>الفاتورة المتعلقة بها هذه الدفعة إن وجد</summary>
        public Guid? SalesInvoiceId { get; set; }
        
        /// <summary>فاتورة الشراء المتعلقة (إذا كانت مدفوعات لمورد)</summary>
        public Guid? PurchaseInvoiceId { get; set; }

        /// <summary>العميل الدافع</summary>
        public Guid? CustomerId { get; set; }
        
        /// <summary>المورد المدفوع له</summary>
        public Guid? SupplierId { get; set; }

        /// <summary>معرف وردية الكاشير التي قبضت هذا المال لمطابقة الدرج</summary>
        public Guid? PosShiftId { get; set; }

        /// <summary>آلية الدفع (نقدي، شبكة، حوالة)</summary>
        public PaymentMethod Method { get; set; }

        /// <summary>المبلغ المسدد والمدفوع فعليا</summary>
        public decimal Amount { get; set; }
        
        /// <summary>تاريخ تلقي المال</summary>
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        /// <summary>معرف الخزنة (للكاش) أو الحساب البنكي (للشبكة) الذي سقط فيه المال</summary>
        public Guid AccountOrSafeId { get; set; }

        /// <summary>رقم الموافقة للعمليات البنكية (Approval Code)</summary>
        public string? TransactionReference { get; set; }
        
        /// <summary>سند القيد المحاسبي</summary>
        public Guid? JournalEntryId { get; set; }
    }
}
