using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// القيم المتعددة للمحور/الخاصية (Variant Attribute).
    /// مثال: لمحور اللون هناك: أحمر، أخضر، أصفر.
    /// </summary>
    public class VariantAttributeValue : TenantBaseEntity
    {
        /// <summary>معرف الخاصية/المحور الأم (اللون)</summary>
        public Guid AttributeId { get; set; }
        
        /// <summary>المحور الأم</summary>
        public virtual VariantAttribute Attribute { get; set; } = null!;

        /// <summary>القيمة الفعلية للخاصية (S, M, أو أحمر)</summary>
        public string Value { get; set; } = null!;
        
        /// <summary>كود الهيكس المرئي للون ليظهر كدائرة ملونة في المتجر الإلكتروني إن أردنا</summary>
        public string? ColorHex { get; set; }
        
        /// <summary>ترتيب القيمة في الظهور عند الفلترة</summary>
        public int DisplayOrder { get; set; } = 0;
    }
}
