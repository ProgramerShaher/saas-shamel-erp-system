using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Inventory
{
    /// <summary>
    /// הרصيد الحالي الجاهز للاستعلام السريع لكل صنف في كل مستودع (Materialized View table).
    /// يُحدَّث مع كل حركة في الـ StockLedger لضمان سرعة عرض المخزون على الشاشات بدون حساب الملايين من الحركات كل مرة.
    /// </summary>
    public class WarehouseStock : TenantBaseEntity
    {
        /// <summary>الصنف المستهدف بالجرد والأرصدة</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>الصنف الخاص بمواصفات معينة كالحجم واللون (متغير)</summary>
        public Guid? VariantId { get; set; }
        
        /// <summary>المنتج المتغير</summary>
        public virtual Catalog.ItemVariant? Variant { get; set; }

        /// <summary>موقع المستودع المراقب رصيده</summary>
        public Guid WarehouseId { get; set; }
        
        /// <summary>المستودع</summary>
        public virtual Organization.Warehouse Warehouse { get; set; } = null!;

        /// <summary>الكمية الكلية الدفترية المحسوسة المتواجدة الآن</summary>
        public decimal QuantityOnHand { get; set; }
        
        /// <summary>الكميات المحجوزة للمبيعات أو أوامر النقل ولم تسلم بعد</summary>
        public decimal QuantityReserved { get; set; }

        /// <summary>الكمية السليمة والحرة المتاحة للاستخدام أو للبيع (الموجود نقص المحجوز)</summary>
        public decimal QuantityAvailable => QuantityOnHand - QuantityReserved;

        /// <summary>آخر مرة تم فيها تعديل أو ضرب الرصيد الزمني</summary>
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
