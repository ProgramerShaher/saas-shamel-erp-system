using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Inventory
{
    /// <summary>
    /// السطر التفصيلي داخل أمر التحويل المرفق بكل كميات الأصناف المشحونة.
    /// </summary>
    public class StockTransferLine : TenantBaseEntity
    {
        /// <summary>معرف أمر التحويل الأقدم للأعلى</summary>
        public Guid TransferOrderId { get; set; }
        
        /// <summary>أمر التحويل المباشر للأم</summary>
        public virtual StockTransferOrder TransferOrder { get; set; } = null!;

        /// <summary>التعريف بالصنف المنقول</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف الرئيسي المتفرع</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>تخصيص الصنف المنقول بمقاسه ولونه (المتغيرات)</summary>
        public Guid? VariantId { get; set; }
        
        /// <summary>المنتج المتغير والمعدل</summary>
        public virtual Catalog.ItemVariant? Variant { get; set; }

        /// <summary>وحدة المعايرة لنقل المنتج المعني (نقل كرتون أم درزن؟)</summary>
        public Guid UnitId { get; set; }
        
        /// <summary>الوحدة المحتسبة للقياس</summary>
        public virtual Catalog.UnitOfMeasure Unit { get; set; } = null!;

        /// <summary>الكمية المطالب بها مبدئياً للنقل</summary>
        public decimal RequestedQty { get; set; }
        
        /// <summary>الكمية التي بالفعل خرجت من المستودع الأول</summary>
        public decimal SentQty { get; set; }
        
        /// <summary>الكمية التي وصلت للمستودع الثاني واعتمدها المستقبل (أحيانا تكون أقل بسبب التلف)</summary>
        public decimal ReceivedQty { get; set; }

        /// <summary>رقم تشغيلة للصنف للسلع الدوائية</summary>
        public string? BatchNumber { get; set; }
        
        /// <summary>صلاحية المواد المنقولة</summary>
        public DateTime? ExpiryDate { get; set; }
        
        /// <summary>التكلفة الموثقة للنقل للعملية المحاسبية</summary>
        public decimal UnitCost { get; set; }
    }
}
