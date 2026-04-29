using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل تفاصيل صنف في فاتورة المشتريات.
    /// </summary>
    public class PurchaseInvoiceItem : TenantBaseEntity
    {
        /// <summary>معرف فاتورة المشتريات الأم</summary>
        public Guid PurchaseInvoiceId { get; set; }
        
        /// <summary>معرف المنتج المشترى</summary>
        public Guid ProductId { get; set; }
        
        /// <summary>الكمية المشتراة وتضاف للمخزون</summary>
        public decimal Quantity { get; set; }
        
        /// <summary>تكلفة الوحدة الواحدة المشتراة</summary>
        public decimal UnitCost { get; set; }
        
        /// <summary>إجمالي التكلفة لهذا السطر</summary>
        public decimal TotalCost { get; set; }

        // Navigation
        public virtual PurchaseInvoice PurchaseInvoice { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
