using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Purchasing
{
    /// <summary>
    /// سطر تفصيلي لأمر الشراء
    /// </summary>
    public class PurchaseOrderLine : TenantBaseEntity
    {
        /// <summary>معرف الطلب الأب</summary>
        public Guid PurchaseOrderId { get; set; }
        
        /// <summary>الطلب الأم</summary>
        public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;

        /// <summary>الصنف المطلوب</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>الصنف المتغير المطلوب</summary>
        public Guid? VariantId { get; set; }
        
        /// <summary>النوع المعين</summary>
        public virtual Catalog.ItemVariant? Variant { get; set; }

        /// <summary>وحدة المعايرة المأمور بها للشراء (كرتون مثلا)</summary>
        public Guid UnitId { get; set; }
        
        /// <summary>الوحدة</summary>
        public virtual Catalog.UnitOfMeasure Unit { get; set; } = null!;

        /// <summary>الكمية المطلوبة</summary>
        public decimal OrderedQty { get; set; }
        
        /// <summary>الكمية التي وصلت فعلا وتحولت لفاتورة (إذا كانت أقل فالطلبية لم تكتمل)</summary>
        public decimal ReceivedQty { get; set; }

        /// <summary>السعر المتفق عليه مبدئيا للوحدة</summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>إجمالي السعر الصافي للسطر</summary>
        public decimal TotalPrice => OrderedQty * UnitPrice;
    }
}
