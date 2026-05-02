using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Inventory
{
    /// <summary>
    /// أسطر مستند التسوية والجرد التي تحوي التفاصيل الدقيقة للأرصدة المستكشفة.
    /// </summary>
    public class InventoryAdjustmentLine : TenantBaseEntity
    {
        /// <summary>معرف رأس مستند التسويات الرئيسي</summary>
        public Guid AdjustmentId { get; set; }
        
        /// <summary>مستند المعايرة والجرد الرئيسي</summary>
        public virtual InventoryAdjustment Adjustment { get; set; } = null!;

        /// <summary>معرف الدلالة للصنف المستكشف</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الموديل المتأثر</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>المقاس أو النموذج الصغير المتأثر</summary>
        public Guid? VariantId { get; set; }
        
        /// <summary>معرف الدفعة المفقودة المرتبطة بالدواء أو الصنف المعني</summary>
        public Guid? BatchLotId { get; set; }
        
        /// <summary>الدفعة المنتهية أو المعابة المفقودة</summary>
        public virtual BatchLot? BatchLot { get; set; }

        /// <summary>الكمية المتوقعة دفترياً ومسجلة بالنظام</summary>
        public decimal SystemQty { get; set; }
        
        /// <summary>الكمية المحصية يدوياً باليد والتي شاهدها الموظف</summary>
        public decimal PhysicalQty { get; set; }
        
        /// <summary>الفرق أو العجز المنجر من الطرح (الفعلي ناقص الدفتري)</summary>
        public decimal DifferenceQty => PhysicalQty - SystemQty;

        /// <summary>تكلفة الوحدة الخاسرة للتعويض في الأصول المالية</summary>
        public decimal UnitCost { get; set; }
        
        /// <summary>تقييم قيمة التسوية والعجز بالأموال الكلية على العميل</summary>
        public decimal TotalAdjustmentValue => DifferenceQty * UnitCost;

        /// <summary>سبب وعلة حدوث فقدان أو كسر أو حتى زيادة لهذا الصنف</summary>
        public string? AdjustmentReason { get; set; }
    }
}
