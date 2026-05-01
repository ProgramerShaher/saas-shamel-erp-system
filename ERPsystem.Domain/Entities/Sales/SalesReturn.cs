using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Sales;
using ERPsystem.Domain.Enums.Shared;

namespace ERPsystem.Domain.Entities.Sales
{
    /// <summary>
    /// مرتجع المبيعات من العميل، يتم عبره إرجاع البضاعة للمستودع وإعادة المال أو إثبات رصيد لصالحه.
    /// </summary>
    public class SalesReturn : TenantBaseEntity
    {
        /// <summary>رقم إيصال المرتجع</summary>
        public string ReturnNumber { get; set; } = null!;

        /// <summary>معرف الفرع</summary>
        public Guid BranchId { get; set; }

        /// <summary>معرف العميل (إن كان مسجلا)</summary>
        public Guid? CustomerId { get; set; }

        /// <summary>فاتورة البيع القديمة المراد إرجاعها</summary>
        public Guid SalesInvoiceId { get; set; }
        
        /// <summary>وثيقة الفاتورة المباعة سابقاً للمطابقة ومنع إرجاع بضاعة لم تباع</summary>
        public virtual SalesInvoice SalesInvoice { get; set; } = null!;

        /// <summary>زمن الإرجاع</summary>
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;

        /// <summary>أصل المبلغ المسترجع</summary>
        public decimal SubTotal { get; set; }
        
        /// <summary>الضريبة العكسية التي لا تدفع للهيئة</summary>
        public decimal TotalTax { get; set; }
        
        /// <summary>إجمالي المال المحسوب للعميل</summary>
        public decimal GrandTotal { get; set; }

        /// <summary>آلية صرف المرتجع (نقداً، رصيد في الفاتورة القادمة، استبدال)</summary>
        public ReturnRefundMethod RefundMethod { get; set; }
        
        /// <summary>هل سلمناه ماله؟</summary>
        public PaymentStatus RefundStatus { get; set; } = PaymentStatus.Unpaid;

        /// <summary>حالة القيد المحاسبي المولد للمرتجع</summary>
        public Guid? JournalEntryId { get; set; }

        /// <summary>الأسطر المردودة</summary>
        public virtual ICollection<SalesReturnLine> Lines { get; set; } = new List<SalesReturnLine>();
    }
}
