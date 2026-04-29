using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل مستند القبض لتسجيل المبالغ المحصلة من الزبائن مقابل ديون أو فواتير.
    /// </summary>
    public class CustomerPayment : TenantBaseEntity
    {
        /// <summary>رقم السند المرجعي للقبض</summary>
        public string PaymentNumber { get; set; } = string.Empty;
        
        /// <summary>العميل دافع المبلغ</summary>
        public Guid CustomerId { get; set; }
        
        /// <summary>تاريخ دفع المستحقات</summary>
        public DateTime PaymentDate { get; set; }
        
        /// <summary>المبلغ الإجمالي المحصل</summary>
        public decimal Amount { get; set; }
        
        /// <summary>وسيلة الدفع (نقداً، بنك.. إلخ)</summary>
        public string PaymentMethod { get; set; } = "Cash";
        
        /// <summary>الملاحظات وغيرها حول الدفع</summary>
        public string? Notes { get; set; }

        // Navigation
        public virtual Customer Customer { get; set; } = null!;
    }
}
