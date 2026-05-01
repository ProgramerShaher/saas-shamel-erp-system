using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// إدارة أنواع وحدات القياس للنظام.
    /// قطعة، حبة، كرتون، كيلوجرام، لتر، متر.
    /// </summary>
    public class UnitOfMeasure : TenantBaseEntity
    {
        /// <summary>اسم وحدة القياس</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>الرمز المختصر العلمي أو التجاري للوحدة (kg, pc, cl, m)</summary>
        public string Symbol { get; set; } = null!;

        /// <summary>هل تعتبر هذه الوحدة هي الوحدة الأساسية الأصغر التي يقييم عليها المخزون افتراضياً؟</summary>
        public bool IsBaseUnit { get; set; } = false;

        /// <summary>حالة العمل للوحدة (هل ما زالت مستخدمة أم ملقاة للتاريخ)</summary>
        public bool IsActive { get; set; } = true;
    }
}
