using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل مستند صرف أو دفتر دفعات المبالغ التي يسددها المركز للموردين أو الدائنين.
    /// </summary>
    public class SupplierPayment : TenantBaseEntity
    {
        /// <summary>رقم إيصال التحويل للمورد أو سند الصرف</summary>
        public string PaymentNumber { get; set; } = string.Empty;
        
        /// <summary>المورد الذي استلم المبلغ كدائن</summary>
        public Guid SupplierId { get; set; }
        
        /// <summary>تاريخ تحويل وخصم واستحقاق الدفعة</summary>
        public DateTime PaymentDate { get; set; }
        
        /// <summary>القيمة المدفوعة</summary>
        public decimal Amount { get; set; }
        
        /// <summary>طريقة التسديد</summary>
        public string PaymentMethod { get; set; } = "Cash";
        
        /// <summary>تعليق وملاحظات على الدفعة</summary>
        public string? Notes { get; set; }

        // Navigation
        public virtual Supplier Supplier { get; set; } = null!;
    }
}
