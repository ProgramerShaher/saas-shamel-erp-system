using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// تعريف لخواص صنف المتغيرات والمقاسات (Variants Attributes).
    /// مثال للخاصية المتغيرة: اللون، المقاس، الحجم، نوع القماش.
    /// (هذه المعمارية مفعلة خصيصا لدعم محلات الملابس والأحذية والأقمشة بشكل مرن وممتاز).
    /// </summary>
    public class VariantAttribute : TenantBaseEntity
    {
        /// <summary>اسم ونوع الخاصية الجذري (مثال: لون التيشرت، سعة الذاكرة، المقاس البنطلون)</summary>
        public string AttributeName { get; set; } = null!;
        
        /// <summary>ترتيب عرض الخاصية للفلترة للزبون في العرض المرئي (مثلا اللون ثم المقاس)</summary>
        public int DisplayOrder { get; set; } = 0;
        
        /// <summary>هل الخاصية قابلة للاستخدام وتدشين مقاسات عليها</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>القيم الفرعية أو الساقطة من هذا المحور (لمحور الـ'مُقاس' هناك S, M, L...الخ)</summary>
        public virtual ICollection<VariantAttributeValue> Values { get; set; } = new List<VariantAttributeValue>();
    }
}
