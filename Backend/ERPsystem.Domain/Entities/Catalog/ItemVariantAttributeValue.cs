using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// جدول التوزيع الذي يربط المنتج الفرعي النهائي بصفاته المتغيرة (مقاسه ولونه).
    /// </summary>
    public class ItemVariantAttributeValue : TenantBaseEntity
    {
        /// <summary>معرف المتغير الفرعي (القميص الأبيض مقاس لارج)</summary>
        public Guid VariantId { get; set; }
        
        /// <summary>المنتج المتغير</summary>
        public virtual ItemVariant Variant { get; set; } = null!;

        /// <summary>معرف القيمة المحددة (مثلا القيمة: لارج)</summary>
        public Guid AttributeValueId { get; set; }
        
        /// <summary>قيمة الخاصية</summary>
        public virtual VariantAttributeValue AttributeValue { get; set; } = null!;
    }
}
