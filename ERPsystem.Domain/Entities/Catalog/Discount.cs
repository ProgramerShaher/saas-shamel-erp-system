using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Catalog;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// جدول الخصومات (Discounts) القابلة للتطبيق تلقائيا في نقاط البيع والفواتير.
    /// يدعم الخصم بالنسب أو المبالغ، وتحديد فترة سريانه، وهدفه.
    /// </summary>
    public class Discount : TenantBaseEntity
    {
        /// <summary>اسم وقالب الخصم (مثال: عروض اليوم الوطني، خصم الشتاء)</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>أنماط وأشكال الخصم (نسبة 15%، 20 ريال مقطوع، اشتر 1 وخذ 1)</summary>
        public DiscountType DiscountType { get; set; }
        
        /// <summary>القيمة الرياضية المراد تطبيقها كخصم</summary>
        public decimal Value { get; set; }

        /// <summary>نطاق تطبيق الخصم (على الفاتورة بجميع أصنافها، أم قسم المكسرات فقط)</summary>
        public DiscountAppliesTo AppliesTo { get; set; }

        /// <summary>معرف الكيان المستهدف (إذا كان الخصم لقسم معين فهو ID القسم، وإن كان لصنف فهو ID الصنف) وإذا الكل يترك Null</summary>
        public Guid? TargetId { get; set; }

        /// <summary>بدء السريان الزمني للخصم (تاريخ تدشين العرض)</summary>
        public DateTime ValidFrom { get; set; }
        
        /// <summary>انتهاء صلاحية هذا العرض الترويجي (لإيقاف الخصم آليا)</summary>
        public DateTime? ValidTo { get; set; }

        /// <summary>فرع محدد يتم تخصيص هذا الخصم له ترويجيا لرفع مبيعاته (Null يعني متاح لكل فروع المنشأة)</summary>
        public Guid? BranchId { get; set; }
        
        /// <summary>الفرع المختص</summary>
        public virtual Organization.Branch? Branch { get; set; }

        /// <summary>أقل مبلغ مدفوع في الفاتورة أو الكمية المطلوبة لتفعيل هذا الخصم التلقائي</summary>
        public decimal? MinPurchaseAmount { get; set; }
        
        /// <summary>حالة استمرارية تفعيل هذا الخصم</summary>
        public bool IsActive { get; set; } = true;
    }
}
