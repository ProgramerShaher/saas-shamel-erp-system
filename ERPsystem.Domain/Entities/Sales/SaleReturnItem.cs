using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل سطراً ضمن مستند الإرجاع وتفاصيل الصنف المسترجع الخاص به.
    /// </summary>
    public class SaleReturnItem : TenantBaseEntity
    {
        /// <summary>معرف فاتورة المرتجع</summary>
        public Guid SaleReturnId { get; set; }
        
        /// <summary>معرف المنتج المسترجع للمخزن</summary>
        public Guid ProductId { get; set; }
        
        /// <summary>الكمية المسترجعة</summary>
        public decimal Quantity { get; set; }
        
        /// <summary>سعر وحدة المنتج المسترد</summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>إجمالي السطر المسترجع</summary>
        public decimal TotalPrice { get; set; }

        // Navigation
        public virtual SaleReturn SaleReturn { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
