using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Sales;
using ERPsystem.Domain.Enums.Shared;

namespace ERPsystem.Domain.Entities.Sales
{
    /// <summary>
    /// فاتورة المبيعات الضريبية المبسطة أو الشاملة، التي تصدر للكاشير والتجزئة، أو للجملة.
    /// </summary>
    public class SalesInvoice : TenantBaseEntity
    {
        /// <summary>الرقم التسلسلي للفاتورة الخاص بالعميل وتنسيقات الضرائب (INV-0001)</summary>
        public string InvoiceNumber { get; set; } = null!;

        /// <summary>معرف الفرع الذي باع وصدرت منه الفاتورة</summary>
        public Guid BranchId { get; set; }
        
        /// <summary>الفرع البائع</summary>
        public virtual Organization.Branch Branch { get; set; } = null!;

        /// <summary>العميل المستفيد من البيع، يكون Null لعميل نقدي سريع إذا لم يدخل معلوماته</summary>
        public Guid? CustomerId { get; set; }
        
        /// <summary>العميل المشتري</summary>
        public virtual Customer? Customer { get; set; }

        /// <summary>نوع الفاتورة (فاتورة كاشير تجزئة مباشر، أو فاتورة مبيعات أجل، فاتورة جملة)</summary>
        public SalesInvoiceType InvoiceType { get; set; }

        /// <summary>تاريخ واصدار الفاتورة</summary>
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        /// <summary>الوردية التي تم تسجيل هذه الفاتورة المرئية تحتها (إذا كانت فاتورة كاشير نقاط بيع)</summary>
        public Guid? PosShiftId { get; set; }
        
        /// <summary>الوردية</summary>
        public virtual PosShift? PosShift { get; set; }

        /// <summary>إجمالي المبلغ الأصلي للخدمة أو المنتجات</summary>
        public decimal SubTotal { get; set; }
        
        /// <summary>إجمالي التخفيضات سواء الكوبونات أو عروض اليوم</summary>
        public decimal TotalDiscount { get; set; }
        
        /// <summary>ضريبة الفاتورة الملزمة للمطالبة من العميل</summary>
        public decimal TotalTax { get; set; }
        
        /// <summary>الإجمالي النهائي المطلوب للدفع من العميل</summary>
        public decimal GrandTotal { get; set; }

        /// <summary>حالة الدفع للفاتورة (كثيرا ما تكون مدفوعة بالكامل في التجزئة، وفي الجملة بالآجل)</summary>
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        
        /// <summary>ما تم دفعه بالفعل حتى اللحظة من قيمة الفاتورة</summary>
        public decimal AmountPaid { get; set; } = 0;

        /// <summary>القيد المحاسبي المترجم لعملية البيع من حساب الإيرادات وتكلفة المبيعات والصندوق</summary>
        public Guid? JournalEntryId { get; set; }

        /// <summary>قائمة المنتجات المباعة في تفاصيل هذه الفاتورة</summary>
        public virtual ICollection<SalesInvoiceLine> Lines { get; set; } = new List<SalesInvoiceLine>();
        
        /// <summary>أقساط الدفع والوسائل البنكية أو النقدية السريعة المرتبطة</summary>
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
