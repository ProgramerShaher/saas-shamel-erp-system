using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل الأصناف الواردة في جرد التسوية واختلاف الكميات بين النظام والواقع.
    /// </summary>
    public class InventoryAdjustmentItem : TenantBaseEntity
    {
        /// <summary>معرف تسوية المخزون الأب</summary>
        public Guid AdjustmentId { get; set; }
        
        /// <summary>معرف المنتج المعني في الجرد</summary>
        public Guid ProductId { get; set; }
        
        /// <summary>الكمية المسجلة حالياً في النظام</summary>
        public decimal SystemQuantity { get; set; }
        
        /// <summary>الكمية الفعلية التي تم جردها بالواقع</summary>
        public decimal ActualQuantity { get; set; }
        
        /// <summary>الفرق بين الكمية الفعلية وكمية النظام</summary>
        public decimal Difference { get; set; }

        // Navigation
        public virtual InventoryAdjustment Adjustment { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
