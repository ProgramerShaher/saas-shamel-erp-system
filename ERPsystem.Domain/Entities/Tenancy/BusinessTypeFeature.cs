using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Domain.Entities.Tenancy
{
    /// <summary>
    /// قوالب الميزات الافتراضية لكل نوع نشاط تجاري.
    /// يسمح لمدير النظام بتحديد الميزات التي تظهر تلقائياً للصيدلية أو السوبر ماركت مثلاً.
    /// هذا الكيان عالمي (System-Level) ولا يتبع لمنشأة محددة.
    /// </summary>
    public class BusinessTypeFeature : BaseEntity
    {
        /// <summary>نوع النشاط التجاري (صيدلية، سوبر ماركت، الخ)</summary>
        public BusinessType BusinessType { get; set; }

        /// <summary>مفتاح الميزة (Feature Key) من الـ Enum</summary>
        public FeatureKey FeatureKey { get; set; }

        /// <summary>هل هذه الميزة مفعلة افتراضياً لهذا النشاط؟</summary>
        public bool IsEnabledByDefault { get; set; } = true;
    }
}
