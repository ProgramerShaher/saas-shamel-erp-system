using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل سطراً ضمن فاتورة البيع (تفاصيل المنتجات المباعة).
    /// </summary>
    public class SaleInvoiceItem : TenantBaseEntity
    {
        /// <summary>معرف فاتورة البيع التي ينتمي إليها السطر</summary>
        public Guid SaleInvoiceId { get; set; }
        
        /// <summary>المعرف الخاص بالمنتج المباع</summary>
        public Guid ProductId { get; set; }
        
        /// <summary>الكمية المباعة من هذا الصنف</summary>
        public decimal Quantity { get; set; }
        
        /// <summary>سعر بيع الوحدة الواحدة</summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>النسبة المئوية للخصم على هذا السطر فقط</summary>
        public decimal DiscountPercent { get; set; }
        
        /// <summary>قيمة الخصم المالي على هذا السطر فقط</summary>
        public decimal DiscountAmount { get; set; }
        
        /// <summary>إجمالي سعر هذا السطر بعد الكمية والخصم</summary>
        public decimal TotalPrice { get; set; }

        // Navigation
        public virtual SaleInvoice SaleInvoice { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
