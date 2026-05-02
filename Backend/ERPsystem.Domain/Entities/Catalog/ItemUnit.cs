using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// جدول تحويل وأسعار وحدات القياس التجارية الخاصة بالصنف.
    /// مثال: الصنف الأساسي بالمستودع يقاس (بحبة). يمكن تخزين أن (الصندوق) يحوي (12 حبة). وكتابة سعر بيع للصندوق كامل.
    /// </summary>
    public class ItemUnit : TenantBaseEntity
    {
        /// <summary>معرف الصنف الأم</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف الأم</summary>
        public virtual Item Item { get; set; } = null!;

        /// <summary>معرف الوحدة التجميعية (الكرتون/الصندوق/الدرزن)</summary>
        public Guid UnitId { get; set; }
        
        /// <summary>وحدة التجميع</summary>
        public virtual UnitOfMeasure Unit { get; set; } = null!;

        /// <summary>معامل التحويل: كل وحدة تجميع كم فيها من الوحدة الأساسية (مثال: 12)</summary>
        public decimal ConversionFactor { get; set; } = 1;

        /// <summary>سعر شراء الوحدة التجميعية مباشرة من المورد (للجملة)</summary>
        public decimal? PurchasePrice { get; set; }
        
        /// <summary>سعر بيع الباقة/الكرتون ككل للزبون (تخصيص الخصم الضمني لشارء الصندوق أحيانا يكون أرخص من 12 حبة مفردة)</summary>
        public decimal? SalePrice { get; set; }

        /// <summary>حالة وحدة التجميع وإذا ما تزال سارية في التعامل</summary>
        public bool IsActive { get; set; } = true;
    }
}
